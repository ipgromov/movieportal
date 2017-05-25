using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace movieapplication
{
    public class User
    {
        private string _username;

        public string Username
        {
            get { return _username; }
            set { _username = value; }
        }

        private string _name;

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        private string _surname;

        public string Surname
        {
            get { return _surname; }
            set { _surname = value; }
        }


        private string _password;

        public string Password
        {
            get { return _password; }
            set { _password = value; }
        }

        private bool _isAdmin;

        public bool IsAdmin
        {
            get { return _isAdmin; }
            set { _isAdmin = value; }
        }

        public User(string username, string name, string surname, string password)
        {
            _username = username;
            _name = name;
            _surname = surname;
            _password = CalculateHash(password);
            _isAdmin = false;
        }

        public User()
        {

        }

        private string CalculateHash(string password)
        {
            MD5 md5 = MD5.Create();
            var hash = md5.ComputeHash(Encoding.ASCII.GetBytes(password));
            return Convert.ToBase64String(hash);
        }

        public string Info
        {
            get
            {
                return $"Пользователь: {_username}; статус: {(_isAdmin ? "администратор" : "обычный пользователь")}.";
            }
        }
    }
}
