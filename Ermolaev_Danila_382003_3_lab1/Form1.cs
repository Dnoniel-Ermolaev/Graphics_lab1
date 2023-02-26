using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.ComponentModel;
using System.Drawing.Imaging;

namespace Ermolaev_Danila_382003_3_lab1
{
    public partial class Form1 : Form
    {
        Bitmap image;
        List<Bitmap> ListBm = new List<Bitmap>();
        int index = -1;
        bool filters = false;
        Color pixelColor;
        Color truepixelColor;
        public Form1()
        {
            InitializeComponent();        
        }

        private void открытьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Image files|*.png;*.jpg;*.bmp|All files(*.*)|*.*";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                image = new Bitmap(dialog.FileName);
                ListBm.Add(image);
                index++;
            }
            pictureBox1.Image = image;
            pictureBox1.Refresh();
        }

        private void инверсияToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Filters filter = new InvertFilter();
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            backgroundWorker1.CancelAsync();
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            Bitmap newImage = ((Filters)e.Argument).processImage(image, backgroundWorker1);
            if (backgroundWorker1.CancellationPending != true)
                image = newImage;
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (!e.Cancelled)
            {
                ListBm.Add(image);
                index++;
                pictureBox1.Image = image;
                pictureBox1.Refresh();
            }
            progressBar1.Value = 0;
        }

        private void размытиеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Filters filter = new BlurFilter();
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void размытиеГауссаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Filters filter = new GaussianFilter();
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void полутонToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Filters filter = new GrayScaleFilter();
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void сепияToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Filters filter = new SepiaFilter();
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void собеляпоОсиYToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Filters filter = new SobelFilter();
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void резкостьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Filters filter = new SharpnessFilter();
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void яркостьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Filters filter = new BrightFilter();
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void тиснениеEmbossingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Filters filter = new EmbossingFilter();
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void переносToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Filters filter = new TransferFilter();
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void поворотToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Filters filter = new TurnFilter();
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void волныToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Filters filter = new WaveFilter();
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void стеклоToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Filters filter = new GlassFilter();
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void резкость20ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Filters filter = new SharpnessFilter2();
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void выделениеГраницToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Filters filter = new BorderHighlightingSharr();
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void button2_Click(object sender, EventArgs e)
        {

            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "JPeg Image|*.jpg|Bitmap Image|*.bmp|Gif Image|*.gif";
            saveFileDialog1.Title = "Save an Image File";
            ImageFormat format= ImageFormat.Png;
            if (saveFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string ext = System.IO.Path.GetExtension(saveFileDialog1.FileName);
                switch (ext)
                {
                    case ".jpg":
                        format = ImageFormat.Jpeg;
                        break;
                    case ".bmp":
                        format = ImageFormat.Bmp;
                        break;
                }
                pictureBox1.Image.Save(saveFileDialog1.FileName, format);
            }
        }

        private void saveFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
         
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (index > 0)
            {
                ListBm.RemoveAt(index);
                index--;
                image = ListBm[index];
                pictureBox1.Image = ListBm[index];
                pictureBox1.Refresh();
            }
            else
            {
                MessageBox.Show("Больше нельзя отменять действия");
            }
        }

        private void медианаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Filters filter = new MedianFilter(2);
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void светящиесяКраяToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Filters filter = new BorderLightMedianFilter();
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void серыйМирToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Filters filter = new GrayWorldFilter();
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void идеальныйОтражательToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Filters filter = new ReflectorFilter();
            backgroundWorker1.RunWorkerAsync(filter);
        }
        private Color GetColorAt(Point point)
        {
            return ((Bitmap)pictureBox1.Image).GetPixel(point.X, point.Y);
        }

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {  
            if (filters)
            {
                if (e.Button == MouseButtons.Left)
                    pixelColor = GetColorAt(e.Location);
                MessageBox.Show("Выберите из палитры настоящий цвет");
                if (colorDialog1.ShowDialog() == DialogResult.OK)
                {
                    Color tmp = colorDialog1.Color;
                    truepixelColor = tmp;
                }
                Filters filter = new CorrectingColorFilter(pixelColor,truepixelColor);
                backgroundWorker1.RunWorkerAsync(filter);
                filters = false;

            }
        }

        private void коррекцияСОпорнымЦветомToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Укажите на пиксель, цвет которого будет считаться опорным.");
            filters = true;

        }

        private void линейноеРастяжениеГистограммыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Filters filter = new LinearStretch();
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void dilationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int[,] structuring_element = new int[3, 3];
            try
            {
                structuring_element[0, 0] = (int)Convert.ToDouble(textBox1.Text);
                structuring_element[0, 1] = (int)Convert.ToDouble(textBox2.Text);
                structuring_element[0, 2] = (int)Convert.ToDouble(textBox3.Text);
                structuring_element[1, 0] = (int)Convert.ToDouble(textBox4.Text);
                structuring_element[1, 1] = (int)Convert.ToDouble(textBox5.Text);
                structuring_element[1, 2] = (int)Convert.ToDouble(textBox6.Text);
                structuring_element[2, 0] = (int)Convert.ToDouble(textBox7.Text);
                structuring_element[2, 1] = (int)Convert.ToDouble(textBox8.Text);
                structuring_element[2, 2] = (int)Convert.ToDouble(textBox9.Text);
                Filters filter = new Dilation(structuring_element);
                backgroundWorker1.RunWorkerAsync(filter);
            }
            catch
            {
                MessageBox.Show("Заполните все поля структурного элемента правильно!\n Проследите, чтобы всё было заполнено целыми числами.");
            }
        }

        private void erosionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int[,] structuring_element = new int[3, 3];
            try
            {
                structuring_element[0, 0] = (int)Convert.ToDouble(textBox1.Text);
                structuring_element[0, 1] = (int)Convert.ToDouble(textBox2.Text);
                structuring_element[0, 2] = (int)Convert.ToDouble(textBox3.Text);
                structuring_element[1, 0] = (int)Convert.ToDouble(textBox4.Text);
                structuring_element[1, 1] = (int)Convert.ToDouble(textBox5.Text);
                structuring_element[1, 2] = (int)Convert.ToDouble(textBox6.Text);
                structuring_element[2, 0] = (int)Convert.ToDouble(textBox7.Text);
                structuring_element[2, 1] = (int)Convert.ToDouble(textBox8.Text);
                structuring_element[2, 2] = (int)Convert.ToDouble(textBox9.Text);
                Filters filter = new Erosion(structuring_element);
                backgroundWorker1.RunWorkerAsync(filter);
            }
            catch
            {
                MessageBox.Show("Заполните все поля структурного элемента правильно!\n Проследите, чтобы всё было заполнено целыми числами.");
            }
        }

        private void openingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int[,] structuring_element = new int[3, 3];
            try
            {
                structuring_element[0, 0] = (int)Convert.ToDouble(textBox1.Text);
                structuring_element[0, 1] = (int)Convert.ToDouble(textBox2.Text);
                structuring_element[0, 2] = (int)Convert.ToDouble(textBox3.Text);
                structuring_element[1, 0] = (int)Convert.ToDouble(textBox4.Text);
                structuring_element[1, 1] = (int)Convert.ToDouble(textBox5.Text);
                structuring_element[1, 2] = (int)Convert.ToDouble(textBox6.Text);
                structuring_element[2, 0] = (int)Convert.ToDouble(textBox7.Text);
                structuring_element[2, 1] = (int)Convert.ToDouble(textBox8.Text);
                structuring_element[2, 2] = (int)Convert.ToDouble(textBox9.Text);
                Filters filter = new Opening(structuring_element);
                backgroundWorker1.RunWorkerAsync(filter);
            }
            catch
            {
                MessageBox.Show("Заполните все поля структурного элемента правильно!\n Проследите, чтобы всё было заполнено целыми числами.");
            }
        }

        private void closingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int[,] structuring_element = new int[3, 3];
            try
            {
                structuring_element[0, 0] = (int)Convert.ToDouble(textBox1.Text);
                structuring_element[0, 1] = (int)Convert.ToDouble(textBox2.Text);
                structuring_element[0, 2] = (int)Convert.ToDouble(textBox3.Text);
                structuring_element[1, 0] = (int)Convert.ToDouble(textBox4.Text);
                structuring_element[1, 1] = (int)Convert.ToDouble(textBox5.Text);
                structuring_element[1, 2] = (int)Convert.ToDouble(textBox6.Text);
                structuring_element[2, 0] = (int)Convert.ToDouble(textBox7.Text);
                structuring_element[2, 1] = (int)Convert.ToDouble(textBox8.Text);
                structuring_element[2, 2] = (int)Convert.ToDouble(textBox9.Text);
                Filters filter = new Closing(structuring_element);
                backgroundWorker1.RunWorkerAsync(filter);
            }
            catch
            {
                MessageBox.Show("Заполните все поля структурного элемента правильно!\n Проследите, чтобы всё было заполнено целыми числами.");
            }
        }

        private void GradToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int[,] structuring_element = new int[3, 3];
            try
            {
                structuring_element[0, 0] = (int)Convert.ToDouble(textBox1.Text);
                structuring_element[0, 1] = (int)Convert.ToDouble(textBox2.Text);
                structuring_element[0, 2] = (int)Convert.ToDouble(textBox3.Text);
                structuring_element[1, 0] = (int)Convert.ToDouble(textBox4.Text);
                structuring_element[1, 1] = (int)Convert.ToDouble(textBox5.Text);
                structuring_element[1, 2] = (int)Convert.ToDouble(textBox6.Text);
                structuring_element[2, 0] = (int)Convert.ToDouble(textBox7.Text);
                structuring_element[2, 1] = (int)Convert.ToDouble(textBox8.Text);
                structuring_element[2, 2] = (int)Convert.ToDouble(textBox9.Text);
                Filters filter = new Grad(structuring_element);
                backgroundWorker1.RunWorkerAsync(filter);
            }
            catch
            {
                MessageBox.Show("Заполните все поля структурного элемента правильно!\n Проследите, чтобы всё было заполнено целыми числами.");
            }
        }

        private void label10_Click(object sender, EventArgs e)
        {

        }
    }
}
