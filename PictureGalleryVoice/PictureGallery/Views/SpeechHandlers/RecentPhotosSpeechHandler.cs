using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PictureGallery.Speech;
using PictureGallery.ViewModels;

namespace PictureGallery.Views.SpeechHandlers
{
    public class RecentPhotosSpeechHandler : ISpeechInputHandler
    {
        private const string OPEN_RECENT_PHOTOS = "show recent";

        public bool CanHandleInput(string input)
        {
            return true;
        }

        public void HandleInput(System.Windows.FrameworkElement target, string input)
        {
            if (string.Compare(OPEN_RECENT_PHOTOS, input, StringComparison.InvariantCultureIgnoreCase) == 0)
            {
                RecentPhotosView recentPhotosView = target as RecentPhotosView;
                App.Instance.SetCurrentViewModel<PhotoListViewModel>(recentPhotosView.DataContext as PhotoListViewModel);
                App.Instance.CurrentPage.NavigationService.Navigate(App.InterestingPage);
            }
        }

        public void NotifyInputError(System.Windows.FrameworkElement target)
        {
            
        }
    }
}
