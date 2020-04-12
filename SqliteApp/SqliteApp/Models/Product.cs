using System;
using System.Collections.Generic;
using System.Text;

namespace SqliteApp.Models
{
    public class Product
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public int Amount { get; set; }

        public override string ToString()
        {
            return $"({Id}) {Name}, {Amount}, {Price}";
        }

    }
}
