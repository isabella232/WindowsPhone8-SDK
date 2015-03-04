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

namespace Telerik.DesignTemplates.WP.Templates.DataForms
{
    public partial class CheckoutForm : UserControl
    {
        public CheckoutForm()
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
            ApplicationBarIconButton okButton = new ApplicationBarIconButton(new Uri("Images/DateTimePickerOk.png", UriKind.RelativeOrAbsolute));
            okButton.Text = "Ok";
            okButton.Click += delegate
            {
                this.DataForm.Commit();
            };
            MainViewModel.Instance.AddButtonToTemplatePageApplicationBar(okButton, 1);
            ApplicationBarIconButton cancelButton = new ApplicationBarIconButton(new Uri("Images/DateTimePickerCancel.png", UriKind.RelativeOrAbsolute));
            cancelButton.Text = "Cancel";
            MainViewModel.Instance.AddButtonToTemplatePageApplicationBar(cancelButton, 2);
            MainViewModel.Instance.RemoveLastButtonFromTemplatePageApplicationBar();
            MainViewModel.Instance.RemoveLastButtonFromTemplatePageApplicationBar();
        }
    }
}
