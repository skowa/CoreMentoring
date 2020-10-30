using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Northwind.Business.Interfaces.Services;
using Northwind.Core.Domain;
using Northwind.Web.Constants;
using Northwind.Web.Models;

namespace Northwind.Web.Controllers
{
    [Route(Routes.Products.Prefix)]
    public class ProductsController : Controller
    {
        private const string IndexView = "Index";

        private readonly IProductsService _productsService;
        private readonly ICategoriesService _categoriesService;
        private readonly ISuppliersService _suppliersService;
        private readonly IMapper _mapper;

        public ProductsController(
            IProductsService productsService,
            ICategoriesService categoriesService,
            ISuppliersService suppliersService,
            IMapper mapper)
        {
            _productsService = productsService;
            _categoriesService = categoriesService;
            _suppliersService = suppliersService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var products = await _productsService.GetProductsAsync();

            return View(_mapper.Map<IEnumerable<ProductModel>>(products));
        }

        [HttpGet]
        [Route(Routes.Products.Create)]
        public async Task<IActionResult> Create()
        {
            await FillViewBagAsync();

            return View();
        }

        [HttpPost]
        [Route(Routes.Products.Create)]
        public async Task<IActionResult> Create(ProductEditModel productEditModel)
        {
            if (!ModelState.IsValid)
            {
                await FillViewBagAsync();

                return View(productEditModel);
            }

            var product = _mapper.Map<Product>(productEditModel);
            await _productsService.AddProductAsync(product);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        [Route(Routes.Products.Update)]
        public async Task<IActionResult> Update(int id)
        {
            var product = await _productsService.GetProductByIdAsync(id);
            await FillViewBagAsync();

            return View(_mapper.Map<ProductEditModel>(product));
        }

        [HttpPost]
        [Route(Routes.Products.Update)]
        public async Task<IActionResult> Update(ProductEditModel productEditModel)
        {
            if (!ModelState.IsValid)
            {
                await FillViewBagAsync();

                return View(productEditModel);
            }

            var product = _mapper.Map<Product>(productEditModel);
            await _productsService.UpdateProductAsync(product);

            return RedirectToAction(nameof(Index));
        }

        private async Task FillViewBagAsync()
        {
            var categories = await _categoriesService.GetCategoriesAsync();
            var suppliers = await _suppliersService.GetSuppliersAsync();

            ViewBag.Categories = categories;
            ViewBag.Suppliers = suppliers;
        }
    }
}
