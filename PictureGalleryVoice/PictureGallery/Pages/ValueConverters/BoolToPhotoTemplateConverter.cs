using System.Windows.Data;
using System.Windows;

namespace PictureGallery.Pages
{
    public class BoolToPhotoTemplateConverter : IValueConverter
    {
        public DataTemplate SquareTemplate
        {
            get;
            set;
        }

        public DataTemplate RectangleTemplate
        {
            get;
            set;
        }

        public object Convert(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if ((bool)value)
            {
                return this.RectangleTemplate;
            }
            else
            {
                return this.SquareTemplate;
            }
        }

        public object ConvertBack(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new System.NotImplementedException();
        }
    }
}
