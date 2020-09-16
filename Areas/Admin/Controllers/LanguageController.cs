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
    public class LanguageController : Controller
    {
        private readonly ApplicationDbContext _db;

        public LanguageController(ApplicationDbContext db)
        {
            _db = db;
        }

        // GET: Admin/Language
        // -------------------------
        public async Task<IActionResult> Index()
        {
            return View(await _db.Language.OrderBy(c => c.Name).ToListAsync());
        }

        // GET: Admin/Language/Create
        // -------------------------
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Language/Create
        // -------------------------
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Language language)
        {
            if (ModelState.IsValid)
            {
                _db.Language.Add(language);
                await _db.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            return View(language);
        }

        // GET: Admin/Language/Edit
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
            var language = await _db.Language.FindAsync(id);

            // If Publisher with such Id wasn't found then show Not Found screen
            // -------------------------
            if (language == null)
            {
                return NotFound();
            }

            // If all commands passed then display proper View
            // -------------------------
            return View(language);
        }

        // POST: Admin/Language/Edit
        // -------------------------
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(short id, [Bind("Id,Name")] Language language)
        {
            // Validate id number
            // -------------------------
            if (id != language.Id)
            {
                return NotFound();
            }

            // Validate model state
            // -------------------------
            if (ModelState.IsValid)
            {
                // Try to update Language name
                // -------------------------
                try
                {
                    _db.Update(language);
                    await _db.SaveChangesAsync();
                }
                // If language update fails then throw an error page
                // -------------------------
                catch (DbUpdateConcurrencyException)
                {
                    // Check if selected language exists in database
                    // -------------------------
                    if (!LanguageExists(language.Id))
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
            return View(language);
        }

        // GET: Admin/Language/Delete
        // -------------------------
        public async Task<IActionResult> Delete(short? id)
        {
            // Check if id was properly sent
            // -------------------------
            if (id == null)
            {
                return NotFound();
            }

            // If Id was properly sent then get Language from Db
            // -------------------------
            var language = await _db.Language
                .FirstOrDefaultAsync(m => m.Id == id);

            // If Language with such Id wasn't found then show Not Found screen
            // -------------------------
            if (language == null)
            {
                return NotFound();
            }

            // If all commands passed then display proper View
            // -------------------------
            return View(language);
        }

        // POST: Admin/Language/Delete
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

            // Search for language with specified id
            // -------------------------
            var language = await _db.Language.FindAsync(id);

            // Check if language exists in database
            // -------------------------
            if (language == null)
            {
                return NotFound();
            }

            // If language exists then remove it from database
            // -------------------------
            _db.Language.Remove(language);
            await _db.SaveChangesAsync();

            // Go back into index action
            // -------------------------
            return RedirectToAction(nameof(Index));
        }

        private bool LanguageExists(short id)
        {
            return _db.Language.Any(e => e.Id == id);
        }
    }
}
