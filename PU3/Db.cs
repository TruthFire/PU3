using System;
using MySql.Data.MySqlClient;
using System.Data;
using System.Collections.Generic;
using Newtonsoft.Json;

/* CREATE TABLE "User" (
	"id"	INTEGER NOT NULL,
	"nick"	TEXT NOT NULL,
	"password"	TEXT NOT NULL,
	"name"	TEXT NOT NULL,
	"surename"	TEXT NOT NULL,
	"dob"	TEXT NOT NULL,
	"avatar"	TEXT,
	"user_group"	INTEGER,
	PRIMARY KEY("id" AUTOINCREMENT)
)
*/


namespace PU3
{
    public class Db
    {
        MySqlConnection dbConnection = new(@"server=localhost;userid=root;password=;database=PU");
        public Db()
        {
   
        }

         public void CreateUser(User u)
         { 
            string sql = string.Format(
            "INSERT INTO `User`(`nick`, `password`, `name`, `surename`, `dob`, `user_group`, `avatar`) VALUES ('{0}', '{1}', '{2}', '{3}', '{4}', 1, 'NoAvatar');",
            u.GetNick(), u.GetPwd(), u.GetName(), u.GetSurename(), u.GetDob()
            );
            Exec(sql);
         }

        public bool CheckNick(string nick)
        {
            string sql = string.Format("SELECT `id` FROM `User` where `nick`='{0}'", nick);
            dbConnection.Open();
            MySqlCommand cmd = new(sql, dbConnection);
            bool rez = Convert.ToInt32(cmd.ExecuteScalar()) != 0;
            dbConnection.Close();
            return rez;

        }


        public int TryAuth(string name, string pwd)
        { 
            string sql = string.Format("SELECT `id` FROM `User` WHERE (`nick`='{0}' AND `password`='{1}')", name, pwd);
            dbConnection.Open();
            MySqlCommand cmd = new(sql, dbConnection);
            int s = Convert.ToInt32(cmd.ExecuteScalar());
            dbConnection.Close();
            return s;
        }

        public User GetUser(string nick, string pwd)
        {

            string sql = string.Format("SELECT * FROM `User` WHERE `id` = '{0}'", TryAuth(nick, pwd));
            dbConnection.Open();
            MySqlCommand cmd = new(sql, dbConnection);
            MySqlDataReader rdr = cmd.ExecuteReader();
            if (rdr.Read())
            {
                string name = rdr["name"].ToString();
                string surename = rdr["surename"].ToString();
                DateTime dob = Convert.ToDateTime(rdr["dob"]);
                int Group = Convert.ToInt32(rdr["user_group"]);
                string avtr = rdr["avatar"].ToString();
                Person p = new(name, surename, dob);
                User u = new(p, nick, pwd, Group);
                dbConnection.Close();
                return u;
            }
            else
            {
                dbConnection.Close();
                return null;
            }
        }

        public string GetNameById(int id)
        {
            string nick = "";
            string sql = string.Format("SELECT `nick` FROM `user` WHERE `id` = '{0}'", id);
            if (dbConnection.State != ConnectionState.Open)
            {
                dbConnection.Open();
                MySqlCommand cmd = new(sql, dbConnection);
                MySqlDataReader rdr = cmd.ExecuteReader();
                
                while (rdr.Read())
                {
                    nick = rdr["nick"].ToString();
                }
                dbConnection.Close();
            }
            else
            {
                MySqlCommand cmd = new(sql, dbConnection);
                MySqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    nick = rdr["nick"].ToString();
                }
            }
            return nick;
        }

        public void DeleteUser(int id)
        {
            string sql = String.Format("DELETE FROM `User` WHERE `id` = '{0}'", id);
            Exec(sql);
        }

        public void UpdateAvatar(int id, string fn)
        {
            string sql = String.Format("UPDATE `User` SET `avatar` = '{0}' WHERE `id` = {1}", fn, id);
            Exec(sql); 
            
        }

        protected void Exec(string sql)
        {
            dbConnection.Open();
            MySqlCommand cmd = new(sql, dbConnection);
            cmd.ExecuteNonQuery();
            dbConnection.Close();
        }

        public bool CheckPwd(int id, string pwd)
        {
            
            string sql = String.Format("SELECT `password` FROM `User` WHERE `id` = '{0}'", id);
            dbConnection.Open();
            MySqlCommand cmd = new(sql, dbConnection);
            MySqlDataReader rdr = cmd.ExecuteReader();
            string pass = "";
            while (rdr.Read())
            {
                   pass = rdr["password"].ToString();
            }
            dbConnection.Close();
            return pass == pwd;
        }

        public void SetPwd(string newPwd, int id)
        {
            string sql = String.Format("UPDATE `User` SET `password` = {0} WHERE id={1}", newPwd, id);
            dbConnection.Open();
            MySqlCommand cmd = new(sql, dbConnection);
            cmd.ExecuteNonQuery();
            dbConnection.Close();
        }
        
        public string GetAvatar(int id)
        {
            string sql = string.Format("SELECT `avatar` FROM `User` where `id` = '{0}'", id);
            dbConnection.Open();
            MySqlCommand cmd = new(sql, dbConnection);
            MySqlDataReader rdr = cmd.ExecuteReader();
            string avtr = "NoAvatar";
            while (rdr.Read())
            {
                avtr = rdr["avatar"].ToString();
            }
            dbConnection.Close();
            return avtr;
        }

        public DataTable FillUserGridView()
        {
            dbConnection.Open();
            MySqlDataAdapter Da = new();
            string sql = "SELECT `id`, `nick`, `name`, `surename` FROM `User`";
            Da.SelectCommand = new MySqlCommand(sql, dbConnection);
            DataTable dt = new();
            Da.Fill(dt);
            dbConnection.Close();
            return dt;
        }

        public DataTable FillCategoryGridView()
        {
            dbConnection.Open();
            MySqlDataAdapter Da = new();
            string sql = "SELECT `id`, `name` FROM `category`";
            Da.SelectCommand = new MySqlCommand(sql, dbConnection);
            DataTable dt = new();
            Da.Fill(dt);
            dbConnection.Close();
            return dt;
        }

        public DataTable FillProductGridView()
        {
            dbConnection.Open();
            MySqlDataAdapter Da = new();
            string sql = "SELECT `id`, `name`, `price`, `category` FROM `products`";
            Da.SelectCommand = new MySqlCommand(sql, dbConnection);
            DataTable dt = new();
            Da.Fill(dt);
            dbConnection.Close();
            return dt;
        }

        public DataTable FillLogsGridView()
        {
            dbConnection.Open();
            MySqlDataAdapter Da = new();
            string sql = "SELECT user.nick, products.name, logs.date, logs.action FROM `user` " +
                "RIGHT JOIN logs ON logs.user_id = user.id LEFT JOIN productsON logs.product_id " +
                "= products.id";
            Da.SelectCommand = new MySqlCommand(sql, dbConnection);
            DataTable dt = new();
            Da.Fill(dt);
            dbConnection.Close();
            return dt;
        }
        //SHOP

        public string[] getCategories()
        {
            List<string> categories = new List<string>();
            string sql = "SELECT `name` FROM `category` WHERE 1";
            dbConnection.Open();
            MySqlCommand cmd = new(sql, dbConnection);
            MySqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                categories.Add(rdr["name"].ToString());
            }
            dbConnection.Close();

            return categories.ToArray();
        }

        public Product[] GetProducts(int category)
        {
            List<Product> products = new List<Product>();
            string sql = String.Format("SELECT `id`,`name`,`img`, `price`, `description` FROM `products` WHERE `category` = {0}", category);
            dbConnection.Open();
            MySqlCommand cmd = new(sql, dbConnection);
            MySqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                products.Add(new((int)rdr["id"],rdr["name"].ToString(), rdr["img"].ToString(), (int)rdr["price"], rdr["description"].ToString()));
            }
            dbConnection.Close();

            return products.ToArray();
        }

        public Product GetProduct(int Id)
        {
            Product p = null;

            string sql = String.Format("SELECT `id`,`name`,`img`, `price`, `description` FROM `products` WHERE `id` = {0}", Id);
            dbConnection.Open();
            MySqlCommand cmd = new(sql, dbConnection);
            MySqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
               p  = new((int)rdr["id"], rdr["name"].ToString(), rdr["img"].ToString(), (int)rdr["price"], rdr["description"].ToString());
            }
            dbConnection.Close();

            return p;
        }


        public void RemoveProduct(int pId)
        {
            string sql = string.Format("DELETE FROM `products` WHERE `id` = '{0}'", pId);
            Exec(sql);
        }
        public int GetProductAmount(int category)
        {
            string sql = string.Format("SELECT COUNT(`id`) AS amount FROM `products` where `category` = '{0}' ", category);
            dbConnection.Open();
            MySqlCommand cmd = new(sql, dbConnection);
            MySqlDataReader rdr = cmd.ExecuteReader();
            int amount = 0;
            while (rdr.Read())
            {
                amount = (int)rdr["amount"];
            }
            dbConnection.Close();
            return amount;
        }

        public int getCategoryId(string cat_name)
        {
            int categoryId = 0;
            string sql = string.Format("SELECT `id` FROM `category` WHERE `name` = '{0}'", cat_name);
            dbConnection.Open();
            MySqlCommand cmd = new(sql, dbConnection);
            MySqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                categoryId = (int)rdr["id"];
            }
            dbConnection.Close();

            return categoryId;
        }

        public void addProduct(string name, string category, int price, string img, string description)
        {
            string sql = string.Format("INSERT INTO `products`(`name`, `price`, `category`, `img`, `description`) VALUES ('{0}','{1}','{2}','{3}','{4}')",name, price, getCategoryId(category), img, description);
            Exec(sql);
        }
        public int GetCommentAmount(int product)
        {
            string sql = string.Format("SELECT COUNT(`id`) AS amount FROM `comments` where `product_id` = '{0}' ", product);
            dbConnection.Open();
            MySqlCommand cmd = new(sql, dbConnection);
            MySqlDataReader rdr = cmd.ExecuteReader();
            int amount = 0;
            while (rdr.Read())
            {
                amount = Convert.ToInt32(rdr["amount"]);
            }
            dbConnection.Close();
            return amount;
        }

        public void AddComment(int posterId,int productId, string text, DateTime d)
        {
            string sql = string.Format("INSERT INTO `comments`(`author_id`, `product_id`, `comment`, `c_date`) VALUES ('{0}','{1}','{2}', '{3}')", posterId, productId, text, d.ToString("dd-MM-yyyy. HH:mm"));
            Exec(sql);
        }

        public void deleteComment(int commId)
        {
            string sql = string.Format("DELETE FROM `comments` WHERE `id` = '{0}'", commId);
            Exec(sql);
        }

        public void AddToWishList(int uId, int pId)
        {
            string sql = string.Format("INSERT INTO `wishlist`(`user_id`,`product_id`) VALUES('{0}','{1}')", uId, pId);
            Exec(sql);
        }
        
        public void RemoveFromWishList(int uId, int pId)
        {
            string sql = string.Format("DELETE FROM `wishlist` WHERE `user_id` = '{0}' AND `product_id` = '{1}'", uId, pId);
            Exec(sql);
        }

        public Comment[] getComments(int prodId)
        {
            List<Comment> comments = new List<Comment>();
            if (GetCommentAmount(prodId) > 0)
            {
                string sql = string.Format("SELECT `id`, `author_id`,`comment`, `c_date` FROM `comments` WHERE `product_id` = '{0}'", prodId);

                dbConnection.Open();
                MySqlCommand cmd = new(sql, dbConnection);
                MySqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    comments.Add(new((int)rdr["id"], rdr["comment"].ToString(), (int)rdr["author_id"], rdr["c_date"].ToString()));
                }
                rdr.Close();
                dbConnection.Close();
                return comments.ToArray();
            }
            return null;
        }

        public int[] getWishedIds(int uId)
        {
            List<int> ids = new List<int>();

            string sql = string.Format("SELECT `product_id` FROM `wishlist` WHERE `user_id` = '{0}'", uId);
            dbConnection.Open();
            MySqlCommand cmd = new(sql, dbConnection);
            MySqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                ids.Add((int)rdr["product_id"]);
            }
            rdr.Close();
            dbConnection.Close();
            return ids.ToArray();

        }

        public string getProductNameById(int pId)
        {
            string sql = string.Format("SELECT `name` FROM `products` WHERE `id`='{0}'", pId);
            dbConnection.Open();
            MySqlCommand cmd = new(sql, dbConnection); 
            MySqlDataReader rdr = cmd.ExecuteReader();
            string prodName = "";
            while (rdr.Read())
            {
                prodName = rdr["name"].ToString();
            }
            rdr.Close();
            dbConnection.Close();

            return prodName;
        }

        public void DeleteCategory(int cId)
        {

            Product[] prods = GetProducts(cId);

            foreach (Product p in prods)
            {
                RemoveProduct(p.getId());
            }

            string sql = string.Format("DELETE FROM `category` WHERE `id` = '{0}'", cId);
            Exec(sql);
        }


        public void AddCategory(string name)
        {
            string sql = string.Format("INSERT INTO `category`(`name`) VALUES ('{0}')", name);
            Exec(sql);
        }

        public void addLog(Log l)
        {
            string sql = string.Format("INSERT INTO `logs`(`user_id`, `product_id`, `action`, `date`)" +
                " VALUES ('{0}','{1}','{2}','{3}')", l.getUId(), l.getPId(), l.getAction(), l.getDateString());
            Exec(sql);
        }

        public void addToCart(User u, Product p)
        {
            string sql = string.Format("INSERT INTO `cart`(`user_id`,`product_id`) VALUES('{0}','{1}')", u.GetId(), p.getId());
            Exec(sql);
        }

        public Product[] getUserCart(int uId)
        {
            List<int> cartIds = new();
            string sql = string.Format("SELECT `product_id` FROM `cart` WHERE `user_id` = {0}", uId);
            dbConnection.Open();
            MySqlCommand cmd = new(sql, dbConnection);
            MySqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                cartIds.Add(Convert.ToInt32(rdr["product_id"]));
            }
            dbConnection.Close();
            List<Product> cart = new();
            foreach(int id in cartIds)
            {
                cart.Add(GetProduct(id));
            }
            return cart.ToArray();
        }

        public void payForCart(User u, Order o)
        {
            string sql = string.Format("DELETE FROM `cart` WHERE `user_id` = {0}", u.GetId());
            Exec(sql);

            o.isPayed = true;
            o.id = getMaxId();
            DateTime currDateTime = DateTime.Now;
            string sqlFormattedDate = currDateTime.ToString("yyyy-MM-dd HH:mm:ss");
            o.o_date = sqlFormattedDate;
            string json = JsonConvert.SerializeObject(o);

            sqlFormattedDate = currDateTime.ToString("yyyy-MM-dd HH:mm:ss.fff");

            sql = string.Format("INSERT INTO `orders`(`content`, `user_id`, `order_dt`) VALUES ('{0}','{1}','{2}')",json, u.GetId(), sqlFormattedDate);
            Exec(sql);
            u.ClearCart();
        }

        public string GetOrderAmount()
        {
            string sql = string.Format("SELECT COUNT(*) AS amount FROM `orders` WHERE 1");
            dbConnection.Open();
            MySqlCommand cmd = new(sql, dbConnection);
            MySqlDataReader rdr = cmd.ExecuteReader();
            string rez = "";
            while(rdr.Read())
            {
                rez = rdr["amount"].ToString();
            }
            rdr.Close();
            dbConnection.Close();
            return rez;
        }

        public Order[] GetOrderList()
        {
            string sql = string.Format("SELECT `content` FROM `orders` WHERE 1");
            dbConnection.Open();
            MySqlCommand cmd = new(sql, dbConnection);
            MySqlDataReader rdr = cmd.ExecuteReader();
            List<Order> orders = new List<Order>();
            Order tmp;
            while (rdr.Read()) {
                tmp = JsonConvert.DeserializeObject<Order>(rdr["content"].ToString());
                //tmp.id = Convert.ToInt32(rdr["id"]);
                orders.Add(tmp);
            }

            rdr.Close();
            dbConnection.Close();

            return orders.ToArray();    
        }

        public int getMaxId()
        {
            string sql = string.Format("SELECT MAX(id) AS max FROM `orders` WHERE 1");
            dbConnection.Open();
            MySqlCommand cmd = new(sql, dbConnection);
            MySqlDataReader rdr = cmd.ExecuteReader();
            int rez = 0;
            while (rdr.Read())
            {
                rez = (int)rdr["max"];
            }
            rdr.Close();
            dbConnection.Close();
            return rez+1;
        }

        public void RemoveItemFromCart(int p_id, int u_id)
        {
            string sql = string.Format("DELETE FROM `cart` WHERE `product_id` = {0} AND `user_id` = {1}", p_id, u_id);
            Exec(sql);
        }

        public Order[] getUserOders(int u_id)
        {
            string sql = string.Format("SELECT `content` FROM `orders` WHERE `user_id` = {0}", u_id);
            dbConnection.Open();
            MySqlCommand cmd = new(sql, dbConnection);
            MySqlDataReader rdr = cmd.ExecuteReader();
            List<Order> orders = new List<Order>();
            Order tmp;
            while (rdr.Read())
            {
                tmp = JsonConvert.DeserializeObject<Order>(rdr["content"].ToString());
                orders.Add(tmp);
            }

            rdr.Close();
            dbConnection.Close();

            return orders.ToArray();

        }
    }
}
