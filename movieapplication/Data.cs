using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Serialization;

namespace movieapplication
{
    static class Data
    {
        static private List<User> _users = new List<User>();

        static private List<Movie> _movies = new List<Movie>();

        static private List<Movie> _moviesSearch = new List<Movie>();

        static private bool _isSearched = false;

        static private User _loggedUser;

        static public List<User> Users
        {
            get { return _users; }
            set { _users = value; }
        }

        static public List<Movie> Movies
        {
            get { return _movies; }
            set { _movies = value; }
        }

        static public List<Movie> MoviesSearch
        {
            get { return _moviesSearch; }
            set { _moviesSearch = value; }
        }

        static public bool IsSearched
        {
            get { return _isSearched; }
            set { _isSearched = value; }
        }

        static public User LoggedUser
        {
            get { return _loggedUser; }
            set { _loggedUser = value; }
        }

        static public void UpdateUsersData()
        {
            using (FileStream fs = new FileStream("users.xml", FileMode.Create))
            {
                XmlSerializer xml = new XmlSerializer(typeof(List<User>));
                xml.Serialize(fs, _users);
            }
        }

        static public void ReadUsersData()
        {
            if (File.Exists("users.xml"))
            {
                using (FileStream fs = new FileStream("users.xml", FileMode.Open))
                {
                    XmlSerializer xml = new XmlSerializer(typeof(List<User>));
                    _users = (List<User>)xml.Deserialize(fs);
                }
            }
        }

        static public void UpdateMoviesData()
        {
            using (FileStream fs = new FileStream("movies.xml", FileMode.Create))
            {
                XmlSerializer xml = new XmlSerializer(typeof(List<Movie>));
                xml.Serialize(fs, _movies);
            }
        }

        static public void ReadMoviesData()
        {
            if (File.Exists("movies.xml"))
            {
                using (FileStream fs = new FileStream("movies.xml", FileMode.Open))
                {
                    XmlSerializer xml = new XmlSerializer(typeof(List<Movie>));
                    _movies = (List<Movie>)xml.Deserialize(fs);
                }
            }
        }

        static public bool checkUsernameValidity(string username)
        {
            return username.All(c => (c >= 'a' && c <= 'z') || (c >= 'A' && c <= 'Z') || c == '_' || char.IsDigit(c));
        }

        static public bool checkIfUsernameIsUsed(string username)
        {
            ReadUsersData();
            foreach (User user in Data.Users)
            {
                if (user.Username == username)
                    return false;
            }
            return true;
        }
    }
}
