using Microsoft.Extensions.Logging;
using NASAapp.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace NASAapp.Services
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

                    LatestSol = marsWeather.soles[0].sol;
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
            if(sol == null)
            {
                solesWeatherList.Add(marsWeather.soles[0]);
            }
            else
            {
                if(Array.Exists(marsWeather.soles, x => x.sol == sol.ToString()))
                {
                    solesWeatherList.Add(Array.Find(marsWeather.soles, x => x.sol == sol.ToString()));
                }
                else
                {
                    solesWeatherList.Add(GetNaSole(sol));
                }
            }
            for(int i = 1; i < 6; i++)
            {
                solesWeatherList.Add(marsWeather.soles[i]);
            }
            return solesWeatherList;
        }

        private Sole GetNaSole(int? sol)
        {
            Sole sole = new Sole
            {
                sol = sol.ToString(),
                terrestrial_date = "N/A",
                min_temp = "N/A",
                max_temp = "N/A",
                pressure = "N/A",
                local_uv_irradiance_index = "N/A",
                sunrise = "N/A",
                sunset = "N/A"
            };

            return sole;
        }
    }
}
