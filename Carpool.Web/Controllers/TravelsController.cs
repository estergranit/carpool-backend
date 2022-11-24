using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Carpool.Web.GeneratedModels;

namespace Carpool.Web.Controllers
{
    public class TravelsController : Controller
    {
        private readonly CarpoolDBContext _context;

        public TravelsController(CarpoolDBContext context)
        {
            _context = context;
        }

        // GET: Travels
        public async Task<IActionResult> Index()
        {
            var carpoolDBContext = _context.Travels.Include(t => t.TravelDriver);
            return View(await carpoolDBContext.ToListAsync());
        }

        // GET: Travels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Travels == null)
            {
                return NotFound();
            }

            var travel = await _context.Travels
                .Include(t => t.TravelDriver)
                .FirstOrDefaultAsync(m => m.TravelId == id);
            if (travel == null)
            {
                return NotFound();
            }

            return View(travel);
        }

        // GET: Travels/Create
        public IActionResult Create()
        {
            ViewData["TravelDriverId"] = new SelectList(_context.Users, "UserId", "UserId");
            return View();
        }

        // POST: Travels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TravelId,TravelDriverId,TravelOrigin,TravelDestination,TravelDepartureTime,TravelAvailable,TravelMap")] Travel travel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(travel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["TravelDriverId"] = new SelectList(_context.Users, "UserId", "UserId", travel.TravelDriverId);
            return View(travel);
        }

        // GET: Travels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Travels == null)
            {
                return NotFound();
            }

            var travel = await _context.Travels.FindAsync(id);
            if (travel == null)
            {
                return NotFound();
            }
            ViewData["TravelDriverId"] = new SelectList(_context.Users, "UserId", "UserId", travel.TravelDriverId);
            return View(travel);
        }

        // POST: Travels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TravelId,TravelDriverId,TravelOrigin,TravelDestination,TravelDepartureTime,TravelAvailable,TravelMap")] Travel travel)
        {
            if (id != travel.TravelId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(travel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TravelExists(travel.TravelId))
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
            ViewData["TravelDriverId"] = new SelectList(_context.Users, "UserId", "UserId", travel.TravelDriverId);
            return View(travel);
        }

        // GET: Travels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Travels == null)
            {
                return NotFound();
            }

            var travel = await _context.Travels
                .Include(t => t.TravelDriver)
                .FirstOrDefaultAsync(m => m.TravelId == id);
            if (travel == null)
            {
                return NotFound();
            }

            return View(travel);
        }

        // POST: Travels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Travels == null)
            {
                return Problem("Entity set 'CarpoolDBContext.Travels'  is null.");
            }
            var travel = await _context.Travels.FindAsync(id);
            if (travel != null)
            {
                _context.Travels.Remove(travel);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TravelExists(int id)
        {
          return _context.Travels.Any(e => e.TravelId == id);
        }
    }
}
