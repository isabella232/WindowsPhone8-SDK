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
using Microsoft.Phone.Shell;
using Telerik.DesignTemplates.WP.ViewModels;

namespace Telerik.DesignTemplates.WP.Templates.Lists
{
    public partial class List09 : UserControl
    {
        public List09()
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
            ApplicationBarIconButton selectButton = new ApplicationBarIconButton(new Uri("Images/Icons/select.png", UriKind.RelativeOrAbsolute));
            selectButton.Text = "select";
            selectButton.Click += delegate
            {
                this.DataBoundListBox.IsCheckModeActive ^= true;
            };
            MainViewModel.Instance.AddButtonToTemplatePageApplicationBar(selectButton, 1);
        }
    }
}
