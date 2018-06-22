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
using System.Windows.Shapes;

namespace UserLoginSys
{
    /// <summary>
    /// ChangePwdWindow.xaml 的交互逻辑
    /// </summary>
    public partial class ChangePwdWindow : Window
    {
        public ChangePwdWindow()
        {
            InitializeComponent();
        }

        private void _ChangeBtn_Click(object sender, RoutedEventArgs e)
        {
            string originalPwdHash = EncryptDecrypt.EncryptString(_OriginalPwdBox.Password);
            if (originalPwdHash!=Workspace.This.LoginUser.PasswordHash)
            {
                MessageBox.Show("Please ensure the original password!");
                return;
            }
            if (_NewPwdBox.Password == string.Empty || _NewPwdBox.Password != _NewPwdConfirmBox.Password)
            {
                MessageBox.Show("Please ensure the new password!");
                return;
            }
            Workspace.This.LoginUser.ChangePassword(_NewPwdBox.Password);
            AccountsManagement.SaveToFile(Workspace.This.Users, Workspace.This.UserFilePath);
            this.Close();
        }
    }
}
