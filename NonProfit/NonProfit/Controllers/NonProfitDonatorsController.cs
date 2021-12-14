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
    public class NonProfitDonatorsController : Controller
    {
        private readonly DataContext _context;

        public NonProfitDonatorsController(DataContext context)
        {
            _context = context;
        }

        // GET: NonProfitDonators
        public async Task<IActionResult> Index()
        {
            var dataContext = _context.NonProfitDonator.Include(n => n.NonProfitLocation);
            return View(await dataContext.ToListAsync());
        }

        // GET: NonProfitDonators/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nonProfitDonator = await _context.NonProfitDonator
                .Include(n => n.NonProfitLocation)
                .FirstOrDefaultAsync(m => m.donatorID == id);
            if (nonProfitDonator == null)
            {
                return NotFound();
            }

            return View(nonProfitDonator);
        }

        // GET: NonProfitDonators/Create
        public IActionResult Create()
        {
            ViewData["locationID"] = new SelectList(_context.NonProfitLocation, "locationID", "locationID");
            return View();
        }

        // POST: NonProfitDonators/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("donatorID,firstname,lastname,email,phone,locationID")] NonProfitDonator nonProfitDonator)
        {
            if (ModelState.IsValid)
            {
                _context.Add(nonProfitDonator);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["locationID"] = new SelectList(_context.NonProfitLocation, "locationID", "locationID", nonProfitDonator.locationID);
            return View(nonProfitDonator);
        }

        // GET: NonProfitDonators/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nonProfitDonator = await _context.NonProfitDonator.FindAsync(id);
            if (nonProfitDonator == null)
            {
                return NotFound();
            }
            ViewData["locationID"] = new SelectList(_context.NonProfitLocation, "locationID", "locationID", nonProfitDonator.locationID);
            return View(nonProfitDonator);
        }

        // POST: NonProfitDonators/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("donatorID,firstname,lastname,email,phone,locationID")] NonProfitDonator nonProfitDonator)
        {
            if (id != nonProfitDonator.donatorID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(nonProfitDonator);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NonProfitDonatorExists(nonProfitDonator.donatorID))
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
            ViewData["locationID"] = new SelectList(_context.NonProfitLocation, "locationID", "locationID", nonProfitDonator.locationID);
            return View(nonProfitDonator);
        }

        // GET: NonProfitDonators/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nonProfitDonator = await _context.NonProfitDonator
                .Include(n => n.NonProfitLocation)
                .FirstOrDefaultAsync(m => m.donatorID == id);
            if (nonProfitDonator == null)
            {
                return NotFound();
            }

            return View(nonProfitDonator);
        }

        // POST: NonProfitDonators/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var nonProfitDonator = await _context.NonProfitDonator.FindAsync(id);
            _context.NonProfitDonator.Remove(nonProfitDonator);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NonProfitDonatorExists(int id)
        {
            return _context.NonProfitDonator.Any(e => e.donatorID == id);
        }
    }
}
