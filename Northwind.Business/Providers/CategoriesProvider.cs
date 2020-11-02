using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
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

        public Task<CategoryEntity> GetCategoryByIdAsync(int id)
        {
            return _unitOfWork.Repository<CategoryEntity>().Get(category => category.CategoryID == id).FirstOrDefaultAsync();
        }

        public Task UpdateCategoryAsync(CategoryEntity category)
        {
            _unitOfWork.Repository<CategoryEntity>().Update(category);

            return _unitOfWork.SaveChangesAsync();
        }
    }
}
