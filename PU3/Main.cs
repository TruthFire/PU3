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
                button2.Text = "APanel";
            }
            if(currentUser.GetGroup() == 3)
            {
                button2.Text = "Užsakymai";

            }
            else if(currentUser.GetGroup() == 1)
            {
                button2.Text = "Mano užsakymai";

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

        private void button2_Click(object sender, EventArgs e)
        {
            if (currentUser.GetGroup() == 2)
            {
                APanel ap = new(currentUser);
                ap.Show();
                this.Hide();
            }
            else if (currentUser.GetGroup() == 3)
            {
                SalesStats ss = new();
                ss.Show();
            }
            else
            {
                MyOrders mo = new(currentUser);
                mo.Show();
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            WishList wl = new(currentUser);
            wl.Show();
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Shop s = new(currentUser);
            s.Show();
            this.Hide();
        }
    }
}
