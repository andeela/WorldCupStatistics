using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DAL;
using DAL.Interfaces;
using DAL.Model.Enums;
using DAL.Repos;
using DAL.Settings;

namespace WorldCupForms
{
    public partial class StartingForm : Form
    {
        readonly ISettingsRepo settingsRepo = RepoFactory.GetSettingsRepo();
        readonly IFavSettingsRepo favSettingsRepo = RepoFactory.GetFavSettingsRepo();
        private AppSettings currentSettings;

        public StartingForm()
        {
            InitializeComponent();
            LoadSettings();
        }

        private async void LoadSettings()
        {
            bool favSettingsExist = await CheckIfFavSettingsExistAsync();
            

            if (favSettingsExist)
            {
                // directly open FavTeamForm with fav settings
                var favTeamForm = new FavTeamForm();
                favTeamForm.Show();
                this.Hide();
            }
            else if (currentSettings != null)
            {
                // load settings into the form control
                currentSettings = await settingsRepo.GetSettingsAsync();

                if (currentSettings != null)
                {
                    cbGenderCategory.SelectedIndex = currentSettings.GenderCategory == GenderCategory.MEN ? 0 : 1;
                    cbLanguage.SelectedIndex = currentSettings.Language == Language.ENGLISH ? 0 : 1;  
                }
            }
        }

        private async Task<bool> CheckIfFavSettingsExistAsync()
        {
            try
            {
                var favSettings = await favSettingsRepo.GetSettingsAsync();
                return favSettings != null && favSettings.FavouriteTeam != null;
            }
            catch (FileNotFoundException)
            {
                return false;
            }
        }

        private async void bttnNext_Click(object sender, EventArgs e)
        {
            if (await SaveSettings())
            {
                var favTeamForm = new FavTeamForm();
                favTeamForm.Show();
                this.Hide(); 
            }
        }

        private async Task<bool> SaveSettings()
        {
            if (cbGenderCategory.SelectedIndex < 0 || cbLanguage.SelectedIndex < 0)
            {
                MessageBox.Show("Please select both gender category and language.");
                return false;
            }

            var genderCategory = cbGenderCategory.SelectedIndex == 0 ? GenderCategory.MEN : GenderCategory.WOMEN;
            var language = cbLanguage.SelectedIndex == 0 ? Language.ENGLISH : Language.CROATIAN;

            currentSettings = new AppSettings
            {
                GenderCategory = genderCategory,
                Language = language
            };

            await settingsRepo.UpdateSettingsAsync(currentSettings);
            return true;
        }

        private void cbGenderCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            // no aditional action needed, as the selection will be saved later
        }

        private void cbLanguage_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbLanguage.SelectedIndex == 0)
            {
                // english selected
                CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("en-US");
                CultureInfo.DefaultThreadCurrentUICulture = new CultureInfo("en-US");
            }
            else if (cbLanguage.SelectedIndex == 1)
            {
                // croatian selected
                CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("hr-HR");
                CultureInfo.DefaultThreadCurrentUICulture = new CultureInfo("hr-HR");
            }
        }
    }
}
