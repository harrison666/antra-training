using ApplicationCore.ServiceInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieShop.MVC.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MovieShop.MVC.Controllers
{
    public class UserController : Controller
    {
        private readonly IMovieService _movieService;
        private readonly IUserService _userService;

        public UserController(IUserService userService, IMovieService movieService)
        {
            _userService = userService;
            _movieService = movieService;
        }

        //[ServiceFilter(typeof(MovieShopHeaderFilter))]
        [Authorize]
        public async Task<IActionResult> Purchases()
        {
            // call the user service with userid and get list of movies that user purchased
            var id = Convert.ToInt32(HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var movies = await _userService.GetPurchases(id);
            return View(movies);
        }
    }
}
