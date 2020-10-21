using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Northwind.Business.Interfaces.Services;
using Northwind.Web.Models;

namespace Northwind.Web.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductsService _productsService;
        private readonly IMapper _mapper;

        public ProductsController(IProductsService productsService, IMapper mapper)
        {
            _productsService = productsService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var products = await _productsService.GetProductsAsync();

            return View(_mapper.Map<IEnumerable<ProductModel>>(products));
        }
    }
}
