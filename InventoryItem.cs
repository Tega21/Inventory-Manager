using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory_Manager
{
    internal class InventoryItem
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string Category { get; set; }

        public InventoryItem(string id, string name, decimal price, int quantity, string category)
        {
            ID = id;
            Name = name;
            Price = price;
            Quantity = quantity;
            Category = category;
        }

        public void UpdateItem(string name, decimal price, int quantity, string category)
        {
            Name = name;
            Price = price;
            Quantity = quantity;
            Category = category;
        }
    }
}

