using Autofac;
using CommonServiceLocator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using UserSystem.Application.UserService;
using UserSystem.Data;

namespace UserSystem.AuthorizationServer
{
    [RoutePrefix("api/auth")]
   public class AuthController : ApiController
    {

        private readonly IUserAppService _userAppService;
        private readonly IUserAppService _userAppService2;

        public AuthController(IUserAppService userAppService, IUserAppService userAppService2)
        {
            _userAppService = userAppService;
            _userAppService2 = userAppService2;
        }

        [Route("get")]
        [HttpGet]
        public string Get()
        {
         
           var dbcontext = ServiceLocator.Current.GetInstance<UserSystemContext>();

           

            //var db2 = ServiceLocator.Current.GetInstance<UserSystemContext>();

            //var service = ServiceLocator.Current.GetInstance<IUserAppService>();

            //var service2 = ServiceLocator.Current.GetInstance<IUserAppService>();

            _userAppService.FindUser("adaw");
            //var go = ReferenceEquals(db2, dbcontext);
            //var dot = ReferenceEquals(_userAppService, service);
            //var dod = ReferenceEquals(service2, service);
            var doc = ReferenceEquals(_userAppService, _userAppService2);
            return "Hello, world!";
        }
        public   T GetPrivateField<T>(object instance, string fieldname)
        {
            BindingFlags flag = BindingFlags.Instance | BindingFlags.NonPublic;
            Type type = instance.GetType();
            FieldInfo field = type.GetField(fieldname, flag);
            return (T)field.GetValue(instance);
        }

    }
}
