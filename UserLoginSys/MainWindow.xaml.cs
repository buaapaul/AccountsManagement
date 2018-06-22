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
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            AccountActivityLog log = new AccountActivityLog();
            log.LogActivity("paul_zhang", Activities.CreateAccount, "Create Lisa");
            InitializeComponent();
            DataContext = Workspace.This;
            Workspace.This.Owner = this;

            LoginWindow loginWindow = new LoginWindow();
            loginWindow.DataContext = Workspace.This;
            loginWindow.ShowDialog();
            if (loginWindow.DialogResult != true)
            {
                this.Close();
            }
        }

        private void _LogoutBtn_Click(object sender, RoutedEventArgs e)
        {
            Workspace.This.LoginUser = null;
            LoginWindow loginWindow = new LoginWindow();
            loginWindow.DataContext = Workspace.This;
            this.Visibility = Visibility.Collapsed;
            loginWindow.ShowDialog();
            if (loginWindow.DialogResult != true)
            {
                this.Close();
            }
            else
            {
                this.Visibility = Visibility.Visible;
            }
        }
    }
}
