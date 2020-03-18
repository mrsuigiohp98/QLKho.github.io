using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Inventory2.API.Models
{
    public class Inventory
    {
        public int Id { get; set; }
        
        public string Name { get; set; }
        
        public int Soluong { get; set; }

        public string NoiSX { get; set; }

        public int UnitId { set; get; }

        public Unit Unit { set; get; }

        public int StockId { get; set; }

        public Stock Stock { get; set; }

        public ICollection<Receipt> Receipts { set; get; }
        public ICollection<Delivery> Deliveries { set; get; }
        //public IList<Stock> Stocks{ get; set; } = new List<Stock>();
    }
}
