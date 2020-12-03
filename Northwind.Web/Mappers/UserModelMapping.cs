using AutoMapper;
using Northwind.Core.Domain;
using Northwind.Web.Models;

namespace Northwind.Web.Mappers
{
    internal static class UserModelMapping
    {
        internal static IProfileExpression ApplyUserModelMapping(this IProfileExpression configuration)
        {
            return configuration.ToUserModel();
        }

        private static IProfileExpression ToUserModel(this IProfileExpression configuration)
        {
            configuration.CreateMap<User, UserModel>();

            return configuration;
        }
    }
}
