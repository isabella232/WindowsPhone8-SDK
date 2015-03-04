using System;
using Microsoft.Phone.Controls;
using PictureGallery.ViewModels;

namespace PictureGallery.Pages
{
    public partial class InterestingPage : PhoneApplicationPage
    {
        public InterestingPage()
        {
            this.InitializeComponent();
            this.photoView.PhotoList = App.Instance.GetCurrentViewModel<PhotoListViewModel>();
        }

        private void OnPhotoTapped(object sender, EventArgs e)
        {
            // Navigate to photo page when a photo is selected.
            App.Instance.SetCurrentViewModel<PhotoListViewModel>(this.photoView.PhotoList);
            this.NavigationService.Navigate(App.PhotoPage);
        }
    }
}