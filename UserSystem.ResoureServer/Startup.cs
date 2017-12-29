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
            var audience = "ngAuthApp";

            
            //显然如果颁发JWT的加密秘钥，与解密秘钥不一致的话，是无法进行JWT解密
            //从而无法通过验证
            var secret = TextEncodings.Base64Url.Decode("IxrAjDoa2FqElO7IhrSrUJELhUckePEPVpaePlS_Xaw");

            // Api controllers with an [Authorize] attribute will be validated with JWT
            app.UseJwtBearerAuthentication(
                new JwtBearerAuthenticationOptions
                {
                    AuthenticationMode = AuthenticationMode.Active,
                    AllowedAudiences = new[] { audience },
                    IssuerSecurityTokenProviders = new IIssuerSecurityTokenProvider[]
                    {
                        new SymmetricKeyIssuerSecurityTokenProvider(issuer, secret)
                    }
                });
        }
    }
}
