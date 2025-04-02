using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;
using System.Runtime.InteropServices;

namespace ImgApp_2_WinForms
{
    public partial class Gradac : Form
    {
        private Bitmap image = null;
        private Bitmap BackUp = null;
        private List<PointF> points = new List<PointF>();
        private Pen splinePen = new Pen(Color.Blue, 2);
        private Pen pointPen = new Pen(Color.Red, 6);
        private bool dragging = false;
        private int draggedPointIndex = -1;

        public Gradac()
        {
            InitializeComponent();

            pictureBox1.AllowDrop = true;
            pictureBox1.DragEnter += pictureBox1_DragEnter;
            pictureBox1.DragDrop += pictureBox1_DragDrop;
            this.AutoScroll = true;
            points.Add(new PointF(0, 0));
            points.Add(new PointF(1, 1));
            comboBox1.SelectedIndex = 0;
            comboBox1.Text = "Линейная интерполяция";
        }
        public Gradac(Bitmap _image)
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
            points.Add(new PointF(0, 0));
            points.Add(new PointF(1, 1));
            comboBox1.Text = "Линейная интерполяция";
        }
        private void Back_Click(object sender, EventArgs e)
        {
            this.Close();
        }
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
                BackUp = new Bitmap(imagePath);

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
            Histogtam.Invalidate();
            Grafic.Invalidate();
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
        private void Histogtam_Paint(object sender, PaintEventArgs e)
        {
            if (pictureBox1.Image == null)
                return;

            int[] histogram = new int[256];

            Graphics g = e.Graphics;
            Bitmap bmp = new Bitmap(pictureBox1.Image);
            for (int i = 0; i < bmp.Width; i++)
            {
                for (int j = 0; j < bmp.Height; j++)
                {
                    Color c = bmp.GetPixel(i, j);
                    int grayScale = (int)((c.R * 0.3) + (c.G * 0.59) + (c.B * 0.11));
                    histogram[grayScale]++;
                }
            }
            int maxCount = histogram.Max();

            Pen blackPen = new Pen(Color.Black, 2);

            float scaleFactor = (float)Histogtam.Height / maxCount;

            for (int i = 0; i < 256; i++)
            {
                float barHeight = histogram[i] * scaleFactor;
                float x = i * ((float)Histogtam.Width / 256);
                g.DrawLine(blackPen, x, Histogtam.Height, x, Histogtam.Height - barHeight);
            }
            blackPen.Dispose();
        }
        private void pictureBoxGraph_Paint(object sender, PaintEventArgs e)
        {
            if (pictureBox1.Image == null)
                return;

            Graphics g = e.Graphics;

            DrawAxis(g);

            if (comboBox1.Text == "Линейная интерполяция")
            {
                DrawSpline(g);
            }
            else
            {
                DrawCubicSplain(g);
            }
            
            DrawControlPoints(g);

        }
        private void DrawAxis(Graphics g)
        {
            g.DrawLine(Pens.Black, 20, Grafic.Height - 20, Grafic.Width - 20, Grafic.Height - 20);
            g.DrawLine(Pens.Black, 20, Grafic.Height - 20, 20, 20);

            g.DrawString("0", Font, Brushes.Black, 5, Grafic.Height - 20);
            g.DrawString("1", Font, Brushes.Black, Grafic.Width - 25, Grafic.Height - 20);
            g.DrawString("1", Font, Brushes.Black, 5, 10);
        }
        private void DrawControlPoints(Graphics g)
        {
            float scaleX = (float)(Grafic.Width - 40);
            float scaleY = (float)(Grafic.Height - 40);

            foreach (PointF point in points)
            {
                float x = 20 + point.X * scaleX;
                float y = Grafic.Height - 20 - point.Y * scaleY;
                g.DrawEllipse(pointPen, x - 3, y - 3, 6, 6);
            }

            textBox1.Text = points.Count.ToString();
        }
        private void DrawCubicSplain(Graphics g)
        {
            if (points.Count < 2)
                return;

            List<PointF> sortedPoints = points.OrderBy(p => p.X).ToList();

            Bitmap bmp = new Bitmap(Grafic.Width, Grafic.Height);

            g.SmoothingMode = SmoothingMode.AntiAlias;

            List<PointF> bezierPoints = new List<PointF>();
            for (int i = 0; i < sortedPoints.Count - 1; i++)
            {
                PointF p0 = sortedPoints[i];
                PointF p1 = sortedPoints[i + 1];

                float controlPointDistance = (p1.X - p0.X) / 3;

                PointF c1 = new PointF(p0.X + controlPointDistance, p0.Y);
                PointF c2 = new PointF(p1.X - controlPointDistance, p1.Y);

                float scaleX = (float)(Grafic.Width - 40);
                float scaleY = (float)(Grafic.Height - 40);

                float x0 = 20 + p0.X * scaleX;
                float y0 = Grafic.Height - 20 - p0.Y * scaleY;
                float xc1 = 20 + c1.X * scaleX;
                float yc1 = Grafic.Height - 20 - c1.Y * scaleY;
                float xc2 = 20 + c2.X * scaleX;
                float yc2 = Grafic.Height - 20 - c2.Y * scaleY;
                float x1 = 20 + p1.X * scaleX;
                float y1 = Grafic.Height - 20 - p1.Y * scaleY;

                bezierPoints.Add(new PointF(x0, y0));
                bezierPoints.Add(new PointF(xc1, yc1));
                bezierPoints.Add(new PointF(xc2, yc2));
                bezierPoints.Add(new PointF(x1, y1));
            }
            for (int i = 0; i < bezierPoints.Count - 1; i += 4)
                g.DrawBezier(splinePen, bezierPoints[i], bezierPoints[i + 1], bezierPoints[i + 2], bezierPoints[i + 3]);
        }
        private void DrawSpline(Graphics g)
        {
            float scaleX = (float)(Grafic.Width - 40);
            float scaleY = (float)(Grafic.Height - 40);

            if (points.Count < 2)
                return;

            points.Sort((p1, p2) => p1.X.CompareTo(p2.X));

            PointF prevPoint = points[0];
            for (float x = 0; x <= 1; x += 0.01f)
            {
                float y = Interpolate(x);

                float scaledX = 20 + x * scaleX;
                float scaledY = Grafic.Height - 20 - y * scaleY;

                g.DrawLine(splinePen, 20 + prevPoint.X * scaleX, Grafic.Height - 20 - prevPoint.Y * scaleY, scaledX, scaledY);
                prevPoint = new PointF(x, y);
            }
        }
        private float Interpolate(float x)
        {
            if (points.Count == 0)
                return 0;
            if (x <= points[0].X)
                return points[0].Y;
            if (x >= points[points.Count - 1].X)
                return points[points.Count - 1].Y;

            for (int i = 0; i < points.Count - 1; i++)
            {
                if (x >= points[i].X && x <= points[i + 1].X)
                {
                    float t = (x - points[i].X) / (points[i + 1].X - points[i].X);
                    return points[i].Y + t * (points[i + 1].Y - points[i].Y);
                }
            }
            return 0;
        }
        private void pictureBoxGraph_MouseDown(object sender, MouseEventArgs e)
        {
            float scaleX = (float)(Grafic.Width - 40);
            float scaleY = (float)(Grafic.Height - 40);

            for (int i = 0; i < points.Count; i++)
            {
                float x = 20 + points[i].X * scaleX;
                float y = Grafic.Height - 20 - points[i].Y * scaleY;
                if (Math.Abs(e.X - x) <= 5 && Math.Abs(e.Y - y) <= 5)
                {
                    if (i == 0 || i == points.Count - 1)
                    {
                        dragging = false;
                        draggedPointIndex = -1;
                        break;
                    }
                    else
                    {
                        dragging = true;
                        draggedPointIndex = i;
                        break;
                    }
                }
            }
        }
        private void pictureBoxGraph_MouseMove(object sender, MouseEventArgs e)
        {
            if (dragging && draggedPointIndex != -1)
            {
                float scaleX = (float)(Grafic.Width - 40);
                float scaleY = (float)(Grafic.Height - 40);
                float x = Math.Max(0, Math.Min(1, (e.X - 20) / scaleX));
                float y = Math.Max(0, Math.Min(1, (Grafic.Height - 20 - e.Y) / scaleY));
                points[draggedPointIndex] = new PointF(x, y);
                Grafic.Invalidate();
                ApplyGradaction();
            }
        }
        private void pictureBoxGraph_MouseUp(object sender, MouseEventArgs e)
        {
            dragging = false;
            draggedPointIndex = -1;
        }
        private void pictureBoxGraph_DoubleClick(object sender, EventArgs e)
        {
            MouseEventArgs me = (MouseEventArgs)e;
            float scaleX = (float)(Grafic.Width - 40);
            float scaleY = (float)(Grafic.Height - 40);
            float x = Math.Max(0, Math.Min(1, (me.X - 20) / scaleX));
            float y = Math.Max(0, Math.Min(1, (Grafic.Height - 20 - me.Y) / scaleY));

            points.Add(new PointF(x, y));
            Grafic.Invalidate();
            ApplyGradaction();
        }
        private void pictureBoxGraph_SizeChanged(object sender, EventArgs e)
        {
            Grafic.Invalidate();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            points.Clear();
            points.Add(new PointF(0, 0));
            points.Add(new PointF(1, 1));

            image = BackUp;
            pictureBox1.Image = image;
            Grafic.Invalidate();
        }
        private void ApplyGradaction()
        {
            if (image == null || BackUp == null)
                return;

            Bitmap originalImage = new Bitmap(BackUp);
            Bitmap newImage = new Bitmap(originalImage.Width, originalImage.Height, originalImage.PixelFormat);

            List<PointF> sortedPoints = points.OrderBy(p => p.X).ToList();

            byte[] lutR = new byte[256];
            byte[] lutG = new byte[256];
            byte[] lutB = new byte[256];

            for (int i = 0; i < 256; i++)
            {
                float normalizedIntensity = (float)i / 255.0f;
                float newIntensity = 0;

                if (comboBox1.Text == "Линейная интерполяция")
                {
                    newIntensity = Interpolate(normalizedIntensity);
                }
                else
                {
                    Dictionary<float, float> splineValues = new Dictionary<float, float>();

                    for (int j = 0; j < sortedPoints.Count - 1; j++)
                    {
                        PointF p0 = sortedPoints[j];
                        PointF p1 = sortedPoints[j + 1];

                        float controlPointDistance = (p1.X - p0.X) / 3;

                        PointF c1 = new PointF(p0.X + controlPointDistance, p0.Y);
                        PointF c2 = new PointF(p1.X - controlPointDistance, p1.Y);

                        for (float t = 0; t <= 1; t += 0.01f)
                        {
                            float intensity = (1 - t) * (1 - t) * (1 - t) * p0.X +
                                              3 * (1 - t) * (1 - t) * t * c1.X +
                                              3 * (1 - t) * t * t * c2.X +
                                              t * t * t * p1.X;

                            float value = (1 - t) * (1 - t) * (1 - t) * p0.Y +
                                          3 * (1 - t) * (1 - t) * t * c1.Y +
                                          3 * (1 - t) * t * t * c2.Y +
                                          t * t * t * p1.Y;

                            splineValues[intensity] = value;
                        }
                    }
                    float normalizedIntensityLookup = (float)i / 255.0f;
                    if (splineValues.ContainsKey(normalizedIntensityLookup))
                    {
                        newIntensity = splineValues[normalizedIntensityLookup];
                    }
                    else
                    {
                        float closestIntensity = splineValues.Keys.OrderBy(v => Math.Abs(v - normalizedIntensityLookup)).First();
                        newIntensity = splineValues[closestIntensity];
                    }
                }

                int newGrayScale = (int)(newIntensity * 255);
                byte newValue = (byte)Math.Max(0, Math.Min(255, newGrayScale));
                lutR[i] = newValue;
                lutG[i] = newValue;
                lutB[i] = newValue;
            }

            BitmapData originalData = originalImage.LockBits(new Rectangle(0, 0, originalImage.Width, originalImage.Height), ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb); // Или Format8bppIndexed, если изображение изначально в оттенках серого
            BitmapData newData = newImage.LockBits(new Rectangle(0, 0, newImage.Width, newImage.Height), ImageLockMode.WriteOnly, PixelFormat.Format24bppRgb);

            try
            {
                IntPtr originalPtr = originalData.Scan0;
                IntPtr newPtr = newData.Scan0;

                int bytes = Math.Abs(originalData.Stride) * originalImage.Height;
                byte[] originalValues = new byte[bytes];
                byte[] newValues = new byte[bytes];

                Marshal.Copy(originalPtr, originalValues, 0, bytes);

                for (int y = 0; y < originalImage.Height; y++)
                {
                    int rowOffset = y * originalData.Stride;
                    for (int x = 0; x < originalImage.Width; x++)
                    {
                        int colOffset = x * 3;

                        byte originalB = originalValues[rowOffset + colOffset];
                        byte originalG = originalValues[rowOffset + colOffset + 1];
                        byte originalR = originalValues[rowOffset + colOffset + 2];

                        newValues[rowOffset + colOffset] = lutB[originalB]; 
                        newValues[rowOffset + colOffset + 1] = lutG[originalG];
                        newValues[rowOffset + colOffset + 2] = lutR[originalR];
                    }
                }
                Marshal.Copy(newValues, 0, newPtr, bytes);
            }
            finally
            {
                originalImage.UnlockBits(originalData);
                newImage.UnlockBits(newData);
            }
            image = newImage;
            pictureBox1.Image = image;
            DisplayImage();
        }
    }
}