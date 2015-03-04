using System;
using System.Windows;
using System.Windows.Data;

namespace PictureGallery.Pages
{
    public class BoolToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var reverse = false;
            if (parameter != null)
            {
                var param = (string)parameter;
                if (param != null)
                {
                    reverse = bool.Parse(parameter.ToString());
                }
            }

            var boolValue = reverse ? (bool)value : !((bool)value);

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
