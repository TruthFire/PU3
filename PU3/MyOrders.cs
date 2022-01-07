using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace PU3
{
    public partial class MyOrders : Form
    {
        User curr;
        Order[] orders;
        public MyOrders(User u)
        {
            curr = u;
            InitializeComponent();
            Db db = new Db();
            orders = db.getUserOders(curr.GetId());

            RenderOrders();
        }


        protected void RenderOrders()
        {
            int PanY = 3;
            for (int i = 0; i < orders.Length; i++)
            {
                Panel p = new Panel();
                p.Name = "order" + i.ToString();
                p.Size = new Size(591, 74);
                p.Location = new Point(3, PanY);
                p.BackColor = Color.White;

                Label l1 = new Label();
                l1.Text = "Id: " + orders[i].id.ToString();
                l1.Location = new Point(8, 5);
                l1.Name = "idlbl" + i.ToString();
                l1.AutoSize = true;

                p.Controls.Add(l1);

                string items = "";
                for (int j = 0; j < orders[i].orderProducts.Count(); j++)
                {
                    items += orders[i].orderProducts[j].name + "; ";
                }

                Label l2 = new Label();
                l2.Text = "Produktai: " + items;
                l2.Location = new Point(8, 26);
                l2.Name = "productlbl" + i.ToString();
                l2.AutoSize = true;

                p.Controls.Add(l2);

                Label l3 = new Label();
                l3.Text = "Kaina: " + orders[i].orderPrice;
                l3.Location = new Point(8, 51);
                l3.Name = "pricelbl" + i.ToString();
                l3.AutoSize = true;

                p.Controls.Add(l3);

                Label l4 = new Label();
                l4.Text = "Data: " + orders[i].o_date;
                l4.Location = new Point(410, 5);
                l4.Name = "datelbl" + i.ToString();
                l4.AutoSize = true;

                p.Controls.Add(l4);

                panel1.Controls.Add(p);
                PanY += 84;
            }
        }
    }
}
