using DAL.Repos;
using Microsoft.VisualBasic.ApplicationServices;
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

namespace WorldCupWPF
{
    /// <summary>
    /// Interaction logic for PlayerControlWPF.xaml
    /// </summary>
    
    public partial class PlayerControlWPF : UserControl
    {
        private const string DEFAULT_ICON = "C:/Users/anđela/source/repos/OOP.NET/WorldCupStatistics/DAL/Images/jersey.png"; // change to relative and figure out why is it working only when path's absolute

        public static readonly DependencyProperty PlayerNameProperty =
            DependencyProperty.Register("PlayerName", typeof(string), typeof(PlayerControlWPF), new PropertyMetadata(""));

        public static readonly DependencyProperty ShirtNumberProperty =
            DependencyProperty.Register("ShirtNumber", typeof(int), typeof(PlayerControlWPF), new PropertyMetadata(0));

        public static readonly DependencyProperty ImagePathProperty =
            DependencyProperty.Register("ImagePath", typeof(string), typeof(PlayerControlWPF), new PropertyMetadata(DEFAULT_ICON, OnImagePathChanged));

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

        public PlayerControlWPF()
        {
            InitializeComponent();
            DataContext = this; 
        }

        public PlayerControlWPF(string playerName, int shirtNumber)
            : this()
        {
            PlayerName = playerName;
            ShirtNumber = shirtNumber;
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
                    Debug.WriteLine($"error loading image: {ex.Message}");
                    playerControl.imgPlayer.Source = new BitmapImage(new Uri(DEFAULT_ICON, UriKind.RelativeOrAbsolute));
                }
            }
        }
    }

}

