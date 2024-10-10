using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MovieStore.Data.Models.Domain;

namespace MovieStore.Data
{
    public class DatabaseContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Genre> Genres { get; set; } = null!;
        public DbSet<Movie> Movies { get; set; } = null!;
        public DbSet<MovieGenre> MovieGenres { get; set; } = null!;

        public DatabaseContext(DbContextOptions<DatabaseContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<MovieGenre>()
                .HasKey(mg => new { mg.MovieId, mg.GenreId });

            builder.Entity<MovieGenre>()
                .HasOne(mg => mg.Movie)
                .WithMany(g => g.Genres)
                .HasForeignKey(mg => mg.MovieId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<MovieGenre>()
                .HasOne(mg => mg.Genre)
                .WithMany(m => m.Movies)
                .HasForeignKey(mg => mg.GenreId)
                .OnDelete(DeleteBehavior.NoAction);

            base.OnModelCreating(builder);
        }
    }
}