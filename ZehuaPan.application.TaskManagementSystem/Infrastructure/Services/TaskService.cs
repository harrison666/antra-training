using ApplicationCore.Models.RequestModels;
using ApplicationCore.Models.ResponseModels;
using ApplicationCore.RepositoryInterface;
using ApplicationCore.ServiceInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationCore.Entities;

namespace Infrastructure.Services
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _taskRepository;
        private readonly IUserRepository _userRepository;
        public TaskService(ITaskRepository taskRepository, IUserRepository userRepository)
        {
            _taskRepository = taskRepository;
            _userRepository = userRepository;
        }

        public async Task<TaskResponseModel> AddTask(TaskRequestModel taskRequestModel)
        {
            var task = new ApplicationCore.Entities.Task()
            {
                userid = taskRequestModel.userid,
                Title = taskRequestModel.Title,
                Description = taskRequestModel.Description,
                DueDate = taskRequestModel.DueDate,
                Priority = taskRequestModel.Priority,
                Remarks = taskRequestModel.Remarks,
            };
            var createdTask = await _taskRepository.AddAsync(task);
            var taskResponse = new TaskResponseModel()
            {
                Id = createdTask.Id,
                userid = createdTask.userid,
                Title = createdTask.Title,
                Description = createdTask.Description,
                DueDate = createdTask.DueDate,
                Priority = createdTask.Priority,
                Remarks = createdTask.Remarks,
            };
            return taskResponse;
        }

        public async System.Threading.Tasks.Task DeleteTaskById(int id)
        {
            var task = await _taskRepository.GetByIdAsync(id);
            await _taskRepository.DeleteAsync(task);
        }

        public async Task<TaskResponseModel> GetTaskById(int id)
        {
            var task = await _taskRepository.GetByIdAsync(id);
            var taskResponse = new TaskResponseModel()
            {
                Id = task.Id,
                userid = task.userid,
                Title = task.Title,
                Description = task.Description,
                DueDate = task.DueDate,
                Priority = task.Priority,
                Remarks = task.Remarks,
            };
            if (task.userid != null)
            {
                var user = await _userRepository.GetByIdAsync((int)task.userid);
                taskResponse.User = new UserResponseModel()
                {
                    Id = user.Id,
                    Email = user.Email,
                    Fullname = user.Fullname,
                    Mobileno = user.Mobileno,
                };
            }
            return taskResponse;
        }

        public async Task<TaskResponseModel> UpdateTaskById(int id, TaskRequestModel taskRequestModel)
        {
            var task = new ApplicationCore.Entities.Task()
            {
                Id = id,
                userid = taskRequestModel.userid,
                Title = taskRequestModel.Title,
                Description = taskRequestModel.Description,
                DueDate = taskRequestModel.DueDate,
                Priority = taskRequestModel.Priority,
                Remarks = taskRequestModel.Remarks,
            };
            var createdTask = await _taskRepository.UpdateAsync(task);
            var taskResponse = new TaskResponseModel()
            {
                Id = createdTask.Id,
                userid = createdTask.userid,
                Title = createdTask.Title,
                Description = createdTask.Description,
                DueDate = createdTask.DueDate,
                Priority = createdTask.Priority,
                Remarks = createdTask.Remarks,
            };
            return taskResponse;
        }
    }
}
