using CSharp_Assignment_3_MovieApi.Models;
using Microsoft.EntityFrameworkCore;

namespace CSharp_Assignment_3_MovieApi.DatabaseContext
{
    public class MovieDbContext : DbContext
    {
        //Tables
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Franchise> Franchises { get; set;}
        public DbSet<Character> Characters { get; set; }

        //Connection string
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("connectionString");
        }


        //Relationships
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Movie>()
                .HasOne<Franchise>(m => m.Franchise)
                .WithMany(f => f.Movies)
                .HasForeignKey(m => m.FranchiseId);

            modelBuilder.Entity<Movie>()
                .HasMany<Character>(m => m.Characters)
                .WithMany(c => c.Movies)
                .UsingEntity(j => j.ToTable("Movie_Characters"));
        }
    }
}