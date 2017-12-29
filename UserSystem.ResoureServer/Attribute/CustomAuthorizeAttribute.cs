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
            if (actionContext.RequestContext.Principal != null)
            {
                //这里的Roles是指Action设置的Roles，而不是上下文获取到的Roles
                var f = actionContext.RequestContext.Principal.IsInRole(Roles);
            }
            var role = Roles;
            base.OnAuthorization(actionContext);
        }

    }
}
