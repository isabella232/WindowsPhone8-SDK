using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Media.Imaging;
using Microsoft.Phone.Controls;
using PictureGallery.Speech;
using PictureGallery.ViewModels;
using System.Windows.Navigation;
using System.Windows;
using PictureGallery.Pages;

namespace PictureGallery
{
    public partial class MainPage : PhoneApplicationPage
    {
        private MainContent content;

        public MainPage()
        {
            InitializeComponent();
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
            SpeechManager.StartListening();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            if (this.LayoutRoot.Children.Contains(this.content))
            {
                SpeechManager.StartListening();
            }
        }
    }
}