using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Northwind.Business.Interfaces.Providers;
using Northwind.Business.Interfaces.Services;
using Northwind.Core.Domain;

namespace Northwind.Business.Services
{
    public class SuppliersService : ISuppliersService
    {
        private readonly ISuppliersProvider _SuppliersProvider;
        private readonly IMapper _mapper;

        public SuppliersService(ISuppliersProvider SuppliersProvider, IMapper mapper)
        {
            _SuppliersProvider = SuppliersProvider;
            _mapper = mapper;
        }

        public async Task<IEnumerable<Supplier>> GetSuppliersAsync()
        {
            var Suppliers = await _SuppliersProvider.GetSuppliersAsync();

            return _mapper.Map<IEnumerable<Supplier>>(Suppliers);
        }
    }
}
