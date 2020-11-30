using System.Collections.Generic;
using System.Threading.Tasks;
using Northwind.Data.Entities;

namespace Northwind.Business.Interfaces.Providers
{
    public interface IProductsProvider
    {
        Task<IEnumerable<ProductEntity>> GetProductsAsync();

        Task<IEnumerable<ProductEntity>> GetProductsAsync(int count);

        Task<ProductEntity> GetProductByIdAsync(int id);

        Task<ProductEntity> AddAsync(ProductEntity product);

        Task UpdateAsync(ProductEntity product);

        Task RemoveAsync(ProductEntity product);
    }
}
