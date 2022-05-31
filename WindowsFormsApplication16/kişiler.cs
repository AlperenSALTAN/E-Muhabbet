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
    public partial class kişiler : Form
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

        public kişiler()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 20, 20));
        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        bool dragging;
        Point offset;

        private void panel5_MouseUp(object sender, MouseEventArgs e)
        {
            dragging = false;
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

        private void panel5_MouseDown(object sender, MouseEventArgs e)
        {
            dragging = true;
            offset = e.Location;
        }
        OpenFileDialog dosya = new OpenFileDialog();
        string DosyaYolu, cevrimici="";

        private void kişiler_Load(object sender, EventArgs e)
        {
            arkadas_listeleme();
            takipci_listeleme();

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
                baglanti.Close();

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

        private void panel10_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Are you Sure to Logout?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
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

        private void panel9_Click(object sender, EventArgs e)
        {
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

        private void arkadas_listeleme()
        {
            OleDbConnection baglanti = new OleDbConnection("Provider=Microsoft.JET.OLEDB.4.0;Data Source=database.mdb");
            baglanti.Open();
            OleDbCommand komut1 = new OleDbCommand("SELECT COUNT(*) FROM arkadaslik Where ekleyen_kişi='" + label6.Text + "'", baglanti);
            int eklenen_arkadaslar_sayisi = Convert.ToInt16(komut1.ExecuteScalar());

            for (int i = 0; i < eklenen_arkadaslar_sayisi; i++)
            {
                OleDbDataAdapter Adaptor = new OleDbDataAdapter("Select * from arkadaslik where ekleyen_kişi='" + label6.Text + "'", baglanti);
                DataTable dt = new DataTable();
                listBox1.DataSource = dt;
                Adaptor.Fill(dt);
                listBox1.DisplayMember = "eklenen_kişi";
                listBox1.ValueMember = "ID";
            }

            baglanti.Close();

        }

        private void takipci_listeleme()
        {
            OleDbConnection baglanti = new OleDbConnection("Provider=Microsoft.JET.OLEDB.4.0;Data Source=database.mdb");
            baglanti.Open();
            OleDbCommand komut2 = new OleDbCommand("SELECT COUNT(*) FROM arkadaslik Where eklenen_kişi='" + label6.Text + "'", baglanti);
            int takipci_sayisi = Convert.ToInt16(komut2.ExecuteScalar());

            for (int i = 0; i < takipci_sayisi; i++)
            {
                OleDbDataAdapter Adaptor1 = new OleDbDataAdapter("Select * from arkadaslik where eklenen_kişi='" + label6.Text + "'", baglanti);
                DataTable dt1= new DataTable();
                listBox2.DataSource = dt1;
                Adaptor1.Fill(dt1);
                listBox2.DisplayMember = "ekleyen_kişi";
                listBox2.ValueMember = "ID";
            }

            baglanti.Close();

        }

        bool sonuc;
        private void pictureBox10_Click(object sender, EventArgs e)
        {
            arkadas_listeleme();

            OleDbConnection baglanti = new OleDbConnection("Provider=Microsoft.JET.OLEDB.4.0;Data Source=database.mdb");
            baglanti.Open();
            OleDbCommand ara_komut = new OleDbCommand();
            ara_komut.CommandText = "Select * from kullanici where kullanici_adi='" + bunifuMaterialTextbox1.Text + "' ";
            ara_komut.Connection = baglanti;
            OleDbDataReader aranan_oku = ara_komut.ExecuteReader();


            if (aranan_oku.Read())
            {
                bunifuFlatButton1.Visible = true;
                bunifuFlatButton2.Visible = false;
                groupBox1.Visible = true;

                string aranan_fotograf = aranan_oku["profil_fotograf"].ToString();
                string aranan_kullanici_adi = aranan_oku["kullanici_adi"].ToString();
                string aranan_dogum_tarihi = aranan_oku["dogum_tarihi"].ToString();
                string aranan_cevrimici_durum = aranan_oku["cevrimici_durumu"].ToString();
                string aranan_cinsiyet = aranan_oku["cinsiyet"].ToString();
                int aranan_yas = Convert.ToInt16(2018 - Convert.ToInt16(aranan_dogum_tarihi));

                CirclePictureBox3.ImageLocation = aranan_fotograf.ToString();
                label9.Text = aranan_dogum_tarihi;
                label11.Text = aranan_yas.ToString();
                label13.Text = aranan_cevrimici_durum;

                if (label13.Text == "Çevrimiçi")
                {
                    label13.ForeColor = Color.Green;
                }

                else
                {
                    label13.ForeColor = Color.Red;
                }

                label16.Text = aranan_cinsiyet;
                label5.Text = aranan_kullanici_adi.ToString();

                OleDbCommand Cmd = new OleDbCommand("SELECT COUNT(*) FROM arkadaslik where eklenen_kişi='" + label5.Text + "'", baglanti);
                label25.Text = Cmd.ExecuteScalar().ToString();

                OleDbCommand Cmd1 = new OleDbCommand("SELECT COUNT(*) FROM arkadaslik where ekleyen_kişi='" + label5.Text + "'", baglanti);
                label26.Text = Cmd1.ExecuteScalar().ToString();

                if (label5.Text == label6.Text)
                {
                    bunifuThinButton21.Visible = false;
                    bunifuThinButton22.Visible = false;
                    bunifuThinButton23.Visible = false;
                }

                else
                {
                    OleDbCommand komut2 = new OleDbCommand();
                    komut2.CommandText = "Select * from arkadaslik where eklenen_kişi='" + label5.Text + "' and ekleyen_kişi='" + label6.Text + "' ";
                    komut2.Connection = baglanti;
                    OleDbDataReader oku = komut2.ExecuteReader();

                    if (oku.Read())
                    {
                        bunifuThinButton21.Visible = false;
                        bunifuThinButton22.Visible = true;
                        bunifuThinButton23.Visible = true;
                    }


                    else
                    {
                        bunifuThinButton21.Visible = true;
                        bunifuThinButton22.Visible = false;
                        bunifuThinButton23.Visible = false;
                    }
                }
        
             }

            else
            {
                bunifuFlatButton1.Visible = false;
                bunifuFlatButton2.Visible = true;
                groupBox1.Visible = false;
            }
           
            baglanti.Close();
        }

        private void bunifuThinButton21_Click(object sender, EventArgs e)
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

        private void panel7_MouseHover(object sender, EventArgs e)
        {
            label23.ForeColor = Color.White;
            pictureBox3.Size = new Size(35, 35);
        }

        private void panel7_MouseLeave(object sender, EventArgs e)
        {
            label23.ForeColor = Color.White;
            pictureBox3.Size = new Size(30, 30);
        }

        private void panel8_MouseHover(object sender, EventArgs e)
        {
            panel8.BackColor = Color.DodgerBlue;
            label22.ForeColor = Color.White;
            pictureBox4.Size = new Size(35, 35);
        }

        private void panel8_MouseLeave(object sender, EventArgs e)
        {
            panel8.BackColor = SystemColors.Control;
            label22.ForeColor = Color.Black;
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

        private void panel2_MouseLeave(object sender, EventArgs e)
        {
            panel2.BackColor = SystemColors.Control;
            label18.ForeColor = Color.Black;
            pictureBox8.Size = new Size(30, 30);
        }

        private void bunifuMaterialTextbox1_OnValueChanged(object sender, EventArgs e)
        {

        }

        private void bunifuMaterialTextbox1_Click(object sender, EventArgs e)
        {
            bunifuMaterialTextbox1.Text = "";
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
           
        }

        private void bunifuThinButton21_Click_1(object sender, EventArgs e)
        {
            OleDbConnection baglanti = new OleDbConnection("Provider=Microsoft.JET.OLEDB.4.0;Data Source=database.mdb");
            baglanti.Open();
            OleDbCommand komut = new OleDbCommand();
            komut.CommandText = "Insert Into arkadaslik(eklenen_kişi,ekleyen_kişi) values('" + label5.Text + "','" + label6.Text + "') ";
            komut.Connection = baglanti;
            komut.ExecuteNonQuery();
            baglanti.Close();

            bunifuThinButton21.Visible = false;
            bunifuThinButton22.Visible = true;
            bunifuThinButton23.Visible = true;

            takipci_listeleme();
            arkadas_listeleme();
        }

        private void bunifuThinButton22_Click(object sender, EventArgs e)
        {
            OleDbConnection baglanti = new OleDbConnection("Provider=Microsoft.JET.OLEDB.4.0;Data Source=database.mdb");
            baglanti.Open();
            OleDbCommand komut = new OleDbCommand();
            komut.CommandText = "Delete from arkadaslik where eklenen_kişi='" + label5.Text + "' and ekleyen_kişi='" + label6.Text + "'";
            komut.Connection = baglanti;
            komut.ExecuteNonQuery();
            baglanti.Close();

            bunifuThinButton21.Visible = true;
            bunifuThinButton22.Visible = false;
            bunifuThinButton23.Visible = false;

            takipci_listeleme();
            arkadas_listeleme();
            
        }

        private void label28_Click(object sender, EventArgs e)
        {

        }

        private void bunifuThinButton23_Click(object sender, EventArgs e)
        {
           

        }

        private void label20_Click(object sender, EventArgs e)
        {

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

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

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
