using System;
using System.Linq;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Tasks;
using Telerik.DesignTemplates.WP.ViewModels;
using Telerik.Windows.Controls;

namespace Telerik.DesignTemplates.WP.Pages
{
    public partial class TagPage : PhoneApplicationPage
    {
        public TagPage()
        {
            this.InitializeComponent(); 
            this.SetValue(RadTileAnimation.ContainerToAnimateProperty, this.DataBoundListBox);
            this.DataContext = MainViewModel.Instance;
        }

        private void RadDataBoundListBox_ItemTap(object sender, Windows.Controls.ListBoxItemTapEventArgs e)
        {
            var model = e.Item.Content as TemplateViewModel;
            MainViewModel.Instance.SelectedTemplate = model;
            this.NavigationService.Navigate(new Uri("/Pages/TemplatePage.xaml", UriKind.RelativeOrAbsolute));
        }

        private void ApplicationBarIconButton_Click(object sender, EventArgs e)
        {
            this.NavigationService.Navigate(new Uri("/Pages/Search.xaml", UriKind.RelativeOrAbsolute));
        }

        private void ApplicationBarMenuItem_Click(object sender, EventArgs e)
        {
            this.NavigationService.Navigate(new Uri("/Pages/About.xaml", UriKind.RelativeOrAbsolute));
        }

        private void ApplicationBarIconButton2_Click(object sender, EventArgs e)
        {
            var task = new WebBrowserTask();
            task.Uri = new Uri("http://www.telerik.com/products/windows-phone/getting-started/metro-design-templates.aspx", UriKind.RelativeOrAbsolute);
            task.Show();
        }
    }
}