using System.Windows.Forms;

namespace ImgApp_2_WinForms
{
    partial class Gradac
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
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.button1 = new System.Windows.Forms.Button();
            this.Back = new System.Windows.Forms.Button();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.Open = new System.Windows.Forms.Button();
            this.Save = new System.Windows.Forms.Button();
            this.Grafic = new System.Windows.Forms.PictureBox();
            this.Histogtam = new System.Windows.Forms.PictureBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Grafic)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Histogtam)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(978, 78);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(900, 923);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(18, 18);
            this.button1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(112, 35);
            this.button1.TabIndex = 1;
            this.button1.Text = "Сброс";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Back
            // 
            this.Back.Location = new System.Drawing.Point(140, 18);
            this.Back.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Back.Name = "Back";
            this.Back.Size = new System.Drawing.Size(112, 35);
            this.Back.TabIndex = 2;
            this.Back.Text = "Обратно";
            this.Back.UseVisualStyleBackColor = true;
            this.Back.Click += new System.EventHandler(this.Back_Click);
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "Кубическая интерполяция",
            "Линейная интерполяция"});
            this.comboBox1.Location = new System.Drawing.Point(504, 22);
            this.comboBox1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(236, 28);
            this.comboBox1.TabIndex = 4;
            // 
            // Open
            // 
            this.Open.Location = new System.Drawing.Point(261, 18);
            this.Open.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Open.Name = "Open";
            this.Open.Size = new System.Drawing.Size(112, 35);
            this.Open.TabIndex = 5;
            this.Open.Text = "Открыть";
            this.Open.UseVisualStyleBackColor = true;
            this.Open.Click += new System.EventHandler(this.Open_Click);
            // 
            // Save
            // 
            this.Save.Location = new System.Drawing.Point(382, 18);
            this.Save.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Save.Name = "Save";
            this.Save.Size = new System.Drawing.Size(112, 35);
            this.Save.TabIndex = 6;
            this.Save.Text = "Сохранить";
            this.Save.UseVisualStyleBackColor = true;
            this.Save.Click += new System.EventHandler(this.Save_Click);
            // 
            // Grafic
            // 
            this.Grafic.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Grafic.Location = new System.Drawing.Point(9, 78);
            this.Grafic.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Grafic.Name = "Grafic";
            this.Grafic.Size = new System.Drawing.Size(959, 614);
            this.Grafic.TabIndex = 7;
            this.Grafic.TabStop = false;
            this.Grafic.SizeChanged += new System.EventHandler(this.pictureBoxGraph_SizeChanged);
            this.Grafic.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBoxGraph_Paint);
            this.Grafic.DoubleClick += new System.EventHandler(this.pictureBoxGraph_DoubleClick);
            this.Grafic.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBoxGraph_MouseDown);
            this.Grafic.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBoxGraph_MouseMove);
            this.Grafic.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBoxGraph_MouseUp);
            // 
            // Histogtam
            // 
            this.Histogtam.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Histogtam.Location = new System.Drawing.Point(9, 694);
            this.Histogtam.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Histogtam.Name = "Histogtam";
            this.Histogtam.Size = new System.Drawing.Size(959, 307);
            this.Histogtam.TabIndex = 8;
            this.Histogtam.TabStop = false;
            this.Histogtam.Paint += new System.Windows.Forms.PaintEventHandler(this.Histogtam_Paint);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(832, 23);
            this.textBox1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(36, 26);
            this.textBox1.TabIndex = 9;
            // 
            // Gradac
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1896, 1048);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.Histogtam);
            this.Controls.Add(this.Grafic);
            this.Controls.Add(this.Save);
            this.Controls.Add(this.Open);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.Back);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.pictureBox1);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "Gradac";
            this.Text = "Gradac";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Grafic)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Histogtam)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button Back;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Button Open;
        private System.Windows.Forms.Button Save;
        private System.Windows.Forms.PictureBox Grafic;
        private System.Windows.Forms.PictureBox Histogtam;
        private TextBox textBox1;
    }
}