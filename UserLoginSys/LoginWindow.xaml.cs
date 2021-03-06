﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;

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
                if(string.Equals(user.UserName,_NameBox.Text, StringComparison.OrdinalIgnoreCase))
                {
                    if (user.LockOutCount >= 5)
                    {
                        MessageBox.Show("You are locked out because of too many bad attempts, please contact the administrators for help!");
                        return;
                    }
                    if (user.VerifyPassword(_PwdBox.Password))
                    {
                        break;
                    }
                    else
                    {
                        MessageBox.Show("Incorrect password!");
                        AccountsManagement.SaveToFile(Workspace.This.Users, Workspace.This.UserFilePath);
                        return;
                    }
                }
            }
            if (i >= Workspace.This.Users.Count)
            {
                MessageBox.Show("Invalid user name!");
                return;
            }
            AccountsManagement.SaveToFile(Workspace.This.Users, Workspace.This.UserFilePath);
            Workspace.This.LoginUser = Workspace.This.Users[i];
            this.DialogResult = true;
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                Workspace.This.Users = AccountsManagement.LoadFromFile(Workspace.This.UserFilePath);
                Keyboard.Focus(this._NameBox);
            }
            catch (System.IO.FileNotFoundException)
            {
                MessageBox.Show("Accounts data missed! contact administrators to setup the software!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                this.Close();
            }
            catch (System.Runtime.Serialization.SerializationException)
            {
                MessageBox.Show("Accounts data are damaged, please contact administrators!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                this.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
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
