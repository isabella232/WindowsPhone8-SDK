using System;
using System.Windows.Data;
using Telerik.Windows.Controls;

namespace AgendaViewer
{
    public class AppointmentToDurationConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var appointment = (IAppointment)value;
            var hours = (appointment.EndDate - appointment.StartDate).Hours;
            if (hours > 0)
            {
                var hoursString = (hours > 1) ? " hours" : " hour";
                return string.Format("{0}{1}", hours, hoursString);
            }
            else
            {
                var minutes = (appointment.EndDate - appointment.StartDate).Minutes;
                var minutesString = (minutes > 1) ? " minutes" : " minute";
                return string.Format("{0}{1}", minutes, minutesString);
            }
        }
        
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}