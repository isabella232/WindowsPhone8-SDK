using System;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Windows;

namespace PictureGallery.ViewModels
{
    [DataContract]
    public abstract class ImageServiceViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public virtual DataTemplate FavoritesTemplate
        {
            get
            {
                return null;
            }
        }

        public virtual Uri PageUri
        {
            get
            {
                return new Uri("");
            }
        }

        public virtual ImageServiceViewModel ActualViewModel
        {
            get
            {
                return this;
            }
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged == null)
            {
                return;
            }

            this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}