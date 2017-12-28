using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using UserSystem.Application.DTO;
namespace UserSystem.Application.UserService
{
    public interface IUserAppService
    {
        Task<string> Register(UserInput userInput);

        Task<UserOutput> FindUser(string Id);

        /// <summary>
        /// 查找用户
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <param name="password">密码</param>
        /// <returns></returns>
        Task<UserOutput> FindUser(string userName, string password);

        /// <summary>
        /// 尝试登录并返回声明标识
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <param name="authenticationType">身份验证类型</param>
        /// <returns></returns>
        Task<ClaimsIdentity> TryLogin(string userName, string password, string authenticationType);
    }
}
