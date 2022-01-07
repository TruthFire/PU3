using System;
using System.Windows.Forms;

namespace PU3
{
    public partial class AddProduct : Form
    {
        User curr;
        public AddProduct(User u)
        {
            curr = u;
            Db db = new();
            String[] categories = db.getCategories();
            InitializeComponent();
            for (int i = 0; i < categories.Length; i++)
            {
                comboBox1.Items.Add(categories[i]);
            }
            comboBox1.SelectedIndex = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Db db = new();
            db.addProduct(textBox1.Text, comboBox1.SelectedItem.ToString(), (int)numericUpDown1.Value, textBox2.Text, textBox3.Text);
            MessageBox.Show("Sekmingai");
        }

        private void AddProduct_FormClosed(object sender, FormClosedEventArgs e)
        {
            APanel ap = new(curr);
            ap.Show();
        }
    }
}
