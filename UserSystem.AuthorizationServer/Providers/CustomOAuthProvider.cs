using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using UserSystem.Application.UserService;
using Autofac;
using System.Web.Http.Dependencies;
using CommonServiceLocator;
using Microsoft.AspNet.Identity;
using UserSystem.Core.Entity;
using UserSystem.Data;
using Microsoft.AspNet.Identity.EntityFramework;

namespace UserSystem.AuthorizationServer.Providers
{
    public class CustomOAuthProvider : OAuthAuthorizationServerProvider
    {
        

        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            string clientId = string.Empty;
            string clientSecret = string.Empty;
            string symmetricKeyAsBase64 = string.Empty;

            if (!context.TryGetBasicCredentials(out clientId, out clientSecret))
            {
                context.TryGetFormCredentials(out clientId, out clientSecret);
            }

            if (context.ClientId == null)
            {
                context.SetError("invalid_clientId", "client_Id is not set");
                return Task.FromResult<object>(null);
            }

            context.Validated();
            return Task.FromResult<object>(null);
        }

        public override Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {

            var dbcontext = ServiceLocator.Current.GetInstance<UserSystemContext>();
            var users = dbcontext.Users.ToList();
            //var   _userAppService = ServiceLocator.Current.GetInstance<IUserAppService>();
            var store = ServiceLocator.Current.GetInstance<IUserStore<IdentityUser>>();
            var _userManger = ServiceLocator.Current.GetInstance<UserManager<IdentityUser>>();
            var aa = store.FindByIdAsync("525f9c96-b818-451a-a961-303b73875cc4").Result;
            //var user=      _userAppService.FindUser("525f9c96-b818-451a-a961-303b73875cc4").Result;
            var list = _userManger.Users.ToList();
            var result = _userManger.CreateAsync(new IdentityUser() { Id = Guid.NewGuid().ToString(), UserName = "Andy2" }, "123456").Result;
            var oo = _userManger.FindById("525f9c96-b818-451a-a961-303b73875cc4");
            //context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });

            //Dummy check here, you need to do your DB checks against membership system http://bit.ly/SPAAuthCode
            if (context.UserName ==null)
            {
                context.SetError("invalid_grant", "The user name or password is incorrect");
                //return;
                return Task.FromResult<object>(null);
            }

            var identity = new ClaimsIdentity("JWT");

            identity.AddClaim(new Claim(ClaimTypes.Name, context.UserName));
            identity.AddClaim(new Claim("sub", context.UserName));
            identity.AddClaim(new Claim(ClaimTypes.Role, "Manager"));
            identity.AddClaim(new Claim(ClaimTypes.Role, "Supervisor"));

            var props = new AuthenticationProperties(new Dictionary<string, string>
                {
                    {
                         "audience", context.ClientId ??string.Empty                     }
                });

            var ticket = new AuthenticationTicket(identity, props);
            context.Validated(ticket);
            return Task.FromResult<object>(null);
        }
    }
}