namespace Northwind.Web.Models
{
    public class Product
    {
        public int ProductId { get; set; }

        public string ProductName { get; set; }

        public string CategoryName { get; set; }

        public string SupplierName { get; set; }

        public string QuantityPerUnit { get; set; }

        public decimal? UnitPrice { get; set; }

        public int? UnitsInStock { get; set; }

        public int? UnitsOnOrder { get; set; }

        public int? ReorderLevel { get; set; }

        public bool Discontinued { get; set; }

        public override string ToString()
        {
            return $"Product Id - {ProductId}; Product Name - {ProductName}; Category Name - {CategoryName}; Supplier Name - {SupplierName}";
        }
    }
}
