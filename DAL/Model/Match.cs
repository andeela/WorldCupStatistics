using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace DAL.Model
{
    public class Match
    {
        [JsonProperty("venue", NullValueHandling = NullValueHandling.Ignore)]
        public string? Venue { get; set; }

        [JsonProperty("location", NullValueHandling = NullValueHandling.Ignore)]
        public string? Location { get; set; }

        [JsonProperty("status", NullValueHandling = NullValueHandling.Ignore)]
        public string? Status { get; set; }

        [JsonProperty("time", NullValueHandling = NullValueHandling.Ignore)]
        public string? Time { get; set; }

        [JsonProperty("fifa_id", NullValueHandling = NullValueHandling.Ignore)]
        public string? FifaId { get; set; }

        [JsonProperty("weather", NullValueHandling = NullValueHandling.Ignore)]
        public Weather? Weather { get; set; }

        [JsonProperty("attendance", NullValueHandling = NullValueHandling.Ignore)]
        public string? Attendance { get; set; }

        [JsonProperty("officials", NullValueHandling = NullValueHandling.Ignore)]
        public List<string>? Officials { get; set; }

        [JsonProperty("stage_name", NullValueHandling = NullValueHandling.Ignore)]
        public string? StageName { get; set; }

        [JsonProperty("home_team_country", NullValueHandling = NullValueHandling.Ignore)]
        public string? HomeTeamCountry { get; set; }

        [JsonProperty("away_team_country", NullValueHandling = NullValueHandling.Ignore)]
        public string? AwayTeamCountry { get; set; }

        [JsonProperty("datetime", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime? DateTime { get; set; }

        [JsonProperty("winner", NullValueHandling = NullValueHandling.Ignore)]
        public string? Winner { get; set; }

        [JsonProperty("winner_code", NullValueHandling = NullValueHandling.Ignore)]
        public string? WinnerCode { get; set; }

        [JsonProperty("home_team", NullValueHandling = NullValueHandling.Ignore)]
        public Team? HomeTeam { get; set; }

        [JsonProperty("away_team", NullValueHandling = NullValueHandling.Ignore)]
        public Team? AwayTeam { get; set; }

        [JsonProperty("home_team_events", NullValueHandling = NullValueHandling.Ignore)]
        public List<TeamEvent>? HomeTeamEvents { get; set; }

        [JsonProperty("away_team_events", NullValueHandling = NullValueHandling.Ignore)]
        public List<TeamEvent>? AwayTeamEvents { get; set; }

        [JsonProperty("home_team_statistics", NullValueHandling = NullValueHandling.Ignore)]
        public TeamStatistics? HomeTeamStatistics { get; set; }

        [JsonProperty("away_team_statistics", NullValueHandling = NullValueHandling.Ignore)]
        public TeamStatistics? AwayTeamStatistics { get; set; }

        [JsonProperty("last_event_update_at", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime? LastEventUpdateAt { get; set; }

        [JsonProperty("last_score_update_at", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime? LastScoreUpdateAt { get; set; }
    }
}
