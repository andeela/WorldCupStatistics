using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Model
{
    public class Team
    {
        [JsonProperty("country", NullValueHandling = NullValueHandling.Ignore)]
        public string? Country { get; set; }

        [JsonProperty("code", NullValueHandling = NullValueHandling.Ignore)]
        public string? Code { get; set; }

        [JsonProperty("goals", NullValueHandling = NullValueHandling.Ignore)]
        public int? Goals { get; set; }

        [JsonProperty("penalties", NullValueHandling = NullValueHandling.Ignore)]
        public int? Penalties { get; set; }

        public override string ToString() => $"{Country} ({Code})";
    }
}
