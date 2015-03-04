using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PictureGallery.Speech;
using Telerik.Windows.Controls;

namespace PictureGallery.Views.SpeechHandlers
{
    public class PhotoPanAndZoomSpeechHandler : ISpeechInputHandler
    {
        private const string GO_BACK = "go back";
        private const string NEXT = "next";
        private const string PREVIOUS = "previous";

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


            if (string.Compare(NEXT, input, StringComparison.InvariantCultureIgnoreCase) == 0)
            {
                RadSlideView sv = target as RadSlideView;
                sv.MoveToNextItem();
            }

            if (string.Compare(PREVIOUS, input, StringComparison.InvariantCultureIgnoreCase) == 0)
            {
                RadSlideView sv = target as RadSlideView;
                sv.MoveToPreviousItem();
            }
        }

        public void NotifyInputError(System.Windows.FrameworkElement target)
        {
            throw new NotImplementedException();
        }
    }
}
