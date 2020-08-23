using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Books.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Books.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class GenreController : Controller
    {
        private readonly ApplicationDbContext _db;

        public GenreController(ApplicationDbContext db)
        {
            _db = db;
        }

        // GET Index action
        // -------------------------
        public async Task<IActionResult> Index()
        {

            return View(await _db.Genre.ToListAsync());
        }

        // GET Create action
        // -------------------------
        public IActionResult Create()
        {
            return View();
        }
    }
}
