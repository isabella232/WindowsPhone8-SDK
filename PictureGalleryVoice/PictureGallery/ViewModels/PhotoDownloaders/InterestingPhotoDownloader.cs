using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Runtime.Serialization;
using System.Collections.Generic;

namespace PictureGallery.ViewModels
{
    [DataContract]
    public class InterestingPhotoDownloader : IPhotoDownloader
    {
        [DataMember]
        public DateTime BeginDate
        {
            get;
            set;
        }

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
