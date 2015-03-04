using System.Runtime.Serialization;

namespace PictureGallery.ViewModels
{
    [DataContract]
    public abstract class SearchViewModel : AsynchronousViewModel
    {
        private string text;

        [DataMember]
        public string Text
        {
            get
            {
                return this.text;
            }

            set
            {
                if (this.text == value)
                {
                    return;
                }

                this.text = value;
                this.OnPropertyChanged("Text");
                this.OnTextChanged();
            }
        }

        public override ImageServiceViewModel ActualViewModel
        {
            get
            {
                var result = this.CreateInstance();
                result.Text = this.Text;
                return result;
            }
        }

        protected abstract void OnTextChanged();

        protected abstract SearchViewModel CreateInstance();
    }
}