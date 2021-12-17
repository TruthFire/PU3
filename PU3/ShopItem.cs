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
            if(u != null)
            {

                panel3.Visible = true;
                curr = u;
            }

            if(comments != null)
            commentPanels = RenderComments(comments);
           
        }

        
        private void ClearComments()
        {
            if (commentPanels != null)
            {
                foreach (Panel p in commentPanels)
                {
                    p.Dispose();
                }
            }
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
    }
}
