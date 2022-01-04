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
    public partial class ShopItem : Form
    {
        User curr;
        Product prod;
        List<Panel> commentPanels;
        Comment[] comments;
        Log prodLog;



        public ShopItem(Product p, User u = null)
        {
            prod = p;
            Db db = new Db();
            comments = db.getComments(p.getId());    
            InitializeComponent();
            if (comments != null)
            {
                label8.Text = "Komentarai:";
            }
            label5.Text = p.getName();
            label7.Text = p.getPrice() + " Eur.";
            pictureBox1.Load("https://i.imgur.com/" + p.getImg());
            textBox2.Text = p.getDescription();
            if(u != null)
            {
                prodLog = new(u.GetId(), p.getId());
                panel3.Visible = true;
                curr = u;
                int[] wishlisted = u.getWishedIds();
                if (wishlisted.Contains(p.getId()))
                {
                    button2.ForeColor = Color.Red;
                    button2.Text = "♥";
                }
            }

            if(comments != null)
            commentPanels = RenderComments(comments);

            
           
        }

        
        private void ClearComments()
        {
            if (commentPanels != null)
            {
                for (int i = 0; i < commentPanels.Count; i++)
                {
                    commentPanels[i].Controls.Clear();
                    commentPanels[i].Dispose();
                }
                foreach (Panel p in commentPanels)
                {

                    p.Dispose();
                }
                
            }

            commentPanels = null;
        }

        private List<Panel> RenderComments(Comment[] coms)
        {
            List<Panel> comPanels = new List<Panel>();
            int panY = 365;
            for (int i = 0; i < coms.Length; i++)
            {
                Panel pan = new Panel();
                pan.Name = "comPanel" + i;
                pan.BorderStyle = BorderStyle.FixedSingle;
                pan.Location = new Point(6, panY); //location
                pan.Size = new Size(642, 70);  //size
                pan.BackColor = Color.White;
                
                Label nameLabel = new Label();
                nameLabel.Name = "NameLabel" + i.ToString();
                nameLabel.Text = coms[i].GetAuthor() + " • " + coms[i].getPDate();
                nameLabel.Location = new Point(5,4);
                nameLabel.Width = 300;
                pan.Controls.Add(nameLabel);

                if (curr != null && curr.GetGroup() == 2)
                {
                    Button btn = new Button();
                    btn.Name = "deleteBtn+" + i.ToString();
                    btn.Text = "X";
                    btn.Size = new Size(20, 23);
                    btn.Location = new Point(617, 4);
                    btn.Click += new EventHandler(this.commentDelter_Click);

                    pan.Controls.Add(btn);

                }

                TextBox tb = new TextBox();
                tb.Name = "commTextBox" + i.ToString();
                tb.Text = coms[i].getCommText();
                tb.Location = new Point(5, 29);
                tb.Size = new Size(550, 50);
                tb.ReadOnly = true;
                tb.BackColor = Color.White;
                tb.BorderStyle = BorderStyle.None;
                
                pan.Controls.Add(tb);

                comPanels.Add(pan);



                panel2.Controls.Add(pan);
                panY += 85;
            } 
            return comPanels;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (curr != null) {
                Db db = new();
                db.AddComment(curr.GetId(), prod.getId(), textBox1.Text, DateTime.Now);

                ClearComments();
                this.comments = db.getComments(this.prod.getId());
                RenderComments(comments);
            }
        }

        private void commentDelter_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            int commNr = Convert.ToInt32(btn.Name.Substring(btn.Name.Length - 1));
            Db db = new();
            db.deleteComment(comments[commNr].getId());


            ClearComments();
            this.comments = db.getComments(this.prod.getId());
            if(comments != null)
            commentPanels = RenderComments(comments);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (curr != null)
            {
                Db db = new();
                if (button2.Text == "❤")
                {
                    
                    db.AddToWishList(curr.GetId(), prod.getId());
                    button2.ForeColor = Color.Red;
                    button2.Text = "♥";
                    prodLog.UpdateAction("Wishlisted");
                }
                else
                {
                    db.RemoveFromWishList(curr.GetId(), prod.getId());
                    button2.ForeColor = Color.Black;
                    button2.Text = "❤";
                    prodLog.UpdateAction("Removed from wishlist");
                }
            }
            else
            {
                MessageBox.Show("Tik registruoti vartotojai gali įtraukti elementus" +
                    " į įsimintinų sąrašą");
            }
        }

        private void ShopItem_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (prodLog != null)
            {
                Db db = new();
                db.addLog(prodLog);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            curr.AddToCart(prod);
            MessageBox.Show("Iš viso krepšelyje yra " + curr.GetCartLength().ToString() +
                "prekė(s). Bendra suma: "+ curr.GetCartPrice().ToString() + " Eur.");

        }
    }
}
