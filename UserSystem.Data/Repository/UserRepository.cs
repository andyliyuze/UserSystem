using System.Threading.Tasks;
using UserSystem.Core.Entity;
using System.Data.Entity;
using System;
using System.Linq.Expressions;
using System.Security.Claims;
using UserSystem.Core.Repository;

namespace UserSystem.Data.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly CustomUserManager _userManager;
        public UserRepository(IUnitOfWork unitOfWork, CustomUserManager userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }

        public async Task<string> Register(User user , string password)
        {
            await _userManager.CreateAsync(user, password);
            return user.Id;
        }

        public async Task<ClaimsIdentity> CreateClaimsIdentity(User user, string authenticationType)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user不用为空");
            }
            return await _userManager.CreateIdentityAsync(user, authenticationType);
        }

        public async Task<User> FindUser(string Id)
        {
            var user = await _userManager.FindByIdAsync(Id);
            return user;
        }

        public async Task<User> FindUser(Expression<Func<User, bool>> condition)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(condition);
            return user;
        }

        public async Task<User> FindUser(string userName, string passWord)
        {
            var user = await _userManager.FindAsync(userName, passWord);          
            return user;
        }


    }
}
