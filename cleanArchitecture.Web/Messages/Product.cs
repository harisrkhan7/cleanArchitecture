using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace cleanArchitecture.Web.Messages
{
    [DataContract]
    public class Product
    {
        [DataMember]
        public Guid Id { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public decimal Price { get; set; }

        [DataMember]
        public decimal DeliveryPrice { get; set; }       
        

        public static Product FromProduct(Core.Entities.ProductAggregate.Product product)
        {
            return new Product()
            {
                DeliveryPrice = product.DeliveryPrice,
                Description = product.Description,
                Id = product.Id,
                Name = product.Name,
                Price = product.Price
            };
        }

        public Core.Entities.ProductAggregate.Product ToProduct()
        {
            return new Core.Entities.ProductAggregate.Product()
            {
                DeliveryPrice = this.DeliveryPrice,
                Description = this.Description,                
                Name = this.Name,
                Price = this.Price                
            };
        }

        public Core.Entities.ProductAggregate.Product Update(Core.Entities.ProductAggregate.Product product)
        {
            product.DeliveryPrice = this.DeliveryPrice;
            product.Description = this.Description;
            product.Name = this.Name;
            product.Price = this.Price;

            return product;
        }
    }
}
