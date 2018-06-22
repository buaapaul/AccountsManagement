using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Data;

namespace UserLoginSys
{
    class UserRoleToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return DependencyProperty.UnsetValue;
            }
            UserRoles userRole = ((User)value).UserRole;
            string strParameter = parameter as string;
            UserRoles givenRole;
            if(!Enum.TryParse(strParameter, out givenRole))
            {
                return DependencyProperty.UnsetValue;
            }
            if (userRole <= givenRole)
            {
                return Visibility.Visible;
            }
            else
            {
                return Visibility.Collapsed;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
