using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NASAapp.Models
{

    public class MarsWeather
    {
        public Descriptions descriptions { get; set; }
        public Sole[] soles { get; set; }
    }

    public class Descriptions
    {
        public string disclaimer_en { get; set; }
        public string disclaimer_es { get; set; }
        public string sol_desc_en { get; set; }
        public string sol_desc_es { get; set; }
        public string terrestrial_date_desc_en { get; set; }
        public string terrestrial_date_desc_es { get; set; }
        public string temp_desc_en { get; set; }
        public string temp_desc_es { get; set; }
        public string pressure_desc_en { get; set; }
        public string pressure_desc_es { get; set; }
        public string abs_humidity_desc_en { get; set; }
        public string abs_humidity_desc_es { get; set; }
        public string wind_desc_en { get; set; }
        public string wind_desc_es { get; set; }
        public string gts_temp_desc_en { get; set; }
        public string gts_temp_desc_es { get; set; }
        public string local_uv_irradiance_index_desc_en { get; set; }
        public string local_uv_irradiance_index_desc_es { get; set; }
        public string atmo_opacity_desc_en { get; set; }
        public string atmo_opacity_desc_es { get; set; }
        public string ls_desc_en { get; set; }
        public string ls_desc_es { get; set; }
        public string season_desc_en { get; set; }
        public string season_desc_es { get; set; }
        public string sunrise_sunset_desc_en { get; set; }
        public string sunrise_sunset_desc_es { get; set; }
    }

    public class Sole
    {
        public string id { get; set; }
        public string terrestrial_date { get; set; }
        public string sol { get; set; }
        public string ls { get; set; }
        public string season { get; set; }
        public string min_temp { get; set; }
        public string max_temp { get; set; }
        public string pressure { get; set; }
        public string pressure_string { get; set; }
        public string abs_humidity { get; set; }
        public string wind_speed { get; set; }
        public string wind_direction { get; set; }
        public string atmo_opacity { get; set; }
        public string sunrise { get; set; }
        public string sunset { get; set; }
        public string local_uv_irradiance_index { get; set; }
        public string min_gts_temp { get; set; }
        public string max_gts_temp { get; set; }
    }


}
