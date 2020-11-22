using System.Collections.Generic;
using System.Threading.Tasks;
using Northwind.Web.Models;

namespace Northwind.Console.Interfaces.Services
{
    public interface INorthwindApiService
    {
        Task<IEnumerable<Category>> GetCategoriesAsync();

        Task<IEnumerable<Product>> GetProductsAsync();
    }
}
