using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace PictureGallery.ViewModels
{
    public interface IPhotoDownloader
    {
        void DownloadPhotos(int page, int perPage, Action<PhotoListViewModel> photosDownloaded);
    }
}
