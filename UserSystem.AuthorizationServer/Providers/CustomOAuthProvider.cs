﻿using Microsoft.Owin.Security;
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

            var clientService = ServiceLocator.Current.GetInstance<IAppClientAppService>();
            var client =await clientService.Find(context.ClientId);
            if (client == null)
            {
                context.SetError("invalid_clientId");
                return;
            }

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

        public override Task TokenEndpoint(OAuthTokenEndpointContext context)
        {
            return base.TokenEndpoint(context);
        }



        public override Task TokenEndpointResponse(OAuthTokenEndpointResponseContext context)
        {
            context.Response.Cookies.Append("Cookies", "Bearer");
            return base.TokenEndpointResponse(context);
        }

        //public override Task GrantAuthorizationCode(OAuthGrantAuthorizationCodeContext context)
        //{
        //    return base.GrantAuthorizationCode(context);
        //}

        //public override Task GrantRefreshToken(OAuthGrantRefreshTokenContext context)
        //{
        //    return base.GrantRefreshToken(context);
        //}
    }
}