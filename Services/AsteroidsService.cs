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
            foreach (Asteroids ast in neoWs.NearEarthObjects.Asteroids)
            {
                Asteroid asteroid = new Asteroid
                {
                    Id = ast.Id,
                    Name = ast.Name,
                    EstimatedDiameterMin = Math.Round(ast.EstimatedDiameter.Meters.EstimatedDiameterMin, 2),
                    EstimatedDiameterMax = Math.Round(ast.EstimatedDiameter.Meters.EstimatedDiameterMax, 2),
                    Hazardous = ast.PotentiallyHazardous,
                    Time = ast.CloseApproachData[0].CloseApproachDateFull.ToString("HH:mm"),
                    Date = ast.CloseApproachData[0].CloseApproachDateFull.ToString("dd/MM/yyyy"),
                    RelativeVelocity = Math.Round(ast.CloseApproachData[0].RelativeVelocity.KilometersPerSecond, 2),
                    MissDistance = Math.Round(ast.CloseApproachData[0].MissDistance.Kilometers, 2)
                };
                asteroidList.Add(asteroid);
            }
            asteroidList.Sort((x, y) => x.Time.CompareTo(y.Time));

            return asteroidList;
        }
    }
}
