using Autofac;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Reflection;
using System.Web.Http;
using UserSystem.Application.UserService;
using UserSystem.Core.Entity;
using UserSystem.Core.Repository;
using UserSystem.Data;
using UserSystem.Data.Repository;
using Microsoft.Owin;
using System.Net.Http;
using Autofac.Extras.CommonServiceLocator;

namespace UserSystem.AuthorizationServer.App_Start
{
    public class AutofacConfig
    {
        
        public static void RegisterServices(ContainerBuilder builder)
        {
            builder.RegisterType<UserSystemContext>().AsSelf().SingleInstance();
            builder.RegisterType<UserStore<User>>().As<IUserStore<User>>().SingleInstance();
            builder.RegisterType<UserManager<User>>().AsSelf().SingleInstance();

            builder.RegisterType<UserSystemContext>().As<IDbContext>().SingleInstance();
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().SingleInstance();

           

            builder.RegisterType<UserAppService>().As<IUserAppService>().SingleInstance();

            builder.RegisterType<UserRepository>().As<IUserRepository>().SingleInstance();


         

        }
    }
}