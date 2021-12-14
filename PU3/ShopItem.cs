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
        public ShopItem(string pName)
        {
            InitializeComponent();
            label5.Text = pName;
           
        }
    }
}
