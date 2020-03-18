using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Inventory2.API.Models
{
    public class Delivery
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Ngayxuat { get; set; }
        public int InventoryId { get; set; }
        public Inventory Inventory { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }

        public int Soluong { get; set; }
        public int Dongia { get; set; }
        public int Thanhtien { get; set; }
    }
}
