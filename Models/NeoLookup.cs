using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NASAapp.Models
{
    public class NeoLookup
    {
        public LookupLinks links { get; set; }
        public int id { get; set; }
        public string neo_reference_id { get; set; }
        public string name { get; set; }
        public string designation { get; set; }
        public string nasa_jpl_url { get; set; }
        public float absolute_magnitude_h { get; set; }
        public Lookup_Estimated_Diameter estimated_diameter { get; set; }
        public bool is_potentially_hazardous_asteroid { get; set; }
        public Lookup_Close_Approach_Data[] close_approach_data { get; set; }
        public Orbital_Data orbital_data { get; set; }
        public bool is_sentry_object { get; set; }
    }

    public class LookupLinks
    {
        public string self { get; set; }
    }

    public class Lookup_Estimated_Diameter
    {
        public Lookup_Kilometers kilometers { get; set; }
        public Lookup_Meters meters { get; set; }
        public Lookup_Miles miles { get; set; }
        public Lookup_Feet feet { get; set; }
    }

    public class Lookup_Kilometers
    {
        public float estimated_diameter_min { get; set; }
        public float estimated_diameter_max { get; set; }
    }

    public class Lookup_Meters
    {
        public float estimated_diameter_min { get; set; }
        public float estimated_diameter_max { get; set; }
    }

    public class Lookup_Miles
    {
        public float estimated_diameter_min { get; set; }
        public float estimated_diameter_max { get; set; }
    }

    public class Lookup_Feet
    {
        public float estimated_diameter_min { get; set; }
        public float estimated_diameter_max { get; set; }
    }

    public class Orbital_Data
    {
        public string orbit_id { get; set; }
        public string orbit_determination_date { get; set; }
        public string first_observation_date { get; set; }
        public string last_observation_date { get; set; }
        public int data_arc_in_days { get; set; }
        public int observations_used { get; set; }
        public string orbit_uncertainty { get; set; }
        public string minimum_orbit_intersection { get; set; }
        public string jupiter_tisserand_invariant { get; set; }
        public string epoch_osculation { get; set; }
        public string eccentricity { get; set; }
        public string semi_major_axis { get; set; }
        public string inclination { get; set; }
        public string ascending_node_longitude { get; set; }
        public string orbital_period { get; set; }
        public string perihelion_distance { get; set; }
        public string perihelion_argument { get; set; }
        public string aphelion_distance { get; set; }
        public string perihelion_time { get; set; }
        public string mean_anomaly { get; set; }
        public string mean_motion { get; set; }
        public string equinox { get; set; }
        public Orbit_Class orbit_class { get; set; }
    }

    public class Orbit_Class
    {
        public string orbit_class_type { get; set; }
        public string orbit_class_description { get; set; }
        public string orbit_class_range { get; set; }
    }

    public class Lookup_Close_Approach_Data
    {
        public string close_approach_date { get; set; }
        public DateTime close_approach_date_full { get; set; }
        public long epoch_date_close_approach { get; set; }
        public Lookup_Relative_Velocity relative_velocity { get; set; }
        public Lookup_Miss_Distance miss_distance { get; set; }
        public string orbiting_body { get; set; }
    }

    public class Lookup_Relative_Velocity
    {
        public decimal kilometers_per_second { get; set; }
        public decimal kilometers_per_hour { get; set; }
        public decimal miles_per_hour { get; set; }
    }

    public class Lookup_Miss_Distance
    {
        public decimal astronomical { get; set; }
        public decimal lunar { get; set; }
        public decimal kilometers { get; set; }
        public decimal miles { get; set; }
    }

}
