using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing.Imaging;
using System.Diagnostics;


namespace ImgApp_2_WinForms
{
    public partial class Form1 : Form
    {
        private List<Bitmap> images = new List<Bitmap>();
        private List<Bitmap> imageBackUp = new List<Bitmap>();
        private List<int> tags = new List<int>();

        private List<Panel> layerPanels = new List<Panel>();

        private int PictureIndex = 0;
        private int ImageIndex = 0;
        private int NewlayerCount = 0;
        private int currentYPosition = 0;

        private Dictionary<int, float> alphaValues = new Dictionary<int, float>();

        public Form1()
        {
            InitializeComponent();

            pictureBox1.AllowDrop = true;
            pictureBox1.DragEnter += pictureBox1_DragEnter;
            pictureBox1.DragDrop += pictureBox1_DragDrop;
            this.AutoScroll = true;
        }

        #region Новые слои
        private void AddPictureBox(string imagePath)
        {
            Bitmap originalBitmap = new Bitmap(imagePath);
            Bitmap copiedBitmap = new Bitmap(originalBitmap.Width, originalBitmap.Height, originalBitmap.PixelFormat);

            using (Graphics gfx = Graphics.FromImage(copiedBitmap))
            {
                gfx.DrawImage(originalBitmap, new Rectangle(0, 0, originalBitmap.Width, originalBitmap.Height));
            }
            imageBackUp.Add(copiedBitmap);

            Panel layerPanel = new Panel();
            layerPanel.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            layerPanel.Location = new Point(930, currentYPosition + 75);
            layerPanel.Size = new Size(330, 75);
            Controls.Add(layerPanel);
            layerPanels.Add(layerPanel);

            PictureBox newpictureBox = new PictureBox();
            newpictureBox.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            newpictureBox.Location = new Point(-20, 0);
            newpictureBox.Name = "pictureBox2" + NewlayerCount.ToString();
            newpictureBox.Image = Image.FromFile(imagePath);
            newpictureBox.Size = new Size(110, 75);
            newpictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            newpictureBox.TabIndex = 4;
            newpictureBox.TabStop = false;

            CheckBox newRed = new CheckBox();
            newRed.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            newRed.AutoSize = true;
            newRed.Checked = true;
            newRed.CheckState = CheckState.Checked;
            newRed.Location = new Point(250, 9);
            newRed.Name = "Red" + NewlayerCount.ToString();
            newRed.Size = new Size(34, 17);
            newRed.TabIndex = 5;
            newRed.Text = "R";
            newRed.UseVisualStyleBackColor = true;
            newRed.Tag = "R" + PictureIndex.ToString();
            newRed.CheckedChanged += RGB;

            CheckBox newGreen = new CheckBox();
            newGreen.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            newGreen.AutoSize = true;
            newGreen.Checked = true;
            newGreen.CheckState = CheckState.Checked;
            newGreen.Location = new Point(250, 32);
            newGreen.Name = "Green" + NewlayerCount.ToString();
            newGreen.Size = new Size(34, 17);
            newGreen.TabIndex = 6;
            newGreen.Text = "G";
            newGreen.UseVisualStyleBackColor = true;
            newGreen.Tag = "G" + PictureIndex.ToString();
            newGreen.CheckedChanged += RGB;

            CheckBox newBlue = new CheckBox();
            newBlue.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            newBlue.AutoSize = true;
            newBlue.Checked = true;
            newBlue.CheckState = CheckState.Checked;
            newBlue.Location = new Point(250, 55);
            newBlue.Name = "Blue" + NewlayerCount.ToString();
            newBlue.Size = new Size(33, 17);
            newBlue.TabIndex = 7;
            newBlue.Text = "B";
            newBlue.UseVisualStyleBackColor = true;
            newBlue.Tag = "B" + PictureIndex.ToString();
            newBlue.CheckedChanged += RGB;

            ComboBox newComboBox = new ComboBox();
            newComboBox.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            newComboBox.FormattingEnabled = true;
            newComboBox.Items.AddRange(new object[] {
           "Ничего",
           "Сумма",
           "Разность",
           "Произведение",
           "Среднее",
           "Минимум",
           "Максимум"});
            newComboBox.Location = new Point(125, 10);
            newComboBox.Name = "Nalozh" + NewlayerCount.ToString();
            newComboBox.Size = new Size(115, 21);
            newComboBox.TabIndex = 8;
            newComboBox.Text = "Ничего";
            newComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            newComboBox.Tag = PictureIndex;
            newComboBox.SelectedIndexChanged += PixelOp;

            Button newUp = new Button();
            newUp.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            newUp.Location = new Point(290, 4);
            newUp.Name = "Up" + NewlayerCount.ToString();
            newUp.Size = new Size(25, 25);
            newUp.TabIndex = 9;
            newUp.Text = "↑";
            newUp.UseVisualStyleBackColor = true;
            newUp.Click += UpClick;
            newUp.Tag = PictureIndex;

            Button newDelete = new Button();
            newDelete.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            newDelete.Location = new Point(290, 27);
            newDelete.Name = "Delete" + NewlayerCount.ToString();
            newDelete.Size = new Size(25, 25);
            newDelete.TabIndex = 10;
            newDelete.Text = "X";
            newDelete.UseVisualStyleBackColor = true;
            newDelete.Click += DeleteClick;
            newDelete.Tag = PictureIndex;

            Button newDown = new Button();
            newDown.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            newDown.Location = new Point(290, 51);
            newDown.Name = "Down" + NewlayerCount.ToString();
            newDown.Size = new Size(25, 25);
            newDown.TabIndex = 11;
            newDown.Text = "↓";
            newDown.UseVisualStyleBackColor = true;
            newDown.Click += DownClick;
            newDown.Tag = PictureIndex;

            TrackBar newTransparent = new TrackBar();
            newTransparent.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            newTransparent.Cursor = Cursors.Arrow;
            newTransparent.LargeChange = 5;
            newTransparent.Location = new Point(125, 37);
            newTransparent.Maximum = 255;
            newTransparent.Name = "transparent" + NewlayerCount.ToString();
            newTransparent.Size = new Size(115, 45);
            newTransparent.TabIndex = 12;
            newTransparent.TickStyle = TickStyle.None;
            newTransparent.Tag = PictureIndex;
            newTransparent.ValueChanged += new EventHandler(Transperent);

            TextBox newXCoord = new TextBox();
            newXCoord.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            newXCoord.Location = new Point(145, 55);
            newXCoord.Name = "Xcoord" + NewlayerCount.ToString();
            newXCoord.Size = new Size(38, 20);
            newXCoord.TabIndex = 13;
            newXCoord.Text = "0";
            newXCoord.Tag = "X" + PictureIndex;
            newXCoord.TextChanged += new EventHandler(XYcoords);

            TextBox newYCoord = new TextBox();
            newYCoord.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            newYCoord.Location = new Point(200, 55);
            newYCoord.Name = "Ycoord" + NewlayerCount.ToString();
            newYCoord.Size = new Size(38, 20);
            newYCoord.TabIndex = 14;
            newYCoord.Text = "0";
            newYCoord.Tag = "Y" + PictureIndex;
            newYCoord.TextChanged += new EventHandler(XYcoords);

            System.Windows.Forms.Label newlabelX = new System.Windows.Forms.Label();
            newlabelX.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            newlabelX.AutoSize = true;
            newlabelX.Location = new Point(124, 58);
            newlabelX.Name = "label1" + NewlayerCount.ToString();
            newlabelX.Size = new Size(14, 13);
            newlabelX.TabIndex = 15;
            newlabelX.Text = "X";

            System.Windows.Forms.Label newlabelY = new System.Windows.Forms.Label();
            newlabelY.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            newlabelY.AutoSize = true;
            newlabelY.Location = new System.Drawing.Point(180, 58);
            newlabelY.Name = "label2" + NewlayerCount.ToString();
            newlabelY.Size = new Size(14, 13);
            newlabelY.TabIndex = 16;
            newlabelY.Text = "Y";

            tags.Add(PictureIndex);

            layerPanel.Controls.Add(newlabelY);
            layerPanel.Controls.Add(newlabelX);
            layerPanel.Controls.Add(newYCoord);
            layerPanel.Controls.Add(newXCoord);
            layerPanel.Controls.Add(newpictureBox);
            layerPanel.Controls.Add(newRed);
            layerPanel.Controls.Add(newGreen);
            layerPanel.Controls.Add(newBlue);
            layerPanel.Controls.Add(newComboBox);
            layerPanel.Controls.Add(newUp);
            layerPanel.Controls.Add(newDelete);
            layerPanel.Controls.Add(newDown);
            layerPanel.Controls.Add(newTransparent);

            PictureIndex++;
            NewlayerCount++;
            currentYPosition += 85;
        }
        #endregion

        #region Перетаскивание
        private void pictureBox1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                foreach (string file in files)
                {
                    string extension = Path.GetExtension(file).ToLower();
                    if (extension == ".png" || extension == ".jpg" || extension == ".jpeg" || extension == ".bmp" || extension == ".gif")
                    {
                        e.Effect = DragDropEffects.Copy;
                        return;
                    }
                }
            }
            else
                e.Effect = DragDropEffects.None;
        }

        private void pictureBox1_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            if (files != null && files.Length > 0)
            {
                try
                {
                    foreach (string file in files)
                    {
                        Bitmap droppedImage = new Bitmap(file);
                        images.Add(droppedImage);
                        AddPictureBox(file);
                    }
                    ImageIndex = 0;
                    DisplayCombinedImage();
                    pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при загрузке изображения: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    foreach (var img in images)
                    {
                        img.Dispose();
                    }
                    images.Clear();
                    pictureBox1.Image = null;
                }
            }
        }
        #endregion

        private void DisplayCombinedImage(Bitmap z = null, int x = 0, int y = 0)
        {
            if (images.Count == 0)
            {
                pictureBox1.Image = null;
                return;
            }

            int totalWidth = 0;
            int maxHeight = 0;
            foreach (Bitmap image in images)
            {
                totalWidth = Math.Max(totalWidth, image.Width);
                maxHeight = Math.Max(maxHeight, image.Height);
            }

            Bitmap combinedBitmap = new Bitmap(totalWidth, maxHeight, PixelFormat.Format32bppArgb);

            using (Graphics g = Graphics.FromImage(combinedBitmap))
            {
                foreach (Bitmap image in images)
                {
                    if (z == image)
                        g.DrawImage(z, x, y);
                    else
                        g.DrawImage(image, 0, 0);
                }
            }

            pictureBox1.Image = combinedBitmap;
        }

        private void bOpen_Click(object sender, EventArgs e)
        {
            using OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = Directory.GetCurrentDirectory();
            openFileDialog.Filter = "Картинки (png, jpg, bmp, gif) |*.png;*.jpg;*.bmp;*.gif|All files (*.*)|*.*";
            openFileDialog.RestoreDirectory = true;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    Bitmap droppedImage = new Bitmap(openFileDialog.FileName);
                    images.Add(droppedImage);
                    ImageIndex = 0;
                    DisplayCombinedImage();
                    AddPictureBox(openFileDialog.FileName);
                    pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при загрузке изображения: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    foreach (var img in images)
                    {
                        img.Dispose();
                    }
                    images.Clear();
                    pictureBox1.Image = null;
                }
            }
        }
        private void bSave_Click(object sender, EventArgs e)
        {
            if (images.Count == 0)
            {
                MessageBox.Show("Нет изображений для сохранения.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.InitialDirectory = Directory.GetCurrentDirectory();
                saveFileDialog.Filter = "Картинки (png, jpg, bmp, gif) |*.png;*.jpg;*.bmp;*.gif|All files (*.*)|*.*";
                saveFileDialog.RestoreDirectory = true;

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        pictureBox1.Image.Save(saveFileDialog.FileName);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка при сохранении изображения: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
        private void UpClick(object sender, EventArgs e)
        {
            Button clickedButton = (Button)sender;
            int IndexI = (int)clickedButton.Tag;
            int Index = tags.IndexOf(IndexI);

            if (images == null && layerPanels == null)
                return;

            if (Index < 1 || Index >= images.Count || Index >= layerPanels.Count)
                return;

            layerPanels[Index].Location = new Point(930, layerPanels[Index].Location.Y - 85);
            layerPanels[Index - 1].Location = new Point(930, layerPanels[Index - 1].Location.Y + 85);

            Bitmap testI = images[Index];
            images[Index] = images[Index - 1];
            images[Index - 1] = testI;
            imageBackUp[Index] = imageBackUp[Index - 1];
            imageBackUp[Index - 1] = testI;

            Panel testP = layerPanels[Index];
            layerPanels[Index] = layerPanels[Index - 1];
            layerPanels[Index - 1] = testP;

            int testT = tags[Index];
            tags[Index] = tags[Index - 1];
            tags[Index - 1] = testT;

            DisplayCombinedImage();
        }
        private void DownClick(object sender, EventArgs e)
        {
            Button clickedButton = (Button)sender;
            int IndexI = (int)clickedButton.Tag;
            int Index = tags.IndexOf(IndexI);

            if (images == null && layerPanels == null)
                return;

            if (Index < 0 || Index >= images.Count - 1 || Index >= layerPanels.Count - 1)
                return;

            layerPanels[Index].Location = new Point(930, layerPanels[Index].Location.Y + 85);
            layerPanels[Index + 1].Location = new Point(930, layerPanels[Index + 1].Location.Y - 85);

            Bitmap testI = images[Index];
            images[Index] = images[Index + 1];
            images[Index + 1] = testI;
            imageBackUp[Index] = imageBackUp[Index + 1];
            imageBackUp[Index + 1] = testI;

            Panel testP = layerPanels[Index];
            layerPanels[Index] = layerPanels[Index + 1];
            layerPanels[Index + 1] = testP;

            int testT = tags[Index];
            tags[Index] = tags[Index + 1];
            tags[Index + 1] = testT;

            DisplayCombinedImage();
        }
        private void DeleteClick(object sender, EventArgs e)
        {
            Button clickedButton = (Button)sender;
            int IndexI = (int)clickedButton.Tag;
            int Index = tags.IndexOf(IndexI);

            images.RemoveAt(Index);
            imageBackUp.RemoveAt(Index);

            foreach (Panel panel in layerPanels.Skip(Index + 1))
            {
                panel.Location = new Point(930, panel.Location.Y - 85);
                currentYPosition -= 85;
            }

            Controls.Remove(layerPanels[Index]);
            layerPanels.RemoveAt(Index);
            tags.RemoveAt(Index);

            PictureIndex--;
            NewlayerCount--;

            if (NewlayerCount == 0)
                currentYPosition = 0;

            DisplayCombinedImage();
        }
        private void RGB(object sender, EventArgs e)
        {
            CheckBox checkboxsender = (CheckBox)sender;
            string TestIndexI = (string)checkboxsender.Tag;
            string IndexI = TestIndexI.Substring(1);
            int Index = tags.IndexOf(int.Parse(IndexI));

            UpdateImage(Index);

            DisplayCombinedImage();
        }
        private void Transperent(object sender, EventArgs e)
        {
            TrackBar trackBarsender = (TrackBar)sender;
            int IndexI = (int)trackBarsender.Tag;
            int Index = tags.IndexOf(IndexI);

            int transparencyValue = 255 - trackBarsender.Value;
            float alpha = (float)transparencyValue / 255f;

            alphaValues[Index] = alpha;

            UpdateImage(Index);

            DisplayCombinedImage();
        }
        private void UpdateImage(int Index)
        {
            Bitmap imageBackup = imageBackUp[Index];
            Bitmap image = images[Index];
            string tagR = "R" + tags[Index].ToString();
            string tagG = "G" + tags[Index].ToString();
            string tagB = "B" + tags[Index].ToString();

            CheckBox redCheckbox = layerPanels.SelectMany(p => p.Controls.OfType<CheckBox>()).FirstOrDefault(cb => cb.Tag.ToString() == tagR);
            CheckBox greenCheckbox = layerPanels.SelectMany(p => p.Controls.OfType<CheckBox>()).FirstOrDefault(cb => cb.Tag.ToString() == tagG);
            CheckBox blueCheckbox = layerPanels.SelectMany(p => p.Controls.OfType<CheckBox>()).FirstOrDefault(cb => cb.Tag.ToString() == tagB);

            bool redEnabled = redCheckbox?.Checked ?? true;
            bool greenEnabled = greenCheckbox?.Checked ?? true;
            bool blueEnabled = blueCheckbox?.Checked ?? true;

            float alpha = 1f;
            if (alphaValues.ContainsKey(Index))
            {
                alpha = alphaValues[Index];
            }

            Bitmap modifiedImage = new Bitmap(imageBackup.Width, imageBackup.Height, PixelFormat.Format32bppArgb);

            using (Graphics g = Graphics.FromImage(modifiedImage))
            {
                g.Clear(Color.Transparent);

                for (int x = 0; x < imageBackup.Width; x++)
                {
                    for (int y = 0; y < imageBackup.Height; y++)
                    {
                        Color originalColor = imageBackup.GetPixel(x, y);

                        int red = redEnabled ? originalColor.R : 0;
                        int green = greenEnabled ? originalColor.G : 0;
                        int blue = blueEnabled ? originalColor.B : 0;

                        Color newColor = Color.FromArgb((int)(alpha * 255), red, green, blue);

                        modifiedImage.SetPixel(x, y, newColor);
                    }
                }
            }
            images[Index] = modifiedImage;
        }
        private void PixelOp(object sender, EventArgs e)
        {
            ComboBox ComboBoxsender = (ComboBox)sender;
            int IndexI = (int)ComboBoxsender.Tag;
            int Index = tags.IndexOf(IndexI);
            object ComboBoxItem = ComboBoxsender.SelectedItem;

            if ((string)ComboBoxItem == "Ничего")
            {
                images[Index] = imageBackUp[Index];
                DisplayCombinedImage();
            }
            if ((string)ComboBoxItem == "Сумма")
            {
                Bitmap result = new Bitmap(images[Index]);

                for (int i = 0; i < Index; i++)
                {
                    result = PixelSum(result, images[i]);
                }
                images[Index] = result;
                DisplayCombinedImage();
            }
            if ((string)ComboBoxItem == "Разность")
            {
                {
                    Bitmap result = new Bitmap(images[Index]);

                    for (int i = 0; i < Index; i++)
                    {
                        result = PixelSub(result, images[i]);
                    }
                    images[Index] = result;
                    pictureBox1.Image = result;
                }
            }
            if ((string)ComboBoxItem == "Произведение")
            {
                {
                    Bitmap result = new Bitmap(images[Index]);

                    foreach (Bitmap image in images.Take(Index))
                    {
                        result = PixelMult(result, image);
                    }
                    images[Index] = result;
                    DisplayCombinedImage();
                }
            }
            if ((string)ComboBoxItem == "Среднее")
            {
                {
                    Bitmap result = new Bitmap(images[Index]);

                    for (int i = 0; i < Index; i++)
                    {
                        result = PixelSr(result, images[i]);
                    }
                    images[Index] = result;
                    DisplayCombinedImage();
                }
            }
            if ((string)ComboBoxItem == "Минимум")
            {
                {
                    Bitmap result = new Bitmap(images[Index]);

                    for (int i = 0; i < Index; i++)
                    {
                        result = PixelMin(result, images[i]);
                    }
                    images[Index] = result;
                    DisplayCombinedImage();
                }
            }
            if ((string)ComboBoxItem == "Максимум")
            {
                {
                    Bitmap result = new Bitmap(images[Index]);

                    for (int i = 0; i < Index; i++)
                    {
                        result = PixelMax(result, images[i]);
                    }
                    images[Index] = result;
                    DisplayCombinedImage();
                }
            }
        }
        private Bitmap PixelSum(Bitmap image1, Bitmap image2)
        {
            int maxWidth = Math.Max(image1.Width, image2.Width);
            int maxHeight = Math.Max(image1.Height, image2.Height);
            Bitmap result = new Bitmap(maxWidth, maxHeight);

            using (Graphics g = Graphics.FromImage(result))
            {
                g.Clear(Color.Transparent);
            }
            int overlayWidth = Math.Min(image1.Width, image2.Width);
            int overlayHeight = Math.Min(image1.Height, image2.Height);

            for (int x = 0; x < overlayWidth; x++)
            {
                for (int y = 0; y < overlayHeight; y++)
                {
                    Color pixel1 = image1.GetPixel(x, y);
                    Color pixel2 = image2.GetPixel(x, y);


                    int r = Math.Min(pixel1.R + pixel2.R, 255);
                    int g = Math.Min(pixel1.G + pixel2.G, 255);
                    int b = Math.Min(pixel1.B + pixel2.B, 255);
                    int a = Math.Min(pixel1.A + pixel2.A, 255);

                    Color sumPixel = Color.FromArgb(a, r, g, b);
                    result.SetPixel(x, y, sumPixel);
                }
            }
            return result;
        }
        private Bitmap PixelSub(Bitmap image1, Bitmap image2)
        {
            int maxWidth = Math.Max(image1.Width, image2.Width);
            int maxHeight = Math.Max(image1.Height, image2.Height);
            Bitmap result = new Bitmap(maxWidth, maxHeight, PixelFormat.Format32bppArgb);

            int overlayWidth = Math.Min(image1.Width, image2.Width);
            int overlayHeight = Math.Min(image1.Height, image2.Height);

            using (Graphics g = Graphics.FromImage(result))
            {
                g.Clear(Color.Transparent);
                g.DrawImage(image2, 0, 0);
                g.DrawImage(image1, 0, 0);
            }

            for (int x = 0; x < overlayWidth; x++)
            {
                for (int y = 0; y < overlayHeight; y++)
                {
                    Color pixel1 = image1.GetPixel(x, y);
                    Color pixel2 = image2.GetPixel(x, y);

                    int r = (int)Clamp(pixel1.R - pixel2.R, 0, 255);
                    int g = (int)Clamp(pixel1.G - pixel2.G, 0, 255);
                    int b = (int)Clamp(pixel1.B - pixel2.B, 0, 255);
                    int a = (int)Clamp(pixel1.A - pixel2.A, 0, 255);

                    Color subPixel = Color.FromArgb(a, r, g, b);
                    result.SetPixel(x, y, subPixel);
                }
            }

            return result;
        }
        private Bitmap PixelMult(Bitmap image1, Bitmap image2)
        {
            int maxWidth = Math.Max(image1.Width, image2.Width);
            int maxHeight = Math.Max(image1.Height, image2.Height);
            Bitmap result = new Bitmap(maxWidth, maxHeight);

            using (Graphics g = Graphics.FromImage(result))
            {
                g.Clear(Color.Transparent);
            }
            int overlayWidth = Math.Min(image1.Width, image2.Width);
            int overlayHeight = Math.Min(image1.Height, image2.Height);

            for (int x = 0; x < overlayWidth; x++)
            {
                for (int y = 0; y < overlayHeight; y++)
                {
                    Color pixel1 = image1.GetPixel(x, y);
                    Color pixel2 = image2.GetPixel(x, y);


                    int r = (int)Clamp(pixel1.R * pixel2.R / 255, 0, 255);
                    int g = (int)Clamp(pixel1.G * pixel2.G / 255, 0, 255);
                    int b = (int)Clamp(pixel1.B * pixel2.B / 255, 0, 255);
                    int a = (int)Clamp(pixel1.A * pixel2.A / 255, 0, 255);

                    Color multPixel = Color.FromArgb(a, r, g, b);
                    result.SetPixel(x, y, multPixel);
                }
            }
            return result;
        }
        private Bitmap PixelMin(Bitmap image1, Bitmap image2)
        {
            int maxWidth = Math.Max(image1.Width, image2.Width);
            int maxHeight = Math.Max(image1.Height, image2.Height);
            Bitmap result = new Bitmap(maxWidth, maxHeight);

            using (Graphics g = Graphics.FromImage(result))
            {
                g.Clear(Color.Transparent);
            }
            int overlayWidth = Math.Min(image1.Width, image2.Width);
            int overlayHeight = Math.Min(image1.Height, image2.Height);

            for (int x = 0; x < overlayWidth; x++)
            {
                for (int y = 0; y < overlayHeight; y++)
                {
                    Color pixel1 = image1.GetPixel(x, y);
                    Color pixel2 = image2.GetPixel(x, y);


                    int r = Math.Min(pixel1.R, pixel2.R);
                    int g = Math.Min(pixel1.G, pixel2.G);
                    int b = Math.Min(pixel1.B, pixel2.B);
                    int a = Math.Min(pixel1.A, pixel2.A);

                    Color minPixel = Color.FromArgb(a, r, g, b);
                    result.SetPixel(x, y, minPixel);
                }
            }
            return result;
        }
        private Bitmap PixelMax(Bitmap image1, Bitmap image2)
        {
            int maxWidth = Math.Max(image1.Width, image2.Width);
            int maxHeight = Math.Max(image1.Height, image2.Height);
            Bitmap result = new Bitmap(maxWidth, maxHeight);

            using (Graphics g = Graphics.FromImage(result))
            {
                g.Clear(Color.Transparent);
            }
            int overlayWidth = Math.Min(image1.Width, image2.Width);
            int overlayHeight = Math.Min(image1.Height, image2.Height);

            for (int x = 0; x < overlayWidth; x++)
            {
                for (int y = 0; y < overlayHeight; y++)
                {
                    Color pixel1 = image1.GetPixel(x, y);
                    Color pixel2 = image2.GetPixel(x, y);


                    int r = Math.Max(pixel1.R, pixel2.R);
                    int g = Math.Max(pixel1.G, pixel2.G);
                    int b = Math.Max(pixel1.B, pixel2.B);
                    int a = Math.Max(pixel1.A, pixel2.A);

                    Color maxPixel = Color.FromArgb(a, r, g, b);
                    result.SetPixel(x, y, maxPixel);
                }
            }
            return result;
        }
        private Bitmap PixelSr(Bitmap image1, Bitmap image2)
        {
            int maxWidth = Math.Max(image1.Width, image2.Width);
            int maxHeight = Math.Max(image1.Height, image2.Height);
            Bitmap result = new Bitmap(maxWidth, maxHeight);

            using (Graphics g = Graphics.FromImage(result))
            {
                g.Clear(Color.Transparent);
            }
            int overlayWidth = Math.Min(image1.Width, image2.Width);
            int overlayHeight = Math.Min(image1.Height, image2.Height);

            for (int x = 0; x < overlayWidth; x++)
            {
                for (int y = 0; y < overlayHeight; y++)
                {
                    Color pixel1 = image1.GetPixel(x, y);
                    Color pixel2 = image2.GetPixel(x, y);


                    int r = (int)Clamp((pixel1.R + pixel2.R) / 2, 0, 255);
                    int g = (int)Clamp((pixel1.G + pixel2.G) / 2, 0, 255);
                    int b = (int)Clamp((pixel1.B + pixel2.B) / 2, 0, 255);
                    int a = (int)Clamp((pixel1.A + pixel2.A) / 2, 0, 255);

                    Color srPixel = Color.FromArgb(a, r, g, b);
                    result.SetPixel(x, y, srPixel);
                }
            }
            return result;
        }
        public static T Clamp<T>(T val, T min, T max) where T : IComparable<T>
        {
            if (val.CompareTo(min) < 0) return min;
            else if (val.CompareTo(max) > 0) return max;
            else return val;
        }
        int LastX = 0;
        int LastY = 0;
        private void XYcoords(object sender, EventArgs e)
        {
            TextBox textboxsender = (TextBox)sender;
            string TestIndexI = (string)textboxsender.Tag;
            string IndexI = TestIndexI.Substring(1);
            int Index = tags.IndexOf(int.Parse(IndexI));

            Bitmap image = images[Index];
            int X = textboxsender.Name[0] == 'X' ? int.Parse(textboxsender.Text) : LastX;
            LastX = X;
            int Y = textboxsender.Name[0] == 'Y' ? int.Parse(textboxsender.Text) : LastY;
            LastY = Y;
            DisplayCombinedImage(image, X, Y);
        }
    }
}