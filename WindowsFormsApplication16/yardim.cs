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
    public partial class yardim : Form
    {
       public string sifre, cinsiyet, eposta, dogum_tarihi, rutbe;
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

        public yardim()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 20, 20));
        }

        private void label1_Click(object sender, EventArgs e)
        {
            OleDbConnection baglanti = new OleDbConnection("Provider=Microsoft.JET.OLEDB.4.0;Data Source=database.mdb");
            baglanti.Open();
            OleDbCommand komut1 = new OleDbCommand();
            komut1.CommandText = "UPDATE kullanici set cevrimici_durumu='Çevrimdışı' WHERE kullanici_adi='" + label6.Text + "'";
            komut1.Connection = baglanti;
            komut1.ExecuteNonQuery();
            baglanti.Close();
            Application.Exit();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        string cevrimici;
        OpenFileDialog dosya = new OpenFileDialog();
        string DosyaYolu;

        private void yardim_Load(object sender, EventArgs e)
        {
            OleDbConnection baglanti = new OleDbConnection("Provider=Microsoft.JET.OLEDB.4.0;Data Source=database.mdb");
            baglanti.Open();
            OleDbCommand komut = new OleDbCommand();
            komut.CommandText = "Select * from kullanici where kullanici_adi='" + label6.Text + "'";
            komut.Connection = baglanti;
            OleDbDataReader oku = komut.ExecuteReader();


            if (oku.Read())
            {
                OleDbCommand komut2 = new OleDbCommand("SELECT COUNT(*) FROM yardim_talebi where Durum='Beklemede' and Gönderen='" + label6.Text + "'", baglanti);
                label16.Text = komut2.ExecuteScalar().ToString();

                OleDbCommand komut3 = new OleDbCommand("SELECT COUNT(*) FROM yardim_talebi where Durum='Çözüldü' and Gönderen='" + label6.Text + "'", baglanti);
                label13.Text = komut3.ExecuteScalar().ToString();

                OleDbCommand komut4 = new OleDbCommand("SELECT COUNT(*) FROM yardim_talebi where Gönderen='" + label6.Text + "'", baglanti);
                label12.Text = komut4.ExecuteScalar().ToString();

                cevrimici = oku["cevrimici_durumu"].ToString();
                DosyaYolu = oku["profil_fotograf"].ToString();
                CirclePictureBox2.ImageLocation = DosyaYolu;

                if (rutbe == "Yönetici")
                {
                    panel2.Visible = true;
                }

                if (rutbe == "Kullanıcı")
                {
                    panel2.Visible = false;
                }

                if (cevrimici == "Çevrimiçi")
                {
                    pictureBox12.Image = Resource1.online;
                    pictureBox12.Location = new Point(65, 123);
                }

                if (cevrimici == "Boşta")
                {
                    pictureBox12.Image = Resource1.boşta;
                    pictureBox12.Location = new Point(75, 123);
                }

                if (cevrimici == "Rahatsız Etmeyin")
                {
                    pictureBox12.Image = Resource1.rahatsız_etme;
                    pictureBox12.Location = new Point(47, 123);
                }

                if (cevrimici == "Görünmez")
                {
                    pictureBox12.Image = Resource1.görünmez;
                    pictureBox12.Location = new Point(66, 123);
                }

                baglanti.Close();

                label3.Text = "Hello " + label6.Text + " ,";

                ToolTip aciklama = new ToolTip();
                aciklama.ShowAlways = true;

                aciklama.SetToolTip(label1, "Close");
                aciklama.SetToolTip(label2, "Recuve");
                aciklama.SetToolTip(pictureBox1, "Homepage");
                aciklama.SetToolTip(pictureBox2, "Elektronic Chat Application!");
                aciklama.SetToolTip(pictureBox3, "Persons");
                aciklama.SetToolTip(pictureBox4, "Messages");
                aciklama.SetToolTip(pictureBox5, "Account Settings");
                aciklama.SetToolTip(pictureBox6, "Logout");
                aciklama.SetToolTip(pictureBox7, "Help");
            }
        }

        private void label1_MouseHover(object sender, EventArgs e)
        {
            label1.ForeColor = Color.Red;
        }

        private void label2_MouseHover(object sender, EventArgs e)
        {
            label2.ForeColor = Color.Red;

        }

        private void label2_MouseLeave(object sender, EventArgs e)
        {
            label2.ForeColor = Color.Black;
        }

        private void label1_MouseLeave(object sender, EventArgs e)
        {
            label1.ForeColor = Color.Black;
        }

        private void panel3_MouseHover(object sender, EventArgs e)
        {
            pictureBox1.Size = new Size(35, 35);
            panel3.BackColor = Color.DodgerBlue;
            label8.ForeColor = Color.White;
        }

        private void panel3_MouseLeave(object sender, EventArgs e)
        {
            pictureBox1.Size = new Size(30, 30);
            panel3.BackColor = SystemColors.Control;
            label8.ForeColor = Color.Black;
        }

        private void panel6_MouseHover(object sender, EventArgs e)
        {
            pictureBox5.Size = new Size(35, 35);
            panel6.BackColor = Color.DodgerBlue;
            label14.ForeColor = Color.White;
        }

        private void panel6_MouseLeave(object sender, EventArgs e)
        {
            pictureBox5.Size = new Size(30, 30);
            panel6.BackColor = SystemColors.Control;
            label14.ForeColor = Color.Black;
        }

        private void panel7_MouseHover(object sender, EventArgs e)
        {
            pictureBox3.Size = new Size(35, 35);
            panel7.BackColor = Color.DodgerBlue;
            label21.ForeColor = Color.White;
        }

        private void panel7_MouseLeave(object sender, EventArgs e)
        {
            pictureBox3.Size = new Size(30, 30);
            panel7.BackColor = SystemColors.Control;
            label21.ForeColor = Color.Black;
        }

        private void panel8_MouseHover(object sender, EventArgs e)
        {
            pictureBox4.Size = new Size(35, 35);
            panel8.BackColor = Color.DodgerBlue;
            label22.ForeColor = Color.White;
        }

        private void panel8_MouseLeave(object sender, EventArgs e)
        {
            pictureBox4.Size = new Size(30, 30);
            panel8.BackColor = SystemColors.Control;
            label22.ForeColor = Color.Black;
        }

        private void panel9_MouseHover(object sender, EventArgs e)
        {
            pictureBox7.Size = new Size(35, 35);
            panel9.BackColor = Color.DodgerBlue;
            label23.ForeColor = Color.White;
        }

        private void panel9_MouseLeave(object sender, EventArgs e)
        {
            pictureBox7.Size = new Size(30, 30);
            panel9.BackColor = Color.DodgerBlue;
            label23.ForeColor = Color.White;
        }

        private void panel10_MouseHover(object sender, EventArgs e)
        {
            pictureBox6.Size = new Size(35, 35);
            panel10.BackColor = Color.DodgerBlue;
            label24.ForeColor = Color.White;
        }

        private void panel10_MouseLeave(object sender, EventArgs e)
        {
            pictureBox6.Size = new Size(30, 30);
            panel10.BackColor = SystemColors.Control;
            label24.ForeColor = Color.Black;
        }

        private void panel10_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Are you sure to Logout?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (dialogResult == DialogResult.Yes)
            {
                OleDbConnection baglanti = new OleDbConnection("Provider=Microsoft.JET.OLEDB.4.0;Data Source=database.mdb");
                baglanti.Open();
                OleDbCommand komut1 = new OleDbCommand();
                komut1.CommandText = "UPDATE kullanici SET cevrimici_durumu='Çevrimdışı' WHERE kullanici_adi='" + label6.Text + "'";
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

        private void panel3_Click(object sender, EventArgs e)
        {
            ana_ekran ana_nesne = new ana_ekran();
            ana_nesne.label7.Text = label6.Text.ToString();
            ana_nesne.sifre = sifre;
            ana_nesne.cinsiyet = cinsiyet;
            ana_nesne.eposta = eposta;
            ana_nesne.dogum_tarihi = dogum_tarihi;
            ana_nesne.rutbe = rutbe;
            ana_nesne.id = Convert.ToInt16(id);
            ana_nesne.Show();
            this.Close();
        }

        private void panel6_Click(object sender, EventArgs e)
        {
            hesap_ayarlari hesap_Ayar = new hesap_ayarlari();
            hesap_Ayar.id = Convert.ToInt16(id);
            hesap_Ayar.label6.Text = label6.Text;
            hesap_Ayar.textBox3.Text = (label6.Text + "#" + id).ToString();
            hesap_Ayar.textBox8.Text = sifre;
            hesap_Ayar.textBox6.Text = cinsiyet;
            hesap_Ayar.textBox4.Text = eposta;
            hesap_Ayar.textBox5.Text = dogum_tarihi;
            hesap_Ayar.textBox7.Text = rutbe;
            hesap_Ayar.Show();
            this.Close();
        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel6_Paint(object sender, PaintEventArgs e)
        {

        }
        bool dragging;
        Point offset;
        private void panel5_MouseDown(object sender, MouseEventArgs e)
        {
            dragging = true;
            offset = e.Location;
        }

        private void panel5_MouseMove(object sender, MouseEventArgs e)
        {
            if (dragging)
            {
                Point currentScreenPos = PointToScreen(e.Location);
                Location = new
                Point(currentScreenPos.X - offset.X,
                currentScreenPos.Y - offset.Y);
            }
        }

        private void panel5_MouseUp(object sender, MouseEventArgs e)
        {
            dragging = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OleDbConnection baglanti = new OleDbConnection("Provider=Microsoft.JET.OLEDB.4.0;Data Source=database.mdb");
            baglanti.Open();
            OleDbCommand komut = new OleDbCommand();
            komut.CommandText = "Insert into yardim_talebi(Gönderen,Konu,Öncelik,Mesaj,Durum) values('"+label6.Text+"','"+textBox1.Text+"','"+comboBox1.SelectedItem+"','"+textBox2.Text+"','Beklemede')";
            komut.Connection = baglanti;
            int sonuc = komut.ExecuteNonQuery();
            baglanti.Close();

            if (sonuc > 0)
            {
                MessageBox.Show("Your request has been received, Thank you.");
                baglanti.Open();
                OleDbCommand komut2 = new OleDbCommand("SELECT COUNT(*) FROM yardim_talebi where Durum='Beklemede' and Gönderen='"+label6.Text+"'", baglanti);
                label16.Text = komut2.ExecuteScalar().ToString();
                baglanti.Close();
            }

            else
            {
                MessageBox.Show("An Error has occurred in the system, Please try Again");
            }
        }

        private void panel2_MouseHover(object sender, EventArgs e)
        {
            pictureBox8.Size = new Size(35, 35);
            panel2.BackColor = Color.DodgerBlue;
            label18.ForeColor = Color.White;
        }

        private void panel2_MouseLeave(object sender, EventArgs e)
        {
            pictureBox8.Size = new Size(30, 30);
            panel2.BackColor = SystemColors.Control;
            label18.ForeColor = Color.Black;
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel10_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel7_Click(object sender, EventArgs e)
        {
            kişiler kişi_nesne = new kişiler();
            kişi_nesne.label6.Text = label6.Text.ToString();
            kişi_nesne.sifre = sifre;
            kişi_nesne.cinsiyet = cinsiyet;
            kişi_nesne.eposta = eposta;
            kişi_nesne.dogum_tarihi = dogum_tarihi;
            kişi_nesne.rutbe = rutbe;
            kişi_nesne.id = Convert.ToInt16(id);
            kişi_nesne.Show();
            this.Close();
        }

        private void panel8_Click(object sender, EventArgs e)
        {
            mesajlar mesajlar_nesne = new mesajlar();
            mesajlar_nesne.label6.Text = label6.Text.ToString();
            mesajlar_nesne.sifre = sifre;
            mesajlar_nesne.cinsiyet = cinsiyet;
            mesajlar_nesne.eposta = eposta;
            mesajlar_nesne.dogum_tarihi = dogum_tarihi;
            mesajlar_nesne.rutbe = rutbe;
            mesajlar_nesne.id = Convert.ToInt16(id);
            mesajlar_nesne.Show();
            this.Close();
        }

        private void panel9_Click(object sender, EventArgs e)
        {
            yoneticipanel_anasayfa yonetici_nesne = new yoneticipanel_anasayfa();
            yonetici_nesne.label4.Text = label6.Text.ToString();
            yonetici_nesne.sifre = sifre;
            yonetici_nesne.cinsiyet = cinsiyet;
            yonetici_nesne.eposta = eposta;
            yonetici_nesne.dogum_tarihi = dogum_tarihi;
            yonetici_nesne.rutbe = rutbe;
            yonetici_nesne.id = Convert.ToInt16(id);
            yonetici_nesne.Show();
            this.Close();
        }

        private void panel2_Click(object sender, EventArgs e)
        {
            yoneticipanel_anasayfa yonetici_nesne = new yoneticipanel_anasayfa();
            yonetici_nesne.label4.Text = label6.Text.ToString();
            yonetici_nesne.sifre = sifre;
            yonetici_nesne.cinsiyet = cinsiyet;
            yonetici_nesne.eposta = eposta;
            yonetici_nesne.dogum_tarihi = dogum_tarihi;
            yonetici_nesne.rutbe = rutbe;
            yonetici_nesne.id = Convert.ToInt16(id);
            yonetici_nesne.Show();
            this.Close();
        }
    }
}
