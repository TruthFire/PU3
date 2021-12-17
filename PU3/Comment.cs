using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PU3
{
    public class Comment
    {
        int Id;
        string CommentText;
        string Author;
        string posted_date;
        
        public Comment(int id, string comm_text, int author_id, string p_date)
        {
            Id = id;
            CommentText = comm_text;
            Db db = new Db();
            Author = db.GetNameById(author_id);
            posted_date = p_date;

        }

        public int getId()
        {
            return Id;
        }

        public string getCommText()
        {
            return CommentText;
        }

        public string GetAuthor()
        {
            return Author;
        }

        public string getPDate()
        {
            return posted_date;
        }
    }
}
