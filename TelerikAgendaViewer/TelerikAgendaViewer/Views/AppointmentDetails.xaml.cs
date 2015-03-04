using System;
using System.Linq;
using System.Windows;
using Microsoft.Phone.Controls;

namespace AgendaViewer
{
    public partial class AppointmentDetails : PhoneApplicationPage
    {
        public AppointmentDetails()
        {
            this.InitializeComponent();
            this.Loaded += new RoutedEventHandler(this.AppointmentDetails_Loaded);
        }

        private void AppointmentDetails_Loaded(object sender, RoutedEventArgs e)
        {
            this.DataContext = AppModel.SelectedAppointment;
            this.DateTimeAndDurationText.Text = String.Format("{0}, {1} - {2}",
                AppModel.SelectedAppointment.StartDate.Date.ToShortDateString(),
                AppModel.SelectedAppointment.StartDate.ToShortTimeString(),
                AppModel.SelectedAppointment.EndDate.ToShortTimeString());
        }
    }
}