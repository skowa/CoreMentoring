namespace Northwind.Web.Models
{
    public class Category
    {
        public int CategoryId { get; set; }

        public string CategoryName { get; set; }

        public string Description { get; set; }

        public override string ToString()
        {
            return $"Category Id - {CategoryId}; Category Name - {CategoryName}; Description - {Description}";
        }
    }
}
