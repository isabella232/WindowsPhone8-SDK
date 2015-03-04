using System;
using System.Windows;
using System.Windows.Threading;
using Microsoft.Phone.Controls;
using Telerik.Windows.Controls;
using Telerik.Windows.Data;

namespace AgendaViewer
{
    public partial class AgendaView : PhoneApplicationPage
    {
        private SampleAppointmentSource agendaSource;
        private DispatcherTimer agendaTitleTimer;
        private string agendaViewTitle = String.Empty;
        private DateTime initialDate;

        public AgendaView()
        {
            this.InitializeComponent();

            this.initialDate = AppModel.SelectedDate;

            this.RadJumpList1.GroupHeaderTemplateSelector = new HeaderTemplateSelector() { RootGroupTemplate = (DataTemplate)this.Resources["GroupDateHeaderTemplate"], NestedGroupTemplate = (DataTemplate)this.Resources["GroupTimeHeaderTemplate"] };

            this.agendaTitleTimer = new DispatcherTimer();
            this.agendaTitleTimer.Interval = TimeSpan.FromMilliseconds(400);
            this.agendaTitleTimer.Tick += new EventHandler(this.timer_Tick);

            this.Loaded += new RoutedEventHandler(this.AgendaView_Loaded);
        }

        private void AgendaView_Loaded(object sender, RoutedEventArgs e)
        {
            this.PrepareAgendaSource();

            this.ConfigureAgendalist();

            this.RadJumpList1.ScrollStateChanged += new EventHandler<ScrollStateChangedEventArgs>(this.RadJumpList1_ScrollStateChanged);
            this.SetCurrentDateText(this.initialDate);
        }

        private void ConfigureAgendalist()
        {
            this.RadJumpList1.SortDescriptors.Add(new GenericSortDescriptor<IAppointment, DateTime>(a => a.StartDate));
            this.RadJumpList1.GroupDescriptors.Add(new GenericGroupDescriptor<IAppointment, DateTime>(a => a.StartDate.Date));
            this.RadJumpList1.GroupDescriptors.Add(new GenericGroupDescriptor<IAppointment, DateTime>(a => a.StartDate));
        }

        private void PrepareAgendaSource()
        {
            this.agendaSource = new SampleAppointmentSource();

            this.agendaSource.DataLoaded += new EventHandler(this.agendaSource_DataLoaded);

            // populate the agenda source with all appointments
            // may cause problems with large data
            this.agendaSource.FetchData(DateTime.MinValue, DateTime.MaxValue);
        }

        private void RadJumpList1_ScrollStateChanged(object sender, ScrollStateChangedEventArgs e)
        {
            if (e.NewState == ScrollState.Scrolling ||
                e.NewState == ScrollState.Flicking)
            {
                this.MonitorTopVisibleGroup();
            }
            else
            {
                this.StopMonitoringTopVisibleGroup();
                this.UpdateAgendaViewTitle();
            }
        }

        private void StopMonitoringTopVisibleGroup()
        {
            this.agendaTitleTimer.Stop();
        }

        private void UpdateAgendaViewTitle()
        {
            var topVisibleItem = this.RadJumpList1.TopVisibleItem;

            var topGroup = topVisibleItem as IDataSourceGroup;
            if (topGroup != null)
            {
                this.SetCurrentDateText((DateTime)((DataGroup)topGroup.Value).Key);
            }
            else
            {
                var topAppointment = topVisibleItem as IDataSourceItem;
                if (topAppointment != null)
                {
                    this.SetCurrentDateText(((IAppointment)topAppointment.Value).StartDate);
                }
            }
        }

        private void SetCurrentDateText(DateTime dateTime)
        {
            var newText = this.FormateDayDate(dateTime);
            if (!newText.Equals(this.agendaViewTitle))
            {
                this.agendaViewTitle = newText;
                this.AgendaTitle.Text = newText;
            }
        }

        private string FormateDayDate(DateTime date)
        {
            if (DateTime.Now.Date == date.Date)
            {
                return "TODAY";
            }

            return date.ToString("dddd, MMMM dd, yyyy", System.Globalization.CultureInfo.CurrentUICulture).ToUpperInvariant();
        }

        private void agendaSource_DataLoaded(object sender, EventArgs e)
        {
            this.RadJumpList1.ItemsSource = this.agendaSource.GetAppointments((IAppointment appointment) =>
            {
                return true;
            });

            this.BringDayIntoView(this.initialDate.Date);
        }

        private void BringDayIntoView(DateTime dateTime)
        {
            foreach (DataGroup group in this.RadJumpList1.Groups)
            {
                if (((DateTime)group.Key).Date == dateTime.Date)
                {
                    this.RadJumpList1.BringIntoView(group);
                    break;
                }
            }
        }

        private void RadJumpList1_ItemTap(object sender, ListBoxItemTapEventArgs e)
        {
            var appointment = (SampleAppointment)e.Item.Content;
            if (appointment != null)
            {
                AppModel.SelectedAppointment = appointment;
                this.NavigationService.Navigate(new Uri("/Views/AppointmentDetails.xaml", UriKind.RelativeOrAbsolute));
            }
        }

        private void MonitorTopVisibleGroup()
        {
            this.agendaTitleTimer.Start();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            this.UpdateAgendaViewTitle();
        }

        private void MonthView_Click(object sender, EventArgs e)
        {
            this.NavigationService.GoBack();
        }

        private void TodayButton_Click(object sender, EventArgs e)
        {
            this.BringDayIntoView(DateTime.Now);
            this.SetCurrentDateText(DateTime.Now);
        }
    }

    public class HeaderTemplateSelector : DataTemplateSelector
    {
        public DataTemplate RootGroupTemplate { get; set; }

        public DataTemplate NestedGroupTemplate { get; set; }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            var dataGroup = item as DataGroup;
            if (dataGroup.HasChildGroups)
            {
                return this.RootGroupTemplate;
            }
            else
            {
                return this.NestedGroupTemplate;
            }
        }
    }
}