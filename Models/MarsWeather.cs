using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NASAapp.Models
{

    public class MarsWeather
    {
        public Sole[] Soles { get; set; }
    }

    public class Sole
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("terrestrial_date")]
        public string Date { get; set; }

        [JsonProperty("sol")]
        public string Sol { get; set; }

        [JsonProperty("ls")]
        public string Ls { get; set; }

        [JsonProperty("season")]
        public string Season { get; set; }

        [JsonProperty("min_temp")]
        public string MinTemp { get; set; }

        [JsonProperty("max_temp")]
        public string MaxTemp { get; set; }

        [JsonProperty("pressure")]
        public string Pressure { get; set; }

        [JsonProperty("pressure_string")]
        public string PressureDescription { get; set; }

        [JsonProperty("abs_humidity")]
        public string AbsHumidity { get; set; }

        [JsonProperty("wind_speed")]
        public string WindSpeed { get; set; }

        [JsonProperty("wind_direction")]
        public string WindDirection { get; set; }

        [JsonProperty("atmo_opacity")]
        public string AtmoOpacity { get; set; }

        [JsonProperty("sunrise")]
        public string Sunrise { get; set; }

        [JsonProperty("sunset")]
        public string Sunset { get; set; }

        [JsonProperty("local_uv_irradiance_index")]
        public string UvIrradiance { get; set; }

        [JsonProperty("min_gts_temp")]
        public string MinGtsTemp { get; set; }

        [JsonProperty("max_gts_temp")]
        public string MaxGtsTemp { get; set; }
    }


}
