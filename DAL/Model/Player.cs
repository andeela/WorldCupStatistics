using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Model
{
    public class Player
    {
        public static string DEAFULT_NAME = "null";

        [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
        public string? Name { get; set; }

        [JsonProperty("captain", NullValueHandling = NullValueHandling.Ignore)]
        public bool Captain { get; set; }

        [JsonProperty("shirt_number", NullValueHandling = NullValueHandling.Ignore)]
        public int ShirtNumber { get; set; }

        [JsonProperty("position", NullValueHandling = NullValueHandling.Ignore)]
        public string? Position { get; set; }

        public override bool Equals(object? obj)
        {
            return obj is Player player &&
                   Name == player.Name;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Name);
        }

        public override string ToString() => $"{Name}";
    }
}
