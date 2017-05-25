using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace movieapplication
{
    public class Movie
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

        private int _year;

        public int Year
        {
            get { return _year; }
            set { _year = value; }
        }

        private string _country;

        public string Country
        {
            get { return _country; }
            set { _country = value; }
        }

        private int _genreId;

        public int GenreId
        {
            get { return _genreId; }
            set { _genreId = value; }
        }

        public Movie()
        {

        }

        public Movie(string name, int year, string country, int genreId)
        {
            _id = IdFormer();
            _name = name;
            _year = year;
            _country = country;
            _genreId = genreId;
        }

        public Movie(int id, string name, int year, string country, int genreId)
        {
            _id = id;
            _name = name;
            _year = year;
            _country = country;
            _genreId = genreId;
        }

        public string Info
        {
            get
            {
                return $"{_name} ({_year}), {_country}. Жанр: {Genre}.";
            }
        }

        public string Genre
        {
            get
            {
                foreach(Genre genre in Data.Genres)
                {
                    if (genre.Id == _genreId)
                        return genre.Name;
                }

                return "Ошибка";
            }
        }

        private int IdFormer()
        {
            int maxId = 0;
            foreach (Movie movie in Data.Movies)
            {
                if (movie.Id > maxId)
                {
                    maxId = movie.Id;
                }
            }
            return maxId+1;
        }
    }
}
