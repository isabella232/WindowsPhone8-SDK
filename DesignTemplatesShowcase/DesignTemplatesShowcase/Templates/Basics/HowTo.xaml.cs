using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Phone.Shell;
using Telerik.DesignTemplates.WP.ViewModels;

namespace Telerik.DesignTemplates.WP.Templates.Basics
{
    public partial class HowTo : UserControl
    {
        public HowTo()
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
            var leftArrowButton = new ApplicationBarIconButton(new Uri("Images/Icons/LeftArrow.png", UriKind.RelativeOrAbsolute));
            leftArrowButton.Text = "Left";
            leftArrowButton.Click += delegate
            {
                this.slideView.MoveToPreviousItem();
            };
            MainViewModel.Instance.AddButtonToTemplatePageApplicationBar(leftArrowButton, 2);

            var rightArrowButton = new ApplicationBarIconButton(new Uri("Images/Icons/RightArrow.png", UriKind.RelativeOrAbsolute));
            rightArrowButton.Text = "Right";
            rightArrowButton.Click += delegate
            {
                this.slideView.MoveToNextItem();
            };
            MainViewModel.Instance.AddButtonToTemplatePageApplicationBar(rightArrowButton, 3);
        }
    }
}