using System;
using System.Windows.Data;
using Telerik.Windows.Controls;

namespace PictureGallery.Pages
{
    public class BoolToDataVirtualizationModeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if ((bool)value)
            {
                return DataVirtualizationMode.OnDemandManual;
            }

            return DataVirtualizationMode.None;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}