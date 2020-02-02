using System.Linq;
using ProductAggregate = cleanArchitecture.Core.Entities.ProductAggregate;
using cleanArchitecture.Core.Interfaces.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using cleanArchitecture.Web.Messages;
using System;
using Microsoft.AspNetCore.Http;

namespace cleanArchitecture.Web.Controllers
{
    public class ProductController : ControllerBase
    {

        IAsyncRepository<ProductAggregate.Product> _productRepository;

        public ProductController(IAsyncRepository<ProductAggregate.Product> efRepository)
        {
            _productRepository = efRepository;
        }

        [HttpGet(nameof(GetAsync))]
        public async Task<IActionResult> GetAsync()
        {
            try
            {
                var products = await this._productRepository.ListAllAsync();
                var productDtos = products.Select(product => Product.FromProduct(product));
                if (null == productDtos)
                {
                    return NotFound();
                }
                else
                {
                    return Ok(productDtos);
                }
            }
            catch (Exception ex)
            {
                var errorResponse = ResponseManager.FormErrorResponse(ex);
                return StatusCode(StatusCodes.Status500InternalServerError, errorResponse);
            }

        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(Guid id)
        {
            try
            {
                var product = await this._productRepository.GetByIdAsync(id);
                if (null == product)
                {
                    return NotFound();
                }
                else
                {
                    return Ok(Product.FromProduct(product));
                }
            }
            catch (Exception ex)
            {
                var errorResponse = ResponseManager.FormErrorResponse(ex);
                return StatusCode(StatusCodes.Status500InternalServerError, errorResponse);
            }
        }


        [HttpPost(nameof(PostAsync))]
        public async Task<IActionResult> PostAsync([FromBody]Product product)
        {
            try
            {
                var productRecord = product.ToProduct();
                var productResult = await this._productRepository.AddAsync(productRecord);
                return new CreatedResult(nameof(PostAsync), Product.FromProduct(productResult));
            }
            catch (Exception ex)
            {
                var errorResponse = ResponseManager.FormErrorResponse(ex);
                return StatusCode(StatusCodes.Status500InternalServerError, errorResponse);
            }
        }

        [HttpPut(nameof(UpdateAsync))]
        public async Task<IActionResult> UpdateAsync([FromBody]Product product)
        {
            try
            {
                var searchResult = await this._productRepository.GetByIdAsync(product.Id);
                if(null == searchResult)
                {
                    return NotFound();
                }
                else
                {
                    
                    await this._productRepository.UpdateAsync(product.Update(searchResult));
                    return new CreatedResult(nameof(UpdateAsync), Product.FromProduct(searchResult));
                }
            }
            catch (Exception ex)
            {
                var errorResponse = ResponseManager.FormErrorResponse(ex);
                return StatusCode(StatusCodes.Status500InternalServerError, errorResponse);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            try
            {
                var product = new ProductAggregate.Product()
                {
                    Id = id
                };

                await this._productRepository.DeleteAsync(product);
                return StatusCode(StatusCodes.Status202Accepted);
            }
            catch (Exception ex)
            {
                var errorResponse = ResponseManager.FormErrorResponse(ex);
                return StatusCode(StatusCodes.Status500InternalServerError, errorResponse);
            }
        }
    }
}
