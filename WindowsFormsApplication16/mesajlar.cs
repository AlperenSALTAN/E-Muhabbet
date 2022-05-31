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
    public partial class mesajlar : Form
    {
        public string sifre, cinsiyet, rutbe, dogum_tarihi, eposta;
        public int id;

        OpenFileDialog dosya = new OpenFileDialog();
        string DosyaYolu, cevrimici = "";

        int secili_sohbet = 0;

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

        public mesajlar()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 20, 20));
        }

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            secili_sohbet = 1;
            groupBox2.Visible = true;
            groupBox3.Visible = false;
            groupBox4.Visible = false;
            panel4.Visible = true;
            OleDbConnection baglanti = new OleDbConnection("Provider=Microsoft.JET.OLEDB.4.0;Data Source=database.mdb");

            baglanti.Open();
            OleDbCommand komutcuk = new OleDbCommand("SELECT COUNT(*) FROM Sohbet#1", baglanti);
            int sohbet1_sayisi = Convert.ToInt16(komutcuk.ExecuteScalar());

            for (int i = 0; i < sohbet1_sayisi; i++)
            {
                OleDbDataAdapter da1 = new OleDbDataAdapter("select * from Sohbet#1", baglanti);
                DataTable dt = new DataTable();
                listBox1.DataSource = dt;
                da1.Fill(dt);

                listBox1.DisplayMember = "kullanici_ve_mesaj";
                listBox1.ValueMember = "ID";
            }

            baglanti.Close();
        }

        private void bunifuFlatButton2_Click(object sender, EventArgs e)
        {
            secili_sohbet = 2;
            groupBox2.Visible = false;
            groupBox3.Visible = true;
            groupBox4.Visible = false;
            panel4.Visible = true;

            OleDbConnection baglanti = new OleDbConnection("Provider=Microsoft.JET.OLEDB.4.0;Data Source=database.mdb");

            baglanti.Open();
            OleDbCommand komut1 = new OleDbCommand("SELECT COUNT(*) FROM Sohbet#2", baglanti);
            int sohbet2_sayisi = Convert.ToInt16(komut1.ExecuteScalar());

            for (int i = 0; i < sohbet2_sayisi; i++)
            {
                OleDbDataAdapter da= new OleDbDataAdapter("select * from Sohbet#2", baglanti);
                DataTable dt = new DataTable();
                listBox2.DataSource = dt;
                da.Fill(dt);

                listBox2.DisplayMember = "kullanici_ve_mesaj";
                listBox2.ValueMember = "ID";
            }

            baglanti.Close();
        }

        private void bunifuFlatButton3_Click(object sender, EventArgs e)
        {
            secili_sohbet = 3;

            groupBox2.Visible = false;
            groupBox3.Visible = false;
            groupBox4.Visible = true;
            panel4.Visible = true;

            OleDbConnection baglanti = new OleDbConnection("Provider=Microsoft.JET.OLEDB.4.0;Data Source=database.mdb");

            baglanti.Open();
            OleDbCommand komut2 = new OleDbCommand("SELECT COUNT(*) FROM Sohbet#3", baglanti);
            int sohbet3_sayisi = Convert.ToInt16(komut2.ExecuteScalar());

            for (int i = 0; i < sohbet3_sayisi; i++)
            {
                OleDbDataAdapter da2 = new OleDbDataAdapter("select * from Sohbet#3", baglanti);
                DataTable dt3 = new DataTable();
                listBox3.DataSource = dt3;
                da2.Fill(dt3);

                listBox3.DisplayMember = "kullanici_ve_mesaj";
                listBox3.ValueMember = "ID";
            }

            baglanti.Close();
        }

        private void mesajlar_Load(object sender, EventArgs e)
        {
            ToolTip aciklama = new ToolTip();
            aciklama.ShowAlways = true;

            aciklama.SetToolTip(label1, "Close");
            aciklama.SetToolTip(label2, "Recuve");
            aciklama.SetToolTip(pictureBox1, "Homepage");
            aciklama.SetToolTip(pictureBox2, "Elektronic Chat Application!");
            aciklama.SetToolTip(pictureBox3, "Persons");
            aciklama.SetToolTip(pictureBox4, "Message");
            aciklama.SetToolTip(pictureBox5, "Account Settings");
            aciklama.SetToolTip(pictureBox6, "Logout");
            aciklama.SetToolTip(pictureBox7, "Help");

            if (rutbe == "Yönetici")
            {
                panel2.Visible = true;
            }

            if (rutbe == "Kullanıcı")
            {
                panel2.Visible = false;
            }

            OleDbConnection baglanti = new OleDbConnection("Provider=Microsoft.JET.OLEDB.4.0;Data Source=database.mdb");
            baglanti.Open();
            OleDbCommand komut = new OleDbCommand();
            komut.CommandText = "Select * from kullanici where kullanici_adi='" + label6.Text + "'";
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
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (secili_sohbet == 1)
            {
                OleDbConnection baglanti = new OleDbConnection("Provider=Microsoft.JET.OLEDB.4.0;Data Source=database.mdb");

                baglanti.Open();
                OleDbCommand komutcuk = new OleDbCommand("SELECT COUNT(*) FROM Sohbet#1", baglanti);
                int sohbet1_sayisi = Convert.ToInt16(komutcuk.ExecuteScalar());

                for (int i = 0; i < sohbet1_sayisi; i++)
                {
                    OleDbDataAdapter da1 = new OleDbDataAdapter("select * from Sohbet#1", baglanti);
                    DataTable dt = new DataTable();
                    listBox1.DataSource = dt;
                    da1.Fill(dt);

                    listBox1.DisplayMember = "kullanici_ve_mesaj";
                    listBox1.ValueMember = "ID";
                }

                baglanti.Close();
            }

            if (secili_sohbet == 2)
            {
                OleDbConnection baglanti = new OleDbConnection("Provider=Microsoft.JET.OLEDB.4.0;Data Source=database.mdb");

                baglanti.Open();
                OleDbCommand komut1 = new OleDbCommand("SELECT COUNT(*) FROM Sohbet#2", baglanti);
                int sohbet2_sayisi = Convert.ToInt16(komut1.ExecuteScalar());

                for (int i = 0; i < sohbet2_sayisi; i++)
                {
                    OleDbDataAdapter da= new OleDbDataAdapter("select * from Sohbet#2", baglanti);
                    DataTable dt = new DataTable();
                    listBox2.DataSource = dt;
                    da.Fill(dt);

                    listBox2.DisplayMember = "kullanici_ve_mesaj";
                    listBox2.ValueMember = "ID";
                }

                baglanti.Close();
            }

            if (secili_sohbet == 3)
            {
                OleDbConnection baglanti = new OleDbConnection("Provider=Microsoft.JET.OLEDB.4.0;Data Source=database.mdb");

                baglanti.Open();
                OleDbCommand komut2 = new OleDbCommand("SELECT COUNT(*) FROM Sohbet#3", baglanti);
                int sohbet3_sayisi = Convert.ToInt16(komut2.ExecuteScalar());

                for (int i = 0; i < sohbet3_sayisi; i++)
                {
                    OleDbDataAdapter da2 = new OleDbDataAdapter("select * from Sohbet#3", baglanti);
                    DataTable dt3 = new DataTable();
                    listBox3.DataSource = dt3;
                    da2.Fill(dt3);
    
                    listBox3.DisplayMember = "kullanici_ve_mesaj";
                    listBox3.ValueMember = "ID";
                }

                baglanti.Close();
            }
        }

        private void panel3_Click(object sender, EventArgs e)
        {
            timer1.Stop();
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
            timer1.Stop();
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

        private void panel9_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            yardim yrd_nesne = new yardim();
            yrd_nesne.label6.Text = label6.Text.ToString();
            yrd_nesne.sifre = sifre;
            yrd_nesne.cinsiyet = cinsiyet;
            yrd_nesne.eposta = eposta;
            yrd_nesne.dogum_tarihi = dogum_tarihi;
            yrd_nesne.rutbe = rutbe;
            yrd_nesne.id = Convert.ToInt16(id);
            yrd_nesne.Show();
            this.Close();
        }

        private void panel10_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Are you sure to Exit?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (dialogResult == DialogResult.Yes)
            {
                timer1.Stop();
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
        }

        private void label1_Click(object sender, EventArgs e)
        {
                timer1.Stop();
                OleDbConnection baglanti = new OleDbConnection("Provider=Microsoft.JET.OLEDB.4.0;Data Source=database.mdb");
                baglanti.Open();
                OleDbCommand komut1 = new OleDbCommand();
                komut1.CommandText = "UPDATE kullanici set cevrimici_durumu='Çevrimdışı' WHERE kullanici_adi='" + label6.Text + "'";
                komut1.Connection = baglanti;
                komut1.ExecuteNonQuery();
                baglanti.Close();

                Application.Exit();
            }

        private void panel8_Click(object sender, EventArgs e)
        {
            
        }

        private void panel3_MouseHover(object sender, EventArgs e)
        {
            panel3.BackColor = Color.DodgerBlue;
            label8.ForeColor = Color.White;
            pictureBox1.Size = new Size(35, 35);
        }

        private void panel3_MouseLeave(object sender, EventArgs e)
        {
            panel3.BackColor = SystemColors.Control;
            label8.ForeColor = Color.Black;
            pictureBox1.Size = new Size(30, 30);
        }

        private void panel6_MouseHover(object sender, EventArgs e)
        {
            panel6.BackColor = Color.DodgerBlue;
            label14.ForeColor = Color.White;
            pictureBox5.Size = new Size(35, 35);
        }

        private void panel6_MouseLeave(object sender, EventArgs e)
        {
            panel6.BackColor = SystemColors.Control;
            label14.ForeColor = Color.White;
            pictureBox5.Size = new Size(30, 30);
        }

        private void panel7_Click(object sender, EventArgs e)
        {
            timer1.Stop();
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

        private void panel7_MouseHover(object sender, EventArgs e)
        {
            panel7.BackColor = Color.DodgerBlue;
            label22.ForeColor = Color.White;
            pictureBox3.Size = new Size(35, 35);
        }

        private void panel7_MouseLeave(object sender, EventArgs e)
        {
            panel7.BackColor = SystemColors.Control;
            label22.ForeColor = Color.Black;
            pictureBox3.Size = new Size(30, 30);
        }

        private void panel8_MouseHover(object sender, EventArgs e)
        {
            label23.ForeColor = Color.White;
            pictureBox4.Size = new Size(35, 35);
        }

        private void panel8_MouseLeave(object sender, EventArgs e)
        {
            label23.ForeColor = Color.White;
            pictureBox4.Size = new Size(30, 30);
        }

        private void panel9_MouseHover(object sender, EventArgs e)
        {
            panel9.BackColor = Color.DodgerBlue;
            label3.ForeColor = Color.White;
            pictureBox7.Size = new Size(35, 35);
        }

        private void panel9_MouseLeave(object sender, EventArgs e)
        {
            panel9.BackColor = SystemColors.Control;
            label3.ForeColor = Color.Black;
            pictureBox7.Size = new Size(30, 30);
        }

        private void panel10_MouseHover(object sender, EventArgs e)
        {
            panel10.BackColor = Color.DodgerBlue;
            label24.ForeColor = Color.White;
            pictureBox6.Size = new Size(35, 35);
        }

        private void panel10_MouseLeave(object sender, EventArgs e)
        {
            panel10.BackColor = SystemColors.Control;
            label24.ForeColor = Color.Black;
            pictureBox6.Size = new Size(30, 30);
        }

        private void panel2_MouseHover(object sender, EventArgs e)
        {
            panel2.BackColor = Color.DodgerBlue;
            label18.ForeColor = Color.White;
            pictureBox8.Size = new Size(35, 35);
        }

        private void groupBox4_Enter(object sender, EventArgs e)
        {

        }

        private void panel2_MouseLeave(object sender, EventArgs e)
        {
            panel2.BackColor = SystemColors.Control;
            label18.ForeColor = Color.Black;
            pictureBox8.Size = new Size(30, 30);
        }

        private void bunifuMetroTextbox1_DoubleClick(object sender, EventArgs e)
        {
            bunifuMetroTextbox1.Text = "";
        }

        private void pictureBox10_Click(object sender, EventArgs e)
        {
            if (secili_sohbet == 1)
            {
                OleDbConnection baglanti = new OleDbConnection("Provider=Microsoft.JET.OLEDB.4.0;Data Source=database.mdb");
                baglanti.Open();
                OleDbCommand komut = new OleDbCommand();
                label5.Text = label6.Text + ": " + bunifuMetroTextbox1.Text;
                komut.CommandText = "Insert Into Sohbet#1(kullanici,mesaj,kullanici_ve_mesaj) values ('" + label6.Text + "','" + bunifuMetroTextbox1.Text + "','"+label5.Text+"')";
                komut.Connection = baglanti;
                komut.ExecuteNonQuery();
                baglanti.Close();
            }

            if (secili_sohbet == 2)
            {
                OleDbConnection baglanti = new OleDbConnection("Provider=Microsoft.JET.OLEDB.4.0;Data Source=database.mdb");
                baglanti.Open();
                OleDbCommand komut = new OleDbCommand();
                label5.Text = label6.Text + ": " + bunifuMetroTextbox1.Text;
                komut.CommandText = "Insert Into Sohbet#2(kullanici,mesaj,kullanici_ve_mesaj) values ('" + label6.Text + "','" + bunifuMetroTextbox1.Text + "','" + label5.Text + "')"; 
                komut.Connection = baglanti;
                komut.ExecuteNonQuery();
                baglanti.Close();
            }

            if (secili_sohbet == 3)
            {
                OleDbConnection baglanti = new OleDbConnection("Provider=Microsoft.JET.OLEDB.4.0;Data Source=database.mdb");
                baglanti.Open();
                OleDbCommand komut = new OleDbCommand();
                label5.Text = label6.Text + ": " + bunifuMetroTextbox1.Text;
                komut.CommandText = "Insert Into Sohbet#3(kullanici,mesaj,kullanici_ve_mesaj) values ('" + label6.Text + "','" + bunifuMetroTextbox1.Text + "','" + label5.Text + "')"; 
                komut.Connection = baglanti;
                komut.ExecuteNonQuery();
                baglanti.Close();
            }
        }

        private void panel2_Click(object sender, EventArgs e)
        {
            timer1.Stop();
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

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }
     
    }
}
