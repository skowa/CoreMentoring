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
        public async Task<IActionResult> GetCategories()
        {
            var categories = await _categoriesService.GetCategoriesAsync();

            return Ok(_mapper.Map<IEnumerable<CategoryModel>>(categories));
        }
    }
}
