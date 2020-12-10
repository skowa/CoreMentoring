using AutoMapper;
using Northwind.Core.Domain;
using Northwind.Web.Models;

namespace Northwind.Web.Mappers
{
    internal static class UserRegisterModelMapping
    {
        internal static IProfileExpression ApplyUserRegisterModelMapping(this IProfileExpression configuration)
        {
            return configuration.FromUserRegisterModel();
        }

        private static IProfileExpression FromUserRegisterModel(this IProfileExpression configuration)
        {
            configuration.CreateMap<UserRegisterModel, User>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(source => source.Email));

            return configuration;
        }
    }
}
