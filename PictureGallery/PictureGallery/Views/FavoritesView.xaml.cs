using System;
using System.Linq;
using System.Windows.Controls;
using PictureGallery.ViewModels;
using Telerik.Windows.Controls;

namespace PictureGallery.Views
{
    public partial class FavoritesView : UserControl
    {
        public FavoritesView()
        {
            this.InitializeComponent();
        }

        public event EventHandler FavoriteTap;

        private void OnFavoritesContextMenuItemTapped(object sender, ContextMenuItemSelectedEventArgs e)
        {
            App.Instance.Favorites.Remove((sender as RadContextMenu).DataContext as ImageServiceViewModel);
        }
       
        private void OnFavoriteTap(object sender, ListBoxItemTapEventArgs e)
        {
            if (this.FavoriteTap == null)
            {
                return;
            }

            this.FavoriteTap((e.Item.Content as ImageServiceViewModel).ActualViewModel, EventArgs.Empty);
        }
    }
}
