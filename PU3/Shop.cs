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
        public Shop()
        {
            Db db = new();
            string[] nodes = db.getCategories();
            Product[] prods = db.GetProducts(1);
            InitializeComponent();
            for(int i = 0; i < nodes.Length; i++)
            {
                treeView1.Nodes.Add(nodes[i]);
            }
            renderedPanels = ShowProducts(1, prods);


            //this.Controls.Add(new LinkLabel() { Name = "ll1", Text = "Link", Location = new System.Drawing.Point(34, 134), Size = new Size(60,15) });

        }

        

        public List<Panel> ShowProducts(int page, Product[] prods)
        {
            List<Panel> panels = new List<Panel>(); 
            int AddY = 0, xMult = 1, i;
            if (page == 1) i = 0;
            else i = 1;
            for(i = i*page;i < 8*page; i++)
            {
                if (i == 4)
                {
                    xMult = 1;
                    AddY = 163;
                }
                Panel pan = new Panel();
                pan.Name = "panel" + i;
                pan.BorderStyle = BorderStyle.FixedSingle;
                pan.Location = new Point(152 * xMult, 57 + AddY); //location
                pan.Size = new Size(133, 157);  //size
                panels.Add(pan);
                LinkLabel prodLabel = new();
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
                this.Controls.Add(pan);
                xMult++;
               
            }
            return panels;

        }

        void prodLl_Click(object sender, EventArgs e)
        {
            LinkLabel ll = sender as LinkLabel;
            MessageBox.Show(ll.Text);
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
    }
}


