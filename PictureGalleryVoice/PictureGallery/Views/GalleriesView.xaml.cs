using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using PictureGallery.ViewModels;
using Telerik.Windows.Controls;

namespace PictureGallery.Views
{
    public partial class GalleriesView : UserControl
    {
        public event EventHandler GalleryTapped;

        public GalleriesView()
        {
            InitializeComponent();
        }

        private void OnGalleryTap(object sender, ListBoxItemTapEventArgs e)
        {
            if(this.GalleryTapped == null)
            {
                return;
            }

            this.GalleryTapped(e.Item.Content, EventArgs.Empty);
        }
    }
}
