using System;
using ProductAggregate = cleanArchitecture.Core.Entities.ProductAggregate;
using Messages = cleanArchitecture.Web.Messages;
using System.Collections.Generic;

namespace cleanArchitecture.UnitTests.MockObjects
{
    public class ProductOption
    {
        public ProductOption()
        {
        }

        public static List<ProductAggregate.ProductOption> GetProductOptions()
        {
            var productOptions = new List<ProductAggregate.ProductOption>();

            productOptions.Add(new ProductAggregate.ProductOption()
            {
                Name = "Colour",
                Description = "Black"
            });

            productOptions.Add(new ProductAggregate.ProductOption()
            {
                Name = "Colour",
                Description = "Blue"
            });

            productOptions.Add(new ProductAggregate.ProductOption()
            {
                Name = "Colour",
                Description = "Red"
            });

            return productOptions;
        }

        public static ProductAggregate.ProductOption GetProductOption()
        {
            var productOption = new ProductAggregate.ProductOption()
            {
                Name = "Colour",
                Description = "Blue"
            };
            return productOption;
        }

        public static ProductAggregate.ProductOption GetProductOption_Empty()
        {
            return null;
        }

        public static Messages.ProductOption GetProductOptionDTO()
        {
            var productOptionDTO = new Messages.ProductOption()
            {
                Name = "Colour",
                Description = "Black"
            };
            return productOptionDTO;
        }


    }
}
