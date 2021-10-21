using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PU3
{
    public class Admin : User
    {
        public Admin(Person p, string nick, string pwd, int group) : base(p, nick, pwd, group) { }

        

    }
}
