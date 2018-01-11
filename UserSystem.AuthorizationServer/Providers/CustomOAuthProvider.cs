using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UserSystem.Application.UserService;
using CommonServiceLocator;
using System.Net;
using UserSystem.Application.AppClientService;

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

            if (context.Parameters["returnUrl"] != null)
            {
                var urlInput = context.Parameters["returnUrl"].ToString();

                Uri uri;

                var result = Uri.TryCreate(urlInput, UriKind.Absolute, out uri);

                if (!result)
                {
                    context.SetError("invalid_returnUrl", "无效的地址");
                }
                if (new Uri(client.RetrunUrl).Host != new Uri(urlInput).Host)
                {
                    context.SetError("invalid_returnUrl", "无效的重定向地址，与注册时不一致");
                }
            }
          
            context.OwinContext.Set<string>("as:aud", "any");
            context.Validated(context.ClientId);
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

        //public override Task TokenEndpoint(OAuthTokenEndpointContext context)
        //{
           
        //    return base.TokenEndpoint(context);
        //}



        //public override Task TokenEndpointResponse(OAuthTokenEndpointResponseContext context)
        //{
          
        //    return base.TokenEndpointResponse(context);
        //}


        public override async Task ValidateClientRedirectUri(OAuthValidateClientRedirectUriContext context)
        {

            string clientId = string.Empty;
            string clientSecret = string.Empty;
          
 
            

            var service = ServiceLocator.Current.GetInstance<IAppClientAppService>();
            var client = await service.Find(context.ClientId);

            if (client == null)
            {
                context.SetError("invalid_clientId");
                return;
            }

            Uri clientUri;
            if (!Uri.TryCreate(client.RetrunUrl, UriKind.Absolute, out clientUri))
            {
                context.SetError("can't set a RedirectUri","数据库无url");
                return;
            }

            Uri uri;
            if (!Uri.TryCreate(context.RedirectUri, UriKind.Absolute, out uri))
            {
                context.SetError("invalid_RedirectUri");
                return;
            }

            if (uri.Host != clientUri.Host)
            {
                context.SetError("invalid_RedirectUri", "与数据库url不一致");
            }

            context.Validated(context.RedirectUri);
            return;
        }


        public override Task AuthorizeEndpoint(OAuthAuthorizeEndpointContext context)
        {
           
            return base.AuthorizeEndpoint(context);
        }

        public override  async Task MatchEndpoint(OAuthMatchEndpointContext context)
        {
            context.MatchesAuthorizeEndpoint();
            return;
           // return base.MatchEndpoint(context);
        }

       
    }
}