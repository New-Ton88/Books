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

            return View(await _db.Genre.Include(g => g.Category)
                                        .OrderBy(g => g.Category)
                                        .ThenBy(g =>g.Name)
                                        .ToListAsync());

            
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
                GenresList = await _db.Genre.OrderBy(g => g.Category)
                                            .ThenBy(g => g.Name)
                                            .Select(g => g.Name)
                                            .Distinct()
                                            .ToListAsync()
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
                    StatusMessage = "Error: Genre " + genres.First().Name + " exists under " + genres.First().Category.Name
                                                                       + " category. Please change name.";
                }
                else
                {

                    StatusMessage = "Genre " + model.Genre.Name + " succesfully created";

                    await _db.Genre.AddAsync(model.Genre);
                    await _db.SaveChangesAsync();

                    

                    return RedirectToAction(nameof(Index));
                }
   
            }

            CategoryAndGenreViewModel modelVM = new CategoryAndGenreViewModel()
            {
                Categories = await _db.Category.ToListAsync(),
                Genre = model.Genre,
                GenresList = await _db.Genre.OrderBy(g => g.Category)
                                        .ThenBy(g => g.Name)
                                        .Select(g => g.Name)
                                        .Distinct()
                                        .ToListAsync(),
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

            // If all commands passed then create ViewModel object to send it
            // to the View
            // -------------------------
            var model = new CategoryAndGenreViewModel()
            {
                Categories = await _db.Category.OrderBy(c => c.Name).ToListAsync(),
                Genre = genre,
                GenresList = await _db.Genre
                                        .OrderBy(g => g.Name)
                                        .Select(g => g.Name)
                                        .Distinct()
                                        .ToListAsync()
            };

            // return ViewModel to View
            // -------------------------
            return View(model);

        }

        // POST Edit
        // -------------------------
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CategoryAndGenreViewModel model, short id)
        {
            // In this case I don't even have to send from View 
            // any data (asp-route-id) it still knows that I am sending genre with form

            // if data send is OK then update database
            // -------------------------
            if (ModelState.IsValid)
            {
                // Verify if genre isn't created already
                // -------------------------
                var genres = await _db.Genre.Include(g => g.Category)
                                            .Where(g => g.Name == model.Genre.Name && g.CategoryId == model.Genre.CategoryId).ToListAsync();

                // If genre already exist display message
                // -------------------------
                if (genres.Count() > 0)
                {
                    StatusMessage = "Error: Genre " + genres.First().Name + " exists under " + genres.First().Category.Name
                                                                       + " category. Please change name.";
                }
                // If genre isn't exist then update it
                // -------------------------
                else
                {

                    // When I use: 
                    // _db.Update(model.Genre);
                    // New element is added to db
                    // Use expression below to update
                    // -------------------------
                    var genreFromDb = await _db.Genre.FindAsync(id);
                    genreFromDb.Name = model.Genre.Name;

                    await _db.SaveChangesAsync();

                    StatusMessage = "Genre " + model.Genre.Name + " succesfully modified";


                    return RedirectToAction(nameof(Index));
                }
                
            }

            // Create new View model
            // -------------------------
            var modelVM = new CategoryAndGenreViewModel()
            {
                Categories = await _db.Category.ToListAsync(),
                Genre = model.Genre,
                GenresList = await _db.Genre.OrderBy(g => g.Name).Select(g => g.Name).ToListAsync(),
                StatusMessage = StatusMessage
            };

            // This is added because in case of trying to pass Subcategory name
            // that already exits id is lost and next update will fail
            // because of bad id
            // -------------------------
            model.Genre.Id = id;

            // if Model is Invalid go back to Edit View
            // -------------------------
            return View(modelVM);
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

            var model = new CategoryAndGenreViewModel()
            {
                Categories = await _db.Category.OrderBy(c => c.Name).ToListAsync(),
                Genre = genre,
                GenresList = await _db.Genre.Select(g => g.Name).Distinct().ToListAsync()
            };

            // If all commands passed then display proper View
            // -------------------------
            return View(model);
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

            StatusMessage = "Genre " + genre.Name + " succesfully removed";

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
