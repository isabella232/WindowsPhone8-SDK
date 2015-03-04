using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using PictureGallery.Speech;
using PictureGallery.ViewModels;

namespace PictureGallery.Pages
{
    public partial class PhotoPanZoomPage : PhoneApplicationPage
    {
        public PhotoPanZoomPage()
        {
            InitializeComponent();
            this.DataContext = App.Instance.GetCurrentViewModel<PhotoListViewModel>();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            SpeechManager.StartListening();
            base.OnNavigatedTo(e);
        }
    }
}