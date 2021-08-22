using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CityView;

namespace CityView.Controllers
{
    public class EventsController : Controller
    {
        private readonly DayInANewCityContext _context;

        public EventsController(DayInANewCityContext context)
        {
            _context = context;
        }

        // GET: Events
        public async Task<IActionResult> Index(int? id, string? name)
        {
            if (id == null) return RedirectToAction("Cities", "Index");
            ViewBag.CityId = id;
            ViewBag.CategoryName = name;
            var eventsByCity = _context.Events.Where(b=>b.CityId == id ).Include(b => b.City);
            return View(await eventsByCity.ToListAsync());
        }

        // GET: Events/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @event = await _context.Events
                .Include(b => b.City)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (@event == null)
            {
                return NotFound();
            }

            return View(@event);
        }

        // GET: Events/Create
        public IActionResult Create(int cityId)
        {
            //ViewData["CityId"] = new SelectList(_context.Cities, "Id", "Name");
            ViewBag.CityId = cityId;
            ViewBag.CityId = _context.Cities.Where(c => c.Id == cityId).FirstOrDefault().Name;

            return View();
        }

        // POST: Events/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int cityId, [Bind("Id,Name,DescriptionInfo,Contacts,Address,EventDay,CityId")] Event @event)
        {
            @event.CityId = cityId;
            if (ModelState.IsValid)
            {
                _context.Add(@event);
                await _context.SaveChangesAsync();
                //  return RedirectToAction(nameof(Index));
                return RedirectToAction("Index", "Events", new { id = cityId, name = _context.Cities.Where(c => c.Id == cityId).FirstOrDefault().Name });
            }
            //  ViewData["CityId"] = new SelectList(_context.Cities, "Id", "Name", @event.CityId);
            // return View(@event);
            return RedirectToAction("Index", "Events", new { id = cityId, name = _context.Cities.Where(c => c.Id == cityId).FirstOrDefault().Name });
        }

        // GET: Events/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @event = await _context.Events.FindAsync(id);
            if (@event == null)
            {
                return NotFound();
            }
            ViewData["CityId"] = new SelectList(_context.Cities, "Id", "Name", @event.CityId);
            return View(@event);
        }

        // POST: Events/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,DescriptionInfo,Contacts,Address,EventDay,CityId")] Event @event)
        {
            if (id != @event.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(@event);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EventExists(@event.Id))
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
            ViewData["CityId"] = new SelectList(_context.Cities, "Id", "Name", @event.CityId);
            return View(@event);
        }

        // GET: Events/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @event = await _context.Events
                .Include(b => b.City)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (@event == null)
            {
                return NotFound();
            }

            return View(@event);
        }

        // POST: Events/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var @event = await _context.Events.FindAsync(id);
            _context.Events.Remove(@event);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EventExists(int id)
        {
            return _context.Events.Any(e => e.Id == id);
        }
    }
}
