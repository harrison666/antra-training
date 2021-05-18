using ApplicationCore.Entities;
using ApplicationCore.Exceptions;
using ApplicationCore.Models.Response;
using ApplicationCore.RepositoryInterfaces;
using ApplicationCore.ServiceInterfaces;
using Infastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class MovieRepository : EfRepository<Movie>, IMovieRepository
    {
        public MovieRepository(MovieShopDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<IEnumerable<Movie>> GetTop30HighestRevenueMovies()
        {
            var movies = await _dbContext.Movies.OrderByDescending(m => m.Revenue).Take(30).ToListAsync();
            return movies;
        }
        
        public async Task<IEnumerable<Movie>> GetTop30HighestRatedMovies()
        {
            //var movies = await _dbContext.Movies.OrderByDescending(m => m.Rating).Take(30).ToListAsync();
            var movies = await _dbContext.Movies.ToListAsync();
            foreach (var movie in movies)
            {
                var id = movie.Id;
                movie.Rating = await _dbContext.Reviews.Where(r => r.MovieId == id).DefaultIfEmpty()
                .AverageAsync(r => r == null ? 0 : r.Rating);
            }
            //var rating = await _dbContext.Reviews.Include(r => r.Movie).GroupBy(r => new { r.MovieId, r.Movie })
            //    .Select(g => new { AverageRating = g.Average(p => p.Rating), Movie = g.Key.Movie })
            //    .OrderByDescending(r => r.AverageRating).Take(30).ToListAsync();
            var topMovies = movies.OrderByDescending(m => m.Rating).Take(30).ToList();

            return topMovies;
        }

        public override async Task<Movie> GetByIdAsync(int id)
        {
            var movie = await _dbContext.Movies.Include(m => m.MovieCasts).ThenInclude(m => m.Cast)
                .Include(m => m.Genres)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (movie == null)
            {
                throw new NotFoundException("Movie Not found");
            }

            var movieRating = await _dbContext.Reviews.Where(r => r.MovieId == id).DefaultIfEmpty()
                .AverageAsync(r => r == null ? 0 : r.Rating);
            if (movieRating > 0) movie.Rating = movieRating;

            return movie;
        }

        public async Task<IEnumerable<Movie>> GetMoviesByGenreId(int genreId)
        {
            var totalMoviesCountByGenre =
                await _dbContext.Genres.Include(g => g.Movies).Where(g => g.Id == genreId).SelectMany(g => g.Movies)
                    .CountAsync();

            if (totalMoviesCountByGenre == 0)
            {
                throw new Exception("NO Movies found for this genre");
            }
            var movies = await _dbContext.Genres.Include(g => g.Movies).Where(g => g.Id == genreId)
                .SelectMany(g => g.Movies)
                .OrderByDescending(m => m.Revenue).ToListAsync();
            return movies;
        }

        public async Task<IEnumerable<Review>> GetReviewsByMovieId(int id)
        {
            var totalReviewCountByMovie =
                await _dbContext.Movies.Where(m => m.Id == id).SelectMany(m => m.Reviews)
                    .CountAsync();
            Console.WriteLine(totalReviewCountByMovie);
            if (totalReviewCountByMovie == 0)
            {
                throw new Exception("NO Reviews found for this movie");
            }
            var reviews = await _dbContext.Movies.Where(m => m.Id == id).SelectMany(m => m.Reviews).ToListAsync();
            return reviews;
        }


        //First()
        //FirstOrDefault()
        //Single()
        //SingleOrDefault()
        //Where()
        //GroupBy()
        //ToList()
        //Any()
    }
}
