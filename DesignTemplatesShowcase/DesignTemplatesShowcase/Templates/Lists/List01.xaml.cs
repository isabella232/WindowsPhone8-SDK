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
using Telerik.Windows.Controls;
using Microsoft.Phone.Shell;

namespace Telerik.DesignTemplates.WP.Templates.Lists
{
    public partial class List01 : UserControl
    {
        public List01()
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
            ApplicationBarIconButton selectButton = new ApplicationBarIconButton(new Uri("Images/Icons/select.png", UriKind.RelativeOrAbsolute));
            selectButton.Text = "select";
            selectButton.Click += delegate
            {
                if (this.MainPivot.SelectedIndex == 0)
                {
                    this.DataBoundListBox1.IsCheckModeActive ^= true;
                }
                else
                {
                    this.DataBoundListBox2.IsCheckModeActive ^= true;
                }
            };
            MainViewModel.Instance.AddButtonToTemplatePageApplicationBar(selectButton, 1);
        }

        private void RadDataBoundListBox_IsCheckModeActiveChanged(object sender, Telerik.Windows.Controls.IsCheckModeActiveChangedEventArgs e)
        {
            RadDataBoundListBox listBox = sender as RadDataBoundListBox;
            if (listBox == null)
            {
                return;
            }
            if (listBox.IsCheckModeActive)
            {
                this.MainPivot.IsLocked = true;
            }
            else
            {
                this.MainPivot.IsLocked = false;
            }
        }
    }
}
