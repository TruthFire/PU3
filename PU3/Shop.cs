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
    public partial class Shop : Form
    {
        List<Panel> renderedPanels;
        int currentPage = 1;
        Product[] prods;
        User curr;
        public Shop(User u = null)
        {
            
            Db db = new();
            string[] nodes = db.getCategories();
            prods = db.GetProducts(1);
            InitializeComponent();
            if (u != null)
            {
                button2.Visible = false;
                curr = u;
                button3.Visible = true;
                button4.Visible = true;
            }

            for (int i = 0; i < nodes.Length; i++)
            {
                treeView1.Nodes.Add(nodes[i]);
            }
            renderedPanels = ShowProducts(1, prods);
            ShowPages(1);


            //this.Controls.Add(new LinkLabel() { Name = "ll1", Text = "Link", Location = new System.Drawing.Point(34, 134), Size = new Size(60,15) });

        }

        public List<Panel> ShowProducts(int page, Product[] prods)
        {
            List<Panel> panels = new List<Panel>();
            int AddY = 0, xMult = 1;
            for(int i = 0;i < prods.Length; i++)
            {
                if (i % 4 == 0)
                {
                    xMult = 1;
                    AddY += 163;
                }
                Panel pan = new Panel();
                pan.Name = "panel" + i;
                pan.BorderStyle = BorderStyle.FixedSingle;
                pan.Location = new Point(152 * xMult, 57 + AddY); //location
                pan.Size = new Size(133, 157);  //size
                pan.BackColor = Color.White;
                
                LinkLabel prodLabel = new();
                prodLabel.Name = "prodLl"+i.ToString();
                prodLabel.Text = prods[i].getName();
                prodLabel.Location = new Point(3, 130);
                prodLabel.Click += new EventHandler(this.prodLl_Click);
                pan.Controls.Add(prodLabel);
                PictureBox pb = new();
                pb.Name = "prodPb" + i.ToString();
                pb.Size = new Size(125, 125);
                pb.Location = new Point(3, 3);
                pb.BorderStyle = BorderStyle.FixedSingle;
                pb.Load("https://i.imgur.com/" + prods[i].getImg());
                pan.Controls.Add((PictureBox)pb);
                panels.Add(pan);
                this.Controls.Add(pan);
                xMult++;
               
            }
            return panels;

        }

        public void ShowPages(int currPage)
        {
            int xVal = 62;
            Panel panel = new Panel();
            panel.Name = "pagesPanel";
            panel.Size = new Size(590, 31);
            panel.Location = new Point(152, 386);
            panel.BorderStyle = BorderStyle.FixedSingle;
            for (int i = 1; i <= 8; i++)
            {
                if (i == currPage)
                {
                    Label currentP = new Label();
                    currentP.Name = "currentPageLabel" + i.ToString();
                    currentP.Text = i.ToString();
                    currentP.Location = new Point(xVal, 8);
                    currentP.Size = new Size(15, 15);
                    panel.Controls.Add(currentP);
                }
                else
                {
                    LinkLabel otherP = new LinkLabel();
                    otherP.Name = "currentPageLabel" + i.ToString();
                    otherP.Text = i.ToString();
                    otherP.Location = new Point(xVal, 8);
                    otherP.Size = new Size(15, 15);
                    panel.Controls.Add(otherP);

                }
                
                xVal += 15;
            }

            this.Controls.Add(panel);
            //return panel;

        }

        void prodLl_Click(object sender, EventArgs e)
        {
            LinkLabel ll = sender as LinkLabel;
            int num = Convert.ToInt32(ll.Name.Substring(ll.Name.Length - 1));
            ShopItem si = new(prods[num], curr);
            si.Show();
        }

        private void Shop_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
           foreach(Panel panel in this.renderedPanels)
            {
                panel.Dispose();
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Auth auth = new Auth();
            auth.Show();
            
        }
    }
}


