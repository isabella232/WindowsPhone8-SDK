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
using Telerik.Windows.Controls.Gestures;

namespace Telerik.DesignTemplates.WP.Templates.Charts
{
    public partial class Indicators : UserControl
    {
        private int trendsIndex = 0;
        private int indicatorsIndex = 0;

        private const int TrendsCount = 6;
        private const int IndicatorsCount = 11;
        private const int SticksCount = 70;

        public Indicators()
        {
            InitializeComponent();

            this.UpdateTrendButtonsState();
            this.UpdateIndicatorButtonsState();
            this.InitData();
            this.SetTrends();
            this.SetIndicators();
            this.UpdatePanZoomBehaviors(this.trendsChart, this.indicatorsChart);
        }

        private void InitData()
        {
            OhlcSeries stickSeries = new OhlcSeries();

            stickSeries.OpenBinding = new PropertyNameDataPointBinding() { PropertyName = "Open" };
            stickSeries.HighBinding = new PropertyNameDataPointBinding() { PropertyName = "High" };
            stickSeries.LowBinding = new PropertyNameDataPointBinding() { PropertyName = "Low" };
            stickSeries.CloseBinding = new PropertyNameDataPointBinding() { PropertyName = "Close" };
            stickSeries.CategoryBinding = new PropertyNameDataPointBinding() { PropertyName = "Date" };

            stickSeries.ItemsSource = FinancialDataModel.DailyData.Take(SticksCount);

            this.trendsChart.Zoom = new Size(7, 1);
            this.trendsChart.PanOffset = new Point(0, 0);

            this.trendsChart.Series.Add(stickSeries);

            this.indicatorsChart.Zoom = new Size(7, 1);
            this.indicatorsChart.PanOffset = new Point(0, 0);
        }

        private void SetTrends()
        {
            ValuePeriodIndicatorBase trendBase;
            switch (this.trendsIndex)
            {
                case 0:
                    trendBase = new MovingAverageIndicator();
                    break;
                case 1:
                    trendBase = new ExponentialMovingAverageIndicator();
                    break;
                case 2:
                    trendBase = new ModifiedMovingAverageIndicator();
                    break;
                case 3:
                    trendBase = new ModifiedExponentialMovingAverageIndicator();
                    break;
                case 4:
                    trendBase = new WeightedMovingAverageIndicator();
                    break;
                default:
                    trendBase = new AdaptiveMovingAverageKaufmanIndicator();
                    break;
            }

            trendBase.CategoryBinding = new PropertyNameDataPointBinding() { PropertyName = "Date" };
            trendBase.ValueBinding = new PropertyNameDataPointBinding() { PropertyName = "Close" };
            trendBase.Stroke = new SolidColorBrush(Colors.Red);
            trendBase.StrokeThickness = 1;
            trendBase.Period = 5;

            trendBase.ItemsSource = FinancialDataModel.DailyData.Take(SticksCount);

            this.SetTrendText();

            this.trendsChart.Indicators.Clear();

            if (this.trendsIndex == 5)
            {
                AdaptiveMovingAverageKaufmanIndicator adaptiveMovingAverageKaufmanIndicator = trendBase as AdaptiveMovingAverageKaufmanIndicator;
                adaptiveMovingAverageKaufmanIndicator.SlowPeriod = 10;
                adaptiveMovingAverageKaufmanIndicator.FastPeriod = 4;
                adaptiveMovingAverageKaufmanIndicator.Period = 2;
                this.trendsChart.Indicators.Add(adaptiveMovingAverageKaufmanIndicator);
                return;
            }

            this.trendsChart.Indicators.Add(trendBase);
        }

        private void SetTrendText()
        {
            switch (this.trendsIndex)
            {
                case 0:
                    this.trendText.Text = "MA (5)";
                    break;
                case 1:
                    this.trendText.Text = "Exponential MA (5)";
                    break;
                case 2:
                    this.trendText.Text = "Modified MA (5)";
                    break;
                case 3:
                    this.trendText.Text = "Modified Exponential MA (5)";
                    break;
                case 4:
                    this.trendText.Text = "Weighted MA (5)";
                    break;
                case 5:
                    this.trendText.Text = "Adaptive MA Kaufman (10, 4, 2)";
                    break;
            }
        }

        private void SetIndicators()
        {
            switch (this.indicatorsIndex)
            {
                case 0:
                    this.CreateAverageTrueRangeIndicator();
                    break;
                case 1:
                    this.CreateCommodityChannelIndexIndicator();
                    break;
                case 2:
                    this.CreateMacdIndicator();
                    break;
                case 3:
                    this.CreateMomentumIndicator();
                    break;
                case 4:
                    this.CreateOscillatorIndicator();
                    break;
                case 5:
                    this.CreateRateOfChangeIndicator();
                    break;
                case 6:
                    this.CreateRelativeMomentumIndexIndicator();
                    break;
                case 7:
                    this.CreateRelativeStrengthIndexIndicator();
                    break;
                case 8:
                    this.CreateStochasticFastIndicator();
                    break;
                case 9:
                    this.CreateStochasticSlowIndicator();
                    break;
                case 10:
                    this.CreateTrueRangeIndicator();
                    break;
            }

            this.SetIndicatorText();
        }

        private void SetIndicatorText()
        {
            switch (this.indicatorsIndex)
            {
                case 0:
                    this.indicatorText.Text = "Average True Range (5)";
                    break;
                case 1:
                    this.indicatorText.Text = "Commodity Channel Index (5)";
                    break;
                case 2:
                    this.indicatorText.Text = "MACD (12, 9, 6)";
                    break;
                case 3:
                    this.indicatorText.Text = "Momentum (5)";
                    break;
                case 4:
                    this.indicatorText.Text = "Oscillator (8, 4)";
                    break;
                case 5:
                    this.indicatorText.Text = "Rate Of Change (8)";
                    break;
                case 6:
                    this.indicatorText.Text = "Relative Momentum Index (8)";
                    break;
                case 7:
                    this.indicatorText.Text = "Relative Strength Index (8)";
                    break;
                case 8:
                    this.indicatorText.Text = "Stochastic Fast (14, 3)";
                    break;
                case 9:
                    this.indicatorText.Text = "Stochastic Slow (14, 3, 3)";
                    break;
                case 10:
                    this.indicatorText.Text = "True Range";
                    break;
            }
        }

        private void UpdatePanZoomBehaviors(RadChartBase firstChart, RadChartBase secondChart)
        {
            (firstChart.Behaviors[0] as ExtendedPanZoomBehavior).SecondChart = secondChart;
            (secondChart.Behaviors[0] as ExtendedPanZoomBehavior).SecondChart = firstChart;
        }

        private void leftTrend_Click(object sender, RoutedEventArgs e)
        {
            this.trendsIndex--;
            this.UpdateTrendButtonsState();
            this.SetTrends();
        }

        private void rightTrend_Click(object sender, RoutedEventArgs e)
        {
            this.trendsIndex++;
            UpdateTrendButtonsState();
            SetTrends();
        }

        private void UpdateTrendButtonsState()
        {
            this.leftTrend.IsEnabled = this.trendsIndex > 0;
            this.rightTrend.IsEnabled = this.trendsIndex < TrendsCount - 1;
        }

        private void leftIndicator_Click(object sender, RoutedEventArgs e)
        {
            this.indicatorsIndex--;
            this.UpdateIndicatorButtonsState();
            this.SetIndicators();
        }

        private void rightIndicator_Click(object sender, RoutedEventArgs e)
        {
            this.indicatorsIndex++;
            this.UpdateIndicatorButtonsState();
            this.SetIndicators();
        }

        private void UpdateIndicatorButtonsState()
        {
            this.leftIndicator.IsEnabled = this.indicatorsIndex > 0;
            this.rightIndicator.IsEnabled = this.indicatorsIndex < IndicatorsCount - 1;
        }

        private void CreateAverageTrueRangeIndicator()
        {
            AverageTrueRangeIndicator indicator = new AverageTrueRangeIndicator();
            indicator.Period = 5;
            this.SetupIndicator(indicator);

            this.indicatorsChart.Indicators.Clear();
            this.indicatorsChart.Indicators.Add(indicator);
        }

        private void CreateCommodityChannelIndexIndicator()
        {
            CommodityChannelIndexIndicator indicator = new CommodityChannelIndexIndicator();
            indicator.Period = 5;
            this.SetupIndicator(indicator);

            this.indicatorsChart.Indicators.Clear();
            this.indicatorsChart.Indicators.Add(indicator);
        }

        private void CreateMacdIndicator()
        {
            MacdIndicator indicator = new MacdIndicator();
            indicator.LongPeriod = 12;
            indicator.ShortPeriod = 9;
            indicator.SignalPeriod = 6;
            indicator.SignalStroke = new SolidColorBrush(Colors.Red);

            this.SetupIndicator(indicator);

            this.indicatorsChart.Indicators.Clear();
            this.indicatorsChart.Indicators.Add(indicator);
        }

        private void CreateMomentumIndicator()
        {
            MomentumIndicator indicator = new MomentumIndicator();
            indicator.Period = 8;
            this.SetupIndicator(indicator);

            this.indicatorsChart.Indicators.Clear();
            this.indicatorsChart.Indicators.Add(indicator);
        }

        private void CreateOscillatorIndicator()
        {
            OscillatorIndicator indicator = new OscillatorIndicator();
            indicator.LongPeriod = 8;
            indicator.ShortPeriod = 4;
            this.SetupIndicator(indicator);

            this.indicatorsChart.Indicators.Clear();
            this.indicatorsChart.Indicators.Add(indicator);
        }

        private void CreateRateOfChangeIndicator()
        {
            RateOfChangeIndicator indicator = new RateOfChangeIndicator();
            indicator.Period = 8;
            this.SetupIndicator(indicator);

            this.indicatorsChart.Indicators.Clear();
            this.indicatorsChart.Indicators.Add(indicator);
        }

        private void CreateRelativeMomentumIndexIndicator()
        {
            RelativeMomentumIndexIndicator indicator = new RelativeMomentumIndexIndicator();
            indicator.Period = 8;
            this.SetupIndicator(indicator);

            this.indicatorsChart.Indicators.Clear();
            this.indicatorsChart.Indicators.Add(indicator);
        }

        private void CreateRelativeStrengthIndexIndicator()
        {
            RelativeStrengthIndexIndicator indicator = new RelativeStrengthIndexIndicator();
            indicator.Period = 8;
            this.SetupIndicator(indicator);

            this.indicatorsChart.Indicators.Clear();
            this.indicatorsChart.Indicators.Add(indicator);
        }

        private void CreateStochasticFastIndicator()
        {
            StochasticFastIndicator indicator = new StochasticFastIndicator();
            indicator.MainPeriod = 14;
            indicator.SignalPeriod = 3;
            indicator.SignalStroke = new SolidColorBrush(Colors.Red);

            this.SetupIndicator(indicator);

            this.indicatorsChart.Indicators.Clear();
            this.indicatorsChart.Indicators.Add(indicator);
        }

        private void CreateStochasticSlowIndicator()
        {
            StochasticSlowIndicator indicator = new StochasticSlowIndicator();
            indicator.MainPeriod = 14;
            indicator.SignalPeriod = 3;
            indicator.SlowingPeriod = 3;
            indicator.SignalStroke = new SolidColorBrush(Colors.Red);

            this.SetupIndicator(indicator);

            this.indicatorsChart.Indicators.Clear();
            this.indicatorsChart.Indicators.Add(indicator);
        }

        private void CreateTrueRangeIndicator()
        {
            TrueRangeIndicator indicator = new TrueRangeIndicator();
            this.SetupIndicator(indicator);

            this.indicatorsChart.Indicators.Clear();
            this.indicatorsChart.Indicators.Add(indicator);
        }

        private void SetupIndicator(HighLowCloseIndicatorBase indicator)
        {
            indicator.Stroke = (Brush)Application.Current.Resources["PhoneForegroundBrush"];
            indicator.StrokeThickness = 1;
            indicator.CategoryBinding = new PropertyNameDataPointBinding() { PropertyName = "Date" };
            indicator.HighBinding = new PropertyNameDataPointBinding() { PropertyName = "High" };
            indicator.LowBinding = new PropertyNameDataPointBinding() { PropertyName = "Low" };
            indicator.CloseBinding = new PropertyNameDataPointBinding() { PropertyName = "Close" };
            indicator.ItemsSource = FinancialDataModel.DailyData.Take(SticksCount);
        }

        private void SetupIndicator(ValueIndicatorBase indicator)
        {
            indicator.Stroke = (Brush)Application.Current.Resources["PhoneForegroundBrush"];
            indicator.StrokeThickness = 1;
            indicator.CategoryBinding = new PropertyNameDataPointBinding() { PropertyName = "Date" };
            indicator.ValueBinding = new PropertyNameDataPointBinding() { PropertyName = "Close" };
            indicator.ItemsSource = FinancialDataModel.DailyData.Take(SticksCount);
        }
    }

    public class ExtendedPanZoomBehavior : ChartPanAndZoomBehavior
    {
        public ExtendedPanZoomBehavior()
        {
        }

        public RadChartBase SecondChart
        {
            get;
            set;
        }

        protected override bool OnGesture(Gesture gesture)
        {
            bool handle = base.OnGesture(gesture);
            if (handle)
            {
                this.SecondChart.Zoom = this.Chart.Zoom;
                this.SecondChart.PanOffset = this.Chart.PanOffset;
            }

            return handle;
        }
    }
}
