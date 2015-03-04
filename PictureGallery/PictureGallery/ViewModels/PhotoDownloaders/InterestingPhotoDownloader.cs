using System;
using System.Runtime.Serialization;

namespace PictureGallery.ViewModels
{
    [DataContract]
    public class InterestingPhotoDownloader : IPhotoDownloader
    {
        [DataMember]
        public DateTime BeginDate { get; set; }

        public void DownloadPhotos(int page, int perPage, Action<PhotoListViewModel> photosDownloaded)
        {
            App.Instance.CurrentImageServiceProvider.GetRecentImages(this.BeginDate, (photoList) =>
            {
                photosDownloaded(photoList);
            },
                page,
                perPage);
        }
    }
}