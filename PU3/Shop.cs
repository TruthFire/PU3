using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace PU3
{
    public partial class Shop : Form
    {
        List<Panel> renderedPanels;
        Product[] prods;
        User curr;
        string[] nodes;
        public Shop(User u = null)
        {

            Db db = new();
            // u = db.GetUser("usr", "111"); //dbg mode   
            nodes = db.getCategories();
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
            renderedPanels = ShowProducts(prods);



            //this.Controls.Add(new LinkLabel() { Name = "ll1", Text = "Link", Location = new System.Drawing.Point(34, 134), Size = new Size(60,15) });

        }

        public List<Panel> ShowProducts(Product[] prods)
        {

            List<Panel> panels = new List<Panel>();
            int AddY = 0, xMult = 1, rendered = 0;
            for (int i = 0; i < prods.Length; i++)
            {
                if (rendered == 4)
                {
                    xMult = 1;
                    AddY += 163;
                    rendered = 0;

                }
                Panel pan = new Panel();
                pan.Name = "panel" + i;
                pan.BorderStyle = BorderStyle.FixedSingle;
                pan.Location = new Point(152 * xMult, 57 + AddY); //location
                pan.Size = new Size(133, 157);  //size
                pan.BackColor = Color.White;

                LinkLabel prodLabel = new();
                prodLabel.Name = "prodLl" + i.ToString();
                prodLabel.Text = prods[i].getName();
                prodLabel.Location = new Point(3, 130);
                prodLabel.Click += new EventHandler(this.prodLl_Click);
                pan.Controls.Add(prodLabel);


                PictureBox pb = new();
                pb.Name = "prodPb" + i.ToString();
                pb.Size = new Size(125, 125);
                pb.Location = new Point(3, 3);
                pb.BorderStyle = BorderStyle.FixedSingle;
                pb.SizeMode = PictureBoxSizeMode.StretchImage;
                pb.Load("https://i.imgur.com/" + prods[i].getImg());
                pan.Controls.Add((PictureBox)pb);
                panels.Add(pan);
                this.Controls.Add(pan);
                xMult++;
                rendered++;

            }



            treeView1.Height = 162 * (int)Math.Ceiling((double)panels.Count() / 4);
            return panels;

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
            foreach (Panel panel in this.renderedPanels)
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

        private void ClearPanels()
        {
            foreach (Panel panel in this.renderedPanels)
            {
                panel.Dispose();
            }
        }

        private void treeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            Db db = new();
            prods = db.GetProducts(db.getCategoryId(e.Node.Text));
            if (renderedPanels != null)
                ClearPanels();
            renderedPanels = ShowProducts(prods);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Main m = new(curr);
            m.Show();
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Shop ss = new Shop();
            ss.Show();
            this.Hide();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            Cart c = new(curr);
            c.Show();
        }
    }
}


