using DAL.Interfaces;
using DAL.Model.Enums;
using DAL.Repos;
using DAL.Settings;
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
using System.Windows.Shapes;
using WorldCupWPF.Views;

namespace WorldCupWPF
{
    /// <summary>
    /// Interaction logic for SettingsWindow.xaml
    /// </summary>
    public partial class SettingsWindow : Window
    {
        private ISettingsRepo settingsRepo;
        private AppSettings currentSettings;

        public event EventHandler SettingsApplied;

        public SettingsWindow()
        {
            InitializeComponent();

            settingsRepo = new AppSettingsRepo();
            LoadSettingsAsync();
        }

        private async void LoadSettingsAsync()
        {
            currentSettings = await settingsRepo.GetSettingsAsync();

            cbGenderCategory.SelectedIndex = currentSettings.GenderCategory == GenderCategory.MEN ? 0 : 1;
            cbLanguage.SelectedIndex = currentSettings.Language == DAL.Model.Enums.Language.ENGLISH ? 0 : 1;
        }

        private async void OnApplyButtonClick(object sender, RoutedEventArgs e)
        {
            try
            {
                bool genderCategoryChanged = currentSettings.GenderCategory != (cbGenderCategory.SelectedIndex == 0 ? GenderCategory.MEN : GenderCategory.WOMEN);

                currentSettings.GenderCategory = cbGenderCategory.SelectedIndex == 0 ? GenderCategory.MEN : GenderCategory.WOMEN;
                currentSettings.Language = cbLanguage.SelectedIndex == 0 ? DAL.Model.Enums.Language.ENGLISH : DAL.Model.Enums.Language.CROATIAN;

                await settingsRepo.UpdateSettingsAsync(currentSettings);

                string selectedResolution = (cbResolution.SelectedItem as ComboBoxItem)?.Content.ToString();

                switch (selectedResolution)
                {
                    case "1280x720":
                        SetWindowSize(1280, 720);
                        break;
                    case "1024x768":
                        SetWindowSize(1024, 768);
                        break;
                    case "FULLSCREEN":
                        SetFullScreen(true);
                        break;
                    default:
                        SetFullScreen(false);
                        break;
                }

                MessageBox.Show("Settings applied successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

                SettingsApplied?.Invoke(this, EventArgs.Empty);

                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
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
    }
}

