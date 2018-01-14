using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UserSystem.Application.UserService;
using CommonServiceLocator;
using System.Net;
using UserSystem.Application.AppClientService;
using Microsoft.Owin;
using UserSystem.Application.DTO;

namespace UserSystem.AuthorizationServer.Providers
{
    public class CustomOAuthProvider : OAuthAuthorizationServerProvider
    {

        private  IAppClientAppService clientService;

        public CustomOAuthProvider()
        {
            clientService = ServiceLocator.Current.GetInstance<IAppClientAppService>();
        }

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

            var client =await clientService.Find(context.ClientId);
            if (client == null)
            {
                context.SetError("invalid_clientId");
                return;
            }

            if (!await MyValidateClientRedirectUri(client, context))
            {
                return;
            };
            context.OwinContext.Set<string>("as:aud", "any");
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
                        "as:client_id", context.ClientId ??string.Empty
                    },
                    {
                        "userName", context.UserName
                    },
                    {
                        "as:aud" , context.OwinContext.Get<string>("as:aud") ?? "any"
                    }
                });

                var ticket = new AuthenticationTicket(identity, props);
                context.Validated(ticket);
                return;
            }
            catch (Exception e) { throw e; }
        }

   
        private async Task<bool> MyValidateClientRedirectUri(AppClinetOutput client, OAuthValidateClientAuthenticationContext context)
        {
            var parms =await context.OwinContext.Request.ReadFormAsync();
     
            string redirect_uri = parms.Get("redirect_uri");
          
            if (redirect_uri == null)
            {             
                return true;
            }
     
            if (client == null)
            {
                context.SetError("invalid_clientId");
                return false;
            }

            Uri clientUri;
            if (!Uri.TryCreate(client.RetrunUrl, UriKind.Absolute, out clientUri))
            {
                return true;
            }

            Uri uri;
            if (!Uri.TryCreate(redirect_uri, UriKind.Absolute, out uri)
                ||( uri.Scheme.ToLower() != "http"
                &&uri.Scheme.ToLower() != "https"))
            {
                context.SetError("invalid_RedirectUri");
                return false;
            }

            if (uri.Host != clientUri.Host)
            {
                context.SetError("invalid_RedirectUri", "与数据库url不一致");
                return false;
            }
            context.Validated();
            return  true;
        }
     
    }
}