using System;
using System.Windows.Input;

namespace PictureGallery.Commands
{
    public abstract class CommandBase : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            if (this.CanExecute(parameter))
            {
                this.ExecuteCore(parameter);
            }
        }

        protected abstract void ExecuteCore(object parameter);

        protected virtual void OnCanExecuteChanged()
        {
            if (this.CanExecuteChanged == null)
            {
                return;
            }

            this.CanExecuteChanged(this, EventArgs.Empty);
        }
    }
}