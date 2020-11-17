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
using Microsoft.AspNetCore.Components.Web;
using Books.Functional.Interfaces;
using Books.Functional.Classes;

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

        IAuthorSupport AuthorSupport;


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
            AuthorSupport = new AuthorSupport() {
                WebRootPath = webHostEnvironment.WebRootPath,
                Db = db,
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
            short exitCode;

            (exitCode, StatusMessage) = await AuthorSupport.AuthorCreate(ModelState, AuthorVM, HttpContext);
            
            if (exitCode == 0)
            {
                return RedirectToAction(nameof(Index));
            }

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

            AuthorVM.Author = author;

            return View(AuthorVM);
        }

        // POST: Admin/Author/Edit/id
        // ------------------------------
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(short? id, Author author)
        {

            if (ModelState.IsValid)
            {
                if (id == null)
                {
                    return NotFound();
                }

                var authorFromDb = await _db.Author.FindAsync(id);

                if (authorFromDb == null)
                {
                    return NotFound();
                }

                // Update img in images/Authors folder
                // ------------------------------
                var files = HttpContext.Request.Form.Files;
                

                // Check if image was uploaded
                // ------------------------------
                if (files.Count() > 0)
                {

                    var fileDetails = await AuthorSupport.ImgCreateAsync(AuthorVM, files[0]);
                    var uploads = fileDetails[0];
                    var extension = fileDetails[1];


                    try {
                        var imagePath = authorFromDb.Image.TrimStart('\\');

                        // Delete old image
                        // ------------------------------
                        if (System.IO.File.Exists(imagePath))
                        {
                            System.IO.File.Delete(imagePath);
                        }
                    }
                    catch
                    {
                        
                    };

                    // add new image
                    // ------------------------------
                    using (var filestream = new FileStream(Path.Combine(uploads, id + extension), FileMode.Create))
                    {
                        files[0].CopyTo(filestream);
                    }

                    authorFromDb.Image = SD.AuthorsImgPath + id + extension;
                }

                // Update data from database
                // ------------------------------
                authorFromDb.Name = AuthorVM.Author.Name;
                authorFromDb.Alias = AuthorVM.Author.Alias;
                authorFromDb.Birthday = AuthorVM.Author.Birthday;
                authorFromDb.Description = AuthorVM.Author.Description;
                authorFromDb.Genre01 = AuthorVM.Author.Genre01;
                authorFromDb.Genre02 = AuthorVM.Author.Genre02;
                authorFromDb.Genre03 = AuthorVM.Author.Genre03;
                authorFromDb.Genre04 = AuthorVM.Author.Genre04;
                authorFromDb.Genre05 = AuthorVM.Author.Genre05;
                authorFromDb.Genre06 = AuthorVM.Author.Genre06;
                authorFromDb.Genre07 = AuthorVM.Author.Genre07;
                authorFromDb.GenreId01 = AuthorVM.Author.GenreId01;
                authorFromDb.GenreId02 = AuthorVM.Author.GenreId02;
                authorFromDb.GenreId03 = AuthorVM.Author.GenreId03;
                authorFromDb.GenreId04 = AuthorVM.Author.GenreId04;
                authorFromDb.GenreId05 = AuthorVM.Author.GenreId05;
                authorFromDb.GenreId06 = AuthorVM.Author.GenreId06;
                authorFromDb.GenreId07 = AuthorVM.Author.GenreId07;
                authorFromDb.Language = AuthorVM.Author.Language;
                authorFromDb.LanguageId = AuthorVM.Author.LanguageId;

                // Apply changes to database
                // ------------------------------
                await _db.SaveChangesAsync();

                // Return status message
                // ------------------------------
                StatusMessage = author.Name + " succesfully updated.";

                // Go back to Index view
                // ------------------------------
                return RedirectToAction(nameof(Index));
            }
            return View(AuthorVM);
        }


        // POST: Admin/Author/Delete/id
        // ------------------------------
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(short id)
        {
            var author = await _db.Author.FindAsync(id);
            _db.Author.Remove(author);
            await _db.SaveChangesAsync();

            StatusMessage = author.Name + " successfully deleted";

            return RedirectToAction(nameof(Index));
        }

        // GET: Admin/Author/Details
        public async Task<IActionResult> Details(short? id)
        {
            // Validate author id
            // ------------------------------
            if (id == null)
            {
                return NotFound();
            }

            // Get author from db for id
            // ------------------------------
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

            // Verify if author with 
            if (author == null)
            {
                return NotFound();
            }

            AuthorVM.Author = author;

            return View(AuthorVM);
        }

        private bool AuthorExists(short id)
        {
            return _db.Author.Any(e => e.Id == id);
        }
    }
}
