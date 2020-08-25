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
    public class PublisherController : Controller
    {
        private readonly ApplicationDbContext _db;

        public PublisherController(ApplicationDbContext db)
        {
            _db = db;
        }

        // GET: Admin/Publisher
        // -------------------------
        public async Task<IActionResult> Index()
        {
            return View(await _db.Publisher.OrderBy(p => p.Name).ToListAsync());
        }



        // GET: Admin/Publisher/Create
        // -------------------------
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Publisher/Create
        // -------------------------
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Publisher publisher)
        {
            if (ModelState.IsValid)
            {
                await _db.Publisher.AddAsync(publisher);
                await _db.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            return View(publisher);
        }

        // GET: Admin/Publisher/Edit/5
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
            var publisher = await _db.Publisher.FindAsync(id);

            // If Publisher with such Id wasn't found then show Not Found screen
            // -------------------------
            if (publisher == null)
            {
                return NotFound();
            }

            // If all commands passed then display proper View
            // -------------------------
            return View(publisher);
        }

        // POST: Admin/Publisher/Edit/5
        // -------------------------
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(short id, [Bind("Id,Name")] Publisher publisher)
        {
            if (id != publisher.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _db.Update(publisher);
                    await _db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PublisherExists(publisher.Id))
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
            return View(publisher);
        }

        // GET: Admin/Publisher/Delete/5
        // -------------------------
        public async Task<IActionResult> Delete(short? id)
        {
            // Check if id was properly sent
            // -------------------------
            if (id == null)
            {
                return NotFound();
            }

            // If Id was properly sent then get Publisher from Db
            // -------------------------
            var publisher = await _db.Publisher
                .FirstOrDefaultAsync(m => m.Id == id);

            // If Publisher with such Id wasn't found then show Not Found screen
            // -------------------------
            if (publisher == null)
            {
                return NotFound();
            }

            // If all commands passed then display proper View
            // -------------------------
            return View(publisher);
        }

        // POST: Admin/Publisher/Delete/5
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

            // If Id was properly sent then get Genre from Db
            // -------------------------
            var publisher = await _db.Publisher.FindAsync(id);

            // If genre with such Id wasn't found then go back to View
            // -------------------------
            if (publisher == null)
            {
                return RedirectToAction(nameof(Index));
            }

            // If genre was found then remove it and save Db
            // -------------------------
            _db.Publisher.Remove(publisher);
            await _db.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        private bool PublisherExists(short id)
        {
            return _db.Publisher.Any(e => e.Id == id);
        }
    }
}
