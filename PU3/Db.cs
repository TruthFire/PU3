using System;
using MySql.Data.MySqlClient;
using System.Data;
using System.Collections.Generic;

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

        public DataTable FillGridView()
        {
            dbConnection.Open();
            MySqlDataAdapter Da = new();
            string sql = "SELECT `id`, `nick`, `name`, `surename` FROM `User`";
            Da.SelectCommand = new MySqlCommand(sql, dbConnection);
            DataTable dt = new();
            Da.Fill(dt);

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
            string sql = String.Format("SELECT `id`,`name`,`img` FROM `products` WHERE `category` = {0}", category);
            dbConnection.Open();
            MySqlCommand cmd = new(sql, dbConnection);
            MySqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                products.Add(new((int)rdr["id"],rdr["name"].ToString(), rdr["img"].ToString()));
            }
            dbConnection.Close();

            return products.ToArray();
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
    }
}
