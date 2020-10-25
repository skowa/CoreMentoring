using System;
using System.Diagnostics;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Northwind.Web.Constants;
using Northwind.Web.Models;
using Serilog;
using Serilog.Context;

namespace Northwind.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger _logger;

        public HomeController(ILogger logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            var error = HttpContext.Features.Get<IExceptionHandlerFeature>().Error;

            var logId = Guid.NewGuid();
            LogContext.PushProperty(LogContextProperties.LogId, logId);
            _logger.Error(error, LogMessages.UnexpectedError);

            return View(new ErrorModel { LogId = logId });
        }
    }
}
