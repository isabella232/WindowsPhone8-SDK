using System;
using System.Windows.Data;

namespace PictureGallery.Pages
{
    public class TagScoreToOpacityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var score = (double)value;
            return Math.Max(0.2, score);
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}