using System;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using SqliteApp.Models;
using SqliteApp.ViewModels;

namespace SqliteApp.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class ProductDetailPage : ContentPage
    {
        BaseDetailViewModel<Product> viewModel;

        public ProductDetailPage(BaseDetailViewModel<Product> viewModel)
        {
            InitializeComponent();

            BindingContext = this.viewModel = viewModel;
            this.viewModel.Title = this.viewModel.Item.Name;

            MessagingCenter.Subscribe<ProductEditPage, Product>(this, "EditItem", async (obj, editedProduct) =>
            {
                await Navigation.PopAsync();
            });

        }

        public ProductDetailPage()
        {
            InitializeComponent();

            var product = new Product
            {
                Name = "TEST",
                Price = 0,
                Amount = 0
            };

            viewModel = new BaseDetailViewModel<Product>(product);
            BindingContext = viewModel;

        }

        async void Edit_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new ProductEditPage(viewModel.Item)));
        }

        async void Delete_Clicked(object sender, EventArgs e)
        {
            MessagingCenter.Send(this, "DeleteItem", viewModel.Item);
            await Navigation.PopAsync();
        }
    }
}