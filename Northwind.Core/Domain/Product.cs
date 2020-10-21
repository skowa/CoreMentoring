namespace Northwind.Core.Domain
{
    public class Product
    {
        public string ProductName { get; set; }

        public string QuantityPerUnit { get; set; }

        public decimal? UnitPrice { get; set; }

        public short? UnitsInStock { get; set; }

        public short? UnitsOnOrder { get; set; }

        public short? ReorderLevel { get; set; }

        public bool Discontinued { get; set; }

        public Category Category { get; set; }

        public Supplier Supplier { get; set; }
    }
}
