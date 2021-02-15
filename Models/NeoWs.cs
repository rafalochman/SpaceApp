using Newtonsoft.Json;
using System;

namespace NASAapp.Models
{
    public class NeoWs
    {
        [JsonProperty("near_earth_objects")]
        public NearEarthObjects NearEarthObjects { get; set; }
    }

    public class NearEarthObjects
    {
        [JsonProperty("asteroids")]
        public Asteroids[] Asteroids { get; set; }
    }

    public class Asteroids
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("neo_reference_id")]
        public string NeoReferenceId { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("estimated_diameter")]
        public EstimatedDiameter EstimatedDiameter { get; set; }

        [JsonProperty("is_potentially_hazardous_asteroid")]
        public string PotentiallyHazardous { get; set; }

        [JsonProperty("close_approach_data")]
        public CloseApproachData[] CloseApproachData { get; set; }
    }

    public class EstimatedDiameter
    {
        [JsonProperty("meters")]
        public Meters Meters { get; set; }
    }

    public class Meters
    {
        [JsonProperty("estimated_diameter_min")]
        public decimal EstimatedDiameterMin { get; set; }

        [JsonProperty("estimated_diameter_max")]
        public decimal EstimatedDiameterMax { get; set; }
    }

    public class CloseApproachData
    {
        [JsonProperty("close_approach_date_full")]
        public DateTime CloseApproachDateFull { get; set; }

        [JsonProperty("relative_velocity")]
        public RelativeVelocity RelativeVelocity { get; set; }

        [JsonProperty("miss_distance")]
        public MissDistance MissDistance { get; set; }

        [JsonProperty("orbiting_body")]
        public string OrbitingBody { get; set; }
    }

    public class RelativeVelocity
    {
        [JsonProperty("kilometers_per_second")]
        public decimal KilometersPerSecond { get; set; }
    }

    public class MissDistance
    {
        [JsonProperty("kilometers")]
        public decimal Kilometers { get; set; }
    }

}
