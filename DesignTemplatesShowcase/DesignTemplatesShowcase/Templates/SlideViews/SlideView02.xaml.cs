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

namespace Telerik.DesignTemplates.WP.Templates.SlideViews
{
    public partial class SlideView02 : UserControl
    {
        public SlideView02()
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
            ApplicationBarIconButton leftArrowButton = new ApplicationBarIconButton(new Uri("Images/Icons/LeftArrow.png", UriKind.RelativeOrAbsolute));
            leftArrowButton.Text = "Left";
            leftArrowButton.Click += delegate
            {
                this.slideView.MoveToPreviousItem();
            };
            MainViewModel.Instance.AddButtonToTemplatePageApplicationBar(leftArrowButton, 2);

            ApplicationBarIconButton rightArrowButton = new ApplicationBarIconButton(new Uri("Images/Icons/RightArrow.png", UriKind.RelativeOrAbsolute));
            rightArrowButton.Text = "Right";
            rightArrowButton.Click += delegate
            {
                this.slideView.MoveToNextItem();
            };
            MainViewModel.Instance.AddButtonToTemplatePageApplicationBar(rightArrowButton, 3);
        }
    }
}
