using System;
using cleanArchitecture.Core.Interfaces.Repositories;
using Moq;
using Xunit;
using ProductAggregate = cleanArchitecture.Core.Entities.ProductAggregate;
using Messages = cleanArchitecture.Web.Messages;
using System.Threading.Tasks;
using System.Linq;
using cleanArchitecture.Web.Controllers;
using cleanArchitecture.UnitTests.MockObjects;
using cleanArchitecture.Core.Entities.ProductAggregate;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace cleanArchitecture.UnitTests.Controllers
{
    public class ProductOptionsControllerTests
    {
        public ProductOptionsControllerTests()
        {
        }

        [Fact]
        public async Task CreateOption_ProductExists()
        {
            //Arrange
            var productRepository = new Mock<IAsyncRepository<ProductAggregate.Product>>();
            productRepository.Setup(repo => repo.UpdateAsync(It.IsAny<ProductAggregate.Product>()))
                .Callback(
                (ProductAggregate.Product p) =>
                {
                    p.ProductOptions =
                    p.ProductOptions.Select
                    (option => new ProductAggregate.ProductOption()
                    {
                        Name = option.Name,
                        Description = option.Description,
                        Product = p,
                        ProductId = p.Id,
                        Id = new Guid()
                    }).ToList();                    
                });

            productRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(MockObjects.Product.GetProduct())
                .Verifiable();

            var productOptionRepository = new Mock<IAsyncRepository<ProductAggregate.ProductOption>>();

            var controlller = new ProductOptionsController(productRepository.Object, productOptionRepository.Object);

            //Act
            var productOptionDTO = MockObjects.ProductOption.GetProductOptionDTO();
            var productGuid = new Guid();

            var result = await controlller.CreateOptionAsync(productGuid, productOptionDTO);

            var createdResult = result as CreatedResult;

            var productOption = createdResult.Value as Messages.ProductOption;


            //Assert
            productRepository.Verify();

            Assert.NotNull(createdResult);

            Assert.NotNull(productOption);

            Assert.NotNull(productOption.Id);
            Assert.Equal(productOptionDTO.Name, productOption.Name);
            Assert.Equal(productOptionDTO.Description, productOption.Description);
            Assert.Equal(StatusCodes.Status201Created, createdResult.StatusCode);
        }

        [Fact]
        public async Task CreateOption_ProductDoesNotExist()
        {
            //Arrange
            var productRepository = new Mock<IAsyncRepository<ProductAggregate.Product>>();
            productRepository.Setup(repo => repo.UpdateAsync(It.IsAny<ProductAggregate.Product>()))
                .Callback(
                (ProductAggregate.Product p) =>
                {
                    p.ProductOptions =
                    p.ProductOptions.Select
                    (option => new ProductAggregate.ProductOption()
                    {
                        Name = option.Name,
                        Description = option.Description,
                        Product = p,
                        ProductId = p.Id,
                        Id = new Guid()
                    }).ToList();
                });

            productRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(MockObjects.Product.GetProduct_Empty())
                .Verifiable();

            var productOptionRepository = new Mock<IAsyncRepository<ProductAggregate.ProductOption>>();

            var controlller = new ProductOptionsController(productRepository.Object, productOptionRepository.Object);

            //Act
            var productOptionDTO = MockObjects.ProductOption.GetProductOptionDTO();
            var productGuid = new Guid();

            var result = await controlller.CreateOptionAsync(productGuid, productOptionDTO);                    

            //Assert
            productRepository.Verify();
            productRepository.VerifyNoOtherCalls();

            //Assert            
            var notFoundResult = result as NotFoundResult;

            productRepository.Verify();
            productRepository.VerifyNoOtherCalls();
            Assert.NotNull(notFoundResult);
            Assert.Equal(StatusCodes.Status404NotFound, notFoundResult.StatusCode);
        }


        [Fact]
        public async Task UpdateOption_Exists()
        {
            //Arrange

            //Setup product repository
            var productRepository = new Mock<IAsyncRepository<ProductAggregate.Product>>();            

            //Setup product options repository
            var productOptionRepository = new Mock<IAsyncRepository<ProductAggregate.ProductOption>>();

            productOptionRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(MockObjects.ProductOption.GetProductOption())
                .Verifiable();

            productOptionRepository.Setup(repo => repo.UpdateAsync(It.IsAny<ProductAggregate.ProductOption>()))            
            .Verifiable();

            var controlller = new ProductOptionsController(productRepository.Object, productOptionRepository.Object);

            //Act
            var productOptionDTO = MockObjects.ProductOption.GetProductOptionDTO();
            var productGuid = new Guid();

            var result = await controlller.UpdateOptionAsync(productGuid, productOptionDTO);

            var createdResult = result as CreatedResult;

            var productOption = createdResult.Value as Messages.ProductOption;


            //Assert
            productRepository.Verify();
            productRepository.VerifyNoOtherCalls();

            Assert.NotNull(createdResult);

            Assert.NotNull(productOption);

            Assert.NotNull(productOption.Id);
            Assert.Equal(productOptionDTO.Name, productOption.Name);
            Assert.Equal(productOptionDTO.Description, productOption.Description);
            Assert.Equal(StatusCodes.Status201Created, createdResult.StatusCode);
        }

        [Fact]
        public async Task UpdateOption_DoesNotExist()
        {
            //Arrange

            //Setup product repository
            var productRepository = new Mock<IAsyncRepository<ProductAggregate.Product>>();

            //Setup product options repository
            var productOptionRepository = new Mock<IAsyncRepository<ProductAggregate.ProductOption>>();

            productOptionRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(MockObjects.ProductOption.GetProductOption_Empty())
                .Verifiable();

            productOptionRepository.Setup(repo => repo.UpdateAsync(It.IsAny<ProductAggregate.ProductOption>()))
            .Verifiable();

            var controlller = new ProductOptionsController(productRepository.Object, productOptionRepository.Object);

            //Act
            var productOptionDTO = MockObjects.ProductOption.GetProductOptionDTO();
            var productGuid = new Guid();

            var result = await controlller.UpdateOptionAsync(productGuid, productOptionDTO);

            //Assert            
            var notFoundResult = result as NotFoundResult;


            productRepository.Verify();
            productRepository.VerifyNoOtherCalls();
            Assert.NotNull(notFoundResult);
            Assert.Equal(StatusCodes.Status404NotFound, notFoundResult.StatusCode);
        }

        [Fact]
        public async Task GetOptions_ProductExists()
        {
            //Arrange
            var productRepository = new Mock<IAsyncRepository<ProductAggregate.Product>>();
            productRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(MockObjects.Product.GetProduct_WithOptions())
                .Verifiable();


            var productOptionsRepository = new Mock<IAsyncRepository<ProductAggregate.ProductOption>>();

            var controlller = new ProductOptionsController(productRepository.Object, productOptionsRepository.Object);

            //Act
            var result = await controlller.GetOptionsAsync(new Guid());

            //Assert
            var okObjectResult = result as OkObjectResult;


            var productOptions = okObjectResult.Value as List<Messages.ProductOption>;


            productRepository.Verify();
            Assert.NotNull(okObjectResult);
            Assert.NotNull(productOptions);
            Assert.NotEmpty(productOptions);
        }

        [Fact]
        public async Task GetOptions_ProductDoesNotExist()
        {
            //Arrange
            var productRepository = new Mock<IAsyncRepository<ProductAggregate.Product>>();
            productRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(MockObjects.Product.GetProduct_Empty())
                .Verifiable();


            var productOptionsRepository = new Mock<IAsyncRepository<ProductAggregate.ProductOption>>();

            var controlller = new ProductOptionsController(productRepository.Object, productOptionsRepository.Object);

            //Act
            var result = await controlller.GetOptionsAsync(new Guid());

            //Assert            
            var notFoundResult = result as NotFoundResult;

            productRepository.Verify();
            Assert.NotNull(notFoundResult);
            Assert.Equal(StatusCodes.Status404NotFound, notFoundResult.StatusCode);
        }

        [Fact]
        public async Task GetOptions_ProductExistsOptionsDoNotExist()
        {
            //Arrange
            var productRepository = new Mock<IAsyncRepository<ProductAggregate.Product>>();
            productRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(MockObjects.Product.GetProduct())
                .Verifiable();


            var productOptionsRepository = new Mock<IAsyncRepository<ProductAggregate.ProductOption>>();

            var controlller = new ProductOptionsController(productRepository.Object, productOptionsRepository.Object);

            //Act
            var result = await controlller.GetOptionsAsync(new Guid());

            //Assert            
            var notFoundResult = result as NotFoundResult;

            productRepository.Verify();
            Assert.NotNull(notFoundResult);
            Assert.Equal(StatusCodes.Status404NotFound, notFoundResult.StatusCode);
        }

        [Fact]
        public async Task GetOption_OptionExists()
        {
            //Arrange
            var productOptionsRepository = new Mock<IAsyncRepository<ProductAggregate.ProductOption>>();
            productOptionsRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(MockObjects.ProductOption.GetProductOption())
                .Verifiable();

            var productRepository = new Mock<IAsyncRepository<ProductAggregate.Product>>();

            var controlller = new ProductOptionsController(productRepository.Object, productOptionsRepository.Object);

            //Act
            var result = await controlller.GetOptionAsync(new Guid());

            //Assert
            var okObjectResult = result as OkObjectResult;


            var product = okObjectResult.Value as object;


            productRepository.Verify();
            Assert.NotNull(okObjectResult);
            Assert.NotNull(product);
        }

        [Fact]
        public async Task GetOption_OptionDoesNotExist()
        {
            //Arrange
            var productOptionsRepository = new Mock<IAsyncRepository<ProductAggregate.ProductOption>>();
            productOptionsRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(MockObjects.ProductOption.GetProductOption_Empty())
                .Verifiable();

            var productRepository = new Mock<IAsyncRepository<ProductAggregate.Product>>();

            var controlller = new ProductOptionsController(productRepository.Object, productOptionsRepository.Object);

            //Act
            var result = await controlller.GetOptionAsync(new Guid());

            //Assert
            var notFoundResult = result as NotFoundResult;

            //Assert
            productRepository.Verify();
            Assert.NotNull(notFoundResult);
            Assert.Equal(StatusCodes.Status404NotFound, notFoundResult.StatusCode);
        }

        [Fact]
        public async Task DeleteOption_Exists()
        {
            //Arrange
            var productOptionsRepository = new Mock<IAsyncRepository<ProductAggregate.ProductOption>>();

            productOptionsRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(MockObjects.ProductOption.GetProductOption())
                .Verifiable();

            var productRepository = new Mock<IAsyncRepository<ProductAggregate.Product>>();

            var controlller = new ProductOptionsController(productRepository.Object, productOptionsRepository.Object);

            //Act
            var result = await controlller.DeleteOptionAsync(new Guid());

            //Assert            
            var acceptedResult = result as StatusCodeResult;

            productRepository.Verify();

            Assert.NotNull(acceptedResult);
            Assert.Equal(StatusCodes.Status202Accepted, acceptedResult.StatusCode);
        }

        [Fact]
        public async Task DeleteOption_DoesNotExist()
        {
            //Arrange
            var productOptionsRepository = new Mock<IAsyncRepository<ProductAggregate.ProductOption>>();

            productOptionsRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(MockObjects.ProductOption.GetProductOption_Empty())
                .Verifiable();

            var productRepository = new Mock<IAsyncRepository<ProductAggregate.Product>>();

            var controlller = new ProductOptionsController(productRepository.Object, productOptionsRepository.Object);

            //Act
            var result = await controlller.DeleteOptionAsync(new Guid());

            //Assert            
            var notFoundResult = result as NotFoundResult;

            productRepository.Verify();
            productRepository.VerifyNoOtherCalls();
            Assert.NotNull(notFoundResult);

            Assert.Equal(StatusCodes.Status404NotFound, notFoundResult.StatusCode);
        }

    }


}
