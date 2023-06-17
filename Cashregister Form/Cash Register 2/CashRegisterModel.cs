using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Collections.Generic;

namespace CashApp
{
    public class CashRegisterModel
    {
        public List<Cashier> Cashiers { get; set; } = new List<Cashier>();
        public List<Item> Items { get; set; } = new List<Item>();
        public List<PurchaseItem> PurchaseItems { get; set; } = new List<PurchaseItem>();
    }

    public class Cashier
    {
        private string username;

        public Cashier(string username, string password)
        {
            this.username = username;
            Password = password;
        }

        public string Name { get; set; }
        public string Password { get; set; }
        public object Username { get; internal set; }
    }

    public class Item
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
    }

    public class PurchaseItem
    {
        public Item Item { get; set; }
        public int Quantity { get; set; }
        public decimal TotalPrice { get; set; }

        public PurchaseItem(Item item, int quantity, decimal totalPrice)
        {
            Item = item;
            Quantity = quantity;
            TotalPrice = totalPrice;
        }
    }
}
