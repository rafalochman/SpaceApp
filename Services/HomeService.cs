using Microsoft.Extensions.Logging;
using SpaceApp.Configs;
using SpaceApp.Models;
using Newtonsoft.Json;
using System;
using System.Net.Http;

namespace SpaceApp.Services
{
    public class HomeService
    {
        private readonly ILogger _logger;

        public HomeService(ILogger logger)
        {
            _logger = logger;
        }
        public Apod GetApod()
        {
            Apod apod = new Apod();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://api.nasa.gov/planetary/");
                try
                {
                    HttpResponseMessage response = client.GetAsync("apod?api_key=" + Config.NASA_KEY).Result;
                    response.EnsureSuccessStatusCode();

                    string content = response.Content.ReadAsStringAsync().Result;
                    apod = JsonConvert.DeserializeObject<Apod>(content);

                    DateTime dateTime = DateTime.Parse(apod.Date);
                    apod.Date = dateTime.ToString("dd/MM/yyyy");
                }
                catch (HttpRequestException e)
                {
                    _logger.LogError(e.Message);
                }
            }
            return apod;
        }
    }
}
