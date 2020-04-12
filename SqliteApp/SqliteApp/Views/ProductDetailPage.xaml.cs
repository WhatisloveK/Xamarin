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
        ProductDetailViewModel viewModel;

        public ProductDetailPage(ProductDetailViewModel viewModel)
        {
            InitializeComponent();

            BindingContext = this.viewModel = viewModel;

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

            viewModel = new ProductDetailViewModel(product);
            BindingContext = viewModel;

        }

        async void Edit_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new ProductEditPage(viewModel.Product)));
        }

        async void Delete_Clicked(object sender, EventArgs e)
        {
            MessagingCenter.Send(this, "DeleteItem", viewModel.Product);
            await Navigation.PopAsync();
        }
    }
}