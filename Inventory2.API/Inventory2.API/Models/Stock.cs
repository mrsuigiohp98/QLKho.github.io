using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Inventory2.API.Models
{
    public class Stock
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<Inventory> Inventories { set; get; }
    }
}

