namespace ImgApp_2_WinForms
{
    partial class Chastot
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
            this.OriginalPictureBox = new System.Windows.Forms.PictureBox();
            this.Save = new System.Windows.Forms.Button();
            this.BackUp = new System.Windows.Forms.Button();
            this.openclick = new System.Windows.Forms.Button();
            this.Back = new System.Windows.Forms.Button();
            this.FourierPictureBox = new System.Windows.Forms.PictureBox();
            this.CenteringCheckBox = new System.Windows.Forms.CheckBox();
            this.FilterComboBox = new System.Windows.Forms.ComboBox();
            this.DFTButton = new System.Windows.Forms.Button();
            this.ApplyFilterButton = new System.Windows.Forms.Button();
            this.RadiusTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.DPFPower = new System.Windows.Forms.TextBox();
            this.Visual = new System.Windows.Forms.Button();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.OriginalPictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.FourierPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // OriginalPictureBox
            // 
            this.OriginalPictureBox.Location = new System.Drawing.Point(12, 59);
            this.OriginalPictureBox.Name = "OriginalPictureBox";
            this.OriginalPictureBox.Size = new System.Drawing.Size(512, 512);
            this.OriginalPictureBox.TabIndex = 20;
            this.OriginalPictureBox.TabStop = false;
            // 
            // Save
            // 
            this.Save.Location = new System.Drawing.Point(182, 12);
            this.Save.Name = "Save";
            this.Save.Size = new System.Drawing.Size(79, 23);
            this.Save.TabIndex = 19;
            this.Save.Text = "Сохранить";
            this.Save.UseVisualStyleBackColor = true;
            this.Save.Click += new System.EventHandler(this.Save_Click);
            // 
            // BackUp
            // 
            this.BackUp.Location = new System.Drawing.Point(97, 12);
            this.BackUp.Name = "BackUp";
            this.BackUp.Size = new System.Drawing.Size(79, 23);
            this.BackUp.TabIndex = 18;
            this.BackUp.Text = "Сброс";
            this.BackUp.UseVisualStyleBackColor = true;
            this.BackUp.Click += new System.EventHandler(this.BackUp_Click);
            // 
            // openclick
            // 
            this.openclick.Location = new System.Drawing.Point(12, 12);
            this.openclick.Name = "openclick";
            this.openclick.Size = new System.Drawing.Size(79, 23);
            this.openclick.TabIndex = 17;
            this.openclick.Text = "Открыть";
            this.openclick.UseVisualStyleBackColor = true;
            this.openclick.Click += new System.EventHandler(this.Open_Click);
            // 
            // Back
            // 
            this.Back.Location = new System.Drawing.Point(267, 12);
            this.Back.Name = "Back";
            this.Back.Size = new System.Drawing.Size(79, 23);
            this.Back.TabIndex = 16;
            this.Back.Text = "Обратно";
            this.Back.UseVisualStyleBackColor = true;
            this.Back.Click += new System.EventHandler(this.Back_Click);
            // 
            // FourierPictureBox
            // 
            this.FourierPictureBox.Location = new System.Drawing.Point(530, 59);
            this.FourierPictureBox.Name = "FourierPictureBox";
            this.FourierPictureBox.Size = new System.Drawing.Size(512, 512);
            this.FourierPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.FourierPictureBox.TabIndex = 21;
            this.FourierPictureBox.TabStop = false;
            // 
            // CenteringCheckBox
            // 
            this.CenteringCheckBox.AutoSize = true;
            this.CenteringCheckBox.Checked = true;
            this.CenteringCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.CenteringCheckBox.Enabled = false;
            this.CenteringCheckBox.Location = new System.Drawing.Point(1105, 554);
            this.CenteringCheckBox.Name = "CenteringCheckBox";
            this.CenteringCheckBox.Size = new System.Drawing.Size(105, 17);
            this.CenteringCheckBox.TabIndex = 22;
            this.CenteringCheckBox.Text = "Центрирование";
            this.CenteringCheckBox.UseVisualStyleBackColor = true;
            this.CenteringCheckBox.Visible = false;
            // 
            // FilterComboBox
            // 
            this.FilterComboBox.FormattingEnabled = true;
            this.FilterComboBox.Items.AddRange(new object[] {
            "Низкочастотная",
            "Высокочастотная",
            "Режекторный",
            "Полосовой",
            "Узкополосный полосовой",
            "Узкополосный режекторный"});
            this.FilterComboBox.Location = new System.Drawing.Point(1088, 134);
            this.FilterComboBox.Name = "FilterComboBox";
            this.FilterComboBox.Size = new System.Drawing.Size(141, 21);
            this.FilterComboBox.TabIndex = 23;
            this.FilterComboBox.Text = "Низкочастотная";
            this.FilterComboBox.Visible = false;
            this.FilterComboBox.SelectedIndexChanged += new System.EventHandler(this.FilterComboBox_SelectedIndexChanged);
            // 
            // DFTButton
            // 
            this.DFTButton.Location = new System.Drawing.Point(1088, 94);
            this.DFTButton.Name = "DFTButton";
            this.DFTButton.Size = new System.Drawing.Size(141, 34);
            this.DFTButton.TabIndex = 24;
            this.DFTButton.Text = "ДПФ";
            this.DFTButton.UseVisualStyleBackColor = true;
            this.DFTButton.Click += new System.EventHandler(this.DFTButton_Click);
            // 
            // ApplyFilterButton
            // 
            this.ApplyFilterButton.Location = new System.Drawing.Point(1088, 537);
            this.ApplyFilterButton.Name = "ApplyFilterButton";
            this.ApplyFilterButton.Size = new System.Drawing.Size(141, 34);
            this.ApplyFilterButton.TabIndex = 25;
            this.ApplyFilterButton.Text = "Выполнить";
            this.ApplyFilterButton.UseVisualStyleBackColor = true;
            this.ApplyFilterButton.Click += new System.EventHandler(this.ApplyFilterButton_Click);
            // 
            // RadiusTextBox
            // 
            this.RadiusTextBox.Location = new System.Drawing.Point(1088, 200);
            this.RadiusTextBox.Name = "RadiusTextBox";
            this.RadiusTextBox.Size = new System.Drawing.Size(141, 20);
            this.RadiusTextBox.TabIndex = 26;
            this.RadiusTextBox.Text = "0";
            this.RadiusTextBox.Visible = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(1085, 184);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 13);
            this.label1.TabIndex = 27;
            this.label1.Text = "Радиус фильтра";
            this.label1.Visible = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(14, 43);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 13);
            this.label2.TabIndex = 28;
            this.label2.Text = "Изображение";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(527, 43);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(74, 13);
            this.label3.TabIndex = 29;
            this.label3.Text = "Фурье-образ";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(1085, 62);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(94, 13);
            this.label4.TabIndex = 31;
            this.label4.Text = "Мощность ДПФ:";
            this.label4.Visible = false;
            // 
            // DPFPower
            // 
            this.DPFPower.Location = new System.Drawing.Point(1185, 55);
            this.DPFPower.Name = "DPFPower";
            this.DPFPower.Size = new System.Drawing.Size(44, 20);
            this.DPFPower.TabIndex = 32;
            this.DPFPower.Text = "1";
            this.DPFPower.Visible = false;
            // 
            // Visual
            // 
            this.Visual.Location = new System.Drawing.Point(1088, 497);
            this.Visual.Name = "Visual";
            this.Visual.Size = new System.Drawing.Size(141, 34);
            this.Visual.TabIndex = 33;
            this.Visual.Text = "Визуализировать";
            this.Visual.UseVisualStyleBackColor = true;
            this.Visual.Click += new System.EventHandler(this.Visual_Click);
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(1088, 248);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(141, 20);
            this.textBox2.TabIndex = 35;
            this.textBox2.Text = "0";
            this.textBox2.Visible = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(1085, 232);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(81, 13);
            this.label6.TabIndex = 37;
            this.label6.Text = "Второй радиус";
            this.label6.Visible = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(1085, 184);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(51, 13);
            this.label5.TabIndex = 38;
            this.label5.Text = "Радиусы";
            this.label5.Visible = false;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(1088, 200);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(141, 68);
            this.textBox1.TabIndex = 39;
            this.textBox1.Text = "20;37;10\r\n-20;37;10\r\n-20;-37;10\r\n20;-37;10";
            this.textBox1.Visible = false;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Checked = true;
            this.checkBox1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox1.Location = new System.Drawing.Point(352, 18);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(66, 17);
            this.checkBox1.TabIndex = 40;
            this.checkBox1.Text = "Фильтр";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.Visible = false;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // Chastot
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1264, 681);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.Visual);
            this.Controls.Add(this.DPFPower);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.RadiusTextBox);
            this.Controls.Add(this.ApplyFilterButton);
            this.Controls.Add(this.DFTButton);
            this.Controls.Add(this.FilterComboBox);
            this.Controls.Add(this.CenteringCheckBox);
            this.Controls.Add(this.FourierPictureBox);
            this.Controls.Add(this.OriginalPictureBox);
            this.Controls.Add(this.Save);
            this.Controls.Add(this.BackUp);
            this.Controls.Add(this.openclick);
            this.Controls.Add(this.Back);
            this.Name = "Chastot";
            this.Text = "Chastot";
            ((System.ComponentModel.ISupportInitialize)(this.OriginalPictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.FourierPictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox OriginalPictureBox;
        private System.Windows.Forms.Button Save;
        private System.Windows.Forms.Button BackUp;
        private System.Windows.Forms.Button openclick;
        private System.Windows.Forms.Button Back;
        private System.Windows.Forms.PictureBox FourierPictureBox;
        private System.Windows.Forms.CheckBox CenteringCheckBox;
        private System.Windows.Forms.ComboBox FilterComboBox;
        private System.Windows.Forms.Button DFTButton;
        private System.Windows.Forms.Button ApplyFilterButton;
        private System.Windows.Forms.TextBox RadiusTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox powerDPF;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox DPFPower;
        private System.Windows.Forms.Button Visual;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.CheckBox checkBox1;
    }
}