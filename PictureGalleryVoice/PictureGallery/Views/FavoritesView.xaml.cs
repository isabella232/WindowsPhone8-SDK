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
using Telerik.Windows.Controls;
using PictureGallery.ViewModels;
using Microsoft.Phone.Shell;

namespace PictureGallery.Views
{
    public partial class FavoritesView : UserControl
    {
        public FavoritesView()
        {
            InitializeComponent();
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
