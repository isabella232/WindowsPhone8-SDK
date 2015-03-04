using System;

namespace PictureGallery.ViewModels
{
    public interface IPhotoDownloader
    {
        void DownloadPhotos(int page, int perPage, Action<PhotoListViewModel> photosDownloaded);
    }
}