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
    public class StationsController : Controller
    {
        private readonly CarpoolDBContext _context;

        public StationsController(CarpoolDBContext context)
        {
            _context = context;
        }

        // GET: Stations
        public async Task<IActionResult> Index()
        {
            var carpoolDBContext = _context.Stations.Include(s => s.StationPassenger).Include(s => s.StationTravel);
            return View(await carpoolDBContext.ToListAsync());
        }

        // GET: Stations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Stations == null)
            {
                return NotFound();
            }

            var station = await _context.Stations
                .Include(s => s.StationPassenger)
                .Include(s => s.StationTravel)
                .FirstOrDefaultAsync(m => m.StationId == id);
            if (station == null)
            {
                return NotFound();
            }

            return View(station);
        }

        // GET: Stations/Create
        public IActionResult Create()
        {
            ViewData["StationPassengerId"] = new SelectList(_context.Users, "UserId", "UserId");
            ViewData["StationTravelId"] = new SelectList(_context.Travels, "TravelId", "TravelId");
            return View();
        }

        // POST: Stations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StationId,StationTravelId,StationPassengerId,StationTime,StationLocation")] Station station)
        {
            if (ModelState.IsValid)
            {
                _context.Add(station);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["StationPassengerId"] = new SelectList(_context.Users, "UserId", "UserId", station.StationPassengerId);
            ViewData["StationTravelId"] = new SelectList(_context.Travels, "TravelId", "TravelId", station.StationTravelId);
            return View(station);
        }

        // GET: Stations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Stations == null)
            {
                return NotFound();
            }

            var station = await _context.Stations.FindAsync(id);
            if (station == null)
            {
                return NotFound();
            }
            ViewData["StationPassengerId"] = new SelectList(_context.Users, "UserId", "UserId", station.StationPassengerId);
            ViewData["StationTravelId"] = new SelectList(_context.Travels, "TravelId", "TravelId", station.StationTravelId);
            return View(station);
        }

        // POST: Stations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("StationId,StationTravelId,StationPassengerId,StationTime,StationLocation")] Station station)
        {
            if (id != station.StationId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(station);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StationExists(station.StationId))
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
            ViewData["StationPassengerId"] = new SelectList(_context.Users, "UserId", "UserId", station.StationPassengerId);
            ViewData["StationTravelId"] = new SelectList(_context.Travels, "TravelId", "TravelId", station.StationTravelId);
            return View(station);
        }

        // GET: Stations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Stations == null)
            {
                return NotFound();
            }

            var station = await _context.Stations
                .Include(s => s.StationPassenger)
                .Include(s => s.StationTravel)
                .FirstOrDefaultAsync(m => m.StationId == id);
            if (station == null)
            {
                return NotFound();
            }

            return View(station);
        }

        // POST: Stations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Stations == null)
            {
                return Problem("Entity set 'CarpoolDBContext.Stations'  is null.");
            }
            var station = await _context.Stations.FindAsync(id);
            if (station != null)
            {
                _context.Stations.Remove(station);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StationExists(int id)
        {
          return _context.Stations.Any(e => e.StationId == id);
        }
    }
}
