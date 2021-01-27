using System;

namespace NASAapp.Models
{

    public class NeoWs
    {
        public Links links { get; set; }
        public int element_count { get; set; }
        public NearEarthObjects near_earth_objects { get; set; }
    }

    public class Links
    {
        public string next { get; set; }
        public string prev { get; set; }
        public string self { get; set; }
    }

    public class NearEarthObjects
    {
        public Asteroids[] asteroids { get; set; }
    }

    public class Asteroids
    {
        public AsteroidsLinks links { get; set; }
        public int id { get; set; }
        public string neo_reference_id { get; set; }
        public string name { get; set; }
        public string nasa_jpl_url { get; set; }
        public float absolute_magnitude_h { get; set; }
        public EstimatedDiameter estimated_diameter { get; set; }
        public string is_potentially_hazardous_asteroid { get; set; }
        public CloseApproachData[] close_approach_data { get; set; }
        public bool is_sentry_object { get; set; }
    }

    public class AsteroidsLinks
    {
        public string self { get; set; }
    }

    public class EstimatedDiameter
    {
        public Kilometers kilometers { get; set; }
        public Meters meters { get; set; }
        public Miles miles { get; set; }
        public Feet feet { get; set; }
    }

    public class Kilometers
    {
        public decimal estimated_diameter_min { get; set; }
        public decimal estimated_diameter_max { get; set; }
    }

    public class Meters
    {
        public decimal estimated_diameter_min { get; set; }
        public decimal estimated_diameter_max { get; set; }
    }

    public class Miles
    {
        public decimal estimated_diameter_min { get; set; }
        public decimal estimated_diameter_max { get; set; }
    }

    public class Feet
    {
        public decimal estimated_diameter_min { get; set; }
        public decimal estimated_diameter_max { get; set; }
    }

    public class CloseApproachData
    {
        public string close_approach_date { get; set; }
        public DateTime close_approach_date_full { get; set; }
        public long epoch_date_close_approach { get; set; }
        public RelativeVelocity relative_velocity { get; set; }
        public MissDistance miss_distance { get; set; }
        public string orbiting_body { get; set; }
    }

    public class RelativeVelocity
    {
        public decimal kilometers_per_second { get; set; }
        public decimal kilometers_per_hour { get; set; }
        public decimal miles_per_hour { get; set; }
    }

    public class MissDistance
    {
        public decimal astronomical { get; set; }
        public decimal lunar { get; set; }
        public decimal kilometers { get; set; }
        public decimal miles { get; set; }
    }

}
