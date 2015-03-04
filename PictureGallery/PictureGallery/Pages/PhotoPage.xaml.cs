using System.Windows;
using System.Windows.Input;
using System.Windows.Navigation;
using PictureGallery.ViewModels;

namespace PictureGallery.Pages
{
    public partial class PhotoPage : PageBase
    {
        public PhotoPage()
        {
            this.InitializeComponent();
            var list = App.Instance.GetCurrentViewModel<PhotoListViewModel>();
            this.DataContext = list;
            this.Loaded += this.OnLoaded;
        }

        public override bool HasPreviousImage
        {
            get
            {
                return this.slideView.PreviousItem != null;
            }
        }

        public override bool HasNextImage
        {
            get
            {
                return this.slideView.NextItem != null;
            }
        }

        protected override ImageServiceViewModel FavoriteViewModel
        {
            get
            {
                return this.PhotoList.ActualViewModel;
            }
        }

        private PhotoListViewModel PhotoList
        {
            get
            {
                return this.DataContext as PhotoListViewModel;
            }
        }

        public override bool CompareViewModel(ImageServiceViewModel viewModel)
        {
            var list = viewModel as PhotoListViewModel;
            if (list == null)
            {
                return false;
            }

            return list.CurrentPhoto.Equals(this.PhotoList.CurrentPhoto);
        }

        public override void PreviousImage()
        {
            this.slideView.SelectedItem = this.slideView.PreviousItem;
        }

        public override void NextImage()
        {
            this.slideView.SelectedItem = this.slideView.NextItem;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            this.PhotoList.PropertyChanged += this.OnListPropertyChanged;
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);
            this.PhotoList.PropertyChanged -= this.OnListPropertyChanged;
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            App.Instance.UpdateNavigationButtons(this);
        }

        private void OnListPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "CurrentPhoto")
            {
                App.Instance.UpdatePinButton();
                App.Instance.UpdateNavigationButtons();
            }
        }

        private void OnPhotoTap(object sender, GestureEventArgs e)
        {
            App.Instance.SetCurrentViewModel<PhotoListViewModel>(this.PhotoList);
            this.NavigationService.Navigate(App.PanZoomPage);
        }

        private void OnAuthorSelected(object sender, GestureEventArgs e)
        {
            App.Instance.SetCurrentViewModel<AuthorViewModel>(this.PhotoList.CurrentPhoto.Author);
            this.NavigationService.Navigate(App.AuthorPage);
        }
    }
}