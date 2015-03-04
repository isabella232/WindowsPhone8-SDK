using System;
using System.Linq;
using System.Windows.Controls;
using Telerik.Windows.Controls;

namespace PictureGallery.Views
{
    public partial class GalleriesView : UserControl
    {
        public GalleriesView()
        {
            this.InitializeComponent();
        }

        public event EventHandler GalleryTapped;

        private void OnGalleryTap(object sender, ListBoxItemTapEventArgs e)
        {
            if (this.GalleryTapped == null)
            {
                return;
            }

            this.GalleryTapped(e.Item.Content, EventArgs.Empty);
        }
    }
}