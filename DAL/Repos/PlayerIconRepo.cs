using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repos
{
    public class PlayerIconRepo : IPlayerIconRepo
    {
        private const string ICON_PATH_FILE = @"..\..\..\..\DAL\Images\iconPaths.txt";

        public async Task<Dictionary<string, string>> GetAllIconPathsAsync()
        {
            var iconPaths = new Dictionary<string, string>();

            if (File.Exists(ICON_PATH_FILE))
            {
                var lines = await File.ReadAllLinesAsync(ICON_PATH_FILE);

                foreach (var line in lines)
                {
                    var parts = line.Split(',');
                    if (parts.Length == 2)
                    {
                        iconPaths[parts[0]] = parts[1];
                    }
                }
            }

            return iconPaths;
        }

        public async Task SaveAllIconsAsync(IDictionary<string, string> iconPaths)
        {
            var lines = iconPaths.Select(kvp => $"{kvp.Key},{kvp.Value}").ToList();
            await File.WriteAllLinesAsync(ICON_PATH_FILE, lines);
        }
    }
}
