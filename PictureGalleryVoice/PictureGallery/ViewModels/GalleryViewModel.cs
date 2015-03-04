using System;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;
using System.Windows;

namespace PictureGallery.ViewModels
{
    [DataContract]
    public class GalleryViewModel : ImageServiceViewModel
    {
        private static DataTemplate visualization = Application.Current.Resources["FavoriteGalleryTemplate"] as DataTemplate;
        private PhotoListViewModel photoList;
        private AuthorViewModel author;

        public GalleryViewModel(string galleryId, string title, string description, string ownerId)
        {
            this.GalleryId = galleryId;
            this.Title = title;
            this.Description = description;
            this.OwnerId = ownerId;
        }

        public override Uri PageUri
        {
            get
            {
                return App.GalleryPage;
            }
        }

        [DataMember]
        public string GalleryId
        {
            get;
            set;
        }
        
        [DataMember]
        public string Title
        {
            get;
            set;
        }

        [DataMember]
        public string Description
        {
            get;
            set;
        }

        [DataMember]
        public string OwnerId
        {
            get;
            set;
        }

        [DataMember]
        public AuthorViewModel Author
        {
            get
            {
                if (this.author == null)
                {
                    App.Instance.CurrentImageServiceProvider.GetAuthor(this.OwnerId, this.OnAuthorDownloaded);
                }

                return this.author;
            }

            set
            {
                if (this.author == value)
                {
                    return;
                }

                this.author = value;
                this.OnPropertyChanged("Author");
            }
        }

        [DataMember]
        public PhotoListViewModel PhotoList
        {
            get
            {
                if (this.photoList == null)
                {
                    App.Instance.CurrentImageServiceProvider.GetImagesForGallery(this.GalleryId, this.OnPhotosDownloaded);
                }

                return this.photoList;
            }

            set
            {
                if (this.photoList == value)
                {
                    return;
                }

                this.photoList = value;
                this.OnPropertyChanged("PhotoList");
            }
        }

        public override DataTemplate FavoritesTemplate
        {
            get 
            {
                return visualization;
            }
        }

        private void OnAuthorDownloaded(AuthorViewModel author)
        {
            this.Author = author;
        }

        private void OnPhotosDownloaded(PhotoListViewModel list)
        {
            this.PhotoList = list;
        }

        public override bool Equals(object obj)
        {
            GalleryViewModel gallery = obj as GalleryViewModel;
            if (gallery != null)
            {
                return gallery.GalleryId == this.GalleryId;
            }

            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return this.GalleryId.GetHashCode();
        }
    }
}
