using Microsoft.EntityFrameworkCore;
using MovieAPILab.Models;

namespace MovieAPILab.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Movie>().HasData(
                new Movie() { Id = 1, Title = "Halloween", Category = "Horror", Year = 1978 },
                new Movie() { Id = 2, Title = "Mr. Brooks", Category = "Drama", Year = 2007 },
                new Movie() { Id = 3, Title = "Star Wars", Category = "SciFi", Year = 1977 },
                new Movie() { Id = 4, Title = "Alien", Category = "Horror", Year = 1979 },
                new Movie() { Id = 5, Title = "Mrs. Doubtfire", Category = "Comedy", Year = 1992 }
                 );
        }
        public DbSet<Movie> Movies { get; set; }
    }
}
