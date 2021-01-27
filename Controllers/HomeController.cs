using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NASAapp.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;

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
            Apod apod = new Apod();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://api.nasa.gov/planetary/");
                try
                {
                    HttpResponseMessage response = client.GetAsync("apod?api_key=DEMO_KEY").Result;
                    response.EnsureSuccessStatusCode();

                    string content = response.Content.ReadAsStringAsync().Result;
                    apod = JsonConvert.DeserializeObject<Apod>(content);

                    DateTime dateTime = DateTime.Parse(apod.date);
                    apod.date = dateTime.ToString("dd/MM/yyyy");
                }
                catch (HttpRequestException e)
                {
                    _logger.LogError(e.Message);
                    return RedirectToAction("Error");
                }

            }
            return View(apod);
        }

        public IActionResult Asteroids()
        {
            List<Asteroid> asteroidList = new List<Asteroid>();
            string currentDate = DateTime.Now.ToString("yyyy-MM-dd");
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://api.nasa.gov/neo/rest/v1/feed");
                try
                {
                    HttpResponseMessage response = client.GetAsync("?start_date=" + currentDate + "&end_date=" + currentDate + "&api_key=DEMO_KEY").Result;
                    response.EnsureSuccessStatusCode();

                    string content = response.Content.ReadAsStringAsync().Result;
                    content = content.Replace(currentDate, "asteroids");
                    NeoWs neoWs = JsonConvert.DeserializeObject<NeoWs>(content);

                    foreach (Asteroids ast in neoWs.near_earth_objects.asteroids)
                    {
                        Asteroid asteroid = new Asteroid
                        {
                            Id = ast.id,
                            Name = ast.name,
                            EstimatedDiameterMin = Math.Round(ast.estimated_diameter.meters.estimated_diameter_min, 2),
                            EstimatedDiameterMax = Math.Round(ast.estimated_diameter.meters.estimated_diameter_max, 2),
                            Hazardous = ast.is_potentially_hazardous_asteroid,
                            Time = ast.close_approach_data[0].close_approach_date_full.ToString("HH:mm"),
                            Date = ast.close_approach_data[0].close_approach_date_full.ToString("dd/MM/yyyy"),
                            RelativeVelocity = Math.Round(ast.close_approach_data[0].relative_velocity.kilometers_per_second, 2),
                            MissDistance = Math.Round(ast.close_approach_data[0].miss_distance.kilometers, 2)
                        };
                        asteroidList.Add(asteroid);
                    }
                    asteroidList.Sort((x, y) => x.Time.CompareTo(y.Time));
                }
                catch (HttpRequestException e)
                {
                    _logger.LogError(e.Message);
                    return RedirectToAction("Error");
                }
            }
            return View(asteroidList);
        }

        public IActionResult History(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Asteroids");
            }
            List<History> historyList = new List<History>();
            List<dynamic> historicDataList = new List<dynamic>();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://www.neowsapp.com/rest/v1/neo/");
                try
                {
                    HttpResponseMessage response = client.GetAsync(id + "?api_key=DEMO_KEY").Result;
                    response.EnsureSuccessStatusCode();

                    string content = response.Content.ReadAsStringAsync().Result;
                    dynamic resultat = JsonConvert.DeserializeObject(content);

                    var approachData = resultat.close_approach_data;
                    foreach (JObject ast in approachData)
                    {
                        dynamic temp = JsonConvert.DeserializeObject(ast.ToString());
                        historicDataList.Add(temp);
                    }
                    int size = historicDataList.Count;
                    for (int i = 0; i < size; i++)
                    {
                        History history = new History();
                        history.Id = resultat.id;
                        history.Name = resultat.name;
                        DateTime dateTime = historicDataList[i].close_approach_date_full;
                        history.Date = dateTime.ToString("dd/MM/yyyy");
                        history.Time = dateTime.ToString("HH:mm");
                        history.RelativeVelocity = historicDataList[i].relative_velocity.kilometers_per_second;
                        Math.Round(history.RelativeVelocity, 2);
                        history.MissDistance = historicDataList[i].miss_distance.kilometers;
                        Math.Round(history.MissDistance, 2);
                        history.OrbitingBody = historicDataList[i].orbiting_body;
                        historyList.Add(history);
                    }

                }
                catch (HttpRequestException e)
                {
                    _logger.LogError(e.Message);
                    return RedirectToAction("Error");
                }

            }
            return View(historyList);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
