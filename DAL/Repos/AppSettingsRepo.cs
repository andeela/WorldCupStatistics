using DAL.Interfaces;
using DAL.Model.Enums;
using DAL.Settings;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repos
{
    internal class AppSettingsRepo : ISettingsRepo
    {
        private const string SETTINGS_FILE_PATH = @"..\..\..\..\DAL\Settings\settings.txt";
        
        public async Task<bool> CreateSettingsAsync()
        {
            if(!File.Exists(SETTINGS_FILE_PATH))
            {
                 var appSettings = new AppSettings();

                 var defaultSettings = new List<string>
                 {
                     $"{SettingsType.Repository}:{LoadingDataBy.API}",
                     $"{SettingsType.Language}:{Language.ENGLISH}",
                     $"{SettingsType.Championship}:{GenderCategory.MEN}",
                     $"{SettingsType.Resolution}:{Resolution.FULLSCREEN}"
                 };            

                 await File.WriteAllLinesAsync(SETTINGS_FILE_PATH, defaultSettings);

                return true;
            }
            return false;
        }

        public async Task<AppSettings> GetSettingsAsync()
        {
            await CreateSettingsAsync();

            //var appSettings = new AppSettings();
            //await UpdateSettingsAsync(appSettings);

            var data = await File.ReadAllLinesAsync(SETTINGS_FILE_PATH);

            var settings = new AppSettings
            {
                LoadingDataBy = Utilities.ParseEnumValue<LoadingDataBy>(data[0]),
                Language = Utilities.ParseEnumValue<Language>(data[1]),
                GenderCategory = Utilities.ParseEnumValue<GenderCategory>(data[2]),
                Resolution = Utilities.ParseEnumValue<Resolution>(data[3])
            };

            return settings;
        }

        public async Task UpdateSettingsAsync(AppSettings appSettings)
        {            
            await CreateSettingsAsync(); // if they don't exist

            var updatedSettings = new List<string>
            {
                $"{SettingsType.Repository}:{appSettings.LoadingDataBy}",
                $"{SettingsType.Language}:{appSettings.Language}",
                $"{SettingsType.Championship}:{appSettings.GenderCategory}",
                $"{SettingsType.Resolution}:{appSettings.Resolution}"
            };

            File.WriteAllLines(SETTINGS_FILE_PATH, updatedSettings);
        }
    }
}
