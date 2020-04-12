using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SqliteApp.Services;
using SqliteApp.Views;
using SqliteApp.Models;
using DL.Standard;

namespace SqliteApp
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            DependencyService.Register<ProductsRepository>();
            MainPage = new MainPage();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
