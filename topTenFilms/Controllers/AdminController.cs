using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using topTenFilms.Models;
using topTenFilms.Annotations;

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
        public async Task<IActionResult> Create([Bind("Id,Name,Director,Genre,YearOfRelease,Poster,Description,Rating")] MovieDTO dto)
        {
            if (!ModelState.IsValid) return View(dto);


            var fileName = Path.GetFileName(dto.Poster.FileName);
            var relativePath = $"{fileName}";
            var absolutePath = Path.Combine(appEnvironment.WebRootPath, "Posters", fileName);
            Directory.CreateDirectory(Path.Combine(appEnvironment.WebRootPath, "Posters"));
            await using (var fileStream = new FileStream(absolutePath, FileMode.Create))
            {
                await dto.Poster.CopyToAsync(fileStream);
            }

            var movie = new Movie
            {
                Name = dto.Name,
                Director = dto.Director,
                Genre = dto.Genre,
                YearOfRelease = dto.YearOfRelease,
                Image = relativePath,
                Description = dto.Description,
                Rating = dto.Rating
            };

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
            var dto = new MovieDTO
            {
                Id = film.Id,
                Name = film.Name,
                Director = film.Director,
                Genre = film.Genre,
                YearOfRelease = film.YearOfRelease,
                Description = film.Description,
                Rating = film.Rating
            };
            return film is null ? NotFound() : View(dto);
        }
        // Post: Home/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Director,Genre,YearOfRelease,Poster,Description,Rating")] MovieDTO dto)
        {
            if (id != dto.Id) return NotFound();
            if (!ModelState.IsValid) return View(dto);
            Movie movie = null;
            if (dto.Poster is not null)
            {
                var fileName = Path.GetFileName(dto.Poster.FileName);
                var relativePath = $"{fileName}";
                var absolutePath = Path.Combine(appEnvironment.WebRootPath, "Posters", fileName);
                Directory.CreateDirectory(Path.Combine(appEnvironment.WebRootPath, "Posters"));
                await using (var fileStream = new FileStream(absolutePath, FileMode.Create))
                {
                    await dto.Poster.CopyToAsync(fileStream);
                }
                movie = new Movie
                {
                    Id = dto.Id,
                    Name = dto.Name,
                    Director = dto.Director,
                    Genre = dto.Genre,
                    YearOfRelease = dto.YearOfRelease,
                    Image = relativePath,
                    Description = dto.Description,
                    Rating = dto.Rating
                };
            }
            else return View(dto);

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
