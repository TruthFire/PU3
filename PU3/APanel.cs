using System;
using System.Windows.Forms;

namespace PU3
{
    public partial class APanel : Form
    {
        User curr;
        public APanel(User a)
        {
            curr = a;
            InitializeComponent();
            UpdateDg();
        }

        public void UpdateDg()
        {
            Db db = new();
            BindingSource bs = new();
            bs.DataSource = db.FillUserGridView();
            dataGridView1.DataSource = bs;

            BindingSource bs2 = new();
            bs2.DataSource = db.FillProductGridView();
            dataGridView2.DataSource = bs2;

            BindingSource bs3 = new();
            bs3.DataSource = db.FillCategoryGridView();
            dataGridView3.DataSource = bs3;

            BindingSource bs4 = new();
            bs4.DataSource = db.FillLogsGridView();
            dataGridView4.DataSource = bs4;
        }

        private void APanel_FormClosed(object sender, FormClosedEventArgs e)
        {
            Main m = new(curr);
            m.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (curr.GetGroup() == 2)
                {
                    Db db = new();
                    db.DeleteUser(Convert.ToInt32(this.textBox1.Text));
                    UpdateDg();
                    MessageBox.Show("Sekmingai");
                }
                else
                {
                    throw new ArgumentException("Jūs neturite prieigos vykdyti šią komandą.");
                }

            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            AddProduct ap = new(curr);
            ap.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                if (curr.GetGroup() == 2)
                {
                    Db db = new();
                    db.RemoveProduct(Convert.ToInt32(this.textBox2.Text));
                    UpdateDg();
                    MessageBox.Show("Sekmingai");
                }
                else
                {
                    throw new ArgumentException("Jūs neturite prieigos vykdyti šią komandą.");
                }

            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                if (curr.GetGroup() == 2)
                {
                    Db db = new();
                    db.DeleteCategory(Convert.ToInt32(this.textBox2.Text));
                    UpdateDg();
                    MessageBox.Show("Kategorija ir visi joje esantys produktai buvo pašalinti");
                }
                else
                {
                    throw new ArgumentException("Jūs neturite prieigos vykdyti šią komandą.");
                }

            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            AddCategory ac = new(curr);
            ac.Show();
        }
    }
}
