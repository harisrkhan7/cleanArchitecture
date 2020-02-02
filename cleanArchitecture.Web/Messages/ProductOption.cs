using System;
using System.Runtime.Serialization;


namespace cleanArchitecture.Web.Messages
{
    [DataContract]
    public class ProductOption
    {
        [DataMember]
        public Guid Id { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string Description { get; set; }        

        [DataMember]
        public Guid? ProductId { get; set; }

        public Core.Entities.ProductAggregate.ProductOption ToProductOption()
        {
            return new Core.Entities.ProductAggregate.ProductOption()
            {
                Description = this.Description,
                Name = this.Name,
                ProductId = this.ProductId               
            };
        }
    }
}
