using System;
using System.Collections.Generic;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SqliteApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FileWorkerPage : ContentPage
    {
        private double y;
        private string filePath;
        public FileWorkerPage()
        {
            InitializeComponent();
            y = 5;
            filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "result.txt");
        }

        private async void GetPlot_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PlotPage(ReadFromFile()));
        }


        private async void CalculateAndWriteToFile_click(object sender, EventArgs e)
        {
            if (double.TryParse(leftBorderInput.Text, out double from) &&
               double.TryParse(rightBorderInput.Text, out double to) &&
               double.TryParse(stepInput.Text, out double step))
            {
                try
                {
                    var result = CalcExpression(from, to, step);
                    WriteToFile(result, from, to, step);
                }
                catch (Exception ex)
                {
                    await DisplayAlert("Alarm!", ex.Message, "OK");
                }
            }
            else
            {
                await DisplayAlert("Alarm!", "Incorrect input!", "OK");
            }
        }

        private Dictionary<double, double> CalcExpression(double from, double to, double step)
        {
            if (step <= 0)
            {
                throw new Exception("Step must be great than 0!");
            }

            var results = new Dictionary<double, double>();

            for (double x = from; x < to; x += step)
            {
                double res = (Math.Pow(Math.E, Math.Pow(x, 3)) + Math.Pow(Math.Cos(x - 4), 2)) / (Math.Atan(x) + 5.2 * y);
                results.Add(x, res);
            }

            if (results.Count == 0)
            {
                throw new Exception("Result list is empty!");
            }

            return results;
        }

        private void WriteToFile(Dictionary<double, double> keyValues, double from, double to, double step)
        {
            string result = string.Empty;
            result += $"Left Border: {from}; Right Border: {to}; Step: {step}{Environment.NewLine}";
            foreach (var element in keyValues)
            {
                result += $"{element.Key}: {element.Value}{Environment.NewLine}";
            }
            File.WriteAllText(filePath, result);
        }

        private string ReadFromFile()
        {
            bool doesExist = File.Exists(filePath);
            string result;
            if (doesExist)
            {
                result = File.ReadAllText(filePath);
            }
            else
            {
                result = "File does not exists";
            }
            return result;
        }
        private async void ReadFromFile_click(object sender, EventArgs e)
        {
            await DisplayAlert("Calculation result", ReadFromFile(), "OK");
        }

    }
}