using Autofac;
using Autofac.Extras.CommonServiceLocator;
using CommonServiceLocator;
using UserSystem.AuthorizationServer.App_Start;
namespace Application.Test
{
    public class TestBase
    {
        public TestBase()
        {
            Init();
        
        }

        private void Init()
        {
            //先注册autofac
            var builder = new ContainerBuilder();

            AutofacConfig.RegisterServices(builder);

            var container = builder.Build();

            ServiceLocator.SetLocatorProvider(() =>
            {
                return new AutofacServiceLocator(container);
            });

            AutoMapperConfig.Register();
        }

    }
}
