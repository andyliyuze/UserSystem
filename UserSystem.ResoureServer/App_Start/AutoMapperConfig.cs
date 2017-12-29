using AutoMapper;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Security.Claims;
using UserSystem.Application.DTO;
using UserSystem.Core.Entity;
namespace UserSystem.ResoureServer.App_Start
{
    public class AutoMapperConfig
    {
        public static void Register()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<User, UserOutput>();
                cfg.CreateMap<UserInput, User>();
                cfg.CreateMap<IdentityUserClaim, Claim>().ForMember(d => d.Value, opt => opt.MapFrom(s => s.ClaimValue))
                .ForMember(d => d.ValueType, opt => opt.MapFrom(s => s.ClaimType));
            });
        }
    }
}
