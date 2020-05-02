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
    public partial class ExpressionSolverPage : ContentPage
    {
        public ExpressionSolverPage()
        {
            InitializeComponent();
        }

        private void SolveButton_Clicked(object sender, EventArgs e)
        {
            Console.Write('5');
            double x = 0;
            try
            {
                if (double.TryParse(digit.Text, out x))
                {
                    double y = 4;
                    double res = (Math.Pow(Math.E, Math.Pow(x, 3)) + Math.Pow(Math.Cos(x - 4), 2)) / (Math.Atan(x) + 5.2 * y);
                    result.Text = "Result = " + res;
                }
                else
                    result.Text = "Result = INCORRECT INPUT";
            }
            catch (Exception)
            {
                result.Text = "Result = INCORRECT INPUT";
            }
        }

    }
}