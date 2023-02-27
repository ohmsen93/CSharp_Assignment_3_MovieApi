using CSharp_Assignment_3_MovieApi.Models;
using Microsoft.EntityFrameworkCore;

namespace CSharp_Assignment_3_MovieApi.DatabaseContext
{
    public class MovieDbContext : DbContext
    {

        private readonly IConfiguration _config;

        public MovieDbContext(IConfiguration config)
        {
            _config = config;
        }
        
        //Tables
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Franchise> Franchises { get; set;}
        public DbSet<Character> Characters { get; set; }

        //Connection string
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_config.GetConnectionString("DefaultConnection"));
        }


        //
        //Database Relationships & initialization
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Relationship between movie and franchise its one franchise to many movies
            modelBuilder.Entity<Movie>()
                .HasOne<Franchise>(m => m.Franchise)
                .WithMany(f => f.Movies)
                .HasForeignKey(m => m.FranchiseId);

            // Relationship between movie and character, as this is a many to many relationship, we generate a new table called Movie_Character, and seed it with info
            modelBuilder.Entity<Movie>()
                .HasMany(m => m.Characters)
                .WithMany(c => c.Movies)
                .UsingEntity<Dictionary<string, object>>(
                    "Movie_Character",
                    r => r.HasOne<Character>().WithMany().HasForeignKey("MovieId"),
                    l => l.HasOne<Movie>().WithMany().HasForeignKey("CharacterId"),
                    je =>
                    {
                        je.HasKey("MovieId", "CharacterId");
                        je.HasData(
                            new { MovieId = 1, CharacterId = 1 },
                            new { MovieId = 1, CharacterId = 2 }
                        );
                    });


            modelBuilder.Entity<Franchise>().HasData(
                new Franchise
                {
                    Id = 1,
                    Name = "Star Wars",
                    Description = "A space opera media franchise created by George Lucas"
                },
                new Franchise
                {
                    Id = 2,
                    Name = "Marvel Cinematic Universe",
                    Description = "A series of superhero films produced by Marvel Studios"
                }
            );

            modelBuilder.Entity<Movie>().HasData(
                new Movie
                {
                    Id = 1,
                    Title = "Star Wars: Episode IV - A New Hope",
                    Genre = "Science Fiction",
                    ReleaseYear = new DateTime(1977, 5, 25),
                    Director = "George Lucas",
                    Picture = "https://www.example.com/star-wars-episode-4.jpg",
                    Trailer = "https://www.example.com/star-wars-episode-4-trailer.mp4",
                    FranchiseId = 1
                },
                new Movie
                {
                    Id = 2,
                    Title = "Avengers: Endgame",
                    Genre = "Superhero",
                    ReleaseYear = new DateTime(2019, 4, 26),
                    Director = "Anthony Russo, Joe Russo",
                    Picture = "https://www.example.com/avengers-endgame.jpg",
                    Trailer = "https://www.example.com/avengers-endgame-trailer.mp4",
                    FranchiseId = 2
                }
            );
            modelBuilder.Entity<Character>().HasData(
                new Character { Id = 1, FirstName = "Harry", LastName = "Potter", Alias = "The Boy Who Lived", Gender = "Male", Picture = "https://example.com/harry_potter.jpg" },
                new Character { Id = 2, FirstName = "Hermione", LastName = "Granger", Alias = "The Brightest Witch of Her Age", Gender = "Female", Picture = "https://example.com/hermione_granger.jpg" },
                new Character { Id = 3, FirstName = "Ron", LastName = "Weasley", Alias = "The Boy Who Stayed", Gender = "Male", Picture = "https://example.com/ron_weasley.jpg" },
                new Character { Id = 4, FirstName = "Tony", LastName = "Stark", Alias = "Iron Man", Gender = "Male", Picture = "https://example.com/tony_stark.jpg" },
                new Character { Id = 5, FirstName = "Steve", LastName = "Rogers", Alias = "Captain America", Gender = "Male", Picture = "https://example.com/steve_rogers.jpg" },
                new Character { Id = 6, FirstName = "Natasha", LastName = "Romanoff", Alias = "Black Widow", Gender = "Female", Picture = "https://example.com/natasha_romanoff.jpg" }
            );





        }
    }
}