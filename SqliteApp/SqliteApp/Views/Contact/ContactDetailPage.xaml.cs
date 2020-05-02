using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contact = Plugin.ContactService.Shared.Contact;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SqliteApp.ViewModels;

namespace SqliteApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ContactDetailPage : ContentPage
    {
        BaseDetailViewModel<Contact> viewModel;
        public ContactDetailPage(BaseDetailViewModel<Contact> viewModel)
        {
            InitializeComponent();
            BindingContext = this.viewModel = viewModel;
            this.viewModel.Title = viewModel.Item.Name;
            
        }
    }
}