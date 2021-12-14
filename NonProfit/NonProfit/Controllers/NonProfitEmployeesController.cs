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
    [Authorize(Roles = "owner")]
    public class NonProfitEmployeesController : Controller
    {
        private readonly DataContext _context;

        public NonProfitEmployeesController(DataContext context)
        {
            _context = context;
        }

        // GET: NonProfitEmployees
        public async Task<IActionResult> Index()
        {
            var dataContext = _context.NonProfitEmployee.Include(n => n.NonProfitLocation);
            return View(await dataContext.ToListAsync());
        }

        // GET: NonProfitEmployees/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nonProfitEmployee = await _context.NonProfitEmployee
                .Include(n => n.NonProfitLocation)
                .FirstOrDefaultAsync(m => m.employeeID == id);
            if (nonProfitEmployee == null)
            {
                return NotFound();
            }

            return View(nonProfitEmployee);
        }

        // GET: NonProfitEmployees/Create
        public IActionResult Create()
        {
            ViewData["locationID"] = new SelectList(_context.NonProfitLocation, "locationID", "locationID");
            return View();
        }

        // POST: NonProfitEmployees/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("employeeID,firstname,lastname,phone,email,city,street,postalcode,locationID,salary")] NonProfitEmployee nonProfitEmployee)
        {
            if (ModelState.IsValid)
            {
                _context.Add(nonProfitEmployee);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["locationID"] = new SelectList(_context.NonProfitLocation, "locationID", "locationID", nonProfitEmployee.locationID);
            return View(nonProfitEmployee);
        }

        // GET: NonProfitEmployees/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nonProfitEmployee = await _context.NonProfitEmployee.FindAsync(id);
            if (nonProfitEmployee == null)
            {
                return NotFound();
            }
            ViewData["locationID"] = new SelectList(_context.NonProfitLocation, "locationID", "locationID", nonProfitEmployee.locationID);
            return View(nonProfitEmployee);
        }

        // POST: NonProfitEmployees/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("employeeID,firstname,lastname,phone,email,city,street,postalcode,locationID,salary")] NonProfitEmployee nonProfitEmployee)
        {
            if (id != nonProfitEmployee.employeeID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(nonProfitEmployee);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NonProfitEmployeeExists(nonProfitEmployee.employeeID))
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
            ViewData["locationID"] = new SelectList(_context.NonProfitLocation, "locationID", "locationID", nonProfitEmployee.locationID);
            return View(nonProfitEmployee);
        }

        // GET: NonProfitEmployees/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nonProfitEmployee = await _context.NonProfitEmployee
                .Include(n => n.NonProfitLocation)
                .FirstOrDefaultAsync(m => m.employeeID == id);
            if (nonProfitEmployee == null)
            {
                return NotFound();
            }

            return View(nonProfitEmployee);
        }

        // POST: NonProfitEmployees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var nonProfitEmployee = await _context.NonProfitEmployee.FindAsync(id);
            _context.NonProfitEmployee.Remove(nonProfitEmployee);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NonProfitEmployeeExists(int id)
        {
            return _context.NonProfitEmployee.Any(e => e.employeeID == id);
        }
    }
}
