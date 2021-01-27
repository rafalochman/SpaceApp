using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NASAapp.Models
{

    public class NeoWs
    {
        public Links links { get; set; }
        public int element_count { get; set; }
        public Near_Earth_Objects near_earth_objects { get; set; }
    }

    public class Links
    {
        public string next { get; set; }
        public string prev { get; set; }
        public string self { get; set; }
    }

    public class Near_Earth_Objects
    {
        public Asteroids[] asteroids { get; set; }
    }

    public class Asteroids
    {
        public Links1 links { get; set; }
        public int id { get; set; }
        public string neo_reference_id { get; set; }
        public string name { get; set; }
        public string nasa_jpl_url { get; set; }
        public float absolute_magnitude_h { get; set; }
        public Estimated_Diameter estimated_diameter { get; set; }
        public string is_potentially_hazardous_asteroid { get; set; }
        public Close_Approach_Data[] close_approach_data { get; set; }
        public bool is_sentry_object { get; set; }
    }

    public class Links1
    {
        public string self { get; set; }
    }

    public class Estimated_Diameter
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

    public class Close_Approach_Data
    {
        public string close_approach_date { get; set; }
        public DateTime close_approach_date_full { get; set; }
        public long epoch_date_close_approach { get; set; }
        public Relative_Velocity relative_velocity { get; set; }
        public Miss_Distance miss_distance { get; set; }
        public string orbiting_body { get; set; }
    }

    public class Relative_Velocity
    {
        public decimal kilometers_per_second { get; set; }
        public decimal kilometers_per_hour { get; set; }
        public decimal miles_per_hour { get; set; }
    }

    public class Miss_Distance
    {
        public decimal astronomical { get; set; }
        public decimal lunar { get; set; }
        public decimal kilometers { get; set; }
        public decimal miles { get; set; }
    }

}
