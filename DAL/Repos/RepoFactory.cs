using DAL.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Interfaces;
using DAL.Model.Enums;
using System.Data.SqlTypes;

namespace DAL.Repos
{
    public class RepoFactory
    {
        public static ISettingsRepo GetSettingsRepo() => new AppSettingsRepo();
       public static IFavSettingsRepo GetFavSettingsRepo() => new FavSettingsRepo();

        public static async Task<IDataRepo> GetDataRepoAsync()
        {
            var settingsRepo = GetSettingsRepo();
            var settings = await settingsRepo.GetSettingsAsync();
            var repoType = settings.LoadingDataBy;

            if(repoType == LoadingDataBy.FILE)
            {
                return new JsonDataRepo(settings.GenderCategory); 
            }
            else if (repoType == LoadingDataBy.API)
            {
                return new ApiDataRepo();
            }
            else { throw new InvalidOperationException("Invalid repo type."); }
        }
        private static readonly AppSettings appSettings;
        public static IDataRepo GetJsonDataRepo() => new JsonDataRepo(appSettings.GenderCategory); 
        public static IDataRepo GetApiRepo() => new ApiDataRepo();
        public static IPlayerIconRepo GetPlayerIconRepo() => new PlayerIconRepo();
    }
}
