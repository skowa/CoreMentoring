using System.Collections.Generic;
using System.Threading.Tasks;
using Northwind.Data.Entities;

namespace Northwind.Business.Interfaces.Providers
{
    public interface ICategoriesProvider
    {
        Task<IEnumerable<CategoryEntity>> GetCategoriesAsync();
    }
}
