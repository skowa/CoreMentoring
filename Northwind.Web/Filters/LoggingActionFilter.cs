using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Options;
using Northwind.Core.Options;
using Northwind.Web.Constants;
using Serilog;

namespace Northwind.Web.Filters
{
    public class LoggingActionFilter : IActionFilter
    {
        private readonly ILogger _logger;
        private readonly LoggingOptions _loggingOptions;

        public LoggingActionFilter(ILogger logger, IOptions<LoggingOptions> loggingOptions)
        {
            _logger = logger;
            _loggingOptions = loggingOptions.Value;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            _logger.Information(LogMessages.ActionMethodStarted, context.ActionDescriptor.DisplayName);
            if (_loggingOptions.LogParameters)
            {
                _logger.Information(LogMessages.ActionMethodParameters, context.ActionArguments);
            }
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            _logger.Information(LogMessages.ActionMethodEnded, context.ActionDescriptor.DisplayName);
        }
    }
}
