using SqliteApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contact = Plugin.ContactService.Shared.Contact;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SqliteApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ContactsPage : ContentPage
    {
        private ContactsViewModel viewModel;
        public ContactsPage()
        {
            InitializeComponent();
            BindingContext = viewModel = new ContactsViewModel();

            viewModel.IsSortView = false;
        }

        void Sort_Clicked(object sender, EventArgs e)
        {
            bool isSortView = !viewModel.IsSortView;
            viewModel.IsSortView = isSortView;
            ToolbarItems[0].Text = isSortView ? "All Contacts" : "Sort";
            viewModel.LoadContactsCommand.Execute(null);
        }


        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var contact = args.SelectedItem as Contact;
            if (contact == null)
                return;

            await Navigation.PushAsync(new ContactDetailPage(new BaseDetailViewModel<Contact>(contact)));

            ContactsListView.SelectedItem = null;
        }

        

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (viewModel.Contacts.Count == 0)
                viewModel.LoadContactsCommand.Execute(null);
        }
    }
}