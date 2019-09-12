using System;
using System.Globalization;
using System.Security;
using System.Windows;

namespace WpfTemplate
{
  
    public class SecureStringToStringConverter : BaseValueConverter<SecureStringToStringConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
           return ((SecureString)value).Unsecure();
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
