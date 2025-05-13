using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;

namespace ImgApp_2_WinForms
{
    public partial class Binar : Form
    {
        Bitmap image = null;
        Bitmap BackUpImage = null;
        int windowSize = 15; //Для всех ниже перечисленных
        double k = -0.2; //Для Ниблека
        int R = 128; //Для Саувола
        double a = 0.5; //Для Вуйльфа
        double t = 0.15; //Для Бредли

        public Binar()
        {
            InitializeComponent();
            pictureBox1.AllowDrop = true;
            pictureBox1.DragEnter += pictureBox1_DragEnter;
            pictureBox1.DragDrop += pictureBox1_DragDrop;
            this.AutoScroll = true;
        }
        public Binar(Bitmap _image)
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

        private Bitmap GradacGray(Bitmap inputImage)
        {
            Bitmap Gray = new Bitmap(inputImage.Width, inputImage.Height);

            for (int x = 0; x < inputImage.Width; x++)
            {
                for (int y = 0; y < inputImage.Height; y++)
                {
                    Color pixelColor = inputImage.GetPixel(x, y);
                    int a = pixelColor.A;
                    int r = pixelColor.R;
                    int g = pixelColor.G;
                    int b = pixelColor.B;

                    int GrayScale = (int)(0.2125 * r + 0.7154 * g + 0.0721 * b);

                    Color newColor = Color.FromArgb(a, GrayScale, GrayScale, GrayScale);
                    Gray.SetPixel(x, y, newColor);
                }
            }

            return Gray;
        }

        private bool Checker()
        {
            if (pictureBox1.Image == null)
            {
                MessageBox.Show("Нет изображения!");
                return false;
            }

            if (krita.Text == "" || kritk.Text == "" || kritR.Text == "" || kritt.Text == "" || WidowSize.Text == "")
            {
                MessageBox.Show("Поля критериев пусты!");
                return false;
            }
            else
            {
                windowSize = int.Parse(WidowSize.Text);
                R = int.Parse(kritR.Text);

                if (kritk.Text.Contains("."))
                {
                    string kk = kritk.Text;
                    kk = kk.Replace(".", ",");
                    k = double.Parse(kk);
                }
                else
                    k = double.Parse(kritk.Text);
                if (krita.Text.Contains("."))
                {
                    string aa = krita.Text;
                    aa = aa.Replace(".", ",");
                    a = double.Parse(aa);
                }
                else
                    a = double.Parse(krita.Text);
                if (kritt.Text.Contains("."))
                {
                    string tt = kritt.Text;
                    tt = tt.Replace(".", ",");
                    t = double.Parse(tt);
                }
                else
                    t = double.Parse(kritt.Text);

                return true;
            }
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

            windowSize = 15;
            k = -0.2;
            R = 128;
            a = 0.5;
            t = 0.15;
            //(krita.Text == "" || kritk.Text == "" || kritR.Text == "" || kritt.Text == "" || WidowSize.Text == "")
            krita.Text = a.ToString();
            kritk.Text = k.ToString();
            kritR.Text = R.ToString();
            kritt.Text = t.ToString();
            WidowSize.Text = windowSize.ToString(); 
        }
        #endregion
        #region Гаврилов
        private void Gavrilov_Click(object sender, EventArgs e)
        {
            Bitmap grayImage = GradacGray(image);
            Bitmap binaryImage = new Bitmap(grayImage.Width, grayImage.Height);
            double threshold = AVGPixel(grayImage);

            for (int x = 0; x < grayImage.Width; x++)
            {
                for (int y = 0; y < grayImage.Height; y++)
                {
                    Color pixelColor = grayImage.GetPixel(x, y);
                    int intensity = pixelColor.R;

                    Color newColor = (intensity <= threshold) ? Color.Black : Color.White;
                    binaryImage.SetPixel(x, y, newColor);
                }
            }
            pictureBox1.Image = binaryImage;
        }
        private static double AVGPixel(Bitmap image)
        {
            double totalIntensity = 0;

            for (int x = 0; x < image.Width; x++)
            {
                for (int y = 0; y < image.Height; y++)
                {
                    Color pixelColor = image.GetPixel(x, y);
                    totalIntensity += pixelColor.R;
                }
            }

            return totalIntensity / (image.Width * image.Height);
        }
        #endregion
        //????
        #region Отсу
        private void Otsu_Click(object sender, EventArgs e)
        {
            Bitmap grayImage = GradacGray(image);
            Bitmap binaryImage = new Bitmap(grayImage.Width, grayImage.Height);
            int[] histogram = new int[256];

            for (int x = 0; x < image.Width; x++)
            {
                for (int y = 0; y < image.Height; y++)
                {
                    Color pixelColor = image.GetPixel(x, y);
                    int intensity = pixelColor.R;
                    histogram[intensity]++;
                }
            }

            int threshold = OtsuThreshold(histogram, grayImage.Width * grayImage.Height);
            
            for (int x = 0; x < grayImage.Width; x++)
            {
                for (int y = 0; y < grayImage.Height; y++)
                {
                    Color pixelColor = grayImage.GetPixel(x, y);
                    int intensity = pixelColor.R;

                    Color newColor = (intensity <= threshold) ? Color.Black : Color.White;
                    binaryImage.SetPixel(x, y, newColor);
                }
            }
            pictureBox1.Image = binaryImage;
        }
        private static int OtsuThreshold(int[] histogram, int totalPixels)
        {
            double sum = 0;
            for (int i = 0; i < 256; i++)
                sum += i * histogram[i];

            double sumB = 0, varMax = 0;
            int wB = 0, wF = 0, threshold = 0;

            for (int t = 0; t < 256; t++)
            {
                wB += histogram[t];
                if (wB == 0) 
                    continue;
                wF = totalPixels - wB;
                if (wF == 0) 
                    break;

                sumB += (double)(t * histogram[t]);
                double mB = sumB / wB;
                double mF = (sum - sumB) / wF;

                double varBetween = (double)wB * (double)wF * (mB - mF) * (mB - mF);

                if (varBetween > varMax)
                {
                    varMax = varBetween;
                    threshold = t;
                }
            }

            return threshold;
        }
        #endregion
        #region Ниблек
        private unsafe void Nibelek_Click(object sender, EventArgs e)
        {
            if (!Checker())
                return;
            // 1. Преобразуем в градации серого (если еще не преобразовано)
            Bitmap grayImage = GradacGray(image);

            // 2. Создаем новое бинарное изображение
            Bitmap binaryImage = new Bitmap(grayImage.Width, grayImage.Height);

            // 3. Блокируем биты для быстрого доступа
            BitmapData grayData = grayImage.LockBits(new Rectangle(0, 0, grayImage.Width, grayImage.Height), ImageLockMode.ReadOnly, PixelFormat.Format8bppIndexed);
            BitmapData binaryData = binaryImage.LockBits(new Rectangle(0, 0, binaryImage.Width, binaryImage.Height), ImageLockMode.WriteOnly, PixelFormat.Format8bppIndexed);

            try
            {
                // Получаем указатели на данные
                IntPtr grayPtr = grayData.Scan0;
                IntPtr binaryPtr = binaryData.Scan0;

                int stride = grayData.Stride;
                int width = grayImage.Width;
                int height = grayImage.Height;

                // 4. Для каждого пикселя вычисляем локальный порог
                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++)
                    {
                        // Вычисляем границы окна
                        int x1 = Math.Max(0, x - windowSize / 2);
                        int y1 = Math.Max(0, y - windowSize / 2);
                        int x2 = Math.Min(width - 1, x + windowSize / 2);
                        int y2 = Math.Min(height - 1, y + windowSize / 2);

                        // Вычисляем среднее значение и стандартное отклонение в окне
                        double mean = 0;
                        double stdDev = 0;
                        CalculateMeanAndStdDev(grayPtr, stride, x1, y1, x2, y2, out mean, out stdDev);

                        // Вычисляем локальный порог
                        double threshold = mean + k * stdDev;

                        // Получаем значение интенсивности текущего пикселя
                        byte intensity = ((byte*)grayPtr + y * stride)[x];

                        // Применяем пороговое значение
                        ((byte*)binaryPtr + y * stride)[x] = (intensity <= threshold) ? (byte)0 : (byte)255;
                    }
                }
            }
            finally
            {
                // Обязательно разблокируем биты!
                grayImage.UnlockBits(grayData);
                binaryImage.UnlockBits(binaryData);
            }

            pictureBox1.Image = binaryImage;
        }

        // Вспомогательная функция для вычисления среднего и стандартного отклонения в окне
        private static unsafe void CalculateMeanAndStdDev(IntPtr ptr, int stride, int x1, int y1, int x2, int y2, out double mean, out double stdDev)
        {
            double sum = 0;
            double sumSq = 0;
            int count = 0;

            for (int y = y1; y <= y2; y++)
            {
                for (int x = x1; x <= x2; x++)
                {
                    byte intensity = ((byte*)ptr + y * stride)[x];
                    sum += intensity;
                    sumSq += intensity * intensity;
                    count++;
                }
            }

            mean = sum / count;
            stdDev = Math.Sqrt((sumSq / count) - (mean * mean));
        }
        #endregion
        #region Саувола
        private unsafe void Sauwol_Click(object sender, EventArgs e)
        {
            if (!Checker())
                return;
            // 1. Преобразуем в градации серого (если еще не преобразовано)
            Bitmap grayImage = GradacGray(image);

            // 2. Создаем новое бинарное изображение
            Bitmap binaryImage = new Bitmap(grayImage.Width, grayImage.Height, PixelFormat.Format8bppIndexed);

            // 3. Блокируем биты для быстрого доступа
            BitmapData grayData = grayImage.LockBits(new Rectangle(0, 0, grayImage.Width, grayImage.Height), ImageLockMode.ReadOnly, PixelFormat.Format8bppIndexed);
            BitmapData binaryData = binaryImage.LockBits(new Rectangle(0, 0, binaryImage.Width, binaryImage.Height), ImageLockMode.WriteOnly, PixelFormat.Format8bppIndexed);

            try
            {
                // Получаем указатели на данные
                IntPtr grayPtr = grayData.Scan0;
                IntPtr binaryPtr = binaryData.Scan0;

                int stride = grayData.Stride;
                int width = grayImage.Width;
                int height = grayImage.Height;

                // 4. Для каждого пикселя вычисляем локальный порог
                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++)
                    {
                        // Вычисляем границы окна
                        int x1 = Math.Max(0, x - windowSize / 2);
                        int y1 = Math.Max(0, y - windowSize / 2);
                        int x2 = Math.Min(width - 1, x + windowSize / 2);
                        int y2 = Math.Min(height - 1, y + windowSize / 2);

                        // Вычисляем среднее значение и стандартное отклонение в окне
                        double mean = 0;
                        double stdDev = 0;
                        CalculateMeanAndStdDev(grayPtr, stride, x1, y1, x2, y2, out mean, out stdDev);

                        // Вычисляем локальный порог (Sauvola)
                        double threshold = mean * (1 + k * ((stdDev / R) - 1));

                        // Получаем значение интенсивности текущего пикселя
                        byte intensity = ((byte*)grayPtr + y * stride)[x];

                        // Применяем пороговое значение
                        ((byte*)binaryPtr + y * stride)[x] = (intensity <= threshold) ? (byte)0 : (byte)255;
                    }
                }
            }
            finally
            {
                // Обязательно разблокируем биты!
                grayImage.UnlockBits(grayData);
                binaryImage.UnlockBits(binaryData);
            }

            pictureBox1.Image = binaryImage;
        }
        #endregion
        #region Вульф
        private unsafe void Wolf_Click(object sender, EventArgs e)
        {
            if (!Checker())
                return;
            // 1. Преобразуем в градации серого (если еще не преобразовано)
            Bitmap grayImage = GradacGray(image);

            // 2. Создаем новое бинарное изображение
            Bitmap binaryImage = new Bitmap(grayImage.Width, grayImage.Height);

            // 3. Блокируем биты изображений
            BitmapData grayData = grayImage.LockBits(new Rectangle(0, 0, grayImage.Width, grayImage.Height), ImageLockMode.ReadOnly, PixelFormat.Format8bppIndexed);
            BitmapData binaryData = binaryImage.LockBits(new Rectangle(0, 0, binaryImage.Width, binaryImage.Height), ImageLockMode.WriteOnly, PixelFormat.Format8bppIndexed);

            try
            {
                IntPtr grayPtr = grayData.Scan0;
                IntPtr binaryPtr = binaryData.Scan0;

                int stride = grayData.Stride;
                int width = grayImage.Width;
                int height = grayImage.Height;

                // 4. Вычисляем максимальное стандартное отклонение по всем окнам
                double maxStdDev = CalculateMaxStdDev(grayPtr, stride, width, height, windowSize);

                // 5. Вычисляем минимальную интенсивность пикселей изображения (самый темный пиксель)
                byte minIntensity = CalculateMinIntensity(grayPtr, stride, width, height);

                // 6. Для каждого пикселя вычисляем локальный порог и бинаризуем
                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++)
                    {
                        // Вычисляем границы окна
                        int x1 = Math.Max(0, x - windowSize / 2);
                        int y1 = Math.Max(0, y - windowSize / 2);
                        int x2 = Math.Min(width - 1, x + windowSize / 2);
                        int y2 = Math.Min(height - 1, y + windowSize / 2);

                        // Вычисляем среднее значение и стандартное отклонение в окне
                        double mean, stdDev;
                        CalculateMeanAndStdDev(grayPtr, stride, x1, y1, x2, y2, out mean, out stdDev);

                        // Вычисляем локальный порог по формуле Вульфа
                        double threshold = (1 - a) * mean + a * minIntensity + a * (stdDev / maxStdDev) * (mean - minIntensity);

                        // Получаем значение интенсивности текущего пикселя
                        byte intensity = ((byte*)grayPtr + y * stride)[x];

                        // Применяем пороговое значение
                        ((byte*)binaryPtr + y * stride)[x] = (intensity <= threshold) ? (byte)0 : (byte)255;
                    }
                }
            }
            finally
            {
                grayImage.UnlockBits(grayData);
                binaryImage.UnlockBits(binaryData);
            }

            pictureBox1.Image = binaryImage;
        }

        // Вспомогательные функции (CalculateMeanAndStdDev, ConvertToGrayscale и др.)
        // Должны быть определены в этом же классе, как в предыдущих примерах

        // Вспомогательная функция для вычисления максимального стандартного отклонения
        private static unsafe double CalculateMaxStdDev(IntPtr ptr, int stride, int width, int height, int windowSize)
        {
            double maxStdDev = 0;

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    int x1 = Math.Max(0, x - windowSize / 2);
                    int y1 = Math.Max(0, y - windowSize / 2);
                    int x2 = Math.Min(width - 1, x + windowSize / 2);
                    int y2 = Math.Min(height - 1, y + windowSize / 2);

                    double mean, stdDev;
                    CalculateMeanAndStdDev(ptr, stride, x1, y1, x2, y2, out mean, out stdDev);

                    maxStdDev = Math.Max(maxStdDev, stdDev);
                }
            }

            return maxStdDev;
        }

        // Вспомогательная функция для вычисления минимальной интенсивности пикселей
        private static unsafe byte CalculateMinIntensity(IntPtr ptr, int stride, int width, int height)
        {
            byte minIntensity = 255; // Начинаем с максимального значения

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    byte intensity = ((byte*)ptr + y * stride)[x];
                    minIntensity = Math.Min(minIntensity, intensity);
                }
            }

            return minIntensity;
        }
        
        #endregion
        #region Бредли
        private unsafe void BredLy_Rot_Click(object sender, EventArgs e)
        {
            if (!Checker())
                return;

            // 1. Преобразуем в градации серого (если еще не преобразовано)
            Bitmap grayImage = GradacGray(image);

            // 2. Создаем интегральное изображение
            int[,] integralImage = CalculateIntegralImage(grayImage);

            // 3. Создаем новое бинарное изображение
            Bitmap binaryImage = new Bitmap(grayImage.Width, grayImage.Height);

            // 4. Блокируем биты для быстрого доступа
            BitmapData grayData = grayImage.LockBits(new Rectangle(0, 0, grayImage.Width, grayImage.Height), ImageLockMode.ReadOnly, PixelFormat.Format8bppIndexed);
            BitmapData binaryData = binaryImage.LockBits(new Rectangle(0, 0, binaryImage.Width, binaryImage.Height), ImageLockMode.WriteOnly, PixelFormat.Format8bppIndexed);

            try
            {
                IntPtr grayPtr = grayData.Scan0;
                IntPtr binaryPtr = binaryData.Scan0;

                int stride = grayData.Stride;
                int width = grayImage.Width;
                int height = grayImage.Height;

                // 5. Для каждого пикселя вычисляем локальный порог
                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++)
                    {
                        // Вычисляем границы окна
                        int x1 = Math.Max(0, x - windowSize / 2);
                        int y1 = Math.Max(0, y - windowSize / 2);
                        int x2 = Math.Min(width - 1, x + windowSize / 2);
                        int y2 = Math.Min(height - 1, y + windowSize / 2);

                        // Вычисляем количество пикселей в окне
                        int count = (x2 - x1 + 1) * (y2 - y1 + 1);

                        // Вычисляем сумму пикселей в окне с использованием интегрального изображения
                        int sum = CalculateSumUsingIntegralImage(integralImage, x1, y1, x2, y2);

                        // Вычисляем порог
                        double threshold = (sum * (1 - t)) / count;

                        // Получаем значение интенсивности текущего пикселя
                        byte intensity = ((byte*)grayPtr + y * stride)[x];

                        // Применяем пороговое значение
                        ((byte*)binaryPtr + y * stride)[x] = (intensity <= threshold) ? (byte)0 : (byte)255;
                    }
                }
            }
            finally
            {
                // Обязательно разблокируем биты!
                grayImage.UnlockBits(grayData);
                binaryImage.UnlockBits(binaryData);
            }

            pictureBox1.Image = binaryImage;
        }

        // Вспомогательная функция для вычисления интегрального изображения
        private static int[,] CalculateIntegralImage(Bitmap image)
        {
            int width = image.Width;
            int height = image.Height;
            int[,] integralImage = new int[width, height];

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    Color pixelColor = image.GetPixel(x, y);
                    int intensity = pixelColor.R; // Grayscale image

                    integralImage[x, y] = intensity;

                    if (x > 0)
                        integralImage[x, y] += integralImage[x - 1, y];
                    if (y > 0)
                        integralImage[x, y] += integralImage[x, y - 1];
                    if (x > 0 && y > 0)
                        integralImage[x, y] -= integralImage[x - 1, y - 1];
                }
            }

            return integralImage;
        }

        private static int CalculateSumUsingIntegralImage(int[,] integralImage, int x1, int y1, int x2, int y2)
        {
            int sum = integralImage[x2, y2];

            if (x1 > 0)
                sum -= integralImage[x1 - 1, y2];
            if (y1 > 0)
                sum -= integralImage[x2, y1 - 1];
            if (x1 > 0 && y1 > 0)
                sum += integralImage[x1 - 1, y1 - 1];
            return sum;
        }
        #endregion
    }
}
