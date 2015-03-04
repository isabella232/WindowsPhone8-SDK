using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace PictureGallery.ViewModels
{
    public class TagListViewModel : AsynchronousViewModel
    {
        private readonly Random randomGenerator = new Random();
        private ObservableCollection<TagViewModel> tags;

        public ObservableCollection<TagViewModel> Tags
        {
            get
            {
                if (this.tags == null && !this.Loading)
                {
                    this.Loading = true;
                    App.Instance.CurrentImageServiceProvider.GetRecentTags(this.OnTagsDownloaded);
                }

                return this.tags;
            }

            set
            {
                if (this.tags == value)
                {
                    return;
                }

                this.tags = value;
                this.OnPropertyChanged("Tags");
            }
        }

        private void OnTagsDownloaded(IEnumerable<TagViewModel> tags)
        {
            var temp = new ObservableCollection<TagViewModel>(tags);

            double max = 0;
            double min = 0;
            foreach (TagViewModel tag in temp)
            {
                max = Math.Max(max, (double)tag.RawScore);
                min = Math.Min(max, (double)tag.RawScore);
            }

            double diff = 1;
            if (max != min)
            {
                diff = Math.Abs(max - min);
            }

            foreach (TagViewModel tag in temp)
            {
                tag.ScaledScore = (tag.RawScore - min) / diff;
            }

            this.ShuffleTags(temp);
            this.Tags = temp;

            this.Loading = false;
        }

        private void ShuffleTags(ObservableCollection<TagViewModel> tags)
        {
            for (var i = 0; i < tags.Count; ++i)
            {
                var randomIndex = this.randomGenerator.Next(0, tags.Count);

                var tmp = tags[i];
                tags[i] = tags[randomIndex];
                tags[randomIndex] = tmp;
            }
        }
    }
}