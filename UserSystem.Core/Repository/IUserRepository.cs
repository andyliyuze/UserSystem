using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using UserSystem.Core.Entity;

namespace UserSystem.Core.Repository
{
   public interface IUserRepository
    {
        Task<string> Add(User user);

        Task<User> FindUser(string Id);

        Task<User> FindUser(Expression<Func<User, bool>> condition);

        Task<User> FindUser(string userName , string passWord);

        Task<ClaimsIdentity> CreateClaimsIdentity(User user, string authenticationType);
    }
}
