using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Northwind.Web.Constants;
using Northwind.Web.Models;

namespace Northwind.Web.ViewComponents
{
    public class BreadcrumbsViewComponent : ViewComponent
    {
        private const string HomeControllerName = "Home";
        private const string CreateActionName = "Create";
        private const string UpdateActionName = "Update";
        private const string EntitiesLink = "/{0}";

        public IViewComponentResult Invoke()
        {
            var breadcrumbs = new List<BreadcrumbModel>();

            var routeValues = HttpContext.Request.RouteValues;
            var currentControllerName = routeValues[RouteValuesKeys.ControllerKey] as string;
            if (!string.Equals(currentControllerName, HomeControllerName))
            {
                breadcrumbs.Add(new BreadcrumbModel { Name = HomeControllerName, Link = string.Format(EntitiesLink, HomeControllerName) });

                var entityBreadcrumb = new BreadcrumbModel { Name = currentControllerName };
                var currentActionName = routeValues[RouteValuesKeys.ActionKey] as string;
                if (string.Equals(currentActionName, CreateActionName) ||
                    string.Equals(currentActionName, UpdateActionName))
                {
                    entityBreadcrumb.Link = string.Format(EntitiesLink, currentControllerName);

                    breadcrumbs.Add(entityBreadcrumb);
                    breadcrumbs.Add(new BreadcrumbModel { Name = currentActionName });
                }
                else
                {
                    breadcrumbs.Add(entityBreadcrumb);
                }
            }

            return View(breadcrumbs);
        }
    }
}
