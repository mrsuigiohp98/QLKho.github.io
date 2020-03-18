using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Inventory2.API.Models
{
    public class Unit
    {
        public int Id { get; set; }

        public string Name { get; set; }
    
        public string Description { get; set; }

        public ICollection<Inventory> Inventories { set; get; }
    }
}
