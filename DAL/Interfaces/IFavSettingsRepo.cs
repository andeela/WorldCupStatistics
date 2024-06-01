using DAL.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IFavSettingsRepo
    {
        Task<bool> CreateSettingsAsync();
        Task<FavouriteSettings> GetSettingsAsync();
        Task UpdateSettingsAsync(FavouriteSettings appSettings);
    }
}
