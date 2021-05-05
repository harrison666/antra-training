using ApplicationCore.Entities;
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
            var movies = await _dbContext.Movies.OrderByDescending(m => m.Rating).Take(30).ToListAsync();
            return movies;
        }

        public override async Task<Movie> GetByIdAsync(int id)
        {
            var movie = await _dbContext.Movies.FirstOrDefaultAsync(m => m.Id == id);
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

            if (totalReviewCountByMovie == 0)
            {
                throw new Exception("NO Movies found for this genre");
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
