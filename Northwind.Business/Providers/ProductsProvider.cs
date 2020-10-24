using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
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

        public async Task<IEnumerable<ProductEntity>> GetProductsAsync(int count)
        {
            return await _unitOfWork.Repository<ProductEntity>()
                .GetWith(
                    product => product.Supplier,
                    product => product.Category)
                .Take(count)
                .ToListAsync();
        }

        public Task<ProductEntity> GetProductByIdAsync(int id)
        {
            return _unitOfWork.Repository<ProductEntity>()
                .Get(product => product.ProductID == id)
                .SingleOrDefaultAsync();
        }

        public Task AddAsync(ProductEntity product)
        {
            _unitOfWork.Repository<ProductEntity>().Add(product);

            return _unitOfWork.SaveChangesAsync();
        }

        public Task UpdateAsync(ProductEntity product)
        {
            _unitOfWork.Repository<ProductEntity>().Update(product);

            return _unitOfWork.SaveChangesAsync();
        }
    }
}
