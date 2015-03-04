using System;
using Telerik.Windows.Controls;

namespace Telerik.DesignTemplates.WP.ViewModels
{
    public class LoopingListItemViewModel : LoopingListDataItem
    {
        private Uri imageThumbnailSource;
        private string title;
        private string information;

        /// <summary>
        /// Gets or sets the image thumbnail source.
        /// </summary>
        public Uri ImageThumbnailSource
        {
            get
            {
                return this.imageThumbnailSource;
            }
            set
            {
                if (this.imageThumbnailSource != value)
                {
                    this.imageThumbnailSource = value;
                    this.OnPropertyChanged("ImageThumbnailSource");
                }
            }
        }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        public string Title
        {
            get
            {
                return this.title;
            }
            set
            {
                if (this.title != value)
                {
                    this.title = value;
                    this.OnPropertyChanged("Title");
                }
            }
        }

        /// <summary>
        /// Gets or sets the information.
        /// </summary>
        public string Information
        {
            get
            {
                return this.information;
            }
            set
            {
                if (this.information != value)
                {
                    this.information = value;
                    this.OnPropertyChanged("Information");
                }
            }
        }
    }
}