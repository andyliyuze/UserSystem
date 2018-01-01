using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UserSystem.Application.UserService;
using CommonServiceLocator;

namespace UserSystem.AuthorizationServer.Providers
{
    public class CustomOAuthProvider : OAuthAuthorizationServerProvider
    {
        

        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
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
                return;
            }

            context.Validated();
            return;
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {

            var allowedOrigin = context.OwinContext.Get<string>("as:clientAllowedOrigin");

            if (allowedOrigin == null) allowedOrigin = "*";

            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { allowedOrigin });


            var service = ServiceLocator.Current.GetInstance<IUserAppService>();

            try
            {
               
                var identity = await service.TryLogin(context.UserName, context.Password, "JWT");

                if (identity == null)
                {
                    context.SetError("invalid_grant", "The user name or password is incorrect");
                    return;
                }

                var props = new AuthenticationProperties(new Dictionary<string, string>
                {
                    {
                        "as:client_id", context.ClientId ??string.Empty                     },
                    {
                        "userName", context.UserName
                    }
                });

                var ticket = new AuthenticationTicket(identity, props);
                context.Validated(ticket);
                return;
            }
            catch(Exception e) { throw e; }
        }

        public override Task GrantAuthorizationCode(OAuthGrantAuthorizationCodeContext context)
        {

            var a = 123123;
            return base.GrantAuthorizationCode(context);
        }

        public override Task GrantRefreshToken(OAuthGrantRefreshTokenContext context)
        {
            return base.GrantRefreshToken(context);
        }
    }
}