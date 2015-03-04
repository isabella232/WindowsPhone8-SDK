using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PictureGallery.ViewModels;

namespace PictureGallery.Views
{
    public class PhotoSelectedEventArgs : EventArgs
    {
        public PhotoSelectedEventArgs(PhotoViewModel photo)
        {
            this.Photo = photo;
        }

        public PhotoViewModel Photo
        {
            get;
            private set;
        }
    }
}
