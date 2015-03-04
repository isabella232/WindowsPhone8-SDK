using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Phone.Shell;
using Telerik.DesignTemplates.WP.ViewModels;
using Telerik.Windows.Controls;

namespace Telerik.DesignTemplates.WP.Templates.BuildingBlocks
{
    public partial class TrialReminder : UserControl
    {
        public TrialReminder()
        {
            this.InitializeComponent();
            this.Loaded += this.OnLoaded;
            this.Unloaded += this.OnUnloaded;
        }

        private void OnUnloaded(object sender, RoutedEventArgs e)
        {
            MainViewModel.Instance.RestoreTemplatePageApplicationBar();
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            var selectButton = new ApplicationBarIconButton(new Uri("Images/Icons/select.png", UriKind.RelativeOrAbsolute));
            selectButton.Text = "select";
            selectButton.Click += delegate
            {
                this.DataBoundListBox.IsCheckModeActive ^= true;
            };
            MainViewModel.Instance.AddButtonToTemplatePageApplicationBar(selectButton, 1);

            var reminder = new RadTrialApplicationReminder();
            reminder.OccurrenceUsageCount = 1;
            reminder.SimulateTrialForTests = true;
            reminder.AllowedTrialPeriod = new TimeSpan(0);
            reminder.Notify();
        }
    }
}
