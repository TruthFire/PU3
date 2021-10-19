using System;
using System.Windows.Forms;

namespace PU3
{
    public partial class Main : Form
    {

        private User currentUser;
        public Main(User u)
        {
            InitializeComponent();
            currentUser = u;
            if (currentUser.GetGroup() == 2)
            {
                button2.Visible = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Profile prof = new(currentUser);
            prof.Show();
            this.Hide();
        }

        private void Main_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}
