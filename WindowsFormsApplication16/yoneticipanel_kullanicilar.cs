using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Data.OleDb;

namespace WindowsFormsApplication16
{
    public partial class yoneticipanel_kullanicilar : Form
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

        public yoneticipanel_kullanicilar()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 20, 20));
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

        string cevrimici="",DosyaYolu="";
        private void yoneticipanel_kullanicilar_Load(object sender, EventArgs e)
        {
            OleDbConnection baglanti = new OleDbConnection("Provider=Microsoft.JET.OLEDB.4.0;Data Source=database.mdb");

            OleDbDataAdapter Adaptor = new OleDbDataAdapter("Select * from kullanici ", baglanti);
            DataSet verikumesi = new DataSet();
            baglanti.Open();
            Adaptor.Fill(verikumesi, "kullanici");
            bunifuCustomDataGrid1.DataSource = verikumesi.Tables["kullanici"];
            baglanti.Close();

            ToolTip aciklama = new ToolTip();
            aciklama.ShowAlways = true;

            aciklama.SetToolTip(label3, "Close");
            aciklama.SetToolTip(label2, "Recurve");
            aciklama.SetToolTip(bunifuFlatButton1, "Dashboard");
            aciklama.SetToolTip(bunifuFlatButton2, "Users");
            aciklama.SetToolTip(bunifuFlatButton4, "Staffs");
            aciklama.SetToolTip(bunifuFlatButton3, "Helps");
            aciklama.SetToolTip(label1, "e-Muhabbet ~ House Keeping");

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

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            yoneticipanel_kullanici_arama arama_nesne = new yoneticipanel_kullanici_arama();
            arama_nesne.label4.Text = label4.Text.ToString();
            arama_nesne.sifre = sifre;
            arama_nesne.cinsiyet = cinsiyet;
            arama_nesne.eposta = eposta;
            arama_nesne.dogum_tarihi = dogum_tarihi;
            arama_nesne.rutbe = rutbe;
            arama_nesne.id = Convert.ToInt16(id);

            arama_nesne.Show();
            this.Hide();
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
