using Inventory2.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Inventory2.API.Resources
{
    public class ReceiptViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Ngaynhap { get; set; }
        public int InventoryId { get; set; }
        public string InventoryName { get; set; }
        public int SupplierId { get; set; }
        public string SupplierName { get; set; }

        public int Soluong { get; set; }
        public int Dongia { get; set; }
        public int Thanhtien { get; set; }
    }
}
