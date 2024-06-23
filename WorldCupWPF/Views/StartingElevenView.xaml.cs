using DAL.Model;
using DAL.Repos;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WorldCupForms;
using DAL;


namespace WorldCupWPF.Views
{
    /// <summary>
    /// Interaction logic for StartingElevenView.xaml
    /// </summary>
    public partial class StartingElevenView : UserControl
    {
        private readonly PlayerIconRepo _playerIconRepo;

        public StartingElevenView()
        {
            InitializeComponent();
            _playerIconRepo = new PlayerIconRepo();
        }

        public void LoadData(List<Player> startingEleven, string nationalTeam, string opponent, int wins, int loses, int draws, int goalsScored, int goalsReceived, int goalDifference)
        {
            tbNationalTeam.Text = $"{nationalTeam} - all around stats";
            tbWins.Text = $"Wins: {wins}";
            tbLoses.Text = $"Losses: {loses}";
            tbDraws.Text = $"Draws: {draws}";
            tbGoalsScored.Text = $"Goals scored: {goalsScored}";
            tbGoalsRecieved.Text = $"Goals received: {goalsReceived}";
            tbGoalDifference.Text = $"Goal difference: {goalDifference}";

            tbStartingEleven.Text = $"Starting Eleven against {opponent}";

            gridStartingEleven.Children.Clear();

            var formation = DetermineFormation(startingEleven);

            ArrangePlayers(startingEleven, formation);
        }

        private string DetermineFormation(List<Player> players)
        {
            int defenders = players.Count(p => p.Position == "Defender");
            int midfield = players.Count(p => p.Position == "Midfield");
            int forward = players.Count(p => p.Position == "Forward");

            return $"{defenders}-{midfield}-{forward}";
        }

        private async void ArrangePlayers(List<Player> players, string formation)
        {
            var iconPaths = await _playerIconRepo.GetAllIconPathsAsync();

            // Clear previous players
            gridStartingEleven.Children.Clear();

            // Place the goalie
            var goalie = players.FirstOrDefault(p => p.Position == "Goalie");
            if (goalie != null)
            {
                PlayerControlWPF goalieControl = new PlayerControlWPF(goalie.Name, goalie.ShirtNumber);
                Grid.SetColumn(goalieControl, 0);  // Goalie is placed in the first row
                Grid.SetRow(goalieControl, 2);  // Goalie is placed in the third column
                gridStartingEleven.Children.Add(goalieControl);
            }
            else
            {
                Debug.WriteLine("No goalie found in the starting eleven.");
                return;  // If no goalie is found, exit the method
            }

            // Define position mappings for each player type
            Dictionary<string, Point[]> positionMappings = new Dictionary<string, Point[]>
            {
                { "Defender", new Point[] { new Point(1, 0), new Point(1, 1), new Point(1, 2), new Point(1, 3), new Point(1, 4) } },
                { "Midfield", new Point[] {/* new Point(2, 0),*/ new Point(2, 1), new Point(2, 2), new Point(2, 3), /*new Point(2, 4),
                                    new Point(3, 0),*/ new Point(3, 1), new Point(3, 2), new Point(3, 3)/*, new Point(3, 4)*/ } },
                { "Forward", new Point[] { new Point(4, 0), new Point(4, 1), new Point(4, 2), new Point(4, 3), new Point(4, 4) } }
            };

            // Create lists to hold players by position
            List<Player> defenders = players.Where(p => p.Position == "Defender").ToList();
            List<Player> midfielders = players.Where(p => p.Position == "Midfield").ToList();
            List<Player> forwards = players.Where(p => p.Position == "Forward").ToList();

            // Method to place players in the grid
            void PlacePlayers(List<Player> playersList, Point[] positions)
            {
                for (int i = 0; i < playersList.Count && i < positions.Length; i++)
                {
                    var player = playersList[i];
                    var playerControl = new PlayerControlWPF
                    {
                        PlayerName = player.Name,
                        ShirtNumber = player.ShirtNumber,
                        ImagePath = iconPaths.ContainsKey(player.Name) ? iconPaths[player.Name] : null
                    };
                    Grid.SetColumn(playerControl, (int)positions[i].X);
                    Grid.SetRow(playerControl, (int)positions[i].Y);
                    gridStartingEleven.Children.Add(playerControl);
                }
            }

            // Place players based on their position
            PlacePlayers(defenders, positionMappings["Defender"]);
            PlacePlayers(midfielders, positionMappings["Midfield"]);
            PlacePlayers(forwards, positionMappings["Forward"]);
        }

        private void OnBtnGoBackClick(object sender, EventArgs e)
        {
            MainWindow mainWindow = Application.Current.MainWindow as MainWindow;
            if (mainWindow != null)
            {
                mainWindow.NavigateToMatchView();
            }
        }
    }
}