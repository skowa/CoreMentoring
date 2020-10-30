using System;
using Northwind.Web.Constants;
using Serilog.Core;
using Serilog.Events;

namespace Northwind.Web.Enrichers
{
    public class LogIdEnricher : ILogEventEnricher
    {
        public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
        {
            logEvent.AddPropertyIfAbsent(propertyFactory.CreateProperty(LogContextProperties.LogId, Guid.NewGuid()));
        }
    }
}
