using System;
using System.Linq;
using System.Windows.Controls;

namespace Telerik.DesignTemplates.WP.Templates.Basics
{
    public partial class Profile : UserControl
    {
        public Profile()
        {
            this.InitializeComponent();
            this.DataContext = new Telerik.DesignTemplates.WP.ViewModels.DataItemViewModel()
            {
                Title = "Title",
                Information = "Information",
                ImageThumbnailSource = new Uri((string)App.Current.Resources["FrameThumbnailSource"], UriKind.RelativeOrAbsolute)
            };
        }
    }
}