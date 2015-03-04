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
using Telerik.Windows.Controls;

namespace Telerik.DesignTemplates.WP.Templates.Expanders
{
    public partial class Expanders05 : UserControl
    {
        public Expanders05()
        {
            InitializeComponent();
        }

        private void ExpandExpander(object sender, System.Windows.Input.GestureEventArgs e)
        {
            HyperlinkButton b = sender as HyperlinkButton;
            RadExpanderControl expander = ElementTreeHelper.FindVisualAncestor<RadExpanderControl>(b);
            if (expander != null)
            {
                expander.IsExpanded = true;
            }
        }

        private void CollapseExpander(object sender, System.Windows.Input.GestureEventArgs e)
        {
            HyperlinkButton b = sender as HyperlinkButton;
            RadExpanderControl expander = ElementTreeHelper.FindVisualAncestor<RadExpanderControl>(b);
            if (expander != null)
            {
                expander.IsExpanded = false;
            }
        }
    }
}
