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
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _db;

        public CategoryController(ApplicationDbContext db)
        {
            _db = db;
        }

        // GET: Admin/Category
        // -------------------------
        public async Task<IActionResult> Index()
        {
            return View(await _db.Category.OrderBy(c => c.Name).ToListAsync());
        }



        // GET: Admin/Category/Create
        // -------------------------
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Category/Create
        // -------------------------
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Category category)
        {
            // Validate Model state
            // -------------------------
            if (ModelState.IsValid)
            {
                // Add category into Category table
                // -------------------------
                _db.Category.Add(category);
                await _db.SaveChangesAsync();

                // Go to Category index page
                // -------------------------
                return RedirectToAction(nameof(Index));
            }

            // If model state is invalid go back Create page
            // -------------------------
            return View(category);
        }

        // GET: Admin/Category/Edit
        // -------------------------
        public async Task<IActionResult> Edit(short? id)
        {

            // Verify if id is not empty, if not go to Not Found page
            // -------------------------
            if (id == null)
            {
                return NotFound();
            }

            // If id was properly sent get category with id value
            // -------------------------
            var category = await _db.Category.FindAsync(id);

            // Verify if category was found in database
            // -------------------------
            if (category == null)
            {
                return NotFound();
            }

            // If everything succeded go to edit page
            // -------------------------
            return View(category);
        }

        // POST: Admin/Category/Edit
        // -------------------------
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(short id, [Bind("Id,Name")] Category category)
        {
            // Verify if id sent is the same as in category object
            // -------------------------
            if (id != category.Id)
            {
                return NotFound();
            }

            // Validate model state
            // -------------------------
            if (ModelState.IsValid)
            {
                // Try to update database with sent category
                // -------------------------
                try
                {
                    _db.Update(category);
                    await _db.SaveChangesAsync();
                }
                // If category update fails check if category exists in database
                // -------------------------
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoryExists(category.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                // Go to category index page
                // -------------------------
                return RedirectToAction(nameof(Index));
            }

            // If model state is invalid go back to edit page
            // -------------------------
            return View(category);
        }

        // GET: Admin/Category/Delete
        // -------------------------
        public async Task<IActionResult> Delete(short? id)
        {
            // Verify if id was properly sent
            // -------------------------
            if (id == null)
            {
                return NotFound();
            }

            // If id was properly sent then get category from database
            // -------------------------
            var category = await _db.Category
                .FirstOrDefaultAsync(m => m.Id == id);

            // Verify if category was found in database
            // -------------------------
            if (category == null)
            {
                return NotFound();
            }

            // Go to Delete page
            // -------------------------
            return View(category);
        }

        // POST: Admin/Category/Delete
        // -------------------------
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(short? id)
        {
            // Verify if id is not empty
            // -------------------------
            if (id == null)
            {
                return NotFound();
            }

            // Get category with id value
            // -------------------------
            var category = await _db.Category.FindAsync(id);

            // Verify if category was found
            // -------------------------
            if (category == null)
            {
                return NotFound();
            }

            // Remove category from database
            // -------------------------
            _db.Category.Remove(category);
            await _db.SaveChangesAsync();

            // Go to category index page
            // -------------------------
            return RedirectToAction(nameof(Index));

        }

        private bool CategoryExists(short id)
        {
            return _db.Category.Any(e => e.Id == id);
        }
    }
}
