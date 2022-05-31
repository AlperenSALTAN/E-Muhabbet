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
    public partial class register : Form
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

        public register()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 20, 20));
        }

        private void label2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void label2_MouseHover(object sender, EventArgs e)
        {
            label2.ForeColor = Color.Red;
            label2.Font = new Font(label2.Font, FontStyle.Bold);
        }

        private void label2_MouseLeave(object sender, EventArgs e)
        {
            label2.Font = new Font(label2.Font, FontStyle.Regular);
            label2.ForeColor = Color.Black;

        }

        private void register_Load(object sender, EventArgs e)
        {
            ToolTip aciklama = new ToolTip();
            aciklama.ShowAlways = true;

            aciklama.SetToolTip(label2, "Close");
            aciklama.SetToolTip(label3, "Recuve");

            for (int i = 1960; i < 2019; i++)
            {
                comboBox3.Items.Add(i);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OleDbConnection baglanti = new OleDbConnection("Provider=Microsoft.JET.OLEDB.4.0;Data Source=database.mdb");
            baglanti.Open();

            if (radioButton1.Checked == true)
            {
                label12.Text = "Girl";
            }

            else if (radioButton2.Checked == true)
            {
                label12.Text = "Boy";
            }
            else
            {
                MessageBox.Show("Please Select Gender/Sex");
            }

            int dogum_tarihi = Convert.ToInt16(comboBox3.SelectedItem);

            if (dogum_tarihi <= 2000)
            {
                if (textBox3.Text == textBox2.Text)
                {
                    OleDbCommand komut = new OleDbCommand();
                    komut.CommandText = "Insert Into kullanici(kullanici_adi,eposta,sifre,cinsiyet,dogum_tarihi,rutbe,ban_durumu,profil_fotograf) values ('" + textBox1.Text + "','" + textBox4.Text + "','" + textBox2.Text + "','" + label12.Text + "','" + comboBox3.SelectedItem + "','Kullanıcı','0','https://i.hizliresim.com/nljod0.png') ";
                    komut.Connection = baglanti;
                    int sonuc = komut.ExecuteNonQuery();
                    if (sonuc > 0)
                    {
                        MessageBox.Show(textBox1.Text + " named User Successful registered");
                        Console.Beep(500, 1000);
                    }

                    else
                    {
                        MessageBox.Show("Register Failed");
                    }

                }

                else
                {
                    MessageBox.Show("Password Don't Match");
                } 
            }

            else
            {
                MessageBox.Show("Sorry, there is no Under 18 Registration available.");
            }
            
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            if (textBox2.Text == textBox3.Text)
            {
                label11.Text = "Password Matched";
                label11.ForeColor = Color.Green;
            }


            else
            {
                label11.Text = "Password Don't Matched";
                label11.ForeColor = Color.Red;
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (textBox3.Text == null)
            {
                if (textBox4.Text == null)
                {
                    label11.Text = "Password Don't Matched";
                    label11.ForeColor = Color.Red;
                }

                else if (textBox2.Text == textBox3.Text)
                {
                    label11.Text = "Password Matched";
                    label11.ForeColor = Color.Green;
                }
                else
                {
                    label11.Text = "Password Don't Matched";
                    label11.ForeColor = Color.Red;
                }
            }
            
        }

        private void label13_Click(object sender, EventArgs e)
        {
            Form1 nesne = new Form1();
            nesne.Show();
            this.Close();
        }

        private void label13_MouseHover(object sender, EventArgs e)
        {
            label13.ForeColor = Color.Red;
            label13.Font = new Font(label13.Font, FontStyle.Bold);
            label13.Font = new Font(label13.Font, FontStyle.Underline);
        }

        private void label13_MouseLeave(object sender, EventArgs e)
        {
            label13.ForeColor = Color.Black;
            label13.Font = new Font(label13.Font, FontStyle.Underline);
        }

        private void label3_MouseHover(object sender, EventArgs e)
        {
            label3.ForeColor = Color.Red;
            label3.Font = new Font(label3.Font, FontStyle.Bold);
        }

        private void label3_MouseLeave(object sender, EventArgs e)
        {
            label3.ForeColor = Color.Black;
            label3.Font = new Font(label3.Font, FontStyle.Regular);
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

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
