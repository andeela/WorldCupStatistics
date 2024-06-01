namespace WorldCupForms
{
    partial class StartingForm
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
            cbGenderCategory = new ComboBox();
            cbLanguage = new ComboBox();
            lbChooseLang = new Label();
            lbChooseChamp = new Label();
            bttnNext = new Button();
            lbWorlCup = new Label();
            SuspendLayout();
            // 
            // cbGenderCategory
            // 
            cbGenderCategory.FormattingEnabled = true;
            cbGenderCategory.Items.AddRange(new object[] { "Men 2018", "Women 2019" });
            cbGenderCategory.Location = new Point(292, 159);
            cbGenderCategory.Name = "cbGenderCategory";
            cbGenderCategory.Size = new Size(183, 28);
            cbGenderCategory.TabIndex = 0;
            cbGenderCategory.SelectedIndexChanged += cbGenderCategory_SelectedIndexChanged;
            // 
            // cbLanguage
            // 
            cbLanguage.FormattingEnabled = true;
            cbLanguage.Items.AddRange(new object[] { "English", "Croatian (hrvatski)" });
            cbLanguage.Location = new Point(292, 237);
            cbLanguage.Name = "cbLanguage";
            cbLanguage.Size = new Size(183, 28);
            cbLanguage.TabIndex = 1;
            cbLanguage.SelectedIndexChanged += cbLanguage_SelectedIndexChanged;
            // 
            // lbChooseLang
            // 
            lbChooseLang.AutoSize = true;
            lbChooseLang.ForeColor = SystemColors.Control;
            lbChooseLang.Location = new Point(292, 201);
            lbChooseLang.Name = "lbChooseLang";
            lbChooseLang.Size = new Size(124, 20);
            lbChooseLang.TabIndex = 2;
            lbChooseLang.Text = "Choose language";
            // 
            // lbChooseChamp
            // 
            lbChooseChamp.AutoSize = true;
            lbChooseChamp.ForeColor = SystemColors.Control;
            lbChooseChamp.Location = new Point(292, 125);
            lbChooseChamp.Name = "lbChooseChamp";
            lbChooseChamp.Size = new Size(157, 20);
            lbChooseChamp.TabIndex = 3;
            lbChooseChamp.Text = "Choose Championship";
            // 
            // bttnNext
            // 
            bttnNext.Location = new Point(346, 312);
            bttnNext.Name = "bttnNext";
            bttnNext.Size = new Size(70, 38);
            bttnNext.TabIndex = 4;
            bttnNext.Text = "NEXT";
            bttnNext.UseVisualStyleBackColor = true;
            bttnNext.Click += bttnNext_Click;
            // 
            // lbWorlCup
            // 
            lbWorlCup.AutoSize = true;
            lbWorlCup.Font = new Font("Impact", 19.8000011F, FontStyle.Regular, GraphicsUnit.Point);
            lbWorlCup.ForeColor = Color.White;
            lbWorlCup.Location = new Point(166, 55);
            lbWorlCup.Name = "lbWorlCup";
            lbWorlCup.Size = new Size(478, 42);
            lbWorlCup.TabIndex = 5;
            lbWorlCup.Text = "Welcome to World Cup Statistics!";
            // 
            // StartingForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ActiveCaptionText;
            ClientSize = new Size(782, 453);
            Controls.Add(lbWorlCup);
            Controls.Add(bttnNext);
            Controls.Add(lbChooseChamp);
            Controls.Add(lbChooseLang);
            Controls.Add(cbLanguage);
            Controls.Add(cbGenderCategory);
            Name = "StartingForm";
            Text = "StartingForm";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ComboBox cbGenderCategory;
        private ComboBox cbLanguage;
        private Label lbChooseLang;
        private Label lbChooseChamp;
        private Button bttnNext;
        private Label lbWorlCup;
    }
}