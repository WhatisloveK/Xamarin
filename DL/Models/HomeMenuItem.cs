using System;
using System.Collections.Generic;
using System.Text;

namespace SqliteApp.Models
{
    public enum MenuItemType
    {
        ExpressionSolver,
        FileWorker,
        Products,
        Summuries,
        Contacts,
        Location,
        Help,
        About
    }
    public class HomeMenuItem
    {
        public MenuItemType Id { get; set; }

        public string Title { get; set; }
    }
}
