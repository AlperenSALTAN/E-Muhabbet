using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;
using System.Runtime.InteropServices;

namespace WindowsFormsApplication16
{
    public partial class Form1 : Form
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

        public Form1()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 20, 20));
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                textBox2.PasswordChar = '\0';
                checkBox1.Text = "Hide Passwordd";
            }

            else
            {
                textBox2.PasswordChar = '*';
                checkBox1.Text = "Show Password";
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

        private void Form1_Load(object sender, EventArgs e)
        {
            ToolTip aciklama = new ToolTip();
            aciklama.ShowAlways = true;

            aciklama.SetToolTip(label1, "Close");
            aciklama.SetToolTip(label2, "Shrink");
            aciklama.SetToolTip(pictureBox1, "Elektronic Chat Application!");

        }

        private void label1_MouseHover(object sender, EventArgs e)
        {
            label1.Font = new Font(label1.Font, FontStyle.Bold);
            label1.ForeColor = Color.Red;
        }

        private void label1_MouseLeave(object sender, EventArgs e)
        {
            label1.Font = new Font(label1.Font, FontStyle.Regular);
            label1.ForeColor = Color.Black;
        }

        private void label2_MouseHover(object sender, EventArgs e)
        {
            label2.Font = new Font(label2.Font, FontStyle.Bold);
            label2.ForeColor = Color.Red;

        }

        private void label2_MouseLeave(object sender, EventArgs e)
        {
            label2.Font = new Font(label2.Font, FontStyle.Regular);
            label2.ForeColor = Color.Black;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OleDbConnection baglanti = new OleDbConnection("Provider=Microsoft.JET.OLEDB.4.0;Data Source = database.mdb");
            baglanti.Open();
            OleDbCommand komut = new OleDbCommand();
            komut.CommandText = "Select * from kullanici where kullanici_adi='" + textBox1.Text + "' and sifre='" + textBox2.Text + "'";
            komut.Connection = baglanti;

            OleDbDataReader oku = komut.ExecuteReader();

            if (oku.Read())
	        {
                string kullanici_adi = oku["kullanici_adi"].ToString();
                label5.Text = kullanici_adi;
                string sifre = oku["sifre"].ToString();
                string cinsiyet = oku["cinsiyet"].ToString();
                string eposta = oku["eposta"].ToString();
                string dogum_tarihi = oku["dogum_tarihi"].ToString();
                int id = Convert.ToInt16(oku["id"]);
                string rutbe = oku["rutbe"].ToString();
                int ban_durumu = Convert.ToInt16(oku["ban_durumu"]);
                string ban_sebebi = oku["ban_sebebi"].ToString();

                if (ban_durumu == 1)
                {
                    MessageBox.Show("Your Login Has Been Banned Because Your Account Has Been Banned. Why: '" + ban_sebebi + "'", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                }

                else
                {
                    MessageBox.Show("Welcome " + kullanici_adi);
                    ana_ekran giris_nesne = new ana_ekran();
                    giris_nesne.label7.Text = kullanici_adi.ToString();
                    giris_nesne.sifre = sifre.ToString();
                    giris_nesne.cinsiyet = cinsiyet.ToString();
                    giris_nesne.eposta = eposta.ToString();
                    giris_nesne.dogum_tarihi = dogum_tarihi.ToString();
                    giris_nesne.rutbe = rutbe.ToString();
                    giris_nesne.id = Convert.ToInt16(id);

                    OleDbCommand komut1 = new OleDbCommand();
                    komut1.CommandText = "UPDATE kullanici set cevrimici_durumu='Çevrimiçi' where kullanici_adi='"+label5.Text+"'";
                    komut1.Connection = baglanti;
                    komut1.ExecuteNonQuery();
                    baglanti.Close();

                    giris_nesne.Show();
                    this.Hide();

                }
                
	        }
            
            else
             {
                    MessageBox.Show("Username/Email or Password Incorrect");
                    baglanti.Close();
             }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            register nesne = new register();
            nesne.Show();
            this.Hide();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            sifremi_unuttum sifre_nesne = new sifremi_unuttum();
            sifre_nesne.Show();
            this.Hide();
        }

        private void linkLabel1_MouseHover(object sender, EventArgs e)
        {
            linkLabel1.LinkColor = Color.Red;
        }

        private void linkLabel1_MouseLeave(object sender, EventArgs e)
        {
            linkLabel1.LinkColor = Color.FromArgb(0, 0, 0, 192);
        }

        bool dragging;
        Point offset;

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            dragging = true;
            offset = e.Location;
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {

            if (dragging)
            {
                Point currentScreenPos = PointToScreen(e.Location);
                Location = new
                Point(currentScreenPos.X - offset.X,
                currentScreenPos.Y - offset.Y);
            }
        }

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            dragging = false;
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {

        }
       
    }
}
