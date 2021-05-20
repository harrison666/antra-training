using ApplicationCore.Entities;
using ApplicationCore.Models.RequestModels;
using ApplicationCore.Models.ResponseModels;
using ApplicationCore.RepositoryInterface;
using ApplicationCore.ServiceInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<UserResponseModel> AddUser(UserRequestModel userRequestModel)
        {
            var user = new User()
            {
                Email = userRequestModel.Email,
                Password = userRequestModel.Password,
                Fullname = userRequestModel.Fullname,
                Mobileno = userRequestModel.Mobileno,
            };
            var createdUser = await _userRepository.AddAsync(user);
            var userResponse = new UserResponseModel()
            {
                Id = createdUser.Id,
                Email = createdUser.Email,
                Fullname = createdUser.Fullname,
                Mobileno = createdUser.Mobileno,
            };
            return userResponse;
        }

        public async System.Threading.Tasks.Task DeleteUserById(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            await _userRepository.DeleteAsync(user);
        }

        public async Task<IEnumerable<UserResponseModel>> GetAllUsers()
        {
            var users = await _userRepository.ListAllAsync();
            var userReponses = new List<UserResponseModel>();
            foreach (var user in users)
            {
                userReponses.Add(new UserResponseModel()
                {
                    Id = user.Id,
                    Email = user.Email,
                    Fullname = user.Fullname,
                    Mobileno = user.Mobileno,

                });
            }
            return userReponses;
        }

        public async Task<UserResponseModel> GetUserById(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            var tasks = new List<TaskResponseModel>();
            var taskHitories = new List<TaskHistoryResponseModel>();

            foreach (var task in user.Tasks)
            {
                tasks.Add(new TaskResponseModel()
                {
                    Id = task.Id,
                    userid = task.userid,
                    Title = task.Title,
                    Description = task.Description,
                    DueDate = task.DueDate,
                    Priority = task.Priority,
                    Remarks = task.Remarks,
                });
            }

            foreach (var task in user.TaskHistories)
            {
                taskHitories.Add(new TaskHistoryResponseModel()
                {
                    TaskId = task.TaskId,
                    UserId = task.UserId,
                    Title = task.Title,
                    Description = task.Description,
                    DueDate = task.DueDate,
                    Completed = task.Completed,
                    Remarks = task.Remarks,
                });
            }
            var userResponse = new UserResponseModel()
            {
                Id = user.Id,
                Email = user.Email,
                Fullname = user.Fullname,
                Mobileno = user.Mobileno,
                Tasks = tasks,
                TaskHistories = taskHitories,
            };
            return userResponse;
        }

        public async Task<UserResponseModel> UpdateUserById(int id, UserRequestModel userRequestModel)
        {
            var user = new User()
            {
                Id = id,
                Email = userRequestModel.Email,
                Password = userRequestModel.Password,
                Fullname = userRequestModel.Fullname,
                Mobileno = userRequestModel.Mobileno,
            };
            var createdUser = await _userRepository.UpdateAsync(user);
            var userResponse = new UserResponseModel()
            {
                Id = createdUser.Id,
                Email = createdUser.Email,
                Fullname = createdUser.Fullname,
                Mobileno = createdUser.Mobileno,
            };
            return userResponse;
        }
    }
}
