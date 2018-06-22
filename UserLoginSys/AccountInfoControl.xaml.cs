﻿using System;
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
    /// AccountInfoControl.xaml 的交互逻辑
    /// </summary>
    public partial class AccountInfoControl : UserControl
    {
        public AccountInfoControl()
        {
            InitializeComponent();
        }

        private void _ChangePwdBtn_Click(object sender, RoutedEventArgs e)
        {
            ChangePwdWindow changePwdWindow = new ChangePwdWindow();
            changePwdWindow.DataContext = Workspace.This;
            changePwdWindow.ShowDialog();
        }
    }
}
