using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;
using LiveCharts;
using LiveCharts.Configurations;
using LiveCharts.Wpf;

namespace _6040CEM_GUI_Windows.View
{
    public partial class GraphsPage : Page
    {
        private DispatcherTimer _timer;
        private List<double> _an0Values;
        private List<double> _an1Values;
        private List<double> _an4Values;

        public GraphsPage()
        {
            InitializeComponent();

            _an0Values = new List<double>();
            _an1Values = new List<double>();
            _an4Values = new List<double>();

            SetupCharts();

            _timer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(1) };
            _timer.Tick += TimerOnTick;
            _timer.Start();
        }

        private void SetupCharts() // Setup the chart
        {
            var mapper = Mappers.Xy<MeasureModel>()
                .X(model => model.DateTime.Ticks)
                .Y(model => model.Value);

            Charting.For<MeasureModel>(mapper);

            // Custom label formatter for the X-axis
            Func<double, string> labelFormatter = value => new DateTime((long)value).ToString("HH:mm:ss");

            ChartAN0.Series = new SeriesCollection
            {
                new LineSeries
                {
                    Title = "AN0:",
                    Values = new ChartValues<MeasureModel>(),
                    PointGeometry = null,
                    Stroke = new SolidColorBrush(Colors.Blue)
                }
            };
            ChartAN0.AxisX.Add(new Axis { LabelFormatter = labelFormatter });

            ChartAN1.Series = new SeriesCollection
            {
                new LineSeries
                {
                    Title = "AN1:",
                    Values = new ChartValues<MeasureModel>(),
                    PointGeometry = null,
                    Stroke = new SolidColorBrush(Colors.Red)
                }
            };
            ChartAN1.AxisX.Add(new Axis { LabelFormatter = labelFormatter });

            ChartAN4.Series = new SeriesCollection
            {
                new LineSeries
                {
                    Title = "AN4:",
                    Values = new ChartValues<MeasureModel>(),
                    PointGeometry = null,
                    Stroke = new SolidColorBrush(Colors.Green)
                }
            };
            ChartAN4.AxisX.Add(new Axis { LabelFormatter = labelFormatter });
            }

        private void TimerOnTick(object sender, EventArgs eventArgs) // Taking the data from Front page
        {
            var now = DateTime.Now;

            // Update data with new values and adding data points to the charts
            double doubleValue; 
            double.TryParse(SerialComReceivedData.sensorDataAN0, out doubleValue);
            _an0Values.Add(doubleValue);
            ChartAN0.Series[0].Values.Add(new MeasureModel { DateTime = now, Value = doubleValue });
            double.TryParse(SerialComReceivedData.sensorDataAN1, out doubleValue);
            _an1Values.Add(doubleValue);
            ChartAN1.Series[0].Values.Add(new MeasureModel { DateTime = now, Value = doubleValue });
            double.TryParse(SerialComReceivedData.sensorDataAN4, out doubleValue);
            _an4Values.Add(doubleValue);
            ChartAN4.Series[0].Values.Add(new MeasureModel { DateTime = now, Value = doubleValue });


            // Remove old data points to keep the charts range limited.
            if (ChartAN0.Series[0].Values.Count > 30) // 30 seconds range
            {
                ChartAN0.Series[0].Values.RemoveAt(0);
                ChartAN1.Series[0].Values.RemoveAt(0);
                ChartAN4.Series[0].Values.RemoveAt(0);
            }
        }
    }

    public class MeasureModel // Global variables
    {
        public DateTime DateTime { get; set; }
        public double Value { get; set; }
    }

}
