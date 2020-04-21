
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Contact = Plugin.ContactService.Shared.Contact;
namespace SqliteApp.ViewModels
{
    public class ContactsViewModel : BaseViewModel
    {
        public ObservableCollection<Contact> Contacts { get; set; }
        public Command LoadContactsCommand { get; set; }

        public bool IsSortView { get; set; }

        public ContactsViewModel()
        {
            Title = "Browse";
            Contacts = new ObservableCollection<Contact>();
            LoadContactsCommand = new Command(async () => await ExecuteLoadItemsCommand());
        }
        async Task ExecuteLoadItemsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Contacts.Clear();
                var contacts = await Plugin.ContactService.CrossContactService.Current.GetContactListAsync();
                
                if (IsSortView)
                {
                    foreach (var item in contacts)
                    {
                        if (item.Name.Contains("iuk"))
                        {
                            Contacts.Add(item);
                        }
                    }
                }
                else
                {
                    foreach (var item in contacts)
                    {
                        Contacts.Add(item);
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
