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
    public partial class WishList : Form
    {
        User curr;
        int[] wishlistedItems = null;
        List<Panel> renderedItems = new List<Panel>();

        public WishList(User u)
        {
            curr = u;
            wishlistedItems = u.getWishedIds();
            InitializeComponent();

            renderWishList();

        }


        private void renderWishList()
        {
            if (wishlistedItems != null) {
                int y = 8;
                Db db = new();
                for (int i = 0; i < wishlistedItems.Length; i++)
                {
                    Panel p = new Panel();
                    p.Size = new Size(605, 38);
                    p.BackColor = Color.White;
                    p.Location = (new Point(9, y));
                    p.BorderStyle = BorderStyle.FixedSingle;
                    p.Name = "wishPanel" + i.ToString();

                    Label nameLabel = new Label();
                    nameLabel.Text = db.getProductNameById(wishlistedItems[i]);
                    nameLabel.Location = new Point(14,11);
                    nameLabel.Name = "namelbl"+i.ToString();

                    p.Controls.Add(nameLabel);

                    LinkLabel linkLabel1 = new LinkLabel();
                    linkLabel1.Text = "Peržiūrėti";
                    linkLabel1.Name = "OpenLl" + i.ToString();
                    linkLabel1.Location = new Point(449, 11);
                    linkLabel1.Width = 80;
                    linkLabel1.Click += new EventHandler(ViewLll_Clicked);

                    p.Controls.Add(linkLabel1);

                    LinkLabel linkLabel2 = new LinkLabel();
                    linkLabel2.Text = "Panaikinti";
                    linkLabel2.Name = "OpenLl" + i.ToString();
                    linkLabel2.Location = new Point(530, 11);
                    linkLabel2.Click += new EventHandler(RemoveFromWL_Clicked);

                    p.Controls.Add(linkLabel2);

                    renderedItems.Add(p);

                    this.panel1.Controls.Add(p);
                    y += 50;

                }
            }
        }

        private void ViewLll_Clicked(object sender, EventArgs e)
        {
            LinkLabel ll = sender as LinkLabel;
            int pNum = Convert.ToInt32(ll.Name.Substring(ll.Name.Length - 1));

            Db db = new Db();

            Product p = db.GetProduct(wishlistedItems[pNum]);
            ShopItem si = new(p, curr);
            si.Show();
        }

        private void ClearWishList()
        {
            foreach(Panel p in renderedItems)
            {
                p.Controls.Clear();
                p.Dispose();
            }
        }

        private void RemoveFromWL_Clicked(object sender, EventArgs e)
        {
            LinkLabel ll = sender as LinkLabel;
            int pNum = Convert.ToInt32(ll.Name.Substring(ll.Name.Length - 1));
            Db db = new();
            db.RemoveFromWishList(curr.GetId(), wishlistedItems[pNum]);
            
            ClearWishList();

            wishlistedItems = curr.getWishedIds();
            if(wishlistedItems.Length > 0)
            renderWishList();
        }

        private void WishList_FormClosed(object sender, FormClosedEventArgs e)
        {
            Main m = new(curr);
            m.Show();
        }
    }
}
