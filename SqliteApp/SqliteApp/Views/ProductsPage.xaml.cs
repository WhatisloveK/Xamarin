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

        private ToolbarItem toolbarItem;

        public ProductsPage()
        {
            InitializeComponent();

            BindingContext = viewModel = new ProductsViewModel();

            InitToolbarMenuItems();

        }

        void OnEditItem_Clicked(object sender, EventArgs e)
        {
            
        }

        public void ChangeToolbarItems()
        {
            if (ToolbarItems.Count == 2)
            {
                ToolbarItems.RemoveAt(1);
            } 
            else
            {
                ToolbarItems.Add(toolbarItem);
            }
        }

        public void InitToolbarMenuItems()
        {
            toolbarItem = new ToolbarItem();
            toolbarItem.Text = "Edit";
            toolbarItem.Clicked += OnEditItem_Clicked;
            
            //var item2 = new ToolbarItem();
            //item2.Text = "Delete";
            //item2.Clicked += OnEditItem_Clicked;
            //toolbarItems.Add(item2);
        }

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var product = args.SelectedItem as Product;
            if (product == null)
                return;
            // < ToolbarItem Text = "Add" Clicked = "AddItem_Clicked" />
            ChangeToolbarItems();

            //await Navigation.PushAsync(new ProductDetailPage(new ProductDetailViewModel(product)));

           // ProductsListView.SelectedItem = null;
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