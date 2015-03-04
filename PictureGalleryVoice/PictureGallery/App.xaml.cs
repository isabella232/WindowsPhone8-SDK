using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.IO;
using System.IO.IsolatedStorage;
using System.Net.NetworkInformation;
using System.Runtime.Serialization;
using System.Windows;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using PictureGallery.Pages;
using PictureGallery.ServiceProviders;
using PictureGallery.ViewModels;
using Telerik.Windows.Controls;

namespace PictureGallery
{
    public partial class App : Application
    {
        public static readonly Uri PhotoPage = new Uri("/Pages/PhotoPage.xaml", UriKind.Relative);
        public static readonly Uri AuthorPage = new Uri("/Pages/AuthorPage.xaml", UriKind.Relative);
        public static readonly Uri PanZoomPage = new Uri("/Pages/PhotoPanZoomPage.xaml", UriKind.Relative);
        public static readonly Uri TagPage = new Uri("/Pages/TagPage.xaml", UriKind.Relative);
        public static readonly Uri SearchPage = new Uri("/Pages/SearchPage.xaml", UriKind.Relative);
        public static readonly Uri GalleryPage = new Uri("/Pages/GalleryPage.xaml", UriKind.Relative);
        public static readonly Uri InterestingPage = new Uri("/Pages/InterestingPage.xaml", UriKind.Relative);
        public static readonly Uri AboutPage = new Uri("/Pages/AboutPage.xaml", UriKind.Relative);

        public static readonly string SupportMail = "wp7tasksapp@telerik.com";
        public static readonly string ApplicationName = "PictureGallery";

        private readonly Type[] serializableTypes = new Type[]
        {
            typeof(ImageServiceViewModel),
            typeof(PhotoViewModel),
            typeof(GalleryViewModel),
            typeof(TagViewModel),
            typeof(PhotoSearchViewModel),
            typeof(PhotoListViewModel),
            typeof(AuthorViewModel),
        };

        private ObservableCollection<ImageServiceViewModel> favorites = new ObservableCollection<ImageServiceViewModel>();
        private IsolatedStorageFile isolatedStorage;
        private ApplicationBarIconButton pinButton;
        private ApplicationBarIconButton prevButton;
        private ApplicationBarIconButton nextButton;

        public ObservableCollection<ImageServiceViewModel> Favorites
        {
            get
            {
                return favorites;
            }
        }

        /// <summary>
        /// Gets the current image service provider.
        /// </summary>
        public IImageServiceProvider CurrentImageServiceProvider
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets a value that determines if the phone has network access.
        /// </summary>
        /// <remarks>
        /// If this returns true, it does not mean that there is internet access, only network access (a local network for example).
        /// </remarks>
        public bool HasNetwork
        {
            get
            {
                return NetworkInterface.GetIsNetworkAvailable();
            }
        }

        public static App Instance
        {
            get
            {
                return (App)Application.Current;
            }
        }

        public PhoneApplicationFrame RootFrame 
        {
            get; 
            private set; 
        }

        public PhoneApplicationPage CurrentPage
        {
            get
            {
                return this.RootFrame.Content as PhoneApplicationPage;
            }
        }

        private ApplicationBar CurrentAppBar
        {
            get
            {
                return this.CurrentPage.ApplicationBar as ApplicationBar;
            }
        }

        public App()
        {
            this.InitErrorHandling();
            this.CurrentImageServiceProvider = new FlickrServiceProvider();

            InitializeComponent();
            InitializePhoneApplication();

            this.InitAppBars();
            this.InitStorage();
        }

        private void InitErrorHandling()
        {
            RadDiagnostics diagnostics = new RadDiagnostics();
            diagnostics.ApplicationName = App.ApplicationName;
            diagnostics.ApplicationVersion = "1.0";
            diagnostics.EmailTo = App.SupportMail;
            diagnostics.ExceptionOccurred += this.OnDiagnosticsExceptionOccurred;
            diagnostics.HandleUnhandledException = false;
            diagnostics.Init();
            ApplicationUsageHelper.Init(diagnostics.ApplicationVersion);
        }

        private void InitStorage()
        {
            try
            {
                this.isolatedStorage = IsolatedStorageFile.GetUserStoreForApplication();
            }
            catch (IsolatedStorageException)
            {
                MessageBox.Show(Strings.PersistenceDisabled);
            }
        }

        private void InitAppBars()
        {
            ApplicationBarMenuItem aboutItem = new ApplicationBarMenuItem("about");
            aboutItem.Click += this.OnAboutClick;

            ApplicationBarIconButton searchButton = new ApplicationBarIconButton(new Uri("/Images/search.png", UriKind.RelativeOrAbsolute));
            searchButton.Click += this.OnAppBarSearchClick;
            searchButton.Text = Strings.Search;

            ApplicationBarIconButton pinButton = new ApplicationBarIconButton(new Uri("/Images/pin.png", UriKind.RelativeOrAbsolute));
            pinButton.Click += this.OnAppBarPinClick;
            pinButton.Text = Strings.Pin;

            ApplicationBarIconButton nextButton = new ApplicationBarIconButton(new Uri("/Images/next.png", UriKind.RelativeOrAbsolute));
            nextButton.Text = Strings.Next;
            nextButton.Click += this.OnAppBarNextClick;

            ApplicationBarIconButton previousButton = new ApplicationBarIconButton(new Uri("/Images/prev.png", UriKind.RelativeOrAbsolute));
            previousButton.Text = Strings.Previous;
            previousButton.Click += this.OnAppBarPrevClick;

            ApplicationBar appBar = this.Resources["AppBar"] as ApplicationBar;
            appBar.MenuItems.Add(aboutItem);
            appBar.Buttons.Add(searchButton);
            appBar.Buttons.Add(pinButton);

            appBar = this.Resources["SearchAppBar"] as ApplicationBar;
            appBar.MenuItems.Add(aboutItem);
            appBar.Buttons.Add(searchButton);

            appBar = this.Resources["PinAppBar"] as ApplicationBar;
            appBar.MenuItems.Add(aboutItem);
            appBar.Buttons.Add(pinButton);

            appBar = this.Resources["PhotoListAppBar"] as ApplicationBar;
            appBar.MenuItems.Add(aboutItem);
            appBar.Buttons.Add(searchButton);
            appBar.Buttons.Add(pinButton);
            appBar.Buttons.Add(previousButton);
            appBar.Buttons.Add(nextButton);

            this.pinButton = pinButton;
            this.prevButton = previousButton;
            this.nextButton = nextButton;
        }

        private void OnAboutClick(object sender, EventArgs e)
        {
            this.CurrentPage.NavigationService.Navigate(AboutPage);
        }

        /// <summary>
        /// Removes a view model from the app state.
        /// </summary>
        /// <typeparam name="T">The type of view model to remove.</typeparam>
        public void ClearViewModel<T>() where T : class
        {
            string typeName = typeof(T).Name;

            if (PhoneApplicationService.Current.State.Keys.Contains(typeName))
            {
                PhoneApplicationService.Current.State.Remove(typeName);
            }
        }
        
        /// <summary>
        /// Sets the view model that will be used by the next page.
        /// </summary>
        /// <typeparam name="T">The type of the view model.</typeparam>
        /// <param name="viewModel">The view model itself.</param>
        public void SetCurrentViewModel<T>(T viewModel) where T : class
        {
            if (viewModel == null)
            {
                return;
            }

            PhoneApplicationService.Current.State[typeof(T).Name] = viewModel;
        }

        /// <summary>
        /// Gets the current view model of the specified type.
        /// </summary>
        /// <typeparam name="T">The type of the view model to get.</typeparam>
        /// <returns>Returns the current view model of the specified type.</returns>
        public T GetCurrentViewModel<T>() where T : class
        {
            string typeName = typeof(T).Name;
            object result;
            PhoneApplicationService.Current.State.TryGetValue(typeName, out result);
            return result as T;
        }

        /// <summary>
        /// Forces an update of the pin button.
        /// </summary>
        public void UpdatePinButton()
        {
            if (this.CurrentAppBar == null)
            {
                return;
            }

            PageBase currentPage = this.CurrentPage as PageBase;

            foreach (ImageServiceViewModel viewModel in Favorites)
            {
                if (currentPage.CompareViewModel(viewModel))
                {
                    pinButton.Text = Strings.Unpin;
                    pinButton.IconUri = new Uri("/Images/unpin.png", UriKind.Relative);
                    return;
                }
            }

            pinButton.Text = Strings.Pin;
            pinButton.IconUri = new Uri("/Images/pin.png", UriKind.Relative);
        }
        
        private void OnAppBarSearchClick(object sender, EventArgs e)
        {
            this.CurrentPage.NavigationService.Navigate(SearchPage);
        }

        private void OnAppBarPinClick(object sender, EventArgs e)
        {
            ApplicationBarIconButton button = sender as ApplicationBarIconButton;
            (this.CurrentPage as PageBase).PinFavorite(button.Text == Strings.Unpin);
            this.UpdatePinButton();
        }

        private void OnAppBarPrevClick(object sender, EventArgs e)
        {
            PageBase page = this.CurrentPage as PageBase;
            page.PreviousImage();
            this.UpdateNavigationButtons();
        }

        private void OnAppBarNextClick(object sender, EventArgs e)
        {
            PageBase page = this.CurrentPage as PageBase;
            page.NextImage();
            this.UpdateNavigationButtons();
        }

        public void UpdateNavigationButtons(PageBase page)
        {
            this.prevButton.IsEnabled = page.HasPreviousImage;
            this.nextButton.IsEnabled = page.HasNextImage;
        }

        public void UpdateNavigationButtons()
        {
            this.UpdateNavigationButtons(this.CurrentPage as PageBase);
        }

        private void OnFavoritesChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (this.isolatedStorage == null)
            {
                return;
            }

            using (IsolatedStorageFileStream file = new IsolatedStorageFileStream("favorites", FileMode.Create, this.isolatedStorage))
            {
                DataContractSerializer serializer = new DataContractSerializer(this.favorites.GetType(), this.serializableTypes);
                serializer.WriteObject(file, this.favorites);
            }
        }

        private void LoadFavorites()
        {
            if (this.isolatedStorage == null)
            {
                return;
            }

            if (!this.isolatedStorage.FileExists("favorites"))
            {
                return;
            }

            if (this.favorites.Count != 0)
            {
                return;
            }

            using (IsolatedStorageFileStream file = new IsolatedStorageFileStream("favorites", FileMode.Open, this.isolatedStorage))
            {
                DataContractSerializer serializer = new DataContractSerializer(this.favorites.GetType(), this.serializableTypes);
                this.favorites = serializer.ReadObject(file) as ObservableCollection<ImageServiceViewModel>;
            }
        }

        public void CheckNetwork()
        {
            if (!this.HasNetwork)
            {
                this.RootFrame.Dispatcher.BeginInvoke(() => RadMessageBox.Show(Strings.NoNetworkTitle, MessageBoxButtons.OK, Strings.NoNetwork, vibrate: false));
            }
        }

        // Code to execute when the application is launching (eg, from Start)
        // This code will not execute when the application is reactivated
        private void Application_Launching(object sender, LaunchingEventArgs e)
        {
            this.LoadFavorites();
            this.favorites.CollectionChanged += this.OnFavoritesChanged;
        }

        // Code to execute when the application is activated (brought to foreground)
        // This code will not execute when the application is first launched
        private void Application_Activated(object sender, ActivatedEventArgs e)
        {
            this.LoadFavorites();
            this.favorites.CollectionChanged += this.OnFavoritesChanged;
        }

        // Code to execute when the application is deactivated (sent to background)
        // This code will not execute when the application is closing
        private void Application_Deactivated(object sender, DeactivatedEventArgs e)
        {
            this.favorites.CollectionChanged -= this.OnFavoritesChanged;
        }

        // Code to execute when the application is closing (eg, user hit Back)
        // This code will not execute when the application is deactivated
        private void Application_Closing(object sender, ClosingEventArgs e)
        {
        }

        private void OnDiagnosticsExceptionOccurred(object sender, ExceptionOccurredEventArgs e)
        {
            string message;
            // Try to handle the exception with the image service provider.
            // For example some searches with the Flickr API throw an exception.
            if (e.Handled = CurrentImageServiceProvider.HandleException(e.Exception, out message))
            {
                this.RootFrame.Dispatcher.BeginInvoke(() => RadMessageBox.Show(string.Empty, MessageBoxButtons.OK, message, vibrate: false));
            }

            e.Cancel = e.Handled;
        }

        #region Phone application initialization

        // Avoid double-initialization
        private bool phoneApplicationInitialized = false;

        // Do not add any additional code to this method
        private void InitializePhoneApplication()
        {
            if (phoneApplicationInitialized)
                return;

            // Create the frame but don't set it as RootVisual yet; this allows the splash
            // screen to remain active until the application is ready to render.
            RootFrame = new RadPhoneApplicationFrame();
            RootFrame.Navigated += CompleteInitializePhoneApplication;

            // Ensure we don't initialize again
            phoneApplicationInitialized = true;
        }

        // Do not add any additional code to this method
        private void CompleteInitializePhoneApplication(object sender, NavigationEventArgs e)
        {
            // Set the root visual to allow the application to render
            if (RootVisual != RootFrame)
                RootVisual = RootFrame;

            // Remove this handler since it is no longer needed
            RootFrame.Navigated -= CompleteInitializePhoneApplication;
        }

        #endregion
    }
}