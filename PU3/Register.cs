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
    public partial class Register : Form
    {

        public Register()
        {
            InitializeComponent();
        }
        

        private string IsNullOrSpace(string s)
        {
            if (s == null || s.Trim() == string.Empty)
            {
                throw new ArgumentException("Ivedimas ne gali buti tuscias arba lygus null");
            }
            return s;

        }

        private DateTime IsDateValid(DateTime d)
        {
            DateTime curr = DateTime.Today;
            if (d.Day >= curr.Day && d.Month >= curr.Month && d.Year > curr.Year)
            {
                throw new ArgumentException("Gimimo data negali buti didesni, nei esama");
            }
            return d;
        }

        private bool DoPasswordsMatch(string p1, string p2)
        {
            return p1 == p2;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            try
            {
                if (DoPasswordsMatch(Pw1_Input.Text, Pw2_Input.Text))
                {
                    Person p;
                    string name = IsNullOrSpace(this.Name_Input.Text);
                    string surename = IsNullOrSpace(Surename_Input.Text);
                    DateTime dob = IsDateValid(DateTime.Parse(Dob_Input.Text));
                    p = new Person(name, surename, dob);
                    if (Convert.ToInt32(p.GetAge()) >= 14)
                    {
                        User NewUser = new(p, Nickname_Input.Text, Pw1_Input.Text, 1);
                        Db Database = new();
                        Database.CreateUser(NewUser);

                    }
                    }
                    else
                    {
                    ErrLabel.Text = "Slaptažodžiai turi sutapti";
                }

            }
            catch (Exception exc)
            {
                this.Name_Input.Text = ""; this.Surename_Input.Text = ""; this.Dob_Input.Text = "";
                MessageBox.Show(exc.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }
    }
}
