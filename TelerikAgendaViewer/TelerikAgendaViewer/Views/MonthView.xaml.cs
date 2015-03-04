using System;
using System.Linq;
using System.Windows;
using Microsoft.Phone.Controls;
using Telerik.Windows.Controls;

namespace AgendaViewer
{
    public partial class MonthView : PhoneApplicationPage
    {
        public MonthView()
        {
            this.InitializeComponent();
            this.Loaded += new RoutedEventHandler(this.MainPage_Loaded);
        }

        private void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            this.RadCalendar1.AppointmentSource = new SampleAppointmentSource();
            this.RadCalendar1.SelectedValueChanged += new EventHandler<ValueChangedEventArgs<object>>(this.RadCalendar1_SelectedValueChanged);
        }

        private void RadCalendar1_SelectedValueChanged(object sender, ValueChangedEventArgs<object> e)
        {
            if (this.RadCalendar1.SelectedValue.HasValue)
            {
                AppModel.SelectedDate = this.RadCalendar1.SelectedValue.Value;
                this.NavigationService.Navigate(new Uri("/Views/AgendaView.xaml", UriKind.RelativeOrAbsolute));
            }
        }

        private void RadCalendar1_SelectedValueChanged_1(object sender, ValueChangedEventArgs<object> e)
        {
        }
    }
}