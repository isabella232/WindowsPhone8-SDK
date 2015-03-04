using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Telerik.DesignTemplates.WP.ViewModels;

namespace Telerik.DesignTemplates.WP.Controls
{
    public partial class MainContent : UserControl
    {
        public MainContent()
        {
            this.InitializeComponent();
            this.Loaded += this.OnLoaded;
            this.DataContext = MainViewModel.Instance;
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            this.newItemsListBox.ItemsSource = MainViewModel.Instance.NewTemplates;
        }

        private void ContentViewsCategory_Tap(object sender, GestureEventArgs e)
        {
            var mainPage = Telerik.Windows.Controls.ElementTreeHelper.FindVisualAncestor<Page>(this);
            mainPage.NavigationService.Navigate(new Uri("/Pages/ContentViews.xaml", UriKind.RelativeOrAbsolute));
        }

        private void BuildingBlocksCategory_Tap(object sender, GestureEventArgs e)
        {
            var buildingBlock = MainViewModel.Instance.AllTags.Where(t => t.Tag == "building blocks").First();
            MainViewModel.Instance.SelectedTag = buildingBlock;
            var mainPage = Telerik.Windows.Controls.ElementTreeHelper.FindVisualAncestor<Page>(this);
            mainPage.NavigationService.Navigate(new Uri("/Pages/TagPage.xaml", UriKind.RelativeOrAbsolute));
        }

        private void PagesCategory_Tap(object sender, GestureEventArgs e)
        {
            var mainPage = Telerik.Windows.Controls.ElementTreeHelper.FindVisualAncestor<Page>(this);
            mainPage.NavigationService.Navigate(new Uri("/Pages/Pages.xaml", UriKind.RelativeOrAbsolute));
        }

        private void RadDataBoundListBox_ItemTap(object sender, Windows.Controls.ListBoxItemTapEventArgs e)
        {
            var model = e.Item.Content as TemplateViewModel;
            MainViewModel.Instance.SelectedTemplate = model;
            MainViewModel.Instance.SelectedTag = new TagInfo() { Tag = MainViewModel.IsNewTagName };
            var mainPage = Telerik.Windows.Controls.ElementTreeHelper.FindVisualAncestor<Page>(this);
            mainPage.NavigationService.Navigate(new Uri("/Pages/TemplatePage.xaml", UriKind.RelativeOrAbsolute));
        }

        private void HyperlinkButton_Click(object sender, RoutedEventArgs e)
        {
            var mainPage = Telerik.Windows.Controls.ElementTreeHelper.FindVisualAncestor<Page>(this);
            mainPage.NavigationService.Navigate(new Uri("/Pages/About.xaml", UriKind.RelativeOrAbsolute));
        }
    }
}