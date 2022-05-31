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
using System.IO;

namespace WindowsFormsApplication16
{
    public partial class hesap_ayarlari : Form
    {
        int sure = 0;

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

        public hesap_ayarlari()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 20, 20));
        }

        private void hesap_ayarlari_Load(object sender, EventArgs e)
        {
            
            OleDbConnection baglanti = new OleDbConnection("Provider=Microsoft.JET.OLEDB.4.0;Data Source=database.mdb");
            baglanti.Open();
            OleDbCommand komut = new OleDbCommand();
            komut.CommandText = "Select * from kullanici where kullanici_adi='"+label6.Text+"'";
            komut.Connection = baglanti;
            OleDbDataReader oku = komut.ExecuteReader();

            if (textBox7.Text == "Yönetici")
            {
                panel11.Visible = true;
            }

            if (textBox7.Text == "Kullanıcı")
            {
                panel11.Visible = false;
            }

            if (oku.Read())
            {
                if (rutbe == "Yönetici")
                {
                    panel11.Visible = true;
                }

                if (rutbe == "Kullanıcı")
                {
                    panel11.Visible = false;
                }

                cevrimici = oku["cevrimici_durumu"].ToString();
                DosyaYolu = oku["profil_fotograf"].ToString();
                CirclePictureBox1.ImageLocation = DosyaYolu;
                pictureBox10.ImageLocation = DosyaYolu;

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

            textBox1.Text = label6.Text;
            textBox2.Text = textBox4.Text;

            if (textBox6.Text == "Girl")
	        {
		        radioButton1.Checked = true;
	        }

            if (textBox6.Text == "Boy")
	        {
		        radioButton2.Checked = true;
	        }

            timer1.Start();

            ToolTip aciklama = new ToolTip();
            aciklama.ShowAlways = true;

            aciklama.SetToolTip(label1, "Close");
            aciklama.SetToolTip(label2, "Reduce");
            aciklama.SetToolTip(pictureBox1, "Homepage");
            aciklama.SetToolTip(pictureBox2, "Elektronic Chat Application!");
            aciklama.SetToolTip(pictureBox3, "Persons");
            aciklama.SetToolTip(pictureBox4, "Messages");
            aciklama.SetToolTip(pictureBox5, "Account Settings");
            aciklama.SetToolTip(pictureBox6, "Logout");
            aciklama.SetToolTip(pictureBox7, "Help");
            }
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

        private void panel1_Paint(object sender, PaintEventArgs e)
        {


        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            //null;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            
        }

        private void pictureBox6_Click(object sender, EventArgs e)
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
            label2.ForeColor = Color.Black;
        }

        private void pictureBox1_MouseHover(object sender, EventArgs e)
        {
            pictureBox1.Size = new Size(35, 35);
        }

        private void pictureBox1_MouseLeave(object sender, EventArgs e)
        {
            pictureBox1.Size = new Size(30, 30);
        }

        private void pictureBox5_MouseHover(object sender, EventArgs e)
        {
            pictureBox5.Size = new Size(35, 35);
        }

        private void pictureBox5_MouseLeave(object sender, EventArgs e)
        {
            pictureBox5.Size = new Size(30, 30);
        }

        private void pictureBox3_MouseHover(object sender, EventArgs e)
        {
            pictureBox3.Size = new Size(35, 35);
        }


        private void pictureBox3_MouseLeave(object sender, EventArgs e)
        {
            pictureBox3.Size = new Size(30, 30);
        }

        private void pictureBox4_MouseHover(object sender, EventArgs e)
        {
            pictureBox4.Size = new Size(35, 35);
        }

        private void pictureBox4_MouseLeave(object sender, EventArgs e)
        {
            pictureBox4.Size = new Size(30, 30);
        }

        private void pictureBox7_MouseHover(object sender, EventArgs e)
        {
            pictureBox7.Size = new Size(35, 35);
        }

        private void pictureBox7_MouseLeave(object sender, EventArgs e)
        {
            pictureBox7.Size = new Size(30, 30);
        }

        private void pictureBox6_MouseHover(object sender, EventArgs e)
        {
            pictureBox6.Size = new Size(35, 35);
        }

        private void pictureBox6_MouseLeave(object sender, EventArgs e)
        {
            pictureBox6.Size = new Size(30, 30);
        }

        string cevrimici;

        private void button2_Click(object sender, EventArgs e)
        {
            string cinsiyet = "";

            if (radioButton1.Checked == true)
	        {
		        cinsiyet="Girl";
	        }       

            else if (radioButton2.Checked == true)
	        {
		        cinsiyet = "Boy";
	        }

            label25.Text = cinsiyet;
            OleDbConnection baglanti = new OleDbConnection("Provider=Microsoft.JET.OLEDB.4.0;Data Source=database.mdb");
            baglanti.Open();

            OleDbCommand komut1 = new OleDbCommand();
            komut1.CommandText = "Update yardim_talebi SET Gönderen='" + textBox1.Text + "' where Gönderen='" + label6.Text + "' ";
            komut1.Connection = baglanti;
            komut1.ExecuteNonQuery();
            baglanti.Close();

            baglanti.Open();
            OleDbCommand komut = new OleDbCommand();
            label26.Text = DosyaYolu;
            komut.CommandText = "Update kullanici SET kullanici_adi='" + textBox1.Text + "', eposta='" + textBox2.Text + "' , sifre='" + textBox9.Text + "' , cinsiyet='" + label25.Text + "', dogum_tarihi='" + textBox5.Text + "', rutbe='" + textBox7.Text + "', cevrimici_durumu='" + comboBox1.SelectedItem + "', profil_fotograf='" + label26.Text + "' where kullanici_adi='"+label6.Text+"'";
            komut.Connection = baglanti;
            int sonuc = komut.ExecuteNonQuery();

            if (sonuc > 0)
            {
                MessageBox.Show("Successful");
                CirclePictureBox1.ImageLocation = DosyaYolu;
                label6.Text = textBox1.Text;
                textBox3.Text = Convert.ToString(label6.Text + "#" + id);
                textBox4.Text = textBox2.Text;
                textBox6.Text = cinsiyet;
                textBox8.Text = textBox9.Text;
            }

            else
            {
                MessageBox.Show("UnSuccessful");
            }

            baglanti.Close();

            baglanti.Open();
            komut.CommandText = "Select * from kullanici";
            komut.Connection = baglanti;
            OleDbDataReader oku = komut.ExecuteReader();

            if (oku.Read())
            {
                cevrimici = oku["cevrimici_durumu"].ToString();

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
            }    
           
            baglanti.Close();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {

            if (cevrimici == "Çevrimiçi")
            {
                pictureBox12.Image = Resource1.online;
                pictureBox12.Location = new Point(65, 123);
            }

            else if (cevrimici == "Boşta")
            {
                pictureBox12.Image = Resource1.boşta;
                pictureBox12.Location = new Point(75, 123);
            }

            else if (cevrimici == "Rahatsız Etmeyin")
            {
                pictureBox12.Image = Resource1.rahatsız_etme;
                pictureBox12.Location = new Point(47, 123);
            }

            else if (cevrimici == "Görünmez")
            {
                pictureBox12.Image = Resource1.görünmez;
                pictureBox12.Location = new Point(66, 123);
            }

            
        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {
           
        }

        OpenFileDialog dosya = new OpenFileDialog();
        string DosyaYolu;

        private void button1_Click(object sender, EventArgs e)
        {
            dosya.Filter = "Picture File |*.jpg;*.nef;*.png;*.jpg;*.gif| Video|*.avi| All Files |*.*";
            dosya.Title = "Browse";
            dosya.ShowDialog();
            DosyaYolu = dosya.FileName;
            pictureBox10.ImageLocation = DosyaYolu;
            

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {
        }

        private void panel3_Click(object sender, EventArgs e)
        {
            ana_ekran giris_nesne = new ana_ekran();
            giris_nesne.label7.Text = label6.Text.ToString();
            giris_nesne.sifre = textBox8.Text.ToString();
            giris_nesne.cinsiyet = textBox6.Text.ToString();
            giris_nesne.eposta = textBox4.Text.ToString();
            giris_nesne.dogum_tarihi = textBox5.Text.ToString();
            giris_nesne.rutbe = textBox7.Text.ToString();
            giris_nesne.id = Convert.ToInt16(id);
            giris_nesne.Show();

            this.Close();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {

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
            panel6.BackColor = Color.DodgerBlue;
            label14.ForeColor = Color.White;
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
            panel9.BackColor = SystemColors.Control;
            label23.ForeColor = Color.Black;
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
                komut1.CommandText = "UPDATE kullanici set cevrimici_durumu='Çevrimdışı' WHERE kullanici_adi='" + label6.Text + "'";
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

        private void panel9_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel9_Click(object sender, EventArgs e)
        {
            yardim yardim_nesne = new yardim();
            yardim_nesne.sifre = textBox8.Text.ToString();
            yardim_nesne.label6.Text = label6.Text.ToString();
            yardim_nesne.cinsiyet = textBox6.Text.ToString();
            yardim_nesne.eposta = textBox4.Text.ToString();
            yardim_nesne.dogum_tarihi = textBox5.Text.ToString();
            yardim_nesne.rutbe = textBox7.Text.ToString();
            yardim_nesne.id = Convert.ToInt16(id);

            yardim_nesne.Show();
            this.Close();
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

        private void panel5_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel11_MouseHover(object sender, EventArgs e)
        {
            pictureBox9.Size = new Size(35, 35);
            panel11.BackColor = Color.DodgerBlue;
            label27.ForeColor = Color.White;
        }

        private void label24_Click(object sender, EventArgs e)
        {

        }

        private void panel11_MouseLeave(object sender, EventArgs e)
        {
            pictureBox9.Size = new Size(30, 30);
            panel11.BackColor = SystemColors.Control;
            label27.ForeColor = Color.Black;
        }

        private void panel7_Click(object sender, EventArgs e)
        {
            kişiler kişiler_nesne = new kişiler();
            kişiler_nesne.sifre = textBox8.Text.ToString();
            kişiler_nesne.label6.Text = label6.Text.ToString();
            kişiler_nesne.cinsiyet = textBox6.Text.ToString();
            kişiler_nesne.eposta = textBox4.Text.ToString();
            kişiler_nesne.dogum_tarihi = textBox5.Text.ToString();
            kişiler_nesne.rutbe = textBox7.Text.ToString();
            kişiler_nesne.id = Convert.ToInt16(id);

            kişiler_nesne.Show();
            this.Close();
        }

        private void panel8_Click(object sender, EventArgs e)
        {
            mesajlar mesajlar_nesne = new mesajlar();
            mesajlar_nesne.sifre = textBox8.Text.ToString();
            mesajlar_nesne.label6.Text = label6.Text.ToString();
            mesajlar_nesne.cinsiyet = textBox6.Text.ToString();
            mesajlar_nesne.eposta = textBox4.Text.ToString();
            mesajlar_nesne.dogum_tarihi = textBox5.Text.ToString();
            mesajlar_nesne.rutbe = textBox7.Text.ToString();
            mesajlar_nesne.id = Convert.ToInt16(id);

            mesajlar_nesne.Show();
            this.Close();
        }

        private void panel11_Click(object sender, EventArgs e)
        {
            yoneticipanel_anasayfa yonetici_nesne = new yoneticipanel_anasayfa();
            yonetici_nesne.sifre = textBox8.Text.ToString();
            yonetici_nesne.label4.Text = label6.Text.ToString();
            yonetici_nesne.cinsiyet = textBox6.Text.ToString();
            yonetici_nesne.eposta = textBox4.Text.ToString();
            yonetici_nesne.dogum_tarihi = textBox5.Text.ToString();
            yonetici_nesne.rutbe = textBox7.Text.ToString();
            yonetici_nesne.id = Convert.ToInt16(id);

            yonetici_nesne.Show();
            this.Close();
        }
    }
}
