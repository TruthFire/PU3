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
    public partial class Profile : Form
    {
        public Profile(User u)
        {
            InitializeComponent();
            this.textBox1.Text = u.Person.GetName();
            this.textBox2.Text = u.Person.GetSurename();
            this.textBox3.Text = u.GetNick();
            this.textBox4.Text = u.GetPwd();
        }
    }
}
