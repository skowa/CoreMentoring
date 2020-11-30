using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Northwind.API.Constants;
using Northwind.Business.Interfaces.Services;
using Northwind.Core.Web.Constants;
using Northwind.Core.Web.Models;

namespace Northwind.API.Controllers
{
    [ApiController]
    [Route(Routes.ControllerApi)]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoriesService _categoriesService;
        private readonly IMapper _mapper;

        public CategoriesController(ICategoriesService categoriesService, IMapper mapper)
        {
            _categoriesService = categoriesService;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<CategoryModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetCategories()
        {
            var categories = await _categoriesService.GetCategoriesAsync();

            return Ok(_mapper.Map<IEnumerable<CategoryModel>>(categories));
        }

        [HttpGet]
        [Route(Routes.Categories.ImageByCategoryId)]
        [ProducesResponseType(typeof(FileContentResult), StatusCodes.Status200OK)]
        public async Task<IActionResult> Image(int id)
        {
            var image = (await _categoriesService.GetCategoryImageAsync(id)).ToArray();

            return File(image, ContentTypes.BmpImage);
        }

        [HttpPut]
        [Route(Routes.Categories.ImageByCategoryId)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Image(IFormFile imageFile, int id)
        {
            try
            {
                using var memoryStream = new MemoryStream();
                await imageFile.CopyToAsync(memoryStream);

                await _categoriesService.UpdateCategoryImageAsync(id, memoryStream.ToArray());

                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
