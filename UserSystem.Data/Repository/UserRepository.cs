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

        private readonly IDbContext _dbContext;

        private readonly UserManager<User> _userManager;
        public UserRepository(IUnitOfWork unitOfWork, IDbContext dbContext )
        {
            _unitOfWork = unitOfWork;
            _dbContext = dbContext;
          
        }
       
        public async Task<string> Add(User user)
        {
            _dbContext.Users.Add(user);
            await _unitOfWork.CommitAsync();
            return user.Id;
        }

        public async Task<User> FindUser(string Id)
        {
            return await Task.Run<User>(() =>
             {
                 return _dbContext.Users.Find(Id);
             });
        }
    }
}
