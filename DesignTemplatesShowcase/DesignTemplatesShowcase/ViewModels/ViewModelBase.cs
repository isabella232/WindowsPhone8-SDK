using System;
using System.ComponentModel;

namespace Telerik.DesignTemplates.WP.ViewModels
{
    public abstract class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propName)
        {
            var eh = this.PropertyChanged;
            if (eh != null)
            {
                eh(this, new PropertyChangedEventArgs(propName));
            }
        }
    }
}