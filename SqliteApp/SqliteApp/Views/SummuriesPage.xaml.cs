using SqliteApp.ViewModels;
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
    public partial class SummuriesPage: ContentPage
    {
        public SummuriesViewModel viewModel;
        public SummuriesPage()
        {
            InitializeComponent();
            BindingContext = viewModel = new SummuriesViewModel();
        }

    }
}