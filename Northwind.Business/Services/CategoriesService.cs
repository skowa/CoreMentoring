using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Northwind.Business.Interfaces.Providers;
using Northwind.Business.Interfaces.Services;
using Northwind.Core.Domain;

namespace Northwind.Business.Services
{
    public class CategoriesService : ICategoriesService
    {
        private readonly ICategoriesProvider _categoriesProvider;
        private readonly IImagesService _imagesService;
        private readonly IMapper _mapper;

        public CategoriesService(ICategoriesProvider categoriesProvider, IImagesService imagesService, IMapper mapper)
        {
            _categoriesProvider = categoriesProvider;
            _imagesService = imagesService;
            _mapper = mapper;
        }

        public async Task<IEnumerable<Category>> GetCategoriesAsync()
        {
            var categories = await _categoriesProvider.GetCategoriesAsync();

            return _mapper.Map<IEnumerable<Category>>(categories);
        }

        public async Task<IEnumerable<byte>> GetCategoryImageAsync(int id)
        {
            var category = await _categoriesProvider.GetCategoryByIdAsync(id);

            return _imagesService.RestoreImage(category.Picture);
        }

        public async Task UpdateCategoryImageAsync(int id, IEnumerable<byte> image)
        {
            if (!_imagesService.VerifyImage(image))
            {
                throw new ArgumentException($"{nameof(image)} is a broken image");
            }

            var category = await _categoriesProvider.GetCategoryByIdAsync(id);

            category.Picture = image.ToArray();
            await _categoriesProvider.UpdateCategoryAsync(category);
        }
    }
}
