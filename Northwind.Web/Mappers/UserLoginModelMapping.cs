using AutoMapper;
using Northwind.Core.Domain;
using Northwind.Web.Models;

namespace Northwind.Web.Mappers
{
    internal static class UserLoginModelMapping
    {
        internal static IProfileExpression ApplyUserLoginModelMapping(this IProfileExpression configuration)
        {
            return configuration.FromUserLoginModel();
        }

        private static IProfileExpression FromUserLoginModel(this IProfileExpression configuration)
        {
            configuration.CreateMap<UserLoginModel, User>();

            return configuration;
        }
    }
}
