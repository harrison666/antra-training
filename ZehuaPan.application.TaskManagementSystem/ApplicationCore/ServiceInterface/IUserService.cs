using ApplicationCore.Models.RequestModels;
using ApplicationCore.Models.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.ServiceInterface
{
    public interface IUserService
    {
        Task<UserResponseModel> GetUserById(int id);
        Task<IEnumerable<UserResponseModel>> GetAllUsers();
        Task<UserResponseModel> AddUser(UserRequestModel userRequestModel);
        Task DeleteUserById(int id);
        Task<UserResponseModel> UpdateUserById(int id, UserRequestModel userRequestModel);

    }
}
