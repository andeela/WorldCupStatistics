using DAL.Model;
using DAL.Model.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IDataRepo
    {
        Task<ISet<Match>> GetAllMatchData(GenderCategory gender);
        Task<ISet<Match>> GetMatchDataByCountry(GenderCategory gender, string country);
        Task<ISet<NationalTeam>> GetAllNationalTeamData(GenderCategory gender);
    }
}
