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

        // GET: Admin/Cover/Edit/5
        public async Task<IActionResult> Edit(short? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cover = await _db.Cover.FindAsync(id);
            if (cover == null)
            {
                return NotFound();
            }
            return View(cover);
        }

        // POST: Admin/Cover/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(short id, [Bind("Id,Name")] Cover cover)
        {
            if (id != cover.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _db.Update(cover);
                    await _db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CoverExists(cover.Id))
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
            return View(cover);
        }

        // GET: Admin/Cover/Delete/5
        public async Task<IActionResult> Delete(short? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cover = await _db.Cover
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cover == null)
            {
                return NotFound();
            }

            return View(cover);
        }

        // POST: Admin/Cover/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(short id)
        {
            var cover = await _db.Cover.FindAsync(id);
            _db.Cover.Remove(cover);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CoverExists(short id)
        {
            return _db.Cover.Any(e => e.Id == id);
        }
    }
}
