using ApplicationCore.Models.RequestModels;
using ApplicationCore.Models.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.ServiceInterface
{
    public interface ITaskHistoryService
    {
        Task<TaskHistoryResponseModel> GetTaskHistoryById(int id);
        Task<TaskHistoryResponseModel> AddTaskHistory(TaskHistoryRequestModel taskHistoryRequestModel);
        Task DeleteTaskHistoryById(int id);
        Task<TaskHistoryResponseModel> UpdateTaskHistoryById(int id, TaskHistoryRequestModel taskHistoryRequestModel);
    }
}
