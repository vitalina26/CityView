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
        public async Task<IActionResult> Index(int? id, string? name)
        {
            if (id == null) return RedirectToAction("Index", "Events");
            ViewBag.EventId = id;
            ViewBag.EventName = name;
            var eventCommentsByEvent = _context.EventComments.Where(b => b.EventId == id).Include(b => b.Event);
            return View(await eventCommentsByEvent.ToListAsync());
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
        public IActionResult Create( int eventId)
        {
            // ViewData["EventId"] = new SelectList(_context.Events, "Id", "Address");
            ViewBag.EventId = eventId;
            ViewBag.EventName = _context.Events.Where(c => c.Id == eventId).FirstOrDefault().Name;


            return View();
        }

        // POST: EventComments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        /*
       public async Task<IActionResult> Create([Bind("Id,Name,DescriptionInfo,Contacts,Address,EventDay,CityId")] Event _event)
       {
           if (ModelState.IsValid)
           {
               _context.Add(_event);
               await _context.SaveChangesAsync();

               return RedirectToAction("Index", "Events", new { id = _event.CityId, name = _context.Cities.Where(c => c.Id == _event.CityId).FirstOrDefault().Name });
           }
           
           return RedirectToAction("Index", "Events", new { id = _event.CityId, name = _context.Cities.Where(c => c.Id == _event.CityId).FirstOrDefault().Name });
       }*/

        public async Task<IActionResult> Create([Bind("Id,Comment,DateOfCreation,EventId")] EventComment eventComment)
        {
            if (ModelState.IsValid)
            {
                _context.Add(eventComment);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "EventComments", new { id = eventComment.EventId, name = _context.Events.Where(c => c.Id == eventComment.EventId).FirstOrDefault().Name });
            }
            // ViewData["EventId"] = new SelectList(_context.Events, "Id", "Address", eventComment.EventId);
            
            return RedirectToAction("Index", "EventComments", new { id = eventComment.EventId, name = _context.Events.Where(c => c.Id == eventComment.EventId).FirstOrDefault().Name });

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
                return RedirectToAction("Index", "EventComments", new { id = eventComment.EventId, name = _context.Events.Where(c => c.Id == eventComment.EventId).FirstOrDefault().Name });
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
            return RedirectToAction("Index", "EventComments", new { id = eventComment.EventId, name = _context.Events.Where(c => c.Id == eventComment.EventId).FirstOrDefault().Name });
        }

        private bool EventCommentExists(int id)
        {
            return _context.EventComments.Any(e => e.Id == id);
        }
    }
}
