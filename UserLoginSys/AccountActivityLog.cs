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
        [Description("automatically logout")]
        AutoLogout = 7,
    }
    class AccountActivityLog
    {
        public string UserName { get; private set; }
        public string Activity { get; private set; }
        public string LogTime { get; private set; }
        public string Comments { get; private set; }

        public AccountActivityLog() { }
        public AccountActivityLog(string userName, string activity, string logTime, string comments)
        {
            UserName = userName;
            Activity = activity;
            LogTime = logTime;
            Comments = comments;
        }
        public void LogActivity(string name, Activities activity, string comments)
        {
            UserName = name;
            Activity = GetDescription(activity);
            LogTime = DateTime.Now.ToString("yyyy-MM-dd-hh-mm-ss");
            Comments = comments;
        }

        class Description : Attribute
        {
            public string Text;
            public Description(string text)
            {
                Text = text;
            }
        }
        static string GetDescription(Enum source)
        {
            Type type = source.GetType();
            MemberInfo[] memberInfos = type.GetMember(source.ToString());
            if (memberInfos != null && memberInfos.Length > 0)
            {
                object[] attrs = memberInfos[0].GetCustomAttributes(typeof(Description), false);
                if (attrs != null && attrs.Length > 0)
                {
                    return ((Description)attrs[0]).Text;
                }
            }
            return source.ToString();
        }
    }
}
