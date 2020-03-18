using Inventory2.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Inventory2.API.Resources
{
    public class DeliveryViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Ngayxuat { get; set; }
        public int InventoryId { set; get; } 
        public string InventoryName { get; set; }
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }

        public int Soluong { get; set; }
        public int Dongia { get; set; }
        public int Thanhtien { get; set; }
    }
}

