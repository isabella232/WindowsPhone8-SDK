using System;
using System.Windows.Data;

namespace PictureGallery.Pages
{
    public class PhotoCountConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            int count = (int)value;

            return count == 1 ? Strings.Photo : Strings.PhotoPlural;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
