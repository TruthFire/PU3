using System;

namespace PU3
{
    public class Log
    {

        protected DateTime _date;
        protected int _uId;
        protected int _pId;
        protected string _action = "none";

        public Log(int uId, int pId, string action = "none")
        {
            _date = DateTime.Now;
            _uId = uId;
            _pId = pId;
            _action = action;
        }

        public string getDateString()
        {
            return _date.ToString();
        }

        public int getUId()
        {
            return _uId;
        }

        public int getPId()
        {
            return _pId;
        }

        public string getAction()
        {
            return _action;
        }

        public void UpdateAction(string action)
        {
            _action = action;
        }
    }
}
