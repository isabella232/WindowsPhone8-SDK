using System.Windows;
using System.Runtime.Serialization;

namespace PictureGallery.ViewModels
{
    [DataContract]
    public class AuthorViewModel : ImageServiceViewModel
    {
        private static readonly DataTemplate visualization = Application.Current.Resources["FavoriteAuthorTemplate"] as DataTemplate;

        public AuthorViewModel(string id, string name, string picture, int photoCount)
        {
            this.Name = name;
            this.Picture = picture;
            this.Id = id;
            this.PhotoCount = photoCount;
        }

        public override DataTemplate FavoritesTemplate
        {
            get
            {
                return visualization;
            }
        }

        public override System.Uri PageUri
        {
            get 
            {
                return App.AuthorPage;
            }
        }

        [DataMember]
        public string Id
        {
            get;
            set;
        }

        [DataMember]
        public int PhotoCount
        {
            get;
            set;
        }

        [DataMember]
        public string Name
        {
            get;
            set;
        }

        [DataMember]
        public string Picture
        {
            get;
            set;
        }
    }
}
