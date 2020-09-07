using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Books.Data;
using Books.Models;

namespace Books.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CoverController : Controller
    {
        private readonly ApplicationDbContext _db;

        public CoverController(ApplicationDbContext db)
        {
            _db = db;
        }

        // GET: Admin/Cover
        // -------------------------
        public async Task<IActionResult> Index()
        {
            return View(await _db.Cover.OrderBy(c => c.Name).ToListAsync());
        }

        // GET: Admin/Cover/Create
        // -------------------------
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Cover/Create
        // -------------------------
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Cover cover)
        {
            if (ModelState.IsValid)
            {
                _db.Cover.Add(cover);
                await _db.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            return View(cover);
        }

        // GET: Admin/Cover/Edit
        // -------------------------
        public async Task<IActionResult> Edit(short? id)
        {
            // Check if id was properly sent
            // -------------------------
            if (id == null)
            {
                return NotFound();
            }

            // If Id was properly sent then get Publisher from Db
            // -------------------------
            var cover = await _db.Cover.FindAsync(id);

            // If Publisher with such Id wasn't found then show Not Found screen
            // -------------------------
            if (cover == null)
            {
                return NotFound();
            }

            // If all commands passed then display proper View
            // -------------------------
            return View(cover);
        }

        // POST: Admin/Cover/Edit
        // -------------------------
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(short id, [Bind("Id,Name")] Cover cover)
        {
            // Validate id number
            // -------------------------
            if (id != cover.Id)
            {
                return NotFound();
            }

            // Validate model state
            // -------------------------
            if (ModelState.IsValid)
            {
                // Try to update Cover name
                // -------------------------
                try
                {
                    _db.Update(cover);
                    await _db.SaveChangesAsync();
                }
                // If cover update fails then throw an error page
                // -------------------------
                catch (DbUpdateConcurrencyException)
                {
                    // Check if selected cover exists in database
                    // -------------------------
                    if (!CoverExists(cover.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                // Redirect to action page
                // -------------------------
                return RedirectToAction(nameof(Index));
            }

            // If model state is invalid go back to Edit page
            // -------------------------
            return View(cover);
        }

        // GET: Admin/Cover/Delete
        // -------------------------
        public async Task<IActionResult> Delete(short? id)
        {
            // Check if id was properly sent
            // -------------------------
            if (id == null)
            {
                return NotFound();
            }

            // If Id was properly sent then get Cover from Db
            // -------------------------
            var cover = await _db.Cover
                .FirstOrDefaultAsync(m => m.Id == id);

            // If Cover with such Id wasn't found then show Not Found screen
            // -------------------------
            if (cover == null)
            {
                return NotFound();
            }

            // If all commands passed then display proper View
            // -------------------------
            return View(cover);
        }

        // POST: Admin/Cover/Delete
        // -------------------------
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(short? id)
        {
            // Check if id was properly sent
            // -------------------------
            if (id == null)
            {
                return NotFound();
            }

            // Search for cover with specified id
            // -------------------------
            var cover = await _db.Cover.FindAsync(id);

            // Check if cover exists in database
            // -------------------------
            if (cover == null)
            {
                return NotFound();
            }

            // If cover exists then remove it from database
            // -------------------------
            _db.Cover.Remove(cover);
            await _db.SaveChangesAsync();

            // Go back into index action
            // -------------------------
            return RedirectToAction(nameof(Index));
        }

        private bool CoverExists(short id)
        {
            return _db.Cover.Any(e => e.Id == id);
        }
    }
}
