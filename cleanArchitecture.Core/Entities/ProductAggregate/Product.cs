﻿using System;
using System.Collections.Generic;

namespace cleanArchitecture.Core.Entities.ProductAggregate
{
    public class Product : BaseEntity
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public decimal DeliveryPrice { get; set; }

        public virtual ICollection<ProductOption> ProductOptions { get; set; }

        public Product()
        {
            ProductOptions = new List<ProductOption>();
        }
    }
}
