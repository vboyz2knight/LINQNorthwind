﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyWCFServices.LINQNorthwindEntities
{
    public class ProductEntity
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public string QuantityPerUnit { get; set; }
        public decimal UnitPrice { get; set; }
        public int UnitsInStock { get; set; }
        public int ReorderLevel { get; set; }
        public int UnitsOnOrder { get; set; }
        public bool Discontinued { get; set; }
        public Byte[] RowVersion { get; set; }
    }
}
