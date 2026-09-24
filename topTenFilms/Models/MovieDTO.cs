using System.ComponentModel.DataAnnotations;
using topTenFilms.Annotations;

namespace topTenFilms.Models
{
    public class MovieDTO
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Поле 'Ім'я' є обов'язковим")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Поле 'Режисер' є обов'язковим")]
        public string? Director { get; set; }

        [Required(ErrorMessage = "Поле 'Жанр' є обов'язковим")]
        public string? Genre { get; set; }

        [Range(1895, 2026, ErrorMessage = "Рік випуску повинен бути від 1895 до 2026 року")]
        public int YearOfRelease { get; set; }

        [Required(ErrorMessage = "Будь ласка, завантажте постер")]
        [MyAttribute([".jpg", ".jpeg", ".png"], ErrorMessage = "Дозволені лише файли з розширенням: .jpg, .jpeg, .png")]
        public IFormFile Poster { get; set; }

        [Required(ErrorMessage = "Поле 'Опис' є обов'язковим")]
        public string? Description { get; set; }

        [Range(0.0, 10.0, ErrorMessage = "Рейтинг повинен бути в межах від 0 до 10")]
        public double Rating { get; set; }
    }
}
