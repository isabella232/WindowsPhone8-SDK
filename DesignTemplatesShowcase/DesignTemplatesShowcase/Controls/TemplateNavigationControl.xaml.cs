using System;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Telerik.DesignTemplates.WP.ViewModels;

namespace Telerik.DesignTemplates.WP.Controls
{
    public partial class TemplateNavigationControl : UserControl, INotifyPropertyChanged
    {
        public static readonly DependencyProperty IsExpandedProperty =
            DependencyProperty.Register("IsExpanded", typeof(bool), typeof(TemplateNavigationControl), new PropertyMetadata(false, OnIsExpandedChanged));

        private int currentTemplateIndex;
        private int templatesCount;
        private bool isInSearchMode;
        
        public TemplateNavigationControl()
        {
            this.InitializeComponent();
            this.DataContext = MainViewModel.Instance;
            this.CurrentTemplateIndex = MainViewModel.Instance.SelectedTagTemplates.IndexOf(MainViewModel.Instance.SelectedTemplate);
            this.TemplatesCount = MainViewModel.Instance.SelectedTagTemplates.Count;
            this.IsExpanded = false;
        }

        public event EventHandler SelectedTemplateChanged;

        public event PropertyChangedEventHandler PropertyChanged;

        public bool IsExpanded
        {
            get
            {
                return (bool)this.GetValue(IsExpandedProperty);
            }
            set
            {
                this.SetValue(IsExpandedProperty, value);
            }
        }

        public string IndexLabel
        {
            get
            {
                if (this.IsInSearchMode)
                {
                    return "1/1";
                }
                return string.Format("{0}{1}/{2}", this.CurrentTemplateIndex, 1, this.TemplatesCount);
            }
        }

        public bool IsInSearchMode
        {
            get
            {
                return this.isInSearchMode;
            }
            set
            {
                this.isInSearchMode = value;
                if (this.IsInSearchMode)
                {
                    this.LeftButton.IsEnabled = false;
                    this.RightButton.IsEnabled = false;
                    this.TemplatesCount = 1;
                }
            }
        }

        public int CurrentTemplateIndex
        {
            get
            {
                return this.currentTemplateIndex;
            }
            set
            {
                if (this.currentTemplateIndex != value)
                {
                    this.currentTemplateIndex = value;
                    if (this.PropertyChanged != null)
                    {
                        this.PropertyChanged(this, new PropertyChangedEventArgs("IndexLabel"));
                    }
                }
            }
        }

        public int TemplatesCount
        {
            get
            {
                return this.templatesCount;
            }
            set
            {
                if (this.templatesCount != value)
                {
                    this.templatesCount = value;
                    if (this.PropertyChanged != null)
                    {
                        this.PropertyChanged(this, new PropertyChangedEventArgs("IndexLabel"));
                    }
                }
            }
        }

        private static void OnIsExpandedChanged(DependencyObject sender, DependencyPropertyChangedEventArgs args)
        {
            var control = sender as TemplateNavigationControl;
            if ((bool)args.NewValue)
            {
                control.ExpandBorder();
            }
            else
            {
                control.CollapseBorder();
            }
        }

        private void RadLeftButton_Tap(object sender, GestureEventArgs e)
        {
            this.CurrentTemplateIndex--;
            if (this.CurrentTemplateIndex < 0)
            {
                this.CurrentTemplateIndex = this.TemplatesCount - 1;
            }
            MainViewModel.Instance.SelectedTemplate = MainViewModel.Instance.SelectedTagTemplates[this.CurrentTemplateIndex];
            if (this.SelectedTemplateChanged != null)
            {
                this.SelectedTemplateChanged(this, new EventArgs());
            }
        }

        private void RadRightButton_Tap(object sender, GestureEventArgs e)
        {
            this.CurrentTemplateIndex++;
            if (this.CurrentTemplateIndex >= this.TemplatesCount)
            {
                this.CurrentTemplateIndex = 0;
            }
            MainViewModel.Instance.SelectedTemplate = MainViewModel.Instance.SelectedTagTemplates[this.CurrentTemplateIndex];
            if (this.SelectedTemplateChanged != null)
            {
                this.SelectedTemplateChanged(this, new EventArgs());
            }
        }

        private void CollapseBorder()
        {
            this.ExpandedBorder.Visibility = Visibility.Collapsed;
            this.CollapsedBorder.Visibility = Visibility.Visible;
        }

        private void ExpandBorder()
        {
            this.ExpandedBorder.Visibility = Visibility.Visible;
            this.CollapsedBorder.Visibility = Visibility.Collapsed;
        }

        private void CollapsedControl_Tap(object sender, GestureEventArgs e)
        {
            this.IsExpanded = true;
        }

        private void ExpandedControl_Tap(object sender, GestureEventArgs e)
        {
            this.IsExpanded = false;
        }
    }
}
