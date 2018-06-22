using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace UserLoginSys
{
    /// <summary>
    /// EditAccountWindow.xaml 的交互逻辑
    /// </summary>
    public partial class EditAccountWindow : Window
    {
        public EditAccountWindow()
        {
            InitializeComponent();
        }

        private void _ChangeBtn_Click(object sender, RoutedEventArgs e)
        {
            if (_NewPwdBox.Password == string.Empty || _NewPwdBox.Password != _NewPwdConfirmBox.Password)
            {
                MessageBox.Show("Please ensure the new password!");
                return;
            }
            Workspace.This.SelectedUser.ChangePassword(_NewPwdBox.Password);
            AccountsManagement.SaveToFile(Workspace.This.Users, Workspace.This.UserFilePath);

            ObservableCollection<User> users = Workspace.This.Users;
            User selectedUser = Workspace.This.SelectedUser;
            Workspace.This.Users = null;
            Workspace.This.Users = users;
            Workspace.This.SelectedUser = selectedUser;
        }

        private void _ResetLockoutBtn_Click(object sender, RoutedEventArgs e)
        {
            Workspace.This.SelectedUser.ResetLockoutCount();
            AccountsManagement.SaveToFile(Workspace.This.Users, Workspace.This.UserFilePath);

            ObservableCollection<User> users = Workspace.This.Users;
            User selectedUser = Workspace.This.SelectedUser;
            Workspace.This.Users = null;
            Workspace.This.Users = users;
            Workspace.This.SelectedUser = selectedUser;
        }

        private void _ResetPwdBtn_Click(object sender, RoutedEventArgs e)
        {
            Workspace.This.SelectedUser.ChangePassword("admin");
            AccountsManagement.SaveToFile(Workspace.This.Users, Workspace.This.UserFilePath);

            ObservableCollection<User> users = Workspace.This.Users;
            User selectedUser = Workspace.This.SelectedUser;
            Workspace.This.Users = null;
            Workspace.This.Users = users;
            Workspace.This.SelectedUser = selectedUser;
        }
    }
}
