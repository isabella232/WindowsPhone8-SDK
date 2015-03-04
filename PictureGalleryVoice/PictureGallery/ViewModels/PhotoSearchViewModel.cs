using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Windows;

namespace PictureGallery.ViewModels
{
    [DataContract]
    public class PhotoSearchViewModel : SearchViewModel
    {
        private static readonly DataTemplate visualization = Application.Current.Resources["FavoriteSearchTemplate"] as DataTemplate;
        private PhotoListViewModel photoList;

        [DataMember]
        public PhotoListViewModel PhotoList
        {
            get
            {
                if (this.photoList == null)
                {
                    this.photoList = App.Instance.CurrentImageServiceProvider.CreateSearchPhotoList(this.Text);
                }

                return this.photoList;
            }

            set
            {
                if (this.photoList == value)
                {
                    return;
                }

                this.photoList = value;
                this.OnPropertyChanged("PhotoList");
            }
        }
        
        public override System.Uri PageUri
        {
            get
            {
                return App.SearchPage;
            }
        }

        public override DataTemplate FavoritesTemplate
        {
            get
            {
                return visualization;
            }
        }
        
        protected override void OnTextChanged()
        {
            if (string.IsNullOrEmpty(this.Text))
            {
                this.PhotoList = null;
                this.Loading = false;
                return;
            }

            this.PhotoList = App.Instance.CurrentImageServiceProvider.CreateSearchPhotoList(this.Text);
        }

        protected override SearchViewModel CreateInstance()
        {
            return new PhotoSearchViewModel() { PhotoList = this.PhotoList.ActualViewModel as PhotoListViewModel };
        }
    }
}
