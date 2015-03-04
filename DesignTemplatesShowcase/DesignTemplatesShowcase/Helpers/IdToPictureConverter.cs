using System;
using System.Windows.Data;
using Telerik.DesignTemplates.WP.ViewModels;

namespace Telerik.DesignTemplates.WP.Helpers
{
    public class IdToPictureConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var id = (int)value;
            var viewModel = new MessagesViewModel();
            foreach (Person person in viewModel.People)
            {
                if (id == person.PersonId)
                {
                    return person.Picture;
                }
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}