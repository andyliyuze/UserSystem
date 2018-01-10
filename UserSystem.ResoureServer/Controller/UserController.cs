using AutoMapper;
using MassTransit;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using UserSystem.Application.DTO;
using UserSystem.Application.UserService;
using UserSystem.Infrastructure;
using UserSystem.MessageContract;
using UserSystem.ResoureServer.Attribute;

namespace UserSystem.ResoureServer.Controller
{
    [RoutePrefix("api/user")]
    [CustomAuthorize]
    public class UserController : BaseApiController
    {
        private readonly IUserAppService _userAppService;
        private readonly WebApiResponseHelper _apiHelper;
        private readonly IPublishEndpoint _publishEndpoint;
       
        public UserController(IUserAppService userAppService,
            IPublishEndpoint publishEndpoint)
        {
            _userAppService = userAppService;
            _apiHelper = new WebApiResponseHelper();
            _publishEndpoint = publishEndpoint;
        }

       // [CustomAuthorize(Roles = "User")]
      
        [Route("get")]
        public string Get()
        {      
            return "value";
        }

        /// <summary>
        /// 用户注册
        /// </summary>
        /// <param name="request"></param>
        /// <param name="userInput"></param>
        /// <returns></returns>
        [HttpPost]
      
        [AllowAnonymous]
        public async Task<HttpResponseMessage> Register(HttpRequestMessage request, UserInput userInput)
        {
            return await _apiHelper.CreateHttpResponse<string>(request, async () =>
          {
              var Id = await _userAppService.Register(userInput);
              if (!string.IsNullOrWhiteSpace(Id))
              {
                  await _publishEndpoint.Publish<UserRegisted>(new { Id = Id, UserName = userInput.UserName });
              }
              return Id;
          });
        }

        /// <summary>
        /// 修改用户信息
        /// </summary>
        /// <param name="request"></param>
        /// <param name="updateUserInfoInput"></param>
        /// <returns></returns>

        [Route("updateUser")]
        [HttpPost]
        public async Task<HttpResponseMessage> UpdateUser(HttpRequestMessage request,UpdateUserInfoInput updateUserInfoInput)
        {
            return await _apiHelper.CreateHttpResponse<string>(request, async () =>
             {
                 var Id = await _userAppService.UpdateUserInfo(UserInfo.Id, updateUserInfoInput);
                 if (!string.IsNullOrWhiteSpace(Id))
                 {
                     var userInfoUpdated = Mapper.Map<UserInfoUpdated>(updateUserInfoInput);
                     userInfoUpdated.Id = UserInfo.Id;
                     await _publishEndpoint.Publish<UserInfoUpdated>(userInfoUpdated);
                 }
                 return Id;
             });
        }

    }
}
