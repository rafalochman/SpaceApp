using Microsoft.Extensions.Logging;
using NASAapp.Configs;
using NASAapp.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;

namespace NASAapp.Services
{
    public class HistoryService
    {
        private readonly ILogger _logger;

        public HistoryService(ILogger logger)
        {
            _logger = logger;
        }
        public List<History> GetHistory(int? id)
        {
            List<History> historyList = new List<History>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://www.neowsapp.com/rest/v1/neo/");
                try
                {
                    HttpResponseMessage response = client.GetAsync(id + "?api_key=" + Config.NASA_KEY).Result;
                    response.EnsureSuccessStatusCode();

                    string content = response.Content.ReadAsStringAsync().Result;
                    NeoLookup neoLookup = JsonConvert.DeserializeObject<NeoLookup>(content);

                    historyList = CreateHistoryList(neoLookup);
                }
                catch (HttpRequestException e)
                {
                    _logger.LogError(e.Message);
                }
            }
            return historyList;
        }

        private List<History> CreateHistoryList(NeoLookup neoLookup)
        {
            List<History> historyList = new List<History>();
            foreach (CloseApproachData data in neoLookup.CloseApproachData)
            {
                History history = new History
                {
                    Id = neoLookup.Id,
                    Name = neoLookup.Name,
                    Date = data.CloseApproachDateFull.ToString("dd/MM/yyyy"),
                    Time = data.CloseApproachDateFull.ToString("HH:mm"),
                    RelativeVelocity = Math.Round(data.RelativeVelocity.KilometersPerSecond, 2),
                    MissDistance = Math.Round(data.MissDistance.Kilometers, 2),
                    OrbitingBody = data.OrbitingBody
                };
                historyList.Add(history);
            }
            return historyList;
        }
    }
}
