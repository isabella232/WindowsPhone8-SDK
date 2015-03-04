using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using Microsoft.Phone.Shell;
using Telerik.DesignTemplates.WP.ViewModels;
using Telerik.Windows.Controls;

namespace Telerik.DesignTemplates.WP.Templates.Basics
{
    public partial class Search02 : UserControl
    {
        public Search02()
        {
            this.InitializeComponent();
            this.Loaded += this.OnLoaded;
            this.Unloaded += this.OnUnloaded;
        }

        private void OnUnloaded(object sender, RoutedEventArgs e)
        {
            MainViewModel.Instance.TemplatePage.BackKeyPress -= new EventHandler<System.ComponentModel.CancelEventArgs>(this.TemplatePage_BackKeyPress);
            MainViewModel.Instance.RestoreTemplatePageApplicationBar();
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            MainViewModel.Instance.TemplatePage.BackKeyPress += new EventHandler<System.ComponentModel.CancelEventArgs>(this.TemplatePage_BackKeyPress);

            var selectButton = new ApplicationBarIconButton(new Uri("Images/Icons/SearchButton.png", UriKind.RelativeOrAbsolute));
            selectButton.Text = "search";
            selectButton.Click += delegate
            {
                this.AnimateIn(this.radAutoComplete);
            };
            MainViewModel.Instance.AddButtonToTemplatePageApplicationBar(selectButton, 4);
        }

        private void TemplatePage_BackKeyPress(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (this.radAutoComplete.Visibility == System.Windows.Visibility.Visible)
            {
                this.AnimateOut(this.radAutoComplete);
                this.radAutoComplete.Text = string.Empty;
                e.Cancel = true;
            }
        }

        private void AnimateIn(Control control)
        {
            var animation = new RadMoveAnimation();
            var startPoint = new Point() { X = 0, Y = -80 };
            var endPoint = new Point() { X = 0, Y = 20 };
            animation.StartPoint = startPoint;
            animation.EndPoint = endPoint;
            animation.SpeedRatio = 2;
            animation.AutoReverse = false;
            animation.Duration = new Duration(TimeSpan.FromSeconds(1));
            var easing = new QuinticEase();
            easing.EasingMode = EasingMode.EaseIn;
            animation.Easing = easing;
            control.Visibility = System.Windows.Visibility.Visible;
            RadAnimationManager.Play(control, animation);
            this.Dispatcher.BeginInvoke(() =>
            {
                control.Focus();
            });
        }

        private void AnimateOut(Control control)
        {
            var animation = new RadMoveAnimation();
            var startPoint = new Point() { X = 0, Y = 20 };
            var endPoint = new Point() { X = 0, Y = -80 };
            animation.StartPoint = startPoint;
            animation.EndPoint = endPoint;
            animation.SpeedRatio = 2;
            animation.AutoReverse = false;
            animation.Duration = new Duration(TimeSpan.FromSeconds(1));
            var easing = new QuinticEase();
            easing.EasingMode = EasingMode.EaseIn;
            animation.Easing = easing;
            animation.Ended += new EventHandler<AnimationEndedEventArgs>(this.animation_Ended);
            RadAnimationManager.Play(control, animation);
        }

        private void animation_Ended(object sender, EventArgs e)
        {
            this.radAutoComplete.Visibility = Visibility.Collapsed;
        }

        private void RadAutoComplete_GotFocus(object sender, RoutedEventArgs e)
        {
            var bar = MainViewModel.Instance.TemplatePage.ApplicationBar;
            bar.IsVisible = false;
        }

        private void RadAutoComplete_LostFocus(object sender, RoutedEventArgs e)
        {
            var bar = MainViewModel.Instance.TemplatePage.ApplicationBar;
            bar.IsVisible = true;
        }
    }
}
