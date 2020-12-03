using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Northwind.Core.Domain;

namespace Northwind.Business.Mappers
{
    internal static class UserMapping
    {
        internal static IProfileExpression ApplyUserMapping(this IProfileExpression configuration)
        {
            return configuration
                .FromUser()
                .ToUser();
        }

        private static IProfileExpression FromUser(this IProfileExpression configuration)
        {
            configuration.CreateMap<User, IdentityUser>();

            return configuration;
        }

        private static IProfileExpression ToUser(this IProfileExpression configuration)
        {
            configuration.CreateMap<IdentityUser, User>();

            return configuration;
        }
    }
}
