using MassTransit;
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
using UserSystem.ResoureServer.Model;

namespace UserSystem.ResoureServer.Controller
{
    [CustomAuthorize]
    public class UserController : BaseApiController
    {
        private readonly IUserAppService _userAppService;
        private readonly WebApiResponseHelper _apiHelper;
        private readonly IPublishEndpoint _publishEndpoint;
        public UserController(IUserAppService userAppService , IPublishEndpoint publishEndpoint)
        {
            _userAppService = userAppService;
            _apiHelper = new WebApiResponseHelper();
            _publishEndpoint = publishEndpoint;
        }

        [CustomAuthorize(Roles = "User")]
        public string Get()
        {      
            var user = UserInfo;
            return "value";
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<HttpResponseMessage> Register(HttpRequestMessage request, UserInput userInput)
        {
            return  await _apiHelper.CreateHttpResponse<string>(request, async () =>
           {
               var Id = await _userAppService.Register(userInput);
               if (!string.IsNullOrWhiteSpace(Id))
               {
                   var f = _publishEndpoint.Publish<UserRegisted>(new { Id = Id, UserName = userInput.UserName }).IsFaulted;
               }
               return Id;
           });
        }
    }
}
