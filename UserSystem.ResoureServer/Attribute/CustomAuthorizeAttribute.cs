using System;
using System.Linq;
using System.Security.Claims;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace UserSystem.ResoureServer.Attribute
{

    [AttributeUsage(AttributeTargets.All, Inherited = false, AllowMultiple = false)]
    public class CustomAuthorizeAttribute : AuthorizeAttribute
    {
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            var pri = actionContext.RequestContext.Principal as ClaimsPrincipal;
            var list = pri.Claims.ToList();
            var user = Users;
            var roles = Roles;

            base.OnAuthorization(actionContext);
        }

    }
}
