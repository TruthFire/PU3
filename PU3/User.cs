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

        private string GetAvatar()
        {
            if (Avatar == null)
            {
                Db db = new();
                Avatar = db.GetAvatar(Id);
            }
            return Avatar;
        }

        public bool IsAdmin()
        {
            return Group == 2;

        }


    }
}
