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

            try
            {
                string favTeamName = File.ReadAllText(FAV_TEAM_FILE).Trim();
                if (string.IsNullOrEmpty(favTeamName))
                {
                    return;
                }

                teams = (await DataFactory.GetNationalTeamsAsync(settings.GenderCategory)).ToList(); 
                var favTeam = teams.FirstOrDefault(t => t.Country == favTeamName);

                if (favTeam == null)
                {
                    return;
                }

                cbStartingTeam.ItemsSource = teams;
                cbStartingTeam.DisplayMemberPath = "Country";
                cbStartingTeam.SelectedItem = favTeam;

                await LoadOpponents(favTeam.Country);
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

            Debug.WriteLine($"Opponents count for {favoriteTeam}: {opponents.Count}");
            foreach (var opponent in opponents)
            {
                Debug.WriteLine($"Opponent: {opponent.Country}");
            }

            cbOpponentTeam.ItemsSource = opponents;
            cbOpponentTeam.DisplayMemberPath = "Country";
            cbOpponentTeam.SelectedValuePath = "Country"; 
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
            Debug.WriteLine("cbOpponents_SelectionChanged triggered");

            var startingTeam = cbStartingTeam.SelectedItem as NationalTeam;
            if (startingTeam == null)
            {
                Debug.WriteLine("cbStartingTeam.SelectedItem is null or not of type NationalTeam");
                return;
            }

            var opponentTeam = cbOpponentTeam.SelectedItem as Team;
            if (opponentTeam == null)
            {
                Debug.WriteLine("cbOpponentTeam.SelectedItem is null or not of type Team");
                return;
            }

            Debug.WriteLine($"Starting team: {startingTeam.Country}, Opponent team: {opponentTeam.Country}");

            var match = matches.FirstOrDefault(m =>
                (m.HomeTeam.Country == startingTeam.Country && m.AwayTeam.Country == opponentTeam.Country) ||
                (m.HomeTeam.Country == opponentTeam.Country && m.AwayTeam.Country == startingTeam.Country));

            if (match != null)
            {
                string result;
                if (match.HomeTeam.Country == startingTeam.Country)
                {
                    result = $"{match.HomeTeam.Goals} : {match.AwayTeam.Goals}";
                }
                else
                {
                    result = $"{match.AwayTeam.Goals} : {match.HomeTeam.Goals}";
                }

                // animating result
                DoubleAnimation fadeInAnimation = new DoubleAnimation();
                fadeInAnimation.From = 0.0;
                fadeInAnimation.To = 1.0;
                fadeInAnimation.Duration = TimeSpan.FromSeconds(0.5); 

                tbResult.BeginAnimation(TextBlock.OpacityProperty, fadeInAnimation);

                tbResult.Text = result;
            }
            else
            {
                Debug.WriteLine("match == null");
            }
        }

        private async void btnAboutStartingTeam_Click(object sender, RoutedEventArgs e)
        {
            var startingTeam = cbStartingTeam.SelectedItem as NationalTeam;
            var opponentTeam = cbOpponentTeam.SelectedItem as Team;

            if (startingTeam == null)
            {
                Debug.WriteLine("No starting team selected.");
                return;
            }

            if (opponentTeam == null)
            {
                Debug.WriteLine("No opponent selected.");
                return;
            }

            var match = matches.FirstOrDefault(m =>
                (m.HomeTeam.Country == startingTeam.Country && m.AwayTeam.Country == opponentTeam.Country) ||
                (m.HomeTeam.Country == opponentTeam.Country && m.AwayTeam.Country == startingTeam.Country));

            if (match == null)
            {
                Debug.WriteLine("No match found for the selected teams.");
                return;
            }

            Debug.WriteLine($"Match found: {match.HomeTeam.Country} vs {match.AwayTeam.Country}");

            try
            {
                var settings = await DataFactory.GetAppSettingsAsync();
                var teams = await DataFactory.GetNationalTeamsAsync(settings.GenderCategory);

                if (startingTeam == null)
                {
                    Debug.WriteLine("Starting team not found in the teams list.");
                    return;
                }

                var startingEleven = match?.HomeTeam?.Country == startingTeam.Country
                    ? match?.HomeTeamStatistics?.StartingEleven
                    : match?.AwayTeamStatistics?.StartingEleven;

                if (startingEleven == null)
                {
                    Debug.WriteLine("Starting eleven not available for the selected match.");
                    return;
                }

                Debug.WriteLine("Starting eleven retrieved successfully.");

                MainWindow mainWindow = Application.Current.MainWindow as MainWindow;
                if (mainWindow != null)
                {
                    mainWindow.NavigateToStartingElevenView(startingEleven, startingTeam, opponentTeam);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error retrieving starting eleven: {ex.Message}");
            }
        }
    }
}
