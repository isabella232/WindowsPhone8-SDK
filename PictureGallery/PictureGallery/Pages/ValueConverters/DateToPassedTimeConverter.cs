using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using Microsoft.Phone.Shell;

namespace PictureGallery.Pages
{
    public class SearchIndexToAppbarConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var index = (int)value;

            if (index == 0)
            {
                object result = Application.Current.Resources["PinAppBar"] as IApplicationBar;
                App.Current.RootVisual.Dispatcher.BeginInvoke(() => App.Instance.UpdatePinButton());

                return result;
            }
            else
            {
                return null;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class DateToPassedTimeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var now = DateTime.Now;
            var commentDate = (DateTime)value;
            var difference = now - commentDate;

            string timeComponent;
            double timeComponentValue;

            if (difference.TotalMinutes < 60)
            {
                timeComponentValue = Math.Round(difference.TotalMinutes);
                timeComponent = "minute";
            }
            else if (difference.TotalHours < 24)
            {
                timeComponentValue = Math.Round(difference.TotalHours);
                timeComponent = "hour";
            }
            else if (difference.TotalDays < 31)
            {
                timeComponentValue = Math.Round(difference.TotalDays);
                timeComponent = "day";
            }
            else
            {
                timeComponentValue = Math.Round(difference.TotalDays / 31);
                timeComponent = "month";
            }

            return string.Format("({0} {1}{2} {3})", timeComponentValue.ToString(), timeComponent, (timeComponentValue == 1 ? "" : "s"), "ago");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
