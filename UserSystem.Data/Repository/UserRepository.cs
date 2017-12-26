using System.Threading.Tasks;
using UserSystem.Core.Entity;
using UserSystem.Core.Repository;
using System.Data.Entity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace UserSystem.Data.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly UserSystemContext  _dbContext;

        private readonly UserManager<IdentityUser> _userManager;
        public UserRepository(IUnitOfWork unitOfWork, UserManager<IdentityUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }

        public async Task<string> Add(User user)
        {
            await _userManager.CreateAsync(user);
            return user.Id;
        }

        public async Task<User> FindUser(string Id)
        {
            var user = await _userManager.FindByIdAsync(Id);
            return null;
        }

        
    }
}
