using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NASAapp.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
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
                catch(Exception e)
                {
                    Debug.WriteLine(e);
                }
               
            }
            return View(apod);
        }

        public IActionResult Asteroids()
        {
            Asteroid asteroid = new Asteroid();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://api.nasa.gov/neo/rest/v1/feed");
                try
                {
                    HttpResponseMessage response = client.GetAsync("?start_date=2021-01-15&end_date=2021-01-15&api_key=DEMO_KEY").Result;
                    response.EnsureSuccessStatusCode();

                    string content = response.Content.ReadAsStringAsync().Result;
                    content = content.Replace(DateTime.Now.ToString("yyyy-MM-dd"), "date");

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
                   
                    asteroid.Id = asteroidsList[0].id;
                    asteroid.Name = asteroidsList[0].name;
                    asteroid.EstimatedDiameterMin = asteroidsList[0].estimated_diameter.meters.estimated_diameter_min;
                    asteroid.EstimatedDiameterMax = asteroidsList[0].estimated_diameter.meters.estimated_diameter_max;
                    asteroid.Hazardous = asteroidsList[0].is_potentially_hazardous_asteroid;
                    asteroid.ApproachDate = approachList[0].close_approach_date_full;
                    asteroid.RelativeVelocity = approachList[0].relative_velocity.kilometers_per_second;
                    asteroid.MissDistance = approachList[0].miss_distance.kilometers;
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e);
                }

            }
            return View(asteroid);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
