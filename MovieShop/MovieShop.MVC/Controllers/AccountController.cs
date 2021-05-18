using ApplicationCore.Models.Request;
using ApplicationCore.ServiceInterfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MovieShop.MVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(UserRegisterRequestModel userRegisterRequestModel)
        {
            var newUser = await _userService.RegisterUser(userRegisterRequestModel);
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Login()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Login(UserLoginRequestModel userLoginRequestModel)
        {
            var user = await _userService.ValidateUser(userLoginRequestModel.Email, userLoginRequestModel.Password);

            if (user == null)
            {
                // Invalid User Name/Password                
                return View();
            }


            // if user entered correct username/password
            // Cookie Based Authentication
            // Claims, first name, last name, date of birth, id... 
            // can be encrypted

            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Surname, user.LastName),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.GivenName, user.FirstName)
            };

            // Identity
            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            // create cookie

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity));

            return RedirectToAction("Index", "Home");
        }


        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Login");
        }

        [HttpGet]
        public async Task<IActionResult> Profile()
        {
            var id = Convert.ToInt32(HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var profile = await _userService.GetProfile(id);
            return View(profile);
        }

        [HttpGet]
        public async Task<IActionResult> EditProfile()
        {
            // call the database and get the user information and fill that in textboxes so that user can edit and save info
            //var id = Convert.ToInt32(HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            //var profile = await _userService.GetProfile(id);
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> EditProfile(UserRequestModel userRequestModel)
        {
            // call the user service and map the UserRequestModel data into User entity and call the repository
            var id = Convert.ToInt32(HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var profile = await _userService.UpdateProfile(id, userRequestModel);
            return RedirectToAction("Profile");
        }
    }
}
