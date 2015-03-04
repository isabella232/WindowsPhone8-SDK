using System;
using System.Windows;
using Telerik.DesignTemplates.WP.ViewModels;
using Telerik.Windows.Controls;

namespace Telerik.DesignTemplates.WP.Helpers
{
    public class MessageItemTemplateSelector : DataTemplateSelector
    {
        public DataTemplate IncomingTemplate { get; set; }

        public DataTemplate OutgoingTemplate { get; set; }

        public DataTemplate EmptyDataItemTemplate { get; set; }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            var listBoxItem = container as RadDataBoundListBoxItem;
            if (listBoxItem.AssociatedDataItem.Previous is Telerik.Windows.Data.IDataSourceGroup)
            {
                return this.EmptyDataItemTemplate;
            }
            var message = item as CustomMessage;
            if (message.Type == ConversationViewMessageType.Incoming)
            {
                return this.IncomingTemplate;
            }
            else
            {
                return this.OutgoingTemplate;
            }
        }
    }
}
