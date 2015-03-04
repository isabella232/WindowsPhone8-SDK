using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using Microsoft.Phone.Controls;
using PictureGallery.ViewModels;

namespace PictureGallery.Pages
{
    public partial class GalleryPage : PageBase
    {
        public GalleryPage()
        {
            InitializeComponent();
            this.DataContext = App.Instance.GetCurrentViewModel<GalleryViewModel>();
        }

        private GalleryViewModel Gallery
        {
            get
            {
                return this.DataContext as GalleryViewModel;
            }
        }

        protected override ImageServiceViewModel FavoriteViewModel
        {
            get 
            {
                return this.Gallery;
            }
        }

        public override bool CompareViewModel(ImageServiceViewModel viewModel)
        {
            GalleryViewModel gallery = viewModel as GalleryViewModel;
            if (gallery == null)
            {
                return false;
            }

            return gallery.GalleryId == this.Gallery.GalleryId;
        }

        private void OnSlideViewTap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            App.Instance.SetCurrentViewModel<PhotoListViewModel>(this.Gallery.PhotoList);
            this.NavigationService.Navigate(App.PanZoomPage);
        }
    }
}