using System;
using System.Collections.Generic;
using ProductAggregate = cleanArchitecture.Core.Entities.ProductAggregate;
using Messages = cleanArchitecture.Web.Messages;

namespace cleanArchitecture.UnitTests.MockObjects
{
    public class Product
    {
        public Product()
        {
        }

        public static List<ProductAggregate.Product> GetProducts()
        {
            var products = new List<ProductAggregate.Product>();

            products.Add(new ProductAggregate.Product()
            {
                Name = "Notebook",
                DeliveryPrice = 200,
                Description = "A4 Size",
                Price = 100
            });

            products.Add(new ProductAggregate.Product()
            {
                Name = "Pen",
                DeliveryPrice = 100,
                Description = "Ink pen",
                Price = 50
            });

            products.Add(new ProductAggregate.Product()
            {
                Name = "Map",
                DeliveryPrice = 150,
                Description = "Coloured",
                Price = 100
            });

            return products;
        }

        public static List<ProductAggregate.Product> GetProducts_Empty()
        {
            return null;
        }

        public static ProductAggregate.Product GetProduct()
        {
            var product = new ProductAggregate.Product()
            {
                Name = "Pen",
                DeliveryPrice = 120,
                Price = 90,
                Description = "Fountain pen",
                Id = new Guid()
            };
            return product;
        }

        public static Messages.Product GetProductDTO()
        {
            var productDto = new Messages.Product()
            {
                Name = "Pen",
                DeliveryPrice = 99,
                Description = "Blue Gel Pen",
                Price = 77
            };
            return productDto;
        }

        public static Messages.Product GetProductDTO(ProductAggregate.Product product)
        {
            var productDto = new Messages.Product()
            {
                Id = product.Id,
                Name = product.Name,
                DeliveryPrice = product.DeliveryPrice,
                Description = product.Description,
                Price = product.Price
            };
            return productDto;
        }

        public static ProductAggregate.Product CreateProduct(ProductAggregate.Product product)
        {
            product.Id = new Guid();
            return product;
        }

        public static ProductAggregate.Product GetProduct_Empty()
        {
            return null;
        }
    }
}
