using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MovieShop.MVC.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using ApplicationCore.Models.Response;

using Infrastructure.Services;
using ApplicationCore.ServiceInterfaces;
using Infastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace MovieShop.MVC.Controllers
{
    public class HomeController : Controller
    {
        //private readonly ILogger<HomeController> _logger;
        //public HomeController(ILogger<HomeController> logger)
        //{
        //    _logger = logger;
        //}
        private readonly IMovieService _movieService;
        private readonly MovieShopDbContext _dbContext;
        public HomeController(IMovieService movieService)
        {
            _movieService = movieService;
        }

        public async Task<IActionResult> Index()
        {
            // Select top 30 * from Movies order by revenue
            // LINQ
            // var movies = dbContext.Movies.OrderByDescedning(m=> m.Revenue).Take(30);
            // foreach(var m in movies) {}

            //var movies = _dbContext.Movies.OrderByDescending(m => m.Revenue).ToList();

            //var topMovies = new List<MovieResponseModel>();

            //foreach (var movie in movies)
            //{
            //    var topMovie = new MovieResponseModel();
            //    topMovie.Id = movie.Id;
            //    topMovie.Title = movie.Title;
            //    topMovie.Budget = movie.Budget;

            //    topMovies.Add(topMovie);
            //    //topMovies.Add( new MovieResponseModel
            //    //{
            //    //    Id = movie.Id, 
            //    //    Budget = movie.Budget,
            //    //    Title = movie.Title
            //    //});
            //}

            ////var movies = _movieService.GetTop30RevenueMovie();
            var topMovies = await _movieService.GetTop30RevenueMovies();
            return View(topMovies);

        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult TopRatedMovies()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
