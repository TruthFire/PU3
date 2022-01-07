using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace PU3
{
    public partial class Cart : Form
    {
        User curr;
        Order o;

        public Cart(User u)
        {
            curr = u;
            o = new(curr.getCart(), false);
            InitializeComponent();
            PrintPrice();
            RenderCart();


        }

        protected void PrintPrice()
        {
            if (curr.DaysToBd() <= 7 || curr.DaysToBd() >= 358)
            {
                label3.Visible = true;
                label4.Visible = true;
                label4.Text += " " + o.getOrderPriceString();
                o.ApplyBdDiscount();
            }
            label5.Text = "Galutine kaina: " + o.getOrderPriceString();
        }

        protected void RenderCart()
        {
            Product[] cart = curr.getCart();
            int panY = 9;
            for (int i = 0; i < cart.Length; i++)
            {
                Panel pan = new();
                pan.Name = "CartPan" + i.ToString();
                pan.Location = new Point(9, panY);
                pan.Size = new Size(579, 84);
                pan.BackColor = Color.White;


                PictureBox pb = new();
                pb.Name = "CartPb" + i.ToString();
                pb.Location = new Point(4, 4);
                pb.Load("https://i.imgur.com/" + cart[i].getImg());
                pb.Size = new Size(75, 75);
                pb.BorderStyle = BorderStyle.FixedSingle;
                pb.SizeMode = PictureBoxSizeMode.StretchImage;

                pan.Controls.Add(pb);


                Label l1 = new();
                l1.Location = new Point(85, 4);
                l1.Name = "NameLabel" + i.ToString();
                l1.Text = cart[i].getName();
                l1.AutoSize = true;

                pan.Controls.Add(l1);

                Label l2 = new();
                l2.Location = new Point(85, 29);
                l2.Name = "PriceLabel" + i.ToString();
                l2.Text = "Kaina: " + cart[i].getPrice() + " Eur.";
                l2.AutoSize = true;

                pan.Controls.Add(l2);

                Button removeBtn = new();
                removeBtn.Name = "removeBtn" + i.ToString();
                removeBtn.Text = "X";
                removeBtn.Size = new Size(18, 23);
                removeBtn.Location = new Point(558, 4);
                removeBtn.Click += new EventHandler(RemoveFromCart);

                pan.Controls.Add(removeBtn);

                panel1.Controls.Add(pan);

                panY += 95;
            }
        }


        protected void ClearPage()
        {
            panel1.Controls.Clear();

            IEnumerable<Control> ctrls = panel1.Controls.Cast<Control>();
            foreach (Control c in ctrls)
            {
                c.Dispose();
            }
        }


        protected void RemoveFromCart(object sender, EventArgs e)
        {
            Product[] cart = curr.getCart();
            Button btn = sender as Button;
            int itemNr = Convert.ToInt32(btn.Name.Substring(btn.Name.Length - 1));
            curr.RemoveFromCart(cart[itemNr].getId());
            o = new(curr.getCart(), false);
            ClearPage();
            RenderCart();
            PrintPrice();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Db db = new();
            db.payForCart(curr, o);


            panel1.Controls.Clear();
            panel1.Dispose();

            MessageBox.Show("Užsakymas apmokėtas");

            //MessageBox.Show(JsonConvert.SerializeObject(o));

            this.Close();
        }
    }
}
