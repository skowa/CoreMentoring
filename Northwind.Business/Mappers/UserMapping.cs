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
                .FromUser();
        }

        private static IProfileExpression FromUser(this IProfileExpression configuration)
        {
            configuration.CreateMap<User, IdentityUser>();

            return configuration;
        }
    }
}
