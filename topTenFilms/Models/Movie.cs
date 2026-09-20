using System.ComponentModel.DataAnnotations;

namespace topTenFilms.Models
{
    public class Movie
    {
        public int Id { get; set; }

        [Display(Name = "Ім'я")]
        public required string Name { get; set; }

        [Display(Name = "Режисер")]
        public required string Director { get; set; }

        [Display(Name = "Жанр")]
        public required string Genre { get; set; }

        [Range(1900, 2026, ErrorMessage = "Рік випуску повинен бути від 1895 до поточного року")]
        [Display(Name = "Рік випуску")]
        public required int YearOfRelease { get; set; }

        [Display(Name = "Зображення")]
        public required string? Image { get; set; }

        [Display(Name = "Опис")]
        public required string Description { get; set; }

        [Range(0.0, 10.0, ErrorMessage = "Рейтинг повинен бути в межах від 0 до 10")]
        [Display(Name = "Рейтинг")]
        public required double Rating { get; set; }
    }
}
