using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationCore.Entities;
using ApplicationCore.Exceptions;
using ApplicationCore.Models.Response;
using ApplicationCore.RepositoryInterfaces;
using ApplicationCore.ServiceInterfaces;
using AutoMapper;

namespace Infrastructure.Services
{

    public class MovieService : IMovieService
    {
        private readonly IMovieRepository _movieRepository;
        private readonly IMapper _mapper;

        public MovieService(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }

        public async Task<List<MovieResponseModel>> GetTop30RevenueMovies()
        {
            var movies = await _movieRepository.GetTop30HighestRevenueMovies();

            var topMovies = new List<MovieResponseModel>();
            foreach (var movie in movies)
            {
                topMovies.Add(new MovieResponseModel
                {
                    Id = movie.Id,
                    Budget = movie.Budget,
                    Title = movie.Title,
                    PosterUrl = movie.PosterUrl
                });
            }

            return topMovies;
        }

        public async Task<List<MovieResponseModel>> GetAllMovies()
        {
            var movies = await _movieRepository.ListAllAsync();

            var allMovies = new List<MovieResponseModel>();
            foreach (var movie in movies)
            {
                allMovies.Add(new MovieResponseModel
                {
                    Id = movie.Id,
                    Title = movie.Title
                });
            }

            return allMovies;
        }

        public async Task<List<MovieResponseModel>> GetTop30RatedMovies()
        {
            var movies = await _movieRepository.GetTop30HighestRatedMovies();

            var topMovies = new List<MovieResponseModel>();
            foreach (var movie in movies)
            {
                topMovies.Add(new MovieResponseModel
                {
                    Id = movie.Id,
                    Rating = movie.Rating,
                    Title = movie.Title
                });
            }

            return topMovies;
        }


        public async Task<MovieResponseModel> GetMovieById(int id)
        {
            var movie = await _movieRepository.GetByIdAsync(id);

            var oneMovie = new MovieResponseModel()
            {
                Id = movie.Id,
                Title = movie.Title
            };
            return oneMovie;
        }

        public async Task<List<MovieResponseModel>> GetMoviesByGenre(int genreId)
        {
            var movies = await _movieRepository.GetMoviesByGenreId(genreId);

            var selectedMovies = new List<MovieResponseModel>();
            foreach (var movie in movies)
            {
                selectedMovies.Add(new MovieResponseModel
                {
                    Id = movie.Id,
                    Title = movie.Title,
                    PosterUrl = movie.PosterUrl
                });
            }

            return selectedMovies;
        }

        public async Task<List<ReviewResponseModel>> GetReviewsForMovie(int id)
        {
            var reviews = await _movieRepository.GetReviewsByMovieId(id);

            var selectedReviews = new List<ReviewResponseModel>();
            foreach (var review in reviews)
            {
                selectedReviews.Add(new ReviewResponseModel
                {
                    MovieId = review.MovieId,
                    UserId = review.UserId,
                    Rating = review.Rating,
                    ReviewText = review.ReviewText
                });
            }

            return selectedReviews;
        }

        public async Task<MovieDetailsResponseModel> GetMovieAsync(int id)
        {
            var movie = await _movieRepository.GetByIdAsync(id);
            if (movie == null) throw new NotFoundException("Movie", id);
            //var response = _mapper.Map<MovieDetailsResponseModel>(movie);
            var genres = new List<GenreResponseModel>();
            var casts = new List<CastResponseModel>();

            foreach (var movieCast in movie.MovieCasts)
            {
                casts.Add(new CastResponseModel()
                {
                    Id = movieCast.CastId,
                    Name = movieCast.Cast.Name,
                    Character = movieCast.Character,
                    ProfilePath = movieCast.Cast.ProfilePath,

                });
            }

            foreach (var genre in movie.Genres)
            {
                genres.Add(new GenreResponseModel()
                {
                    Id = genre.Id,
                    Name = genre.Name,
                });
            }

            var response = new MovieDetailsResponseModel()
            {
                Id = movie.Id,
                Title = movie.Title,
                PosterUrl = movie.PosterUrl,
                BackdropUrl = movie.BackdropUrl,
                Rating = movie.Rating,
                Overview = movie.Overview,
                Tagline = movie.Tagline,
                Budget = movie.Budget,
                Revenue = movie.Revenue,
                ImdbUrl = movie.ImdbUrl,
                TmdbUrl = movie.TmdbUrl,
                ReleaseDate = movie.ReleaseDate,
                RunTime = movie.RunTime,
                Price = movie.Price,
                Casts = casts,
                Genres = genres
    };
            return response;
        }
    }
}
