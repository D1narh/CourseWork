using DataModels;
using MaimApp.Class.MainProductC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaimApp.Class.User
{
    public class AuthUser
    {
        public static int? UserId { get; set; }
        public static string Login { get; set; }
        public static string Password { get; set; }
        public static string Name { get; set; }
        public static string LastName { get; set; }
        public static string SurName { get; set; }
        public static int? UserRoleID { get; set; }
        public static string Email { get; set; }
        public static string Status { get; set; }

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
                using (var db = new DbA99dc4MaimfDB())
                {
                    var result = db.Users.FirstOrDefault(x => x.Login == Login && x.Password == Password);
                    int? idstatus = result?.Status;
                    var status = db.Status.FirstOrDefault(x => x.Id == idstatus);
                    if (result != null)
                    {
                        var PersonalData = db.UserPrData.FirstOrDefault(x => x.UserId == result.Id);
                        Name = PersonalData.Name;
                        LastName = PersonalData.LastName;
                        SurName = PersonalData.Surname;
                        Email = result.Mail;
                        UserRoleID = result.RoleId ?? 1;
                        UserId = result.Id;
                        Status = status.Name.ToString();
                        FavoriteProduct();
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
        }

        async void FavoriteProduct()
        {
            ViewProduct product = new ViewProduct();
            await product.GetAllFavorite();
        }

        public bool ExitAcc()
        {
            UserId = null;
            Status = null;
            Login= null;
            Password= null;
            Name= null;
            LastName= null;
            SurName= null;
            Email = null;
            Status= null;
            UserRoleID= null;

            return true;
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
        public string GetStatus()
        {
            return Status;
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
