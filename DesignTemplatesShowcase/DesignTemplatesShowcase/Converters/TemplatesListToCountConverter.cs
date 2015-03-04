using System;
using System.Collections.Generic;
using System.Windows.Data;
using Telerik.DesignTemplates.WP.ViewModels;

namespace Telerik.DesignTemplates.WP.Converters
{
    public class TemplatesListToCountConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var categoryId = int.Parse(parameter.ToString());
            var allTemplates = (List<TemplateViewModel>)value;
            var count = 0;
            var categoryName = string.Empty;
            switch (categoryId)
            {
                case 0:
                    categoryName = MainViewModel.ContentViewsCategoryName;
                    break;
                case 1:
                    categoryName = MainViewModel.BuildingBlocksCategoryName;
                    break;
                case 2:
                    categoryName = MainViewModel.PagesCategoryName;
                    break;
            }
            foreach (TemplateViewModel template in allTemplates)
            {
                if (template.CategoryName == categoryName)
                {
                    count++;
                }
            }
            return count;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
