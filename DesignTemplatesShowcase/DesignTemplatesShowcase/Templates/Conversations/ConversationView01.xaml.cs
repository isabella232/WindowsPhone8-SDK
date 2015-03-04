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
using Telerik.DesignTemplates.WP.ViewModels;

namespace Telerik.DesignTemplates.WP.Templates.Conversations
{
    public partial class ConversationView01 : UserControl
    {
        public ConversationView01()
        {
            InitializeComponent();
            this.SetConversationParticipants();
        }

        private void SetConversationParticipants()
        {
            MessagesViewModel viewModel = this.DataContext as MessagesViewModel;
            viewModel.ConversationBuddy = viewModel.People[0];
            viewModel.You = viewModel.People[1];
        }

        private void OnSendingMessage(object sender, ConversationViewMessageEventArgs e)
        {
            if (string.IsNullOrEmpty((e.Message as ConversationViewMessage).Text))
            {
                return;
            }
            ConversationViewMessage originalMessage = e.Message as ConversationViewMessage;
            MessagesViewModel viewModel = this.DataContext as MessagesViewModel;
            CustomMessage customMessage = new CustomMessage(originalMessage.Text, originalMessage.TimeStamp, originalMessage.Type, viewModel.You.PersonId);
            viewModel.Messages.Add(customMessage);
        }
    }
}
