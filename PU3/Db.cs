using System;
using System.Data.SQLite;
using System.IO;

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
        readonly SQLiteConnection dbConnection = new SQLiteConnection("Data Source=pu.db");
        public Db()
        {
            if (!File.Exists("pu.db"))
            {
                SQLiteConnection.CreateFile("pu.db");
            }
           
            
            string sql = "CREATE TABLE IF NOT EXISTS \"User\" (" +
                "\"id\"	INTEGER NOT NULL, \"nick\"	TEXT NOT NULL, \"password\"	TEXT NOT NULL," +
                "\"name\"	TEXT NOT NULL, \"surename\"	TEXT NOT NULL, \"dob\" TEXT NOT NULL, \"avatar\"   TEXT," +
                "\"user_group\"  INTEGER, PRIMARY KEY(\"id\" AUTOINCREMENT))";
            Exec(sql);
        }

        public bool CreateUser(User u)
        {

            if (CheckName(u.GetName()) == 0)
                return false;
            else
            {
                string sql = string.Format(
                    "INSERT INTO User(nick, password, name, surename, dob, user_group) VALUES ('{0}', '{1}', '{2}', '{3}', '{4}', 1);",
                u.GetNick(), u.GetPwd(), u.GetName(), u.GetSurename(), u.GetDob()
                );
                Exec(sql);
                return true;
            }



        }

        private int CheckName(string name)
        {
            string sql = string.Format("SELECT id FROM User where nick={0}", name);
            dbConnection.Open();
            SQLiteCommand cmd = new(sql, dbConnection);
            int s = Convert.ToInt32(cmd.ExecuteScalar());
            dbConnection.Close();
            return s;

        }


        public int TryAuth(string name, string pwd)
        { 
            string sql = string.Format("SELECT id FROM User WHERE (nick='{0}' AND password='{1}')", name, pwd);
            dbConnection.Open();
            SQLiteCommand cmd = new SQLiteCommand(sql, dbConnection);
            int s = Convert.ToInt32(cmd.ExecuteScalar());
            dbConnection.Close();
            return s;
        }

        public User GetUser(string nick, string pwd)
        {

            string sql = string.Format("SELECT * FROM User WHERE id = {0}", TryAuth(nick, pwd));
            dbConnection.Open();
            SQLiteCommand cmd = new(sql, dbConnection);
            SQLiteDataReader rdr = cmd.ExecuteReader();
            if (rdr.Read())
            {
                string name = rdr["name"].ToString();
                string surename = rdr["surename"].ToString();
                DateTime dob = Convert.ToDateTime(rdr["dob"]);
                int Group = Convert.ToInt32(rdr["user_group"]);
                Person p = new(name, surename, dob);
                User u = new(p,nick, pwd, Group);
                p = u;
                dbConnection.Close();
                return u;
            }
            else
            {
                dbConnection.Close();
                return null;
            }
        }

        public void DeleteUser(string username)
        {
            string sql = String.Format("DELETE FROM User WHERE nick={0}", username);
            Exec(sql);
        }

        public void UpdateImg(int id, string fn)
        {
            string sql = String.Format("UPDATE User set avatar={0} WHERE id={1}", fn, id);
            Exec(sql);
            
        }

        protected void Exec(string sql)
        {
            dbConnection.Open();
            SQLiteCommand cmd = new(sql, dbConnection);
            cmd.ExecuteNonQuery();
            dbConnection.Close();
        }

        public bool CheckPwd(int id, string pwd)
        {
            
            string sql = String.Format("SELECT password FROM User WHERE id={0}", id);
            dbConnection.Open();
            SQLiteCommand cmd = new(sql, dbConnection);
            SQLiteDataReader rdr = cmd.ExecuteReader();
            string pass = rdr["password"].ToString();
            dbConnection.Close();
            return pass == pwd;
        }

        public void SetPwd(string newPwd, int id)
        {
            string sql = String.Format("UPDATE User SET password={0} WHERE id={1}", newPwd, id);
            Exec(sql);

        } 

    }
}
