using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using DAL.Model;
using DAL.Repos;

namespace WorldCupWPF
{
    public partial class PlayerControlWPF : UserControl
    {
        private const string DEFAULT_ICON = "/Images/playerIcon.png";
        private PlayerWindow currentPlayerWindow;

        public static readonly DependencyProperty PlayerNameProperty =
            DependencyProperty.Register("PlayerName", typeof(string), typeof(PlayerControlWPF), new PropertyMetadata(""));

        public static readonly DependencyProperty ShirtNumberProperty =
            DependencyProperty.Register("ShirtNumber", typeof(int), typeof(PlayerControlWPF), new PropertyMetadata(0));

        public static readonly DependencyProperty ImagePathProperty =
            DependencyProperty.Register("ImagePath", typeof(string), typeof(PlayerControlWPF), new PropertyMetadata(DEFAULT_ICON, OnImagePathChanged));

        public static readonly DependencyProperty CountryProperty =
            DependencyProperty.Register("Country", typeof(NationalTeam), typeof(PlayerControlWPF), new PropertyMetadata(null));

        public string PlayerName
        {
            get { return (string)GetValue(PlayerNameProperty); }
            set { SetValue(PlayerNameProperty, value); }
        }

        public int ShirtNumber
        {
            get { return (int)GetValue(ShirtNumberProperty); }
            set { SetValue(ShirtNumberProperty, value); }
        }

        public string ImagePath
        {
            get { return (string)GetValue(ImagePathProperty); }
            set { SetValue(ImagePathProperty, value); }
        }

        public NationalTeam Country
        {
            get { return (NationalTeam)GetValue(CountryProperty); }
            set { SetValue(CountryProperty, value); }
        }

        public PlayerControlWPF()
        {
            InitializeComponent();
            DataContext = this;
            MouseLeftButtonUp += OnPlayerSelected;
        }

        public PlayerControlWPF(string playerName, int shirtNumber, NationalTeam country)
            : this()
        {
            PlayerName = playerName;
            ShirtNumber = shirtNumber;
            Country = country;
        }

        private async void OnPlayerSelected(object sender, MouseButtonEventArgs e)
        {
            try
            {
                var parentWindow = Window.GetWindow(this);
                if (parentWindow != null)
                {
                    var playerData = await DataFactory.GetPlayerDataForSelectedCountryAsync(Country.Country);

                    if (playerData.TryGetValue(PlayerName, out var ranking))
                    {
                        if (currentPlayerWindow == null || currentPlayerWindow.tbPlayerName != tbPlayerName)
                        {
                            OpenPlayerDetailWindow(ranking);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error handling player selection: {ex.Message}");
            }
        }

        private void OpenPlayerDetailWindow(PlayerRanking ranking)
        {
            currentPlayerWindow?.Close(); // Close existing window if open

            currentPlayerWindow = new PlayerWindow(PlayerName, ShirtNumber, ImagePath, ranking.Goals, ranking.YellowCards);
            currentPlayerWindow.Closed += PlayerDetailsWindow_Closed; // Handle window closed event
            currentPlayerWindow.ShowDialog();
        }

        private void PlayerDetailsWindow_Closed(object sender, EventArgs e)
        {
            currentPlayerWindow.Closed -= PlayerDetailsWindow_Closed; // Remove event handler
            currentPlayerWindow = null; // Clear reference to current window
        }

        private static void OnImagePathChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is PlayerControlWPF playerControl)
            {
                try
                {
                    var imagePath = e.NewValue as string;
                    if (string.IsNullOrEmpty(imagePath))
                    {
                        imagePath = DEFAULT_ICON;
                    }
                    playerControl.imgPlayer.Source = new BitmapImage(new Uri(imagePath, UriKind.RelativeOrAbsolute));
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Error loading image: {ex.Message}");
                    playerControl.imgPlayer.Source = new BitmapImage(new Uri(DEFAULT_ICON, UriKind.RelativeOrAbsolute));
                }
            }
        }
    }
}
