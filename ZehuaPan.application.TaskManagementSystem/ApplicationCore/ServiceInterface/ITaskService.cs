using ApplicationCore.Models.RequestModels;
using ApplicationCore.Models.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.ServiceInterface
{
    public interface ITaskService
    {
        Task<TaskResponseModel> GetTaskById(int id);
        Task<TaskResponseModel> AddTask(TaskRequestModel taskRequestModel);
        Task DeleteTaskById(int id);
        Task<TaskResponseModel> UpdateTaskById(int id, TaskRequestModel taskRequestModel);
    }
}
