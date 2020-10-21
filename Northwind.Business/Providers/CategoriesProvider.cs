using System.Collections.Generic;
using System.Threading.Tasks;
using Northwind.Business.Interfaces.Providers;
using Northwind.Data;
using Northwind.Data.Entities;

namespace Northwind.Business.Providers
{
    public class CategoriesProvider : ICategoriesProvider
    {
        private readonly IUnitOfWork _unitOfWork;

        public CategoriesProvider(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Task<IEnumerable<CategoryEntity>> GetCategoriesAsync()
        {
            return _unitOfWork.Repository<CategoryEntity>().GetAsync();
        }
    }
}
