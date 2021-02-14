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

        public MarsWeatherService(ILogger logger)
        {
            _logger = logger;
        }
        public MarsWeather GetMarsWeather()
        {
            MarsWeather marsWeather = new MarsWeather();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://mars.nasa.gov/rss/api/?feed=weather&category=msl&feedtype=json");
                try
                {
                    HttpResponseMessage response = client.GetAsync("").Result;
                    response.EnsureSuccessStatusCode();

                    string content = response.Content.ReadAsStringAsync().Result;
                    marsWeather = JsonConvert.DeserializeObject<MarsWeather>(content);
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
            return marsWeather;
        }
    }
}
