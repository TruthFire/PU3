using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PU3
{
    public class Product
    {
        int id;
        string name;
        string description;
        int cat_id;
        string img;
        int Price;

        public Product(int Id, string Name, string Img, int price)
        {
            this.id = Id;
            this.name = Name;
            this.img = Img;
            this.Price = price;
        }

        public int getId()
        {
            return id;
        }

        public int getPrice()
        {
            return Price;
        }
        
        public string getName()
        {
            return name;
        }

        public string getImg()
        {
            return img;
        }

    }
}
