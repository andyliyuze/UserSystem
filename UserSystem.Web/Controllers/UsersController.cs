using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using UserSystem.Web.Models;
using System.Net.Http;
namespace UserSystem.Web.Controllers
{
    public class UsersController : BaseController
    {
        private readonly string _user_login_url = "http://localhost:40048/oauth2/token";
        // GET: Users
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Login(LoginModel loginModel)
        { 
            var result = await HttpClientHepler.PostAysnc(_user_login_url, loginModel);
           
            if (result.IsSuccessStatusCode == false || result.StatusCode == HttpStatusCode.Unauthorized)
            {
                ViewBag.Message = "密码错误";
                return  View("Login");
            }

            var loginResponse = await result.Content.ReadAsAsync<LoginResponse>();
            var cookie = new HttpCookie("token", loginResponse.access_token)
            {
                Expires = DateTime.Now.AddSeconds(Convert.ToDouble(loginResponse.expires_in)),
                Domain = ConfigurationManager.AppSettings["domain"].ToString()
            };
            Response.SetCookie(cookie);


            return Redirect(Request.Form["returnUrl"].ToString());

        }

    }
}