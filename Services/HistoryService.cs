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
            foreach (CloseApproachData data in neoLookup.close_approach_data)
            {
                History history = new History
                {
                    Id = neoLookup.id,
                    Name = neoLookup.name,
                    Date = data.close_approach_date_full.ToString("dd/MM/yyyy"),
                    Time = data.close_approach_date_full.ToString("HH:mm"),
                    RelativeVelocity = Math.Round(data.relative_velocity.kilometers_per_second, 2),
                    MissDistance = Math.Round(data.miss_distance.kilometers, 2),
                    OrbitingBody = data.orbiting_body
                };
                historyList.Add(history);
            }
            return historyList;
        }
    }
}
