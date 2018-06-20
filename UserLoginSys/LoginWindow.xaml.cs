using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Xml.Linq;
using System.Xml.XPath;
//using System.Windows.Shapes;

namespace UserLoginSys
{
    /// <summary>
    /// LoginWindow.xaml 的交互逻辑
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void _LoginBtn_Click(object sender, RoutedEventArgs e)
        {
            if (_NameBox.Text == null || _NameBox.Text == "")
            {
                MessageBox.Show("Please input user name!");
                return;
            }
            if (_PwdBox.Password == null || _PwdBox.Password == "")
            {
                MessageBox.Show("Please input password");
                return;
            }

            string enteredPasswordHash = EncryptDecrypt.EncryptString(_PwdBox.Password);
            User user;
            int i = 0;
            for (; i < Workspace.This.Users.Count; i++)
            {
                user = Workspace.This.Users[i];
                if (user.UserName == _NameBox.Text && user.PasswordHash == enteredPasswordHash)
                {
                    break;
                }
            }
            if (i >= Workspace.This.Users.Count)
            {
                MessageBox.Show("Wrong user name or password");
                return;
            }
            Workspace.This.LoginUser = Workspace.This.Users[i];
            this.DialogResult = true;
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            string userFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Users.xml");
            if (!File.Exists(userFilePath))
            {
                MessageBox.Show("Error finding users information file");
                return;
            }
            Workspace.This.Users = AccountsManagement.LoadFromFile(userFilePath);
            var test = AccountsManagement.DeleteAccount(Workspace.This.Users, Workspace.This.Users[2]);
            AccountsManagement.SaveToFile(Workspace.This.Users, Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "haha.xml"));
        }

        private void _PwdBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                _LoginBtn_Click(sender, e);
            }
        }
    }
}
