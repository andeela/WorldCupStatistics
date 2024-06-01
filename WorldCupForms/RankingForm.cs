using DAL.Interfaces;
using DAL.Model;
using DAL.Model.Enums;
using DAL.Repos;
using DAL.Settings;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Reflection.Metadata;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WorldCupForms
{
    public partial class RankingForm : Form
    {
        private readonly string selectedTeam;
        private static readonly ISettingsRepo settingsRepository = RepoFactory.GetSettingsRepo();


        public RankingForm(string team)
        {
            InitializeComponent();
            selectedTeam = team;
            InitializeComboBox();
        }

        private void InitializeComboBox()
        {
            cbRankBy.Items.Add("Players: most goals");
            cbRankBy.Items.Add("Players: most yellow cards");
            cbRankBy.Items.Add("Matches: most attendance");
            cbRankBy.SelectedIndex = 0;
        }

        private async void btnGenerateRanks_Click(object sender, EventArgs e)
        {
            string selectedCriteria = cbRankBy.SelectedItem.ToString();
            await DisplayRankingsAsync(selectedCriteria);
        }

        private async Task DisplayRankingsAsync(string selectedCriteria)
        {
            lbRankings.Items.Clear();
            if (selectedCriteria.Contains("Players"))
            {
                var playerRankings = await DataFactory.GetPlayerDataForSelectedCountryAsync(selectedTeam);
                var playerPictures = await DataFactory.GetPlayerPicturePathsAsync();
                List<(string PlayerName, int Goals, int YellowCards, int Occurances)> rankedPlayers;

                if (selectedCriteria == "Players: most goals")
                {
                    rankedPlayers = playerRankings.OrderByDescending(x => x.Value.Goals)
                        .Select(x => (x.Key, x.Value.Goals, x.Value.YellowCards, x.Value.Occurances)).ToList();
                }
                else
                {
                    rankedPlayers = playerRankings.OrderByDescending(x => x.Value.YellowCards)
                        .Select(x => (x.Key, x.Value.Goals, x.Value.YellowCards, x.Value.Occurances)).ToList();
                }

                foreach (var player in rankedPlayers)
                {
                    lbRankings.Items.Add($"{player.PlayerName} - Goals: {player.Goals}, Yellow Cards: {player.YellowCards}, Occurances: {player.Occurances}");
                }
            }
            else if (selectedCriteria == "Matches: most attendance")
            {          
                var settings = await settingsRepository.GetSettingsAsync();
                var matches = await DataFactory.GetMatchesAsync(settings.GenderCategory);  

                var rankedMatches = matches.OrderByDescending(m => m.Attendance)
                    .Select(m => new { Location = m.Location, Attendance = m.Attendance, HomeTeam = m.HomeTeamCountry, AwayTeam = m.AwayTeamCountry });

                foreach (var match in rankedMatches)
                {
                    lbRankings.Items.Add($"Location: {match.Location}, Attendance: {match.Attendance}, Home: {match.HomeTeam}, Away: {match.AwayTeam}");
                }
            }
        }

        private void btnPDF_Click(object sender, EventArgs e)
        {
            ExportListBoxToPDF(lbRankings, "Ranking.pdf"); 
        }

        private void ExportListBoxToPDF(ListBox listBox, string fileName)
        {
            string docPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments); // prominit putanju do pdfa!
            string filePath = Path.Combine(docPath, fileName);

            if (listBox.Items.Count > 0)
            {
                using (iTextSharp.text.Document pdfDoc = new iTextSharp.text.Document(PageSize.A4))
                {
                    PdfWriter.GetInstance(pdfDoc, new FileStream(fileName, FileMode.Create));
                    pdfDoc.Open();

                    foreach (var item in listBox.Items)
                    {
                        pdfDoc.Add(new Paragraph(item.ToString()));
                    }

                    pdfDoc.Close();
                }

                MessageBox.Show($"PDF has been successfully created at: {filePath}", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
