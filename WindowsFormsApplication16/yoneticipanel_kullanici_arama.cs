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
    public partial class yoneticipanel_kullanici_arama : Form
    {
        public string sifre, cinsiyet, rutbe, dogum_tarihi, eposta;
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

        public yoneticipanel_kullanici_arama()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 20, 20));
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void bunifuMaterialTextbox1_DoubleClick(object sender, EventArgs e)
        {
            bunifuMaterialTextbox1.Text = "";
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void bunifuThinButton21_Click(object sender, EventArgs e)
        {
            OleDbConnection baglanti = new OleDbConnection("Provider=Microsoft.JET.OLEDB.4.0;Data Source=database.mdb");
            baglanti.Open();
            OleDbCommand ara_komut = new OleDbCommand();
            ara_komut.CommandText = "Select * from kullanici where kullanici_adi='" + bunifuMaterialTextbox1.Text + "' ";
            ara_komut.Connection = baglanti;
            OleDbDataReader aranan_oku = ara_komut.ExecuteReader();


            if (aranan_oku.Read())
            {
                bunifuFlatButton7.Visible = true;
                bunifuFlatButton8.Visible = false;
                panel6.Visible = true;

                string aranan_fotograf = aranan_oku["profil_fotograf"].ToString();
                string aranan_kullanici_adi = aranan_oku["kullanici_adi"].ToString();
                string aranan_dogum_tarihi = aranan_oku["dogum_tarihi"].ToString();
                string aranan_cevrimici_durum = aranan_oku["cevrimici_durumu"].ToString();
                string aranan_e_posta = aranan_oku["eposta"].ToString();
                string aranan_cinsiyet = aranan_oku["cinsiyet"].ToString();

                CirclePictureBox3.ImageLocation = aranan_fotograf.ToString();
                textBox1.Text = aranan_kullanici_adi.ToString();
                textBox2.Text = aranan_e_posta.ToString();
                textBox3.Text = aranan_cinsiyet.ToString();
                textBox4.Text = aranan_dogum_tarihi.ToString();

                if (aranan_kullanici_adi == label4.Text)
                {
                    bunifuThinButton22.Visible = false;
                    bunifuThinButton23.Visible = false;
                    textBox1.Enabled = false;
                    textBox2.Enabled = false;
                    textBox3.Enabled = false;
                    textBox4.Enabled = false;
                    bunifuDropdown1.Enabled = false;
                }

                else
                {
                    bunifuThinButton22.Visible = true;
                    bunifuThinButton23.Visible = true;
                    textBox1.Enabled = false;
                    textBox2.Enabled = true;
                    textBox3.Enabled = true;
                    textBox4.Enabled = true;
                    bunifuDropdown1.Enabled = true;
                }
            }

            else
            {
                panel6.Visible = false;
                bunifuFlatButton7.Visible = false;
                bunifuFlatButton8.Visible = true;
            }
        }

        string cevrimici = "", DosyaYolu = "";

        private void yoneticipanel_kullanici_arama_Load(object sender, EventArgs e)
        {
            ToolTip aciklama = new ToolTip();
            aciklama.ShowAlways = true;

            aciklama.SetToolTip(label3, "Close");
            aciklama.SetToolTip(label2, "Recurve");
            aciklama.SetToolTip(bunifuFlatButton1, "Dashboard");
            aciklama.SetToolTip(bunifuFlatButton2, "Users");
            aciklama.SetToolTip(bunifuFlatButton4, "Staffs");
            aciklama.SetToolTip(bunifuFlatButton3, "Helps");
            aciklama.SetToolTip(label1, "e-Muhabbet ~ House Keeping");

            OleDbConnection baglanti = new OleDbConnection("Provider=Microsoft.JET.OLEDB.4.0;Data Source=database.mdb");
            baglanti.Open();
            OleDbCommand komut = new OleDbCommand();
            komut.CommandText = "Select * from kullanici where kullanici_adi='" + label4.Text + "'";
            komut.Connection = baglanti;
            OleDbDataReader oku = komut.ExecuteReader();

            if (oku.Read())
            {
                cevrimici = oku["cevrimici_durumu"].ToString();
                DosyaYolu = oku["profil_fotograf"].ToString();
                CirclePictureBox2.ImageLocation = DosyaYolu;

                if (cevrimici == "Çevrimiçi")
                {
                    pictureBox12.Image = Resource1.online;
                }

                if (cevrimici == "Boşta")
                {
                    pictureBox12.Image = Resource1.boşta;
                }

                if (cevrimici == "Rahatsız Etmeyin")
                {
                    pictureBox12.Image = Resource1.rahatsız_etme;
                }

                if (cevrimici == "Görünmez")
                {
                    pictureBox12.Image = Resource1.görünmez;
                }
            }

            baglanti.Close();
        }

        private void bunifuFlatButton5_Click(object sender, EventArgs e)
        {
            ana_ekran ana_nesne = new ana_ekran();
            ana_nesne.label7.Text = label4.Text.ToString();
            ana_nesne.sifre = sifre;
            ana_nesne.cinsiyet = cinsiyet;
            ana_nesne.eposta = eposta;
            ana_nesne.dogum_tarihi = dogum_tarihi;
            ana_nesne.rutbe = rutbe;
            ana_nesne.id = Convert.ToInt16(id);

            ana_nesne.Show();
            this.Hide();
        }

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            yoneticipanel_anasayfa anasayfa_nesne = new yoneticipanel_anasayfa();
            anasayfa_nesne.label4.Text = label4.Text.ToString();
            anasayfa_nesne.sifre = sifre;
            anasayfa_nesne.cinsiyet = cinsiyet;
            anasayfa_nesne.eposta = eposta;
            anasayfa_nesne.dogum_tarihi = dogum_tarihi;
            anasayfa_nesne.rutbe = rutbe;
            anasayfa_nesne.id = Convert.ToInt16(id);

            anasayfa_nesne.Show();
            this.Hide();
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            yoneticipanel_kullanicilar kullanici_nesne = new yoneticipanel_kullanicilar();
            kullanici_nesne.label4.Text = label4.Text.ToString();
            kullanici_nesne.sifre = sifre;
            kullanici_nesne.cinsiyet = cinsiyet;
            kullanici_nesne.eposta = eposta;
            kullanici_nesne.dogum_tarihi = dogum_tarihi;
            kullanici_nesne.rutbe = rutbe;
            kullanici_nesne.id = Convert.ToInt16(id);

            kullanici_nesne.Show();
            this.Hide();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            OleDbConnection baglanti = new OleDbConnection("Provider=Microsoft.JET.OLEDB.4.0;Data Source=database.mdb");
            baglanti.Open();
            OleDbCommand komut1 = new OleDbCommand();
            komut1.CommandText = "UPDATE kullanici set cevrimici_durumu='Çevrimdışı' WHERE kullanici_adi='" + label4.Text + "'";
            komut1.Connection = baglanti;
            komut1.ExecuteNonQuery();
            baglanti.Close();
            Application.Exit();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void label3_MouseHover(object sender, EventArgs e)
        {
            label3.ForeColor = Color.Gray;
        }

        private void label3_MouseLeave(object sender, EventArgs e)
        {
            label3.ForeColor = Color.White;
        }

        private void label2_MouseHover(object sender, EventArgs e)
        {
            label2.ForeColor = Color.Gray;
        }

        private void label2_MouseLeave(object sender, EventArgs e)
        {
            label2.ForeColor = Color.White;
        }

        private void bunifuMaterialTextbox1_DoubleClick_1(object sender, EventArgs e)
        {
            bunifuMaterialTextbox1.Text = "";
        }

        private void bunifuThinButton22_Click(object sender, EventArgs e)
        {
            OleDbConnection baglanti = new OleDbConnection("Provider=Microsoft.JET.OLEDB.4.0;Data Source=database.mdb");
            OleDbCommand komut = new OleDbCommand();
            baglanti.Open();
            komut.CommandText = "Update kullanici SET kullanici_adi='" + textBox1.Text + "', eposta='" + textBox2.Text + "'  , cinsiyet='" + textBox3.Text + "', dogum_tarihi='" + textBox4.Text + "', rutbe='" + bunifuDropdown1.selectedValue + "', profil_fotograf='" + CirclePictureBox3.ImageLocation + "' where kullanici_adi='" + textBox1.Text + "'";
            komut.Connection = baglanti;
            int sonuc = komut.ExecuteNonQuery();

            if (sonuc > 0)
            {
                MessageBox.Show("Successful");
            }

            else
            {
                MessageBox.Show("FAIL");
            }

            baglanti.Close();
        }

        private void panel6_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            yoneticipanel_yasaklama yasaklama_nesne = new yoneticipanel_yasaklama();
            yasaklama_nesne.label4.Text = label4.Text.ToString();
            yasaklama_nesne.sifre = sifre;
            yasaklama_nesne.cinsiyet = cinsiyet;
            yasaklama_nesne.eposta = eposta;
            yasaklama_nesne.dogum_tarihi = dogum_tarihi;
            yasaklama_nesne.rutbe = rutbe;
            yasaklama_nesne.id = Convert.ToInt16(id);

            yasaklama_nesne.Show();
            this.Hide();
        }

        private void bunifuFlatButton4_Click(object sender, EventArgs e)
        {
            yoneticipanel_gorevliler gorevliler_nesne = new yoneticipanel_gorevliler();
            gorevliler_nesne.label4.Text = label4.Text.ToString();
            gorevliler_nesne.sifre = sifre;
            gorevliler_nesne.cinsiyet = cinsiyet;
            gorevliler_nesne.eposta = eposta;
            gorevliler_nesne.dogum_tarihi = dogum_tarihi;
            gorevliler_nesne.rutbe = rutbe;
            gorevliler_nesne.id = Convert.ToInt16(id);

            gorevliler_nesne.Show();
            this.Hide();
        }

        private void bunifuThinButton23_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show( textBox1.Text + " adlı Kişiyi Silmek İstediğinizden Emin Misiniz?", "DİKKAT", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (dialogResult == DialogResult.Yes)
            {
                OleDbConnection baglanti = new OleDbConnection("Provider=Microsoft.JET.OLEDB.4.0;Data Source=database.mdb");
                OleDbCommand komut = new OleDbCommand();
                baglanti.Open();
                komut.CommandText = "Delete * from kullanici where kullanici_adi='" + textBox1.Text + "'";
                komut.Connection = baglanti;
                int sonuc = komut.ExecuteNonQuery();

                if (sonuc > 0)
                {
                    MessageBox.Show("Successful");
                    panel6.Visible = false;
                }

                else
                {
                    MessageBox.Show("FAIL");
                }

                baglanti.Close();
            }
            
        }

        private void bunifuFlatButton3_Click(object sender, EventArgs e)
        {
            yoneticipanel_sohbetler sohbetler_nesne = new yoneticipanel_sohbetler();
            sohbetler_nesne.label4.Text = label4.Text.ToString();
            sohbetler_nesne.sifre = sifre;
            sohbetler_nesne.cinsiyet = cinsiyet;
            sohbetler_nesne.eposta = eposta;
            sohbetler_nesne.dogum_tarihi = dogum_tarihi;
            sohbetler_nesne.rutbe = rutbe;
            sohbetler_nesne.id = Convert.ToInt16(id);

            sohbetler_nesne.Show();
            this.Hide();
        }

        private void bunifuFlatButton6_Click(object sender, EventArgs e)
        {
            yoneticipanel_destek Destek_nesne = new yoneticipanel_destek();
            Destek_nesne.label4.Text = label4.Text.ToString();
            Destek_nesne.sifre = sifre;
            Destek_nesne.cinsiyet = cinsiyet;
            Destek_nesne.eposta = eposta;
            Destek_nesne.dogum_tarihi = dogum_tarihi;
            Destek_nesne.rutbe = rutbe;
            Destek_nesne.id = Convert.ToInt16(id);

            Destek_nesne.Show();
            this.Hide();
        }
    }
}

