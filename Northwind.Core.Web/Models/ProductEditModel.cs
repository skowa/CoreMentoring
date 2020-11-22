using System.ComponentModel.DataAnnotations;

namespace Northwind.Core.Web.Models
{
    public class ProductEditModel
    {
        public int ProductId { get; set; }

        [Display(Name = "Product name")]
        [Required]
        [MinLength(4)]
        public string ProductName { get; set; }

        [Display(Name = "Category")]
        public int CategoryId { get; set; }

        [Display(Name = "Supplier")]
        public int SupplierId { get; set; }

        [Display(Name = "Quantity Per Unit")]
        public string QuantityPerUnit { get; set; }

        [Display(Name = "Price")]
        [Range(0, int.MaxValue)]
        public decimal? UnitPrice { get; set; }

        [Display(Name = "Units in stock")]
        [Range(0, int.MaxValue)]
        public short? UnitsInStock { get; set; }

        [Display(Name = "Units on order")]
        [Range(0, int.MaxValue)]
        public short? UnitsOnOrder { get; set; }

        [Display(Name = "Reorder level")]
        [Range(0, 30)]
        public short? ReorderLevel { get; set; }

        public bool Discontinued { get; set; }
    }
}
