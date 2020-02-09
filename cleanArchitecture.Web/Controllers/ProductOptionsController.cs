using System;
using ProductAggregate = cleanArchitecture.Core.Entities.ProductAggregate;
using cleanArchitecture.Web.Messages;
using Microsoft.AspNetCore.Mvc;
using cleanArchitecture.Core.Interfaces.Repositories;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.AspNetCore.Http;

namespace cleanArchitecture.Web.Controllers
{
    public class ProductOptionsController : ControllerBase
    {
        IAsyncRepository<ProductAggregate.Product> _productsRepository;

        IAsyncRepository<ProductAggregate.ProductOption> _productOptionsRepository;

        public ProductOptionsController(IAsyncRepository<ProductAggregate.Product> productRepository,
            IAsyncRepository<ProductAggregate.ProductOption> productOptionsRepository)
        {
            _productsRepository = productRepository;
            _productOptionsRepository = productOptionsRepository;
        }

        [HttpGet("{productId}/options")]
        public async Task<IActionResult> GetOptionsAsync(Guid productId)
        {
            try
            {
                var product = await _productsRepository.GetByIdAsync(productId);
                if(null == product)
                {
                    return NotFound();
                }
                else if(null == product.ProductOptions || product.ProductOptions.Count == 0)
                {
                    return NotFound();
                }

                var productOptions = product.ProductOptions.Select(productOption => ProductOption.FromProductOption(productOption)).ToList();
                return Ok(productOptions);
            }
            catch(Exception ex)
            {
                var errorResponse = ResponseManager.FormErrorResponse(ex);
                return StatusCode(StatusCodes.Status500InternalServerError, errorResponse);
            }    
        }

        [HttpGet("/options/{id}")]
        public async Task<IActionResult> GetOptionAsync(Guid id)
        {
            try
            {
                var productOption = await _productOptionsRepository.GetByIdAsync(id);
                if(null == productOption)
                {
                    return NotFound();
                }
                return Ok(ProductOption.FromProductOption(productOption));
            }
            catch (Exception ex)
            {
                var errorResponse = ResponseManager.FormErrorResponse(ex);
                return StatusCode(StatusCodes.Status500InternalServerError, errorResponse);
            }
        }


        [HttpPost("{productId}/options")]
        public async Task<IActionResult> CreateOptionAsync(Guid productId, [FromBody] ProductOption option)
        {
            try
            {
                var product = await _productsRepository.GetByIdAsync(productId);

                if (null == product)
                {
                    return NotFound();
                }

                var productOptionRecord = option.ToProductOption();
                product.ProductOptions.Add(productOptionRecord);

                await _productsRepository.UpdateAsync(product);

                var updatedProductOption = ProductOption.FromProductOption(productOptionRecord);

                return new CreatedResult(nameof(CreateOptionAsync), updatedProductOption);
            }
            catch (Exception ex)
            {
                var errorResponse = ResponseManager.FormErrorResponse(ex);
                return StatusCode(StatusCodes.Status500InternalServerError, errorResponse);
            }
        }


        [HttpPut("/options/{id}")]
        public async Task<IActionResult> UpdateOptionAsync(Guid id, [FromBody] ProductOption option)
        {
            try
            {
                var searchResult = await this._productOptionsRepository.GetByIdAsync(id);

                if (null == searchResult)
                {
                    return NotFound();
                }
                else
                {
                    await this._productOptionsRepository.UpdateAsync(option.Update(searchResult));
                    return new CreatedResult(nameof(UpdateOptionAsync), ProductOption.FromProductOption(searchResult));
                }
            }
            catch (Exception ex)
            {
                var errorResponse = ResponseManager.FormErrorResponse(ex);
                return StatusCode(StatusCodes.Status500InternalServerError, errorResponse);
            }
        }

        [HttpDelete("/options/{id}")]
        public async Task<IActionResult> DeleteOptionAsync(Guid id)
        {
            try
            {
                var searchResult = await this._productOptionsRepository.GetByIdAsync(id);
                if (null == searchResult)
                {
                    return NotFound();
                }
                else
                {
                    await this._productOptionsRepository.DeleteAsync(searchResult);
                    return StatusCode(StatusCodes.Status202Accepted);
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
