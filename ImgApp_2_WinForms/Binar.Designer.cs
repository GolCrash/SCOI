namespace ImgApp_2_WinForms
{
    partial class Binar
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
            this.Otsu = new System.Windows.Forms.Button();
            this.Nibelek = new System.Windows.Forms.Button();
            this.Sauwol = new System.Windows.Forms.Button();
            this.Wolf = new System.Windows.Forms.Button();
            this.BredLy_Rot = new System.Windows.Forms.Button();
            this.Back = new System.Windows.Forms.Button();
            this.Щ = new System.Windows.Forms.Button();
            this.BackUp = new System.Windows.Forms.Button();
            this.Save = new System.Windows.Forms.Button();
            this.kritt = new System.Windows.Forms.TextBox();
            this.krita = new System.Windows.Forms.TextBox();
            this.kritR = new System.Windows.Forms.TextBox();
            this.kritk = new System.Windows.Forms.TextBox();
            this.WidowSize = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(12, 30);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(980, 620);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(1001, 156);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(163, 65);
            this.button1.TabIndex = 1;
            this.button1.Text = "Метод Гаврилова";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.Gavrilov_Click);
            // 
            // Otsu
            // 
            this.Otsu.Location = new System.Drawing.Point(1172, 156);
            this.Otsu.Name = "Otsu";
            this.Otsu.Size = new System.Drawing.Size(163, 65);
            this.Otsu.TabIndex = 2;
            this.Otsu.Text = "Метод Отсу";
            this.Otsu.UseVisualStyleBackColor = true;
            this.Otsu.Click += new System.EventHandler(this.Otsu_Click);
            // 
            // Nibelek
            // 
            this.Nibelek.Location = new System.Drawing.Point(1002, 415);
            this.Nibelek.Name = "Nibelek";
            this.Nibelek.Size = new System.Drawing.Size(163, 65);
            this.Nibelek.TabIndex = 4;
            this.Nibelek.Text = "Метод Ниблека";
            this.Nibelek.UseVisualStyleBackColor = true;
            this.Nibelek.Click += new System.EventHandler(this.Nibelek_Click);
            // 
            // Sauwol
            // 
            this.Sauwol.Location = new System.Drawing.Point(1172, 415);
            this.Sauwol.Name = "Sauwol";
            this.Sauwol.Size = new System.Drawing.Size(163, 65);
            this.Sauwol.TabIndex = 3;
            this.Sauwol.Text = "Метод Сауволы";
            this.Sauwol.UseVisualStyleBackColor = true;
            this.Sauwol.Click += new System.EventHandler(this.Sauwol_Click);
            // 
            // Wolf
            // 
            this.Wolf.Location = new System.Drawing.Point(1001, 486);
            this.Wolf.Name = "Wolf";
            this.Wolf.Size = new System.Drawing.Size(163, 65);
            this.Wolf.TabIndex = 6;
            this.Wolf.Text = "Метод Вульфа";
            this.Wolf.UseVisualStyleBackColor = true;
            this.Wolf.Click += new System.EventHandler(this.Wolf_Click);
            // 
            // BredLy_Rot
            // 
            this.BredLy_Rot.Location = new System.Drawing.Point(1172, 486);
            this.BredLy_Rot.Name = "BredLy_Rot";
            this.BredLy_Rot.Size = new System.Drawing.Size(163, 65);
            this.BredLy_Rot.TabIndex = 5;
            this.BredLy_Rot.Text = "Метод Брэдли-Рота";
            this.BredLy_Rot.UseVisualStyleBackColor = true;
            this.BredLy_Rot.Click += new System.EventHandler(this.BredLy_Rot_Click);
            // 
            // Back
            // 
            this.Back.Location = new System.Drawing.Point(1253, 30);
            this.Back.Name = "Back";
            this.Back.Size = new System.Drawing.Size(79, 23);
            this.Back.TabIndex = 7;
            this.Back.Text = "Обратно";
            this.Back.UseVisualStyleBackColor = true;
            this.Back.Click += new System.EventHandler(this.Back_Click);
            // 
            // Щ
            // 
            this.Щ.Location = new System.Drawing.Point(998, 30);
            this.Щ.Name = "Щ";
            this.Щ.Size = new System.Drawing.Size(79, 23);
            this.Щ.TabIndex = 8;
            this.Щ.Text = "Открыть";
            this.Щ.UseVisualStyleBackColor = true;
            this.Щ.Click += new System.EventHandler(this.Open_Click);
            // 
            // BackUp
            // 
            this.BackUp.Location = new System.Drawing.Point(1083, 30);
            this.BackUp.Name = "BackUp";
            this.BackUp.Size = new System.Drawing.Size(79, 23);
            this.BackUp.TabIndex = 9;
            this.BackUp.Text = "Сброс";
            this.BackUp.UseVisualStyleBackColor = true;
            this.BackUp.Click += new System.EventHandler(this.BackUp_Click);
            // 
            // Save
            // 
            this.Save.Location = new System.Drawing.Point(1168, 30);
            this.Save.Name = "Save";
            this.Save.Size = new System.Drawing.Size(79, 23);
            this.Save.TabIndex = 10;
            this.Save.Text = "Сохранить";
            this.Save.UseVisualStyleBackColor = true;
            this.Save.Click += new System.EventHandler(this.Save_Click);
            // 
            // kritt
            // 
            this.kritt.Location = new System.Drawing.Point(1256, 380);
            this.kritt.Name = "kritt";
            this.kritt.Size = new System.Drawing.Size(60, 20);
            this.kritt.TabIndex = 11;
            this.kritt.Text = "0,15";
            // 
            // krita
            // 
            this.krita.Location = new System.Drawing.Point(1256, 329);
            this.krita.Name = "krita";
            this.krita.Size = new System.Drawing.Size(60, 20);
            this.krita.TabIndex = 12;
            this.krita.Text = "0,5";
            // 
            // kritR
            // 
            this.kritR.Location = new System.Drawing.Point(1256, 353);
            this.kritR.Name = "kritR";
            this.kritR.Size = new System.Drawing.Size(60, 20);
            this.kritR.TabIndex = 13;
            this.kritR.Text = "128";
            // 
            // kritk
            // 
            this.kritk.Location = new System.Drawing.Point(1256, 303);
            this.kritk.Name = "kritk";
            this.kritk.Size = new System.Drawing.Size(60, 20);
            this.kritk.TabIndex = 14;
            this.kritk.Text = "-0,2";
            // 
            // WidowSize
            // 
            this.WidowSize.Location = new System.Drawing.Point(1256, 276);
            this.WidowSize.Name = "WidowSize";
            this.WidowSize.Size = new System.Drawing.Size(60, 20);
            this.WidowSize.TabIndex = 15;
            this.WidowSize.Text = "15";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F);
            this.label1.Location = new System.Drawing.Point(998, 129);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(194, 24);
            this.label1.TabIndex = 16;
            this.label1.Text = "Глобальные методы";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F);
            this.label2.Location = new System.Drawing.Point(997, 235);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(183, 24);
            this.label2.TabIndex = 17;
            this.label2.Text = "Локальные методы";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label3.Location = new System.Drawing.Point(998, 277);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(212, 17);
            this.label3.TabIndex = 18;
            this.label3.Text = "Размер окна(нечётное число):\r\n";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label4.Location = new System.Drawing.Point(998, 259);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(271, 17);
            this.label4.TabIndex = 19;
            this.label4.Text = "Критерии(заполнить все) Только числа";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label5.Location = new System.Drawing.Point(998, 304);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(143, 17);
            this.label5.TabIndex = 20;
            this.label5.Text = "Чувствительность k:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label6.Location = new System.Drawing.Point(998, 330);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(88, 17);
            this.label6.TabIndex = 21;
            this.label6.Text = "Усиление a:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label7.Location = new System.Drawing.Point(999, 354);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(22, 17);
            this.label7.TabIndex = 22;
            this.label7.Text = "R:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label8.Location = new System.Drawing.Point(998, 381);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(257, 17);
            this.label8.TabIndex = 23;
            this.label8.Text = "Чувствительность для Брэдли-Рота t:";
            // 
            // Binar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1341, 681);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.WidowSize);
            this.Controls.Add(this.kritk);
            this.Controls.Add(this.kritR);
            this.Controls.Add(this.krita);
            this.Controls.Add(this.kritt);
            this.Controls.Add(this.Save);
            this.Controls.Add(this.BackUp);
            this.Controls.Add(this.Щ);
            this.Controls.Add(this.Back);
            this.Controls.Add(this.Wolf);
            this.Controls.Add(this.BredLy_Rot);
            this.Controls.Add(this.Nibelek);
            this.Controls.Add(this.Sauwol);
            this.Controls.Add(this.Otsu);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.pictureBox1);
            this.Name = "Binar";
            this.Text = "Binar";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button Otsu;
        private System.Windows.Forms.Button Nibelek;
        private System.Windows.Forms.Button Sauwol;
        private System.Windows.Forms.Button Wolf;
        private System.Windows.Forms.Button BredLy_Rot;
        private System.Windows.Forms.Button Back;
        private System.Windows.Forms.Button Щ;
        private System.Windows.Forms.Button BackUp;
        private System.Windows.Forms.Button Save;
        private System.Windows.Forms.TextBox kritt;
        private System.Windows.Forms.TextBox krita;
        private System.Windows.Forms.TextBox kritR;
        private System.Windows.Forms.TextBox kritk;
        private System.Windows.Forms.TextBox WidowSize;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
    }
}