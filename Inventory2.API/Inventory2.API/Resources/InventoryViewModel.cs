using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Inventory2.API.Resources
{
    public class InventoryViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Soluong { get; set; }

        public string NoiSX { get; set; }

        public int UnitId { set; get; }

        public string UnitName { set; get; }

        public int StockId { get; set; }

        public string StockName { get; set; }
    }
}
