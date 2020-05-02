using OxyPlot;
using OxyPlot.Series;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SqliteApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PlotPage : ContentPage
    {
        public PlotPage(string points)
        {
            InitializeComponent();
            this.Title = "Plot";
            DrawPlot(points);
        }

        private void DrawPlot(string points)
        {

            var parsedPoints = Parse(points);

            var oxyPlotPoints = parsedPoints.Select<KeyValuePair<double, double>, OxyPlot.DataPoint>(item => new DataPoint(item.Key, item.Value));

            var plotModel = new PlotModel();

            var lineSeries = new LineSeries();

            lineSeries.Points.AddRange(oxyPlotPoints);

            plotModel.Series.Add(lineSeries);
            this.plotView.Model = plotModel;
        }


        private Dictionary<double, double> Parse(string values)
        {
            string[] expressions = FirsltyParse(values);

            var result = GetValues(expressions);

            return result;
        }

        private string[] FirsltyParse(string values)
        {
            string[] expressions = values.Split('\n');
            string[] result = new string[expressions.Length - 2];
            for (int i = 1; i < expressions.Length - 1; i++)
            {
                result[i - 1] = expressions[i];
            }
            return result;
        }

        private Dictionary<double, double> GetValues(string[] expressions)
        {
            List<(string key, string value)> parsedValues = GetParsedValues(expressions);

            Dictionary<double, double> result = new Dictionary<double, double>();
            foreach (var expr in parsedValues)
            {
                var key = double.Parse(expr.key);
                var value = double.Parse(expr.value);

                result.Add(key, value);
            }
            return result;
        }

        private List<(string, string)> GetParsedValues(string[] expressions)
        {
            List<(string key, string value)> parsedExpressions = new List<(string, string)>();
            for (int i = 0; i < expressions.Length; i++)
            {
                var pair = expressions[i].Split(':');
                parsedExpressions.Add((pair[0], pair[1]));
            }
            return parsedExpressions;
        }

    }
}