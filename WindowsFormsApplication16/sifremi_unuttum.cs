using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Mail;
using System.Data.OleDb;
using System.Runtime.InteropServices;


namespace WindowsFormsApplication16
{
    public partial class sifremi_unuttum : Form
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

        OleDbConnection baglanti = new OleDbConnection("Provider=Microsoft.JET.OLEDB.4.0;Data Source=database.mdb");
        OleDbCommand komut = new OleDbCommand();

        public sifremi_unuttum()
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
            label2.Font = new Font(label2.Font, FontStyle.Regular);

        }

        private void label2_MouseLeave(object sender, EventArgs e)
        {
            label2.ForeColor = Color.Black;
            label2.Font = new Font(label2.Font, FontStyle.Regular);

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

        private void label13_Click(object sender, EventArgs e)
        {
            Form1 nesne = new Form1();
            nesne.Show();
            this.Close();
        }

        private void label13_MouseHover(object sender, EventArgs e)
        {
            label13.ForeColor = Color.Red;
            label13.Font = new Font(label13.Font, FontStyle.Underline);

        }

        private void label13_MouseLeave(object sender, EventArgs e)
        {
            label13.ForeColor = Color.Black;
            label13.Font = new Font(label13.Font, FontStyle.Underline);

        }

        private void sifremi_unuttum_Load(object sender, EventArgs e)
        {
            ToolTip aciklama = new ToolTip();
            aciklama.ShowAlways = true;

            aciklama.SetToolTip(label2, "Close");
            aciklama.SetToolTip(label3, "Recuve"); 
        }

        private void button1_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            komut.CommandText = "Select sifre,eposta,kullanici_adi from kullanici where kullanici_adi='" + textBox1.Text + "' and eposta='"+textBox2.Text+"'";
            komut.Connection = baglanti;

            OleDbDataReader oku = komut.ExecuteReader();

            if (oku.Read())
            {
                string sifre = oku["sifre"].ToString();
                string ePosta1 = oku["eposta"].ToString();
                string kullanici_adi = oku["kullanici_adi"].ToString();

                MailMessage ePosta = new MailMessage();
                ePosta.From = new MailAddress("YOUR GMAIL ADDRESS");//buraya kendi gmail hesabınız
                ePosta.To.Add(ePosta1);//bura şifre unutanın maili textboxdan çekdiniz.
                ePosta.Subject = "e-Muhabbet ~ Password Reminder"; //butonda veri tabanı çekdikden sonra aldımız değer konu değeri
                //
                ePosta.Body = " Hello " + kullanici_adi + " , We received information that you forgot your password , We sends your password. Your Password: " + "'" + sifre + "'";  // buda şifremiz
                //
                SmtpClient smtp = new SmtpClient();
                //
                smtp.Credentials = new System.Net.NetworkCredential("YOUR GMAIL ADDRESS", "YOUR GMAIL PASSWORD");
                //kendi gmail hesabiniz var şifresi
                smtp.Port = 587;
                smtp.Host = "smtp.gmail.com";
                smtp.EnableSsl = true;
                object userState = ePosta;
                bool kontrol;
                kontrol = true;

                try
                {
                    smtp.SendAsync(ePosta, (object)ePosta);

                    MessageBox.Show("Your Email Has Been Sent Successfully, Check your Incoming Emails.", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (SmtpException ex)
                {
                    kontrol = false;
                    System.Windows.Forms.MessageBox.Show(ex.Message, "Mail Sending Error");
                }

            }

            else
            {
                MessageBox.Show("User or E-mail Not Matched");
            }

            baglanti.Close();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

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
    }
}
