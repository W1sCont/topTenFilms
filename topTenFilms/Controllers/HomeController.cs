using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using topTenFilms.Models;

namespace topTenFilms.Controllers
{
    public class HomeController(MovieContext context) : Controller
    {
        private readonly MovieContext _context = context;
        public async Task<IActionResult> Index()
        {
            return View(await _context.Movies.AsNoTracking().OrderByDescending(m => m.Id).ToListAsync());
        }

        // GET: Home/Details
        public async Task<IActionResult> Details(int? id)
        {
            if (id is null) return NotFound();
            var film = await _context.Movies
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.Id == id);

            return film is null ? NotFound() : View(film);
        }
    }
}
