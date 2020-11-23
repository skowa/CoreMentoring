using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Northwind.API.Constants;
using Northwind.Business.Interfaces.Services;
using Northwind.Core.Domain;
using Northwind.Core.Web.Models;

namespace Northwind.API.Controllers
{
    [ApiController]
    [Route(Routes.ControllerApi)]
    public class ProductsController : ControllerBase
    {
        private readonly IProductsService _productsService;
        private readonly IMapper _mapper;

        public ProductsController(IProductsService productsService, IMapper mapper)
        {
            _productsService = productsService;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ProductModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetProducts()
        {
            var products = await _productsService.GetProductsAsync();

            return Ok(_mapper.Map<IEnumerable<ProductModel>>(products));
        }

        [HttpGet]
        [Route(Routes.Products.ProductById)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProductModel), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetProductById(int id)
        {
            var product = await _productsService.GetProductByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<ProductModel>(product));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProductModel), StatusCodes.Status201Created)]
        public async Task<IActionResult> CreateProduct([FromBody] ProductEditModel productEditModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.Values);
            }

            var product = await _productsService.AddProductAsync(_mapper.Map<Product>(productEditModel));

            return CreatedAtAction(nameof(GetProductById), new { id = product.ProductId }, _mapper.Map<ProductModel>(product));
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> UpdateProduct([FromBody] ProductEditModel productEditModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.Values);
            }

            await _productsService.UpdateProductAsync(_mapper.Map<Product>(productEditModel));

            return NoContent();
        }

        [HttpDelete]
        [Route(Routes.Products.ProductById)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteProductById(int id)
        {
            await _productsService.DeleteProductAsync(id);

            return NoContent();
        }
    }
}
