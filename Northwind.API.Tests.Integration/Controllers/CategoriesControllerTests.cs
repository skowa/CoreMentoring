using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using Northwind.API.Tests.Integration.Builders;
using Northwind.API.Tests.Integration.Infrastructure;
using Northwind.API.Tests.Integration.Infrastructure.Data;
using Northwind.API.Tests.Integration.Infrastructure.Services;
using Northwind.API.Tests.Integration.Infrastructure.Services.Models;
using Xunit;

namespace Northwind.API.Tests.Unit.Controllers
{
    public class CategoriesControllerTests : IClassFixture<InMemoryDbWebApplicationFactory<Startup>>
    {
        private readonly InMemoryDbWebApplicationFactory<Startup> _factory;

        public CategoriesControllerTests(InMemoryDbWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task GetCategories_ReturnsExactEntities()
        {
            // Arrange
            var client = _factory.CreateClient();
            var northwindApi = new NorthwindAPI(client, true);
            var expectedCategories = FakeCategoryModels();

            // Act
            var response = await northwindApi.GetCategoriesAsync();

            // Arrange
            response.Should().NotBeNull();
            response.Should().HaveCount(2);
            response.Should().BeEquivalentTo(expectedCategories);
        }

        [Fact]
        public async Task GetImage_ReturnsImage()
        {
            // Arrange
            var client = _factory.CreateClient();
            var northwindApi = new NorthwindAPI(client, true);

            // Act
            var response = await northwindApi.ImageAsync(InMemoryDbEntitiesValues.TestSeafoodCategoryId);

            // Arrange
            response.Should().NotBeNull();
        }

        private static IEnumerable<CategoryModel> FakeCategoryModels()
        {
            var seafoodCategory = new CategoryModelBuilder()
                    .WithCategoryId(InMemoryDbEntitiesValues.TestSeafoodCategoryId)
                    .WithCategoryName(InMemoryDbEntitiesValues.TestSeafoodCategoryName)
                    .Build();

            var meatCategory = new CategoryModelBuilder()
                    .WithCategoryId(InMemoryDbEntitiesValues.TestMeatCategoryId)
                    .WithCategoryName(InMemoryDbEntitiesValues.TestMeatCategoryName)
                    .Build();

            return new[] { seafoodCategory, meatCategory };
        }
    }
}
