using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using UserSystem.Application.DTO;
using UserSystem.Application.UserService;
using UserSystem.Infrastructure;
using UserSystem.ResoureServer.Attribute;

namespace UserSystem.ResoureServer.Controller
{
    [CustomAuthorize]
    public class UserController : ApiController
    {
        private readonly IUserAppService _userAppService;
        public UserController(IUserAppService userAppService)
        {
            _userAppService = userAppService;
        }
        public string Get()
        {
            return "value";
        }

        [AllowAnonymous]
        public async Task<HttpResponseMessage> Register(HttpRequestMessage request, UserInput userInput)
        {

            return  WebApiResponseHelper.CreateHttpResponse<string>(request,
                () => 
                {
                    var Id = await _userAppService.Register(userInput);
                    return Id;
                });
        }
    }
}
