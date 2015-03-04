using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace PictureGallery.ViewModels
{
    [DataContract]
    public class GalleryPhotoDownloader : IPhotoDownloader
    {
        [DataMember]
        public string GalleryId
        {
            get;
            set;
        }

        public void DownloadPhotos(int page, int perPage, Action<PhotoListViewModel> photosDownloaded)
        {
            App.Instance.CurrentImageServiceProvider.GetImagesForGallery(this.GalleryId, (photoList) =>
                {
                    photosDownloaded(photoList);
                });
        }
    }
}
