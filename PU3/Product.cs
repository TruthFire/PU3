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
        string Description;
        int cat_id;
        string img;
        int Price;

        public Product(int Id, string Name, string Img, int price, string description = "")
        {
            this.id = Id;
            this.name = Name;
            this.img = Img;
            this.Price = price;
            Description = description;
        }

        public int getId()
        {
            return id;
        }

        public string getDescription()
        {
            return Description;
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
