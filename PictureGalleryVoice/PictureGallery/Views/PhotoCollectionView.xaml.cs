using System;
using System.Windows;
using System.Windows.Controls;
using PictureGallery.ViewModels;
using Telerik.Windows.Controls;

namespace PictureGallery.Views
{
    public partial class PhotoCollectionView : UserControl
    {
        public static readonly DependencyProperty PhotoListProperty =
            DependencyProperty.Register("PhotoList", typeof(PhotoListViewModel), typeof(PhotoCollectionView), new PropertyMetadata(null));

        public static readonly DependencyProperty PhotoCountProperty =
            DependencyProperty.Register("PhotoCount", typeof(int), typeof(PhotoCollectionView), new PropertyMetadata(0));

        public static readonly DependencyProperty EmptyContentProperty =
            DependencyProperty.Register("EmptyContent", typeof(object), typeof(PhotoCollectionView), new PropertyMetadata(null));

        public PhotoCollectionView()
        {
            InitializeComponent();
        }

        public event EventHandler PhotoTapped;

        public PhotoListViewModel PhotoList
        {
            get
            {
                return (PhotoListViewModel)this.GetValue(PhotoCollectionView.PhotoListProperty);
            }

            set
            {
                this.SetValue(PhotoCollectionView.PhotoListProperty, value);
            }
        }

        public int PhotoCount
        {
            get
            {
                return (int)this.GetValue(PhotoCollectionView.PhotoCountProperty);
            }

            set
            {
                this.SetValue(PhotoCollectionView.PhotoCountProperty, value);
            }
        }
        
        public object EmptyContent
        {
            get
            {
                return this.GetValue(PhotoCollectionView.EmptyContentProperty);
            }

            set
            {
                this.SetValue(PhotoCollectionView.EmptyContentProperty, value);
            }
        }

        private void OnDataRequested(object sender, EventArgs e)
        {
            this.PhotoList.LoadMore();
        }

        private void OnPhotoTapped()
        {
            if (this.PhotoTapped == null)
            {
                return;
            }

            this.PhotoTapped(this, null);
        }

        private void OnItemTap(object sender, ListBoxItemTapEventArgs e)
        {
            this.OnPhotoTapped();
        }
    }
}
