using Newtonsoft.Json;

namespace NASAapp.Models
{
    public class NeoLookup
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("neo_reference_id")]
        public string NeoReferenceId { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("designation")]
        public string Designation { get; set; }

        [JsonProperty("estimated_diameter")]
        public EstimatedDiameter EstimatedDiameter { get; set; }

        [JsonProperty("is_potentially_hazardous_asteroid")]
        public bool PotentiallyHazardous { get; set; }

        [JsonProperty("close_approach_data")]
        public CloseApproachData[] CloseApproachData { get; set; }
    }

}
