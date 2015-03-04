using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Telerik.DesignTemplates.WP.ViewModels;
using Telerik.Windows.Controls;

namespace Telerik.DesignTemplates.WP.Templates.Calendars
{
    public partial class Calendar01 : UserControl
    {
        private SampleAppointmentSource appointmentsSource = new SampleAppointmentSource();

        public Calendar01()
        {
            this.InitializeComponent();
            this.Loaded += this.OnLoaded;
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            this.DisplayAppointmentsForDate(DateTime.Now.Date);
        }

        private void DisplayAppointmentsForDate(DateTime date)
        {
            this.appointmentsSource.FetchData(date, date.AddDays(1));
            this.AppointmentsList.ItemsSource = this.appointmentsSource.GetAppointments((IAppointment appointment) =>
            {
                var currentAppointmentStart = appointment.StartDate;
                var currentAppointmentEnd = appointment.EndDate;
                var requiredAppointmentsStartDate = date.Date;
                var requiredAppointmentsEndDate = date.Date.AddDays(1);

                if (requiredAppointmentsEndDate > currentAppointmentStart && requiredAppointmentsStartDate < currentAppointmentEnd)
                {
                    return true;
                }

                return false;
            });
        }

        private void RadCalendar_SelectedValueChanged(object sender, ValueChangedEventArgs<object> e)
        {
            if (e.NewValue == null)
            {
                this.AppointmentsList.ItemsSource = null;
                return;
            }
            this.DisplayAppointmentsForDate((e.NewValue as DateTime?).Value);
        }
    }
}