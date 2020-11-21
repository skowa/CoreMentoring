using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Northwind.API.Constants;
using Northwind.Business.Interfaces.Services;
using Northwind.Web.Models;

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
        public async Task<IActionResult> GetProducts()
        {
            var products = await _productsService.GetProductsAsync();

            return Ok(_mapper.Map<IEnumerable<ProductModel>>(products));
        }
    }
}
