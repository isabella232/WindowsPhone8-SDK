using System;
using System.Windows.Input;
using Microsoft.Phone.Tasks;

namespace Telerik.DesignTemplates.WP.Commands
{
    public class RateThisAppCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            var reviewTask = new MarketplaceReviewTask();
            reviewTask.Show();
        }
    }
}