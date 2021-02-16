using Microsoft.Extensions.Logging;
using SpaceApp.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;

namespace SpaceApp.Services
{
    public class MarsWeatherService
    {
        private readonly ILogger _logger;
        public string LatestSol;

        public MarsWeatherService(ILogger logger)
        {
            _logger = logger;
        }
        public List<Sole> GetSolesWeather(int? sol)
        {
            List<Sole> solesWeatherList = new List<Sole>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://mars.nasa.gov/rss/api/?feed=weather&category=msl&feedtype=json");
                try
                {
                    HttpResponseMessage response = client.GetAsync("").Result;
                    response.EnsureSuccessStatusCode();

                    string content = response.Content.ReadAsStringAsync().Result;
                    var marsWeather = JsonConvert.DeserializeObject<MarsWeather>(content);

                    LatestSol = marsWeather.Soles[0].Sol;
                    solesWeatherList = GetSolesWeatherList(marsWeather, sol);
                }
                catch (HttpRequestException e)
                {
                    _logger.LogError(e.Message);
                }
                catch (JsonSerializationException e)
                {
                    _logger.LogError(e.Message);
                }
            }
            return solesWeatherList;
        }

        private List<Sole> GetSolesWeatherList(MarsWeather marsWeather, int? sol)
        {
            List<Sole> solesWeatherList = new List<Sole>();
            if (sol == null)
            {
                solesWeatherList.Add(marsWeather.Soles[0]);
            }
            else
            {
                if (Array.Exists(marsWeather.Soles, x => x.Sol == sol.ToString()))
                {
                    solesWeatherList.Add(Array.Find(marsWeather.Soles, x => x.Sol == sol.ToString()));
                }
                else
                {
                    solesWeatherList.Add(GetNaSole(sol));
                }
            }

            for (int i = 1; i < 7; i++)
            {
                solesWeatherList.Add(marsWeather.Soles[i]);
            }

            foreach (Sole sole in solesWeatherList)
            {
                try
                {
                    DateTime dateTime = DateTime.Parse(sole.Date);
                    sole.Date = dateTime.ToString("dd/MM/yyyy");
                }
                catch (FormatException e)
                {
                    _logger.LogError(e.Message);
                }
            }
            return solesWeatherList;
        }

        private Sole GetNaSole(int? sol)
        {
            Sole sole = new Sole
            {
                Sol = sol.ToString(),
                Date = "N/A",
                MinTemp = "N/A",
                MaxTemp = "N/A",
                Pressure = "N/A",
                UvIrradiance = "N/A",
                Sunrise = "N/A",
                Sunset = "N/A"
            };
            return sole;
        }
    }
}
