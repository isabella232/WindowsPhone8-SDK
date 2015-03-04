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

namespace Telerik.DesignTemplates.WP.Templates.Lists
{
    public partial class List10 : UserControl
    {
        public List10()
        {
            InitializeComponent();
        }

        private void slideView_Tap(object sender, GestureEventArgs e)
        {
            MainViewModel.Instance.TemplatePage.NavigationService.Navigate(new Uri("/Pages/ZoomImage.xaml?item=" + this.slideView.SelectedItem.ToString(), UriKind.RelativeOrAbsolute));
        }
    }
}
