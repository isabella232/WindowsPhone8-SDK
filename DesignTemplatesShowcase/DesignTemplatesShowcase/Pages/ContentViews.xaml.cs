using System;
using System.Linq;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Tasks;
using Telerik.DesignTemplates.WP.ViewModels;

namespace Telerik.DesignTemplates.WP.Pages
{
    public partial class ContentViews : PhoneApplicationPage
    {
        public ContentViews()
        {
            this.InitializeComponent();
            this.DataContext = MainViewModel.Instance;
        }

        private void RadJumpList_ItemTap(object sender, Windows.Controls.ListBoxItemTapEventArgs e)
        {
            var selectedTag = (TagInfo)e.Item.Content;
            MainViewModel.Instance.SelectedTag = selectedTag;
            this.NavigationService.Navigate(new Uri("/Pages/TagPage.xaml", UriKind.RelativeOrAbsolute));
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