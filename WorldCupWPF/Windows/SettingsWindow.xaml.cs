using DAL.Interfaces;
using DAL.Model.Enums;
using DAL.Repos;
using DAL.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace WorldCupWPF
{
    public partial class SettingsWindow : Window
    {
        public static ISettingsRepo settingsRepo = RepoFactory.GetSettingsRepo();
        private AppSettings currentSettings;

        public event EventHandler SettingsApplied;

        public SettingsWindow()
        {
            InitializeComponent();
            LoadSettingsAsync();
        }

        private async void LoadSettingsAsync()
        {
            try
            {
                currentSettings = await settingsRepo.GetSettingsAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading settings: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async void OnApplyButtonClick(object sender, RoutedEventArgs e)
        {
            try
            {
                currentSettings.GenderCategory = cbGenderCategory.SelectedIndex == 0 ? GenderCategory.MEN : GenderCategory.WOMEN;
                currentSettings.Language = cbLanguage.SelectedIndex == 0 ? DAL.Model.Enums.Language.ENGLISH : DAL.Model.Enums.Language.CROATIAN;

                if (cbResolution.SelectedValue != null && Enum.TryParse(cbResolution.SelectedValue.ToString(), out Resolution resolutionEnum))
                {
                    switch (resolutionEnum)
                    {
                        case Resolution.FULLSCREEN:
                            SetFullScreen(true);
                            break;
                        case Resolution.r1280x720:
                            SetWindowSize(1280, 720);
                            break;
                        case Resolution.r1024x768:
                            SetWindowSize(1024, 768);
                            break;
                        default:
                            SetFullScreen(false);
                            break;
                    }

                    currentSettings.Resolution = resolutionEnum;
                }
                await settingsRepo.UpdateSettingsAsync(currentSettings);

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
