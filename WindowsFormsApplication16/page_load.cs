using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace WindowsFormsApplication16
{
    public partial class page_load : Form
    {
        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn
        (
            int nLeftRect,     // x-coordinate of upper-left corner
            int nTopRect,      // y-coordinate of upper-left corner
            int nRightRect,    // x-coordinate of lower-right corner
            int nBottomRect,   // y-coordinate of lower-right corner
            int nWidthEllipse, // height of ellipse
            int nHeightEllipse // width of ellipse
        );

        int sayac = 0;

        public page_load()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 20, 20));
        }

        private void page_load_Load(object sender, EventArgs e)
        {
            circularProgressBar1.Value = 0;
            timer1.Start();

            ToolTip aciklama = new ToolTip();
            aciklama.ShowAlways = true;

            aciklama.SetToolTip(label1, "Close");
            aciklama.SetToolTip(label2, "Recuve");
            aciklama.SetToolTip(pictureBox1, "Elektronic Chat Application!");

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            sayac++;
            circularProgressBar1.Text = "%" + sayac.ToString();
            circularProgressBar1.Value = sayac;

            if (sayac == 100)
            {
                timer1.Stop();
                Form1 nesne = new Form1();
                nesne.Show();
                this.Hide();
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void label1_MouseHover(object sender, EventArgs e)
        {
            label1.ForeColor = Color.Red;
        }

        private void label1_MouseLeave(object sender, EventArgs e)
        {
            label1.ForeColor = Color.Black;
        }

        private void label2_MouseHover(object sender, EventArgs e)
        {
            label2.ForeColor = Color.Red;
        }

        private void label2_MouseLeave(object sender, EventArgs e)
        {
            label2.ForeColor = Color.Black;
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}
