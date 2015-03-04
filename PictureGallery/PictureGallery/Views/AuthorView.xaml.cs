using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using PictureGallery.ViewModels;

namespace PictureGallery.Views
{
    public partial class AuthorView : UserControl
    {
        public static readonly DependencyProperty AuthorProperty =
            DependencyProperty.Register("Author", typeof(AuthorViewModel), typeof(AuthorView), new PropertyMetadata(null));

        public AuthorView()
        {
            this.InitializeComponent();
        }

        public event EventHandler AuthorTapped;

        public AuthorViewModel Author
        {
            get
            {
                return (AuthorViewModel)this.GetValue(AuthorView.AuthorProperty);
            }

            set
            {
                this.SetValue(AuthorView.AuthorProperty, value);
            }
        }

        private void OnAuthorSelected(object sender, GestureEventArgs e)
        {
            if (this.Author == null || this.AuthorTapped == null)
            {
                return;
            }

            this.AuthorTapped(this, EventArgs.Empty);
        }
    }
}
