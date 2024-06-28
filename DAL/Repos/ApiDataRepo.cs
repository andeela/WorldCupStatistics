using DAL.Interfaces;
using DAL.Model;
using DAL.Model.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace DAL.Repos
{
    internal class ApiDataRepo : IDataRepo
    {
        private const string URLBASE = "https://worldcup-vua.nullbit.hr";
        private readonly HttpClient _httpClient;

        public ApiDataRepo() => _httpClient = new HttpClient();

        public async Task<ISet<Match>> GetAllMatchData(GenderCategory gender)
        {
            string url = $"{URLBASE}/{GetGender(gender)}/matches";
            return await Utilities.GetJsonObjFromUrlAsync<HashSet<Match>>(url);
        }

        public async Task<ISet<NationalTeam>> GetAllNationalTeamData(GenderCategory gender)
        {
            string url = $"{URLBASE}/{GetGender(gender)}/teams/results";
            return await Utilities.GetJsonObjFromUrlAsync<HashSet<NationalTeam>>(url);
        }

        public async Task<ISet<Match>> GetMatchDataByCountry(GenderCategory gender, string country)
        {
            string url = $"{URLBASE}/{GetGender(gender)}/matches/country?fifa_code={country}";
            return await Utilities.GetJsonObjFromUrlAsync<HashSet<Match>>(url);
        }

        private string GetGender(GenderCategory gender) => gender == GenderCategory.MEN ? "men" : "women";

    }
}
