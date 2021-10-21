using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;

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
            bs.DataSource = db.FillGridView();
            dataGridView1.DataSource = bs;
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
                if(curr.IsAdmin())
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
    }
}
