using System;
using System.Linq;
using System.Windows;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Tasks;

namespace Telerik.DesignTemplates.WP.Pages
{
    public partial class About : PhoneApplicationPage
    {
        public About()
        {
            this.InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var task = new MarketplaceDetailTask();
            task.ContentIdentifier = "fd55f526-d6f7-df11-9264-00237de2db9e";
            task.ContentType = MarketplaceContentType.Applications;
            task.Show();
        }
    }
}