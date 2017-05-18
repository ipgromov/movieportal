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
        private int _id;

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        private string _username;

        public string Username
        {
            get { return _username; }
            set { _username = value; }
        }

        private string _password;

        public string Password
        {
            get { return _password; }
            set { _password = value; }
        }

        public User(string username, string password)
        {
            _username = username;
            _password = CalculateHash(password);
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
    }
}
