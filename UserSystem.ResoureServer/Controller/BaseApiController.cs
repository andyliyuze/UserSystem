using System.Linq;
using System.Security.Claims;
using System.Web.Http;
using Newtonsoft.Json;
using UserSystem.Application.DTO;

namespace UserSystem.ResoureServer.Controller
{

    public class BaseApiController : ApiController
    { 
        public BaseApiController()
        {           
            var identity = User.Identity as ClaimsIdentity;
        }

        /// <summary>
        /// 已验证的用户信息
        /// </summary>
        protected virtual UserOutput UserInfo
        {
            get
            {
                var principal = RequestContext.Principal as ClaimsPrincipal;
                var userData = principal.Claims.Where(a => a.Type == ClaimTypes.UserData).FirstOrDefault();
                if (userData != null)
                {
                    try
                    {
                        return JsonConvert.DeserializeObject<UserOutput>(userData.Value);
                    }
                    catch { return null; }
                }
                return null;
            }
        }

    }

    
}
