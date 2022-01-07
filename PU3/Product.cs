namespace PU3
{
    public class Product
    {
        public int id { get; set; }
        public string name { get; set; }
        string Description { get; set; }

        string img { get; set; }
        public int Price { get; set; }

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
