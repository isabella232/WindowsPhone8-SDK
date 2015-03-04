using System.Windows.Navigation;
using PictureGallery.ViewModels;
using PictureGallery.Views;
using Telerik.Windows.Controls;

namespace PictureGallery.Pages
{
    public partial class TagPage : PageBase
    {
        public TagPage()
        {
            InitializeComponent();
            RadContinuumAnimation.SetContinuumElement(this, this);
            this.DataContext = App.Instance.GetCurrentViewModel<TagViewModel>();
        }

        private TagViewModel TagViewModel
        {
            get
            {
                return this.DataContext as TagViewModel;
            }
        }

        protected override ImageServiceViewModel FavoriteViewModel
        {
            get 
            {
                return this.TagViewModel;
            }
        }

        public override bool CompareViewModel(ImageServiceViewModel viewModel)
        {
            TagViewModel tag = viewModel as TagViewModel;
            if (tag == null)
            {
                return false;
            }

            return tag.Tag == this.TagViewModel.Tag;
        }

        private void OnPhotoTapped(object sender, System.EventArgs e)
        {
            App.Instance.SetCurrentViewModel<PhotoListViewModel>(this.photoView.PhotoList as PhotoListViewModel);
            NavigationService.Navigate(App.PhotoPage);
        }
    }
}