using ApplicationCore.Models.Request;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieShop.MVC.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateMovie(MovieCreateRequestModel movieCreateRequestModel)
        {
            // take the information from View and Save it to Database
            return View();
        }

        [HttpGet]
        public IActionResult CreateMovie()
        {
            return View();
        }
    }
}
