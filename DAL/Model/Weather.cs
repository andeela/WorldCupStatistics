using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace DAL.Model
{
    public class Weather
    {
        [JsonProperty("humidity", NullValueHandling = NullValueHandling.Ignore)]
        public string? Humidity { get; set; }

        [JsonProperty("temp_celsius", NullValueHandling = NullValueHandling.Ignore)]
        public string? TempCelsius { get; set; }

        [JsonProperty("temp_farenheit", NullValueHandling = NullValueHandling.Ignore)]
        public string? TempFarenheit { get; set; }

        [JsonProperty("wind_speed", NullValueHandling = NullValueHandling.Ignore)]
        public string? WindSpeed { get; set; }

        [JsonProperty("description", NullValueHandling = NullValueHandling.Ignore)]
        public string? Description { get; set; }
    }
}
