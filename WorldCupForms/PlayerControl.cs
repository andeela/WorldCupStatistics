using DAL;
using DAL.Settings;
using System;
using System.Windows.Forms;

namespace WorldCupForms
{
    public class PlayerControl : UserControl
    {
        public string PlayerName { get; private set; }
        public string Position { get; private set; }
        public int ShirtNumber { get; private set; }
        public bool IsCaptain { get; private set; }
        public bool IsFavourite { get; private set; }

        private PictureBox playerIcon;
        private Label playerInfo;
        private ContextMenuStrip contextMenu;

        public PlayerControl(string playerName, string position, int shirtNumber, bool isCaptain, bool isFavourite)
        {
            PlayerName = playerName;
            Position = position;
            ShirtNumber = shirtNumber;
            IsCaptain = isCaptain;
            IsFavourite = isFavourite;

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
                ImageLocation = @"..\..\..\..\DAL\Images\playerIcon.png",
                // prominit u zensku ikonicu za zensko prvenstvo
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

            ContextMenuStrip = contextMenu;
        }

        private void MarkFavouriteMenuItem_Click(object sender, EventArgs e)
        {
            IsFavourite = !IsFavourite;
            playerInfo.Text = GetPlayerInfoText();
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
