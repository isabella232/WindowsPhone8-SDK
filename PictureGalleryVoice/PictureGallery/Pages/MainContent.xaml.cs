using System;
using System.Windows.Controls;
using PictureGallery.ViewModels;
using Microsoft.Phone.Shell;
using System.Windows.Input;

namespace PictureGallery.Pages
{
    public partial class MainContent : UserControl
    {
        public MainContent()
        {
            InitializeComponent();

            this.recentPhotosView.DataContext = App.Instance.CurrentImageServiceProvider.CreateRecentPhotoList(DateTime.Today.AddDays(-7));
            this.recentTagsView.DataContext = App.Instance.CurrentImageServiceProvider.CreateRecentTagList();
            this.favoritesView.DataContext = App.Instance.Favorites;

            this.Loaded += this.OnLoaded;
        }

        private void OnLoaded(object sender, System.Windows.RoutedEventArgs e)
        {
            this.Loaded -= this.OnLoaded;
            App.Instance.CheckNetwork();
        }

        private void OnFavoriteTap(object sender, EventArgs e)
        {
            ImageServiceViewModel viewModel = sender as ImageServiceViewModel;
            PhoneApplicationService.Current.State[viewModel.GetType().Name] = viewModel;
            App.Instance.CurrentPage.NavigationService.Navigate(viewModel.PageUri);
        }
        
        private void OnRecentImagesTap(object sender, GestureEventArgs e)
        {
            App.Instance.SetCurrentViewModel<PhotoListViewModel>(this.recentPhotosView.DataContext as PhotoListViewModel);
            App.Instance.CurrentPage.NavigationService.Navigate(App.InterestingPage);
        }

        private void OnTagTap(object sender, EventArgs e)
        {
            App.Instance.SetCurrentViewModel<TagViewModel>(sender as TagViewModel);
            App.Instance.CurrentPage.NavigationService.Navigate(App.TagPage);
        }
    }
}
