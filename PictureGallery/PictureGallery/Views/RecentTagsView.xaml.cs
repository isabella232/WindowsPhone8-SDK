using System;
using System.Windows;
using System.Windows.Controls;
using PictureGallery.ViewModels;
using Telerik.Windows.Controls;

namespace PictureGallery.Views
{
    public partial class RecentTagsView : UserControl
    {
        public static readonly DependencyProperty TagListProperty =
            DependencyProperty.Register("TagList", typeof(TagListViewModel), typeof(RecentTagsView), new PropertyMetadata(null));

        public RecentTagsView()
        {
            this.InitializeComponent();
        }

        public event EventHandler TagTap;

        public TagListViewModel TagList
        {
            get
            {
                return (TagListViewModel)this.GetValue(RecentTagsView.TagListProperty);
            }

            set
            {
                this.SetValue(RecentTagsView.TagListProperty, value);
            }
        }

        private void OnTagTap(object sender, ListBoxItemTapEventArgs e)
        {
            if (this.TagTap == null)
            {
                return;
            }

            this.TagTap(e.Item.Content, EventArgs.Empty);
        }
    }
}