using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Model
{
    public class TeamStatistics
    {
        [JsonProperty("country", NullValueHandling = NullValueHandling.Ignore)]
        public string? Country { get; set; }

        [JsonProperty("attempts_on_goal", NullValueHandling = NullValueHandling.Ignore)]
        public int? AttemptsOnGoal { get; set; }

        [JsonProperty("on_target", NullValueHandling = NullValueHandling.Ignore)]
        public int? OnTarget { get; set; }

        [JsonProperty("off_target", NullValueHandling = NullValueHandling.Ignore)]
        public int? OffTarget { get; set; }

        [JsonProperty("blocked", NullValueHandling = NullValueHandling.Ignore)]
        public int? Blocked { get; set; }

        [JsonProperty("woodwork", NullValueHandling = NullValueHandling.Ignore)]
        public int? Woodwork { get; set; }

        [JsonProperty("corners", NullValueHandling = NullValueHandling.Ignore)]
        public int? Corners { get; set; }

        [JsonProperty("offsides", NullValueHandling = NullValueHandling.Ignore)]
        public int? Offsides { get; set; }

        [JsonProperty("ball_possession", NullValueHandling = NullValueHandling.Ignore)]
        public int? BallPossession { get; set; }

        [JsonProperty("pass_accuracy", NullValueHandling = NullValueHandling.Ignore)]
        public int? PassAccuracy { get; set; }

        [JsonProperty("num_passes", NullValueHandling = NullValueHandling.Ignore)]
        public int? NumPasses { get; set; }

        [JsonProperty("passes_completed", NullValueHandling = NullValueHandling.Ignore)]
        public int? PassesCompleted { get; set; }

        [JsonProperty("distance_covered", NullValueHandling = NullValueHandling.Ignore)]
        public int? DistanceCovered { get; set; }

        [JsonProperty("balls_recovered", NullValueHandling = NullValueHandling.Ignore)]
        public int? BallsRecovered { get; set; }

        [JsonProperty("tackles", NullValueHandling = NullValueHandling.Ignore)]
        public int? Tackles { get; set; }

        [JsonProperty("clearances", NullValueHandling = NullValueHandling.Ignore)]
        public int? Clearances { get; set; }

        [JsonProperty("yellow_cards", NullValueHandling = NullValueHandling.Ignore)]
        public int? YellowCards { get; set; }

        [JsonProperty("red_cards", NullValueHandling = NullValueHandling.Ignore)]
        public int? RedCards { get; set; }

        [JsonProperty("fouls_committed", NullValueHandling = NullValueHandling.Ignore)]
        public int? FoulsCommitted { get; set; }

        [JsonProperty("tactics", NullValueHandling = NullValueHandling.Ignore)]
        public string? Tactics { get; set; }

        [JsonProperty("starting_eleven", NullValueHandling = NullValueHandling.Ignore)]
        public List<Player>? StartingEleven { get; set; }

        [JsonProperty("substitutes", NullValueHandling = NullValueHandling.Ignore)]
        public List<Player>? Substitutes { get; set; }
    }
}
