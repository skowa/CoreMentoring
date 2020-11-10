using System;
using FluentAssertions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Northwind.Web.Controllers;
using Northwind.Web.Models;
using Serilog;
using Xunit;

namespace Northwind.Web.Tests.Unit.Controllers
{
    public class HomeControllerTests
    {
        private readonly Mock<ILogger> _loggerMock;

        private readonly HomeController _sut;

        public HomeControllerTests()
        {
            _loggerMock = new Mock<ILogger>();

            _sut = new HomeController(_loggerMock.Object);
        }

        [Fact]
        public void Index_ReturnsViewResult()
        {
            // Act
            var result = _sut.Index();

            // Assert
            result.Should().BeOfType<ViewResult>();
        }

        [Fact]
        public void Error_ReturnsViewResultWithModel()
        {
            // Arrange
            _sut.ControllerContext = new ControllerContext { HttpContext = new DefaultHttpContext() };
            _sut.HttpContext.Features.Set<IExceptionHandlerFeature>(new ExceptionHandlerFeature
            {
                Error = new ArgumentException()
            });

            // Act
            var result = _sut.Error();

            // Assert
            result.Should().BeOfType<ViewResult>();
            var viewDataModel = result.As<ViewResult>().ViewData.Model;
            viewDataModel.Should().BeAssignableTo<ErrorModel>();
        }
    }
}
