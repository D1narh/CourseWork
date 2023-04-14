using DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaimApp.Class.User
{
    public class AuthUser
    {
        public static string Login { get; set; }
        public static string Password { get; set; }

        public AuthUser(string login, string password)
        {
            Login = login;
            Password = password;
        }
        public AuthUser() { }

        public bool AuthOrNo()
        {
            var result = App.Entity.Users.FirstOrDefault(x => x.Login == Login && x.Password == Password);
            if (result != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public string GetUserLogin()
        {
            return Login;
        }
    }
}
