using System;
using System.Collections.Generic;
using Telerik.Windows.Controls;

namespace Telerik.DesignTemplates.WP.SampleData
{
    public class OptionsInfoProvider : IGenericListFieldInfoProvider
    {
        public System.Collections.IEnumerable ItemsSource
        {
            get
            {
                return new List<string>() { "Option 1", "Option 2", "Option 3", "Option 4", "Option 5" };
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
