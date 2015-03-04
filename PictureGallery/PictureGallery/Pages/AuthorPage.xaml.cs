using System;
using PictureGallery.ViewModels;
using Telerik.Windows.Controls;

namespace PictureGallery.Pages
{
    public partial class AuthorPage : PageBase
    {
        public AuthorPage()
        {
            this.InitializeComponent();
            RadContinuumAnimation.SetContinuumElement(this, this);

            this.DataContext = App.Instance.GetCurrentViewModel<AuthorViewModel>();

            this.photoView.DataContext = App.Instance.CurrentImageServiceProvider.CreateAuthorPhotoList(this.Author.Id);
            this.galleriesView.DataContext = App.Instance.CurrentImageServiceProvider.CreateGalleryList(this.Author.Id);
        }

        protected override ImageServiceViewModel FavoriteViewModel
        {
            get 
            {
                return this.Author;
            }
        }

        private AuthorViewModel Author
        {
            get
            {
                return this.DataContext as AuthorViewModel;
            }
        }

        public override bool CompareViewModel(ImageServiceViewModel viewModel)
        {
            var author = viewModel as AuthorViewModel;
            if (author == null)
            {
                return false;
            }

            return this.Author.Id == author.Id;
        }

        private void OnGalleryTapped(object sender, EventArgs args)
        {
            App.Instance.SetCurrentViewModel<GalleryViewModel>(sender as GalleryViewModel);
            this.NavigationService.Navigate(App.GalleryPage);
        }

        private void OnPhotoTapped(object sender, EventArgs e)
        {
            var photoList = this.photoView.DataContext as PhotoListViewModel;
            photoList.Photos.Apply<PhotoViewModel>((photo) => photo.Author = this.Author);
            App.Instance.SetCurrentViewModel<PhotoListViewModel>(photoList);
            this.NavigationService.Navigate(App.PhotoPage);
        }
    }
}