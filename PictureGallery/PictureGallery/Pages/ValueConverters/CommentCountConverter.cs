using System;
using System.Windows.Data;

namespace PictureGallery.Pages
{
    public class CommentCountConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var count = (int)value;

            return count == 1 ? Strings.Comment : Strings.CommentPlural;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}