using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Telerik.DesignTemplates.WP.ViewModels;
using Microsoft.Phone.Shell;
using Telerik.Windows.Controls;

namespace Telerik.DesignTemplates.WP.Templates.Lists
{
    public partial class List06 : UserControl
    {
        public List06()
        {
            InitializeComponent();
            this.Loaded += this.OnLoaded;
            this.Unloaded += this.OnUnloaded;
        }

        void OnUnloaded(object sender, RoutedEventArgs e)
        {
            MainViewModel.Instance.RestoreTemplatePageApplicationBar();
        }

        void OnLoaded(object sender, RoutedEventArgs e)
        {
            ApplicationBarIconButton wrapModeButton = this.Resources["WrapModeButton"] as ApplicationBarIconButton;
            MainViewModel.Instance.AddButtonToTemplatePageApplicationBar(wrapModeButton, 1);
        }

        private void StackModeButton_Click(object sender, EventArgs e)
        {
            StackVirtualizationStrategyDefinition stackDefinition = new StackVirtualizationStrategyDefinition();
            stackDefinition.Orientation = System.Windows.Controls.Orientation.Vertical;
            this.listBox.VirtualizationStrategyDefinition = stackDefinition;
            this.listBox.ItemTemplate = this.Resources["StackModeItemTemplate"] as DataTemplate;
            ApplicationBarIconButton wrapModeButton = this.Resources["WrapModeButton"] as ApplicationBarIconButton;
            MainViewModel.Instance.AddButtonToTemplatePageApplicationBar(wrapModeButton, 1);
        }

        private void WrapModeButton_Click(object sender, EventArgs e)
        {
            WrapVirtualizationStrategyDefinition wrapDefinition = new WrapVirtualizationStrategyDefinition();
            wrapDefinition.Orientation = System.Windows.Controls.Orientation.Horizontal;
            this.listBox.VirtualizationStrategyDefinition = wrapDefinition;
            this.listBox.ItemTemplate = this.Resources["WrapModeItemTemplate"] as DataTemplate;
            ApplicationBarIconButton stackModeButton = this.Resources["StackModeButton"] as ApplicationBarIconButton;
            MainViewModel.Instance.AddButtonToTemplatePageApplicationBar(stackModeButton, 1);
        }
    }
}
