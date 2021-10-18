namespace PU3
{
    public class User
    {
        public Person Person { get; set; }
        protected string Nick { get; set; }
        protected string Pwd { get; set; }
        protected int Group { get; set; }

        public User(Person p, string nick, string pwd, int group)
        {
            Person = p;
            Nick = nick;
            Pwd = pwd;
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



    }
}
