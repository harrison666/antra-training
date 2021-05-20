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
    public class TaskController : ControllerBase
    {
        private readonly ITaskService _taskService;
        public TaskController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        [HttpGet]
        [Route("{id:int}", Name = "GetTask")]

        public async Task<ActionResult> GetTaskById(int id)
        {
            var task = await _taskService.GetTaskById(id);
            return Ok(task);
        }

        [HttpPost]
        [Route("add", Name = "AddTask")]
        public async Task<ActionResult> AddTask(TaskRequestModel taskRequestModel)
        {
            var task = await _taskService.AddTask(taskRequestModel);
            return Created("GetTask", task);
        }

        [HttpDelete]
        [Route("delete/{id:int}", Name = "DeleteTask")]
        public async Task DeleteTaskById(int id)
        {
            await _taskService.DeleteTaskById(id);
        }

        [HttpPut]
        [Route("update/{id:int}", Name = "UpdateTask")]
        public async Task<ActionResult> UpdateTaskById(int id, TaskRequestModel taskRequestModel)
        {
            var task = await _taskService.UpdateTaskById(id, taskRequestModel);
            return Created("GetTask", task);
        }
    }
}
