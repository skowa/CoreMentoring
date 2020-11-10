using System.Collections.Generic;

namespace Northwind.Data.Entities
{
    public class CategoryEntity
    {
        public int CategoryID { get; set; }

        public string CategoryName { get; set; }

        public string Description { get; set; }

        public byte[] Picture { get; set; }

        public ICollection<ProductEntity> Products { get; set; }
    }
}
