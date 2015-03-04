using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using PictureGallery.ViewModels;

namespace PictureGallery.Views
{
    public partial class RecentPhotosView : UserControl
    {
        public static readonly DependencyProperty PhotoListProperty =
            DependencyProperty.Register("PhotoList", typeof(PhotoListViewModel), typeof(RecentPhotosView), new PropertyMetadata(null));

        public RecentPhotosView()
        {
            this.InitializeComponent();
        }

        public PhotoListViewModel PhotoList
        {
            get
            {
                return (PhotoListViewModel)this.GetValue(RecentPhotosView.PhotoListProperty);
            }

            set
            {
                this.SetValue(RecentPhotosView.PhotoListProperty, value);
            }
        }

        protected override void OnTap(GestureEventArgs e)
        {
            if (this.PhotoList == null)
            {
                e.Handled = true;
            }

            base.OnTap(e);
        }
    }
}
