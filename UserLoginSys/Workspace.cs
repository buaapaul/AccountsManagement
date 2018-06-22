using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;

namespace UserLoginSys
{
    class Workspace : ViewModelBase
    {
        public static Workspace This { get; } = new Workspace();

        #region Private Fields
        private ObservableCollection<User> _Users;
        private User _LoginUser;
        private User _SelectedUser;
        #endregion Private Fields

        #region Constructor
        public Workspace()
        {
        }
        #endregion Constructor

        #region Public Properties
        public string UserFilePath { get; private set; } = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Users.xml");
        public bool IsDeleteUserAllowed
        {
            get { return LoginUser?.UserRole == UserRoles.SuperAdmin; }
        }
        public User LoginUser
        {
            get { return _LoginUser; }
            set
            {
                if (_LoginUser != value)
                {
                    _LoginUser = value;
                    RaisePropertyChanged("LoginUser");
                    RaisePropertyChanged("IsDeleteUserAllowed");
                }
            }
        }
        public User SelectedUser
        {
            get { return _SelectedUser; }
            set
            {
                if (_SelectedUser != value)
                {
                    _SelectedUser = value;
                    RaisePropertyChanged("SelectedUser");
                }
            }
        }
        public ObservableCollection<User> Users
        {
            get { return _Users; }
            set
            {
                _Users = value;
                RaisePropertyChanged("Users");
            }
        }
        public MainWindow Owner { get; set; }
        #endregion Public Properties
    }
}
