using System;
using Microsoft.Owin;
using Owin;
using System.Web.Http;
using Microsoft.Owin.Security.OAuth;
using UserSystem.AuthorizationServer.Providers;
using UserSystem.AuthorizationServer.Formats;
using Autofac;
using UserSystem.AuthorizationServer.App_Start;
using Autofac.Integration.WebApi;
using System.Reflection;
using CommonServiceLocator;
using Autofac.Extras.CommonServiceLocator;
using Microsoft.Owin.Security.Cookies;
using System.Threading.Tasks;

namespace UserSystem.AuthorizationServer
{
    public  class Startup
    {
        public   void Configuration(IAppBuilder app)
        {

            var config = new HttpConfiguration();

            //先注册autofac
            var builder = new ContainerBuilder();
            // Register dependencies, then...
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            AutofacConfig.RegisterServices(builder);

            var container = builder.Build();

            ServiceLocator.SetLocatorProvider(() =>
            {
                return new AutofacServiceLocator(container);
            });

            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);

            // Register the Autofac middleware FIRST. This also adds
            // Autofac-injected middleware registered with the container.
            ConfigureOAuth(app);

            app.UseAutofacMiddleware(container);

            WebApiConfig.Register(config);
            AutoMapperConfig.Register();

            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);
            app.UseAutofacWebApi(config);
            app.UseWebApi(config);
        }
     

        private  void ConfigureOAuth(IAppBuilder app)
        {

            OAuthAuthorizationServerOptions OAuthServerOptions = new OAuthAuthorizationServerOptions()
            {
                AuthorizeEndpointPath = PathString.FromUriComponent("/Authorize"),
                //For Dev enviroment only (on production should be AllowInsecureHttp = false)
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/oauth2/token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromMinutes(30),
                Provider = new CustomOAuthProvider()  ,
                AccessTokenFormat = new CustomJwtFormat("http://jwtauthzsrv.azurewebsites.net")
               
            };

            // OAuth 2.0 Bearer Access Token Generation
            app.UseOAuthAuthorizationServer(OAuthServerOptions);

        }


      
    }
}
