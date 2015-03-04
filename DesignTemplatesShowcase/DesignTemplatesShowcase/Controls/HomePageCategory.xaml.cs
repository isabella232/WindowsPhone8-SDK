using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Telerik.DesignTemplates.WP.Controls
{
    public partial class HomePageCategory : UserControl
    {
        public static readonly DependencyProperty CountProperty =
            DependencyProperty.Register("Count", typeof(int), typeof(HomePageCategory), new PropertyMetadata(0));

        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register("Title", typeof(string), typeof(HomePageCategory), new PropertyMetadata(string.Empty));

        public HomePageCategory()
        {
            this.InitializeComponent();
        }

        public int Count
        {
            get
            {
                return (int)this.GetValue(CountProperty);
            }
            set
            {
                this.SetValue(CountProperty, value);
            }
        }

        public string Title
        {
            get
            {
                return (string)this.GetValue(TitleProperty);
            }
            set
            {
                this.SetValue(TitleProperty, value);
            }
        }
    }
}