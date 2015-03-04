using Microsoft.Phone.Controls;
using System.Linq;
using PictureGallery.ViewModels;

namespace PictureGallery.Pages
{
    public abstract class PageBase : PhoneApplicationPage
    {
        protected abstract ImageServiceViewModel FavoriteViewModel
        {
            get;
        }

        public abstract bool CompareViewModel(ImageServiceViewModel viewModel);

        public void PinFavorite(bool unpin)
        {
            if (unpin)
            {
                ImageServiceViewModel viewModelToRemove = App.Instance.Favorites.Single<ImageServiceViewModel>((viewModel) =>
                {
                    return this.CompareViewModel(viewModel);
                });

                App.Instance.Favorites.Remove(viewModelToRemove);
            }
            else
            {
                App.Instance.Favorites.Add(this.FavoriteViewModel);
            }
        }

        public virtual void NextImage()
        {
        }

        public virtual void PreviousImage()
        {
        }

        public virtual bool HasPreviousImage
        {
            get
            {
                return true;
            }
        }

        public virtual bool HasNextImage
        {
            get
            {
                return true;
            }
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            App.Instance.UpdatePinButton();
        }
    }
}
