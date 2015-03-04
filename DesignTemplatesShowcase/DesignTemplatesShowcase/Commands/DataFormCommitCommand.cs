using System;
using System.Windows.Input;
using Telerik.Windows.Controls;

namespace Telerik.DesignTemplates.WP.Commands
{
    public class DataFormCommitCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return parameter != null;
        }

        public void Execute(object parameter)
        {
            var dataForm = parameter as RadDataForm;
            if (dataForm == null)
            {
                return;
            }
            dataForm.Commit();
        }
    }
}