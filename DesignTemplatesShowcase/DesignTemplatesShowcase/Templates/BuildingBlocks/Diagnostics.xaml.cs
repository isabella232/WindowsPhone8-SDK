using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Phone.Shell;
using Telerik.DesignTemplates.WP.ViewModels;

namespace Telerik.DesignTemplates.WP.Templates.BuildingBlocks
{
    public partial class Diagnostics : UserControl
    {
        private static bool isDiagnosticsIllustrated = false;
        private static int loadedCount = 0;

        private string originalErrorReportingEmailTo;

        public Diagnostics()
        {
            this.InitializeComponent();
            loadedCount++;
            this.Loaded += this.OnLoaded;
            this.Unloaded += this.OnUnloaded;
        }

        private void OnUnloaded(object sender, RoutedEventArgs e)
        {
            (Application.Current as App).diagnostics.EmailTo = this.originalErrorReportingEmailTo;
            MainViewModel.Instance.RestoreTemplatePageApplicationBar();
            loadedCount--;
            if (loadedCount == 0)
            {
                isDiagnosticsIllustrated = false;
            }
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            this.originalErrorReportingEmailTo = (Application.Current as App).diagnostics.EmailTo;
            (Application.Current as App).diagnostics.EmailTo = "YourName@YourCompany.com";
            var selectButton = new ApplicationBarIconButton(new Uri("Images/Icons/select.png", UriKind.RelativeOrAbsolute));
            selectButton.Text = "select";
            selectButton.Click += delegate
            {
                this.DataBoundListBox.IsCheckModeActive ^= true;
            };
            MainViewModel.Instance.AddButtonToTemplatePageApplicationBar(selectButton, 1);

            if (!isDiagnosticsIllustrated)
            {
                isDiagnosticsIllustrated = true;
                throw new Exception("Exception thrown to illustrate the RadDiagnostics component");
            }
        }
    }
}