using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

            AutoMapperConfig.Register();
        }

    }
}
