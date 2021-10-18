using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PU3
{
    public partial class Main : Form
    {

        private User currentUser;
        public Main(User u)
        {
            InitializeComponent();
            this.currentUser = u;
            if(currentUser.GetGroup() == 2)
            {
                this.button2.Visible = true;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }
    }
}
