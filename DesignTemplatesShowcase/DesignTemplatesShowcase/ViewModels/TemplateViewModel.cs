using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Telerik.DesignTemplates.WP.ViewModels
{
    public class TemplateViewModel : ViewModelBase
    {
        private string title;
        private string shortTitle;
        private List<string> tags;
        private bool isNew;
        private string templateLocation;
        private ImageSource screenshot;
        private string screenshotName;
        private string categoryName;

        public TemplateViewModel()
        {
            this.tags = new List<string>();
        }

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

        public string ShortTitle
        {
            get
            {
                return this.shortTitle;
            }
            set
            {
                if (this.shortTitle != value)
                {
                    this.shortTitle = value;
                    this.OnPropertyChanged("ShortTitle");
                }
            }
        }

        public List<string> Tags
        {
            get
            {
                return this.tags;
            }
        }

        public bool IsNew
        {
            get
            {
                return this.isNew;
            }
            set
            {
                if (this.isNew != value)
                {
                    this.isNew = value;
                    this.OnPropertyChanged("IsNew");
                }
            }
        }

        public string TemplateLocation
        {
            get
            {
                return this.templateLocation;
            }
            set
            {
                if (this.templateLocation != value)
                {
                    this.templateLocation = value;
                    this.OnPropertyChanged("TemplateLocation");
                }
            }
        }

        public ImageSource Screenshot
        {
            get
            {
                if (this.screenshot != null)
                {
                    return this.screenshot;
                }

                if (!string.IsNullOrEmpty(this.ScreenshotName))
                {
                    var bitmap = new BitmapImage();
                    var appAssembly = Application.Current.GetType().Assembly;
                    var themeName = MainViewModel.Instance.IsDarkTheme ? "Dark" : "Light";
                    using (var imageStream = appAssembly.GetManifestResourceStream(string.Format("Telerik.DesignTemplates.WP.Images.{0}.Screenshots.{1}", themeName, this.ScreenshotName)))
                    {
                        bitmap.SetSource(imageStream);
                        this.screenshot = bitmap;
                    }
                }
                return this.screenshot;
            }
        }

        public string ScreenshotName
        {
            get
            {
                return this.screenshotName;
            }
            set
            {
                if (this.screenshotName != value)
                {
                    this.screenshotName = value;
                    this.OnPropertyChanged("ScreenshotName");
                }
            }
        }

        public string CategoryName
        {
            get
            {
                return this.categoryName;
            }
            set
            {
                if (this.categoryName != value)
                {
                    this.categoryName = value;
                    this.OnPropertyChanged("CategoryName");
                }
            }
        }
    }
}
