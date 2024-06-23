using DAL.Interfaces;
using DAL.Model.Enums;
using DAL.Repos;
using DAL.Settings;
using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WorldCupForms
{
    public partial class SettingsForm : Form
    {
        private readonly ISettingsRepo settingsRepo = RepoFactory.GetSettingsRepo();
        private AppSettings currentSettings;

        private const string SETTINGS_FILE_PATH = @"..\..\..\..\DAL\Settings\settings.txt";

        public SettingsForm()
        {
            InitializeComponent();
            LoadSettingsAsync();
        }

        private async Task LoadSettingsAsync()
        {
            currentSettings = await settingsRepo.GetSettingsAsync();
            ChangeSettings();
        }

        private void ChangeSettings()
        {
            cbGenderCategory.SelectedIndex = currentSettings.GenderCategory == GenderCategory.MEN ? 0 : 1;
            cbLanguage.SelectedIndex = currentSettings.Language == Language.ENGLISH ? 0 : 1;
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

            currentSettings.GenderCategory = genderCategory;
            currentSettings.Language = language;

            await settingsRepo.UpdateSettingsAsync(currentSettings);

            string[] lines = File.ReadAllLines(SETTINGS_FILE_PATH);
            foreach (string line in lines)
            {
                Debug.WriteLine(line);
            }

            return true;
        }

        private async void btnApplyChanges_Click(object sender, EventArgs e)
        {
            if (await SaveSettings())
            {
                FavTeamForm favTeamForm = new FavTeamForm();
                favTeamForm.Show();
                this.Close();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            FavTeamForm favTeamForm = new FavTeamForm();
            favTeamForm.Show();
            this.Close();
        }
    }
}
