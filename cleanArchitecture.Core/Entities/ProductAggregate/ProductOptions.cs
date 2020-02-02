using System;
namespace cleanArchitecture.Core.Entities.ProductAggregate
{
    public class ProductOptions :BaseEntity
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public bool IsNew { get; }

        public Guid? ProductId { get; set; }

        public virtual Product Product { get; set; }

        public ProductOptions()
        {
        }
    }
}
