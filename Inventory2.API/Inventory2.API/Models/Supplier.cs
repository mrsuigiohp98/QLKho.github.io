﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Inventory2.API.Models
{
    public class Supplier
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Diachi { get; set; }
        public int Sdt { get; set; }
        public ICollection<Receipt> Receipts { set; get; }
    }
}
