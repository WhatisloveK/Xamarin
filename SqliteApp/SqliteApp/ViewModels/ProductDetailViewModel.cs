using System;

using SqliteApp.Models;
using Xamarin.Forms;

namespace SqliteApp.ViewModels
{
    public class ProductDetailViewModel : BaseViewModel
    {
        public Product Product { get; set; }
        public ProductDetailViewModel(Product product = null)
        {
            Title = product?.Name;
            Product = product;

        }

        
    }
}
