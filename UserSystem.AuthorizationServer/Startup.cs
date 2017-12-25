using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using System.Web.Http;
using Microsoft.Owin.Security.OAuth;
using UserSystem.AuthorizationServer.Providers;
using UserSystem.AuthorizationServer.Formats;
using UserSystem.AuthorizationServer.API;
using Autofac;
using UserSystem.AuthorizationServer.App_Start;
using UserSystem.Application.UserService;
using Autofac.Integration.WebApi;
using System.Reflection;
using CommonServiceLocator;
using Autofac.Extras.CommonServiceLocator;

 

namespace UserSystem.AuthorizationServer
{
    public  class Startup
    {
        public static  void Configuration(IAppBuilder app)
        {

            var config = new HttpConfiguration();

            //先注册autofac
            var builder = new ContainerBuilder();
            // Register dependencies, then...
            AutofacConfig.RegisterServices(builder);
            var container = builder.Build();

           
            // Register the Autofac middleware FIRST. This also adds
            // Autofac-injected middleware registered with the container.
            app.UseAutofacMiddleware(container);

            var csl = new AutofacServiceLocator(container);
            ServiceLocator.SetLocatorProvider(() => csl);

            //builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

         //   config.DependencyResolver = new AutofacWebApiDependencyResolver(container); 
            
            WebApiConfig.Register(config);
        
            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);
            
            ConfigureOAuth(app);

            app.UseWebApi(config);
        }



        static void ConfigureOAuth(IAppBuilder app)
        {  

            OAuthAuthorizationServerOptions OAuthServerOptions = new OAuthAuthorizationServerOptions()
            {
                //For Dev enviroment only (on production should be AllowInsecureHttp = false)
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/oauth2/token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromMinutes(30),
                Provider = new CustomOAuthProvider(),
                AccessTokenFormat = new CustomJwtFormat("http://jwtauthzsrv.azurewebsites.net")
            };

            // OAuth 2.0 Bearer Access Token Generation
            app.UseOAuthAuthorizationServer(OAuthServerOptions);

        }
    }
}
