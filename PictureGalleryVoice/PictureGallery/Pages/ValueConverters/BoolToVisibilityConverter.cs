using System;
using System.Windows.Data;
using System.Windows;

namespace PictureGallery.Pages
{
    public class BoolToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            bool reverse = false;
            if (parameter != null)
            {
                string param = (string)parameter;
                if (param != null)
                {
                    reverse = bool.Parse(parameter.ToString());
                }
            }

            bool boolValue = reverse ? (bool)value : !((bool)value);

            if (boolValue)
            {
                return Visibility.Visible;
            }
            else
            {
                return Visibility.Collapsed;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
