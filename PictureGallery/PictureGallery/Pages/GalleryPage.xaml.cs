using System.Linq;
using PictureGallery.ViewModels;

namespace PictureGallery.Pages
{
    public partial class GalleryPage : PageBase
    {
        public GalleryPage()
        {
            this.InitializeComponent();
            this.DataContext = App.Instance.GetCurrentViewModel<GalleryViewModel>();
        }

        protected override ImageServiceViewModel FavoriteViewModel
        {
            get 
            {
                return this.Gallery;
            }
        }

        private GalleryViewModel Gallery
        {
            get
            {
                return this.DataContext as GalleryViewModel;
            }
        }

        public override bool CompareViewModel(ImageServiceViewModel viewModel)
        {
            var gallery = viewModel as GalleryViewModel;
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