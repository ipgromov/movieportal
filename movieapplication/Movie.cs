using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace movieapplication
{
    public class Movie
    {
        private string _name;

        public string name
        {
            get { return _name; }
            set { _name = value; }
        }

        private int _year;

        public int year
        {
            get { return _year; }
            set { _year = value; }
        }

        private string _country;

        public string country
        {
            get { return _country; }
            set { _country = value; }
        }

        private string _genre;

        public string genre
        {
            get { return _genre; }
            set { _genre = value; }
        }

        public Movie()
        {

        }

        public Movie(string name, int year, string country, string genre)
        {
            _name = name;
            _year = year;
            _country = country;
            _genre = genre;
        }

        public string Info
        {
            get
            {
                return $"{_name} ({_year}), {_country}. Жанр: {_genre}.";
            }
        }
    }
}
