using System;
using System.Runtime.Serialization;
using System.Windows;

namespace PictureGallery.ViewModels
{
    [DataContract]
    public class CommentViewModel : ImageServiceViewModel
    {
        public CommentViewModel(string userName, string userId, string userPicture, string text, DateTime date)
        {
            this.UserName = userName;
            this.UserPicture = userPicture;
            this.Text = text;
            this.Date = date;
            this.UserId = userId;
        }

        [DataMember]
        public string UserName
        {
            get;
            set;
        }

        [DataMember]
        public string UserId
        {
            get;
            set;
        }

        [DataMember]
        public string UserPicture
        {
            get;
            set;
        }

        [DataMember]
        public string Text
        {
            get;
            set;
        }

        [DataMember]
        public DateTime Date
        {
            get;
            set;
        }

        public override Uri PageUri
        {
            get 
            { 
                throw new NotSupportedException();
            }
        }

        public override DataTemplate FavoritesTemplate
        {
            get 
            {
                return null;
            }
        }
    }
}
