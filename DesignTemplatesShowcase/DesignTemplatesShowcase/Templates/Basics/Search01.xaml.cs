using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Telerik.DesignTemplates.WP.ViewModels;

namespace Telerik.DesignTemplates.WP.Templates.Basics
{
    public partial class Search01 : UserControl
    {
        public Search01()
        {
            this.InitializeComponent();
        }

        private void RadAutoComplete_GotFocus(object sender, RoutedEventArgs e)
        {
            var bar = MainViewModel.Instance.TemplatePage.ApplicationBar;
            bar.IsVisible = false;
        }

        private void RadAutoComplete_LostFocus(object sender, RoutedEventArgs e)
        {
            var bar = MainViewModel.Instance.TemplatePage.ApplicationBar;
            bar.IsVisible = true;
        }
    }
}