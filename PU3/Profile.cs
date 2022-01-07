using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace PU3
{
    public partial class Profile : Form
    {
        User curr;
        public Profile(User u)
        {
            InitializeComponent();
            curr = u;
            Db db = new();
            Image avtr;
            if (db.GetAvatar(u.GetId()) != "NoAvatar")
            {
                avtr = Image.FromFile(string.Format(@"img\avatars\{0}\{1}", curr.GetId(), db.GetAvatar(u.GetId())));
                this.pictureBox1.Image = new Bitmap(avtr, 100, 100);
            }
            else
            {
                avtr = Image.FromFile(@"NoAvatar.png");

            }
            this.pictureBox1.Image = new Bitmap(avtr, 100, 100);
        }

        private void EditProfileSwitch(bool status) // True = EditMode OFF, false = EditMode ON
        {
            this.textBox1.ReadOnly = status;
            this.textBox2.ReadOnly = status;
            this.textBox3.ReadOnly = status;
            button1.Visible = status;
            button4.Visible = !status;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            EditProfileSwitch(false);
        }

        private void Profile_FormClosed(object sender, FormClosedEventArgs e)
        {
            Main m = new(curr);
            m.Show();
        }


        private void button3_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp)|*.jpg; *.jpeg; *.gif; *.bmp";
            if (open.ShowDialog() == DialogResult.OK)
            {
                var fn = open.FileName;

                String AvtrDir = String.Format(@"img\avatars\{0}\" + Path.GetFileName(fn), curr.GetId());
                if (!File.Exists(AvtrDir))
                {
                    pictureBox1.Image = new Bitmap(fn);
                    Image avtr = Image.FromFile(fn);
                    this.pictureBox1.Image = new Bitmap(avtr, 100, 100);
                    Directory.CreateDirectory(String.Format(@"img\avatars\{0}\", curr.GetId()));
                    File.Copy(fn, AvtrDir);
                    Db db = new();
                    db.UpdateAvatar(curr.GetId(), Path.GetFileName(fn));
                }
                else
                {
                    MessageBox.Show("Failo pavadinimas netinka.");
                }

            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string OldPwd = textBox1.Text;
            string NewPwd = textBox2.Text;
            string NewPwd1 = textBox3.Text;
            Db db = new();
            if (NewPwd != NewPwd1)
            {
                this.label5.Text = "Nauji slaptažodžiai nesutampa";
            }
            else if (!db.CheckPwd(curr.GetId(), OldPwd))
            {
                MessageBox.Show(db.CheckPwd(curr.GetId(), OldPwd).ToString());
                this.label5.Text = "Senas slaptažodis įvestas neteisingai";
            }
            else
            {
                db.SetPwd(NewPwd, curr.GetId());
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Main m = new(curr);
            //m.Show();
            this.Close();
        }
    }
}
