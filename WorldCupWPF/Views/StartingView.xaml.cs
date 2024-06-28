using DAL.Interfaces;
using DAL.Repos;
using DAL.Settings;
using DAL.Model;
using System;
using System.Collections.Generic;
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
using DAL.Model.Enums;

namespace WorldCupWPF.Views
{
    /// <summary>
    /// Interaction logic for StartingView.xaml
    /// </summary>
    public partial class StartingView : UserControl
    {
        public static ISettingsRepo settingsRepo = RepoFactory.GetSettingsRepo();

        public StartingView()
        {
            InitializeComponent();
            LoadSettings();
        }

        private async void LoadSettings()
        {
            var settings = await settingsRepo.GetSettingsAsync();

            if (settings != null) 
            {
                MainWindow mainWindow = Application.Current.MainWindow as MainWindow;
                if (mainWindow != null)
                {
                    mainWindow.NavigateToMatchView();
                }
            }
            else
            {
                // makni visak - pribaci loadSettings u matchView ili promini nacin 
                // trenutno postavke za gender category i langugage ne rade nista lol samo se ucitaju u combobox 
                // ali ok je jer se svakako ucitaju postavke postavljenje ili defaultne iz winformsa
                cbGenderCategory.SelectedIndex = settings.GenderCategory == GenderCategory.MEN ? 0 : 1;
                cbLanguage.SelectedIndex = settings.Language == DAL.Model.Enums.Language.ENGLISH ? 0 : 1;
                string selectedResolution = (cbResolution.SelectedItem as ComboBoxItem)?.Content.ToString();
                
                switch (selectedResolution)
                {
                    case "FULLSCREEN":
                        SetFullScreen(true);
                        break;
                    case "r1280x720":
                        SetWindowSize(1280, 720);
                        break;
                    case "r1024x768":
                        SetWindowSize(1024, 768);
                        break;
                    default:
                        SetFullScreen(false);
                        break;
                }
            }
        }

        private void SetWindowSize(int width, int height)
        {
            Application.Current.MainWindow.WindowState = WindowState.Normal;
            Application.Current.MainWindow.WindowStyle = WindowStyle.SingleBorderWindow;
            Application.Current.MainWindow.ResizeMode = ResizeMode.CanResize;
            Application.Current.MainWindow.Width = width;
            Application.Current.MainWindow.Height = height;
        }

        private void SetFullScreen(bool isFullScreen)
        {
            if (isFullScreen)
            {
                Application.Current.MainWindow.WindowState = WindowState.Maximized;
                Application.Current.MainWindow.WindowStyle = WindowStyle.None;
                Application.Current.MainWindow.ResizeMode = ResizeMode.NoResize;
            }
            else
            {
                Application.Current.MainWindow.WindowState = WindowState.Normal;
                Application.Current.MainWindow.WindowStyle = WindowStyle.SingleBorderWindow;
                Application.Current.MainWindow.ResizeMode = ResizeMode.CanResize;
            }
        }

        private void OnNextButtonClick(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = Application.Current.MainWindow as MainWindow;
            if (mainWindow != null)
            {
                mainWindow.NavigateToMatchView();
            }
        }
    }
}
