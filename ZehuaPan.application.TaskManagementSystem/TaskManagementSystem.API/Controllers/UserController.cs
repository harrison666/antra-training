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
    public class UserController : ControllerBase
    {

        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        [Route("", Name = "GetAllUsers")]

        public async Task<ActionResult> GetAllUsers()
        {
            var users = await _userService.GetAllUsers();
            return Ok(users);
        }

        [HttpGet]
        [Route("{id:int}", Name = "GetUser")]

        public async Task<ActionResult> GetUserById(int id)
        {
            var user = await _userService.GetUserById(id);
            return Ok(user);
        }

        [HttpPost]
        [Route("add", Name = "AddUser")]
        public async Task<ActionResult> AddUser(UserRequestModel userRequestModel)
        {
            var user = await _userService.AddUser(userRequestModel);
            return Created("GetUser", user);
        }

        [HttpDelete]
        [Route("delete/{id:int}", Name = "DeleteUser")]
        public async Task DeleteUserById(int id)
        {
            await _userService.DeleteUserById(id);
        }

        [HttpPut]
        [Route("update/{id:int}", Name = "UpdateUser")]
        public async Task<ActionResult> UpdateUserById(int id, UserRequestModel userRequestModel)
        {
            var user = await _userService.UpdateUserById(id, userRequestModel);
            return Created("GetUser", user);
        }
    }
}
