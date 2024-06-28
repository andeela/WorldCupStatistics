using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DAL.Interfaces;
using DAL.Model;
using DAL.Repos;
using DAL.Settings;
using iTextSharp.text.pdf.parser;

namespace WorldCupForms
{
    public partial class FavTeamForm : Form 
    {
        private const string FAV_PLAYERS = @"..\..\..\..\DAL\Settings\fav_players.txt";
        private const string FAV_TEAM = @"..\..\..\..\DAL\Settings\fav_team";
        public static IFavSettingsRepo favSettingsRepo = RepoFactory.GetFavSettingsRepo();
        public static IPlayerIconRepo playerIconRepo = RepoFactory.GetPlayerIconRepo();

        public FavTeamForm()
        {            
            InitializeComponent();
            LoadTeams();
            InitializeDragDrop();
        }

        private void InitializeDragDrop()
        {
            pnlFavPlayers.AllowDrop = true;
            pnlFavTeam.AllowDrop = true;

            pnlFavPlayers.DragEnter += Pnl_DragEnter;
            pnlFavPlayers.DragDrop += Pnl_DragDrop;

            pnlFavTeam.DragEnter += Pnl_DragEnter;
            pnlFavTeam.DragDrop += Pnl_DragDrop;
        }

        private void Pnl_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = e.Data.GetDataPresent(typeof(PlayerControl)) ? DragDropEffects.Move : DragDropEffects.None;
        }

        private void Pnl_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(PlayerControl)))
            {
                var playerControl = (PlayerControl)e.Data.GetData(typeof(PlayerControl));
                var panel = (Panel)sender;

                panel.Controls.Add(playerControl);
                ReorderPlayerControls(panel);

                if (panel == pnlFavPlayers)
                {
                    playerControl.SetFavourite(true);
                }
                else if (panel == pnlFavTeam)
                {
                    playerControl.SetFavourite(false);
                }

                SaveFavouritePlayers();
            }
        }

        private void ReorderPlayerControls(Panel panel)
        {
            int topPosition = 0;
            foreach (Control control in panel.Controls)
            {
                control.Top = topPosition;
                control.Left = 0;
                topPosition += control.Height + 10;
            }
        }

        private async void LoadTeams()
        {
            try
            {
                var settings = await DataFactory.GetAppSettingsAsync();
                var teams = await DataFactory.GetNationalTeamsAsync(settings.GenderCategory);

                cbFavTeam.DataSource = teams.ToList();
                cbFavTeam.DisplayMember = "Country";
                cbFavTeam.ValueMember = "Country";

                if (File.Exists(FAV_TEAM))
                {
                    string favTeam = File.ReadAllText(FAV_TEAM).Trim();
                    if (!string.IsNullOrEmpty(favTeam))
                    {
                        // vidi drugi nacin - malo je sporo
                        foreach (var item in cbFavTeam.Items)
                        {
                            if (cbFavTeam.GetItemText(item) == favTeam)
                            {
                                cbFavTeam.SelectedItem = item;
                                break;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading teams: {ex.Message}");
            }
        }

        private async void cbFavTeam_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selectedTeam = cbFavTeam.SelectedItem as NationalTeam;

            if (selectedTeam != null)
            {
                await LoadPlayers(selectedTeam);
                File.WriteAllText(FAV_TEAM, selectedTeam.Country);
            }
        }

        private async Task LoadPlayers(NationalTeam selectedTeam)
        {
            try
            {
                var countryName = selectedTeam.Country;
                var players = await DataFactory.GetPlayersForSelectedCountryAsync(countryName);

                if (players == null || !players.Any())
                {
                    MessageBox.Show("No players found for selected country");
                    return;
                }

                var favPlayers = LoadFavouritePlayers();
                var iconPaths = await playerIconRepo.GetAllIconPathsAsync();

                pnlFavPlayers.Controls.Clear();
                pnlFavTeam.Controls.Clear();

                foreach (var player in players)
                {
                    var isFavourite = favPlayers.Contains(player.Name);
                    iconPaths.TryGetValue(player.Name, out string imagePath);

                    var playerControl = new PlayerControl(
                        player.Name,
                        player.Position,
                        player.ShirtNumber,
                        player.Captain,
                        isFavourite,
                        imagePath,
                        pnlFavPlayers,
                        pnlFavTeam
                    );

                    if (isFavourite)
                    {
                        pnlFavPlayers.Controls.Add(playerControl);
                    }
                    else
                    {
                        pnlFavTeam.Controls.Add(playerControl);
                    }
                }

                ReorderPlayerControls(pnlFavPlayers);
                ReorderPlayerControls(pnlFavTeam);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading players: {ex.Message}");
            }
        }

        private HashSet<string> LoadFavouritePlayers()
        {
            if (!File.Exists(FAV_PLAYERS))
                return new HashSet<string>();

            var lines = File.ReadAllLines(FAV_PLAYERS);
            return new HashSet<string>(lines);
        }

        private void SaveFavouritePlayers()
        {
            var favPlayerNames = pnlFavPlayers.Controls.OfType<PlayerControl>().Select(pc => pc.PlayerName);
            File.WriteAllLines(FAV_PLAYERS, favPlayerNames);
        }

        private void btnRanks_Click(object sender, EventArgs e)
        {
            var selectedTeam = cbFavTeam.SelectedItem as NationalTeam;
            if (selectedTeam != null)
            {
                var rankingForm = new RankingForm(selectedTeam.Country);
                rankingForm.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Please select a team before proceeding to rankings.");
            }
        }

        private void btnSettings_Click(object sender, EventArgs e)
        {
            var settingsForm = new SettingsForm();
            settingsForm.Show();
            this.Hide();
        }
    }
}
