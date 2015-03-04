using System;
using System.Linq;
using System.Windows.Input;
using Microsoft.Phone.Controls;
using Telerik.DesignTemplates.WP.ViewModels;

namespace Telerik.DesignTemplates.WP.Pages
{
    public partial class Search : PhoneApplicationPage
    {
        public Search()
        {
            this.InitializeComponent();
            this.DataContext = MainViewModel.Instance;
        }

        private void RadDataBoundListBox_ItemTap(object sender, Windows.Controls.ListBoxItemTapEventArgs e)
        {
            var selectedTag = (TagInfo)e.Item.Content;
            MainViewModel.Instance.SelectedTag = selectedTag;
            this.NavigationService.Navigate(new Uri("/Pages/TagPage.xaml", UriKind.RelativeOrAbsolute));
        }

        private void RadAutoCompleteBox_ActionButtonTap(object sender, EventArgs e)
        {
            //this.TagsListBox.Visibility = Visibility.Collapsed;
            //this.SearchResultsListBox.Visibility = Visibility.Visible;
            //this.SearchTextBlock.Tap += new EventHandler<GestureEventArgs>(SearchTextBlock_Tap);
        }

        private void SearchTextBlock_Tap(object sender, GestureEventArgs e)
        {
            //    this.TagsListBox.Visibility = Visibility.Visible;
            //    this.SearchResultsListBox.Visibility = Visibility.Collapsed;
            //    this.radAutoComplete.Text = string.Empty;
            //    this.SearchTextBlock.Tap -= new EventHandler<GestureEventArgs>(SearchTextBlock_Tap);
        }

        private void SearchResultsListBox_ItemTap(object sender, Windows.Controls.ListBoxItemTapEventArgs e)
        {
            var template = (TemplateViewModel)e.Item.Content;
            MainViewModel.Instance.SelectedTemplate = template;
            MainViewModel.Instance.SelectedTag = null;
            this.NavigationService.Navigate(new Uri("/Pages/TemplatePage.xaml?search=true", UriKind.RelativeOrAbsolute));
        }
    }
}