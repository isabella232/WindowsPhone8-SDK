using System;
using System.Linq;
using System.Windows;
using Telerik.Windows.Controls;

namespace PictureGallery.Pages
{
    public class CommentTemplateSelector : DataTemplateSelector
    {
        private bool tempalteSelector;

        public DataTemplate Template1 { get; set; }

        public DataTemplate Template2 { get; set; }

        public override System.Windows.DataTemplate SelectTemplate(object item, System.Windows.DependencyObject container)
        {
            this.tempalteSelector = !this.tempalteSelector;

            if (this.tempalteSelector)
            {
                return this.Template1;
            }
            else
            {
                return this.Template2;
            }
        }
    }
}
