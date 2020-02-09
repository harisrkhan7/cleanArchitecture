using System.Linq;
using ProductAggregate = cleanArchitecture.Core.Entities.ProductAggregate;
using cleanArchitecture.Core.Interfaces.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using cleanArchitecture.Web.Messages;
using System;
using Microsoft.AspNetCore.Http;

namespace cleanArchitecture.Web.Controllers
{
    public class ProductController : ControllerBase
    {

        IAsyncRepository<ProductAggregate.Product> _productsRepository;

        public ProductController(IAsyncRepository<ProductAggregate.Product> efRepository)
        {
            _productsRepository = efRepository;
        }

        [HttpGet("/products")]
        public async Task<IActionResult> GetAsync()
        {
            try
            {
                var products = await this._productsRepository.ListAllAsync();

                if (null == products)
                {
                    return NotFound();
                }
                else
                {
                    var productDtos = products.Select(product => Product.FromProduct(product));
                    return Ok(productDtos);
                }
            }
            catch (Exception ex)
            {
                var errorResponse = ResponseManager.FormErrorResponse(ex);
                return StatusCode(StatusCodes.Status500InternalServerError, errorResponse);
            }

        }

        [HttpGet("/products/{id}")]
        public async Task<IActionResult> GetAsync(Guid id)
        {
            try
            {
                var product = await this._productsRepository.GetByIdAsync(id);
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


        [HttpPost("/products")]
        public async Task<IActionResult> PostAsync([FromBody]Product product)
        {
            try
            {
                var productRecord = product.ToProduct();
                var productResult = await this._productsRepository.AddAsync(productRecord);
                return new CreatedResult(nameof(PostAsync), Product.FromProduct(productResult));
            }
            catch (Exception ex)
            {
                var errorResponse = ResponseManager.FormErrorResponse(ex);
                return StatusCode(StatusCodes.Status500InternalServerError, errorResponse);
            }
        }

        [HttpPut("/products")]
        public async Task<IActionResult> UpdateAsync([FromBody]Product product)
        {
            try
            {
                var searchResult = await this._productsRepository.GetByIdAsync(product.Id);
                if(null == searchResult)
                {
                    return NotFound();
                }
                else
                {
                    
                    await this._productsRepository.UpdateAsync(product.Update(searchResult));                    
                    return new CreatedResult(nameof(UpdateAsync), Product.FromProduct(searchResult));
                }
            }
            catch (Exception ex)
            {
                var errorResponse = ResponseManager.FormErrorResponse(ex);
                return StatusCode(StatusCodes.Status500InternalServerError, errorResponse);
            }
        }

        [HttpDelete("/products/{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            try
            {
                var searchResult = await this._productsRepository.GetByIdAsync(id);
                if (null == searchResult)
                {
                    return NotFound();
                }
                else
                {
                    await this._productsRepository.DeleteAsync(searchResult);
                    return Accepted();
                }
            }
            catch (Exception ex)
            {
                var errorResponse = ResponseManager.FormErrorResponse(ex);
                return StatusCode(StatusCodes.Status500InternalServerError, errorResponse);
            }
        }
    }
}
