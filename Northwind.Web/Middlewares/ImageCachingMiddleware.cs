using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Northwind.Core.Caching.Interfaces;
using Northwind.Core.Web.Constants;
using Northwind.Web.Constants;

namespace Northwind.Web.Middlewares
{
    public class ImageCachingMiddleware
    {
        private const string ImagesControllerName = "Categories";
        private const string ImagesActionName = "Image";
        private const string GetMethod = "GET";

        private readonly RequestDelegate _next;
        private readonly IFilesCachingService _cachingService;

        public ImageCachingMiddleware(RequestDelegate next, IFilesCachingService cachingService)
        {
            _next = next;
            _cachingService = cachingService;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var routeValues = context.Request.RouteValues;
            if (context.Request.Method.Equals(GetMethod, StringComparison.OrdinalIgnoreCase) &&
                string.Equals(routeValues[RouteValuesKeys.ControllerKey] as string, ImagesControllerName, StringComparison.OrdinalIgnoreCase) &&
                string.Equals(routeValues[RouteValuesKeys.ActionKey] as string, ImagesActionName, StringComparison.OrdinalIgnoreCase))
            {
                var fileName = (routeValues[RouteValuesKeys.IdKey] as string) + FileExtensions.Bmp;
                var cacheItem = _cachingService.Get(fileName);
                if (cacheItem != null)
                {
                    context.Response.ContentType = ContentTypes.BmpImage;
                    await context.Response.Body.WriteAsync(cacheItem.ToArray());
                }
                else
                {
                    var originalResponse = context.Response.Body;

                    using var responseMemoryStream = new MemoryStream();
                    context.Response.Body = responseMemoryStream;

                    await _next(context);

                    if (context.Response.ContentType.Equals(ContentTypes.BmpImage))
                    {
                        responseMemoryStream.Seek(0, SeekOrigin.Begin);

                        _cachingService.Add(fileName, responseMemoryStream.ToArray());
                    }

                     await responseMemoryStream.CopyToAsync(originalResponse);
                     context.Response.Body = originalResponse;
                }
            }
            else
            {
                await _next(context);
            }
        }
    }
}
