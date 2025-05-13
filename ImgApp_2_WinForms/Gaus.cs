using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace ImgApp_2_WinForms
{
    public partial class Gaus : Form
    {
        Bitmap image = null;
        Bitmap BackUpImage = null;

        bool GausSig = false;
        int sigma = 3;

        double[,] matrix = null;

        public Gaus()
        {
            InitializeComponent();
            pictureBox1.AllowDrop = true;
            pictureBox1.DragEnter += pictureBox1_DragEnter;
            pictureBox1.DragDrop += pictureBox1_DragDrop;
            this.AutoScroll = true;
        }
        public Gaus(Bitmap _image)
        {
            InitializeComponent();

            image = _image;
            if (image != null)
            {
                pictureBox1.Image = image;
                pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            }
            pictureBox1.AllowDrop = true;
            pictureBox1.DragEnter += pictureBox1_DragEnter;
            pictureBox1.DragDrop += pictureBox1_DragDrop;
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
                pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке изображения: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                if (image != null)
                {
                    image.Dispose();
                    image = null;
                }
                pictureBox1.Image = null;
            }
        }
        private void DisplayImage()
        {
            if (image == null)
            {
                pictureBox1.Image = null;
                return;
            }

            pictureBox1.Image = image;
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
            pictureBox1.Image = image;
        }
        public static T Clamp<T>(T val, T min, T max) where T : IComparable<T>
        {
            if (val.CompareTo(min) < 0) return min;
            else if (val.CompareTo(max) > 0) return max;
            else return val;
        }
        #endregion

        private double[,] GausCoef(int X, int Y)
        {
            double[,] gausmatrix = new double[X, Y];
            double sum = 0.0;
            int Xcenter = X / 2;
            int Ycenter = Y / 2;

            for (int x = 0; x < X; x++)
            {
                for (int y = 0; y < Y; y++)
                {
                    double exponent = -((x - Xcenter) * (x - Xcenter) + (y - Ycenter) * (y - Ycenter)) / (2 * sigma * sigma);
                    gausmatrix[x, y] = Math.Exp(exponent) / (2 * Math.PI * sigma * sigma);
                    sum += gausmatrix[x, y];
                }
            }

            for (int x = 0; x < X; x++)
            {
                for (int y = 0; y < Y; y++)
                {
                    gausmatrix[x, y] /= sum;
                }
            }

            return gausmatrix;
        }
        
        private void RGauss_CheckedChanged(object sender, EventArgs e)
        {
            if (RGauss.Checked)
            {
                GausSig = true;
                sig.Enabled = true;
                sigma = int.Parse(sig.Text);
            }
            else
            {
                GausSig = false;
                sig.Enabled = false;
            }
        }
        private bool CheckerVatrix()
        {
            if (int.Parse(XMatrix.Text) % 2 == 0 || int.Parse(YMatrix.Text) % 2 == 0)
            {
                MessageBox.Show("Матрица должна быть нечётной!");
                return true;
            }
            else
                return false;
        }
        private void quicksort(int[] a, int p, int r)
        {
            if (p < r)
            {
                int q = partition(a, p, r);
                quicksort(a, p, q - 1);
                quicksort(a, q + 1, r);
            }
        }
        private int partition(int[] a, int p, int r)
        {
            int x = a[r];
            int i = p - 1;
            int tmp;
            for (int j = p; j < r; j++)
            {

                if (a[j] <= x)
                {
                    i++;
                    tmp = a[i];
                    a[i] = a[j];
                    a[j] = tmp;

                }
            }
            tmp = a[r];
            a[r] = a[i + 1];
            a[i + 1] = tmp;
            return (i + 1);

        }
        private static int MirrorIndex(int idx, int max)
        {
            if (idx < 0)
                return -idx;
            if (idx >= max)
                return max - (idx - max + 1);
            return idx;
        }
        private void Filtr_Click(object sender, EventArgs e)
        {
            if (CheckerVatrix())
                return;

            if (comboBox1.Text == "Линейная фильтрация")
            {
                MatrixField(int.Parse(XMatrix.Text), int.Parse(YMatrix.Text), GausSig);
                LineinFiltr();
            }
            else
            {
               MedianFiltr();
            }
        }
        private void change(object sender, EventArgs e)
        {
            if (comboBox1.Text == "Линейная фильтрация")
            {
                RGauss.Visible = true;
                sig.Visible = true;
                label1.Visible = true;
            }
            else
            {
                RGauss.Visible = false;
                sig.Visible = false;
                label1.Visible = false;
            }
        }

        private void LineinFiltr()
        {
            int kHeight = matrix.GetLength(0);
            int kWidth = matrix.GetLength(1);
            int kHalfHeight = kHeight / 2;
            int kHalfWidth = kWidth / 2;

            Bitmap result = new Bitmap(image.Width, image.Height, image.PixelFormat);
            Rectangle rect = new Rectangle(0, 0, image.Width, image.Height);
            BitmapData srcData = image.LockBits(rect, ImageLockMode.ReadOnly, image.PixelFormat);
            BitmapData dstData = result.LockBits(rect, ImageLockMode.WriteOnly, image.PixelFormat);

            int bytesPerPixel = Image.GetPixelFormatSize(image.PixelFormat) / 8;
            int stride = srcData.Stride;
            int height = image.Height;
            int width = image.Width;
            int totalBytes = stride * height;
            byte[] pixelBuffer = new byte[totalBytes];
            byte[] resultBuffer = new byte[totalBytes];

            Marshal.Copy(srcData.Scan0, pixelBuffer, 0, totalBytes);
            image.UnlockBits(srcData);


            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    double[] newValues = new double[bytesPerPixel];
                    for (int c = 0; c < bytesPerPixel; c++)
                    {
                        newValues[c] = 0;
                    }

                    for (int ky = 0; ky < kHeight; ky++)
                    {
                        for (int kx = 0; kx < kWidth; kx++)
                        {
                            int imgX = MirrorIndex(x + kx - kHalfWidth, width);
                            int imgY = MirrorIndex(y + ky - kHalfHeight, height);
                            int idx = imgY * stride + imgX * bytesPerPixel;

                            for (int c = 0; c < bytesPerPixel; c++)
                            {
                                newValues[c] += pixelBuffer[idx + c] * matrix[ky, kx];
                            }
                        }
                    }

                    int resultIdx = y * stride + x * bytesPerPixel;
                    for (int c = 0; c < bytesPerPixel; c++)
                    {
                        int val = (int)Math.Round(newValues[c]);
                        if (val < 0) val = 0;
                        if (val > 255) val = 255;
                        resultBuffer[resultIdx + c] = (byte)val;
                    }
                }
            }

            Marshal.Copy(resultBuffer, 0, dstData.Scan0, totalBytes);
            result.UnlockBits(dstData);
            pictureBox1.Image = result;
        }
        private void MedianFiltr()
        {
            if (image == null)
            {
                throw new ArgumentNullException(nameof(image));
            }

            int windowHeight = int.Parse(YMatrix.Text);
            int windowWidth = int.Parse(XMatrix.Text);

            if (windowWidth <= 0 || windowHeight <= 0)
            {
                throw new ArgumentException("Ширина и высота окна должны быть больше 0.");
            }

            Bitmap filteredImage = new Bitmap(image.Width, image.Height);

            BitmapData sourceData = image.LockBits(new Rectangle(0, 0, image.Width, image.Height), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
            BitmapData targetData = filteredImage.LockBits(new Rectangle(0, 0, filteredImage.Width, filteredImage.Height), ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);

            try
            {
                int width = image.Width;
                int height = image.Height;
                int pixelSize = 4;

                unsafe
                {
                    byte* srcPtr = (byte*)sourceData.Scan0;
                    byte* dstPtr = (byte*)targetData.Scan0;

                    for (int y = 0; y < height; y++)
                    {
                        for (int x = 0; x < width; x++)
                        {
                            List<int> redValues = new List<int>();
                            List<int> greenValues = new List<int>();
                            List<int> blueValues = new List<int>();

                            for (int i = -windowHeight / 2; i <= windowHeight / 2; i++)
                            {
                                for (int j = -windowWidth / 2; j <= windowWidth / 2; j++)
                                {
                                    int nx = x + j;
                                    int ny = y + i;

                                    if (nx >= 0 && nx < width && ny >= 0 && ny < height)
                                    {
                                        byte* neighborPtr = srcPtr + (ny * sourceData.Stride) + (nx * pixelSize);
                                        blueValues.Add(neighborPtr[0]);
                                        greenValues.Add(neighborPtr[1]);
                                        redValues.Add(neighborPtr[2]);
                                    }
                                }
                            }

                            redValues.Sort();
                            greenValues.Sort();
                            blueValues.Sort();

                            byte medianBlue = (byte)blueValues[blueValues.Count / 2];
                            byte medianGreen = (byte)greenValues[greenValues.Count / 2];
                            byte medianRed = (byte)redValues[redValues.Count / 2];

                            byte* currentPtr = dstPtr + (y * targetData.Stride) + (x * pixelSize);
                            currentPtr[0] = medianBlue;
                            currentPtr[1] = medianGreen;
                            currentPtr[2] = medianRed;
                            currentPtr[3] = 255;
                        }
                    }
                }
            }
            finally
            {
                image.UnlockBits(sourceData);
                filteredImage.UnlockBits(targetData);
            }

            pictureBox1.Image = filteredImage;
        }
        
        private void MatrixField(int X, int Y, bool GaussSig)
        {
            if (GausSig)
            {
                if (matrix != null && (matrix.GetLength(0) != X || matrix.GetLength(1) != Y))
                {
                    matrix = new double[X, Y];
                }
                else if (matrix == null)
                {
                    matrix = new double[X, Y];
                }

                matrix = GausCoef(X, Y);
            }
            else
            {
                if (matrix != null && (matrix.GetLength(0) != X || matrix.GetLength(1) != Y))
                {
                    matrix = new double[X, Y];
                }
                else if (matrix == null)
                {
                    matrix = new double[X, Y];
                }
                double value = 1.0 / (X * Y);
                for (int i = 0; i < X; i++)
                {
                    for (int j = 0; j < Y; j++)
                    {
                        matrix[i, j] = value;
                    }
                }
            }
        }
    }
}
