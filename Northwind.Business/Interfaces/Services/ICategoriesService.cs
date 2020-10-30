using System.Collections.Generic;
using System.Threading.Tasks;
using Northwind.Core.Domain;

namespace Northwind.Business.Interfaces.Services
{
    public interface ICategoriesService
    {
        Task<IEnumerable<Category>> GetCategoriesAsync();
    }
}
