using System.Collections.Generic;
using System.Threading.Tasks;
using Northwind.Business.Interfaces.Providers;
using Northwind.Data;
using Northwind.Data.Entities;

namespace Northwind.Business.Providers
{
    public class ProductsProvider : IProductsProvider
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductsProvider(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Task<IEnumerable<ProductEntity>> GetProductsAsync()
        {
            return _unitOfWork.Repository<ProductEntity>()
                .GetWithAsync(
                    product => product.Supplier,
                    product => product.Category);
        }
    }
}
