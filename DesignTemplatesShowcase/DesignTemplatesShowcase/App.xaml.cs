using System;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using System.Windows.Navigation;
using Microsoft.Phone.Shell;
using Telerik.DesignTemplates.WP.ViewModels;
using Telerik.Windows.Controls;

namespace Telerik.DesignTemplates.WP
{
    public partial class App : Application
    {
        public RadDiagnostics diagnostics;
        
        private RadPhoneApplicationFrame rootFrame;

        /// <summary>
        /// Constructor for the Application object.
        /// </summary>
        public App()
        {
            // Standard Silverlight initialization
            this.InitializeComponent();

            this.rootFrame = new RadPhoneApplicationFrame();
            this.rootFrame.Navigated += new NavigatedEventHandler(this.OnRootFrameNavigated);

            // Global handler for uncaught exceptions. 
            this.UnhandledException += this.Application_UnhandledException;

            // Show graphics profiling information while debugging.
            if (System.Diagnostics.Debugger.IsAttached)
            {
                // Display the current frame rate counters.
                Application.Current.Host.Settings.EnableFrameRateCounter = true;
                // Show the areas of the app that are being redrawn in each frame.
                //Application.Current.Host.Settings.EnableRedrawRegions = true;
                // Enable non-production analysis visualization mode, 
                // which shows areas of a page that are handed off to GPU with a colored overlay.
                //Application.Current.Host.Settings.EnableCacheVisualization = true;
            }

            MainViewModel.Instance.IsDarkTheme = (Visibility)this.Resources["PhoneDarkThemeVisibility"] == Visibility.Visible;

            this.LoadResources();

            // assign the custom style of the frame
            this.rootFrame.Style = this.Resources["RadPhoneApplicationFrameStyle"] as Style;
            this.rootFrame.Background = (Brush)Application.Current.Resources["BackgroundBrush"];

            this.diagnostics = new RadDiagnostics()
            {
                EmailTo = "wp7tasksapp@telerik.com",
                HandleUnhandledException = true,
                IncludeScreenshot = true,
            };
            this.diagnostics.Init();
        }

        private void LoadResources()
        {
            var themeName = MainViewModel.Instance.IsDarkTheme ? "Dark" : "Light";
            var path = String.Format("/Telerik.DesignTemplates.WP;Component/Themes/{0}.xaml", themeName);
            var resources = new ResourceDictionary();
            resources.Source = new Uri(path, UriKind.Relative);

            foreach (object key in resources.Keys)
            {
                var value = resources[key];
                resources.Remove(key);
                this.Resources.Add(key, value);
            }
        }

        // Code to execute when the application is launching (eg, from Start)
        // This code will not execute when the application is reactivated
        private void Application_Launching(object sender, LaunchingEventArgs e)
        {
            ApplicationUsageHelper.Init("Q1 2013 WP8");
        }

        // Code to execute when the application is activated (brought to foreground)
        // This code will not execute when the application is first launched
        private void Application_Activated(object sender, ActivatedEventArgs e)
        {
            ApplicationUsageHelper.OnApplicationActivated();
        }

        // Code to execute when the application is deactivated (sent to background)
        // This code will not execute when the application is closing
        private void Application_Deactivated(object sender, DeactivatedEventArgs e)
        {
            // Ensure that required application state is persisted here.
        }

        // Code to execute when the application is closing (eg, user hit Back)
        // This code will not execute when the application is deactivated
        private void Application_Closing(object sender, ClosingEventArgs e)
        {
        }

        // Code to execute if a navigation fails
        private void RootFrame_NavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            if (System.Diagnostics.Debugger.IsAttached)
            {
                // A navigation has failed; break into the debugger
                System.Diagnostics.Debugger.Break();
            }
        }

        // Code to execute on Unhandled Exceptions
        private void Application_UnhandledException(object sender, ApplicationUnhandledExceptionEventArgs e)
        {
            if (System.Diagnostics.Debugger.IsAttached)
            {
                // An unhandled exception has occurred; break into the debugger
                System.Diagnostics.Debugger.Break();
            }
        }

        private void OnRootFrameNavigated(object sender, NavigationEventArgs e)
        {
            Application.Current.RootVisual = this.rootFrame;

            this.rootFrame.Navigated -= this.OnRootFrameNavigated;
            MainViewModel.Instance.MainFrame = this.rootFrame;
        }
    }
}