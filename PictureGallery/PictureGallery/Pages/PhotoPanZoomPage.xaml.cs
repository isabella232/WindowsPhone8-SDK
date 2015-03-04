using Microsoft.Phone.Controls;
using PictureGallery.ViewModels;

namespace PictureGallery.Pages
{
    public partial class PhotoPanZoomPage : PhoneApplicationPage
    {
        public PhotoPanZoomPage()
        {
            this.InitializeComponent();
            this.DataContext = App.Instance.GetCurrentViewModel<PhotoListViewModel>();
        }
    }
}