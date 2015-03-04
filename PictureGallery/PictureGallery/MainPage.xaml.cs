using System;
using System.Linq;
using System.Windows;
using Microsoft.Phone.Controls;
using PictureGallery.Pages;

namespace PictureGallery
{
    public partial class MainPage : PhoneApplicationPage
    {
        private MainContent content;

        public MainPage()
        {
            this.InitializeComponent();
        }

        private void OnBusyIndicatorLoaded(object sender, RoutedEventArgs e)
        {
            this.Dispatcher.BeginInvoke(() =>
            {
                this.content = new MainContent();
                this.content.Loaded += this.OnContentLoaded;
                this.LayoutRoot.Children.Add(this.content);
            });

            this.busyIndicator.Loaded -= this.OnBusyIndicatorLoaded;
        }

        private void OnContentLoaded(object sender, RoutedEventArgs e)
        {
            this.busyIndicator.IsRunning = false;
            this.LayoutRoot.Children.Remove(this.fakeBackground);
            this.LayoutRoot.Children.Remove(this.busyIndicator);
            this.content.Loaded -= this.OnContentLoaded;
        }
    }
}