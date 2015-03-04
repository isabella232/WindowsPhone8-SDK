using System;
using System.Windows.Data;

namespace AgendaViewer
{
    public class TimeHeaderConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var date = (DateTime)value;
            return date.ToShortTimeString();
        }
        
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}