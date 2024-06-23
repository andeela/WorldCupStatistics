using DAL.Model.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Settings
{
    public class AppSettings
    {
        public Language Language { get; set; }
        public GenderCategory GenderCategory { get; set; }
        public LoadingDataBy LoadingDataBy { get; set; }
        public Resolution Resolution { get; set; }

        public FavouriteSettings FavouriteSettings { get; set; }
    }
}
