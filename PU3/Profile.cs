using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;

namespace PU3
{
    public partial class Profile : Form
    {
        User curr;
        public Profile(User u)
        {
            InitializeComponent();
            curr = u;
            this.textBox1.Text = u.GetName();
            this.textBox2.Text = u.GetSurename();
            this.pictureBox1.Image = new Bitmap(string.Format(@"\img\avatars\avatar\{0}\", curr.GetId()));
        }
        
        private void EditProfileSwitch(bool status) // True = EditMode OFF, false = EditMode ON
        {
            this.textBox1.ReadOnly = status;
            this.textBox2.ReadOnly = status;
            button1.Visible = status;
            button3.Visible = !status;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            EditProfileSwitch(false);
        }

        private void Profile_FormClosed(object sender, FormClosedEventArgs e)
        {
            Main m = new(curr);
            m.Show();
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp)|*.jpg; *.jpeg; *.gif; *.bmp";
            if (open.ShowDialog() == DialogResult.OK)
            {
             
                pictureBox1.Image = new Bitmap(open.FileName);
                
                var fn = open.FileName;
                File.Copy(fn, string.Format(@"\img\avatars\avatar\{0}\" + Path.GetFileName(fn), curr.GetId()));
                Db db = new();
                db.UpdateImg(curr.GetId(), fn);

            }
        }
    }
}
