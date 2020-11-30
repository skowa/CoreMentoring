using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Northwind.Business.Interfaces.Services;
using Northwind.Core.Web.Constants;
using Northwind.Core.Web.Models;

namespace Northwind.Web.Controllers
{
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
        public async Task<IActionResult> Image(int id)
        {
            var image = (await _categoriesService.GetCategoryImageAsync(id)).ToArray();

            return File(image, ContentTypes.BmpImage);
        }

        [HttpPost]
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
        public IActionResult Update(int id)
        {
            var categoryModel = new CategoryModel { CategoryId = id };

            return View(categoryModel);
        }
    }
}
