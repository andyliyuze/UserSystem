using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserSystem.Core.Entity;

namespace UserSystem.Core.Repository
{
   public interface IUserRepository
    {
        Task<string> Add(User user);

        Task<User> FindUser(string Id);
    }
}
