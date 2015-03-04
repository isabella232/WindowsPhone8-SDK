using System;
using System.Windows;
using Telerik.DesignTemplates.WP.ViewModels;
using Telerik.Windows.Controls;
using Telerik.Windows.Data;

namespace Telerik.DesignTemplates.WP.Helpers
{
    public class MessageGroupTemplateSelector : DataTemplateSelector
    {
        private int uniqueGroupIdentifier = 0;

        public DataTemplate IncomingTemplate { get; set; }

        public DataTemplate OutgoingTemplate { get; set; }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            var currentGroup = item as DataGroup;
            var firstMessageInGroup = currentGroup.Key as CustomMessage;
            switch (firstMessageInGroup.Type)
            {
                case ConversationViewMessageType.Incoming:
                    return this.IncomingTemplate;
                case ConversationViewMessageType.Outgoing:
                    return this.OutgoingTemplate;
            }

            return null;
        }
    }
}
