using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using DAL;
using DAL.Model;
using DAL.Repos;
using DAL.Settings;
using Newtonsoft.Json.Linq;
using WorldCupWPF.Views;

namespace WorldCupWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            MainContentFrame.Navigate(new Uri("Views/StartingView.xaml", UriKind.Relative));
        }

        public void NavigateToMatchView()
        {
            MainContentFrame.Navigate(new Uri("Views/MatchView.xaml", UriKind.Relative));
        }

        public void NavigateToStartingElevenView(List<Player> startingEleven, NationalTeam team, Team opponent)
        {
            StartingElevenView startingElevenView = new StartingElevenView();
            
            int wins = team.Wins.Value;
            int loses = team.Losses.Value;
            int draws = team.Draws.Value;
            int goalsScored = team.GoalsFor.Value;
            int goalsReceived = team.GoalsAgainst.Value;
            int goalDifference = team.GoalDifferential.Value;

            startingElevenView.LoadData(startingEleven, team, opponent.Country, wins, loses, draws, goalsScored, goalsReceived, goalDifference);

            this.Content = startingElevenView;
        }
    }
}