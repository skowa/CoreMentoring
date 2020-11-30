using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Northwind.Business.Interfaces.Services;
using Northwind.Core.Domain;
using Northwind.Core.Web.Models;
using Northwind.Web.Controllers;
using Northwind.Web.Tests.Unit.Builders;
using Xunit;

namespace Northwind.Web.Tests.Unit.Controllers
{
    public class CategoriesControllerTests
    {
        private const string TestCategoryName = "Seafood";

        private readonly Mock<ICategoriesService> _categoriesServiceMock;
        private readonly Mock<IMapper> _mapperMock;

        private readonly CategoriesController _sut;

        private readonly IEnumerable<Category> _categories;

        public CategoriesControllerTests()
        {
            _categories = FakeCategories();

            _categoriesServiceMock = new Mock<ICategoriesService>();
            _categoriesServiceMock.Setup(s => s.GetCategoriesAsync()).ReturnsAsync(_categories);

            _mapperMock = new Mock<IMapper>();

            _sut = new CategoriesController(_categoriesServiceMock.Object, _mapperMock.Object);
        }

        [Fact]
        public async Task Index_ServiceTimesOnce()
        {
            // Act
            var result = await _sut.Index();

            // Assert
            _categoriesServiceMock.Verify(s => s.GetCategoriesAsync(), Times.Once());
        }

        [Fact]
        public async Task Index_ReturnsViewResultWithModel()
        {
            // Arrange
            var categoryModels = FakeCategoryModels();
            _mapperMock.Setup(m => m.Map<IEnumerable<CategoryModel>>(_categories)).Returns(FakeCategoryModels());

            // Act
            var result = await _sut.Index();

            // Assert
            result.Should().BeOfType<ViewResult>();
            var viewDataModel = result.As<ViewResult>().ViewData.Model;
            viewDataModel.Should().BeAssignableTo<IEnumerable<CategoryModel>>();
            viewDataModel.Should().BeEquivalentTo(categoryModels);
        }

        private static IEnumerable<Category> FakeCategories()
        {
            var category = new CategoryBuilder()
                .WithCategoryName(TestCategoryName)
                .Build();

            return new List<Category> { category };
        }

        private static IEnumerable<CategoryModel> FakeCategoryModels()
        {
            var categoryModel = new CategoryModelBuilder()
                .WithCategoryName(TestCategoryName)
                .Build();

            return new List<CategoryModel> { categoryModel };
        }
    }
}
