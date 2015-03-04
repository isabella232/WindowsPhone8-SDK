using System;
using System.Linq;
using System.Windows;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Tasks;
using Telerik.DesignTemplates.WP.Controls;
using Telerik.DesignTemplates.WP.ViewModels;

namespace Telerik.DesignTemplates.WP
{
    public partial class MainPage : PhoneApplicationPage
    {
        private MainContent mainContent;

        public MainPage()
        {
            this.InitializeComponent();

            if (MainViewModel.Instance.IsDataLoaded)
            {
                this.AddMainContent(false);
            }
            this.Loaded += this.OnLoaded;
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            if (MainViewModel.Instance.IsDataLoaded)
            {
                return;
            }
            MainViewModel.Instance.LoadData();
            this.Dispatcher.BeginInvoke(() =>
            {
                this.AddMainContent(true);
            });
        }

        private void AddMainContent(bool waitForLoaded)
        {
            this.mainContent = new MainContent();

            if (waitForLoaded)
            {
                this.mainContent.Opacity = 0;
                this.mainContent.Loaded += this.OnMainContent_Loaded;
            }
            else
            {
                this.LayoutRoot.Children.Remove(this.loadingScreen);
                this.Dispatcher.BeginInvoke(() =>
                {
                    this.ApplicationBar.IsVisible = true;
                });
            }
            this.Dispatcher.BeginInvoke(() =>
            {
                this.LayoutRoot.Children.Add(this.mainContent);
            });
        }

        private void OnMainContent_Loaded(object sender, RoutedEventArgs e)
        {
            this.mainContent.Loaded -= this.OnMainContent_Loaded;
            this.Dispatcher.BeginInvoke(() =>
            {
                this.mainContent.Opacity = 1;
                this.LayoutRoot.Children.Remove(this.loadingScreen);
                this.Dispatcher.BeginInvoke(() =>
                {
                    this.ApplicationBar.IsVisible = true;
                });
            });
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