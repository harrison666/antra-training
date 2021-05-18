using ApplicationCore.Entities;
using ApplicationCore.Models.Request;
using ApplicationCore.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.ServiceInterfaces
{
    public interface IUserService
    {
        Task<UserRegisterResponseModel> RegisterUser(UserRegisterRequestModel registerRequest);
        Task<UserLoginResponseModel> ValidateUser(string email, string password);
        Task<List<MovieResponseModel>> GetPurchases(int id);
        Task<UserProfileResponseModel> GetProfile(int id);
        Task<User> GetUser(string email);

        Task AddFavorite(FavoriteRequestModel favoriteRequest);
        Task RemoveFavorite(FavoriteRequestModel favoriteRequest);
        Task<bool> FavoriteExists(int id, int movieId);
        Task<FavoriteResponseModel> GetAllFavoritesForUser(int id);

        Task PurchaseMovie(PurchaseRequestModel purchaseRequest);
        Task<bool> IsMoviePurchased(PurchaseRequestModel purchaseRequest);
        Task<UserPurchaseResponseModel> GetAllPurchasesForUser(int id);

        Task AddMovieReview(ReviewRequestModel reviewRequest);
        Task UpdateMovieReview(ReviewRequestModel reviewRequest);
        Task DeleteMovieReview(int userId, int movieId);
        Task<ReviewResponseModel> GetAllReviewsByUser(int id);
        Task<UserProfileResponseModel> UpdateProfile(int id, UserRequestModel userRequestModel);
    }
}
