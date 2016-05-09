using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Pw.n.js
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void generateButton_Click(object sender, EventArgs e)
        {
            try
            {
                var image = this.getImageFromBase64();
                cookieImage.Image = image;

                var bitmap = new Bitmap(image);

                long h = 0;
                for(var i=31;i>=0;i--)
                {
                    var c = bitmap.GetPixel(i, 0);
                    h = h * 2;

                    var d = this.colorToInt(c);

                    bitmapTextbox.Text += d.ToString();
                    if(d==1)
                    {
                        h++;
                    }
                }
                cookie.Text = h.ToString();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }

            


        }

        public Image getImageFromBase64 ()
        {
            string base64 = base64textbox.Text;
            if(String.IsNullOrEmpty(base64))
            {
                throw new ArgumentNullException("Wrong base64 format");
            }

            byte[] bytes = Convert.FromBase64String(base64);

            Image image;
            using (MemoryStream ms = new MemoryStream(bytes))
            {
                image = Image.FromStream(ms);
            }

            return image;
        }

        /* Returns 0 if black 1 if white */
        public int colorToInt(Color color)
        {
            return color.R + color.G + color.B == 0 ? 0 : 1;
        }
    }
}
