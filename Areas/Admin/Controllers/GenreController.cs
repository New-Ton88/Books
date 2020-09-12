using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Books.Data;
using Books.Models;
using Books.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;

namespace Books.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class GenreController : Controller
    {
        private readonly ApplicationDbContext _db;

        [TempData]
        public string StatusMessage { get; set; }

        public GenreController(ApplicationDbContext db)
        {
            _db = db;
        }

        // GET Index action
        // -------------------------
        public async Task<IActionResult> Index()
        {

            return View(await _db.Genre.Include(g => g.Category).OrderBy(g =>g.Name).ToListAsync());
        }

        // GET Create action
        // -------------------------
        public async Task<IActionResult> Create()
        {
            // Create new model in order to display also category for
            // which new Genre will be created
            // -------------------------
            CategoryAndGenreViewModel model = new CategoryAndGenreViewModel()
            {
                Categories = await _db.Category.ToListAsync(),
                Genre = new Genre(),
                GenresList = await _db.Genre.OrderBy(g => g.Name).Select(g => g.Name).Distinct().ToListAsync()
            };

            return View(model);
        }

        // POST Create action
        // -------------------------
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CategoryAndGenreViewModel model)
        {
            if (ModelState.IsValid)
            {
                var genres = _db.Genre.Include(g => g.Category)
                                            .Where(g => g.Name == model.Genre.Name && g.CategoryId == model.Genre.CategoryId);
                
                if (genres.Count() > 0)
                {
                    StatusMessage = "Error: Genre exists under " + genres.First().Name 
                                                                       + " category. Please change name.";
                }
                else
                {
                    await _db.Genre.AddAsync(model.Genre);
                    await _db.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
   
            }

            CategoryAndGenreViewModel modelVM = new CategoryAndGenreViewModel()
            {
                Categories = await _db.Category.ToListAsync(),
                Genre = model.Genre,
                GenresList = await _db.Genre.OrderBy(g => g.Name).Select(g => g.Name).ToListAsync(),
                StatusMessage = StatusMessage
            };

            return View(modelVM);

        }

        // GET Edit action
        // -------------------------
        public async Task<IActionResult> Edit(short? id)
        {
            // Check if id was properly sent
            // -------------------------
            if (id == null)
            {
                return NotFound();
            }

            // If Id was properly sent then get Genre from Db
            // -------------------------
            var genre = await _db.Genre.FindAsync(id);

            // If genre with such Id wasn't found then show Not Found screen
            // -------------------------
            if (genre == null)
            {
                return NotFound();
            }

            // If all commands passed then display proper View
            // -------------------------
            return View(genre);

        }

        // POST Edit
        // -------------------------
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Genre genre)
        {
            // In this case I don't even have to send from View 
            // any data (asp-route-id) it still knows that I am sending genre with form

            // if data send is OK then update database
            // -------------------------
            if (ModelState.IsValid)
            {
                _db.Update(genre);
                await _db.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            // if Model is Invalid go back to Edit View
            // -------------------------
            return View(genre);
        }

        // GET Delete
        // -------------------------
        public async Task<IActionResult> Delete(short? id)
        {
            // Check if id was properly sent
            // -------------------------
            if (id == null)
            {
                return NotFound();
            }

            // If Id was properly sent then get Genre from Db
            // -------------------------
            var genre = await _db.Genre.FindAsync(id);

            // If genre with such Id wasn't found then show Not Found screen
            // -------------------------
            if (genre == null)
            {
                return NotFound();
            }

            // If all commands passed then display proper View
            // -------------------------
            return View(genre);
        }

        // POST Delete action
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
            var genre = await _db.Genre.FindAsync(id);

            // If genre with such Id wasn't found then go back to View
            // -------------------------
            if (genre == null)
            {
                return RedirectToAction(nameof(Index));
            }

            // If genre was found then remove it and save Db
            // -------------------------
            _db.Genre.Remove(genre);
            await _db.SaveChangesAsync();

            return RedirectToAction(nameof(Index));

        }

        // GET Genres action
        // -------------------------
        [ActionName("GenreGet")]
        public async Task<IActionResult> GenreGet(short id)
        {
            // Create genres object
            // -------------------------
            List<Genre> genres = new List<Genre>();

            // get genres from database with requested category id
            // -------------------------
            genres = await (from genre in _db.Genre
                            where genre.CategoryId == id
                            select genre).ToListAsync();

            // return genres as json format
            // -------------------------
            return Json(new SelectList(genres, "Id", "Name"));
        }
    }
}
