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
            if(apod.date == null)
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
            MarsWeather marsWeather = new MarsWeather();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://api.maas2.apollorion.com/" + sol);
                try
                {
                    HttpResponseMessage response = client.GetAsync("").Result;
                    response.EnsureSuccessStatusCode();

                    string content = response.Content.ReadAsStringAsync().Result;
                    marsWeather = JsonConvert.DeserializeObject<MarsWeather>(content);

                    DateTime dateTime = DateTime.Parse(marsWeather.terrestrial_date);
                    marsWeather.terrestrial_date = dateTime.ToString("dd/MM/yyyy");
                }
                catch (HttpRequestException e)
                {
                    _logger.LogError(e.Message);
                    return RedirectToAction("MarsWeather");
                }
                catch (JsonSerializationException e)
                {
                    _logger.LogError(e.Message);
                    return RedirectToAction("MarsWeather");
                }
            }
            return View(marsWeather);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
