using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UserLoginSys
{
    public enum UserRoles
    {
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
        public string PasswordHash { get; private set; }
        public UserRoles UserRole { get; set; }
        public int LockOutCount { get; private set; }
        public string ExpirationDate { get; private set; }

        public User(string userName, string displayName, string passwordHash, UserRoles role, int lockoutCount, DateTime expiration)
        {
            UserName = userName;
            DisplayName = displayName;
            PasswordHash = passwordHash;
            UserRole = role;
            LockOutCount = lockoutCount;
            ExpirationDate = string.Format("{0:yyyy-MM-dd}", expiration);
        }

        public void ChangePassword(string newPassword)
        {
            string newPasswordHash = EncryptDecrypt.EncryptString(newPassword);
            PasswordHash = newPasswordHash;
            LockOutCount = 0;
            ExpirationDate = DateTime.Today.AddDays(30).ToString("yyyy-MM-dd");
        }

        public bool VerifyPassword(string tryPassword)
        {
            string tryPasswordHash = EncryptDecrypt.EncryptString(tryPassword);
            if (tryPasswordHash == PasswordHash)
            {
                LockOutCount = 0;
                return true;
            }
            else
            {
                LockOutCount++;
                return false;
            }
        }

        public void ResetLockoutCount()
        {
            LockOutCount = 0;
        }
    }
}
