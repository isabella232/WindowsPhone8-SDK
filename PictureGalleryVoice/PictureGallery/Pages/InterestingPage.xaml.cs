using System;
using Microsoft.Phone.Controls;
using PictureGallery.Speech;
using PictureGallery.ViewModels;

namespace PictureGallery.Pages
{
    public partial class InterestingPage : PhoneApplicationPage
    {
        public InterestingPage()
        {
            InitializeComponent();
            this.photoView.PhotoList = App.Instance.GetCurrentViewModel<PhotoListViewModel>();
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            SpeechManager.StartListening();
        }

        protected override void OnNavigatedFrom(System.Windows.Navigation.NavigationEventArgs e)
        {
            SpeechManager.Reset();
            base.OnNavigatedFrom(e);
        }

        private void OnPhotoTapped(object sender, EventArgs e)
        {
            // Navigate to photo page when a photo is selected.
            App.Instance.SetCurrentViewModel<PhotoListViewModel>(this.photoView.PhotoList);
            NavigationService.Navigate(App.PhotoPage);
        }
    }
}