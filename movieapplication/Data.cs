using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Serialization;
using System.Windows;

namespace movieapplication
{
    static class Data
    {
        static private List<User> _users = new List<User>();

        static private List<Movie> _movies = new List<Movie>();

        static private List<Genre> _genres = new List<Genre>();

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

        static public List<Genre> Genres
        {
            get { return _genres; }
            set { _genres = value; }
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
                try
                {
                    using (FileStream fs = new FileStream("users.xml", FileMode.Open))
                    {
                        XmlSerializer xml = new XmlSerializer(typeof(List<User>));
                        _users = (List<User>)xml.Deserialize(fs);
                    }
                }
                catch
                {
                    UpdateUsersData();
                    MessageBox.Show("Файл с информацией о пользователях повреждён. \nИнформация не была восстановлена.", "Ошибка!");
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
                try
                {
                    using (FileStream fs = new FileStream("movies.xml", FileMode.Open))
                    {
                        XmlSerializer xml = new XmlSerializer(typeof(List<Movie>));
                        _movies = (List<Movie>)xml.Deserialize(fs);
                    }
                }
                catch
                {
                    UpdateMoviesData();
                    MessageBox.Show("Файл с информацией о фильмах повреждён. \nИнформация не была восстановлена.", "Ошибка!");
                }
            }
        }

        static public void UpdateGenresData()
        {
            using (FileStream fs = new FileStream("genres.xml", FileMode.Create))
            {
                XmlSerializer xml = new XmlSerializer(typeof(List<Genre>));
                xml.Serialize(fs, _genres);
            }
        }

        static public void ReadGenresData()
        {
            if (File.Exists("genres.xml"))
            {
                try
                {
                    using (FileStream fs = new FileStream("genres.xml", FileMode.Open))
                    {
                        XmlSerializer xml = new XmlSerializer(typeof(List<Genre>));
                        _genres = (List<Genre>)xml.Deserialize(fs);
                    }
                }
                catch
                {
                    _genres = DefaultGenreList;
                    UpdateGenresData();
                    MessageBox.Show("Файл с информацией о жанрах повреждён. \nИнформация не была восстановлена.", "Ошибка!");
                }
            }
            else
            {
                _genres = DefaultGenreList;
                UpdateGenresData();
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

        static private List<Genre> DefaultGenreList
        {
            get
            {
                return new List<Genre>() {new Genre(0, "Другое"), new Genre(1, "Драма"), new Genre(2, "Комедия"), new Genre(3, "Триллер"),
                                             new Genre(4, "Мелодрама"), new Genre(5, "Ужасы"), new Genre(6, "Мюзикл"), new Genre(7,"Фантастика"),
                                             new Genre(8, "Мультипликация"),new Genre(9, "Докуметальный"), new Genre(10,"Биография"),
                                             new Genre(11,"Исторический")};
            }
        }
    }
}
