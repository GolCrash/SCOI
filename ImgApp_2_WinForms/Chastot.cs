using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Numerics;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImgApp_2_WinForms
{
    public partial class Chastot : Form
    {
        private Bitmap image = null;
        private Bitmap BackUpImage = null;
        private Bitmap BackUpFUrie = null;
        private Bitmap resultImage2 = null;
        private Complex[][,] fourierImage;
        private int powderDPF = 1;
        private int radius1 = 0;
        private int radius2 = 0;
        private List<CircleParams> circleParams = new List<CircleParams>();

        public Chastot()
        {
            InitializeComponent();
            OriginalPictureBox.AllowDrop = true;
            OriginalPictureBox.DragEnter += pictureBox1_DragEnter;
            OriginalPictureBox.DragDrop += pictureBox1_DragDrop;
            this.AutoScroll = true;
        }
        public Chastot(Bitmap _image)
        {
            InitializeComponent();

            image = _image;
            if (image != null)
            {
                OriginalPictureBox.Image = image;
                OriginalPictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            }
            OriginalPictureBox.AllowDrop = true;
            OriginalPictureBox.DragEnter += pictureBox1_DragEnter;
            OriginalPictureBox.DragDrop += pictureBox1_DragDrop;
            this.AutoScroll = true;
        }
        #region В общем кнопки
        private void pictureBox1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                if (files != null && files.Length > 0)
                {
                    string extension = Path.GetExtension(files[0]).ToLower();
                    if (extension == ".png" || extension == ".jpg" || extension == ".jpeg" || extension == ".bmp" || extension == ".gif")
                    {
                        e.Effect = DragDropEffects.Copy;
                        return;
                    }
                }
            }
            e.Effect = DragDropEffects.None;
        }
        private void pictureBox1_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            if (files != null && files.Length > 0)
            {
                LoadImage(files[0]);
            }
        }
        private void LoadImage(string imagePath)
        {
            try
            {
                if (image != null)
                    image.Dispose();

                Bitmap originalBitmap = new Bitmap(imagePath);
                BackUpImage = new Bitmap(imagePath);

                image = new Bitmap(imagePath);
                DisplayImage();
                OriginalPictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке изображения: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                if (image != null)
                {
                    image.Dispose();
                    image = null;
                }
                OriginalPictureBox.Image = null;
            }
        }
        private void DisplayImage()
        {
            if (image == null)
            {
                OriginalPictureBox.Image = null;
                return;
            }

            OriginalPictureBox.Image = image;
        }
        private void Open_Click(object sender, EventArgs e)
        {
            using OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = Directory.GetCurrentDirectory();
            openFileDialog.Filter = "Картинки (png, jpg, bmp, gif) |*.png;*.jpg;*.bmp;*.gif|All files (*.*)|*.*";
            openFileDialog.RestoreDirectory = true;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                LoadImage(openFileDialog.FileName);
            }
        }
        private void Save_Click(object sender, EventArgs e)
        {
            using SaveFileDialog saveFileFialog = new SaveFileDialog();
            saveFileFialog.InitialDirectory = Directory.GetCurrentDirectory();
            saveFileFialog.Filter = "Картинки (png, jpg, bmp, gif) |*.png;*.jpg;*.bmp;*.gif|All files (*.*)|*.*";
            saveFileFialog.RestoreDirectory = true;
            image = new Bitmap(OriginalPictureBox.Image);

            if (saveFileFialog.ShowDialog() == DialogResult.OK)
            {
                if (image != null)
                {
                    image.Save(saveFileFialog.FileName);
                }
            }
        }
        private void Back_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void BackUp_Click(object sender, EventArgs e)
        {
            image = BackUpImage;
            OriginalPictureBox.Image = image;
        }
        public static T Clamp<T>(T val, T min, T max) where T : IComparable<T>
        {
            if (val.CompareTo(min) < 0) return min;
            else if (val.CompareTo(max) > 0) return max;
            else return val;
        }
        #endregion

        #region Фурьефункции

        static Complex[] Compute1DDFT(Complex[] input)
        {
            int N = input.Length;
            Complex[] output = new Complex[N];

            for (int k = 0; k < N; k++)
            {
                output[k] = new Complex(0, 0);
                for (int n = 0; n < N; n++)
                {
                    double angle = -2 * Math.PI * k * n / N;
                    output[k] += input[n] * new Complex(Math.Cos(angle), Math.Sin(angle));
                }
            }

            return output;
        }

        static Complex[] Compute1DIDFT(Complex[] input)
        {
            int N = input.Length;
            Complex[] output = new Complex[N];

            for (int k = 0; k < N; k++)
            {
                output[k] = new Complex(0, 0);
                for (int n = 0; n < N; n++)
                {
                    double angle = 2 * Math.PI * k * n / N;
                    output[k] += input[n] * new Complex(Math.Cos(angle), Math.Sin(angle));
                }
                output[k] /= N;
            }

            return output;
        }

        public Complex[][,] ComputeDFT2D(Bitmap image, bool centering = false)
        {
            int width = image.Width;
            int height = image.Height;
            Complex[][,] dft = new Complex[3][,];

            for (int c = 0; c < 3; c++)
            {
                dft[c] = new Complex[height, width];
                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++)
                    {
                        Color color = image.GetPixel(x, y);
                        double factor = centering ? Math.Pow(-1, x + y) : 1;

                        double colorValue = c == 0 ? color.R : c == 1 ? color.G : color.B;
                        dft[c][y, x] = new Complex(colorValue * factor, 0);
                    }
                }
            }

            for (int c = 0; c < 3; c++)
            {
                // По строкам
                Parallel.For(0, height, y =>
                {
                    Complex[] row = new Complex[width];
                    for (int x = 0; x < width; x++) row[x] = dft[c][y, x];
                    row = Compute1DDFT(row);
                    for (int x = 0; x < width; x++) dft[c][y, x] = row[x];
                });

                Parallel.For(0, width, x =>
                {
                    Complex[] col = new Complex[height];
                    for (int y = 0; y < height; y++) col[y] = dft[c][y, x];
                    col = Compute1DDFT(col);
                    for (int y = 0; y < height; y++) dft[c][y, x] = col[y];
                });
            }

            return dft;
        }
        public Bitmap ComputeIDFT2D(Complex[][,] dft, bool centering = false)
        {
            int width = dft[0].GetLength(1);
            int height = dft[0].GetLength(0);
            Complex[][,] idft = new Complex[3][,];

            for (int c = 0; c < 3; c++)
            {
                idft[c] = new Complex[height, width];
                Array.Copy(dft[c], idft[c], dft[c].Length);

                Parallel.For(0, height, y =>
                {
                    Complex[] row = new Complex[width];
                    for (int x = 0; x < width; x++) row[x] = idft[c][y, x];
                    row = Compute1DIDFT(row);
                    for (int x = 0; x < width; x++) idft[c][y, x] = row[x];
                });

                Parallel.For(0, width, x =>
                {
                    Complex[] col = new Complex[height];
                    for (int y = 0; y < height; y++) col[y] = idft[c][y, x];
                    col = Compute1DIDFT(col);
                    for (int y = 0; y < height; y++) idft[c][y, x] = col[y];
                });
            }

            Bitmap result = new Bitmap(width, height);

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    double factor = centering ? Math.Pow(-1, x + y) : 1;

                    double r = Math.Max(0, Math.Min(255, idft[0][y, x].Real * factor));
                    double g = Math.Max(0, Math.Min(255, idft[1][y, x].Real * factor));
                    double b = Math.Max(0, Math.Min(255, idft[2][y, x].Real * factor));

                    Color color = Color.FromArgb(Clamp((int)r, 0, 255), Clamp((int)g, 0, 255), Clamp((int)b, 0, 255));
                    result.SetPixel(x, y, color);
                }
            }

            return result;
        }

        public Bitmap VisualizeFourierImage(Complex[][,] fourierImage)
        {
            int height = fourierImage[0].GetLength(0);
            int width = fourierImage[0].GetLength(1);
            Bitmap visualization = new Bitmap(width, height);

            double maxMagnitude = 0;
            for (int c = 0; c < 3; c++)
            {
                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++)
                    {
                        maxMagnitude = Math.Max(maxMagnitude, fourierImage[c][y, x].Magnitude);
                    }
                }
            }

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    double magnitude = (fourierImage[0][y, x].Magnitude + fourierImage[1][y, x].Magnitude + fourierImage[2][y, x].Magnitude) / 3;
                    double logMagnitude = Math.Log(magnitude + 1);
                    int grayValue = (int)(logMagnitude * 255 / Math.Log(maxMagnitude + 1));
                    grayValue = Clamp(grayValue, 0, 255);

                    Color color = Color.FromArgb(grayValue, grayValue, grayValue);
                    visualization.SetPixel(x, y, color);
                }
            }

            return visualization;
        }

        #endregion

        #region Фильтры

        private Complex[][,] ApplyFilter(Complex[][,] dft, string filterType, int radius1, int radius2 = 0)
        {
            switch (filterType)
            {
                case "Низкочастотная":
                    return ApplyLowPassFilter(dft, radius1);
                case "Высокочастотная":
                    return ApplyHighPassFilter(dft, radius1);
                case "Полосовой":
                    return ApplyBandPassFilter(dft, radius1, radius2);
                case "Режекторный":
                    return ApplyBandRejectFilter(dft, radius1, radius2);
                case "Узкополосный полосовой":
                    return ApplyNarrowBandPassFilter(dft, circleParams);
                case "Узкополосный режекторный":
                    return ApplyNarrowBandRejectFilter(dft, circleParams);
                default:
                    return dft;
            }
        }

        private Complex[][,] ApplyLowPassFilter(Complex[][,] dft, int radius)
        {
            int height = dft[0].GetLength(0);
            int width = dft[0].GetLength(1);
            Complex[][,] filtered = new Complex[3][,];
            int centerX = width / 2;
            int centerY = height / 2;

            for (int c = 0; c < 3; c++)
            {
                filtered[c] = new Complex[height, width];
                Parallel.For(0, height, y =>
                {
                    for (int x = 0; x < width; x++)
                    {
                        double dx = x - centerX;
                        double dy = y - centerY;
                        double distance = Math.Sqrt(dx * dx + dy * dy);
                        filtered[c][y, x] = distance <= radius ? dft[c][y, x] : Complex.Zero;
                    }
                });
            }
            return filtered;
        }
        private Complex[][,] ApplyHighPassFilter(Complex[][,] dft, int radius)
        {
            int height = dft[0].GetLength(0);
            int width = dft[0].GetLength(1);
            Complex[][,] filtered = new Complex[3][,];
            int centerX = width / 2;
            int centerY = height / 2;

            for (int c = 0; c < 3; c++)
            {
                filtered[c] = new Complex[height, width];
                Parallel.For(0, height, y =>
                {
                    for (int x = 0; x < width; x++)
                    {
                        double dx = x - centerX;
                        double dy = y - centerY;
                        double distance = Math.Sqrt(dx * dx + dy * dy);
                        filtered[c][y, x] = distance > radius ? dft[c][y, x] : Complex.Zero;
                    }
                });
            }
            return filtered;
        }

        private Complex[][,] ApplyBandPassFilter(Complex[][,] dft, int radius1, int radius2)
        {
            {
                int height = dft[0].GetLength(0);
                int width = dft[0].GetLength(1);
                Complex[][,] filtered = new Complex[3][,];
                int centerX = width / 2;
                int centerY = height / 2;

                int innerRadius = Math.Min(radius1, radius2);
                int outerRadius = Math.Max(radius1, radius2);

                for (int c = 0; c < 3; c++)
                {
                    filtered[c] = new Complex[height, width];
                    for (int i = 0; i < height; i++)
                    {
                        for (int j = 0; j < width; j++)
                        {
                            double dx = j - centerX;
                            double dy = i - centerY;
                            double distance = Math.Sqrt(dx * dx + dy * dy);

                            if (distance >= innerRadius && distance <= outerRadius)
                            {
                                filtered[c][i, j] = dft[c][i, j];
                            }
                            else
                            {
                                filtered[c][i, j] = Complex.Zero;
                            }
                        }
                    }
                }
                return filtered;
            }
        }

        private Complex[][,] ApplyBandRejectFilter(Complex[][,] dft, int radius1, int radius2)
        {
            int height = dft[0].GetLength(0);
            int width = dft[0].GetLength(1);
            Complex[][,] filtered = new Complex[3][,];
            int centerX = width / 2;
            int centerY = height / 2;

            int innerRadius = Math.Min(radius1, radius2);
            int outerRadius = Math.Max(radius1, radius2);

            for (int c = 0; c < 3; c++)
            {
                filtered[c] = new Complex[height, width];
                for (int i = 0; i < height; i++)
                {
                    for (int j = 0; j < width; j++)
                    {
                        double dx = j - centerX;
                        double dy = i - centerY;
                        double distance = Math.Sqrt(dx * dx + dy * dy);

                        if (distance >= innerRadius && distance <= outerRadius)
                        {
                            filtered[c][i, j] = Complex.Zero;
                        }
                        else
                        {
                            filtered[c][i, j] = dft[c][i, j];
                        }
                    }
                }
            }
            return filtered;
        }

        public static Complex[][,] ApplyNarrowBandPassFilter(Complex[][,] dft, List<CircleParams> circles)
        {
            int height = dft[0].GetLength(0);
            int width = dft[0].GetLength(1);
            Complex[][,] filtered = new Complex[3][,];

            int centerX = width / 2;
            int centerY = height / 2;

            for (int c = 0; c < 3; c++)
            {
                filtered[c] = new Complex[height, width];
                for (int i = 0; i < height; i++)
                {
                    for (int j = 0; j < width; j++)
                    {
                        filtered[c][i, j] = new Complex(0, 0);
                        foreach (var circle in circles)
                        {
                            int circleCenterX = circle.X + centerX;
                            int circleCenterY = circle.Y + centerY;

                            double dx = j - circleCenterX;
                            double dy = i - circleCenterY;
                            double distance = Math.Sqrt(dx * dx + dy * dy);

                            if (circle.RadiusInner == 0 && circle.RadiusOuter == 0)
                            {
                                if (distance <= circle.Radius)
                                {
                                    filtered[c][i, j] = dft[c][i, j];
                                    break;
                                }
                            }
                            else
                            {
                                if (distance >= circle.RadiusInner && distance <= circle.RadiusOuter)
                                {
                                    filtered[c][i, j] = dft[c][i, j];
                                    break;
                                }
                            }
                        }
                    }
                }
            }

            return filtered;
        }

        public static Complex[][,] ApplyNarrowBandRejectFilter(Complex[][,] dft, List<CircleParams> circles)
        {
            int height = dft[0].GetLength(0);
            int width = dft[0].GetLength(1);
            Complex[][,] filtered = new Complex[3][,];

            int centerX = width / 2;
            int centerY = height / 2;

            for (int c = 0; c < 3; c++)
            {
                filtered[c] = new Complex[height, width];
                for (int i = 0; i < height; i++)
                {
                    for (int j = 0; j < width; j++)
                    {
                        filtered[c][i, j] = dft[c][i, j];

                        bool isInAnyCircle = false;

                        foreach (var circle in circles)
                        {
                            int circleCenterX = circle.X + centerX;
                            int circleCenterY = circle.Y + centerY;

                            double dx = j - circleCenterX;
                            double dy = i - circleCenterY;
                            double distance = Math.Sqrt(dx * dx + dy * dy);

                            if (circle.RadiusInner == 0 && circle.RadiusOuter == 0)
                            {
                                if (distance <= circle.Radius)
                                {
                                    isInAnyCircle = true;
                                    break;
                                }
                            }
                            else
                            {
                                if (distance >= circle.RadiusInner && distance <= circle.RadiusOuter)
                                {
                                    isInAnyCircle = true;
                                    break;
                                }
                            }
                        }
                        if (isInAnyCircle)
                        {
                            filtered[c][i, j] = Complex.Zero;
                        }
                    }
                }
            }

            return filtered;
        }
        #endregion
        #region Кнопки управления
        private bool CheckDPFPower()
        {
            try
            {
                powderDPF = int.Parse(DPFPower.Text);
                if (powderDPF != 0 && powderDPF > 0)
                {
                    return true;
                }
                else
                    return false;
            }
            catch
            {
                return false;
            }
        }
        private void DFTButton_Click(object sender, EventArgs e)
        {
            if (image != null)
            {
                if (!CheckDPFPower())
                {
                    MessageBox.Show("Введите нормальное число!");
                    return;
                }

                fourierImage = ComputeDFT2D(image, CenteringCheckBox.Checked);
                Bitmap visualization = VisualizeFourierImage(fourierImage);
                FourierPictureBox.Image = visualization;
                BackUpFUrie = visualization;

                if (!RadiusTextBox.Visible)
                {
                    RadiusTextBox.Visible = true;
                    label1.Visible = true;
                    FilterComboBox.Visible = true;
                }
            }
            else
            {
                MessageBox.Show("Пожалуйста, сначала загрузите изображение.");
            }
        }
        private void ApplyFilterButton_Click(object sender, EventArgs e)
        {
            if (fourierImage != null)
            {
                string selectedFilter = FilterComboBox.SelectedItem.ToString();

                if (selectedFilter == "Низкочастотная" || selectedFilter == "Высокочастотная")
                {
                    if (!int.TryParse(RadiusTextBox.Text, out radius1))
                    {
                        MessageBox.Show("Пожалуйста, введите корректный радиус.");
                        return;
                    }
                    radius2 = 0;
                }
                else if (selectedFilter == "Режекторный" || selectedFilter == "Полосовой")
                {
                    if (!int.TryParse(RadiusTextBox.Text, out radius1) || !int.TryParse(textBox2.Text, out radius2))
                    {
                        MessageBox.Show("Пожалуйста, введите корректные радиусы.");
                        return;
                    }
                }
                else if (selectedFilter == "Узкополосный полосовой" || selectedFilter == "Узкополосный режекторный")
                {
                    circleParams = ParseCircleParameters(textBox1.Text);
                    if (circleParams == null)
                    {
                        MessageBox.Show("Ошибка в формате параметров кругов.");
                        return;
                    }
                }

                Complex[][,] filteredImage = ApplyFilter(fourierImage, selectedFilter, radius1, radius2);
                Bitmap resultImage = ComputeIDFT2D(filteredImage, CenteringCheckBox.Checked);

                resultImage2 = resultImage;
                OriginalPictureBox.Image = resultImage;
            }
            else
            {
                MessageBox.Show("Пожалуйста, сначала выполните ДПФ.");
            }
        }
        private void Visual_Click(object sender, EventArgs e)
        {
            if (FourierPictureBox.Image != null)
            {
                Bitmap vizualBitmap = new Bitmap(BackUpFUrie);
                Graphics g = Graphics.FromImage(vizualBitmap);

                int centerX = vizualBitmap.Width / 2;
                int centerY = vizualBitmap.Height / 2;
                Pen circlePen = new Pen(Color.White, 2);
                string selectedFilter = FilterComboBox.SelectedItem.ToString();
                if (selectedFilter == "Низкочастотная" || selectedFilter == "Высокочастотная")
                {
                    if (int.TryParse(RadiusTextBox.Text, out radius1))
                    {
                        g.DrawEllipse(circlePen, centerX - radius1, centerY - radius1, 2 * radius1, 2 * radius1);
                    }
                    else
                    {
                        MessageBox.Show("Пожалуйста, введите корректный радиус.");
                        return;
                    }

                    radius2 = 0;
                }
                else if (selectedFilter == "Режекторный" || selectedFilter == "Полосовой")
                {
                    if (int.TryParse(RadiusTextBox.Text, out radius1) && int.TryParse(textBox2.Text, out radius2))
                    {
                        g.DrawEllipse(circlePen, centerX - radius1, centerY - radius1, 2 * radius1, 2 * radius1);
                        g.DrawEllipse(circlePen, centerX - radius2, centerY - radius2, 2 * radius2, 2 * radius2);
                    }
                    else
                    {
                        MessageBox.Show("Пожалуйста, введите корректные радиусы.");
                        return;
                    }
                }
                else if (selectedFilter == "Узкополосный полосовой" || selectedFilter == "Узкополосный режекторный")
                {
                    circleParams = ParseCircleParameters(textBox1.Text);

                    if (circleParams == null)
                    {
                        MessageBox.Show("Ошибка в формате параметров кругов.");
                        return;
                    }
                    foreach (var circle in circleParams)
                    {
                        int shiftedX = circle.X + centerX;
                        int shiftedY = circle.Y + centerY;

                        if (circle.RadiusInner != 0 || circle.RadiusOuter != 0)
                        {
                            g.DrawEllipse(circlePen, (shiftedX) - circle.RadiusInner, (shiftedY) - circle.RadiusInner, 2 * circle.RadiusInner, 2 * circle.RadiusInner);
                            g.DrawEllipse(circlePen, (shiftedX) - circle.RadiusOuter, (shiftedY) - circle.RadiusOuter, 2 * circle.RadiusOuter, 2 * circle.RadiusOuter);
                        }
                        else
                            g.DrawEllipse(circlePen, (shiftedX) - circle.Radius, (shiftedY) - circle.Radius, 2 * circle.Radius, 2 * circle.Radius);
                    }
                }
                else
                {
                    MessageBox.Show("Пожалуйста, выберите фильтр.");
                    return;
                }
                FourierPictureBox.Image = vizualBitmap;
                circlePen.Dispose();
                g.Dispose();
            }
        }
        #endregion
        private void FilterComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            string filtr = FilterComboBox.SelectedItem.ToString();
            switch (filtr)
            {
                case "Низкочастотная":
                    label5.Visible = false;
                    textBox1.Visible = false;
                    label1.Visible = true;
                    RadiusTextBox.Visible = true;
                    label6.Visible = false;
                    textBox2.Visible = false;
                    FourierPictureBox.Image = BackUpFUrie;
                    break;
                case "Высокочастотная":
                    label5.Visible = false;
                    textBox1.Visible = false;
                    label1.Visible = true;
                    RadiusTextBox.Visible = true;
                    label6.Visible = false;
                    textBox2.Visible = false;
                    FourierPictureBox.Image = BackUpFUrie;
                    break;
                case "Режекторный":
                    label5.Visible = false;
                    textBox1.Visible = false;
                    label1.Visible = true;
                    RadiusTextBox.Visible = true;
                    label6.Visible = true;
                    textBox2.Visible = true;
                    FourierPictureBox.Image = BackUpFUrie;
                    break;
                case "Полосовой":
                    label5.Visible = false;
                    textBox1.Visible = false;
                    label1.Visible = true;
                    RadiusTextBox.Visible = true;
                    label6.Visible = true;
                    textBox2.Visible = true;
                    FourierPictureBox.Image = BackUpFUrie;
                    break;
                case "Узкополосный полосовой":
                    textBox1.Text = "";
                    label5.Visible = true;
                    textBox1.Visible = true;
                    label1.Visible = false;
                    RadiusTextBox.Visible = false;
                    label6.Visible = false;
                    textBox2.Visible = false;
                    FourierPictureBox.Image = BackUpFUrie;
                    break;
                case "Узкополосный режекторный":
                    textBox1.Text = "";
                    label5.Visible = true;
                    textBox1.Visible = true;
                    label1.Visible = false;
                    RadiusTextBox.Visible = false;
                    label6.Visible = false;
                    textBox2.Visible = false;
                    FourierPictureBox.Image = BackUpFUrie;
                    break;
            }
        }
        private List<CircleParams> ParseCircleParameters(string text)
        {
            List<CircleParams> circles = new List<CircleParams>();
            string[] lines = text.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

            foreach (string line in lines)
            {
                string[] values = line.Split(';');

                if (values.Length == 3 && values.All(v => !string.IsNullOrWhiteSpace(v)))
                {
                    if (int.TryParse(values[0], out int x) &&
                        int.TryParse(values[1], out int y) &&
                        int.TryParse(values[2], out int radius))
                    {
                        circles.Add(new CircleParams(x, y, radius));
                    }
                    else
                        return null;
                }
                else if (values.Length == 4 && values.All(v => !string.IsNullOrWhiteSpace(v)))
                {
                    if (int.TryParse(values[0], out int x) &&
                        int.TryParse(values[1], out int y) &&
                        int.TryParse(values[2], out int radiusInner) &&
                        int.TryParse(values[3], out int radiusOuter))
                    {
                        circles.Add(new CircleParams(x, y, radiusInner, radiusOuter));
                    }
                    else
                        return null;
                }
                else
                    return null;
            }
            return circles;
        }
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (!checkBox1.Checked)
            {
                OriginalPictureBox.Image = BackUpImage;
            }
            if (checkBox1.Checked)
            {
                OriginalPictureBox.Image = resultImage2;
            }
        }
    }
}
public class CircleParams
{
    public int X { get; set; }
    public int Y { get; set; }
    public int Radius { get; set; }

    public int RadiusInner { get; set; }
    public int RadiusOuter { get; set; }

    public CircleParams(int x, int y, int radius)
    {
        X = x;
        Y = y;
        Radius = radius;
        RadiusInner = 0;
        RadiusOuter = 0;
    }
    public CircleParams(int x, int y, int radiusOuter, int radiusInner)
    {
        X = x;
        Y = y;
        Radius = 0;
        RadiusInner = radiusInner;
        RadiusOuter = radiusOuter;
    }
}