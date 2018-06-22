using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Xml.XPath;

namespace UserLoginSys
{
    static class AccountsManagement
    {
        public static ObservableCollection<User> LoadFromFile(string filePath)
        {
            if (!File.Exists(filePath))
            {
                return null;
            }
            try
            {
                ObservableCollection<User> users = new ObservableCollection<User>();
                XElement elements = XElement.Load(filePath);
                XNode node = elements.FirstNode;
                string userName, displayName, passwordHash;
                int lockoutCount;
                UserRoles role;
                DateTime expiration;
                while (node != null)
                {
                    XElement subElement = node.XPathSelectElement("UserName");
                    userName = subElement.Value;
                    subElement = node.XPathSelectElement("DisplayName");
                    displayName = subElement.Value;
                    subElement = node.XPathSelectElement("PasswordHash");
                    passwordHash = subElement.Value;
                    subElement = node.XPathSelectElement("UserRole");
                    if (subElement.Value == "SuperAdmin")
                    {
                        role = UserRoles.SuperAdmin;
                    }
                    else if (subElement.Value == "Admin")
                    {
                        role = UserRoles.Admin;
                    }
                    else if (subElement.Value == "Manager")
                    {
                        role = UserRoles.Manager;
                    }
                    else if (subElement.Value == "Standard")
                    {
                        role = UserRoles.Standard;
                    }
                    else
                    {
                        role = UserRoles.Standard;
                    }
                    subElement = node.XPathSelectElement("LockoutCount");
                    lockoutCount = Convert.ToInt32(subElement.Value);
                    subElement = node.XPathSelectElement("ExpirationDate");
                    expiration = Convert.ToDateTime(subElement.Value);

                    User user = new User(userName, displayName, passwordHash, role, lockoutCount, expiration);
                    users.Add(user);
                    node = node.NextNode;
                }
                return users;
            }
            catch
            {
                return null;
            }
        }
        public static bool SaveToFile(ObservableCollection<User> users, string filePath)
        {
            if (users == null || filePath == null)
            {
                return false;
            }
            try
            {
                XElement root = new XElement(
                    new XElement("UserAccounts"));
                for (int i = 0; i < users.Count; i++)
                {
                    XElement account = new XElement("UserAccount",
                        new XElement("UserName", users[i].UserName),
                        new XElement("DisplayName", users[i].DisplayName),
                        new XElement("PasswordHash", users[i].PasswordHash),
                        new XElement("UserRole", users[i].UserRole),
                        new XElement("LockoutCount", users[i].LockOutCount.ToString()),
                        new XElement("ExpirationDate", string.Format("{0:yyyy-MM-dd}", users[i].ExpirationDate))
                        );
                    root.Add(account);
                }
                root.Save(filePath);
                return true;
            }
            catch
            {
                return false;
            }
        }

    }
}
