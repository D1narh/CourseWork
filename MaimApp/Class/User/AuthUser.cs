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
        public static int UserId { get; set; }
        public static string Login { get; set; }
        public static string Password { get; set; }
        public static string Name { get; set; }
        public static string LastName { get; set; }
        public static string SurName { get; set; }
        public static int UserRoleID { get; set; }
        public static string Email { get; set; }

        public AuthUser(string login, string password)
        {
            Login = login;
            Password = password;
        }
        public AuthUser() { }

        public bool AuthOrNo()
        {
            if(Email != null)
            {
                return true;
            }
            else
            {
                using (var db = new DbA96b40MaimfDB())
                {
                    var result = db.Users.FirstOrDefault(x => x.Login == Login && x.Password == Password);
                    if (result != null)
                    {
                        var PersonalData = db.UserPrData.FirstOrDefault(x => x.UserId == result.Id);
                        Name = PersonalData.Name;
                        LastName = PersonalData.LastName;
                        SurName = PersonalData.Surname;
                        Email = result.Mail;
                        UserRoleID = result.RoleId ?? 1;
                        UserId = result.Id;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
        }

        public int? GetUserRole()
        {
            return UserRoleID;
        }
        public int? GetUserId()
        {
            return UserId;
        }

        public string GetEmail()
        {
            return Email;
        }

        public string GetUserLogin()
        {
            return Login;
        }

        public string UserFIO()
        {
            return LastName + " " + SurName + " " + Name;
        }
    }
}
