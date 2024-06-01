namespace WorldCupForms
{
    partial class SettingsForm
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
            lbChangeSettings = new Label();
            lbChooseChamp = new Label();
            cbGenderCategory = new ComboBox();
            lbChooseLang = new Label();
            cbLanguage = new ComboBox();
            btnApplyChanges = new Button();
            SuspendLayout();
            // 
            // lbChangeSettings
            // 
            lbChangeSettings.AutoSize = true;
            lbChangeSettings.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point);
            lbChangeSettings.ForeColor = Color.White;
            lbChangeSettings.Location = new Point(278, 81);
            lbChangeSettings.Name = "lbChangeSettings";
            lbChangeSettings.Size = new Size(272, 31);
            lbChangeSettings.TabIndex = 0;
            lbChangeSettings.Text = "Wanna change settings?";
            // 
            // lbChooseChamp
            // 
            lbChooseChamp.AutoSize = true;
            lbChooseChamp.ForeColor = SystemColors.Control;
            lbChooseChamp.Location = new Point(315, 140);
            lbChooseChamp.Name = "lbChooseChamp";
            lbChooseChamp.Size = new Size(157, 20);
            lbChooseChamp.TabIndex = 4;
            lbChooseChamp.Text = "Choose Championship";
            // 
            // cbGenderCategory
            // 
            cbGenderCategory.FormattingEnabled = true;
            cbGenderCategory.Items.AddRange(new object[] { "Men 2018", "Women 2019" });
            cbGenderCategory.Location = new Point(315, 172);
            cbGenderCategory.Name = "cbGenderCategory";
            cbGenderCategory.Size = new Size(183, 28);
            cbGenderCategory.TabIndex = 5;
            // 
            // lbChooseLang
            // 
            lbChooseLang.AutoSize = true;
            lbChooseLang.ForeColor = SystemColors.Control;
            lbChooseLang.Location = new Point(315, 222);
            lbChooseLang.Name = "lbChooseLang";
            lbChooseLang.Size = new Size(124, 20);
            lbChooseLang.TabIndex = 6;
            lbChooseLang.Text = "Choose language";
            // 
            // cbLanguage
            // 
            cbLanguage.FormattingEnabled = true;
            cbLanguage.Items.AddRange(new object[] { "English", "Croatian (hrvatski)" });
            cbLanguage.Location = new Point(315, 259);
            cbLanguage.Name = "cbLanguage";
            cbLanguage.Size = new Size(183, 28);
            cbLanguage.TabIndex = 7;
            // 
            // btnApplyChanges
            // 
            btnApplyChanges.Location = new Point(360, 331);
            btnApplyChanges.Name = "btnApplyChanges";
            btnApplyChanges.Size = new Size(94, 29);
            btnApplyChanges.TabIndex = 8;
            btnApplyChanges.Text = "DONE";
            btnApplyChanges.UseVisualStyleBackColor = true;
            // 
            // SettingsForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Black;
            ClientSize = new Size(800, 450);
            Controls.Add(btnApplyChanges);
            Controls.Add(cbLanguage);
            Controls.Add(lbChooseLang);
            Controls.Add(cbGenderCategory);
            Controls.Add(lbChooseChamp);
            Controls.Add(lbChangeSettings);
            Name = "SettingsForm";
            Text = "SettingsForm";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lbChangeSettings;
        private Label lbChooseChamp;
        private ComboBox cbGenderCategory;
        private Label lbChooseLang;
        private ComboBox cbLanguage;
        private Button btnApplyChanges;
    }
}