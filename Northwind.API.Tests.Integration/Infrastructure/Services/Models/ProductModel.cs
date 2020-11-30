// <auto-generated>
// Code generated by Microsoft (R) AutoRest Code Generator.
// Changes may cause incorrect behavior and will be lost if the code is
// regenerated.
// </auto-generated>

namespace Northwind.API.Tests.Integration.Infrastructure.Services.Models
{
    using Newtonsoft.Json;
    using System.Linq;

    public partial class ProductModel
    {
        /// <summary>
        /// Initializes a new instance of the ProductModel class.
        /// </summary>
        public ProductModel()
        {
            CustomInit();
        }

        /// <summary>
        /// Initializes a new instance of the ProductModel class.
        /// </summary>
        public ProductModel(int? productId = default(int?), string productName = default(string), string categoryName = default(string), string supplierName = default(string), string quantityPerUnit = default(string), double? unitPrice = default(double?), int? unitsInStock = default(int?), int? unitsOnOrder = default(int?), int? reorderLevel = default(int?), bool discontinued = default(bool))
        {
            ProductId = productId;
            ProductName = productName;
            CategoryName = categoryName;
            SupplierName = supplierName;
            QuantityPerUnit = quantityPerUnit;
            UnitPrice = unitPrice;
            UnitsInStock = unitsInStock;
            UnitsOnOrder = unitsOnOrder;
            ReorderLevel = reorderLevel;
            Discontinued = discontinued;
            CustomInit();
        }

        /// <summary>
        /// An initialization method that performs custom operations like setting defaults
        /// </summary>
        partial void CustomInit();

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "productId")]
        public int? ProductId { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "productName")]
        public string ProductName { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "categoryName")]
        public string CategoryName { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "supplierName")]
        public string SupplierName { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "quantityPerUnit")]
        public string QuantityPerUnit { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "unitPrice")]
        public double? UnitPrice { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "unitsInStock")]
        public int? UnitsInStock { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "unitsOnOrder")]
        public int? UnitsOnOrder { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "reorderLevel")]
        public int? ReorderLevel { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "discontinued")]
        public bool Discontinued { get; set; }

    }
}
