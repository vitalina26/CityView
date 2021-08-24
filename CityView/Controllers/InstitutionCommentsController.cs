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
    public class InstitutionCommentsController : Controller
    {
        private readonly DayInANewCityContext _context;

        public InstitutionCommentsController(DayInANewCityContext context)
        {
            _context = context;
        }

        // GET: InstitutionComments
        public async Task<IActionResult> Index(int? id, string? name)
        {
            if (id == null) return RedirectToAction("Index", "Institutions");
            ViewBag.InstitutionId = id;
            ViewBag.InstitutionName = name;
            var institutionCommentsByInstitution = _context.InstitutionComments.Where(b => b.InstitutionId == id).Include(b => b.Institution);
            return View(await institutionCommentsByInstitution.ToListAsync());
        }

        // GET: InstitutionComments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var institutionComment = await _context.InstitutionComments
                .Include(i => i.Institution)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (institutionComment == null)
            {
                return NotFound();
            }

            return View(institutionComment);
        }

        // GET: InstitutionComments/Create
        public IActionResult Create(int institutionId)
        {
            ViewBag.InstitutionId = institutionId;
            ViewBag.InstitutionName = _context.Institutions.Where(c => c.Id == institutionId).FirstOrDefault().Name;


            return View();
        }

        // POST: InstitutionComments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Comment,DateOfCreation,InstitutionId")] InstitutionComment institutionComment)
        {
            if (ModelState.IsValid)
            {
                _context.Add(institutionComment);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "InstitutionComments", new { id = institutionComment.InstitutionId, name = _context.Institutions.Where(c => c.Id == institutionComment.InstitutionId).FirstOrDefault().Name });

            }
            return RedirectToAction("Index", "InstitutionComments", new { id = institutionComment.InstitutionId, name = _context.Institutions.Where(c => c.Id == institutionComment.InstitutionId).FirstOrDefault().Name });

        }

        // GET: InstitutionComments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var institutionComment = await _context.InstitutionComments.FindAsync(id);
            if (institutionComment == null)
            {
                return NotFound();
            }
            ViewData["InstitutionId"] = new SelectList(_context.Institutions, "Id", "Id", institutionComment.InstitutionId);
            return View(institutionComment);
        }

        // POST: InstitutionComments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Comment,DateOfCreation,InstitutionId")] InstitutionComment institutionComment)
        {
            if (id != institutionComment.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(institutionComment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InstitutionCommentExists(institutionComment.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index", "InstitutionComments", new { id = institutionComment.InstitutionId, name = _context.Institutions.Where(c => c.Id == institutionComment.InstitutionId).FirstOrDefault().Name });

            }
            ViewData["InstitutionId"] = new SelectList(_context.Institutions, "Id", "Id", institutionComment.InstitutionId);
            return View(institutionComment);
        }

        // GET: InstitutionComments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var institutionComment = await _context.InstitutionComments
                .Include(i => i.Institution)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (institutionComment == null)
            {
                return NotFound();
            }

            return View(institutionComment);
        }

        // POST: InstitutionComments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var institutionComment = await _context.InstitutionComments.FindAsync(id);
            _context.InstitutionComments.Remove(institutionComment);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "InstitutionComments", new { id = institutionComment.InstitutionId, name = _context.Institutions.Where(c => c.Id == institutionComment.InstitutionId).FirstOrDefault().Name });

        }

        private bool InstitutionCommentExists(int id)
        {
            return _context.InstitutionComments.Any(e => e.Id == id);
        }
    }
}
