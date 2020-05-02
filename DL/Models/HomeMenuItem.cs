using System;
using System.Collections.Generic;
using System.Text;

namespace SqliteApp.Models
{
    public enum MenuItemType
    {
        FileWorker,
        Products,
        Summuries,
        Contacts,
        Location,
        About
    }
    public class HomeMenuItem
    {
        public MenuItemType Id { get; set; }

        public string Title { get; set; }
    }
}
