using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Telerik.DesignTemplates.WP.ViewModels;

namespace Telerik.DesignTemplates.WP.Templates.Galleries
{
    public partial class Gallery02 : UserControl
    {
        public Gallery02()
        {
            InitializeComponent();
        }

        private void jumpList_ItemTap(object sender, Telerik.Windows.Controls.ListBoxItemTapEventArgs e)
        {
            MainViewModel.Instance.TemplatePage.NavigationService.Navigate(new Uri("/Pages/ZoomImage.xaml?item=" + this.jumpList.SelectedItem.ToString(), UriKind.RelativeOrAbsolute));
        }
    }
}
