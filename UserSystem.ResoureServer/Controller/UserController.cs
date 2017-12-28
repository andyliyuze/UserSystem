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
        private readonly WebApiResponseHelper _apiHelper;
        public UserController(IUserAppService userAppService)
        {
            _userAppService = userAppService;
            _apiHelper = new WebApiResponseHelper();
        }
        public string Get()
        {
            return "value";
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<HttpResponseMessage> Register(HttpRequestMessage request, UserInput userInput)
        {
            return await _apiHelper.CreateHttpResponse<string>(request, () =>
           {
               return _userAppService.Register(userInput);
           });
        }
    }
}
