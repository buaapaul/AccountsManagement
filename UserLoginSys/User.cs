using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UserLoginSys
{
    public enum UserRoles
    {
        Default = -1,
        SuperAdmin = 0,
        Admin = 1,
        Manager = 2,
        Standard = 3,
    }
    public class User
    {
        //public int Position { get; set; }
        public string UserName { get; set; }
        public string DisplayName { get; set; }
        public string PasswordHash { get; set; }
        public UserRoles UserRole { get; set; }
        public bool IsLockOut { get; set; }
        public DateTime ExpirationDate { get; set; }

        public User()
        {
            UserName = "Dummy";
            DisplayName = "Dummy";
            PasswordHash = "";
            UserRole = UserRoles.Default;
            IsLockOut = false;
            ExpirationDate = DateTime.Today.AddDays(30);
        }

    }
}
