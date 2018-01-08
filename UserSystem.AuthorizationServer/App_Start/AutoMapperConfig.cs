using AutoMapper;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Security.Claims;
using UserSystem.Application.DTO;
using UserSystem.Core.Entity;

namespace UserSystem.AuthorizationServer.App_Start
{
    public class AutoMapperConfig
    {
        public static void Register()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<User, UserOutput>();
                cfg.CreateMap<UserInput, User>();
                cfg.CreateMap<IdentityUserClaim, Claim>()
                .ConstructUsing(x => new Claim(x.ClaimType, x.ClaimValue))
                .ForMember(d => d.Value, opt => opt.MapFrom(s => s.ClaimValue))
                .ForMember(d => d.Type, opt => opt.MapFrom(s => s.ClaimType));

                cfg.CreateMap<AppClient, AppClinetOutput>();
            });
        }
    }
}
