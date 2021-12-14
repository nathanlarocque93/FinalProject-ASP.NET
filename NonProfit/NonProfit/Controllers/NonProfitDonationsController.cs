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
    public class NonProfitDonationsController : Controller
    {
        private readonly DataContext _context;

        public NonProfitDonationsController(DataContext context)
        {
            _context = context;
        }

        // GET: NonProfitDonations
        public async Task<IActionResult> Index()
        {
            var dataContext = _context.NonProfitDonation.Include(n => n.NonProfitDonator);
            return View(await dataContext.ToListAsync());
        }

        // GET: NonProfitDonations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nonProfitDonation = await _context.NonProfitDonation
                .Include(n => n.NonProfitDonator)
                .FirstOrDefaultAsync(m => m.donationID == id);
            if (nonProfitDonation == null)
            {
                return NotFound();
            }

            return View(nonProfitDonation);
        }

        // GET: NonProfitDonations/Create
        public IActionResult Create()
        {
            ViewData["donatorID"] = new SelectList(_context.NonProfitDonator, "donatorID", "donatorID");
            return View();
        }

        // POST: NonProfitDonations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("donationID,amount,creditcard,city,street,postalcode,date,donatorID")] NonProfitDonation nonProfitDonation)
        {
            if (ModelState.IsValid)
            {
                _context.Add(nonProfitDonation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["donatorID"] = new SelectList(_context.NonProfitDonator, "donatorID", "donatorID", nonProfitDonation.donatorID);
            return View(nonProfitDonation);
        }

        // GET: NonProfitDonations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nonProfitDonation = await _context.NonProfitDonation.FindAsync(id);
            if (nonProfitDonation == null)
            {
                return NotFound();
            }
            ViewData["donatorID"] = new SelectList(_context.NonProfitDonator, "donatorID", "donatorID", nonProfitDonation.donatorID);
            return View(nonProfitDonation);
        }

        // POST: NonProfitDonations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("donationID,amount,creditcard,city,street,postalcode,date,donatorID")] NonProfitDonation nonProfitDonation)
        {
            if (id != nonProfitDonation.donationID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(nonProfitDonation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NonProfitDonationExists(nonProfitDonation.donationID))
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
            ViewData["donatorID"] = new SelectList(_context.NonProfitDonator, "donatorID", "donatorID", nonProfitDonation.donatorID);
            return View(nonProfitDonation);
        }

        // GET: NonProfitDonations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nonProfitDonation = await _context.NonProfitDonation
                .Include(n => n.NonProfitDonator)
                .FirstOrDefaultAsync(m => m.donationID == id);
            if (nonProfitDonation == null)
            {
                return NotFound();
            }

            return View(nonProfitDonation);
        }

        // POST: NonProfitDonations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var nonProfitDonation = await _context.NonProfitDonation.FindAsync(id);
            _context.NonProfitDonation.Remove(nonProfitDonation);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NonProfitDonationExists(int id)
        {
            return _context.NonProfitDonation.Any(e => e.donationID == id);
        }
    }
}
