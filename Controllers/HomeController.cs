using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NASAapp.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using NASAapp.Configs;
using NASAapp.Services;
using System.Linq;

namespace NASAapp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            var homeService = new HomeService(_logger);
            Apod apod = homeService.GetApod();
            if(apod.Date == null)
            {
                return RedirectToAction("Error");
            }
            return View(apod);
        }

        public IActionResult Asteroids()
        {
            var asteroidsService = new AsteroidsService(_logger);
            List<Asteroid> asteroidList = asteroidsService.GetAsteroids();
            if (!asteroidList.Any())
            {
                return RedirectToAction("Error");
            }
            return View(asteroidList);
        }

        public IActionResult History(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Asteroids");
            }

            var historyService = new HistoryService(_logger);
            List<History> historyList = historyService.GetHistory(id);
            if (!historyList.Any())
            {
                return RedirectToAction("Error");
            }
            return View(historyList);
        }

        [HttpGet]
        public IActionResult MarsWeather(int? sol)
        {
            var marsWeatherService = new MarsWeatherService(_logger);
            List<Sole> solesWeatherList = marsWeatherService.GetSolesWeather(sol);
            if (!solesWeatherList.Any())
            {
                return RedirectToAction("Error");
            }
            ViewBag.latestSol = marsWeatherService.LatestSol;
            return View(solesWeatherList);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
