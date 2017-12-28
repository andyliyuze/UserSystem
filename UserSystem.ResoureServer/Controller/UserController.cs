using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using UserSystem.ResoureServer.Attribute;

namespace UserSystem.ResoureServer.Controller
{
    [CustomAuthorize]
    public class UserController : ApiController
    {
        public string Get()
        {
            return "value";
        }
    }
}
