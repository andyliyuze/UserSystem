using Autofac;
using Microsoft.AspNet.Identity;
using UserSystem.Application.UserService;
using UserSystem.Core.Entity;
using UserSystem.Core.Repository;
using UserSystem.Data;
using UserSystem.Data.Repository;

namespace UserSystem.AuthorizationServer.App_Start
{
    public class AutofacConfig
    {    
        public static void RegisterServices(ContainerBuilder builder)
        {
            builder.RegisterType<UserSystemContext>().InstancePerLifetimeScope();
            //使用构造函数配置
            builder.Register(c=>new CustomUserStore(c.Resolve<UserSystemContext>())).As<IUserStore<User>>().InstancePerLifetimeScope();
            //使用构造函数配置
            //builder.Register(c => new CustomUserManager(c.Resolve<CustomUserStore>())).InstancePerLifetimeScope();

            builder.RegisterType<CustomUserManager>().UsingConstructor(typeof(IUserStore<User>)).InstancePerLifetimeScope();
        
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerLifetimeScope();
          
            builder.RegisterType<UserAppService>().As<IUserAppService>().InstancePerLifetimeScope(); 

            builder.RegisterType<UserRepository>().As<IUserRepository>().InstancePerLifetimeScope(); 

        }
    }
}