using System;
using System.Linq;
using PictureGallery.ViewModels;

namespace PictureGallery.Views
{
    public class PhotoSelectedEventArgs : EventArgs
    {
        public PhotoSelectedEventArgs(PhotoViewModel photo)
        {
            this.Photo = photo;
        }

        public PhotoViewModel Photo { get; private set; }
    }
}