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
                    dynamic resultat = JsonConvert.DeserializeObject(content);

                    apod.Title = resultat.title;
                    apod.Explanation = resultat.explanation;
                    apod.MediaType = resultat.media_type;
                    apod.Url = resultat.url;
                    DateTime dateTime = resultat.date;
                    apod.Date = dateTime.ToString("dd/MM/yyyy");
                }
                catch (Exception e)
                {
                    _logger.LogError("Error: " + e);
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
                    content = content.Replace(currentDate, "date");

                    dynamic resultat = JsonConvert.DeserializeObject(content);
                    var asteroids = resultat.near_earth_objects.date;
                    List<dynamic> asteroidsList = new List<dynamic>();
                    List<dynamic> approachList = new List<dynamic>();
                    foreach (JObject ast in asteroids)
                    {
                        dynamic temp = JsonConvert.DeserializeObject(ast.ToString());
                        asteroidsList.Add(temp);
                        foreach (JObject dat in temp.close_approach_data)
                        {
                            approachList.Add(JsonConvert.DeserializeObject(dat.ToString()));
                        }
                    }
                    int size = resultat.element_count;
                    for (int i = 0; i < size; i++)
                    {
                        Asteroid asteroid = new Asteroid();
                        asteroid.Id = asteroidsList[i].id;
                        asteroid.Name = asteroidsList[i].name;
                        asteroid.EstimatedDiameterMin = asteroidsList[i].estimated_diameter.meters.estimated_diameter_min;
                        Math.Round(asteroid.EstimatedDiameterMin, 2);
                        asteroid.EstimatedDiameterMax = asteroidsList[i].estimated_diameter.meters.estimated_diameter_max;
                        Math.Round(asteroid.EstimatedDiameterMax, 2);
                        asteroid.Hazardous = asteroidsList[i].is_potentially_hazardous_asteroid;
                        DateTime dateTime = approachList[i].close_approach_date_full;
                        asteroid.Time = dateTime.ToString("HH:mm");
                        asteroid.Date = dateTime.ToString("dd/MM/yyyy");
                        asteroid.RelativeVelocity = approachList[i].relative_velocity.kilometers_per_second;
                        Math.Round(asteroid.RelativeVelocity, 2);
                        asteroid.MissDistance = approachList[i].miss_distance.kilometers;
                        Math.Round(asteroid.MissDistance, 2);
                        asteroidList.Add(asteroid);
                    }
                    asteroidList.Sort((x, y) => x.Time.CompareTo(y.Time));
                }
                catch (Exception e)
                {
                    _logger.LogError("Error: " + e);
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
                catch (Exception e)
                {
                    _logger.LogError("Error: " + e);
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
