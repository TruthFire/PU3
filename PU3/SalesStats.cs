using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.IO;
using Newtonsoft.Json;

namespace PU3
{
    public partial class SalesStats : Form
    {
        Order[] orders;
        public SalesStats()
        {
            InitializeComponent();
            Db db = new Db();
            label1.Text += db.GetOrderAmount();
            orders = db.GetOrderList();

            RenderOrders();
        }

        protected void RenderOrders()
        {
            int PanY = 3;
            for(int i = 0 ; i < orders.Length; i++)
            {
                Panel p = new Panel();
                p.Name = "order" + i.ToString();
                p.Size = new Size(591, 74);
                p.Location = new Point(3, PanY);
                p.BackColor = Color.White;

                Label l1 = new Label();
                l1.Text = "Id: " + orders[i].id.ToString();
                l1.Location = new Point(8,5);
                l1.Name = "idlbl" + i.ToString();
                l1.AutoSize = true;

                p.Controls.Add(l1);

                string items = "";
                for(int j = 0; j < orders[i].orderProducts.Count(); j++)
                {
                    items += orders[i].orderProducts[j].name +"; ";
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

        private void button1_Click(object sender, EventArgs e)
        {
            //string path = @"ataskaita_" + DateTime.Now.ToString() + ".json";
            string path = @"Ataskaita_" + DateTime.Now.ToString("MM-dd-yyyy_H-mm-ss") + ".json";
            // This text is added only once to the file.
            if (!File.Exists(path))
            {
                // Create a file to write to.
                using (StreamWriter sw = File.CreateText(path))
                {
                    sw.WriteLine("{");

                    for (int i = 0; i < orders.Length; i++)
                    {   
                        string line = "\"order" +orders[i].id + "\":" + JsonConvert.SerializeObject(orders[i]);
                        if(i != orders.Length - 1)
                        {
                            line += ",";
                        }
                        
                        sw.WriteLine(line);
                    }
                    sw.WriteLine("}"); 

                }

                MessageBox.Show("Failas sukurtas. " + path);


            }
            
        }
    }
}
