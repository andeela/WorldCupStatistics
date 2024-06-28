using DAL.Interfaces;
using DAL.Model;
using DAL.Model.Enums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repos
{
    internal class JsonDataRepo : IDataRepo
    {
        private const string FOLDER_PATH = @"..\..\..\..\DAL\JsonData";
        private readonly string _matchesPath;
        private readonly string _teamsPath;

        public JsonDataRepo(GenderCategory gender)
        {
            string genderPath = gender == GenderCategory.MEN ? "men" : "women";
            _matchesPath = Path.Combine(FOLDER_PATH, genderPath, "matches.json");
            _teamsPath = Path.Combine(FOLDER_PATH, genderPath, "results.json");
        }

        public async Task<ISet<Match>> GetAllMatchData(GenderCategory gender)
        {
            return await Utilities.ReadJsonFileAsync<HashSet<Match>>(_matchesPath);
        }

        public async Task<ISet<NationalTeam>> GetAllNationalTeamData(GenderCategory gender)
        {
            return await Utilities.ReadJsonFileAsync<HashSet<NationalTeam>>(_teamsPath);
        }

        public async Task<ISet<Match>> GetMatchDataByCountry(GenderCategory gender, string country)
        {
            var allMatches = await GetAllMatchData(gender);
            var countryMatches = new HashSet<Match>();
            foreach (Match match in allMatches)
            {
                if (match.HomeTeam.Code == country || match.AwayTeam.Code == country)
                {
                    countryMatches.Add(match);
                }
            }
            return countryMatches;
        }
    }
}
