using Autofac;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using UserSystem.Application.UserService;
using UserSystem.Core.Entity;
using UserSystem.Core.Repository;
using UserSystem.Data;
using UserSystem.Data.Repository;
using UserSystem.Infrastructure;
using MassTransit;
namespace UserSystem.ResoureServer.App_Start
{
    public class AutofacConfig
    {     
        public static void RegisterServices(ContainerBuilder builder)
        {
            builder.RegisterType<UserSystemContext>().InstancePerRequest();
            //使用构造函数配置
            builder.Register(c=>new CustomUserStore(c.Resolve<UserSystemContext>())).As<IUserStore<User>>().InstancePerRequest();
            //使用构造函数配置
          //  builder.Register(c => new CustomUserManager(c.Resolve<CustomUserStore>())).InstancePerLifetimeScope();

            builder.RegisterType<CustomUserManager>().UsingConstructor(typeof(IUserStore<User>)).InstancePerRequest();
        
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerRequest();
          
            builder.RegisterType<UserAppService>().As<IUserAppService>().InstancePerRequest(); 

            builder.RegisterType<UserRepository>().As<IUserRepository>().InstancePerRequest();

            builder.Register<IBusControl>(c => BusInitializer.CreateBus())
            .As<IBusControl>()
            .As<IPublishEndpoint>()
            .SingleInstance();
        }
    }
}