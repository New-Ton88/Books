using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Books.Data;
using Books.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Books.Models.ViewModels;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Microsoft.Extensions.DependencyInjection;
using Books.StaticDetails;
using System.Net;

namespace Books.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AuthorController : Controller
    {
        private readonly ApplicationDbContext _db;

        [TempData]
        public string StatusMessage { get; set; }

        // If You don't want to add everytime AuthorViewModel
        // into action create property in class with BindProperty attribute
        // ------------------------------
        [BindProperty]
        public AuthorViewModel AuthorVM { get; set; }

        private readonly IWebHostEnvironment _webHostEnvironment;

        public AuthorController(ApplicationDbContext db, IWebHostEnvironment webHostEnvironment)
        {
            _db = db;
            _webHostEnvironment = webHostEnvironment;
            AuthorVM = new AuthorViewModel()
            {
                Author = new Author(),
                Genres = _db.Genre,
                Languages = _db.Language
            };
        }

        // GET: Admin/Author
        // ------------------------------
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

        // GET: Admin/Author/Create
        // ------------------------------
        public IActionResult Create()
        {
            // Author, Languages and Genres are already filled because
            // they are added as BindProperty a fulfiled in constructor
            // ------------------------------
            return View(AuthorVM);
        }

        // POST: Admin/Author/Create
        // ------------------------------
        [HttpPost]
        [ActionName("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreatePost()
        {
            // Check if model state is valid
            // ------------------------------
            if (ModelState.IsValid)
            {

                // Check if author name or alias entered already exists
                // ------------------------------
                var authors = _db.Author.Where(a => a.Name == AuthorVM.Author.Name || a.Alias == AuthorVM.Author.Alias);

                // If author already exists inform user
                // ------------------------------
                if (authors.Count() > 0)
                {
                    // Verify if user name already exists
                    // ------------------------------
                    var author = authors.First();
                    var authorNameExists = author.Name == AuthorVM.Author.Name;
                    if (authorNameExists)
                    {
                        StatusMessage = "Error: Author with " + author.Name + " name already exists!";
                    }

                    // Verify if user alias already exists
                    // ------------------------------
                    if (author.Alias == AuthorVM.Author.Alias)
                    {
                        // If user Name also exist then append in newline message
                        // ------------------------------
                        if (authorNameExists)
                        {
                            StatusMessage += "\nError: Author with " + author.Alias + " alias already exists!";
                        }
                        // If author name doesn't exists then 
                        // show only error message for alias
                        // ------------------------------
                        else
                        {
                            StatusMessage = "Error: Author with " + author.Alias + " alias already exists!";
                        }
                        
                    }

                }
                // If author isn't exists in db then add it
                // ------------------------------
                else
                {
                    _db.Author.Add(AuthorVM.Author);
                    await _db.SaveChangesAsync();

                    // Image saving section 
                    // ------------------------------
                    var webRootPath = _webHostEnvironment.WebRootPath;
                    var files = HttpContext.Request.Form.Files;
                    var authorFromDb = await _db.Author.FindAsync(AuthorVM.Author.Id);

                    if (files.Count() > 0)
                    {
                        // Files uploaded
                        // ------------------------------
                        var uploads = Path.Combine(webRootPath, "img", "Authors");
                        var extension = Path.GetExtension(files[0].FileName);

                        // Create file for author
                        // ------------------------------
                        using (var fileStream = new FileStream(Path.Combine(uploads, AuthorVM.Author.Id + extension), FileMode.Create))
                        {
                            // Add file added by user into created file
                            // ------------------------------
                            files[0].CopyTo(fileStream);
                        }

                        // Add img image path into database
                        // ------------------------------
                        authorFromDb.Image = SD.AuthorsImgPath + AuthorVM.Author.Id + extension;
                    }
                    else
                    {
                        // image wasn't uploaded so create image for current author
                        // from default image and save it's path into db
                        // ------------------------------
                        var uploads = Path.Combine(webRootPath, SD.AuthorsImgPath + SD.DefaultAuthorImgName);
                        var newImgPath = SD.AuthorsImgPath + AuthorVM.Author.Id + ".jpg";
                        System.IO.File.Copy(uploads, webRootPath + newImgPath);

                        // Add img image path into database
                        // ------------------------------
                        authorFromDb.Image = newImgPath;

                    }

                    // Save Changes to database
                    // ------------------------------
                    await _db.SaveChangesAsync();

                    // Redirect into index page
                    // ------------------------------
                    return RedirectToAction(nameof(Index));

                }
            }

            // If something fails go back to the same view
            // ------------------------------
            return View(AuthorVM);

        }

        // GET: Admin/Author/Edit
        // ------------------------------
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

        // GET: Admin/Author/Details
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

        private bool AuthorExists(short id)
        {
            return _db.Author.Any(e => e.Id == id);
        }
    }
}
