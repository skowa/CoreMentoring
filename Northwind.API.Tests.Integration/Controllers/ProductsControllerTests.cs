using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Northwind.API.Tests.Integration.Builders;
using Northwind.API.Tests.Integration.Infrastructure;
using Northwind.API.Tests.Integration.Infrastructure.Data;
using Northwind.API.Tests.Integration.Infrastructure.Services;
using Northwind.API.Tests.Integration.Infrastructure.Services.Models;
using Xunit;

namespace Northwind.API.Tests.Integration.Controllers
{
    public class ProductsControllerTests : IClassFixture<InMemoryDbWebApplicationFactory<Startup>>
    {
        private readonly InMemoryDbWebApplicationFactory<Startup> _factory;

        public ProductsControllerTests(InMemoryDbWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task GetProductById_ReturnsExactEntities()
        {
            // Arrange
            var client = _factory.CreateClient();
            var northwindApi = new NorthwindAPI(client, true);
            var expectedProduct = FakeProductModels().First(product => product.ProductId == InMemoryDbEntitiesValues.TestChangProductId);

            // Act
            var response = await northwindApi.GetProductByIdAsync(InMemoryDbEntitiesValues.TestChangProductId);

            // Arrange
            response.Should().NotBeNull();
            response.Should().BeEquivalentTo(expectedProduct);
        }

        [Fact]
        public async Task CreateProduct_ReturnsExactEntities()
        {
            // Arrange
            var client = _factory.CreateClient();
            var northwindApi = new NorthwindAPI(client, true);

            // Act
            var response = await northwindApi.CreateProductAsync(FakeProductEditModel());

            // Arrange
            response.Should().NotBeNull();
            response.As<ProductModel>().ProductName.Should().Be(InMemoryDbEntitiesValues.TestTofuProductName);
        }

        [Fact]
        public async Task UpdateProduct_ReturnsExactEntities()
        {
            // Arrange
            var client = _factory.CreateClient();
            var northwindApi = new NorthwindAPI(client, true);

            // Act
            var response = await northwindApi.UpdateProductWithHttpMessagesAsync(FakeProductEditModel());

            // Arrange
            response.Should().NotBeNull();
            response.Response.IsSuccessStatusCode.Should().BeTrue();
        }

        [Fact]
        public async Task DeleteProduct_ReturnsExactEntities()
        {
            // Arrange
            var client = _factory.CreateClient();
            var northwindApi = new NorthwindAPI(client, true);

            // Act
            var response = await northwindApi.DeleteProductByIdWithHttpMessagesAsync(InMemoryDbEntitiesValues.TestTofuProductId);

            // Arrange
            response.Should().NotBeNull();
            response.Response.IsSuccessStatusCode.Should().BeTrue();
        }

        private static IEnumerable<ProductModel> FakeProductModels()
        {
            var tofuProduct = new ProductModelBuilder()
                    .WithProductId(InMemoryDbEntitiesValues.TestTofuProductId)
                    .WithProductName(InMemoryDbEntitiesValues.TestTofuProductName)
                    .WithCategoryName(InMemoryDbEntitiesValues.TestSeafoodCategoryName)
                    .WithSupplierName(InMemoryDbEntitiesValues.TestSunnyDaySupplierName)
                    .Build();

            var changProduct = new ProductModelBuilder()
                    .WithProductId(InMemoryDbEntitiesValues.TestChangProductId)
                    .WithProductName(InMemoryDbEntitiesValues.TestChangProductName)
                    .WithCategoryName(InMemoryDbEntitiesValues.TestMeatCategoryName)
                    .WithSupplierName(InMemoryDbEntitiesValues.TestSunnyDaySupplierName)
                    .Build();

            return new[] { tofuProduct, changProduct };
        }

        private static ProductEditModel FakeProductEditModel(int productId = default)
        {
            return new ProductEditModelBuilder()
                .WithProductId(productId)
                .WithProductName(InMemoryDbEntitiesValues.TestTofuProductName)
                .WithCategoryId(InMemoryDbEntitiesValues.TestMeatCategoryId)
                .WithSupplierId(InMemoryDbEntitiesValues.TestSunnyDaySupplierId)
                .Build();
        }
    }
}
