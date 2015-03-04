using System;
using System.Linq;
using Microsoft.Phone.Controls;

namespace Telerik.DesignTemplates.WP.Pages
{
    public partial class ZoomImage : PhoneApplicationPage
    {
        public ZoomImage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            base.OnNavigatedTo(e);

            if (this.NavigationContext.QueryString.ContainsKey("item"))
            {
                var queryString = this.NavigationContext.QueryString["item"];
                this.Dispatcher.BeginInvoke(() =>
                {
                    foreach (var item in this.slideView.ItemsSource)
                    {
                        if (item.ToString().Equals(queryString))
                        {
                            this.slideView.SelectedItem = item;
                            return;
                        }
                    }
                });
            }
        }
    }
}