using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Northwind.Business.Interfaces.Services;
using Northwind.Web.Constants;
using Northwind.Web.Models;

namespace Northwind.Web.Controllers
{
    [Route(Routes.Categories.Prefix)]
    public class CategoriesController : Controller
    {
        private const string IndexView = "Index";

        private readonly ICategoriesService _categoriesService;
        private readonly IMapper _mapper;

        public CategoriesController(ICategoriesService categoriesService, IMapper mapper)
        {
            _categoriesService = categoriesService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var categories = await _categoriesService.GetCategoriesAsync();

            return View(_mapper.Map<IEnumerable<CategoryModel>>(categories));
        }

        [HttpGet]
        [Route(Routes.Categories.Image)]
        public async Task<IActionResult> Image(int id)
        {
            var image = (await _categoriesService.GetCategoryImageAsync(id)).ToArray();

            return File(image, ContentTypes.BmpImage);
        }

        [HttpPost]
        [Route(Routes.Categories.Image)]
        public async Task<IActionResult> Image(IFormFile imageFile, int id)
        {
            try
            {
                using var memoryStream = new MemoryStream();
                await imageFile.CopyToAsync(memoryStream);

                await _categoriesService.UpdateCategoryImageAsync(id, memoryStream.ToArray());

                return RedirectToAction(IndexView);
            }
            catch (ArgumentException)
            {
                return RedirectToAction(nameof(Update), id);
            }
        }

        [HttpGet]
        [Route(Routes.Categories.Update)]
        public IActionResult Update(int id)
        {
            var categoryModel = new CategoryModel { CategoryId = id };

            return View(categoryModel);
        }
    }
}
