using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using SqliteApp.Models;
using SqliteApp.Views;
using SqliteApp.ViewModels;
using System.Windows.Input;

namespace SqliteApp.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class ProductsPage : ContentPage
    {
        ProductsViewModel viewModel;


        //private ToolbarItem toolbarItem;

        public ProductsPage()
        {
            InitializeComponent();

            BindingContext = viewModel = new ProductsViewModel();

            viewModel.IsSortView = false;
            //InitToolbarMenuItems();

        }

        void Sort_Clicked(object sender, EventArgs e)
        {
            bool isSortView = !viewModel.IsSortView;
            viewModel.IsSortView = isSortView;
            ToolbarItems[0].Text = isSortView ? "All products" : "Sort";
            viewModel.LoadItemsCommand.Execute(null);
        }


        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var product = args.SelectedItem as Product;
            if (product == null)
                return;

            await Navigation.PushAsync(new ProductDetailPage(new ProductDetailViewModel(product)));

            ProductsListView.SelectedItem = null;
        }

        async void AddItem_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new NewProductPage()));
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (viewModel.Products.Count == 0)
                viewModel.LoadItemsCommand.Execute(null);
        }
    }
}