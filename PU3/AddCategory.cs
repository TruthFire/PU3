using System;
using System.Windows.Forms;

namespace PU3
{
    public partial class AddCategory : Form
    {
        User curr;
        public AddCategory(User u)
        {
            curr = u;
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Db db = new Db();
            db.AddCategory(textBox1.Text);
        }

        private void AddCategory_FormClosed(object sender, FormClosedEventArgs e)
        {

        }
    }
}
