using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace DAL.Model
{
    public class NationalTeam
    {
        [JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)]
        public int? Id { get; set; }        
        
        [JsonProperty("country", NullValueHandling = NullValueHandling.Ignore)]
        public string? Country { get; set; }

        [JsonProperty("alternate_name", NullValueHandling = NullValueHandling.Ignore)]
        public string? AlternateName { get; set; }

        [JsonProperty("fifa_code", NullValueHandling = NullValueHandling.Ignore)]
        public string? FifaCode { get; set; }

        [JsonProperty("group_id", NullValueHandling = NullValueHandling.Ignore)]
        public int? GroupId { get; set; }

        [JsonProperty("group_letter", NullValueHandling = NullValueHandling.Ignore)]
        public string? GroupLetter { get; set; }

        [JsonProperty("wins", NullValueHandling = NullValueHandling.Ignore)]
        public int? Wins { get; set; }

        [JsonProperty("draws", NullValueHandling = NullValueHandling.Ignore)]
        public int? Draws { get; set; }

        [JsonProperty("losses", NullValueHandling = NullValueHandling.Ignore)]
        public int? Losses { get; set; }

        [JsonProperty("games_played", NullValueHandling = NullValueHandling.Ignore)]
        public int? GamesPlayed { get; set; }

        [JsonProperty("points", NullValueHandling = NullValueHandling.Ignore)]
        public int? Points { get; set; }

        [JsonProperty("goals_for", NullValueHandling = NullValueHandling.Ignore)]
        public int? GoalsFor { get; set; }

        [JsonProperty("goals_against", NullValueHandling = NullValueHandling.Ignore)]
        public int? GoalsAgainst { get; set; }

        [JsonProperty("goal_differential", NullValueHandling = NullValueHandling.Ignore)]
        public int? GoalDifferential { get; set; }

        public override bool Equals(object? obj)
        {
            return obj is NationalTeam team &&
                   Id == team.Id;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id);
        }

        public override string ToString() => $"{Country} ({FifaCode})";
    }
}
