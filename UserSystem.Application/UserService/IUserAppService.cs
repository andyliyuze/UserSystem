using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserSystem.Application.DTO;
namespace UserSystem.Application.UserService
{
   public interface IUserAppService
    {
        Task<string> Register(UserInput userInput);

        Task<UserOutput> FindUser(string Id);
    }
}
