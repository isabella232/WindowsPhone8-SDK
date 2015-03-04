using System;
using System.Windows.Data;

namespace AgendaViewer
{
    public class DayHeaderConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var date = (DateTime)value;
            var now = DateTime.Now;
            
            if (now.Date == date.Date)
            {
                return "TODAY";
            }
            
            return date.ToString("dddd, MMMM dd, yyyy", System.Globalization.CultureInfo.CurrentUICulture).ToUpperInvariant();
        }
        
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}