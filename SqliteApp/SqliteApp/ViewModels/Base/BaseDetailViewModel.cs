using System;

using SqliteApp.Models;
using SqliteApp.Views;
using Xamarin.Forms;

namespace SqliteApp.ViewModels
{
    public class BaseDetailViewModel<T> : BaseViewModel
    {
        public T Item { get; set; }
        public BaseDetailViewModel(T item)
        {
            //Title = item?.Name;
            Item = item;
        }

        
    }
}
