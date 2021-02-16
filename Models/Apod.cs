using Newtonsoft.Json;

namespace SpaceApp.Models
{
    public class Apod
    {
        [JsonProperty("date")]
        public string Date { get; set; }

        [JsonProperty("explanation")]
        public string Description { get; set; }

        [JsonProperty("media_type")]
        public string MediaType { get; set; }

        [JsonProperty("service_version")]
        public string Version { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }
    }

}