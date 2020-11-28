using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Books.Data;
using Books.Models;
using Microsoft.AspNetCore.Hosting;
using Books.Models.ViewModels;
using System.IO;
using Books.StaticDetails;
using Books.Functional.Interfaces;
using Books.Functional.Classes;

namespace Books.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BookController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IWebHostEnvironment _webHostEnvironment;

        [BindProperty]
        public BookViewModel BookVM { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        IAuthorSupport AuthorSupport;

        public BookController(ApplicationDbContext db, IWebHostEnvironment webHostEnvironment)
        {
            _db = db;
            _webHostEnvironment = webHostEnvironment;
            BookVM = new BookViewModel
            {
                Book = new Book() { 
                    Author = new Author(),
                    Cover = new Cover(),
                    Genre = new Genre { 
                    Category = new Category()
                    },
                    Publisher = new Publisher(),
                    Language = new Language(),
                },
                Authors = _db.Author.ToList(),
                Covers = _db.Cover.ToList(),
                Genres = _db.Genre.ToList(),
                Publishers = _db.Publisher.ToList(),
                Languages = _db.Language.ToList(),
                Categories = _db.Category.ToList()
            };
            AuthorSupport = new AuthorSupport()
            {
                WebRootPath = webHostEnvironment.WebRootPath,
                Db = db,
            };
        }

        // GET: Admin/Book
        // =================
        public async Task<IActionResult> Index()
        {
            // Get all books from database
            // ----------------------
            var books = _db.Book.Include(b => b.Author).Include(b => b.Cover)
                                               .Include(b => b.Genre).Include(b => b.Language)
                                               .Include(b => b.Publisher)
                                               .OrderBy(b => b.Author.Alias).ThenBy(b => b.Name)
                                               .ToListAsync();

            // Return books to View
            // ----------------------
            return View(await books);

        }


        // GET: Admin/Book/Create
        // =================
        public IActionResult Create()
        {
            return View(BookVM);
        }

        // POST: Admin/Book/Create
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create()
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _db.Add(book);
        //        await _db.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    ViewData["AuthorId"] = new SelectList(_db.Author, "Id", "Name", book.AuthorId);
        //    ViewData["CoverId"] = new SelectList(_db.Cover, "Id", "Name", book.CoverId);
        //    ViewData["GenreId"] = new SelectList(_db.Genre, "Id", "Name", book.GenreId);
        //    ViewData["LanguageId"] = new SelectList(_db.Language, "Id", "Name", book.LanguageId);
        //    ViewData["PublisherId"] = new SelectList(_db.Publisher, "Id", "Name", book.PublisherId);
        //    return View(book);
        //}

        [HttpPost]
        [ActionName("Create")]
        [ValidateAntiForgeryToken]
        public IActionResult CreatePost()
        {
            return View(BookVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async ValueTask<Tuple<short, string>> CreateAuthor(AuthorViewModel authorVM)
        {

            var status = await AuthorSupport.AuthorCreate(ModelState, authorVM, HttpContext);
            return status;
            //if (exitCode == 0)
            //{
            //    return RedirectToAction(nameof(Index));
            //}

        }

        // GET: Admin/Book/Details/id
        // =================
        public async Task<IActionResult> Details(long? id)
        {

            // Verify if Book with id exists
            // ---------------------- 
            if (id == null)
            {
                return NotFound();
            }

            var book = await _db.Book
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


        // GET: Admin/Book/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _db.Book.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }
            ViewData["AuthorId"] = new SelectList(_db.Author, "Id", "Name", book.AuthorId);
            ViewData["CoverId"] = new SelectList(_db.Cover, "Id", "Name", book.CoverId);
            ViewData["GenreId"] = new SelectList(_db.Genre, "Id", "Name", book.GenreId);
            ViewData["LanguageId"] = new SelectList(_db.Language, "Id", "Name", book.LanguageId);
            ViewData["PublisherId"] = new SelectList(_db.Publisher, "Id", "Name", book.PublisherId);
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
                    _db.Update(book);
                    await _db.SaveChangesAsync();
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
            ViewData["AuthorId"] = new SelectList(_db.Author, "Id", "Name", book.AuthorId);
            ViewData["CoverId"] = new SelectList(_db.Cover, "Id", "Name", book.CoverId);
            ViewData["GenreId"] = new SelectList(_db.Genre, "Id", "Name", book.GenreId);
            ViewData["LanguageId"] = new SelectList(_db.Language, "Id", "Name", book.LanguageId);
            ViewData["PublisherId"] = new SelectList(_db.Publisher, "Id", "Name", book.PublisherId);
            return View(book);
        }

        // GET: Admin/Book/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _db.Book
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
            var book = await _db.Book.FindAsync(id);
            _db.Book.Remove(book);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BookExists(long id)
        {
            return _db.Book.Any(e => e.Id == id);
        }
    }
}
