using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
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
                while (node != null)
                {
                    User user = new User();
                    XElement subElement = node.XPathSelectElement("UserName");
                    user.UserName = subElement.Value;
                    subElement = node.XPathSelectElement("DisplayName");
                    user.DisplayName = subElement.Value;
                    subElement = node.XPathSelectElement("PasswordHash");
                    user.PasswordHash = subElement.Value;
                    subElement = node.XPathSelectElement("UserRole");
                    if (subElement.Value == "SuperAdmin")
                    {
                        user.UserRole = UserRoles.SuperAdmin;
                    }
                    else if (subElement.Value == "Admin")
                    {
                        user.UserRole = UserRoles.Admin;
                    }
                    else if (subElement.Value == "Manager")
                    {
                        user.UserRole = UserRoles.Manager;
                    }
                    else if (subElement.Value == "Standard")
                    {
                        user.UserRole = UserRoles.Standard;
                    }
                    subElement = node.XPathSelectElement("IsLockOut");
                    user.IsLockOut = Convert.ToBoolean(subElement.Value);
                    subElement = node.XPathSelectElement("ExpirationDate");
                    if (subElement.Value != string.Empty)
                    {
                        user.ExpirationDate = Convert.ToDateTime(subElement.Value);
                    }

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
            if(users==null || filePath == null)
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
                        new XElement("IsLockOut", users[i].IsLockOut.ToString()),
                        new XElement("ExpirationDate", string.Format("{0:yyyy-MM-dd}", users[0].ExpirationDate))
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
        public static ObservableCollection<User> DeleteAccount(ObservableCollection<User> users, User userTobeDeleted)
        {
            ObservableCollection<User> newUsers = new ObservableCollection<User>();
            if (users.Contains(userTobeDeleted))
            {
                users.Remove(userTobeDeleted);
            }
            newUsers = users;
            return newUsers;
        }
        public static ObservableCollection<User> AddAccount(ObservableCollection<User> users, User userTobeAdded)
        {
            if (users == null)
            {
                return null;
            }
            users.Add(userTobeAdded);
            return users;
        }
    }
}
