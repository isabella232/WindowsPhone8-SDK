using System;
using System.Windows.Input;
using Microsoft.Phone.Tasks;

namespace Telerik.DesignTemplates.WP.Commands
{
    public class SendAnEmailCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            var emailTask = new EmailComposeTask();
            emailTask.To = "info@company.com";
            if (parameter != null)
            {
                var email = parameter.ToString();
                emailTask.To = email;
            }
            emailTask.Show();
        }
    }
}