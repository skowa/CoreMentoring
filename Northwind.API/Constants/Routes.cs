namespace Northwind.API.Constants
{
    public static class Routes
    {
        public const string ControllerApi = "api/[controller]";

        public static class Products
        {
            public const string ProductById = "{id}";
        }

        public static class Categories
        {
            public const string ImageByCategoryId = "{id}/image";
        }
    }
}
