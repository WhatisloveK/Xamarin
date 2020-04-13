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
    public class ProductsViewModel : BaseViewModel
    {
        public ObservableCollection<Product> Products { get; set; }
        public Command LoadItemsCommand { get; set; }
        public bool IsSortView { get; set; }

        public ProductsViewModel()
        {
            Title = "Browse";
            Products = new ObservableCollection<Product>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

            SubscribeMessaginCenterEvents();
        }

        public void SubscribeMessaginCenterEvents()
        {
            MessagingCenter.Subscribe<NewProductPage, Product>(this, "AddItem", async (obj, product) =>
            {
                var newProduct = product as Product;
                Products.Add(newProduct);
                await DataStore.AddItemAsync(newProduct);
            });

            MessagingCenter.Subscribe<ProductEditPage, Product>(this, "EditItem", async (obj, product) =>
            {
                var editedProduct = product as Product;
                Product old = await DataStore.GetItemAsync(product.Id);

                var index = Products.IndexOf(old);
                Products.Remove(old);
                Products.Add(editedProduct);
                await DataStore.UpdateItemAsync(editedProduct);
                
            });

            MessagingCenter.Subscribe<ProductDetailPage, Product>(this, "DeleteItem", async (obj, product) =>
            {
                var productForDeletion = product as Product;
                Products.Remove(productForDeletion);
                await DataStore.DeleteItemAsync(productForDeletion.Id);
            });
        }

        async Task ExecuteLoadItemsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Products.Clear();
                var products = await DataStore.GetItemsAsync(true);
                if (IsSortView)
                {
                    foreach (var item in products)
                    {
                        if (item.Amount > 5)
                        {
                            Products.Add(item);
                        }
                    }
                } else
                {
                    foreach (var item in products)
                    {
                        Products.Add(item);
                    }
                }
                
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }
        
    }
}