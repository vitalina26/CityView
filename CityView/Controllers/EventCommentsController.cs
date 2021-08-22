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
    public class EventCommentsController : Controller
    {
        private readonly DayInANewCityContext _context;

        public EventCommentsController(DayInANewCityContext context)
        {
            _context = context;
        }

        // GET: EventComments
        public async Task<IActionResult> Index()
        {
            var dayInANewCityContext = _context.EventComments.Include(e => e.Event);
            return View(await dayInANewCityContext.ToListAsync());
        }

        // GET: EventComments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eventComment = await _context.EventComments
                .Include(e => e.Event)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (eventComment == null)
            {
                return NotFound();
            }

            return View(eventComment);
        }

        // GET: EventComments/Create
        public IActionResult Create()
        {
            ViewData["EventId"] = new SelectList(_context.Events, "Id", "Address");
            return View();
        }

        // POST: EventComments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Comment,DateOfCreation,EventId")] EventComment eventComment)
        {
            if (ModelState.IsValid)
            {
                _context.Add(eventComment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EventId"] = new SelectList(_context.Events, "Id", "Address", eventComment.EventId);
            return View(eventComment);
        }

        // GET: EventComments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eventComment = await _context.EventComments.FindAsync(id);
            if (eventComment == null)
            {
                return NotFound();
            }
            ViewData["EventId"] = new SelectList(_context.Events, "Id", "Address", eventComment.EventId);
            return View(eventComment);
        }

        // POST: EventComments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Comment,DateOfCreation,EventId")] EventComment eventComment)
        {
            if (id != eventComment.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(eventComment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EventCommentExists(eventComment.Id))
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
            ViewData["EventId"] = new SelectList(_context.Events, "Id", "Address", eventComment.EventId);
            return View(eventComment);
        }

        // GET: EventComments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eventComment = await _context.EventComments
                .Include(e => e.Event)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (eventComment == null)
            {
                return NotFound();
            }

            return View(eventComment);
        }

        // POST: EventComments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var eventComment = await _context.EventComments.FindAsync(id);
            _context.EventComments.Remove(eventComment);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EventCommentExists(int id)
        {
            return _context.EventComments.Any(e => e.Id == id);
        }
    }
}
