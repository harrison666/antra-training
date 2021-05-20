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
    public class TaskHistoryService : ITaskHistoryService
    {
        private readonly ITaskHistoryRepository _taskHistoryRepository;
        private readonly IUserRepository _userRepository;
        public TaskHistoryService(ITaskHistoryRepository taskHistoryRepository, IUserRepository userRepository)
        {
            _taskHistoryRepository = taskHistoryRepository;
            _userRepository = userRepository;
        }

        public async Task<TaskHistoryResponseModel> AddTaskHistory(TaskHistoryRequestModel taskHistoryRequestModel)
        {
            var taskHistory = new ApplicationCore.Entities.TaskHistory()
            {
                UserId = taskHistoryRequestModel.UserId,
                Title = taskHistoryRequestModel.Title,
                Description = taskHistoryRequestModel.Description,
                DueDate = taskHistoryRequestModel.DueDate,
                Completed = taskHistoryRequestModel.Completed,
                Remarks = taskHistoryRequestModel.Remarks,
            };
            var createdTaskHistory = await _taskHistoryRepository.AddAsync(taskHistory);
            var taskHistoryResponse = new TaskHistoryResponseModel()
            {
                TaskId = createdTaskHistory.TaskId,
                UserId = createdTaskHistory.UserId,
                Title = createdTaskHistory.Title,
                Description = createdTaskHistory.Description,
                DueDate = createdTaskHistory.DueDate,
                Completed = createdTaskHistory.Completed,
                Remarks = createdTaskHistory.Remarks,
            };
            return taskHistoryResponse;
        }

        public async System.Threading.Tasks.Task DeleteTaskHistoryById(int id)
        {
            var taskHistory = await _taskHistoryRepository.GetByIdAsync(id);
            await _taskHistoryRepository.DeleteAsync(taskHistory);
        }

        public async Task<TaskHistoryResponseModel> GetTaskHistoryById(int id)
        {
            var taskHistory = await _taskHistoryRepository.GetByIdAsync(id);
            var taskHistoryResponse = new TaskHistoryResponseModel()
            {
                TaskId = taskHistory.TaskId,
                UserId = taskHistory.UserId,
                Title = taskHistory.Title,
                Description = taskHistory.Description,
                DueDate = taskHistory.DueDate,
                Completed = taskHistory.Completed,
                Remarks = taskHistory.Remarks,
            };
            if (taskHistory.UserId != null)
            {
                var user = await _userRepository.GetByIdAsync((int)taskHistory.UserId);
                taskHistoryResponse.User = new UserResponseModel()
                {
                    Id = user.Id,
                    Email = user.Email,
                    Fullname = user.Fullname,
                    Mobileno = user.Mobileno,
                };
            }
            return taskHistoryResponse;
        }

        public async Task<TaskHistoryResponseModel> UpdateTaskHistoryById(int id, TaskHistoryRequestModel taskHistoryRequestModel)
        {
            var taskHistory = new TaskHistory()
            {
                TaskId = id,
                UserId = taskHistoryRequestModel.UserId,
                Title = taskHistoryRequestModel.Title,
                Description = taskHistoryRequestModel.Description,
                DueDate = taskHistoryRequestModel.DueDate,
                Completed = taskHistoryRequestModel.Completed,
                Remarks = taskHistoryRequestModel.Remarks,
            };
            var createdTaskHistory = await _taskHistoryRepository.UpdateAsync(taskHistory);
            var taskHistoryResponse = new TaskHistoryResponseModel()
            {
                TaskId = createdTaskHistory.TaskId,
                UserId = createdTaskHistory.UserId,
                Title = createdTaskHistory.Title,
                Description = createdTaskHistory.Description,
                DueDate = createdTaskHistory.DueDate,
                Completed = createdTaskHistory.Completed,
                Remarks = createdTaskHistory.Remarks,
            };
            return taskHistoryResponse;
        }
    }
}
