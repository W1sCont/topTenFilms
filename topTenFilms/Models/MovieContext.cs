using Microsoft.EntityFrameworkCore;
namespace topTenFilms.Models
{
    public class MovieContext : DbContext
    {
        public DbSet<Movie> Movies { get; set; }
        public MovieContext(DbContextOptions<MovieContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Movie>().HasData(
                new Movie {
                    Id = 1,
                    Name = "The Shawshank Redemption",
                    Director = "Frank Darabont",
                    Genre = "Drama",
                    YearOfRelease = 1994,
                    Image = @"shawshank.jpg",
                    Description = "Two imprisoned men bond over a number of years, finding solace and eventual redemption through acts of common decency.",
                    Rating = 9.3
                },
                new Movie
                {
                    Id = 2,
                    Name = "The Godfather",
                    Director = "Francis Ford Coppola",
                    Genre = "Crime, Drama",
                    YearOfRelease = 1972,
                    Image = @"godfather.jpg",
                    Description = "The aging patriarch of an organized crime dynasty transfers control of his clandestine empire to his reluctant son.",
                    Rating = 9.2
                },
                new Movie
                {
                    Id = 3,
                    Name = "The Dark Knight",
                    Director = "Christopher Nolan",
                    Genre = "Action, Crime, Drama",
                    YearOfRelease = 2008,
                    Image = @"darkknight.jpg",
                    Description = "When the menace known as the Joker emerges from his mysterious past, he wreaks havoc and chaos on the people of Gotham.",
                    Rating = 9.0
                },
                new Movie
                {
                    Id = 4,
                    Name = "Pulp Fiction",
                    Director = "Quentin Tarantino",
                    Genre = "Crime, Drama",
                    YearOfRelease = 1994,
                    Image = @"pulpfiction.jpg",
                    Description = "The lives of two mob hitmen, a boxer, a gangster's wife, and a pair of diner bandits intertwine in four tales of violence and redemption.",
                    Rating = 9.0
                },
                new Movie
                {
                    Id = 5,
                    Name = "The Lord of the Rings: The Return of the King",
                    Director = "Peter Jackson",
                    Genre = "Action, Adventure, Drama",
                    YearOfRelease = 2003,
                    Image = @"thelordoftherings.jpg",
                    Description = "Gandalf and Aragorn lead the World of Men against Sauron's army to draw his gaze from Frodo and Sam as they approach Mount Doom with the One Ring.",
                    Rating = 9.0
                },
                new Movie
                {
                    Id = 6,
                    Name = "Forrest Gump",
                    Director = "Robert Zemeckis",
                    Genre = "Drama, Romance",
                    YearOfRelease = 1994,
                    Image = @"forrestgump.jpg",
                    Description = "The presiding administrator of a small town in the American South, who is also a former astronaut.",
                    Rating = 8.8
                },
                new Movie
                {
                    Id = 7,
                    Name = "Inception",
                    Director = "Christopher Nolan",
                    Genre = "Action, Adventure, Sci-Fi",
                    YearOfRelease = 2010,
                    Image = @"inception.jpg",
                    Description = "A thief who steals corporate secrets through the use of dream-sharing technology is given the inverse task of planting an idea into the mind of a CEO.",
                    Rating = 8.8
                },
                new Movie
                {
                    Id = 8,
                    Name = "Fight Club",
                    Director = "David Fincher",
                    Genre = "Drama",
                    YearOfRelease = 1999,
                    Image = @"fightclub.jpg",
                    Description = "An insomniac office worker and a devil-may-care soap maker form an underground fight club that evolves into something much, much more.",
                    Rating = 8.8
                },
                new Movie
                {
                    Id = 9,
                    Name = "The Matrix",
                    Director = "Lana Wachowski, Lilly Wachowski",
                    Genre = "Action, Sci-Fi",
                    YearOfRelease = 1999,
                    Image = @"matrix.jpg",
                    Description = "A computer hacker learns from mysterious rebels about the true nature of his reality and his role in the war against its controllers.",
                    Rating = 8.7
                },
                new Movie
                {
                    Id = 10,
                    Name = "Goodfellas",
                    Director = "Martin Scorsese",
                    Genre = "Biography, Crime, Drama",
                    YearOfRelease = 1990,
                    Image = @"goodfellas.jpg",
                    Description = "The story of Henry Hill and his life in the mob, covering his relationship with his wife Karen Hill and his mob partners Jimmy Conway and Tommy DeVito.",
                    Rating = 8.7
                }
            );
        }
    }
}
