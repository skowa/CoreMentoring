namespace Northwind.API.Tests.Integration.Infrastructure.Data
{
    internal static class InMemoryDbEntitiesValues
    {
        internal const int TestSeafoodCategoryId = 1;
        internal const string TestSeafoodCategoryName = "Seafood";
        internal static readonly byte[] TestSeafoodCategoryPicture = new byte[] { 1, 2, 3 };

        internal const int TestMeatCategoryId = 2;
        internal const string TestMeatCategoryName = "Meat";
        internal static readonly byte[] TestMeatCategoryPicture = new byte[] { 1, 2, 3 };

        internal const int TestSunnyDaySupplierId = 1;
        internal const string TestSunnyDaySupplierName = "Sunny Day";

        internal const int TestTofuProductId = 1;
        internal const string TestTofuProductName = "Tofu";
        internal const int TestTofuCategoryId = 1;
        internal const int TestTofuSupplierId = 1;

        internal const int TestChangProductId = 2;
        internal const string TestChangProductName = "Chang";
        internal const int TestChangCategoryId = 2;
        internal const int TestChangSupplierId = 1;
    }
}
