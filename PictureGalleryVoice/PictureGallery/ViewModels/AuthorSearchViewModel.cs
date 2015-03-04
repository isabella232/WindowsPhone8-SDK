using System.Runtime.Serialization;

namespace PictureGallery.ViewModels
{
    [DataContract]
    public class AuthorSearchViewModel : SearchViewModel
    {
        private AuthorViewModel author;

        [DataMember]
        public AuthorViewModel Author
        {
            get
            {
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

        protected override void OnTextChanged()
        {
            if (string.IsNullOrEmpty(this.Text))
            {
                this.Author = null;
                return;
            }

            this.Author = null;

            this.Loading = true;
            App.Instance.CurrentImageServiceProvider.SearchAuthors(this.Text, (author) => 
                {
                    this.Author = author;
                    this.Loading = false;
                });
        }

        protected override SearchViewModel CreateInstance()
        {
            return new AuthorSearchViewModel() { Author = this.Author };
        }
    }
}
