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
            builder.RegisterType<UserSystemContext>();
            //使用构造函数配置
            //builder.RegisterType<UserStore<IdentityUser>>().As<IUserStore<IdentityUser>>();
            builder.Register(c=>new UserStore<IdentityUser>(c.Resolve<UserSystemContext>())).As<IUserStore<IdentityUser>>();
            //使用构造函数配置
            builder.RegisterType<UserManager<IdentityUser>>().UsingConstructor(typeof(IUserStore<IdentityUser>));
         


            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>();
            builder.RegisterType<UserAppService>().As<IUserAppService>();
            builder.RegisterType<UserRepository>().As<IUserRepository>();

        }
    }
}