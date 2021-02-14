using Microsoft.Extensions.Logging;
using NASAapp.Configs;
using NASAapp.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;

namespace NASAapp.Services
{
    public class AsteroidsService
    {
        private readonly ILogger _logger;

        public AsteroidsService(ILogger logger)
        {
            _logger = logger;
        }
        public List<Asteroid> GetAsteroids()
        {
            List<Asteroid> asteroidList = new List<Asteroid>();
            string currentDate = DateTime.Now.ToString("yyyy-MM-dd");
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://api.nasa.gov/neo/rest/v1/feed");
                try
                {
                    HttpResponseMessage response = client.GetAsync("?start_date=" + currentDate + "&end_date=" + currentDate + "&api_key=" + Config.NASA_KEY).Result;
                    response.EnsureSuccessStatusCode();

                    string content = response.Content.ReadAsStringAsync().Result;
                    content = content.Replace(currentDate, "asteroids");
                    NeoWs neoWs = JsonConvert.DeserializeObject<NeoWs>(content);

                    asteroidList = CreateAsteroidsList(neoWs);
                }
                catch (HttpRequestException e)
                {
                    _logger.LogError(e.Message);
                }
            }
            return asteroidList;
        }

        private List<Asteroid> CreateAsteroidsList(NeoWs neoWs)
        {
            List<Asteroid> asteroidList = new List<Asteroid>();
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

            return asteroidList;
        }
    }
}
