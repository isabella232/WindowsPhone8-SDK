using System.Linq;
using Microsoft.Phone.Controls;
using PictureGallery.ViewModels;

namespace PictureGallery.Pages
{
    public abstract class PageBase : PhoneApplicationPage
    {
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

        protected abstract ImageServiceViewModel FavoriteViewModel { get; }

        public abstract bool CompareViewModel(ImageServiceViewModel viewModel);

        public void PinFavorite(bool unpin)
        {
            if (unpin)
            {
                var viewModelToRemove = App.Instance.Favorites.Single<ImageServiceViewModel>((viewModel) =>
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

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            App.Instance.UpdatePinButton();
        }
    }
}
