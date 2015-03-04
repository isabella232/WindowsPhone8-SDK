using System;
using System.Windows.Data;
using Telerik.Windows.Controls;

namespace PictureGallery.Pages
{
    public class BoolToLayoutStrategyConverter : IValueConverter
    {
        public VirtualizationStrategyDefinition StackStrategy { get; set; }

        public VirtualizationStrategyDefinition WrapStrategy { get; set; }

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if ((bool)value)
            {
                return this.StackStrategy;
            }
            else
            {
                return this.WrapStrategy;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}