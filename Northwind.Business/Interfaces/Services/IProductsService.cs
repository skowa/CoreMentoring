using System.Collections.Generic;
using System.Threading.Tasks;
using Northwind.Core.Domain;

namespace Northwind.Business.Interfaces.Services
{
    public interface IProductsService
    {
        Task<IEnumerable<Product>> GetProductsAsync();

        Task<Product> GetProductByIdAsync(int id);

        Task<Product> AddProductAsync(Product product);

        Task UpdateProductAsync(Product product);

        Task DeleteProductAsync(int id);
    }
}
