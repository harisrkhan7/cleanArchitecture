﻿using System;
namespace cleanArchitecture.Core.Entities.ProductAggregate
{
    public class ProductOption :BaseEntity
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public Guid? ProductId { get; set; }

        public virtual Product Product { get; set; }

        public ProductOption()
        {

        }

    }
}
