using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;
using System;
using System.Windows;
using System.Collections;

namespace PictureGallery.ViewModels
{
    [DataContract]
    [KnownType(typeof(TagPhotoDownloader))]
    [KnownType(typeof(AuthorPhotoDownloader))]
    [KnownType(typeof(SearchPhotoDownloader))]
    [KnownType(typeof(GalleryPhotoDownloader))]
    [KnownType(typeof(InterestingPhotoDownloader))]
    public class PhotoListViewModel : AsynchronousViewModel
    {
        private static readonly DataTemplate visualization = Application.Current.Resources["FavoritePhotoTemplate"] as DataTemplate;
        private PhotoViewModel currentPhoto = null;
        private int totalPhotos;
        private bool canDownloadMore = true;
        private ObservableCollection<PhotoViewModel> photos;

        public IEnumerable Thumbnails
        {
            get
            {
                if (this.Photos == null)
                {
                    yield break;
                }

                foreach (PhotoViewModel photo in this.Photos)
                {
                    yield return photo.Thumbnail;
                }
            }
        }

        public IEnumerable SmallSizes
        {
            get
            {
                if (this.Photos == null)
                {
                    yield break;
                }

                foreach (PhotoViewModel photo in this.Photos)
                {
                    yield return photo.Sizes[0];
                }
            }
        }

        public IEnumerable MediumSizes
        {
            get
            {
                if (this.Photos == null)
                {
                    yield break;
                }

                foreach (PhotoViewModel photo in this.Photos)
                {
                    yield return photo.Sizes[1];
                }
            }
        }

        public IEnumerable LargeSizes
        {
            get
            {
                if (this.Photos == null)
                {
                    yield break;
                }

                foreach (PhotoViewModel photo in this.Photos)
                {
                    yield return photo.Sizes[2];
                }
            }
        }

        [DataMember]
        public ObservableCollection<PhotoViewModel> Photos
        {
            get
            {
                if (this.photos == null && !this.Loading)
                {
                    this.Load();
                }

                return this.photos;
            }

            set
            {
                if (this.photos == value)
                {
                    return;
                }

                this.photos = value;
                this.OnPropertyChanged("Photos");
            }
        }

        [DataMember]
        public int TotalPhotos
        {
            get
            {
                return this.totalPhotos;
            }

            set
            {
                if (this.totalPhotos == value)
                {
                    return;
                }

                this.totalPhotos = value;
                this.OnPropertyChanged("TotalPhotos");
            }
        }
        
        [DataMember]
        public int LoadedPages
        {
            get;
            set;
        }

        [DataMember]
        public IPhotoDownloader PhotoDownloader
        {
            get;
            set;
        }

        public bool CanDownloadMore
        {
            get
            {
                return this.canDownloadMore;
            }

            set
            {
                if (this.canDownloadMore == value)
                {
                    return;
                }

                this.canDownloadMore = value;
                this.OnPropertyChanged("CanDownloadMore");
            }
        }

        [DataMember]
        public PhotoViewModel CurrentPhoto
        {
            get
            {
                return this.currentPhoto;
            }

            set
            {
                if (this.currentPhoto == value)
                {
                    return;
                }

                this.currentPhoto = value;

                if (this.currentPhoto == null)
                {
                    return;
                }

                this.OnPropertyChanged("CurrentPhoto");
            }
        }

        public override Uri PageUri
        {
            get
            {
                return App.PhotoPage;
            }
        }

        public override DataTemplate FavoritesTemplate
        {
            get
            {
                return visualization;
            }
        }

        public override ImageServiceViewModel ActualViewModel
        {
            get
            {
                return new PhotoListViewModel() 
                { 
                    Photos = this.Photos, 
                    CurrentPhoto = this.CurrentPhoto,
                    PhotoDownloader = this.PhotoDownloader,
                    TotalPhotos = this.TotalPhotos,
                    LoadedPages = this.LoadedPages
                };
            }
        }

        public void LoadMore()
        {
            this.Loading = true;
            this.PhotoDownloader.DownloadPhotos(this.LoadedPages, 12, this.AddPhotos);
        }

        private void AddPhotos(PhotoListViewModel photos)
        {
            if (photos.Photos.Count > 0)
            {
                this.LoadedPages++;
                this.CanDownloadMore = true;
            }
            else
            {
                this.CanDownloadMore = false;
            }

            if (this.photos == null)
            {
                this.Photos = new ObservableCollection<PhotoViewModel>(photos.photos);
                this.TotalPhotos = photos.TotalPhotos;
            }
            else
            {
                foreach (PhotoViewModel photo in photos.photos)
                {
                    this.Photos.Add(photo);
                }
            }

            this.Loading = false;

            this.OnPropertyChanged("Thumbnails");
            this.OnPropertyChanged("SmallSizes");
            this.OnPropertyChanged("MediumSizes");
            this.OnPropertyChanged("LargeSizes");
        }

        private Collection<object> CreateThumbnails()
        {
            if (this.Photos == null || this.Photos.Count == 0)
            {
                return null;
            }

            Collection<object> urls = new Collection<object>();
            foreach (PhotoViewModel photo in this.Photos)
            {
                urls.Add(photo.Thumbnail);
            }

            return urls;
        }

        private void Load()
        {
            this.Loading = true;
            this.LoadedPages = 1;
            this.PhotoDownloader.DownloadPhotos(this.LoadedPages, 11, this.AddPhotos);
        }
    }
}
