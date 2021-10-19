namespace PU3
{
    public class User : Person
    {

        protected string Nick { get; set; }
        protected string Pwd { get; set; }
        protected int Group { get; set; }
        protected int Id { get; set; }

        public User(Person p, string nick, string pwd, int group)
        {
            Name = p.GetName();
            Surename = p.GetSurename();
            Dob = p.GetDob();
            Nick = nick;
            Pwd = pwd;
            Group = group;
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

        public int GetId()
        {
            Db DataBase = new();
            return DataBase.TryAuth(Nick, Pwd);
        }



    }
}
