using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp9
{
public partial class Form1 : Form
    {
        public enum ColorPart
        {
            Red,
            Green,
            Blue,
            Gray
        }

        public Form1()
        {
            InitializeComponent();
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox3.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox4.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox5.SizeMode = PictureBoxSizeMode.Zoom;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Image files (*.BMP, *.JPG, " + "*.GIF, *.PNG)|*.bmp;*.jpg;*.gif;*.png";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Load(dialog.FileName);
                pictureBox2.Image = ToImage(ToByte(pictureBox1.Image, ColorPart.Red), ColorPart.Red);
                pictureBox3.Image = ToImage(ToByte(pictureBox1.Image, ColorPart.Green), ColorPart.Green);
                pictureBox4.Image = ToImage(ToByte(pictureBox1.Image, ColorPart.Blue), ColorPart.Blue);
                pictureBox5.Image = ToImage(ToByte(pictureBox1.Image, ColorPart.Gray), ColorPart.Gray);
            }
        }
        public int[,] ToByte(Image img, ColorPart part)
        {
            var bmp = new Bitmap(img);
            var mass = new int[bmp.Width, bmp.Height];
            for (var y = 0; y < img.Height; y++)
            {
                for (var x = 0; x < img.Width; x++)
                {
                    switch (part)
                    {
                        case ColorPart.Red:
                            mass[x, y] = bmp.GetPixel(x, y).R;
                            break;
                        case ColorPart.Green:
                            mass[x, y] = bmp.GetPixel(x, y).G;
                            break;
                        case ColorPart.Blue:
                            mass[x, y] = bmp.GetPixel(x, y).B;
                            break;
                        case ColorPart.Gray:
                            mass[x, y] = (bmp.GetPixel(x, y).R + bmp.GetPixel(x, y).G + bmp.GetPixel(x, y).B) / 3;
                            break;
                    }
                }
            }
            return mass;
        }

        public Image ToImage(int[,] img, ColorPart part)
        {
            var bmp = new Bitmap(img.GetLength(0), img.GetLength(1));
            for (var y = 0; y < bmp.Height; y++)
            {
                for (var x = 0; x < bmp.Width; x++)
                {
                    switch (part)
                    {
                        case ColorPart.Red:
                            bmp.SetPixel(x, y, Color.FromArgb(img[x, y], 0, 0));
                            break;
                        case ColorPart.Green:
                            bmp.SetPixel(x, y, Color.FromArgb(0, img[x, y], 0));
                            break;
                        case ColorPart.Blue:
                            bmp.SetPixel(x, y, Color.FromArgb(0, 0, img[x, y]));
                            break;
                        case ColorPart.Gray:
                            var value = img[x, y];
                            bmp.SetPixel(x, y, Color.FromArgb(value, value, value));
                            break;
                    }
                }
            }
            return bmp;
        }

    }
}
        
        