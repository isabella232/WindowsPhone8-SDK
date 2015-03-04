using System;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using Telerik.Windows.Data;

namespace AgendaViewer
{
    public class AppointmentConflictVisibilityConverter : IValueConverter
    { 
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var group = (DataGroup)value;
            
            return group.Items.Cast<object>().Count() > 1 ? Visibility.Visible : Visibility.Collapsed;
        }
        
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}