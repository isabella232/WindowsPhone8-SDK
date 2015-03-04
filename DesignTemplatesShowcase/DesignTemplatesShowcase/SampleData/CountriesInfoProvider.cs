using System;
using System.Collections.Generic;
using Telerik.Windows.Controls;

namespace Telerik.DesignTemplates.WP.SampleData
{
    public class CountriesInfoProvider : IGenericListFieldInfoProvider
    {
        public System.Collections.IEnumerable ItemsSource
        {
            get
            {
                return new List<string>() { "Country 1", "Country 2", "Country 3", "Country 4", "Country 5" };
            }
        }

        public IGenericListValueConverter ValueConverter
        {
            get
            {
                return null;
            }
        }
    }
}
