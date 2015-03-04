using System;
using System.Collections.Generic;
using System.Windows.Data;

namespace Telerik.DesignTemplates.WP.Converters
{
    public class ListToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var tags = (List<string>)value;
            var text = string.Join("; ", tags);
            return text;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
