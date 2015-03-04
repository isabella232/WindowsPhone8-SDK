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

namespace Telerik.DesignTemplates.WP.Templates.DataForms
{
    public partial class SignIn : UserControl
    {
        public SignIn()
        {
            InitializeComponent();
            this.Loaded += this.OnLoaded;
            this.Unloaded += this.OnUnloaded;
        }

        void OnUnloaded(object sender, RoutedEventArgs e)
        {
            MainViewModel.Instance.ShowApplicationBar();
        }

        void OnLoaded(object sender, RoutedEventArgs e)
        {
            MainViewModel.Instance.HideApplicationBar();
        }

        private void SignUp_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            MainViewModel.Instance.TemplatePage.NavigationService.Navigate(new Uri("/Pages/SignUpPage.xaml", UriKind.RelativeOrAbsolute));
        }
    }
}
