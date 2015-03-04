using System.Windows.Controls;
using System.Windows.Input;
using System.Windows;
using PictureGallery.ViewModels;
using System;

namespace PictureGallery.Views
{
    public partial class AuthorView : UserControl
    {
        public static readonly DependencyProperty AuthorProperty =
            DependencyProperty.Register("Author", typeof(AuthorViewModel), typeof(AuthorView), new PropertyMetadata(null));

        public event EventHandler AuthorTapped;

        public AuthorView()
        {
            InitializeComponent();
        }

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
