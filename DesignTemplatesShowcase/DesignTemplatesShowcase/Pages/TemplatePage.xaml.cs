using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Phone.Controls;
using Telerik.DesignTemplates.WP.ViewModels;

namespace Telerik.DesignTemplates.WP.Pages
{
    public partial class TemplatePage : PhoneApplicationPage
    {
        public TemplatePage()
        {
            this.InitializeComponent();
            MainViewModel.Instance.TemplatePage = this;
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            if (this.NavigationContext.QueryString.ContainsKey("search"))
            {
                this.NavigationControl.IsInSearchMode = true;
            }

            this.LoadMainContent();
        }

        protected override void OnBackKeyPress(System.ComponentModel.CancelEventArgs e)
        {
            if (this.NavigationControl.IsExpanded)
            {
                this.NavigationControl.IsExpanded = false;
                e.Cancel = true;
            }
            else
            {
                base.OnBackKeyPress(e);
            }
        }

        private void TemplateNavigationControl_SelectedTemplateChanged(object sender, EventArgs e)
        {
            this.LoadMainContent();
        }

        private void LoadMainContent()
        {
            var typeString = string.Format("Telerik.DesignTemplates.WP.{0}", MainViewModel.Instance.SelectedTemplate.TemplateLocation);
            var type = Application.Current.GetType().Assembly.GetType(typeString);
            var content = Activator.CreateInstance(type) as UserControl;
            this.MainContent.Content = content;
        }
    }
}