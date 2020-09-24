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
    public class BookController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BookController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Admin/Book
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Book.Include(b => b.Author).Include(b => b.Cover).Include(b => b.Genre).Include(b => b.Language).Include(b => b.Publisher);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Admin/Book/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Book
                .Include(b => b.Author)
                .Include(b => b.Cover)
                .Include(b => b.Genre)
                .Include(b => b.Language)
                .Include(b => b.Publisher)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // GET: Admin/Book/Create
        public IActionResult Create()
        {
            ViewData["AuthorId"] = new SelectList(_context.Author, "Id", "Name");
            ViewData["CoverId"] = new SelectList(_context.Cover, "Id", "Name");
            ViewData["GenreId"] = new SelectList(_context.Genre, "Id", "Name");
            ViewData["LanguageId"] = new SelectList(_context.Language, "Id", "Name");
            ViewData["PublisherId"] = new SelectList(_context.Publisher, "Id", "Name");
            return View();
        }

        // POST: Admin/Book/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,AuthorId,ReleaseDate,CoverId,GenreId,PublisherId,LanguageId,Image,Description,OnStock,Type,Price")] Book book)
        {
            if (ModelState.IsValid)
            {
                _context.Add(book);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AuthorId"] = new SelectList(_context.Author, "Id", "Name", book.AuthorId);
            ViewData["CoverId"] = new SelectList(_context.Cover, "Id", "Name", book.CoverId);
            ViewData["GenreId"] = new SelectList(_context.Genre, "Id", "Name", book.GenreId);
            ViewData["LanguageId"] = new SelectList(_context.Language, "Id", "Name", book.LanguageId);
            ViewData["PublisherId"] = new SelectList(_context.Publisher, "Id", "Name", book.PublisherId);
            return View(book);
        }

        // GET: Admin/Book/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Book.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }
            ViewData["AuthorId"] = new SelectList(_context.Author, "Id", "Name", book.AuthorId);
            ViewData["CoverId"] = new SelectList(_context.Cover, "Id", "Name", book.CoverId);
            ViewData["GenreId"] = new SelectList(_context.Genre, "Id", "Name", book.GenreId);
            ViewData["LanguageId"] = new SelectList(_context.Language, "Id", "Name", book.LanguageId);
            ViewData["PublisherId"] = new SelectList(_context.Publisher, "Id", "Name", book.PublisherId);
            return View(book);
        }

        // POST: Admin/Book/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,Name,AuthorId,ReleaseDate,CoverId,GenreId,PublisherId,LanguageId,Image,Description,OnStock,Type,Price")] Book book)
        {
            if (id != book.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(book);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookExists(book.Id))
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
            ViewData["AuthorId"] = new SelectList(_context.Author, "Id", "Name", book.AuthorId);
            ViewData["CoverId"] = new SelectList(_context.Cover, "Id", "Name", book.CoverId);
            ViewData["GenreId"] = new SelectList(_context.Genre, "Id", "Name", book.GenreId);
            ViewData["LanguageId"] = new SelectList(_context.Language, "Id", "Name", book.LanguageId);
            ViewData["PublisherId"] = new SelectList(_context.Publisher, "Id", "Name", book.PublisherId);
            return View(book);
        }

        // GET: Admin/Book/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Book
                .Include(b => b.Author)
                .Include(b => b.Cover)
                .Include(b => b.Genre)
                .Include(b => b.Language)
                .Include(b => b.Publisher)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // POST: Admin/Book/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var book = await _context.Book.FindAsync(id);
            _context.Book.Remove(book);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BookExists(long id)
        {
            return _context.Book.Any(e => e.Id == id);
        }
    }
}
