using Autofac;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using UserSystem.Application.UserService;
using UserSystem.Core.Repository;
using UserSystem.Data;
using UserSystem.Data.Repository;

namespace UserSystem.AuthorizationServer.App_Start
{
    public class AutofacConfig
    {
        
        public static void RegisterServices(ContainerBuilder builder)
        {
            builder.RegisterType<UserSystemContext>().InstancePerRequest();
            //使用构造函数配置
            builder.Register(c=>new UserStore<IdentityUser>(c.Resolve<UserSystemContext>())).As<IUserStore<IdentityUser>>().InstancePerRequest();
            //使用构造函数配置
            builder.RegisterType<UserManager<IdentityUser>>().UsingConstructor(typeof(IUserStore<IdentityUser>)).InstancePerRequest();
        
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerRequest();
          
            builder.RegisterType<UserAppService>().As<IUserAppService>().InstancePerRequest(); 

            builder.RegisterType<UserRepository>().As<IUserRepository>().InstancePerRequest(); 

        }
    }
}