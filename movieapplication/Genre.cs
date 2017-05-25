using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace movieapplication
{
    public class Genre
    {
        private int _id;

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        private string _name;

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }


        public Genre(string name)
        {
            _id = IdFormer();
            _name = name;
        }

        public Genre(int id, string name)
        {
            _id = id;
            _name = name;
        }

        public Genre()
        {

        }

        private int IdFormer()
        {
            int maxId = 0;
            foreach (Genre genre in Data.Genres)
            {
                if (genre.Id > maxId)
                {
                    maxId = genre.Id;
                }
            }
            return maxId + 1;
        }
    }
}
