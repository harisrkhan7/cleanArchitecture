using System;
using System.Runtime.Serialization;
using ProductAggregate = cleanArchitecture.Core.Entities.ProductAggregate;


namespace cleanArchitecture.Web.Messages
{
    [DataContract]
    public class ProductOption
    {
        [DataMember]
        public Guid? Id { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string Description { get; set; }        


        public Core.Entities.ProductAggregate.ProductOption ToProductOption()
        {
            return new ProductAggregate.ProductOption()
            {
                Description = this.Description,
                Name = this.Name  
            };
        }

        public static ProductOption FromProductOption(Core.Entities.ProductAggregate.ProductOption productOption)
        {
            return new ProductOption()
            {
                Description = productOption.Description,
                Id = productOption.Id,
                Name = productOption.Name
            };
        }

        public ProductAggregate.ProductOption Update(ProductAggregate.ProductOption productOption)
        {
            productOption.Description = this.Description;
            productOption.Name = this.Name;
            return productOption;
        }
    }
}
