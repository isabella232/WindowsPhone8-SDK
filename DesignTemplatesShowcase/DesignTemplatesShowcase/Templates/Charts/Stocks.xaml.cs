using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Telerik.Windows.Controls;
using Telerik.DesignTemplates.WP.Chart;
using System.Windows.Controls.Primitives;
using Telerik.Charting;

namespace Telerik.DesignTemplates.WP.Templates.Charts
{
    public partial class Stocks : UserControl
    {
        private CandlestickSeries candleStickSeries;
        private OhlcSeries stickSeries;
        private LineSeries lineSeries;
        private CartesianSeries[] seriesArray;
        private int seriesIndex;
        private int dataIndex;

        public Stocks()
        {
            InitializeComponent();

            this.candleStickSeries = new CandlestickSeries();
            this.stickSeries = new OhlcSeries();
            this.lineSeries = new LineSeries();

            this.seriesArray = new CartesianSeries[] { this.candleStickSeries, this.stickSeries, this.lineSeries };
            DataTemplate trackBallTemplate = this.Resources["trackBallTemplate"] as DataTemplate;
            DataTemplate trackInfoTemplate = this.Resources["trackBallInfoTemplate"] as DataTemplate;

            for (int i = 0; i < 3; i++)
            {
                OhlcSeriesBase series = this.seriesArray[i] as OhlcSeriesBase;
                if (series != null)
                {
                    series.OpenBinding = new PropertyNameDataPointBinding() { PropertyName = "Open" };
                    series.HighBinding = new PropertyNameDataPointBinding() { PropertyName = "High" };
                    series.LowBinding = new PropertyNameDataPointBinding() { PropertyName = "Low" };
                    series.CloseBinding = new PropertyNameDataPointBinding() { PropertyName = "Close" };
                    series.CategoryBinding = new PropertyNameDataPointBinding() { PropertyName = "Date" };
                }
                else
                {
                    this.lineSeries.ValueBinding = new PropertyNameDataPointBinding() { PropertyName = "AdjacentClose" };
                    this.lineSeries.CategoryBinding = new PropertyNameDataPointBinding() { PropertyName = "Date" };
                }

                this.seriesArray[i].TrackBallTemplate = trackBallTemplate;
                this.seriesArray[i].TrackBallInfoTemplate = trackInfoTemplate;
            }

            this.SetSeries();
        }

        private void InitDailyData(ChartSeries series)
        {
            this.stockChart.Zoom = new Size(36, 1);
            this.stockChart.PanOffset = new Point(0, 0);
            series.ItemsSource = FinancialDataModel.DailyData;
        }

        private void InitWeeklyData(ChartSeries series)
        {
            this.stockChart.Zoom = new Size(7, 1);
            this.stockChart.PanOffset = new Point(0, 0);
            series.ItemsSource = FinancialDataModel.WeeklyData;
        }

        private void InitMonthlyData(ChartSeries series)
        {
            this.stockChart.Zoom = new Size(2, 1);
            this.stockChart.PanOffset = new Point(0, 0);
            series.ItemsSource = FinancialDataModel.MonthlyData;
        }

        private void SetSeries()
        {
            this.stockChart.Series.Clear();
            CartesianSeries currentSeries = this.seriesArray[this.seriesIndex];

            switch (this.dataIndex)
            {
                case 0:
                    this.InitDailyData(currentSeries);
                    break;
                case 1:
                    this.InitWeeklyData(currentSeries);
                    break;
                case 2:
                    this.InitMonthlyData(currentSeries);
                    break;
            }

            // hide the series
            currentSeries.Opacity = 0;
            this.stockChart.Series.Add(currentSeries);

            // use the dispatcher to allow the series to update its visuals (chart invalidation is done asynchronously)
            this.Dispatcher.BeginInvoke(() =>
            {
                currentSeries.Opacity = 1;
            });
        }

        private void ToggleDataButtons(ToggleButton toCheck)
        {
            foreach (ToggleButton button in this.dataButtonHolder.Children)
            {
                button.IsChecked = false;
            }

            toCheck.IsChecked = true;
        }

        private void OnDaily_Click(object sender, RoutedEventArgs e)
        {
            this.dataIndex = 0;
            this.ToggleDataButtons(sender as ToggleButton);
            this.SetSeries();
        }

        private void OnWeekly_Click(object sender, RoutedEventArgs e)
        {
            this.dataIndex = 1;
            this.ToggleDataButtons(sender as ToggleButton);
            this.SetSeries();
        }

        private void OnMonthly_Click(object sender, RoutedEventArgs e)
        {
            this.dataIndex = 2;
            this.ToggleDataButtons(sender as ToggleButton);
            this.SetSeries();
        }

        private void OnCandlestick_Click(object sender, RoutedEventArgs e)
        {
            this.seriesIndex = 0;
            this.SetSeries();
        }

        private void OnStick_Click(object sender, RoutedEventArgs e)
        {
            this.seriesIndex = 1;
            this.SetSeries();
        }

        private void OnLine_Click(object sender, RoutedEventArgs e)
        {
            this.seriesIndex = 2;
            this.SetSeries();
        }

        private void TrackInfoUpdated(object sender, TrackBallInfoEventArgs e)
        {
            DateTime date = DateTime.Now;
            foreach (DataPointInfo info in e.Context.DataPointInfos)
            {
                CategoricalDataPointBase dataPoint = info.DataPoint as CategoricalDataPointBase;
                date = (DateTime)dataPoint.Category;

                if (dataPoint is OhlcDataPoint)
                {
                    OhlcDataPoint ohlcPoint = dataPoint as OhlcDataPoint;
                    info.DisplayHeader = "High: ";
                    info.DisplayContent = ohlcPoint.High;
                }
                else
                {
                    CategoricalDataPoint categoryPoint = dataPoint as CategoricalDataPoint;
                    info.DisplayHeader = "Adjacent close: ";
                    info.DisplayContent = categoryPoint.Value;
                }
            }

            e.Header = date.ToString("MMMM-yyyy");
        }
    }
}
