using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using System.Web.Http;
using UserSystem.ResoureServer.App_Start;
using Autofac;
using Autofac.Integration.WebApi;
using System.Reflection;
using Microsoft.Owin.Security.DataHandler.Encoder;
using Microsoft.Owin.Security.Jwt;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using MassTransit;
using MassTransit.Util;
using System.Threading;
using Microsoft.Owin.BuilderProperties;
using System.Configuration;

[assembly: OwinStartup(typeof(UserSystem.ResoureServer.Startup))]

namespace UserSystem.ResoureServer
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // 有关如何配置应用程序的详细信息，请访问 https://go.microsoft.com/fwlink/?LinkID=316888
            var config = new HttpConfiguration();

            //先注册autofac
            var builder = new ContainerBuilder();
            // Register dependencies, then...
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            AutofacConfig.RegisterServices(builder);

            var container = builder.Build();
        
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
                    
            WebApiConfig.Register(config);

            AutoMapperConfig.Register();

            ConfigureOAuth(app);

            BusInit(app, container);

            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);
            app.UseWebApi(config);

        }

        public void ConfigureOAuth(IAppBuilder app)
        {
            //显然如果颁发JWT的issuer，这里的issuer不一致的话
            //也是无法通过验证
            var issuer = "http://jwtauthzsrv.azurewebsites.net";

            //显然如果颁发JWT的audience，这里的aud不一致的话
            //也是无法通过验证
            var audience = "Fish";

            //显然如果颁发JWT的加密秘钥，与解密秘钥不一致的话，得到的签名就会
            //不一致，从而资源服务器会认为jwt被篡改
            //从而无法通过验证
            string symmetricKeyAsBase64 = ConfigurationManager.AppSettings["JwtSecret"].ToString();
            var secret = TextEncodings.Base64Url.Decode(symmetricKeyAsBase64);

            // Api controllers with an [Authorize] attribute will be validated with JWT
            app.UseJwtBearerAuthentication(
                new JwtBearerAuthenticationOptions
                {
                    AuthenticationMode = AuthenticationMode.Active,
                    AllowedAudiences = new[] { audience,"any" } ,
                    IssuerSecurityTokenProviders = new IIssuerSecurityTokenProvider[]
                    {
                        new SymmetricKeyIssuerSecurityTokenProvider(issuer, secret)
                    }
                });
        }



        public void BusInit(IAppBuilder app , IContainer container)
        {
            //启动bus
          //  var bus = container.Resolve<IBusControl>();
          //  CancellationToken cancellationToken = new CancellationToken();
          //  cancellationToken.Register(()=>bus.Stop());
          //var busHandle = TaskUtil.Await(() => bus.StartAsync(cancellationToken));

          //  //当app stop时，bus销毁
          //  var properties = new AppProperties(app.Properties);

          //  if (properties.OnAppDisposing != CancellationToken.None)
          //  {
          //      properties.OnAppDisposing.Register(() => busHandle.Stop(TimeSpan.FromSeconds(30)));
          //  }
        }
    }
}
