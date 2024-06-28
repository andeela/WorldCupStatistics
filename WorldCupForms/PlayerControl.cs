using DAL;
using DAL.Interfaces;
using DAL.Repos;
using DAL.Settings;
using System;
using System.Windows.Forms;


namespace WorldCupForms
{
    public class PlayerControl : UserControl
    {
        public static IPlayerIconRepo playerIconRepo = RepoFactory.GetPlayerIconRepo();

        public string PlayerName { get; private set; }
        public string Position { get; private set; }
        public int ShirtNumber { get; private set; }
        public bool IsCaptain { get; private set; }
        public bool IsFavourite { get; private set; }
        public string ImagePath { get; private set; }

        private PictureBox playerIcon;
        private Label playerInfo;
        private ContextMenuStrip contextMenu;

        private Panel favPanel;
        private Panel nonFavPanel;

        public PlayerControl(string playerName, string position, int shirtNumber, bool isCaptain, bool isFavourite, string imagePath, Panel favPanel, Panel nonFavPanel)
        {
            PlayerName = playerName;
            Position = position;
            ShirtNumber = shirtNumber;
            IsCaptain = isCaptain;
            IsFavourite = isFavourite;
            ImagePath = imagePath ?? @"..\..\..\..\DAL\Images\playerIcon.png";
            // todo: playerIconWoman for woman championship
            this.favPanel = favPanel;
            this.nonFavPanel = nonFavPanel;

            InitializeControl();
            InitializeContextMenu();
        }

        private void InitializeControl()
        {
            Width = 360;
            Height = 100;
            BorderStyle = BorderStyle.FixedSingle;
            Margin = new Padding(10);

            playerIcon = new PictureBox
            {
                ImageLocation = ImagePath,
                SizeMode = PictureBoxSizeMode.Zoom,
                Width = 60,
                Height = 60,
                Left = 10,
                Top = 25
            };
            
            playerInfo = new Label
            {
                Text = GetPlayerInfoText(),
                Left = 70,
                Top = 25,
                Width = Width - 80
            };

            Controls.Add(playerIcon);
            Controls.Add(playerInfo);

            MouseDown += PlayerControl_MouseDown;
        }

        private string GetPlayerInfoText()
        {
            return $"{ShirtNumber} - {PlayerName} ({Position})" +
                   (IsCaptain ? " (C)" : "") +
                   (IsFavourite ? " ★" : "");
        }

        private void InitializeContextMenu()
        {
            contextMenu = new ContextMenuStrip();
            var markFavouriteMenuItem = new ToolStripMenuItem("Mark as Favourite");
            markFavouriteMenuItem.Click += MarkFavouriteMenuItem_Click;
            contextMenu.Items.Add(markFavouriteMenuItem);

            var setImageMenuItem = new ToolStripMenuItem("Set Player Image");
            setImageMenuItem.Click += SetImageMenuItem_Click;
            contextMenu.Items.Add(setImageMenuItem);

            ContextMenuStrip = contextMenu;
        }

        private void MarkFavouriteMenuItem_Click(object sender, EventArgs e)
        {
            IsFavourite = !IsFavourite;
            playerInfo.Text = GetPlayerInfoText();

            UpdateContextMenuText();

            Panel currentPanel = this.Parent as Panel;
            currentPanel.Controls.Remove(this);

            if (IsFavourite)
            {
                favPanel.Controls.Add(this);
            }
            else
            {
                nonFavPanel.Controls.Add(this);
            }

            ReorderPlayerControls(favPanel);
            ReorderPlayerControls(nonFavPanel);
        }

        private void UpdateContextMenuText()
        {
            var menuItem = contextMenu.Items[0] as ToolStripItem;
            menuItem.Text = IsFavourite ? "Remove from favs" : "Mark as favorite";
        }

        private void SetImageMenuItem_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
               openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.bmp";
               if (openFileDialog.ShowDialog() == DialogResult.OK)
               {
                    ImagePath = openFileDialog.FileName;
                    playerIcon.ImageLocation = ImagePath;
                    SavePlayerIconPath();
               }
            }
        }

        private void SavePlayerIconPath()
        {
            playerIconRepo.SavePlayerIconPath(PlayerName, ImagePath);
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

        private void PlayerControl_MouseDown(object sender, MouseEventArgs e)
        {
            DoDragDrop(this, DragDropEffects.Move);
        }

        public void SetFavourite(bool isFavourite)
        {
            IsFavourite = isFavourite;
            playerInfo.Text = GetPlayerInfoText();
        }
    }
}
