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
    public class NonProfitLocationsController : Controller
    {
        private readonly DataContext _context;

        public NonProfitLocationsController(DataContext context)
        {
            _context = context;
        }

        // GET: NonProfitLocations
        public async Task<IActionResult> Index()
        {
            var dataContext = _context.NonProfitLocation.Include(n => n.NonProfitOwner);
            return View(await dataContext.ToListAsync());
        }

        // GET: NonProfitLocations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nonProfitLocation = await _context.NonProfitLocation
                .Include(n => n.NonProfitOwner)
                .FirstOrDefaultAsync(m => m.locationID == id);
            if (nonProfitLocation == null)
            {
                return NotFound();
            }

            return View(nonProfitLocation);
        }

        // GET: NonProfitLocations/Create
        public IActionResult Create()
        {
            ViewData["OwnerID"] = new SelectList(_context.NonProfitOwner, "ownerID", "ownerID");
            return View();
        }

        // POST: NonProfitLocations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("locationID,city,street,postalcode,phone,OwnerID")] NonProfitLocation nonProfitLocation)
        {
            if (ModelState.IsValid)
            {
                _context.Add(nonProfitLocation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["OwnerID"] = new SelectList(_context.NonProfitOwner, "ownerID", "ownerID", nonProfitLocation.OwnerID);
            return View(nonProfitLocation);
        }

        // GET: NonProfitLocations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nonProfitLocation = await _context.NonProfitLocation.FindAsync(id);
            if (nonProfitLocation == null)
            {
                return NotFound();
            }
            ViewData["OwnerID"] = new SelectList(_context.NonProfitOwner, "ownerID", "ownerID", nonProfitLocation.OwnerID);
            return View(nonProfitLocation);
        }

        // POST: NonProfitLocations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("locationID,city,street,postalcode,phone,OwnerID")] NonProfitLocation nonProfitLocation)
        {
            if (id != nonProfitLocation.locationID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(nonProfitLocation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NonProfitLocationExists(nonProfitLocation.locationID))
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
            ViewData["OwnerID"] = new SelectList(_context.NonProfitOwner, "ownerID", "ownerID", nonProfitLocation.OwnerID);
            return View(nonProfitLocation);
        }

        // GET: NonProfitLocations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nonProfitLocation = await _context.NonProfitLocation
                .Include(n => n.NonProfitOwner)
                .FirstOrDefaultAsync(m => m.locationID == id);
            if (nonProfitLocation == null)
            {
                return NotFound();
            }

            return View(nonProfitLocation);
        }

        // POST: NonProfitLocations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var nonProfitLocation = await _context.NonProfitLocation.FindAsync(id);
            _context.NonProfitLocation.Remove(nonProfitLocation);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NonProfitLocationExists(int id)
        {
            return _context.NonProfitLocation.Any(e => e.locationID == id);
        }
    }
}
