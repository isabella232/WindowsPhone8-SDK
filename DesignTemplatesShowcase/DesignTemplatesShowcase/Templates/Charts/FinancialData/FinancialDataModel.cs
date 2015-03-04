using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Collections.Generic;
using System.Windows.Resources;
using System.IO;
using System.Globalization;

namespace Telerik.DesignTemplates.WP.Chart
{
    public static class FinancialDataModel
    {
        private static List<OhlcModel> dailyData;
        private static List<OhlcModel> weeklyData;
        private static List<OhlcModel> monthlyData;

        public static List<OhlcModel> DailyData
        {
            get
            {
                if (dailyData == null)
                {
                    dailyData = new List<OhlcModel>();
                    LoadData(dailyData, "Daily");
                }

                return dailyData;
            }
        }

        public static List<OhlcModel> WeeklyData
        {
            get
            {
                if (weeklyData == null)
                {
                    weeklyData = new List<OhlcModel>();
                    LoadData(weeklyData, "Weekly");
                }

                return weeklyData;
            }
        }

        public static List<OhlcModel> MonthlyData
        {
            get
            {
                if (monthlyData == null)
                {
                    monthlyData = new List<OhlcModel>();
                    LoadData(monthlyData, "Monthly");
                }

                return monthlyData;
            }
        }

        private static void LoadData(List<OhlcModel> list, string fileName)
        {
            StreamResourceInfo resource = Application.GetResourceStream(new Uri("Templates/Charts/FinancialData/" + fileName + ".txt", UriKind.RelativeOrAbsolute));
            using (StreamReader reader = new StreamReader(resource.Stream))
            {
                string line = string.Empty;
                while (!string.IsNullOrEmpty(line = reader.ReadLine()))
                {
                    string[] values = line.Split('\t');
                    double[] args = new double[values.Length - 1];

                    // first argument is the Date, start from the second splitted value
                    for(int i = 1; i < values.Length; i++)
                    {
                        args[i - 1] = double.Parse(values[i], CultureInfo.InvariantCulture);
                    }

                    OhlcModel model = new OhlcModel(0.5, args);
                    model.Date = DateTime.Parse(values[0], CultureInfo.InvariantCulture);

                    list.Add(model);
                }
            }
        }
    }
}
