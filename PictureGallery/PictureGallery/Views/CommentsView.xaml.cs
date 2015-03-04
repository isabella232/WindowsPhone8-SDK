using System.Linq;
using System.Windows;
using System.Windows.Controls;
using PictureGallery.ViewModels;
using Telerik.Windows.Controls;

namespace PictureGallery.Views
{
    /// <summary>
    /// Represents a view that can be bound to a data source of photo comments.
    /// </summary>
    public partial class CommentsView : UserControl
    {
        public static readonly DependencyProperty PhotoProperty =
            DependencyProperty.Register("Photo", typeof(PhotoViewModel), typeof(CommentsView), new PropertyMetadata(null));

        public CommentsView()
        {
            this.InitializeComponent();
        }

        public PhotoViewModel Photo
        {
            get
            {
                return (PhotoViewModel)this.GetValue(CommentsView.PhotoProperty);
            }

            set
            {
                this.SetValue(CommentsView.PhotoProperty, value);
            }
        }

        private void OnCommentsTap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            if (this.busyIndicator.IsRunning)
            {
                // If we're currently downloading comments, ignore all tap gestures.
                return;
            }

            // Find the tapped comment.
            var listBoxItem = ElementTreeHelper.FindVisualDescendant<RadDataBoundListBoxItem>(sender as DependencyObject, (child) =>
            {
                var item = child as RadDataBoundListBoxItem;
                if (item == null)
                {
                    return false;
                }

                var layoutSlot = new Rect(new Point(), item.RenderSize);
                var tapLocation = e.GetPosition(item);

                return layoutSlot.Contains(tapLocation);
            });

            if (listBoxItem == null)
            {
                return;
            }

            var comment = (CommentViewModel)listBoxItem.Content;
            App.Instance.CurrentImageServiceProvider.GetAuthor(comment.UserId, this.OnCommentAuthorDownloaded);
        }

        private void OnCommentAuthorDownloaded(AuthorViewModel commentAuthor)
        {
            if (!this.conversationView.IsLoaded)
            {
                // If the download finishes after the user has navigated to another page, we do nothing.
                return;
            }

            // Navigate to the author page with the downloaded author.
            App.Instance.SetCurrentViewModel(commentAuthor);
            App.Instance.CurrentPage.NavigationService.Navigate(App.AuthorPage);
        }
    }
}
