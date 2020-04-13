using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

using Xamarin.Forms;

using SqliteApp.Models;
using SqliteApp.Views;
using System.Collections.Generic;

namespace SqliteApp.ViewModels
{
    public class SummuriesViewModel: BaseViewModel
    {
        public Command CalculateSummuriesCommand { get; set; }
        public decimal MaxPrice { get; set; }
        public decimal MinPrice { get; set; }

        public SummuriesViewModel()
        {
            Title = "Summuries";
            CalculateSummuriesCommand = new Command(async () => await ExecuteCalculateSummuriesCommand());
            CalculateSummuriesCommand.Execute(null);
        }

        async Task ExecuteCalculateSummuriesCommand()
        {
            var products = await DataStore.GetItemsAsync();
            decimal max = Decimal.MinValue, min = Decimal.MaxValue;

            foreach(var item in products)
            {
                if (item.Price > max)
                {
                    max = item.Price;
                }
                if (item.Price < min)
                {
                    min = item.Price;
                }
            }
            MaxPrice = max;
            MinPrice = min;
        }
    }
}
