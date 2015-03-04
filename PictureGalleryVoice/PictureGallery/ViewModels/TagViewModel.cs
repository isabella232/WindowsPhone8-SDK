using System.Windows;
using System.Runtime.Serialization;
using System.Collections.Generic;
using System;

namespace PictureGallery.ViewModels
{
    [DataContract]
    public class TagViewModel : ImageServiceViewModel
    {
        private static readonly DataTemplate visualization = Application.Current.Resources["FavoriteTagTemplate"] as DataTemplate;
        private string tag;
        private PhotoListViewModel photoList;
        private double scaledScore;

        public TagViewModel(string tag, int photoCount)
        {
            this.Tag = tag;
            this.RawScore = photoCount;
        }

        [DataMember]
        public PhotoListViewModel PhotoList
        {
            get
            {
                if (this.photoList == null)
                {
                    this.photoList = App.Instance.CurrentImageServiceProvider.CreateTagPhotoList(this.tag);
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

        [DataMember]
        public string Tag
        {
            get
            {
                return this.tag;
            }

            set
            {
                if (this.tag == value)
                {
                    return;
                }

                this.tag = value;
                this.OnPropertyChanged("Tag");
            }
        }

        public override System.Uri PageUri
        {
            get 
            {
                return App.TagPage;
            }
        }

        [DataMember]
        public double RawScore
        {
            get;
            set;
        }

        [DataMember]
        public double ScaledScore
        {
            get
            {
                return this.scaledScore;
            }

            set
            {
                this.scaledScore = value;
            }
        }

        public override DataTemplate FavoritesTemplate
        {
            get 
            {
                return visualization;
            }
        }
    }
}
