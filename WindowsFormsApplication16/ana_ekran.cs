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
using System.Data.OleDb;

namespace WindowsFormsApplication16
{
    public partial class ana_ekran : Form
    {
        public string sifre, cinsiyet, rutbe, dogum_tarihi , eposta;
        public int id;
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

        public ana_ekran()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 20, 20));
        }

        string cevrimici = "";
        OpenFileDialog dosya = new OpenFileDialog();
        string DosyaYolu;
        private void ana_ekran_Load(object sender, EventArgs e)
        {
            timer1.Start();

            if (rutbe == "Yönetici")
            {
                panel11.Visible = true;
            }

            if (rutbe == "Kullanıcı")
            {
                panel11.Visible = false;
            }

            OleDbConnection baglanti = new OleDbConnection("Provider=Microsoft.JET.OLEDB.4.0;Data Source=database.mdb");
            baglanti.Open();
            OleDbCommand komut = new OleDbCommand();
            komut.CommandText = "Select * from kullanici where kullanici_adi='"+label7.Text+"'";
            komut.Connection = baglanti;
            OleDbDataReader oku = komut.ExecuteReader();

            if (oku.Read())
            {

                cevrimici = oku["cevrimici_durumu"].ToString();
                DosyaYolu = oku["profil_fotograf"].ToString();
                CirclePictureBox1.ImageLocation = DosyaYolu;

                if (cevrimici == "Çevrimiçi")
                {
                    pictureBox1.Image = Resource1.online;
                    pictureBox1.Location = new Point(65, 123);
                }

                if (cevrimici == "Boşta")
                {
                    pictureBox1.Image = Resource1.boşta;
                    pictureBox1.Location = new Point(75, 123);
                }

                if (cevrimici == "Rahatsız Etmeyin")
                {
                    pictureBox1.Image = Resource1.rahatsız_etme;
                    pictureBox1.Location = new Point(47, 123);
                }

                if (cevrimici == "Görünmez")
                {
                    pictureBox1.Image = Resource1.görünmez;
                    pictureBox1.Location = new Point(66, 123);
                }

                baglanti.Close();
            }

            label3.Text = "Hello " + label7.Text + " ,";

            ToolTip aciklama = new ToolTip();
            aciklama.ShowAlways = true;

            aciklama.SetToolTip(label1, "Close");
            aciklama.SetToolTip(label2, "Shrink");
            aciklama.SetToolTip(pictureBox16, "Homepage");
            aciklama.SetToolTip(pictureBox17, "Elektronic Chat Application!");
            aciklama.SetToolTip(pictureBox15, "Persons");
            aciklama.SetToolTip(pictureBox14, "Messages");
            aciklama.SetToolTip(pictureBox13, "Account Options");
            aciklama.SetToolTip(pictureBox11, "Logout");
            aciklama.SetToolTip(pictureBox12, "Settings");
        }

        private void label1_Click(object sender, EventArgs e)
        {
            OleDbConnection baglanti = new OleDbConnection("Provider=Microsoft.JET.OLEDB.4.0;Data Source=database.mdb");
            baglanti.Open();
            OleDbCommand komut1 = new OleDbCommand();
            komut1.CommandText = "UPDATE kullanici set cevrimici_durumu='Çevrimdışı' WHERE kullanici_adi='" + label7.Text + "'";
            komut1.Connection = baglanti;
            komut1.ExecuteNonQuery();
            baglanti.Close();
            Application.Exit();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_MouseDown(object sender, MouseEventArgs e)
        {
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox6_Click_1(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Are you sure to Logout?", "UYARI", MessageBoxButtons.YesNo);
            if (dialogResult == System.Windows.Forms.DialogResult.Yes)
            {
                Form1 cikis_nesne = new Form1();
                cikis_nesne.Show();
                this.Close();
            }

            else
            {

            }
            
        }

        private void label6_Click(object sender, EventArgs e)
        {
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            hesap_ayarlari hsp_ayarlari = new hesap_ayarlari();
            hsp_ayarlari.Show();
            this.Hide();
        }

        private void panel5_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox16_Click(object sender, EventArgs e)
        {
            panel4.Visible = true;
        }

        private void pictureBox13_Click(object sender, EventArgs e)
        {
            
        }

        private void pictureBox11_Click(object sender, EventArgs e)
        {
            
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
            label1.ForeColor = Color.Black;
        }

        private void panel3_MouseHover(object sender, EventArgs e)
        {
            pictureBox16.Size = new Size(35, 35);
            panel3.BackColor = Color.DodgerBlue;
            label8.ForeColor = Color.White;
        }

        private void panel3_MouseLeave(object sender, EventArgs e)
        {
            pictureBox16.Size = new Size(30, 30);
            panel3.BackColor = Color.DodgerBlue;
            label8.ForeColor = Color.White;
        }

        private void panel2_MouseHover(object sender, EventArgs e)
        {
            pictureBox13.Size = new Size(35, 35);
            panel2.BackColor = Color.DodgerBlue;
            label14.ForeColor = Color.White;
        }

        private void panel2_MouseLeave(object sender, EventArgs e)
        {
            pictureBox13.Size = new Size(30, 30);
            panel2.BackColor = SystemColors.Control;
            label14.ForeColor = Color.Black;
        }

        private void panel2_Paint_1(object sender, PaintEventArgs e)
        {
            
        }

        private void panel2_Click(object sender, EventArgs e)
        {
            hesap_ayarlari hesap_Ayar = new hesap_ayarlari();
            hesap_Ayar.id = Convert.ToInt16(id);
            hesap_Ayar.label6.Text = label7.Text;
            hesap_Ayar.textBox3.Text = (label7.Text + "#" + id).ToString();
            hesap_Ayar.textBox8.Text = sifre;
            hesap_Ayar.textBox6.Text = cinsiyet.ToString();
            hesap_Ayar.textBox4.Text = eposta.ToString();
            hesap_Ayar.textBox5.Text = dogum_tarihi.ToString();
            hesap_Ayar.textBox7.Text = rutbe.ToString();

            hesap_Ayar.Show();
            this.Close();
        }

        private void panel1_MouseHover(object sender, EventArgs e)
        {
            pictureBox15.Size = new Size(35, 35);
            panel1.BackColor = Color.DodgerBlue;
            label21.ForeColor = Color.White;
        }

        private void panel1_MouseLeave(object sender, EventArgs e)
        {
            pictureBox15.Size = new Size(30, 30);
            panel1.BackColor = SystemColors.Control;
            label21.ForeColor = Color.Black;
        }

        private void panel8_MouseHover(object sender, EventArgs e)
        {
            pictureBox14.Size = new Size(35, 35);
            panel8.BackColor = Color.DodgerBlue;
            label22.ForeColor = Color.White;
        }

        private void panel8_MouseLeave(object sender, EventArgs e)
        {
            pictureBox14.Size = new Size(30, 30);
            panel8.BackColor = SystemColors.Control;
            label22.ForeColor = Color.Black;
        }

        private void panel9_MouseHover(object sender, EventArgs e)
        {
            pictureBox12.Size = new Size(35, 35);
            panel9.BackColor = Color.DodgerBlue;
            label23.ForeColor = Color.White;
        }

        private void panel9_MouseLeave(object sender, EventArgs e)
        {
            pictureBox12.Size = new Size(30, 30);
            panel9.BackColor = SystemColors.Control;
            label23.ForeColor = Color.Black;
        }

        private void panel10_MouseHover(object sender, EventArgs e)
        {
            pictureBox11.Size = new Size(35, 35);
            panel10.BackColor = Color.DodgerBlue;
            label24.ForeColor = Color.White;
        }

        private void panel10_MouseLeave(object sender, EventArgs e)
        {
            pictureBox11.Size = new Size(30, 30);
            panel10.BackColor = SystemColors.Control;
            label24.ForeColor = Color.Black;
        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel10_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Are you sure to Logout", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (dialogResult == DialogResult.Yes)
            {

                OleDbConnection baglanti = new OleDbConnection("Provider=Microsoft.JET.OLEDB.4.0;Data Source=database.mdb");
                baglanti.Open();
                OleDbCommand komut1 = new OleDbCommand();
                komut1.CommandText = "UPDATE kullanici set cevrimici_durumu='Çevrimdışı' WHERE kullanici_adi='" + label7.Text + "'";
                komut1.Connection = baglanti;
                komut1.ExecuteNonQuery();
                baglanti.Close();

                Form1 cikis_Yap = new Form1();
                cikis_Yap.Show();
                this.Close();
            }
            else
            {
                //null
            }
        }

        private void panel10_Paint(object sender, PaintEventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (cevrimici == "Çevrimiçi")
            {
                pictureBox1.Image = Resource1.online;
                pictureBox1.Location = new Point(65, 123);
            }

            if (cevrimici == "Boşta")
            {
                pictureBox1.Image = Resource1.boşta;
                pictureBox1.Location = new Point(75, 123);
            }

            if (cevrimici == "Rahatsız Etmeyin")
            {
                pictureBox1.Image = Resource1.rahatsız_etme;
                pictureBox1.Location = new Point(47, 123);
            }

            if (cevrimici == "Görünmez")
            {
                pictureBox1.Image = Resource1.görünmez;
                pictureBox1.Location = new Point(66, 123);
            }
        }

        private void panel9_Paint(object sender, PaintEventArgs e)
        {
            
        }

        private void label23_Click(object sender, EventArgs e)
        {

        }

        private void panel9_Click(object sender, EventArgs e)
        {
            yardim yardim_nesne = new yardim();
            yardim_nesne.id = Convert.ToInt16(id);
            yardim_nesne.label6.Text = label7.Text;
            yardim_nesne.sifre = sifre;
            yardim_nesne.cinsiyet = cinsiyet.ToString();
            yardim_nesne.eposta = eposta.ToString();
            yardim_nesne.dogum_tarihi = dogum_tarihi.ToString();
            yardim_nesne.rutbe = rutbe.ToString();

            yardim_nesne.Show();
            this.Close();
        }
        bool dragging;
        Point offset;
        private void panel7_MouseDown(object sender, MouseEventArgs e)
        {
            dragging = true;
            offset = e.Location;
        }

        private void panel7_MouseMove(object sender, MouseEventArgs e)
        {
            if (dragging)
            {
                Point currentScreenPos = PointToScreen(e.Location);
                Location = new
                Point(currentScreenPos.X - offset.X,
                currentScreenPos.Y - offset.Y);
            }
        }

        private void panel7_MouseUp(object sender, MouseEventArgs e)
        {
            dragging = false;
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel7_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel11_MouseHover(object sender, EventArgs e)
        {
            pictureBox2.Size = new Size(35, 35);
            panel11.BackColor = Color.DodgerBlue;
            label18.ForeColor = Color.White;
        }

        private void panel11_MouseLeave(object sender, EventArgs e)
        {
            pictureBox2.Size = new Size(30, 30);
            panel11.BackColor = SystemColors.Control;
            label18.ForeColor = Color.Black;
        }

        private void panel11_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label14_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Click(object sender, EventArgs e)
        {
            kişiler kişiler_nesne = new kişiler();
            kişiler_nesne.id = Convert.ToInt16(id);
            kişiler_nesne.label6.Text = label7.Text;
            kişiler_nesne.sifre = sifre;
            kişiler_nesne.cinsiyet = cinsiyet.ToString();
            kişiler_nesne.eposta = eposta.ToString();
            kişiler_nesne.dogum_tarihi = dogum_tarihi.ToString();
            kişiler_nesne.rutbe = rutbe.ToString();

            kişiler_nesne.Show();
            this.Close();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel8_Click(object sender, EventArgs e)
        {
            mesajlar mesajlar_nesne = new mesajlar();
            mesajlar_nesne.id = Convert.ToInt16(id);
            mesajlar_nesne.label6.Text = label7.Text;
            mesajlar_nesne.sifre = sifre;
            mesajlar_nesne.cinsiyet = cinsiyet.ToString();
            mesajlar_nesne.eposta = eposta.ToString();
            mesajlar_nesne.dogum_tarihi = dogum_tarihi.ToString();
            mesajlar_nesne.rutbe = rutbe.ToString();

            mesajlar_nesne.Show();
            this.Close();
        }

        private void panel11_Click(object sender, EventArgs e)
        {
            yoneticipanel_anasayfa yonetici_nesne = new yoneticipanel_anasayfa();
            yonetici_nesne.id = Convert.ToInt16(id);
            yonetici_nesne.label4.Text = label7.Text;
            yonetici_nesne.sifre = sifre;
            yonetici_nesne.cinsiyet = cinsiyet.ToString();
            yonetici_nesne.eposta = eposta.ToString();
            yonetici_nesne.dogum_tarihi = dogum_tarihi.ToString();
            yonetici_nesne.rutbe = rutbe.ToString();

            yonetici_nesne.Show();
            this.Close();
        }
    }
}
