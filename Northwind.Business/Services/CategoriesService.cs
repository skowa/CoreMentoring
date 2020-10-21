using System.Collections.Generic;
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
        private readonly IMapper _mapper;

        public CategoriesService(ICategoriesProvider categoriesProvider, IMapper mapper)
        {
            _categoriesProvider = categoriesProvider;
            _mapper = mapper;
        }

        public async Task<IEnumerable<Category>> GetCategoriesAsync()
        {
            var categories = await _categoriesProvider.GetCategoriesAsync();

            return _mapper.Map<IEnumerable<Category>>(categories);
        }
    }
}
