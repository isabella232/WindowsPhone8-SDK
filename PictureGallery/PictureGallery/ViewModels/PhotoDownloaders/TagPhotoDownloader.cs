using System;
using System.Runtime.Serialization;

namespace PictureGallery.ViewModels
{
    [DataContract]
    public class TagPhotoDownloader : IPhotoDownloader
    {
        [DataMember]
        public string Tag { get; set; }

        public void DownloadPhotos(int page, int perPage, Action<PhotoListViewModel> photosDownloaded)
        {
            App.Instance.CurrentImageServiceProvider.GetImagesForTag(this.Tag, (photoList) =>
            {
                photosDownloaded(photoList);
            },
                page,
                perPage);
        }
    }
}