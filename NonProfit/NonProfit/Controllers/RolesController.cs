using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NonProfit.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NonProfit.Controllers
{
    [Authorize(Roles = "superadmin")]
    public class RolesController : Controller
    {
        private RoleManager<IdentityRole> roleManager;
        private UserManager<IdentityUser> userManager;

        public RolesController(RoleManager<IdentityRole> roleManager,
            UserManager<IdentityUser> userManager)
        {

            this.roleManager = roleManager;
            this.userManager = userManager;
        }

        public IActionResult Index()
        {
            return View(roleManager.Roles);
        }

        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult>
            Create([Bind("RoleName")] RoleModel roleModel)
        {
            if (ModelState.IsValid)
            {

                IdentityResult result = await roleManager
                        .CreateAsync(new IdentityRole(roleModel.RoleName));

                if (result.Succeeded)
                {

                    return RedirectToAction("Index");
                }
                else
                {

                    return Content("Error on creating Role!");

                }
                //if not, do something else
            }
            else
            {
                return Content("Error on creating Role!");


            }


            return View(roleModel);
        }

        [HttpGet]
        public async Task<IActionResult> Update(string id)
        {

            IdentityRole role = await roleManager.FindByIdAsync(id);

            if (role != null)
            {
                List<IdentityUser> members = new List<IdentityUser>();
                List<IdentityUser> nonmembers = new List<IdentityUser>();

                foreach (IdentityUser user in userManager.Users)
                {

                    //if user belongs to the specified Role
                    bool IsInRole = await userManager.IsInRoleAsync(user, role.Name);

                    if (IsInRole)
                    {
                        members.Add(user);
                    }
                    else
                    {
                        nonmembers.Add(user);
                    }
                }

                RoleUpdateModel model = new RoleUpdateModel
                {
                    Role = role,
                    Members = members,
                    NonMembers = nonmembers

                };

                return View(model);

                //were able to find role by ID
            }
            else
            {
                return Content("Unable to find view with ID = " + id);
            }


        }

        [HttpPost]
        public async Task<IActionResult> Update(RoleModification model)
        {
            IdentityResult result;
            if (ModelState.IsValid)
            {
                foreach (string userId in model.AddIds ?? new string[] { })
                {
                    IdentityUser user = await userManager.FindByIdAsync(userId);
                    if (user != null)
                    {
                        result = await userManager.AddToRoleAsync(user, model.RoleName);
                        if (!result.Succeeded)
                            return Content("Unable to add user to role");
                    }
                }
                foreach (string userId in model.DeleteIds ?? new string[] { })
                {
                    IdentityUser user = await userManager.FindByIdAsync(userId);
                    if (user != null)
                    {
                        result = await userManager.RemoveFromRoleAsync(user, model.RoleName);
                        if (!result.Succeeded)
                            return Content("Unable to remove user from role");
                    }
                }
            }

            if (ModelState.IsValid)
                return RedirectToAction(nameof(Index));
            else
                return await Update(model.RoleId);
        }

        public async Task<IActionResult> Delete(string id)
        {
            IdentityRole role = await roleManager.FindByIdAsync(id);
            if (role != null)
            {
                IdentityResult result = await roleManager.DeleteAsync(role);
                if (result.Succeeded)
                    return RedirectToAction("Index");
                else
                    return Content("Role could not be deleted");
            }
            else
                ModelState.AddModelError("", "No role found");
            return View("Index", roleManager.Roles);
        }
    }
}
