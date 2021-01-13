using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NASAapp.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

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
            Apod apod;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://api.nasa.gov/planetary/");
                HttpResponseMessage response = client.GetAsync("apod?api_key=DEMO_KEY").Result; ;
                response.EnsureSuccessStatusCode();

                string content = response.Content.ReadAsStringAsync().Result;
                dynamic resultat = JsonConvert.DeserializeObject(content);

                apod = new Apod();
                apod.Title = resultat.title;
                apod.Explanation = resultat.explanation;
                apod.MediaType = resultat.media_type;
                apod.Url = resultat.url;
                apod.Date = resultat.date;
            }
            return View(apod);
        }

        public IActionResult Privacy()
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
