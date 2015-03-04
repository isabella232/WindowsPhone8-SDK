using System;
using System.Windows.Data;

namespace PictureGallery.Pages
{
    public class GalleryCountConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var count = (int)value;

            return count == 1 ? Strings.Gallery : Strings.GalleryPlural;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}