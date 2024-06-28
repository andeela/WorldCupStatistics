using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using DAL.Model;
using DAL.Repos;
using DAL.Settings;

namespace WorldCupWPF.Views
{
    public partial class MatchView : UserControl
    {
        private const string FAV_TEAM_FILE = @"..\..\..\..\DAL\Settings\fav_team";
        private List<DAL.Model.Match> matches;
        private List<NationalTeam> teams;

        public MatchView()
        {
            InitializeComponent();
            LoadFavoriteTeam();
        }

        private async void LoadFavoriteTeam()
        {
            var settings = await DataFactory.GetAppSettingsAsync();
            string favTeamName = File.Exists(FAV_TEAM_FILE) ? File.ReadAllText(FAV_TEAM_FILE).Trim() : string.Empty;

            try
            {
                teams = (await DataFactory.GetNationalTeamsAsync(settings.GenderCategory)).ToList();
                var favTeam = teams.FirstOrDefault(t => t.Country == favTeamName);

                cbStartingTeam.ItemsSource = teams;
                cbStartingTeam.DisplayMemberPath = "Country";
                cbStartingTeam.SelectedItem = favTeam;

                if (favTeam != null)
                {
                    await LoadOpponents(favTeam.Country);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error loading teams: {ex.Message}");
            }
        }

        private async Task LoadOpponents(string favoriteTeam)
        {
            var settings = await DataFactory.GetAppSettingsAsync();

            matches = (await DataFactory.GetMatchesAsync(settings.GenderCategory)).ToList();
            var opponents = matches.Where(m => m.HomeTeam.Country == favoriteTeam || m.AwayTeam.Country == favoriteTeam)
                                   .Select(m => m.HomeTeam.Country == favoriteTeam ? m.AwayTeam : m.HomeTeam)
                                   .Distinct()
                                   .ToList();

            cbOpponentTeam.ItemsSource = opponents;
            cbOpponentTeam.DisplayMemberPath = "Country";
            cbOpponentTeam.SelectedValuePath = "Country";

            if (!opponents.Any())
            {
                cbOpponentTeam.SelectedIndex = -1;
            }
        }

        private async void cbStartingTeam_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbStartingTeam.SelectedItem is NationalTeam selectedTeam)
            {
                await LoadOpponents(selectedTeam.Country);
            }
        }

        private void cbOpponents_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var startingTeam = cbStartingTeam.SelectedItem as NationalTeam;
            var opponentTeam = cbOpponentTeam.SelectedItem as Team;

            if (startingTeam != null && opponentTeam != null)
            {
                var match = matches.FirstOrDefault(m =>
                    (m.HomeTeam.Country == startingTeam.Country && m.AwayTeam.Country == opponentTeam.Country) ||
                    (m.HomeTeam.Country == opponentTeam.Country && m.AwayTeam.Country == startingTeam.Country));

                if (match != null)
                {
                    string result = match.HomeTeam.Country == startingTeam.Country
                        ? $"{match.HomeTeam.Goals} : {match.AwayTeam.Goals}"
                        : $"{match.AwayTeam.Goals} : {match.HomeTeam.Goals}";

                    DoubleAnimation fadeInAnimation = new DoubleAnimation
                    {
                        From = 0.0,
                        To = 1.0,
                        Duration = TimeSpan.FromSeconds(0.5)
                    };

                    tbResult.BeginAnimation(TextBlock.OpacityProperty, fadeInAnimation);
                    tbResult.Text = result;
                }
                else
                {
                    tbResult.Text = "Match not found";
                }
            }
        }

        private async void btnAboutStartingTeam_Click(object sender, RoutedEventArgs e)
        {
            var startingTeam = cbStartingTeam.SelectedItem as NationalTeam;
            var opponentTeam = cbOpponentTeam.SelectedItem as Team;

            if (startingTeam == null || opponentTeam == null)
            {
                MessageBox.Show("Select both Teams.", "Hey!", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var match = matches.FirstOrDefault(m =>
                (m.HomeTeam.Country == startingTeam.Country && m.AwayTeam.Country == opponentTeam.Country) ||
                (m.HomeTeam.Country == opponentTeam.Country && m.AwayTeam.Country == startingTeam.Country));

            try
            {
                var settings = await DataFactory.GetAppSettingsAsync();
                var teams = await DataFactory.GetNationalTeamsAsync(settings.GenderCategory);

                var startingEleven = match?.HomeTeam?.Country == startingTeam.Country
                    ? match?.HomeTeamStatistics?.StartingEleven
                    : match?.AwayTeamStatistics?.StartingEleven;

                MainWindow mainWindow = Application.Current.MainWindow as MainWindow;
                mainWindow?.NavigateToStartingElevenView(startingEleven, startingTeam, opponentTeam);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error retrieving starting eleven: {ex.Message}");
            }
        }

        private void OnSettingsButtonClick(object sender, RoutedEventArgs e)
        {
            var settingsWindow = new SettingsWindow();
            settingsWindow.SettingsApplied += SettingsWindow_SettingsApplied;
            settingsWindow.ShowDialog();
        }

        private void SettingsWindow_SettingsApplied(object sender, EventArgs e)
        {
            LoadFavoriteTeam();
        }
    }
}
