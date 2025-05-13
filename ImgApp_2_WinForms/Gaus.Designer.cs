namespace ImgApp_2_WinForms
{
    partial class Gaus
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
            this.components = new System.ComponentModel.Container();
            this.Save = new System.Windows.Forms.Button();
            this.BackUp = new System.Windows.Forms.Button();
            this.openclick = new System.Windows.Forms.Button();
            this.Back = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.XMatrix = new System.Windows.Forms.TextBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.YMatrix = new System.Windows.Forms.TextBox();
            this.X = new System.Windows.Forms.Label();
            this.Y = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.Filtr = new System.Windows.Forms.Button();
            this.RGauss = new System.Windows.Forms.CheckBox();
            this.contextMenuStrip2 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.sig = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // Save
            // 
            this.Save.Location = new System.Drawing.Point(1073, 23);
            this.Save.Name = "Save";
            this.Save.Size = new System.Drawing.Size(79, 23);
            this.Save.TabIndex = 14;
            this.Save.Text = "Сохранить";
            this.Save.UseVisualStyleBackColor = true;
            this.Save.Click += new System.EventHandler(this.Save_Click);
            // 
            // BackUp
            // 
            this.BackUp.Location = new System.Drawing.Point(988, 23);
            this.BackUp.Name = "BackUp";
            this.BackUp.Size = new System.Drawing.Size(79, 23);
            this.BackUp.TabIndex = 13;
            this.BackUp.Text = "Сброс";
            this.BackUp.UseVisualStyleBackColor = true;
            this.BackUp.Click += new System.EventHandler(this.BackUp_Click);
            // 
            // openclick
            // 
            this.openclick.Location = new System.Drawing.Point(903, 23);
            this.openclick.Name = "openclick";
            this.openclick.Size = new System.Drawing.Size(79, 23);
            this.openclick.TabIndex = 12;
            this.openclick.Text = "Открыть";
            this.openclick.UseVisualStyleBackColor = true;
            this.openclick.Click += new System.EventHandler(this.Open_Click);
            // 
            // Back
            // 
            this.Back.Location = new System.Drawing.Point(1158, 23);
            this.Back.Name = "Back";
            this.Back.Size = new System.Drawing.Size(79, 23);
            this.Back.TabIndex = 11;
            this.Back.Text = "Обратно";
            this.Back.UseVisualStyleBackColor = true;
            this.Back.Click += new System.EventHandler(this.Back_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(12, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(885, 657);
            this.pictureBox1.TabIndex = 15;
            this.pictureBox1.TabStop = false;
            // 
            // XMatrix
            // 
            this.XMatrix.Location = new System.Drawing.Point(1058, 186);
            this.XMatrix.Name = "XMatrix";
            this.XMatrix.Size = new System.Drawing.Size(37, 20);
            this.XMatrix.TabIndex = 16;
            this.XMatrix.Text = "3";
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // YMatrix
            // 
            this.YMatrix.Location = new System.Drawing.Point(1058, 212);
            this.YMatrix.Name = "YMatrix";
            this.YMatrix.Size = new System.Drawing.Size(37, 20);
            this.YMatrix.TabIndex = 18;
            this.YMatrix.Text = "3";
            // 
            // X
            // 
            this.X.AutoSize = true;
            this.X.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.X.Location = new System.Drawing.Point(899, 184);
            this.X.Name = "X";
            this.X.Size = new System.Drawing.Size(137, 20);
            this.X.TabIndex = 19;
            this.X.Text = "Высота матрицы";
            // 
            // Y
            // 
            this.Y.AutoSize = true;
            this.Y.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.Y.Location = new System.Drawing.Point(899, 210);
            this.Y.Name = "Y";
            this.Y.Size = new System.Drawing.Size(138, 20);
            this.Y.TabIndex = 20;
            this.Y.Text = "Длинна матрицы";
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "Линейная фильтрация",
            "Медианная фильтрация"});
            this.comboBox1.Location = new System.Drawing.Point(988, 68);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(164, 21);
            this.comboBox1.TabIndex = 21;
            this.comboBox1.Text = "Линейная фильтрация";
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.change);
            // 
            // Filtr
            // 
            this.Filtr.Location = new System.Drawing.Point(988, 272);
            this.Filtr.Name = "Filtr";
            this.Filtr.Size = new System.Drawing.Size(164, 38);
            this.Filtr.TabIndex = 22;
            this.Filtr.Text = "Фильтровать";
            this.Filtr.UseVisualStyleBackColor = true;
            this.Filtr.Click += new System.EventHandler(this.Filtr_Click);
            // 
            // RGauss
            // 
            this.RGauss.AutoSize = true;
            this.RGauss.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.RGauss.Location = new System.Drawing.Point(903, 131);
            this.RGauss.Name = "RGauss";
            this.RGauss.Size = new System.Drawing.Size(178, 24);
            this.RGauss.TabIndex = 23;
            this.RGauss.Text = "Размытие по Гауссу";
            this.RGauss.UseVisualStyleBackColor = true;
            this.RGauss.CheckedChanged += new System.EventHandler(this.RGauss_CheckedChanged);
            // 
            // contextMenuStrip2
            // 
            this.contextMenuStrip2.Name = "contextMenuStrip2";
            this.contextMenuStrip2.Size = new System.Drawing.Size(61, 4);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.label1.Location = new System.Drawing.Point(1136, 132);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 20);
            this.label1.TabIndex = 26;
            this.label1.Text = "Sigma:";
            // 
            // sig
            // 
            this.sig.Enabled = false;
            this.sig.Location = new System.Drawing.Point(1200, 134);
            this.sig.Name = "sig";
            this.sig.Size = new System.Drawing.Size(37, 20);
            this.sig.TabIndex = 25;
            this.sig.Text = "3";
            // 
            // Gaus
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1264, 681);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.sig);
            this.Controls.Add(this.RGauss);
            this.Controls.Add(this.Filtr);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.Y);
            this.Controls.Add(this.X);
            this.Controls.Add(this.YMatrix);
            this.Controls.Add(this.XMatrix);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.Save);
            this.Controls.Add(this.BackUp);
            this.Controls.Add(this.openclick);
            this.Controls.Add(this.Back);
            this.Name = "Gaus";
            this.Text = "Gaus";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Save;
        private System.Windows.Forms.Button BackUp;
        private System.Windows.Forms.Button openclick;
        private System.Windows.Forms.Button Back;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TextBox XMatrix;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.TextBox YMatrix;
        private System.Windows.Forms.Label X;
        private System.Windows.Forms.Label Y;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Button Filtr;
        private System.Windows.Forms.CheckBox RGauss;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox sig;
    }
}