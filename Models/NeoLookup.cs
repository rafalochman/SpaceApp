using System;

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
        public EstimatedDiameter estimated_diameter { get; set; }
        public bool is_potentially_hazardous_asteroid { get; set; }
        public CloseApproachData[] close_approach_data { get; set; }
        public OrbitalData orbital_data { get; set; }
        public bool is_sentry_object { get; set; }
    }

    public class LookupLinks
    {
        public string self { get; set; }
    }

    public class OrbitalData
    {
        public int orbit_id { get; set; }
        public DateTime orbit_determination_date { get; set; }
        public DateTime first_observation_date { get; set; }
        public DateTime last_observation_date { get; set; }
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
        public OrbitClass orbit_class { get; set; }
    }

    public class OrbitClass
    {
        public string orbit_class_type { get; set; }
        public string orbit_class_description { get; set; }
        public string orbit_class_range { get; set; }
    }

}
