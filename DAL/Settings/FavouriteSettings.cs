using DAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Settings
{
    public class FavouriteSettings
    {
        public NationalTeam FavouriteTeam { get; set; }
        public IList<Player> FavouritePlayers { get; set; }

        public override string ToString() => FavouriteTeam.ToString() + " - " + FavouritePlayers.ToString();
    }
}
