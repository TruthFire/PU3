using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PU3
{
    public class User
    {
        public Person Person { get; set; }
        protected string Nick { get; set; }
        protected string Pwd { get; set; }

        public User(Person p, string nick, string pwd)
        {
            this.Person = p;
            this.Nick = nick;
            this.Pwd = pwd;
        }

        public string GetNick()
        {
            return this.Nick;
        }  
        public string GetPwd()
        {
            return this.Pwd;
        }
        
       

    }
}
