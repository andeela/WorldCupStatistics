using DAL.Interfaces;
using DAL.Model;
using DAL.Model.Enums;
using DAL.Settings;
using System.Diagnostics;

namespace DAL.Repos
{
    public class DataFactory
    {
        private static readonly Lazy<Task<IDataRepo>> dataRepository
            = new Lazy<Task<IDataRepo>>(() => RepoFactory.GetDataRepoAsync()); 
        private static readonly ISettingsRepo settingsRepository = RepoFactory.GetSettingsRepo();
        private static readonly IFavSettingsRepo favouriteSettingsRepository = RepoFactory.GetFavSettingsRepo();
        private static readonly IPlayerIconRepo playerIconRepo = RepoFactory.GetPlayerIconRepo();

        public static async Task<ISet<TeamStatistics>> GetStatisticsAsync() => await Task.Run(() => GetStatistics());
        public static async Task<ISet<TeamEvent>> GetTeamEventsAsync() => await Task.Run(() => GetTeamEvents());

        public static async Task<ISet<NationalTeam>> GetNationalTeamsAsync(GenderCategory gender)
        {
            var dataRepoInstance = await dataRepository.Value;
            var teams = await dataRepoInstance.GetAllNationalTeamData(gender);
            return teams.OrderBy(nt => nt.Country).ToHashSet();
        }

        public static async Task<ISet<Match>> GetMatchesAsync(GenderCategory gender)
        {
            var dataRepoInstance = await dataRepository.Value;
            var matches = await dataRepoInstance.GetAllMatchData(gender);
            return matches;
        }


        public static async Task<ISet<Player>> GetPlayersAsync()
        {
            var players = new HashSet<Player>();
            var stats = await GetStatisticsAsync();

            foreach (var stat in stats)
            {
                if (stat.StartingEleven != null)
                {
                    players.UnionWith(stat.StartingEleven);
                }
                if (stat.Substitutes != null)
                {
                    players.UnionWith(stat.Substitutes);
                }
            }
            return players;
        }


        public static async Task<ISet<Player>> GetPlayersForSelectedCountryAsync(string country)
        {
            if (string.IsNullOrEmpty(country))
            {
                return new HashSet<Player>();
            }

            try
            {
                var settings = await settingsRepository.GetSettingsAsync();
                var matches = await GetMatchesAsync(settings.GenderCategory);

                TeamStatistics? stat = null;

                foreach (var match in matches)
                {
                    if (match.HomeTeam?.Country == country && match.HomeTeamStatistics != null)
                    {
                        stat = match.HomeTeamStatistics;
                        break;
                    }
                    else if (match.AwayTeam?.Country == country && match.AwayTeamStatistics != null)
                    {
                        stat = match.AwayTeamStatistics;
                        break;
                    }
                }

                if (stat == null)
                {
                    Debug.WriteLine($"No statistics found for team: {country}");
                    return new HashSet<Player>();
                }

                var startingEleven = stat.StartingEleven ?? Enumerable.Empty<Player>();
                var substitutes = stat.Substitutes ?? Enumerable.Empty<Player>();

                return startingEleven.Union(substitutes).ToHashSet();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error in GetPlayersForSelectedCountryAsync: {ex.Message}");
                throw;
            }
        }

        public static async Task<ISet<TeamEvent>> GetEventsAsync()
        {
            var settings = await settingsRepository.GetSettingsAsync();
            var events = new HashSet<TeamEvent>();
            var matches = await GetMatchesAsync(settings.GenderCategory);

            foreach (var match in matches)
            {
                if (match.HomeTeamEvents != null)
                {
                    events.UnionWith(match.HomeTeamEvents);
                }

                if (match.AwayTeamEvents != null)
                {
                    events.UnionWith(match.AwayTeamEvents);
                }
            }

            return events;
        }

        public static async Task<ISet<Match>> GetMatchDataForSelectedCountryAsync()
        {
            var dataRepoInstance = await dataRepository.Value; // Await the Lazy<Task<IDataRepo>> to get the IDataRepo instance
            var settings = await settingsRepository.GetSettingsAsync(); // Get the current app settings
            var favSettings = await GetFavouriteSettingsAsync();

            if (favSettings != null && favSettings.FavouriteTeam != null && favSettings.FavouriteTeam.FifaCode != null)
            {
                var favTeamFifaCode = favSettings.FavouriteTeam.FifaCode; // Get the favourite team's FIFA code
                return await dataRepoInstance.GetMatchDataByCountry(settings.GenderCategory, favTeamFifaCode);
            }
            else
            {
                // Handle the case where favSettings, favSettings.FavouriteTeam, or favSettings.FavouriteTeam.FifaCode is null
                throw new Exception("Favourite team or FIFA code is not available.");
            }
        }


        public static async Task<IDictionary<string, PlayerRanking>> GetPlayerDataForSelectedCountryAsync(string countryName)
        {
            var allPlayersData = await GetAllPlayerRankingDataAsync();
            var nededPlayersData = new Dictionary<string, PlayerRanking>();
            //var choosenTeamName = (await GetFavouriteSettingsAsync()).FavouriteTeam.Country;
            var countryTeamPlayerNames = new HashSet<string>();

            var settings = await settingsRepository.GetSettingsAsync();

            var matches = await GetMatchesAsync(settings.GenderCategory);
            foreach (var match in matches)
            {
                if (match.HomeTeamCountry == countryName)
                {
                    if (match.HomeTeamStatistics != null)
                    {
                        foreach (var player in match.HomeTeamStatistics.StartingEleven)
                        {
                            countryTeamPlayerNames.Add(player.Name);
                        }
                        foreach (var player in match.HomeTeamStatistics.Substitutes)
                        {
                            countryTeamPlayerNames.Add(player.Name);
                        }
                    }
                }
                else if (match.AwayTeamCountry == countryName)
                {
                    if (match.AwayTeamStatistics != null)
                    {
                        foreach (var player in match.AwayTeamStatistics.StartingEleven)
                        {
                            countryTeamPlayerNames.Add(player.Name);
                        }
                        foreach (var player in match.AwayTeamStatistics.Substitutes)
                        {
                            countryTeamPlayerNames.Add(player.Name);
                        }
                    }
                }
            }

            foreach (var playerName in countryTeamPlayerNames)
            {
                if (allPlayersData.TryGetValue(playerName, out var data) && !nededPlayersData.ContainsKey(playerName))
                {
                    nededPlayersData[playerName] = data;
                }
            }

            return nededPlayersData;
        }


        public static async Task<IDictionary<string, string>> GetPlayerPicturePathsAsync()
        {
            return await playerIconRepo.GetAllIconPathsAsync();
        }

        public static async Task SetPlayerPicturePathsAsync(IDictionary<string, string> paths)
        {
            await playerIconRepo.SaveAllIconsAsync(paths);
        }

        public static async Task<AppSettings> GetAppSettingsAsync()
        {
            return await settingsRepository.GetSettingsAsync();
        }

        public static async Task SetAppSettingsAsync(AppSettings settings)
        {
            await settingsRepository.UpdateSettingsAsync(settings);
        }

        public static async Task<FavouriteSettings> GetFavouriteSettingsAsync()
        {
            var favouriteSettings = await favouriteSettingsRepository.GetSettingsAsync();
            if (favouriteSettings == null)
            {
                throw new Exception("Favourite settings unavailable.");
            }

            return favouriteSettings;
        }

        public static async Task SetFavouriteSettingsAsync(FavouriteSettings settings)
        {
            await favouriteSettingsRepository.UpdateSettingsAsync(settings);
        }

        private static async Task<ISet<TeamStatistics>> GetStatistics()
        {
            var settings = await settingsRepository.GetSettingsAsync();
            var stats = new HashSet<TeamStatistics>();
            var matches = await GetMatchesAsync(settings.GenderCategory);
            foreach (var match in matches)
            {
                stats.Add(match.AwayTeamStatistics);
                stats.Add(match.HomeTeamStatistics);
            }
            return stats;
        }

        private static async Task<ISet<TeamEvent>> GetTeamEvents()
        {
            var settings = await settingsRepository.GetSettingsAsync();
            var events = new HashSet<TeamEvent>();
            var matches = await GetMatchesAsync(settings.GenderCategory);
            foreach (var match in matches)
            {
                events.UnionWith(match.HomeTeamEvents);
                events.UnionWith(match.AwayTeamEvents);
            }
            return events;
        }

        public static async Task<IDictionary<string, PlayerRanking>> GetAllPlayerRankingDataAsync()
        {
            var playersData = await GetPlayerGoalAndYellowCardDataAsync();

            var stats = await GetStatisticsAsync();
            foreach (var stat in stats)
            {
                foreach (var player in stat.StartingEleven)
                {
                    if (!playersData.ContainsKey(player.Name))
                    {
                        playersData[player.Name] = new PlayerRanking { Goals = 0, YellowCards = 0, Occurances = 1 };
                    }
                    else
                    {
                        playersData[player.Name].Occurances++;
                    }
                }
            }

            var events = await GetTeamEventsAsync();
            foreach (var ev in events)
            {
                if (ev.TypeOfEvent == "substitution-in")
                {
                    if (!playersData.ContainsKey(ev.Player))
                    {
                        playersData[ev.Player] = new PlayerRanking { Goals = 0, YellowCards = 0, Occurances = 1 };
                    }
                    else
                    {
                        playersData[ev.Player].Occurances++;
                    }
                }
            }

            return playersData;
        }

        private static async Task<IDictionary<string, PlayerRanking>> GetPlayerGoalAndYellowCardDataAsync()
        {
            var playersData = new Dictionary<string, PlayerRanking>();

            var events = await GetTeamEventsAsync();
            foreach (var item in events)
            {
                if (item.TypeOfEvent == "goal")
                {
                    if (!playersData.ContainsKey(item.Player))
                    {
                        playersData[item.Player] = new PlayerRanking { Goals = 1, YellowCards = 0, Occurances = 0 };
                    }
                    else
                    {
                        playersData[item.Player].Goals++;
                    }
                }
                else if (item.TypeOfEvent == "yellow-card")
                {
                    if (!playersData.ContainsKey(item.Player))
                    {
                        playersData[item.Player] = new PlayerRanking { Goals = 0, YellowCards = 1, Occurances = 0 };
                    }
                    else
                    {
                        playersData[item.Player].YellowCards++;
                    }
                }
            }

            return playersData;
        }
    }
}
