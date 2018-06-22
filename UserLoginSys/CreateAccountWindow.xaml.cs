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
    /// CreateAccountWindow.xaml 的交互逻辑
    /// </summary>
    public partial class CreateAccountWindow : Window
    {
        public CreateAccountWindow()
        {
            InitializeComponent();
        }

        private void _CreateBtn_Click(object sender, RoutedEventArgs e)
        {
            CreateAccountViewModel dataContext = DataContext as CreateAccountViewModel;
            if (dataContext == null)
            {
                return;
            }
            dataContext.Password = _PwdBox.Password;
            dataContext.ConfirmPassword = _PwdConfirmBox.Password;
            dataContext.CreateCmd.Execute(null);
            if (dataContext.IsCreationSucceeded)
            {
                this.Close();
            }
        }

        private void _CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
