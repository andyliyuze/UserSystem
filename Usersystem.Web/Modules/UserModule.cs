using Nancy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Usersystem.Web.Modules
{
    public class UserModule: NancyModule
    {
        public UserModule()
        {
            //主页
            Get["/"] = r =>
            {
                return View["Login"];
            };
        }
    }
}
