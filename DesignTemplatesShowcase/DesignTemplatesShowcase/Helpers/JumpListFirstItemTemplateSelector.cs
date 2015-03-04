using System;
using System.Windows;
using Telerik.Windows.Controls;

namespace Telerik.DesignTemplates.WP.Helpers
{
    public class JumpListFirstItemTemplateSelector : DataTemplateSelector
    {
        private bool isFirst = true;

        public DataTemplate FirstItemTemplate { get; set; }

        public DataTemplate StandardItemTemplate { get; set; }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            if (this.isFirst)
            {
                this.isFirst = false;
                return this.FirstItemTemplate;
            }
            else
            {
                return this.StandardItemTemplate;
            }
        }
    }
}