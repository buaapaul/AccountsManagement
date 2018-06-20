using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace UserLoginSys
{
    /// <summary>
    /// AccountsManagementControl.xaml 的交互逻辑
    /// </summary>
    public partial class AccountsManagementControl : UserControl
    {
        public AccountsManagementControl()
        {
            InitializeComponent();
        }

        private void _DeleteBtn_Click(object sender, RoutedEventArgs e)
        {
            if (Workspace.This.SelectedUser == null)
            {
                MessageBox.Show("Please select a user account first");
                return;
            }
            if (MessageBox.Show("Are you sure to delete " + Workspace.This.SelectedUser.UserName, "Delete user", MessageBoxButton.YesNo, MessageBoxImage.Exclamation) != MessageBoxResult.Yes)
            {
                return;
            }
            Workspace.This.Users = AccountsManagement.DeleteAccount(Workspace.This.Users, Workspace.This.SelectedUser);
            AccountsManagement.SaveToFile(Workspace.This.Users, AppDomain.CurrentDomain.BaseDirectory + "haha.xml");
        }
    }
}
