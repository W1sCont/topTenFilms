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

        [Display(Name = "Рік випуску")]
        public required int YearOfRelease { get; set; }

        [Display(Name = "Зображення")]
        public required string Image { get; set; }

        [Display(Name = "Опис")]
        public required string Description { get; set; }

        [Display(Name = "Рейтинг")]
        public required double Rating { get; set; }
    }
}
