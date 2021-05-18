using ApplicationCore.Entities;
using ApplicationCore.Exceptions;
using ApplicationCore.Models.Request;
using ApplicationCore.Models.Response;
using ApplicationCore.RepositoryInterfaces;
using ApplicationCore.ServiceInterfaces;
using AutoMapper;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using static ApplicationCore.Models.Response.PurchaseResponseModel;
using static ApplicationCore.Models.Response.UserPurchaseResponseModel;

namespace Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly ICurrentUserService _currentUserService;
        //private readonly ICryptoService _encryptionService;
        private readonly IAsyncRepository<Favorite> _favoriteRepository;
        private readonly IMovieService _movieService;
        private readonly IAsyncRepository<Purchase> _purchaseRepository;
        private readonly IAsyncRepository<Review> _reviewRepository;
        private readonly IUserRepository _userRepository;
        //private readonly IBlobService _blobService;

        public UserService(IUserRepository userRepository,
            IAsyncRepository<Favorite> favoriteRepository, ICurrentUserService currentUserService,
            IMovieService movieService, IAsyncRepository<Purchase> purchaseRepository,
            IAsyncRepository<Review> reviewRepository)
        {
            //_encryptionService = encryptionService;
            _userRepository = userRepository;
            _favoriteRepository = favoriteRepository;
            _currentUserService = currentUserService;
            _movieService = movieService;
            _purchaseRepository = purchaseRepository;
            _reviewRepository = reviewRepository;
            //_blobService = blobService;
        }

        public async Task<UserRegisterResponseModel> RegisterUser(UserRegisterRequestModel registerRequest)
        {
            // first check whether the user with same email exists in our database.
            var dbUser = await _userRepository.GetUserByEmail(registerRequest.Email);

            // if user exists
            if (dbUser != null)
            {
                throw new Exception("User Already exists, please try to login");
            }

            // generate a unique salt
            var salt = CreateSalt();

            // hash the password with salt generated in the above step
            var hashedPassword = CreateHashedPassword(registerRequest.Password, salt);

            // then create User Object and save it to database with UserRepository 
            var user = new User()
            {
                FirstName = registerRequest.FirstName,
                LastName = registerRequest.LastName,
                Email = registerRequest.Email,
                Salt = salt,
                HashedPassword = hashedPassword,
                DateOfBirth = registerRequest.DateOfBirth
            };

            // call repository to save User info that included salt and hashed password
            var createdUser = await _userRepository.AddAsync(user);

            // map user object to UserRegisterResponseModel object
            var createdUserResponse = new UserRegisterResponseModel
            {
                Id = createdUser.Id,
                Email = createdUser.Email,
                FirstName = createdUser.FirstName,
                LastName = createdUser.LastName
            };

            return createdUserResponse;


        }

        public async Task<UserLoginResponseModel> ValidateUser(string email, string password)
        {
            // get the user info my email by using GetUserByEmail

            var dbUser = await _userRepository.GetUserByEmail(email);

            if (dbUser == null)
            {
                return null;
            }

            var hashedPassword = CreateHashedPassword(password, dbUser.Salt);

            if (hashedPassword == dbUser.HashedPassword)
            {
                // passwords match so create response model object

                var loginResponseModel = new UserLoginResponseModel
                {
                    Id = dbUser.Id,
                    Email = dbUser.Email,
                    FirstName = dbUser.FirstName,
                    LastName = dbUser.LastName
                };
                return loginResponseModel;
            }

            return null;
        }

        private string CreateSalt()
        {
            byte[] randomBytes = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomBytes);
            }
            return Convert.ToBase64String(randomBytes);
        }

        private string CreateHashedPassword(string password, string salt)
        {
            var hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: Convert.FromBase64String(salt),
                prf: KeyDerivationPrf.HMACSHA512,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));
            return hashed;
        }

        public async Task<List<MovieResponseModel>> GetPurchases(int id)
        {
            var movies = await _userRepository.GetPurchasesByIdAsync(id);
            var purchasedMovies = new List<MovieResponseModel>();
            foreach (var movie in movies)
            {
                purchasedMovies.Add(new MovieResponseModel()
                {
                    Id = movie.Id,
                    Title = movie.Title,
                    PosterUrl = movie.PosterUrl
                });
            }
            return purchasedMovies;
            
        }

        public async Task<UserProfileResponseModel> GetProfile(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null)
            {
                throw new NotFoundException("user does not exist");
            }
            var profile = new UserProfileResponseModel()
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                PhoneNumber = user.PhoneNumber,
                Email = user.Email,
                DateOfBirth = user.DateOfBirth,

            };
            
            return profile;
        }

        public async Task<User> GetUser(string email)
        {
            return await _userRepository.GetUserByEmail(email);
        }

        public async Task AddFavorite(FavoriteRequestModel favoriteRequest)
        {
            if (_currentUserService.UserId != favoriteRequest.UserId)
                throw new HttpException(HttpStatusCode.Unauthorized, "You are not Authorized to purchase");
            // See if Movie is already Favorite.
            if (await FavoriteExists(favoriteRequest.UserId, favoriteRequest.MovieId))
                throw new ConflictException("Movie already Favorited");

            var favorite = new Favorite()
            {
                MovieId = favoriteRequest.MovieId,
                UserId = favoriteRequest.UserId
            };

            await _favoriteRepository.AddAsync(favorite);
        }

        public async Task RemoveFavorite(FavoriteRequestModel favoriteRequest)
        {
            var dbFavorite =
                await _favoriteRepository.ListAsync(r => r.UserId == favoriteRequest.UserId &&
                                                         r.MovieId == favoriteRequest.MovieId);
            await _favoriteRepository.DeleteAsync(dbFavorite.First());
        }

        public async Task<bool> FavoriteExists(int id, int movieId)
        {
            return await _favoriteRepository.GetExistsAsync(f => f.MovieId == movieId &&
                                                                 f.UserId == id);
        }

        public async Task<FavoriteResponseModel> GetAllFavoritesForUser(int id)
        {
            if (_currentUserService.UserId != id)
                throw new HttpException(HttpStatusCode.Unauthorized, "You are not Authorized to View Favorites");

            var favoriteMovies = await _favoriteRepository.ListAllWithIncludesAsync(
                p => p.UserId == _currentUserService.UserId,
                p => p.Movie);
            var favoriteResponse = new FavoriteResponseModel() 
            {
                UserId = id,
                FavoriteMovies = new List<FavoriteResponseModel.FavoriteMovieResponseModel>()
            };
            foreach (var favoriteMovie  in favoriteMovies)
            {
                favoriteResponse.FavoriteMovies.Add(new FavoriteResponseModel.FavoriteMovieResponseModel()
                {
                    Id = favoriteMovie.Movie.Id,
                    Title = favoriteMovie.Movie.Title,
                    Budget = favoriteMovie.Movie.Budget,
                    PosterUrl = favoriteMovie.Movie.PosterUrl,
                    Rating = favoriteMovie.Movie.Rating,
                });
            };

            return favoriteResponse;
        }

        public async Task PurchaseMovie(PurchaseRequestModel purchaseRequest)
        {
            if (_currentUserService.UserId != purchaseRequest.UserId)
                throw new HttpException(HttpStatusCode.Unauthorized, "You are not Authorized to purchase");
            if (_currentUserService.UserId != null) purchaseRequest.UserId = _currentUserService.UserId.Value;
            // See if Movie is already purchased.
            if (await IsMoviePurchased(purchaseRequest))
                throw new ConflictException("Movie already Purchased");
            // Get Movie Price from Movie Table
            var movie = await _movieService.GetMovieAsync(purchaseRequest.MovieId);
            purchaseRequest.TotalPrice = movie.Price;

            var purchase = new Purchase()
            {
                UserId = purchaseRequest.UserId,
                PurchaseNumber = purchaseRequest.PurchaseNumber,
                TotalPrice = purchaseRequest.TotalPrice,
                PurchaseDateTime = purchaseRequest.PurchaseDateTime,
                MovieId = purchaseRequest.MovieId,
            };
            await _purchaseRepository.AddAsync(purchase);
        }

        public async Task<bool> IsMoviePurchased(PurchaseRequestModel purchaseRequest)
        {
            return await _purchaseRepository.GetExistsAsync(p =>
                p.UserId == purchaseRequest.UserId && p.MovieId == purchaseRequest.MovieId);
        }

        public async Task<UserPurchaseResponseModel> GetAllPurchasesForUser(int id)
        {
            if (_currentUserService.UserId != id)
                throw new HttpException(HttpStatusCode.Unauthorized, "You are not Authorized to View Purchases");

            var purchasedMovies = await _purchaseRepository.ListAllWithIncludesAsync(
                p => p.UserId == _currentUserService.UserId,
                p => p.Movie);
            var purchaseResponse = new UserPurchaseResponseModel()
            {
                UserId = id,
                PurchasedMovies = new List<UserPurchasedMovieResponseModel>(),
            };
            foreach (var purchasedMovie in purchasedMovies)
            {
                purchaseResponse.PurchasedMovies.Add(new UserPurchasedMovieResponseModel()
                {
                    Id = purchasedMovie.Movie.Id,
                    Title = purchasedMovie.Movie.Title,
                    Budget = purchasedMovie.Movie.Budget,
                    PosterUrl = purchasedMovie.Movie.PosterUrl,
                    Rating = purchasedMovie.Movie.Rating,
                });
            }
            return purchaseResponse;
        }

        public async Task AddMovieReview(ReviewRequestModel reviewRequest)
        {
            if (_currentUserService.UserId != reviewRequest.UserId)
                throw new HttpException(HttpStatusCode.Unauthorized, "You are not Authorized to Review");
            var review = new Review() 
            { 
                MovieId = reviewRequest.MovieId,
                UserId = reviewRequest.UserId,
                Rating = reviewRequest.Rating,
                ReviewText = reviewRequest.ReviewText,
            };

            await _reviewRepository.AddAsync(review);
        }

        public async Task UpdateMovieReview(ReviewRequestModel reviewRequest)
        {
            if (_currentUserService.UserId != reviewRequest.UserId)
                throw new HttpException(HttpStatusCode.Unauthorized, "You are not Authorized to Review");
            var review = new Review()
            {
                MovieId = reviewRequest.MovieId,
                UserId = reviewRequest.UserId,
                Rating = reviewRequest.Rating,
                ReviewText = reviewRequest.ReviewText,
            };

            await _reviewRepository.UpdateAsync(review);
        }

        public async Task DeleteMovieReview(int userId, int movieId)
        {
            if (_currentUserService.UserId != userId)
                throw new HttpException(HttpStatusCode.Unauthorized, "You are not Authorized to Delete Review");
            var review = await _reviewRepository.ListAsync(r => r.UserId == userId && r.MovieId == movieId);
            await _reviewRepository.DeleteAsync(review.First());
        }

        public async Task<ReviewResponseModel> GetAllReviewsByUser(int id)
        {
            if (_currentUserService.UserId != id)
                throw new HttpException(HttpStatusCode.Unauthorized, "You are not Authorized to View Reviews");

            var userReviews = await _reviewRepository.ListAllWithIncludesAsync(r => r.UserId == id, r => r.Movie);
            var reviewResponse = new ReviewResponseModel()
            {
                UserId = id,
                MovieReviews = new List<ReviewMovieResponseModel>(),
            };
            foreach (var userReview in userReviews)
            {
                reviewResponse.MovieReviews.Add(
                    new ReviewMovieResponseModel()
                    {
                        MovieId = userReview.MovieId,
                        UserId = userReview.UserId,
                        Rating = userReview.Rating,
                        ReviewText = userReview.ReviewText,
                        Name = userReview.Movie.Title,
                    });
            }
            return reviewResponse;
        }

        public async Task<UserProfileResponseModel> UpdateProfile(int id, UserRequestModel userRequestModel)
        {
            //if (_currentUserService.UserId != userRequestModel.Id)
            //    throw new HttpException(HttpStatusCode.Unauthorized, "You are not Authorized to Edit");
            //var oldUser = await _userRepository.GetByIdAsync(id);
            var user = new User()
            {
                Id = id,
                FirstName = userRequestModel.FirstName,
                LastName = userRequestModel.LastName,
                PhoneNumber = userRequestModel.PhoneNumber,
                DateOfBirth = userRequestModel.DateOfBirth,
                Email = userRequestModel.Email,
                //HashedPassword = CreateHashedPassword(userRequestModel.Password, oldUser.Salt),
                //Salt = oldUser.Salt,
            };
            var profile = await _userRepository.UpdateAsync(user);

            var profileResponse = new UserProfileResponseModel()
            {
                FirstName = profile.FirstName,
                LastName = profile.LastName,
                PhoneNumber = profile.PhoneNumber,
                DateOfBirth = profile.DateOfBirth,
                Email = profile.Email,
            };
            return profileResponse;
        }
    }
}
