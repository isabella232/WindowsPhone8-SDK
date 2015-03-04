using System;
using System.Collections.ObjectModel;

namespace Telerik.DesignTemplates.WP.ViewModels
{
    public class DifferentImageSizeViewModel : ViewModelBase
    {
        private ObservableCollection<DataItemViewModel> items;

        /// <summary>
        /// A collection for <see cref="DataItemViewModel"/> objects.
        /// </summary>
        public ObservableCollection<DataItemViewModel> Items
        {
            get
            {
                if (this.items == null)
                {
                    this.InitializeItems();
                }
                return this.items;
            }
            private set
            {
                this.items = value;
            }
        }

        /// <summary>
        /// Initializes the items.
        /// </summary>
        private void InitializeItems()
        {
            this.items = new ObservableCollection<DataItemViewModel>();
            for (var i = 1; i <= 7; i++)
            {
                var newItem = new DataItemViewModel();
                newItem.ImageSource = (i % 2 == 1) ? new Uri((string)App.Current.Resources["FrameSource"], UriKind.RelativeOrAbsolute) :
                                      new Uri((string)App.Current.Resources["FrameVerticalSource"], UriKind.RelativeOrAbsolute);
                newItem.ImageThumbnailSource = new Uri((string)App.Current.Resources["FrameThumbnailSource"], UriKind.RelativeOrAbsolute);
                newItem.Title = string.Format("Title {0}", i);
                newItem.Information = string.Format("Information {0}", i);
                newItem.Group = (i % 2 == 0) ? "EVEN" : "ODD";
                this.items.Add(newItem);
            }
        }
    }
}