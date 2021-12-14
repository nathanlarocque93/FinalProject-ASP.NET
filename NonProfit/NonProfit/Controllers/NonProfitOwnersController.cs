 using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NonProfit.Models;

namespace NonProfit.Controllers
{
    [Authorize(Roles = "superadmin")]
    public class NonProfitOwnersController : Controller
    {
        private readonly DataContext _context;

        public NonProfitOwnersController(DataContext context)
        {
            _context = context;
        }

        // GET: NonProfitOwners
        public async Task<IActionResult> Index()
        {
            return View(await _context.NonProfitOwner.ToListAsync());
        }

        // GET: NonProfitOwners/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nonProfitOwner = await _context.NonProfitOwner
                .FirstOrDefaultAsync(m => m.ownerID == id);
            if (nonProfitOwner == null)
            {
                return NotFound();
            }

            return View(nonProfitOwner);
        }

        // GET: NonProfitOwners/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: NonProfitOwners/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ownerID,firstname,lastname,phone,email,city,street,postalcode,salary")] NonProfitOwner nonProfitOwner)
        {
            if (ModelState.IsValid)
            {
                _context.Add(nonProfitOwner);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(nonProfitOwner);
        }

        // GET: NonProfitOwners/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nonProfitOwner = await _context.NonProfitOwner.FindAsync(id);
            if (nonProfitOwner == null)
            {
                return NotFound();
            }
            return View(nonProfitOwner);
        }

        // POST: NonProfitOwners/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ownerID,firstname,lastname,phone,email,city,street,postalcode,salary")] NonProfitOwner nonProfitOwner)
        {
            if (id != nonProfitOwner.ownerID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(nonProfitOwner);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NonProfitOwnerExists(nonProfitOwner.ownerID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(nonProfitOwner);
        }

        // GET: NonProfitOwners/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nonProfitOwner = await _context.NonProfitOwner
                .FirstOrDefaultAsync(m => m.ownerID == id);
            if (nonProfitOwner == null)
            {
                return NotFound();
            }

            return View(nonProfitOwner);
        }

        // POST: NonProfitOwners/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var nonProfitOwner = await _context.NonProfitOwner.FindAsync(id);
            _context.NonProfitOwner.Remove(nonProfitOwner);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NonProfitOwnerExists(int id)
        {
            return _context.NonProfitOwner.Any(e => e.ownerID == id);
        }
    }
}
