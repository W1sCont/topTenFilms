using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using topTenFilms.Models;

namespace topTenFilms.Controllers
{
    public class AdminController(MovieContext context, IWebHostEnvironment appEnvironment) : Controller
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
        // GET: Home/Add
        public IActionResult Create() => View();

        // Post: Home/Add
        [HttpPost]
        [ValidateAntiForgeryToken]
        [RequestSizeLimit(1_000_000_000)]
        public async Task<IActionResult> Create([Bind("Id,Name,Director,Genre,YearOfRelease,Image,Description,Rating")] Movie movie, IFormFile? uploadedFile)
        {
            if (uploadedFile is null || uploadedFile.Length == 0)
            {
                ModelState.AddModelError("uploadedFile", "Будь ласка, завантажте постер");
            }
            ModelState.Remove(nameof(Movie.Image));
            if (!ModelState.IsValid) return View(movie);

            var fileName = Path.GetFileName(uploadedFile.FileName);
            var relativePath = $"{fileName}";
            var absolutePath = Path.Combine(appEnvironment.WebRootPath, "Posters", fileName);
            Directory.CreateDirectory(Path.Combine(appEnvironment.WebRootPath, "Posters"));
            await using (var fileStream = new FileStream(absolutePath, FileMode.Create))
            {
                await uploadedFile.CopyToAsync(fileStream);
            }

            movie.Image = relativePath;

            _context.Add(movie);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        // GET: Home/Edit
        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null) return NotFound();
            var film = await _context.Movies
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.Id == id);
            return film is null ? NotFound() : View(film);
        }
        // Post: Home/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Director,Genre,YearOfRelease,Image,Description,Rating")] Movie movie, IFormFile? uploadedFile)
        {
            if (id != movie.Id) return NotFound();
            if (!ModelState.IsValid) return View(movie);

            if (uploadedFile is not null)
            {
                var fileName = Path.GetFileName(uploadedFile.FileName);
                var relativePath = $"{fileName}";
                movie.Image = relativePath;
                var absolutePath = Path.Combine(appEnvironment.WebRootPath, "Posters", fileName);
                Directory.CreateDirectory(Path.Combine(appEnvironment.WebRootPath, "Posters"));
                await using (var fileStream = new FileStream(absolutePath, FileMode.Create))
                {
                    await uploadedFile.CopyToAsync(fileStream);
                }
            }
            else
            {
                var existingMovie = await _context.Movies.AsNoTracking().FirstOrDefaultAsync(m => m.Id == id);
                if (existingMovie is null) return NotFound();
                movie.Image = existingMovie.Image;
            }

            _context.Update(movie);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        // GET: Home/Delete
        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null) return NotFound();
            var film = await _context.Movies
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.Id == id);
            return film is null ? NotFound() : View(film);
        }
        // Post: Home/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var film = await _context.Movies.FindAsync(id);
            if (film is null) return NotFound();

            if (!string.IsNullOrEmpty(film.Image))
            {
                var relativePath = $"{film.Image}";
                var absolutePath = Path.Combine(appEnvironment.WebRootPath, "Posters", film.Image);
                Directory.CreateDirectory(Path.Combine(appEnvironment.WebRootPath, "Posters"));
                if (System.IO.File.Exists(absolutePath))
                {
                    System.IO.File.Delete(absolutePath);
                }
            }

            _context.Movies.Remove(film);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
