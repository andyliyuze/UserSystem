using AutoMapper;
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
            });
        }
    }
}
