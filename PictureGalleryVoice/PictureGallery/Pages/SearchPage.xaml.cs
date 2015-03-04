﻿using System;
using System.Windows.Controls;
using System.Windows.Input;
using PictureGallery.ViewModels;
using System.ComponentModel;
using Microsoft.Phone.Shell;
using System.Windows.Data;
using Telerik.Windows.Controls;

namespace PictureGallery.Pages
{
    public partial class SearchPage : PageBase
    {
        private PhotoSearchViewModel photoSearchViewModel;
        private AuthorSearchViewModel authorSearchViewModel;
        private bool isTextBoxFocused;

        public SearchPage()
        {
            InitializeComponent();
            this.photoSearchViewModel = App.Instance.GetCurrentViewModel<PhotoSearchViewModel>();
            if (this.photoSearchViewModel == null)
            {
                this.photoSearchViewModel = new PhotoSearchViewModel() { Text = "" };
            }
            else
            {
                this.searchTextBox.Text = this.photoSearchViewModel.Text;
                this.DataContext = this.photoSearchViewModel;
            }

            this.photoSearchViewModel.PropertyChanged += this.OnPhotoSearchTextChanged;

            this.authorSearchViewModel = new AuthorSearchViewModel() { Text = "" };

            Binding binding = new Binding("SelectedIndex");
            binding.Source = this.pivot;
            binding.Converter = new Pages.SearchIndexToAppbarConverter();
            this.SetBinding(SearchPage.ApplicationBarProperty, binding);
        }

        private void OnPhotoSearchTextChanged(object sender, PropertyChangedEventArgs e)
        {
            App.Instance.UpdatePinButton();
        }

        private SearchViewModel SearchViewModel
        {
            get
            {
                return this.DataContext as SearchViewModel;
            }
        }

        protected override ImageServiceViewModel FavoriteViewModel
        {
            get 
            {
                return this.photoSearchViewModel.ActualViewModel;
            }
        }

        private void OnSearchTextBoxLostFocus(object sender, System.Windows.RoutedEventArgs e)
        {
            this.isTextBoxFocused = false;

            this.UpdateAppBar(!string.IsNullOrEmpty(this.photoSearchViewModel.Text));
        }

        private void OnSearchTextBoxGotFocus(object sender, System.Windows.RoutedEventArgs e)
        {
            this.searchTextBox.SelectAll();
            this.isTextBoxFocused = true;

            this.UpdateAppBar(false);
        }

        public override bool CompareViewModel(ImageServiceViewModel viewModel)
        {
            SearchViewModel search = viewModel as SearchViewModel;
            if (search == null)
            {
                return false;
            }

            return search.Text == this.photoSearchViewModel.Text;
        }

        private void OnTextBoxKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                this.Search();
            }
        }

        private void UpdateAppBar(bool isVisible)
        {
            if (this.ApplicationBar == null)
            {
                return;
            }

            this.ApplicationBar.IsVisible = isVisible;
        }

        private void Search()
        {
            this.Focus();
            this.SearchViewModel.Text = this.searchTextBox.Text;
            App.Instance.UpdatePinButton();
            this.UpdateAppBar(!string.IsNullOrEmpty(this.searchTextBox.Text));
        }

        private void OnPivotSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string currentSearch = "";
            if (this.SearchViewModel != null)
            {
                currentSearch = this.SearchViewModel.Text;
            }

            if (this.pivot.SelectedIndex == 0)
            {
                this.DataContext = this.photoSearchViewModel;
            }
            else
            {
                this.DataContext = this.authorSearchViewModel;
            }

            this.SearchViewModel.Text = currentSearch;

            if (string.IsNullOrEmpty(this.SearchViewModel.Text))
            {
                this.searchTextBox.Focus();
            }

            this.Dispatcher.BeginInvoke(() => 
                {
                    this.UpdateAppBar(!this.isTextBoxFocused);
                });
        }

        protected override void OnBackKeyPress(CancelEventArgs e)
        {
            base.OnBackKeyPress(e);

            App.Instance.ClearViewModel<PhotoSearchViewModel>();
        }

        private void OnAuthorTapped(object sender, EventArgs e)
        {
            App.Instance.SetCurrentViewModel<AuthorViewModel>(this.authorSearchViewModel.Author);
            this.NavigationService.Navigate(App.AuthorPage);
        }

        private void OnPhotoTapped(object sender, EventArgs e)
        {
            App.Instance.SetCurrentViewModel<PhotoListViewModel>(this.photoSearchViewModel.PhotoList);
            this.NavigationService.Navigate(App.PhotoPage);
        }

        private void OnSearch(object sender, EventArgs e)
        {
            this.Search();
        }
    }
}