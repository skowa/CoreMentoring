using System.Collections.Generic;

namespace Northwind.Data.Entities
{
    public class SupplierEntity
    {
        public int SupplierID { get; set; }

        public string CompanyName { get; set; }

        public ICollection<ProductEntity> Products { get; set; }
    }
}
