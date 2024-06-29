using DAL.Model;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using WorldCupWPF.Views;

namespace WorldCupWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private UserControl currentView;

        public MainWindow()
        {
            InitializeComponent();
            MainContentFrame.Navigate(new Uri("Views/MatchView.xaml", UriKind.Relative)); 
        }

        public void NavigateToMatchView()
        {
            MatchView matchView = new MatchView();
            this.Content = matchView;
            currentView = matchView;
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
