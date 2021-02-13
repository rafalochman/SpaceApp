using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NASAapp.Models
{
    public class MarsWeather
    {
        public int status { get; set; }
        public int id { get; set; }
        public string terrestrial_date { get; set; }
        public int ls { get; set; }
        public string season { get; set; }
        public int min_temp { get; set; }
        public int max_temp { get; set; }
        public int pressure { get; set; }
        public string pressure_string { get; set; }
        public object abs_humidity { get; set; }
        public object wind_speed { get; set; }
        public string atmo_opacity { get; set; }
        public string sunrise { get; set; }
        public string sunset { get; set; }
        public string local_uv_irradiance_index { get; set; }
        public int min_gts_temp { get; set; }
        public int max_gts_temp { get; set; }
        public object wind_direction { get; set; }
        public int sol { get; set; }
        public string unitOfMeasure { get; set; }
        public string TZ_Data { get; set; }
    }

}
