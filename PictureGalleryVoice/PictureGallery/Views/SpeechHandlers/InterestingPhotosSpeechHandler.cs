using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PictureGallery.Speech;
using PictureGallery.ViewModels;

namespace PictureGallery.Views.SpeechHandlers
{
    public class InterestingPhotosSpeechHandler : ISpeechInputHandler
    {
        private const string GO_BACK = "go back";
        private const string ZOOM_IN = "zoom in";

        public bool CanHandleInput(string input)
        {
            return true;
        }

        public void HandleInput(System.Windows.FrameworkElement target, string input)
        {
            if (string.Compare(GO_BACK, input, StringComparison.InvariantCultureIgnoreCase) == 0)
            {
                if (App.Instance.CurrentPage.NavigationService.CanGoBack)
                {
                    App.Instance.CurrentPage.NavigationService.GoBack();
                }
            }

            if (string.Compare(ZOOM_IN, input, StringComparison.InvariantCultureIgnoreCase) == 0)
            {
                PhotoCollectionView view = target as PhotoCollectionView;
                App.Instance.SetCurrentViewModel<PhotoListViewModel>(view.PhotoList);
                App.Instance.CurrentPage.NavigationService.Navigate(App.PhotoPage);
            }
        }

        public void NotifyInputError(System.Windows.FrameworkElement target)
        {
            throw new NotImplementedException();
        }
    }
}
