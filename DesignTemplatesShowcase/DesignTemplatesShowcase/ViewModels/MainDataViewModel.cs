using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Telerik.Windows.Controls;
using Telerik.Windows.Data;

namespace Telerik.DesignTemplates.WP.ViewModels
{
    public class MainDataViewModel : ViewModelBase
    {
        private ObservableCollection<DataItemViewModel> items;
        private LoopingListDataSource loopingListDataSource;
        private List<DataDescriptor> groupDescriptors;
        private Uri titleImage;

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
        /// A collection for <see cref="LoopingListItemViewModel"/> objects.
        /// </summary>
        public LoopingListDataSource LoopingListDataSource
        {
            get
            {
                if (this.loopingListDataSource == null)
                {
                    this.InitializeLoopingListDataSource();
                }
                return this.loopingListDataSource;
            }
            private set
            {
                this.loopingListDataSource = value;
            }
        }

        /// <summary>
        /// Gets the group descriptors.
        /// </summary>
        public List<DataDescriptor> GroupDescriptors
        {
            get
            {
                if (this.groupDescriptors == null)
                {
                    this.InitializeGroupDescriptors();
                }
                return this.groupDescriptors;
            }
            private set
            {
                this.groupDescriptors = value;
            }
        }

        /// <summary>
        /// Gets the title image.
        /// </summary>
        public Uri TitleImage
        {
            get
            {
                if (this.titleImage == null)
                {
                    this.InitializeTitleImage();
                }
                return this.titleImage;
            }
            private set
            {
                this.titleImage = value;
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
                this.items.Add(new DataItemViewModel()
                {
                    ImageSource = new Uri((string)App.Current.Resources["FrameSource"], UriKind.RelativeOrAbsolute),
                    ImageThumbnailSource = new Uri((string)App.Current.Resources["FrameThumbnailSource"], UriKind.RelativeOrAbsolute),
                    Title = string.Format("Title {0}", i),
                    Information = string.Format("Information {0}", i),
                    Group = (i % 2 == 0) ? "EVEN" : "ODD"
                });
            }
        }

        /// <summary>
        /// Initializes the looping list data source that will be used in RadLoopingList.
        /// </summary>
        private void InitializeLoopingListDataSource()
        {
            this.loopingListDataSource = new LoopingListDataSource(10);
            this.loopingListDataSource.ItemNeeded += new EventHandler<LoopingListDataItemEventArgs>(this.DataSource_ItemNeeded);
            this.loopingListDataSource.ItemUpdated += new EventHandler<LoopingListDataItemEventArgs>(this.DataSource_ItemUpdated);
        }

        /// <summary>
        /// Initializes the group descriptors that will be used for grouping in RadJumpList.
        /// </summary>
        private void InitializeGroupDescriptors()
        {
            this.groupDescriptors = new List<DataDescriptor>();
            var groupDescriptor = new GenericGroupDescriptor<DataItemViewModel, string>(item => item.Group);
            this.groupDescriptors.Add(groupDescriptor);
        }

        /// <summary>
        /// Handles the ItemNeeded event of the DataSource control. The ItemNeeded event is raised whenever a data item instance is needed.
        /// </summary>
        private void DataSource_ItemNeeded(object sender, LoopingListDataItemEventArgs e)
        {
            e.Item = new LoopingListItemViewModel()
            {
                ImageThumbnailSource = new Uri((string)App.Current.Resources["FrameThumbnailSource"], UriKind.RelativeOrAbsolute),
            };
        }

        /// <summary>
        /// Handles the ItemUpdated event of the DataSource control. The ItemUpdated event is raised whenever a data item instance needs to be updated with new content.
        /// </summary>
        private void DataSource_ItemUpdated(object sender, LoopingListDataItemEventArgs e)
        {
            (e.Item as LoopingListItemViewModel).ImageThumbnailSource = new Uri((string)App.Current.Resources["FrameThumbnailSource"], UriKind.RelativeOrAbsolute);
        }

        private void InitializeTitleImage()
        {
            this.titleImage = new Uri((string)App.Current.Resources["TitleImageSource"], UriKind.RelativeOrAbsolute);
        }
    }
}
