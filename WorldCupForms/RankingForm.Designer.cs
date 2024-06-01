namespace WorldCupForms
{
    partial class RankingForm
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
            cbRankBy = new ComboBox();
            lblRankBy = new Label();
            lbRankings = new ListBox();
            btnPDF = new Button();
            btnGenerateRanks = new Button();
            SuspendLayout();
            // 
            // cbRankBy
            // 
            cbRankBy.FormattingEnabled = true;
            cbRankBy.Location = new Point(18, 41);
            cbRankBy.Name = "cbRankBy";
            cbRankBy.Size = new Size(151, 28);
            cbRankBy.TabIndex = 0;
            // 
            // lblRankBy
            // 
            lblRankBy.AutoSize = true;
            lblRankBy.Location = new Point(18, 9);
            lblRankBy.Name = "lblRankBy";
            lblRankBy.Size = new Size(64, 20);
            lblRankBy.TabIndex = 1;
            lblRankBy.Text = "Rank by:";
            // 
            // lbRankings
            // 
            lbRankings.FormattingEnabled = true;
            lbRankings.ItemHeight = 20;
            lbRankings.Location = new Point(20, 80);
            lbRankings.Name = "lbRankings";
            lbRankings.Size = new Size(768, 344);
            lbRankings.TabIndex = 2;
            // 
            // btnPDF
            // 
            btnPDF.Location = new Point(635, 45);
            btnPDF.Name = "btnPDF";
            btnPDF.Size = new Size(153, 29);
            btnPDF.TabIndex = 3;
            btnPDF.Text = "Export to PDF";
            btnPDF.UseVisualStyleBackColor = true;
            btnPDF.Click += btnPDF_Click;
            // 
            // btnGenerateRanks
            // 
            btnGenerateRanks.Location = new Point(175, 40);
            btnGenerateRanks.Name = "btnGenerateRanks";
            btnGenerateRanks.Size = new Size(79, 29);
            btnGenerateRanks.TabIndex = 4;
            btnGenerateRanks.Text = "Generate";
            btnGenerateRanks.UseVisualStyleBackColor = true;
            btnGenerateRanks.Click += btnGenerateRanks_Click;
            // 
            // RankingForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(btnGenerateRanks);
            Controls.Add(btnPDF);
            Controls.Add(lbRankings);
            Controls.Add(lblRankBy);
            Controls.Add(cbRankBy);
            Name = "RankingForm";
            Text = "RankingForm";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ComboBox cbRankBy;
        private Label lblRankBy;
        private ListBox lbRankings;
        private Button btnPDF;
        private Button btnGenerateRanks;
    }
}