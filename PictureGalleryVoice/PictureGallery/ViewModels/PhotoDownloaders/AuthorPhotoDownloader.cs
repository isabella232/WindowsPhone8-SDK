using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace PictureGallery.ViewModels
{
    [DataContract]
    public class AuthorPhotoDownloader : IPhotoDownloader
    {
        [DataMember]
        public string AuthorId
        {
            get;
            set;
        }

        public void DownloadPhotos(int page, int perPage, Action<PhotoListViewModel> photosDownloaded)
        {
            App.Instance.CurrentImageServiceProvider.GetImagesForAuthor(this.AuthorId, (photoList) =>
            {
                photosDownloaded(photoList);
            },
            page, 
            perPage);
        }
    }
}
