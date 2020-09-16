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
    public class AuthorController : Controller
    {
        private readonly ApplicationDbContext _db;

        public AuthorController(ApplicationDbContext db)
        {
            _db = db;
        }

        // GET: Admin/Author
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _db.Author.Include(a => a.Genre01)
                                                 .Include(a => a.Genre02)
                                                 .Include(a => a.Genre03)
                                                 .Include(a => a.Genre04)
                                                 .Include(a => a.Genre05)
                                                 .Include(a => a.Genre06)
                                                 .Include(a => a.Genre07)
                                                 .Include(a => a.Language);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Admin/Author/Details/5
        public async Task<IActionResult> Details(short? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var author = await _db.Author
                .Include(a => a.Genre01)
                .Include(a => a.Genre02)
                .Include(a => a.Genre03)
                .Include(a => a.Genre04)
                .Include(a => a.Genre05)
                .Include(a => a.Genre06)
                .Include(a => a.Genre07)
                .Include(a => a.Language)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (author == null)
            {
                return NotFound();
            }

            return View(author);
        }

        // GET: Admin/Author/Create
        public IActionResult Create()
        {
            ViewData["GenreId01"] = new SelectList(_db.Genre, "Id", "Name");
            ViewData["GenreId02"] = new SelectList(_db.Genre, "Id", "Name");
            ViewData["GenreId03"] = new SelectList(_db.Genre, "Id", "Name");
            ViewData["GenreId04"] = new SelectList(_db.Genre, "Id", "Name");
            ViewData["GenreId05"] = new SelectList(_db.Genre, "Id", "Name");
            ViewData["GenreId06"] = new SelectList(_db.Genre, "Id", "Name");
            ViewData["GenreId07"] = new SelectList(_db.Genre, "Id", "Name");
            ViewData["LanguageId"] = new SelectList(_db.Language, "Id", "Name");
            return View();
        }

        // POST: Admin/Author/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Alias,Birthday,LanguageId,Description,Image,GenreId01,GenreId02,GenreId03,GenreId04,GenreId05,GenreId06,GenreId07")] Author author)
        {
            if (ModelState.IsValid)
            {
                _db.Add(author);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["GenreId01"] = new SelectList(_db.Genre, "Id", "Name", author.GenreId01);
            ViewData["GenreId02"] = new SelectList(_db.Genre, "Id", "Name", author.GenreId02);
            ViewData["GenreId03"] = new SelectList(_db.Genre, "Id", "Name", author.GenreId03);
            ViewData["GenreId04"] = new SelectList(_db.Genre, "Id", "Name", author.GenreId04);
            ViewData["GenreId05"] = new SelectList(_db.Genre, "Id", "Name", author.GenreId05);
            ViewData["GenreId06"] = new SelectList(_db.Genre, "Id", "Name", author.GenreId06);
            ViewData["GenreId07"] = new SelectList(_db.Genre, "Id", "Name", author.GenreId07);
            ViewData["LanguageId"] = new SelectList(_db.Language, "Id", "Name", author.LanguageId);
            return View(author);
        }

        // GET: Admin/Author/Edit/5
        public async Task<IActionResult> Edit(short? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var author = await _db.Author.FindAsync(id);
            if (author == null)
            {
                return NotFound();
            }
            ViewData["GenreId01"] = new SelectList(_db.Genre, "Id", "Name", author.GenreId01);
            ViewData["GenreId02"] = new SelectList(_db.Genre, "Id", "Name", author.GenreId02);
            ViewData["GenreId03"] = new SelectList(_db.Genre, "Id", "Name", author.GenreId03);
            ViewData["GenreId04"] = new SelectList(_db.Genre, "Id", "Name", author.GenreId04);
            ViewData["GenreId05"] = new SelectList(_db.Genre, "Id", "Name", author.GenreId05);
            ViewData["GenreId06"] = new SelectList(_db.Genre, "Id", "Name", author.GenreId06);
            ViewData["GenreId07"] = new SelectList(_db.Genre, "Id", "Name", author.GenreId07);
            ViewData["LanguageId"] = new SelectList(_db.Language, "Id", "Name", author.LanguageId);
            return View(author);
        }

        // POST: Admin/Author/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(short id, [Bind("Id,Name,Alias,Birthday,LanguageId,Description,Image,GenreId01,GenreId02,GenreId03,GenreId04,GenreId05,GenreId06,GenreId07")] Author author)
        {
            if (id != author.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _db.Update(author);
                    await _db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AuthorExists(author.Id))
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
            ViewData["GenreId01"] = new SelectList(_db.Genre, "Id", "Name", author.GenreId01);
            ViewData["GenreId02"] = new SelectList(_db.Genre, "Id", "Name", author.GenreId02);
            ViewData["GenreId03"] = new SelectList(_db.Genre, "Id", "Name", author.GenreId03);
            ViewData["GenreId04"] = new SelectList(_db.Genre, "Id", "Name", author.GenreId04);
            ViewData["GenreId05"] = new SelectList(_db.Genre, "Id", "Name", author.GenreId05);
            ViewData["GenreId06"] = new SelectList(_db.Genre, "Id", "Name", author.GenreId06);
            ViewData["GenreId07"] = new SelectList(_db.Genre, "Id", "Name", author.GenreId07);
            ViewData["LanguageId"] = new SelectList(_db.Language, "Id", "Name", author.LanguageId);
            return View(author);
        }

        // GET: Admin/Author/Delete/5
        public async Task<IActionResult> Delete(short? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var author = await _db.Author
                .Include(a => a.Genre01)
                .Include(a => a.Genre02)
                .Include(a => a.Genre03)
                .Include(a => a.Genre04)
                .Include(a => a.Genre05)
                .Include(a => a.Genre06)
                .Include(a => a.Genre07)
                .Include(a => a.Language)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (author == null)
            {
                return NotFound();
            }

            return View(author);
        }

        // POST: Admin/Author/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(short id)
        {
            var author = await _db.Author.FindAsync(id);
            _db.Author.Remove(author);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AuthorExists(short id)
        {
            return _db.Author.Any(e => e.Id == id);
        }
    }
}
