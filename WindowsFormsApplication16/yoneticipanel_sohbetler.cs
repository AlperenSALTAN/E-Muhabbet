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
    public partial class yoneticipanel_sohbetler : Form
    {
        public string sifre, cinsiyet, rutbe, dogum_tarihi, eposta;
        public int id, secili_sohbet;

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

        public yoneticipanel_sohbetler()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 20, 20));
        }

        string cevrimici, DosyaYolu;

        private void yoneticipanel_sohbetler_Load(object sender, EventArgs e)
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

        private void bunifuFlatButton9_Click(object sender, EventArgs e)
        {
            secili_sohbet = 1;
            bunifuCustomDataGrid1.HeaderBgColor = Color.DarkGreen;

            groupBox1.Visible = true;
            groupBox2.Visible = true;
            OleDbConnection baglanti = new OleDbConnection("Provider=Microsoft.JET.OLEDB.4.0;Data Source=database.mdb");
            
            OleDbDataAdapter Adaptor = new OleDbDataAdapter("Select * from Sohbet#1 ", baglanti);
            DataSet verikumesi = new DataSet();
            baglanti.Open();
            Adaptor.Fill(verikumesi, "Sohbet#1");
            bunifuCustomDataGrid1.DataSource = verikumesi.Tables["Sohbet#1"];
            baglanti.Close();
           
        }

        private void bunifuFlatButton8_Click(object sender, EventArgs e)
        {
            secili_sohbet = 2;
            bunifuCustomDataGrid1.HeaderBgColor = Color.DarkRed;

            groupBox1.Visible = true;
            groupBox2.Visible = true;
            OleDbConnection baglanti = new OleDbConnection("Provider=Microsoft.JET.OLEDB.4.0;Data Source=database.mdb");

            OleDbDataAdapter Adaptor = new OleDbDataAdapter("Select * from Sohbet#2 ", baglanti);
            DataSet verikumesi = new DataSet();
            baglanti.Open();
            Adaptor.Fill(verikumesi, "Sohbet#2");
            bunifuCustomDataGrid1.DataSource = verikumesi.Tables["Sohbet#2"];
            baglanti.Close();
        }

        private void bunifuFlatButton7_Click(object sender, EventArgs e)
        {
            secili_sohbet = 3;
            bunifuCustomDataGrid1.HeaderBgColor = Color.FromArgb(0, 50, 64);

            groupBox1.Visible = true;
            groupBox2.Visible = true;
            OleDbConnection baglanti = new OleDbConnection("Provider=Microsoft.JET.OLEDB.4.0;Data Source=database.mdb");

            OleDbDataAdapter Adaptor = new OleDbDataAdapter("Select * from Sohbet#3 ", baglanti);
            DataSet verikumesi = new DataSet();
            baglanti.Open();
            Adaptor.Fill(verikumesi, "Sohbet#3");
            bunifuCustomDataGrid1.DataSource = verikumesi.Tables["Sohbet#3"];
            baglanti.Close();
        }

        private void bunifuFlatButton10_Click(object sender, EventArgs e)
        {
            secili_sohbet = 4;
            bunifuCustomDataGrid1.HeaderBgColor = Color.Purple;

            groupBox1.Visible = true;
            groupBox2.Visible = true;
            OleDbConnection baglanti = new OleDbConnection("Provider=Microsoft.JET.OLEDB.4.0;Data Source=database.mdb");

            OleDbDataAdapter Adaptor = new OleDbDataAdapter("Select * from Yonetici_sohbet#4 ", baglanti);
            DataSet verikumesi = new DataSet();
            baglanti.Open();
            Adaptor.Fill(verikumesi, "Yonetici_sohbet#4");
            bunifuCustomDataGrid1.DataSource = verikumesi.Tables["Yonetici_sohbet#4"];
            baglanti.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (secili_sohbet == 1)
            {
                DialogResult dialogResult = MessageBox.Show(textBox1.Text + " numberred ID will delete,  Are you Sure?", "WARNING", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (dialogResult == DialogResult.Yes)
                {
                    OleDbConnection baglanti = new OleDbConnection("Provider=Microsoft.JET.OLEDB.4.0;Data Source=database.mdb");
                    OleDbCommand komut = new OleDbCommand();
                    baglanti.Open();
                    komut.CommandText = "Delete * from Sohbet#1 where ID=" + textBox1.Text + "";
                    komut.Connection = baglanti;
                    int sonuc = komut.ExecuteNonQuery();

                    if (sonuc > 0)
                    {
                        MessageBox.Show("Succesful");

                        OleDbDataAdapter Adaptor = new OleDbDataAdapter("Select * from Sohbet#1 ", baglanti);
                        DataSet verikumesi = new DataSet();
                        Adaptor.Fill(verikumesi, "Sohbet#1");
                        bunifuCustomDataGrid1.DataSource = verikumesi.Tables["Sohbet#1"];
                        baglanti.Close();
                    }

                    else
                    {
                        MessageBox.Show("Error");
                    }

                    baglanti.Close();
                }
            }

            else if (secili_sohbet == 2)
            {
                DialogResult dialogResult = MessageBox.Show(textBox1.Text + " numberred ID will delete,  Are you Sure?", "WARNING", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (dialogResult == DialogResult.Yes)
                {
                    OleDbConnection baglanti = new OleDbConnection("Provider=Microsoft.JET.OLEDB.4.0;Data Source=database.mdb");
                    OleDbCommand komut = new OleDbCommand();
                    baglanti.Open();
                    komut.CommandText = "Delete * from Sohbet#2 where ID=" + textBox1.Text + "";
                    komut.Connection = baglanti;
                    int sonuc = komut.ExecuteNonQuery();

                    if (sonuc > 0)
                    {
                        MessageBox.Show("Successfull");

                        OleDbDataAdapter Adaptor = new OleDbDataAdapter("Select * from Sohbet#2 ", baglanti);
                        DataSet verikumesi = new DataSet();
                        Adaptor.Fill(verikumesi, "Sohbet#2");
                        bunifuCustomDataGrid1.DataSource = verikumesi.Tables["Sohbet#2"];
                        baglanti.Close();
                    }

                    else
                    {
                        MessageBox.Show("Error");
                    }

                    baglanti.Close();
                }
            }

            else if (secili_sohbet == 3)
            {
                DialogResult dialogResult = MessageBox.Show(textBox1.Text + " numberred ID will delete,  Are you Sure?", "WARNING", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (dialogResult == DialogResult.Yes)
                {
                    OleDbConnection baglanti = new OleDbConnection("Provider=Microsoft.JET.OLEDB.4.0;Data Source=database.mdb");
                    OleDbCommand komut = new OleDbCommand();
                    baglanti.Open();
                    komut.CommandText = "Delete * from Sohbet#3 where ID=" + textBox1.Text + "";
                    komut.Connection = baglanti;
                    int sonuc = komut.ExecuteNonQuery();

                    if (sonuc > 0)
                    {
                        MessageBox.Show("Successful");

                        OleDbDataAdapter Adaptor = new OleDbDataAdapter("Select * from Sohbet#3 ", baglanti);
                        DataSet verikumesi = new DataSet();
                        Adaptor.Fill(verikumesi, "Sohbet#3");
                        bunifuCustomDataGrid1.DataSource = verikumesi.Tables["Sohbet#3"];
                        baglanti.Close();
                    }

                    else
                    {
                        MessageBox.Show("Error");
                    }

                    baglanti.Close();
                }
            }

            else if (secili_sohbet == 4)
            {
                DialogResult dialogResult = MessageBox.Show(textBox1.Text + " numberred ID will delete,  Are you Sure?", "WARNING", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (dialogResult == DialogResult.Yes)
                {
                    OleDbConnection baglanti = new OleDbConnection("Provider=Microsoft.JET.OLEDB.4.0;Data Source=database.mdb");
                    OleDbCommand komut = new OleDbCommand();
                    baglanti.Open();
                    komut.CommandText = "Delete * from Yonetici_sohbet#4 where ID=" + textBox1.Text + "";
                    komut.Connection = baglanti;
                    int sonuc = komut.ExecuteNonQuery();

                    if (sonuc > 0)
                    {
                        MessageBox.Show("SUCCESSFUL");

                        OleDbDataAdapter Adaptor = new OleDbDataAdapter("Select * from Yonetici_sohbet#4 ", baglanti);
                        DataSet verikumesi = new DataSet();
                        Adaptor.Fill(verikumesi, "Yonetici_sohbet#4");
                        bunifuCustomDataGrid1.DataSource = verikumesi.Tables["Yonetici_sohbet#4"];
                        baglanti.Close();
                    }

                    else
                    {
                        MessageBox.Show("error");
                    }

                    baglanti.Close();
                }
            }
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

        private void label8_Click(object sender, EventArgs e)
        {

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

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            yoneticipanel_yoneticisohbeti yoneticisohbet_nesne = new yoneticipanel_yoneticisohbeti();
            yoneticisohbet_nesne.label4.Text = label4.Text.ToString();
            yoneticisohbet_nesne.sifre = sifre;
            yoneticisohbet_nesne.cinsiyet = cinsiyet;
            yoneticisohbet_nesne.eposta = eposta;
            yoneticisohbet_nesne.dogum_tarihi = dogum_tarihi;
            yoneticisohbet_nesne.rutbe = rutbe;
            yoneticisohbet_nesne.id = Convert.ToInt16(id);

            yoneticisohbet_nesne.Show();
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
