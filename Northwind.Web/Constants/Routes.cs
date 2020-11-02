namespace Northwind.Web.Constants
{
    public static class Routes
    {
        public static class Products
        {
            public const string Prefix = "products";
            public const string Create = "create";
            public const string Update = "update";
        }

        public static class Categories
        {
            public const string Prefix = "categories";
            public const string Image = "image/{id}";
            public const string Update = "update";
        }
    }
}
