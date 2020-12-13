using AutoMapper;
using Northwind.Core.Domain;
using Northwind.Web.Models;

namespace Northwind.Web.Mappers
{
    internal static class ResetPasswordModelMapping
    {
        internal static IProfileExpression ApplyResetPasswordModelMapping(this IProfileExpression configuration)
        {
            return configuration.FromResetPasswordModel();
        }

        private static IProfileExpression FromResetPasswordModel(this IProfileExpression configuration)
        {
            configuration.CreateMap<ResetPasswordModel, User>();

            return configuration;
        }
    }
}
