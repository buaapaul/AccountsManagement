using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;

namespace UserLoginSys
{
    enum Activities
    {
        [Description("Default")]
        Default = 0,
        [Description("Login")]
        Login = 1,
        [Description("Create account")]
        CreateAccount = 2,
        [Description("Change password")]
        ChangePassword = 3,
        [Description("Reset lockout status")]
        ResetLockout = 4,
        [Description("Delete account")]
        DeleteAccount = 5,
        [Description("Manual logout")]
        ManualLogout = 6,
        [Description("Automatically logout")]
        AutoLogout = 7,
    }
    class ActivityLogManagement
    {
        public string AccountName { get; private set; }
        public string Activity { get; private set; }
        public string LogTime { get; private set; }
        public string Comments { get; private set; }

        #region Constructors
        public ActivityLogManagement(string accountName)
        {
            AccountName = accountName;
            Activity = GetDescription(Activities.Default);
            LogTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            Comments = string.Empty;
        }
        public ActivityLogManagement(string accountName, Activities activity, string logTime, string comments)
        {
            AccountName = accountName;
            Activity = GetDescription(activity);
            LogTime = logTime;
            Comments = comments;
        }
        #endregion Constructors

        #region Public Functions
        public void LogActivity(string name, Activities activity, string comments)
        {
            AccountName = name;
            Activity = GetDescription(activity);
            LogTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            Comments = comments;
        }
        public void LogLogin(bool succeeded)
        {
            Activity = GetDescription(Activities.Login);
            LogTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            Comments = succeeded ? "Succeeded" : "Failed";
        }
        public void LogCreateAccount(string createdAccount)
        {
            Activity = GetDescription(Activities.CreateAccount);
            LogTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            Comments = "Created " + createdAccount;
        }
        public void LogDeleteAccount(string deletedAccount)
        {
            Activity = GetDescription(Activities.DeleteAccount);
            LogTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            Comments = "Deleted " + deletedAccount;
        }
        public void LogLogout(bool isAuto)
        {
            if (isAuto)
            {
                Activity = GetDescription(Activities.AutoLogout);
            }
            else
            {
                Activity = GetDescription(Activities.ManualLogout);
            }
            LogTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            Comments = string.Empty;
        }
        #endregion Public Functions

        static string GetDescription(Enum source)
        {
            Type type = source.GetType();
            string name = Enum.GetName(type, source);
            if (name != null)
            {
                FieldInfo fieldInfo = type.GetField(name);
                if (fieldInfo != null)
                {
                    DescriptionAttribute attribute = Attribute.GetCustomAttribute(fieldInfo, typeof(DescriptionAttribute), false) as DescriptionAttribute;
                    if (attribute != null)
                    {
                        return attribute.Description;
                    }
                }
            }
            return source.ToString();
        }
    }
}
