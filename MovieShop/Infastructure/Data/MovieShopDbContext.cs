using ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infastructure.Data
{
    public class MovieShopDbContext:DbContext
    {
        public MovieShopDbContext(DbContextOptions<MovieShopDbContext> options): base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<MovieCast>().HasKey(table => new {
                table.CastId,
                table.MovieId,
                table.Character
            });

            builder.Entity<MovieCrew>().HasKey(table => new { table.MovieId, table.CrewId, table.Department, table.Job });
            builder.Entity<MovieGenre>().HasKey(table => new { table.MovieId, table.GenreId });
            builder.Entity<Review>().HasKey(t => new { t.MovieId, t.UserId });
            builder.Entity<UserRole>().HasKey(t => new { t.UserId, t.RoleId });
        }

        public DbSet<Genre> Genres { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Crew> Crews { get; set; }
        public DbSet<Cast> Casts { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<MovieCast> MovieCasts { get; set; }
        public DbSet<MovieCrew> MovieCrew { get; set; }
        public DbSet<MovieGenre> MovieGenres { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<Trailer> Trailers { get; set; }
        public DbSet<Favorite> Favorites { get; set; }
        public DbSet<Purchase> Purchases { get; set; }

    }
}
