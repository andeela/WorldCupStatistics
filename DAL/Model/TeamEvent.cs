using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Model
{
    public class TeamEvent
    {
        [JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)]
        public int? Id { get; set; }

        [JsonProperty("type_of_event", NullValueHandling = NullValueHandling.Ignore)]
        public string? TypeOfEvent { get; set; }

        [JsonProperty("player", NullValueHandling = NullValueHandling.Ignore)]
        public string? Player { get; set; }

        [JsonProperty("time", NullValueHandling = NullValueHandling.Ignore)]
        public string? Time { get; set; }

        public override bool Equals(object? obj)
        {
            return obj is TeamEvent @event &&
                   Id == @event.Id;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id);
        }
    }
}
