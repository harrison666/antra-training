using ApplicationCore.Models.RequestModels;
using ApplicationCore.ServiceInterface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskManagementSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskHistoryController : ControllerBase
    {
        private readonly ITaskHistoryService _taskHistoryService;

        public TaskHistoryController(ITaskHistoryService taskHistoryService)
        {
            _taskHistoryService = taskHistoryService;
        }

        [HttpGet]
        [Route("{id:int}", Name = "GetTaskHistory")]

        public async Task<ActionResult> GetTaskHistoryById(int id)
        {
            var taskHistory = await _taskHistoryService.GetTaskHistoryById(id);
            return Ok(taskHistory);
        }

        [HttpPost]
        [Route("add", Name = "AddTaskHistory")]
        public async Task<ActionResult> AddTask(TaskHistoryRequestModel taskHistoryRequestModel)
        {
            var taskHistory = await _taskHistoryService.AddTaskHistory(taskHistoryRequestModel);
            return Created("GetTaskHistory", taskHistory);
        }

        [HttpDelete]
        [Route("delete/{id:int}", Name = "DeleteTaskHistory")]
        public async Task DeleteTaskHistoryById(int id)
        {
            await _taskHistoryService.DeleteTaskHistoryById(id);
        }

        [HttpPut]
        [Route("update/{id:int}", Name = "UpdateTaskHistory")]
        public async Task<ActionResult> UpdateTaskHistoryById(int id, TaskHistoryRequestModel taskHistoryRequestModel)
        {
            var taskHistory = await _taskHistoryService.UpdateTaskHistoryById(id, taskHistoryRequestModel);
            return Created("GetTaskHistory", taskHistory);
        }
    }
}
