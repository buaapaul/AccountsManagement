using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;

namespace UserLoginSys
{
    class CreateAccountViewModel : ViewModelBase
    {
        private string _UserName = string.Empty;
        private string _DisplayName = string.Empty;
        private RelayCommand _CreateCmd;
        private UserRoles _SelectedRole;

        public CreateAccountViewModel(UserRoles setterRole)
        {
            AvailableRoles = new ObservableCollection<UserRoles>();
            switch (setterRole)
            {
                case UserRoles.SuperAdmin:
                    AvailableRoles.Add(UserRoles.Admin);
                    AvailableRoles.Add(UserRoles.Manager);
                    AvailableRoles.Add(UserRoles.Standard);
                    break;
                case UserRoles.Admin:
                    AvailableRoles.Add(UserRoles.Manager);
                    AvailableRoles.Add(UserRoles.Standard);
                    break;
                case UserRoles.Manager:
                    AvailableRoles.Add(UserRoles.Standard);
                    break;
            }
        }

        public string UserName
        {
            get { return _UserName; }
            set
            {
                if (_UserName != value)
                {
                    _UserName = value;
                    RaisePropertyChanged("UserName");
                }
            }
        }
        public string DisplayName
        {
            get { return _DisplayName; }
            set
            {
                if (_DisplayName != value)
                {
                    _DisplayName = value;
                    RaisePropertyChanged("DisplayName");
                }
            }
        }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public ObservableCollection<UserRoles> AvailableRoles { get; }
        public UserRoles SelectedRole
        {
            get { return _SelectedRole; }
            set
            {
                if (_SelectedRole != value)
                {
                    _SelectedRole = value;
                    RaisePropertyChanged("SelectedRole");
                }
            }
        }
        public bool IsCreationSucceeded { get; private set; }
        public RelayCommand CreateCmd
        {
            get
            {
                if (_CreateCmd == null)
                {
                    _CreateCmd = new RelayCommand(ExecuteCreateCmd, CanExecuteCreateCmd);
                }
                return _CreateCmd;
            }
        }

        private void ExecuteCreateCmd(object obj)
        {
            IsCreationSucceeded = false;
            if (UserName == string.Empty)
            {
                MessageBox.Show("Please enter user name!");
                return;
            }
            if (DisplayName == string.Empty)
            {
                MessageBox.Show("Please enter display name!");
                return;
            }
            if (Password == string.Empty || Password != ConfirmPassword)
            {
                MessageBox.Show("Please ensure the password!");
                return;
            }
            if (SelectedRole == UserRoles.SuperAdmin)
            {
                MessageBox.Show("Please select the User Role!");
                return;
            }
            User user = new User(UserName, DisplayName, EncryptDecrypt.EncryptString(Password), SelectedRole, 0, DateTime.Today.AddDays(30));
            Workspace.This.Users.Add(user);
            AccountsManagement.SaveToFile(Workspace.This.Users, Workspace.This.UserFilePath);
            IsCreationSucceeded = true;
        }
        private bool CanExecuteCreateCmd(object obj)
        {
            return true;
        }

    }
}
