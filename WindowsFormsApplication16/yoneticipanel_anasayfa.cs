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
    public partial class yoneticipanel_anasayfa : Form
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

        public yoneticipanel_anasayfa()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 20, 20));
        }

        string dakika, saat;
        int dakika_sayısı, saat_sayisi;
        private void timer1_Tick(object sender, EventArgs e)
        {
            dakika_sayısı = Convert.ToInt16(DateTime.Now.Minute);
            saat_sayisi = Convert.ToInt16(DateTime.Now.Hour);

            if (saat_sayisi < 10)
            {
                saat = Convert.ToString("0" + saat_sayisi);
            }
            
            else
            {
                saat = Convert.ToString(saat_sayisi);
            }

            if (dakika_sayısı < 10)
            {
                dakika = Convert.ToString("0" + dakika_sayısı);
            }

            else
            {
                dakika = Convert.ToString(dakika_sayısı);
            }

            label18.Text = saat + ":" + dakika; 
        }

        private void label3_Click(object sender, EventArgs e)
        {
            timer1.Stop();
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

        private void panel8_Paint(object sender, PaintEventArgs e)
        {

        }

        OpenFileDialog dosya = new OpenFileDialog();
        string DosyaYolu , cevrimici="";

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void yoneticipanel_anasyafa_Load(object sender, EventArgs e)
        {
            timer1.Start();

            OleDbConnection baglanti = new OleDbConnection("Provider=Microsoft.JET.OLEDB.4.0;Data Source=database.mdb");

            // Kullanici Sayisi Gösterme 
            baglanti.Open();
            OleDbCommand komut_kullanici = new OleDbCommand("SELECT COUNT(*) FROM kullanici Where kullanici_adi", baglanti);
            int kullanici_sayisi = Convert.ToInt16(komut_kullanici.ExecuteScalar());
            label8.Text = kullanici_sayisi.ToString();
            baglanti.Close();

            //Toplam Şikayet Sayısı Gösterme
            baglanti.Open();
            OleDbCommand komut_sikayet = new OleDbCommand("SELECT COUNT(*) FROM yardim_talebi Where Durum='Beklemede'", baglanti);
            int sikayet_sayisi = Convert.ToInt16(komut_sikayet.ExecuteScalar());
            label11.Text = sikayet_sayisi.ToString();
            baglanti.Close();

            //Toplam Cevrimiçi Sayısı Gösterme
            baglanti.Open();
            OleDbCommand komut_cevrimici = new OleDbCommand("SELECT COUNT(*) FROM kullanici Where cevrimici_durumu='Çevrimiçi'", baglanti);
            int cevrimici_sayisi = Convert.ToInt16(komut_cevrimici.ExecuteScalar());
            label13.Text = cevrimici_sayisi.ToString();
            bunifuGauge1.Value = cevrimici_sayisi;
            baglanti.Close();


            //Cevrimici Kullanıcıları Listeleme
            for (int i = 0; i < cevrimici_sayisi; i++)
            {
                OleDbDataAdapter Adaptor = new OleDbDataAdapter("Select * from kullanici where cevrimici_durumu='Çevrimiçi'", baglanti);
                DataTable dt= new DataTable();
                listBox1.DataSource = dt;
                Adaptor.Fill(dt);
                listBox1.DisplayMember = "kullanici_adi";
                listBox1.ValueMember = "ID";
            }

            ToolTip aciklama = new ToolTip();
            aciklama.ShowAlways = true;

            aciklama.SetToolTip(label3, "Close");
            aciklama.SetToolTip(label2, "Recuve");
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

        private void bunifuFlatButton5_Click(object sender, EventArgs e)
        {
            timer1.Stop();
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

        private void bunifuFlatButton2_Click(object sender, EventArgs e)
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
