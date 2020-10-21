using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Northwind.Business.Interfaces.Providers;
using Northwind.Business.Interfaces.Services;
using Northwind.Core.Domain;

namespace Northwind.Business.Services
{
    public class ProductsService : IProductsService
    {
        private readonly IProductsProvider _productsProvider;
        private readonly IMapper _mapper;

        public ProductsService(IProductsProvider productsProvider, IMapper mapper)
        {
            _productsProvider = productsProvider;
            _mapper = mapper;
        }

        public async Task<IEnumerable<Product>> GetProductsAsync()
        {
            var products = await _productsProvider.GetProductsAsync();

            return _mapper.Map<IEnumerable<Product>>(products);
        }
    }
}
