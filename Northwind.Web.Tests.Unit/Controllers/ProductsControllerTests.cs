using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Northwind.Business.Interfaces.Services;
using Northwind.Core.Domain;
using Northwind.Web.Controllers;
using Northwind.Web.Models;
using Northwind.Web.Tests.Unit.Builders;
using Xunit;

namespace Northwind.Web.Tests.Unit.Controllers
{
    public class ProductsControllerTests
    {
        private const int TestProductId = 1;
        private const string TestProductName = "Tofu";
        private const string TestCategoryName = "Seafood";
        private const string TestCompanyName = "Exotic Liquids";
        private const string TestErrorKey = "errorKey";
        private const string TestError = "error";

        private readonly Mock<IProductsService> _productsServiceMock;
        private readonly Mock<ICategoriesService> _categoriesServiceMock;
        private readonly Mock<ISuppliersService> _suppliersServiceMock;
        private readonly Mock<IMapper> _mapperMock;

        private readonly ProductsController _sut;

        public ProductsControllerTests()
        {
            _productsServiceMock = new Mock<IProductsService>();

            _categoriesServiceMock = new Mock<ICategoriesService>();
            _categoriesServiceMock.Setup(s => s.GetCategoriesAsync()).ReturnsAsync(FakeCategories());

            _suppliersServiceMock = new Mock<ISuppliersService>();
            _suppliersServiceMock.Setup(s => s.GetSuppliersAsync()).ReturnsAsync(FakeSuppliers());

            _mapperMock = new Mock<IMapper>();

            _sut = new ProductsController(_productsServiceMock.Object, _categoriesServiceMock.Object, _suppliersServiceMock.Object, _mapperMock.Object);
        }

        [Fact]
        public async Task Index_ServiceTimesOnce()
        {
            // Arrange
            var products = FakeProducts();

            _productsServiceMock.Setup(s => s.GetProductsAsync()).ReturnsAsync(products);

            // Act
            var result = await _sut.Index();

            // Assert
            _productsServiceMock.Verify(s => s.GetProductsAsync(), Times.Once());
        }

        [Fact]
        public async Task Index_ReturnsViewModelWithProducts()
        {
            // Arrange
            var products = FakeProducts();
            var productModels = FakeProductModels();

            _productsServiceMock.Setup(s => s.GetProductsAsync()).ReturnsAsync(products);
            _mapperMock.Setup(m => m.Map<IEnumerable<ProductModel>>(products)).Returns(FakeProductModels());

            // Act
            var result = await _sut.Index();

            // Assert
            result.Should().BeOfType<ViewResult>();
            var viewDataModel = result.As<ViewResult>().ViewData.Model;
            viewDataModel.Should().BeAssignableTo<IEnumerable<ProductModel>>();
            viewDataModel.Should().BeEquivalentTo(productModels);
        }

        [Fact]
        public async Task CreateGet_ReturnsView()
        {
            // Act
            var result = await _sut.Index();

            // Assert
            result.Should().BeOfType<ViewResult>();
        }

        [Fact]
        public async Task CreatePost_ServiceTimesOnce()
        {
            // Arrange
            var product = FakeProductEditModel();
            _mapperMock.Setup(m => m.Map<Product>(product)).Returns(FakeProduct());

            // Act
            var result = await _sut.Create(product);

            // Assert
            _productsServiceMock.Verify(s => s.AddProductAsync(It.Is<Product>(p => p.ProductName.Equals(product.ProductName))), Times.Once());
        }

        [Fact]
        public async Task CreatePost_ReturnsRedirectResult()
        {
            // Act
            var result = await _sut.Create(FakeProductEditModel());

            // Assert
            result.Should().BeOfType<RedirectToActionResult>();
        }

        [Fact]
        public async Task CreatePost_ModelIsNotValid_ReturnsViewModelWithProduct()
        {
            // Arrange
            _sut.ModelState.AddModelError(TestErrorKey, TestError);

            // Act
            var result = await _sut.Create(FakeProductEditModel());

            // Assert
            result.Should().BeOfType<ViewResult>();
            var viewDataModel = result.As<ViewResult>().ViewData.Model;
            viewDataModel.Should().BeAssignableTo<ProductEditModel>();
            viewDataModel.Should().BeEquivalentTo(FakeProductEditModel());
        }

        [Fact]
        public async Task UpdateGet_ReturnsView()
        {
            // Arrange
            var product = FakeProduct();
            _productsServiceMock.Setup(s => s.GetProductByIdAsync(TestProductId)).ReturnsAsync(product);
            _mapperMock.Setup(m => m.Map<ProductEditModel>(product)).Returns(FakeProductEditModel());

            // Act
            var result = await _sut.Update(TestProductId);

            // Assert
            result.Should().BeOfType<ViewResult>();
            var viewDataModel = result.As<ViewResult>().ViewData.Model;
            viewDataModel.Should().BeAssignableTo<ProductEditModel>();
            viewDataModel.Should().BeEquivalentTo(FakeProductEditModel());
        }

        [Fact]
        public async Task UpdatePost_ServiceTimesOnce()
        {
            // Arrange
            var product = FakeProductEditModel();
            _mapperMock.Setup(m => m.Map<Product>(product)).Returns(FakeProduct());

            // Act
            var result = await _sut.Update(product);

            // Assert
            _productsServiceMock.Verify(s => s.UpdateProductAsync(It.Is<Product>(p => p.ProductName.Equals(product.ProductName))), Times.Once());
        }

        [Fact]
        public async Task UpdatePost_ReturnsRedirectResult()
        {
            // Act
            var result = await _sut.Update(FakeProductEditModel());

            // Assert
            result.Should().BeOfType<RedirectToActionResult>();
        }

        [Fact]
        public async Task UpdatePost_ModelIsNotValid_ReturnsViewModelWithProduct()
        {
            // Arrange
            _sut.ModelState.AddModelError(TestErrorKey, TestError);

            // Act
            var result = await _sut.Update(FakeProductEditModel());

            // Assert
            result.Should().BeOfType<ViewResult>();
            var viewDataModel = result.As<ViewResult>().ViewData.Model;
            viewDataModel.Should().BeAssignableTo<ProductEditModel>();
            viewDataModel.Should().BeEquivalentTo(FakeProductEditModel());
        }

        private static ProductEditModel FakeProductEditModel()
        {
            return new ProductEditModelBuilder()
                .WithProductName(TestProductName)
                .Build();
        }

        private static Product FakeProduct()
        {
            return new ProductBuilder()
                .WithProductName(TestProductName)
                .Build(); ;
        }

        private static IEnumerable<Product> FakeProducts()
        {
            return new List<Product> { FakeProduct() };
        }

        private static IEnumerable<ProductModel> FakeProductModels()
        {
            var productModel = new ProductModelBuilder()
                .WithProductName(TestProductName)
                .Build();

            return new List<ProductModel> { productModel };
        }

        private static IEnumerable<Category> FakeCategories()
        {
            var category = new CategoryBuilder()
                .WithCategoryName(TestCategoryName)
                .Build();

            return new List<Category> { category };
        }

        private static IEnumerable<Supplier> FakeSuppliers()
        {
            var supplier = new SupplierBuilder()
                .WithCompanyName(TestCompanyName)
                .Build();

            return new List<Supplier> { supplier };
        }
    }
}
