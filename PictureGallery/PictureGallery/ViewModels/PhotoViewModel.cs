using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Windows;

namespace PictureGallery.ViewModels
{
    [DataContract]
    public class PhotoViewModel : ImageServiceViewModel
    {
        private static readonly DataTemplate visualization = Application.Current.Resources["FavoritePhotoTemplate"] as DataTemplate;
        private AuthorViewModel author;
        private IEnumerable<CommentViewModel> comments;
        private bool downloadingComments;
        private bool downloadingAuthor;

        public PhotoViewModel(string title, string photoId, string authorId, string thumbnail, string original, Collection<string> sizes, Collection<string> tags)
        {
            this.Title = title;
            this.AuthorId = authorId;
            this.PhotoId = photoId;
            this.Tags = tags;
            this.Thumbnail = thumbnail;
            this.Sizes = sizes;
        }

        public AuthorViewModel Author
        {
            get
            {
                if (this.author == null && !this.downloadingAuthor)
                {
                    this.downloadingAuthor = true;
                    App.Instance.CurrentImageServiceProvider.GetAuthorOfPhoto(this.PhotoId, this.OnAuthorDownloaded);
                }

                return this.author;
            }

            set
            {
                if (this.author == value)
                {
                    return;
                }

                this.author = value;
                this.OnPropertyChanged("Author");
            }
        }

        public IEnumerable<CommentViewModel> Comments
        {
            get
            {
                if (this.comments == null && !this.downloadingComments)
                {
                    this.downloadingComments = true;
                    App.Instance.CurrentImageServiceProvider.GetComments(this.PhotoId, this.OnCommentsDownloaded);
                }

                return this.comments;
            }
        }

        [DataMember]
        public string PhotoId { get; set; }

        [DataMember]
        public string Title { get; set; }

        [DataMember]
        public string AuthorId { get; set; }

        [DataMember]
        public string Thumbnail { get; set; }

        [DataMember]
        public Collection<string> Tags { get; set; }

        [DataMember]
        public Collection<string> Sizes { get; set; }

        public override System.Uri PageUri
        {
            get 
            {
                return App.PhotoPage;
            }
        }

        public override DataTemplate FavoritesTemplate
        {
            get 
            {
                return visualization;
            }
        }

        public override bool Equals(object obj)
        {
            var model = obj as PhotoViewModel;
            if (model != null)
            {
                return model.PhotoId == this.PhotoId;
            }

            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return this.PhotoId.GetHashCode();
        }

        private void OnAuthorDownloaded(AuthorViewModel author)
        {
            this.Author = author;
            this.downloadingAuthor = false;
        }

        private void OnCommentsDownloaded(IEnumerable<CommentViewModel> comments)
        {
            this.comments = from comment in comments orderby comment.Date descending select comment;
            this.downloadingComments = false;
            this.OnPropertyChanged("Comments");
        }
    }
}
