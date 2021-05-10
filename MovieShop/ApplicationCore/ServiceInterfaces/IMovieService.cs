using ApplicationCore.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.ServiceInterfaces
{
    public interface IMovieService
    {
        Task<List<MovieResponseModel>> GetAllMovies();
        Task<MovieResponseModel> GetMovieById(int id);
        Task<List<MovieResponseModel>> GetTop30RevenueMovies();
        Task<List<MovieResponseModel>> GetTop30RatedMovies();
        Task<List<MovieResponseModel>> GetMoviesByGenre(int genreId);
        Task<List<ReviewResponseModel>> GetReviewsForMovie(int Id);
        Task<MovieDetailsResponseModel> GetMovieAsync(int id);


    }
}
