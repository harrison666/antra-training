using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using ApplicationCore.Entities;
using ApplicationCore.Exceptions;
using ApplicationCore.Models.Request;
using ApplicationCore.Models.Response;
using ApplicationCore.RepositoryInterfaces;
using ApplicationCore.ServiceInterfaces;
using AutoMapper;

namespace Infrastructure.Services
{

    public class MovieService : IMovieService
    {
        private readonly IAsyncRepository<Favorite> _favoriteRepository;
        private readonly IAsyncRepository<Genre> _genreRepository;
        private readonly IMovieRepository _movieRepository;
        private readonly IPurchaseRepository _purchaseRepository;
        private readonly IAsyncRepository<Review> _reviewRepository;


        public MovieService(IMovieRepository movieRepository, IPurchaseRepository purchaseRepository,
            IAsyncRepository<Favorite> favoriteRepository, IAsyncRepository<Review> reviewRepository,
            IAsyncRepository<Genre> genreRepository)
        {
            _movieRepository = movieRepository;
            _purchaseRepository = purchaseRepository;
            _favoriteRepository = favoriteRepository;
            _reviewRepository = reviewRepository;
            _genreRepository = genreRepository;
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
                    Title = movie.Title,
                    PosterUrl = movie.PosterUrl,
                    Budget = movie.Budget,
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

        public async Task<List<ReviewMovieResponseModel>> GetReviewsForMovie(int id)
        {
            var reviews = await _movieRepository.GetReviewsByMovieId(id);

            var selectedReviews = new List<ReviewMovieResponseModel>();
            foreach (var review in reviews)
            {
                selectedReviews.Add(new ReviewMovieResponseModel
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

        public async Task<MovieDetailsResponseModel> CreateMovie(MovieCreateRequestModel movieCreateRequestModel)
        {
            //check whether the user is Admin and can create the movie claim
            var movie = new Movie()
            {
                Title = movieCreateRequestModel.Title,
                Overview = movieCreateRequestModel.Overview,
                Tagline = movieCreateRequestModel.Tagline,
                Revenue = movieCreateRequestModel.Revenue,
                Budget = movieCreateRequestModel.Budget,
                ImdbUrl = movieCreateRequestModel.ImdbUrl,
                TmdbUrl = movieCreateRequestModel.TmdbUrl,
                PosterUrl = movieCreateRequestModel.PosterUrl,
                BackdropUrl = movieCreateRequestModel.BackdropUrl,
                OriginalLanguage = movieCreateRequestModel.OriginalLanguage,
                ReleaseDate = movieCreateRequestModel.ReleaseDate,
                RunTime = movieCreateRequestModel.RunTime,
                Price = movieCreateRequestModel.Price,
                Genres = new List<Genre>()
            };
            foreach (var genre in movieCreateRequestModel.Genres)
            {
                movie.Genres.Add(new Genre()
                {
                    Id = genre.Id,
                    Name = genre.Name,
                });
            }

            var newMovie = await _movieRepository.AddAsync(movie);
            var moiveResponse = new MovieDetailsResponseModel()
            {
                Id = newMovie.Id,
                Title = newMovie.Title,
                Budget = newMovie.Budget,
                PosterUrl = newMovie.PosterUrl,
                Rating = newMovie.Rating,
                ReleaseDate = newMovie.ReleaseDate,
                BackdropUrl = newMovie.BackdropUrl,
                ImdbUrl = newMovie.ImdbUrl,
                Tagline = newMovie.Tagline,
                Price = newMovie.Price,
                Revenue = newMovie.Revenue,
                RunTime = newMovie.RunTime,
                Overview = newMovie.Overview,
                TmdbUrl = newMovie.TmdbUrl,

            };
            return moiveResponse;
            
            
        }

        public async Task<MovieDetailsResponseModel> UpdateMovie(MovieCreateRequestModel movieCreateRequestModel)
        {
            var movie = new Movie()
            {
                Id = movieCreateRequestModel.Id,
                Title = movieCreateRequestModel.Title,
                Overview = movieCreateRequestModel.Overview,
                Tagline = movieCreateRequestModel.Tagline,
                Revenue = movieCreateRequestModel.Revenue,
                Budget = movieCreateRequestModel.Budget,
                ImdbUrl = movieCreateRequestModel.ImdbUrl,
                TmdbUrl = movieCreateRequestModel.TmdbUrl,
                PosterUrl = movieCreateRequestModel.PosterUrl,
                BackdropUrl = movieCreateRequestModel.BackdropUrl,
                OriginalLanguage = movieCreateRequestModel.OriginalLanguage,
                ReleaseDate = movieCreateRequestModel.ReleaseDate,
                RunTime = movieCreateRequestModel.RunTime,
                Price = movieCreateRequestModel.Price,
                Genres = new List<Genre>()
            };
            foreach (var genre in movieCreateRequestModel.Genres)
            {
                movie.Genres.Add(new Genre()
                {
                    Id = genre.Id,
                    Name = genre.Name,
                });
            }

            var newMovie = await _movieRepository.UpdateAsync(movie);
            var moiveResponse = new MovieDetailsResponseModel()
            {
                Id = newMovie.Id,
                Title = newMovie.Title,
                Budget = newMovie.Budget,
                PosterUrl = newMovie.PosterUrl,
                Rating = newMovie.Rating,
                ReleaseDate = newMovie.ReleaseDate,
                BackdropUrl = newMovie.BackdropUrl,
                ImdbUrl = newMovie.ImdbUrl,
                Tagline = newMovie.Tagline,
                Price = newMovie.Price,
                Revenue = newMovie.Revenue,
                RunTime = newMovie.RunTime,
                Overview = newMovie.Overview,
                TmdbUrl = newMovie.TmdbUrl,

            };
            return moiveResponse;
        }

        public async Task<List<PurchaseResponseModel>> GetAllMoviePurchases()
        {
            var purchases = await _purchaseRepository.GetAllPurchases();

            var purchaseResponse = new List<PurchaseResponseModel>();
            foreach (var purchase in purchases)
            {
                purchaseResponse.Add(new PurchaseResponseModel()
                {
                    UserId = purchase.UserId,
                    PurchaseDateTime = purchase.PurchaseDateTime,
                    MovieId = purchase.MovieId,
                    PurchaseNumber = purchase.PurchaseNumber,
                    TotalPrice = purchase.TotalPrice,
                    Id = purchase.Id,
                    Customer = purchase.Customer,
                    Movie = purchase.Movie,
                });
            }

            return purchaseResponse;
        }
    }
}
