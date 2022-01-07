using System.Collections.Generic;
using System.Linq;
namespace PU3
{
    public class User : Person
    {

        protected string Nick { get; set; }
        protected string Pwd { get; set; }
        protected int Group { get; set; }
        protected int Id { get; set; }
        protected string Avatar = null;
       
        protected int[] WishListedIds { get; set; }

        protected List<Product> cart = new();

        protected double CartPrice = 0;

        public User(Person p, string nick, string pwd, int group)
        {
            Name = p.GetName();
            Surename = p.GetSurename();
            Dob = p.GetDob();
            Nick = nick;
            Pwd = pwd;
            Group = group;
            Id = GetId();
            Avatar = GetAvatar();
            LoadCart();
            CountCartPrice();
        }

        public void ClearCart()
        {
            cart.Clear();
        }

        public int GetCartLength()
        {
            return cart.Count();
        }

        public double GetCartPrice()
        {
            return CartPrice;
        }

        public string GetNick()
        {
            return Nick;
        }
        public string GetPwd()
        {
            return Pwd;
        }

        public int GetGroup()
        {
            return Group;
        }
        
        public int[] getWishedIds()
        {
            Db db = new Db();
            return db.getWishedIds(Id);
        }

        public Product[] getCart()
        {
            Db db = new();
            return db.getUserCart(Id);
        }

        public int GetId()
        {
            if (Id == 0)
            {
                Db DataBase = new();
                this.Id = DataBase.TryAuth(Nick, Pwd);
            }
            return this.Id;
        }

        public void SetAvatar(string avtr)
        {
            Avatar = avtr;
            Db db = new();
            db.UpdateAvatar(Id, avtr);

        }

        public bool IsAdmin()
        {
            return Group == 2;

        }

        public void AddToCart(Product p)
        {
            cart.Add(p);
            Db db = new();
            db.addToCart(this, p);
            CountCartPrice();
        }

        protected string GetAvatar()
        {
            if (Avatar == null)
            {
                Db db = new();
                Avatar = db.GetAvatar(Id);
            }
            return Avatar;
        }

        protected void LoadCart()
        {
            Db db = new();
            cart = db.getUserCart(Id).ToList();
        }

        protected void CountCartPrice()
        {
            CartPrice = 0;
            foreach (Product p in cart)
            {
                CartPrice += p.getPrice();
            }
        }

        public void RemoveFromCart(int p_id)
        {
            Db db = new();
            db.RemoveItemFromCart(p_id, Id);
            LoadCart();
        }

    }
}
