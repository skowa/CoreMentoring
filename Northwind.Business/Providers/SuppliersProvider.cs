using System.Collections.Generic;
using System.Threading.Tasks;
using Northwind.Business.Interfaces.Providers;
using Northwind.Data;
using Northwind.Data.Entities;

namespace Northwind.Business.Providers
{
    public class SuppliersProvider : ISuppliersProvider
    {
        private readonly IUnitOfWork _unitOfWork;

        public SuppliersProvider(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Task<IEnumerable<SupplierEntity>> GetSuppliersAsync()
        {
            return _unitOfWork.Repository<SupplierEntity>().GetAsync();
        }
    }
}
