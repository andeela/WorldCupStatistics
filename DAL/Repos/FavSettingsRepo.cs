using DAL.Interfaces;
using DAL.Model;
using DAL.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repos
{
    public class FavSettingsRepo : IFavSettingsRepo
    {
        private const string SETTINGS_FILE_PATH = @"..\..\..\..\DAL\Settings\FavSettings.txt";
        private const string SETTINGS_TEAM = "favouriteTeam";
        private const string SETTINGS_PLAYERS = "favouritePlayers";

        public async Task<bool> CreateSettingsAsync() 
        {
            if(!File.Exists(SETTINGS_FILE_PATH))
            {
                var settings = new List<string>
                {
                    $"{SETTINGS_TEAM}{Utilities.DELIMITER}null",
                    $"{SETTINGS_PLAYERS}{Utilities.DELIMITER}{string.Join(Utilities.DELIMITER_LIST, Enumerable.Repeat(Player.DEAFULT_NAME, 3))}"
                };

                await File.WriteAllLinesAsync(SETTINGS_FILE_PATH, settings);
                return true;
            }
            return false;
        }

        public async Task<FavouriteSettings> GetSettingsAsync()
        {
            await CreateSettingsAsync(); // if they don't exist
            var data = await File.ReadAllLinesAsync(SETTINGS_FILE_PATH);
            var settings = new FavouriteSettings
            {
                FavouriteTeam = await ParseFavTeamAsync(data[0]),
                FavouritePlayers = await ParseFavPlayers(data[1])
            };

            return settings;
        }

        private async Task<List<Player>> ParseFavPlayers(string line)
        {
            var favPlayers = new List<Player>();
            var playerNames = Utilities.GetValuesFromLine(line);
            var players = await DataFactory.GetPlayersAsync();

            foreach ( var playerName in playerNames ) {
                var player = players.FirstOrDefault(p => p.Name == playerName);
                if ( player != null )
                {
                    favPlayers.Add(player);
                }
            }                
            return favPlayers;
        }

        private async Task<NationalTeam> ParseFavTeamAsync(string line)
        {

            string value = Utilities.GetValueFromLine(line); 
            var settings = await DataFactory.GetAppSettingsAsync();
            var gender = settings.GenderCategory;
            var nationalTeams = await DataFactory.GetNationalTeamsAsync(gender);
            return nationalTeams.FirstOrDefault(t => t.Country == value);
        }

        

        public async Task UpdateSettingsAsync(FavouriteSettings appSettings)
        {
            await CreateSettingsAsync(); // if they don't exist

            var playersString = string.Join(Utilities.DELIMITER_LIST, appSettings.FavouritePlayers.Select(p => p.Name));

            var settings = new List<string>
            {
                $"{SETTINGS_TEAM}{Utilities.DELIMITER}{appSettings.FavouriteTeam.Country}",
                $"{SETTINGS_PLAYERS}{Utilities.DELIMITER}{playersString}"
            };

            await File.WriteAllLinesAsync(SETTINGS_FILE_PATH, settings);
        }
    }
}
