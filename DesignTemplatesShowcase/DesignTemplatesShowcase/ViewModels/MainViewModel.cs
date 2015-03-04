using System;
using System.Collections.Generic;
using System.Windows;
using System.Xml.Linq;
using Microsoft.Phone.Shell;
using Telerik.DesignTemplates.WP.Pages;
using Telerik.Windows.Controls;
using Telerik.Windows.Data;

namespace Telerik.DesignTemplates.WP.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        /// <summary>
        /// The name of the category ContentViews.
        /// </summary>
        public const string ContentViewsCategoryName = "ContentViews";

        /// <summary>
        /// The name of the category Pages.
        /// </summary>
        public const string PagesCategoryName = "Pages";

        /// <summary>
        /// The name of the category BuildingBlocks.
        /// </summary>
        public const string BuildingBlocksCategoryName = "BuildingBlocks";

        /// <summary>
        /// The name that shows for new templates.
        /// </summary>
        public const string IsNewTagName = "NEW";

        private static MainViewModel instance;
        private List<TemplateViewModel> allTemplates;
        private List<TagInfo> contentViewsTagInformation;
        private List<TagInfo> buildingBlocksTagInformation;
        private List<TagInfo> pagesTagInformation;
        private List<DataDescriptor> groupDescriptors;
        private TagInfo selectedTag;
        private TemplateViewModel selectedTemplate;
        private List<TemplateViewModel> selectedTagTemplates;
        private MainDataViewModel dataViewModel;
        private TemplatePage templatePage;
        private List<TemplateViewModel> newTemplates;
        private List<TagInfo> allTags;

        /// <summary>
        /// Initializes a new instance of the <see cref="MainViewModel"/> class.
        /// </summary>
        public MainViewModel()
        {
            this.allTemplates = new List<TemplateViewModel>();
            this.contentViewsTagInformation = new List<TagInfo>();
            this.buildingBlocksTagInformation = new List<TagInfo>();
            this.pagesTagInformation = new List<TagInfo>();
            this.newTemplates = new List<TemplateViewModel>();
            this.selectedTagTemplates = new List<TemplateViewModel>();
        }

        /// <summary>
        /// Gets the instance.
        /// </summary>
        public static MainViewModel Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new MainViewModel();
                }

                return instance;
            }
        }

        /// <summary>
        /// Gets the view model filled with sample data.
        /// </summary>
        public MainDataViewModel DataViewModel
        {
            get
            {
                if (this.dataViewModel == null)
                {
                    this.dataViewModel = new MainDataViewModel();
                }
                return this.dataViewModel;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is data loaded.
        /// </summary>
        public bool IsDataLoaded { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is dark theme.
        /// </summary>
        public bool IsDarkTheme { get; set; }

        /// <summary>
        /// Gets or sets the main frame.
        /// </summary>
        public RadPhoneApplicationFrame MainFrame { get; set; }

        /// <summary>
        /// Gets or sets the template page.
        /// </summary>
        public TemplatePage TemplatePage
        {
            get
            {
                return this.templatePage;
            }
            set
            {
                this.templatePage = value;
            }
        }

        /// <summary>
        /// Gets or sets the selected tag.
        /// </summary>
        public TagInfo SelectedTag
        {
            get
            {
                return this.selectedTag;
            }
            set
            {
                this.selectedTag = value;
                this.UpdateSelectedTagTemplates();
            }
        }

        /// <summary>
        /// Gets or sets the selected template.
        /// </summary>
        public TemplateViewModel SelectedTemplate
        {
            get
            {
                return this.selectedTemplate;
            }
            set
            {
                this.selectedTemplate = value;
                this.OnPropertyChanged("SelectedTemplate");
            }
        }

        /// <summary>
        /// Gets all templates with the selected tag.
        /// </summary>
        public List<TemplateViewModel> SelectedTagTemplates
        {
            get
            {
                return this.selectedTagTemplates;
            }
        }

        /// <summary>
        /// Gets all templates.
        /// </summary>
        public List<TemplateViewModel> AllTemplates
        {
            get
            {
                return this.allTemplates;
            }
        }

        /// <summary>
        /// Gets all tags from the category ContentViews.
        /// </summary>
        public List<TagInfo> ContentViewsTagInformation
        {
            get
            {
                return this.contentViewsTagInformation;
            }
        }

        /// <summary>
        /// Gets all tags from the category BuildingBlocks.
        /// </summary>
        public List<TagInfo> BuildingBlocksTagInformation
        {
            get
            {
                return this.buildingBlocksTagInformation;
            }
        }

        /// <summary>
        /// Gets all tags from the category Pages.
        /// </summary>
        public List<TagInfo> PagesTagInformation
        {
            get
            {
                return this.pagesTagInformation;
            }
        }

        /// <summary>
        /// Gets all tags.
        /// </summary>
        public List<TagInfo> AllTags
        {
            get
            {
                if (this.allTags == null)
                {
                    this.allTags = new List<TagInfo>();
                    this.allTags.AddRange(this.ContentViewsTagInformation);
                    this.allTags.AddRange(this.PagesTagInformation);
                    this.allTags.AddRange(this.BuildingBlocksTagInformation);
                }
                return this.allTags;
            }
        }

        /// <summary>
        /// Gets the new templates.
        /// </summary>
        public List<TemplateViewModel> NewTemplates
        {
            get
            {
                return this.newTemplates;
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

        internal void LoadData()
        {
            if (this.IsDataLoaded)
            {
                return;
            }

            this.LoadTemplates();
            this.LoadTagsInformation();

            this.IsDataLoaded = true;
        }

        internal void RestoreTemplatePageApplicationBar()
        {
            if (this.TemplatePage == null)
            {
                return;
            }
            var bar = this.TemplatePage.ApplicationBar;
            if (bar == null)
            {
                return;
            }
            bar.Buttons.Clear();
            for (var i = 1; i <= 4; i++)
            {
                var standardButton = new ApplicationBarIconButton(new Uri("/Images/Icons/EmptyIcon.png", UriKind.RelativeOrAbsolute));
                standardButton.Text = string.Format("Button {0}", i);
                bar.Buttons.Add(standardButton);
            }
        }

        internal void AddButtonToTemplatePageApplicationBar(ApplicationBarIconButton button, int position)
        {
            var bar = this.TemplatePage.ApplicationBar;
            bar.Buttons[position - 1] = button;
        }

        internal void RemoveLastButtonFromTemplatePageApplicationBar()
        {
            var bar = this.TemplatePage.ApplicationBar;
            bar.Buttons.RemoveAt(bar.Buttons.Count - 1);
        }

        internal void HideApplicationBar()
        {
            var bar = this.TemplatePage.ApplicationBar;
            bar.IsVisible = false;
        }

        internal void ShowApplicationBar()
        {
            var bar = this.TemplatePage.ApplicationBar;
            bar.IsVisible = true;
        }

        private void LoadTemplates()
        {
            var stream = Application.Current.GetType().Assembly.GetManifestResourceStream("Telerik.DesignTemplates.WP.Templates.xml");
            var element = XElement.Load(stream);

            this.ParseTemplates(element);

            stream.Close();
        }

        private void ParseTemplates(XElement element)
        {
            foreach (XElement categoryElement in element.Elements("TemplateCategory"))
            {
                var categoryName = string.Empty;
                foreach (XAttribute attribute in categoryElement.Attributes())
                {
                    if (attribute.Name.LocalName == "CategoryName")
                    {
                        categoryName = attribute.Value;
                    }
                }
                this.ParseSingleCategory(categoryName, categoryElement);
            }
        }

        private void ParseSingleCategory(string categoryName, XElement element)
        {
            foreach (XElement templateElement in element.Elements("Template"))
            {
                var template = new TemplateViewModel();
                template.CategoryName = categoryName;
                this.ParseSingleTemplate(template, templateElement);
                this.AllTemplates.Add(template);
                if (template.IsNew)
                {
                    this.NewTemplates.Add(template);
                }
            }
        }

        private void ParseSingleTemplate(TemplateViewModel model, XElement element)
        {
            foreach (XAttribute attribute in element.Attributes())
            {
                switch (attribute.Name.LocalName)
                {
                    case "Title":
                        model.Title = attribute.Value;
                        break;
                    case "ShortTitle":
                        model.ShortTitle = attribute.Value;
                        break;
                    case "IsNew":
                        model.IsNew = bool.Parse(attribute.Value);
                        break;
                    case "TemplateLocation":
                        model.TemplateLocation = attribute.Value;
                        break;
                    case "ScreenshotName":
                        model.ScreenshotName = attribute.Value;
                        break;
                    case "Tags":
                        var tags = attribute.Value.Split(';');
                        foreach (string tag in tags)
                        {
                            model.Tags.Add(tag);
                        }
                        break;
                }
            }
        }

        private void LoadTagsInformation()
        {
            var stream = Application.Current.GetType().Assembly.GetManifestResourceStream("Telerik.DesignTemplates.WP.Tags.xml");
            var element = XElement.Load(stream);

            foreach (XElement categoryElement in element.Elements("Category"))
            {
                var categoryName = string.Empty;
                foreach (XAttribute attribute in categoryElement.Attributes())
                {
                    if (attribute.Name.LocalName == "CategoryName")
                    {
                        categoryName = attribute.Value;
                    }
                }
                this.ParseTagsInCategory(categoryName, categoryElement);
            }

            stream.Close();
        }

        private void ParseTagsInCategory(string categoryName, XElement categoryElement, string parentTag = null)
        {
            foreach (XElement element in categoryElement.Elements("Tag"))
            {
                foreach (XAttribute attribute in element.Attributes())
                {
                    if (attribute.Name.LocalName == "Name")
                    {
                        var tagName = attribute.Value;
                        var parentTagName = (parentTag == null) ? string.Empty : parentTag;
                        var templatesCount = 0;
                        foreach (TemplateViewModel model in this.AllTemplates)
                        {
                            if (model.Tags.Contains(tagName))
                            {
                                templatesCount++;
                            }
                        }
                        if (templatesCount > 0)
                        {
                            var newTagInfo = new TagInfo()
                            {
                                Category = categoryName,
                                ParentTag = parentTagName,
                                Tag = tagName,
                                TemplatesCount = templatesCount
                            };

                            this.AddNewTagInfoToTagInformation(newTagInfo);
                        }
                    }
                }
            }
            foreach (XElement element in categoryElement.Elements("ParentTag"))
            {
                string parentTagName = null;
                foreach (XAttribute attribute in element.Attributes())
                {
                    if (attribute.Name.LocalName == "Name")
                    {
                        parentTagName = attribute.Value;
                    }
                }
                this.ParseTagsInCategory(categoryName, element, parentTagName);
            }
        }

        private void AddNewTagInfoToTagInformation(TagInfo tagInfo)
        {
            switch (tagInfo.Category)
            {
                case ContentViewsCategoryName:
                    this.ContentViewsTagInformation.Add(tagInfo);
                    break;
                case BuildingBlocksCategoryName:
                    this.BuildingBlocksTagInformation.Add(tagInfo);
                    break;
                case PagesCategoryName:
                    this.PagesTagInformation.Add(tagInfo);
                    break;
            }
        }

        private void InitializeGroupDescriptors()
        {
            this.groupDescriptors = new List<DataDescriptor>();
            var groupDescriptor = new GenericGroupDescriptor<TagInfo, string>(item => item.ParentTag);
            groupDescriptor.SortMode = ListSortMode.None;
            this.groupDescriptors.Add(groupDescriptor);
        }

        private void UpdateSelectedTagTemplates()
        {
            if (this.SelectedTag == null)
            {
                return;
            }

            this.SelectedTagTemplates.Clear();

            if (this.SelectedTag.Tag == IsNewTagName)
            {
                foreach (TemplateViewModel template in this.NewTemplates)
                {
                    this.SelectedTagTemplates.Add(template);
                }
                return;
            }

            foreach (TemplateViewModel templateModel in this.AllTemplates)
            {
                if (templateModel.Tags.Contains(this.SelectedTag.Tag))
                {
                    this.SelectedTagTemplates.Add(templateModel);
                }
            }
        }
    }

    public class TagInfo
    {
        public TagInfo()
        {
        }

        public string Tag { get; set; }

        public string ParentTag { get; set; }

        public string Category { get; set; }

        public int TemplatesCount { get; set; }
    }
}
