namespace WorldCupForms
{
    partial class FavTeamForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            lbChooseTeam = new Label();
            cbFavTeam = new ComboBox();
            lbDropFavPlayers = new Label();
            pnlFavTeam = new Panel();
            sbScrollFavTeam = new HScrollBar();
            pnlFavPlayers = new Panel();
            btnRanks = new Button();
            btnSettings = new Button();
            pnlFavTeam.SuspendLayout();
            SuspendLayout();
            // 
            // lbChooseTeam
            // 
            lbChooseTeam.AutoSize = true;
            lbChooseTeam.Location = new Point(12, 9);
            lbChooseTeam.Name = "lbChooseTeam";
            lbChooseTeam.Size = new Size(192, 20);
            lbChooseTeam.TabIndex = 0;
            lbChooseTeam.Text = "Choose your favourite team";
            // 
            // cbFavTeam
            // 
            cbFavTeam.FormattingEnabled = true;
            cbFavTeam.Location = new Point(12, 43);
            cbFavTeam.Name = "cbFavTeam";
            cbFavTeam.Size = new Size(151, 28);
            cbFavTeam.TabIndex = 1;
            cbFavTeam.SelectedIndexChanged += cbFavTeam_SelectedIndexChanged;
            // 
            // lbDropFavPlayers
            // 
            lbDropFavPlayers.AutoSize = true;
            lbDropFavPlayers.Location = new Point(412, 16);
            lbDropFavPlayers.Name = "lbDropFavPlayers";
            lbDropFavPlayers.Size = new Size(227, 20);
            lbDropFavPlayers.TabIndex = 2;
            lbDropFavPlayers.Text = "Drop your favourite players here!";
            // 
            // pnlFavTeam
            // 
            pnlFavTeam.AutoScroll = true;
            pnlFavTeam.Controls.Add(sbScrollFavTeam);
            pnlFavTeam.Location = new Point(12, 83);
            pnlFavTeam.Name = "pnlFavTeam";
            pnlFavTeam.Size = new Size(394, 355);
            pnlFavTeam.TabIndex = 3;
            // 
            // sbScrollFavTeam
            // 
            sbScrollFavTeam.Location = new Point(405, 5);
            sbScrollFavTeam.Name = "sbScrollFavTeam";
            sbScrollFavTeam.Size = new Size(18, 352);
            sbScrollFavTeam.TabIndex = 0;
            // 
            // pnlFavPlayers
            // 
            pnlFavPlayers.AllowDrop = true;
            pnlFavPlayers.Location = new Point(412, 47);
            pnlFavPlayers.Name = "pnlFavPlayers";
            pnlFavPlayers.Size = new Size(376, 393);
            pnlFavPlayers.TabIndex = 4;
            // 
            // btnRanks
            // 
            btnRanks.Location = new Point(264, 42);
            btnRanks.Name = "btnRanks";
            btnRanks.Size = new Size(122, 29);
            btnRanks.TabIndex = 5;
            btnRanks.Text = "Rank things!";
            btnRanks.UseVisualStyleBackColor = true;
            btnRanks.Click += btnRanks_Click;
            // 
            // btnSettings
            // 
            btnSettings.Location = new Point(704, 13);
            btnSettings.Name = "btnSettings";
            btnSettings.Size = new Size(84, 27);
            btnSettings.TabIndex = 7;
            btnSettings.Text = "Settings";
            btnSettings.TextAlign = ContentAlignment.TopCenter;
            btnSettings.UseVisualStyleBackColor = true;
            btnSettings.Click += btnSettings_Click;
            // 
            // FavTeamForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(btnSettings);
            Controls.Add(btnRanks);
            Controls.Add(pnlFavPlayers);
            Controls.Add(pnlFavTeam);
            Controls.Add(lbDropFavPlayers);
            Controls.Add(cbFavTeam);
            Controls.Add(lbChooseTeam);
            Name = "FavTeamForm";
            Text = "FavTeamForm";
            pnlFavTeam.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lbChooseTeam;
        private ComboBox cbFavTeam;
        private Label lbDropFavPlayers;
        private Panel pnlFavTeam;
        private Panel pnlFavPlayers;
        private Button btnRanks;
        private HScrollBar sbScrollFavTeam;
        private Button btnSettings;
    }
}