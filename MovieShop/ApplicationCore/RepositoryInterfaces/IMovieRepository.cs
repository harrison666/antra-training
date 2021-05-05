using ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.RepositoryInterfaces
{
    public interface IMovieRepository : IAsyncRepository<Movie>
    {
        Task<IEnumerable<Movie>> GetTop30HighestRevenueMovies();
        Task<IEnumerable<Movie>> GetTop30HighestRatedMovies();
        Task<IEnumerable<Movie>> GetMoviesByGenreId(int genreId);
        Task<IEnumerable<Review>> GetReviewsByMovieId(int id);
    }
}
