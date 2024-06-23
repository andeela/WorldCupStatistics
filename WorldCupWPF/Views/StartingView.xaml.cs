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
        private readonly ISettingsRepo settingsRepo;

        public StartingView()
        {
            InitializeComponent();
            settingsRepo = new AppSettingsRepo();

            LoadSettings();
        }

        private async void LoadSettings()
        {
            AppSettings settings = await settingsRepo.GetSettingsAsync();

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
                cbGenderCategory.SelectedIndex = settings.GenderCategory == GenderCategory.MEN ? 0 : 1;
                cbLanguage.SelectedIndex = settings.Language == DAL.Model.Enums.Language.ENGLISH ? 0 : 1;
                cbResolution.SelectedIndex = settings.Resolution == Resolution.FULLSCREEN ? 0 : 1;
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
