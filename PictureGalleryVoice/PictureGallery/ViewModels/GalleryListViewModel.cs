using System.Collections.ObjectModel;
using System.Runtime.Serialization;

namespace PictureGallery.ViewModels
{
    [DataContract]
    public abstract class AsynchronousViewModel : ImageServiceViewModel
    {
        private bool loading = false;

        public bool Loading
        {
            get
            {
                return this.loading;
            }

            protected set
            {
                if (this.loading == value)
                {
                    return;
                }

                this.loading = value;
                this.OnPropertyChanged("Loading");
            }
        }
    }

    [DataContract]
    public class GalleryListViewModel : AsynchronousViewModel
    {
        private ObservableCollection<GalleryViewModel> galleries;

        [DataMember]
        public string AuthorId
        {
            get;
            set;
        }

        public ObservableCollection<GalleryViewModel> Galleries
        {
            get
            {
                if (this.galleries == null && !this.Loading)
                {
                    this.Load();
                }

                return this.galleries;
            }

            set
            {
                if (this.galleries == value)
                {
                    return;
                }

                this.galleries = value;
                this.OnPropertyChanged("Galleries");
            }
        }

        private void Load()
        {
            this.Loading = true;
            App.Instance.CurrentImageServiceProvider.GetGalleriesForAuthor(this.AuthorId, (galleries) => 
                {
                    this.Galleries = new ObservableCollection<GalleryViewModel>(galleries);
                    this.Loading = false;
                });
        }
    }
}
