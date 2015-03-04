using System;
using System.Runtime.Serialization;

namespace PictureGallery.ViewModels
{
    [DataContract]
    public class SearchPhotoDownloader : IPhotoDownloader
    {
        [DataMember]
        public string Text
        {
            get;
            set;
        }

        public void DownloadPhotos(int page, int perPage, Action<PhotoListViewModel> photosDownloaded)
        {
            App.Instance.CurrentImageServiceProvider.SearchImages(this.Text, (photoList) =>
            {
                photosDownloaded(photoList);
            },
            page,
            perPage);
        }
    }
}
