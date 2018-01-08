using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserSystem.Core.Entity;
using UserSystem.Core.Repository;
namespace UserSystem.Data.Repository
{
   public class AppClientRepository: IAppClientRepository
    {
        private readonly UserSystemContext _userSystemContext;

        public AppClientRepository(UserSystemContext userSystemContext)
        {
            _userSystemContext = userSystemContext;
        }

        public AppClient Add(AppClient appClient)
        {
            return _userSystemContext.AppClient.Add(appClient);
        }

        public async Task<AppClient> Find(string Id)
        {
          return await _userSystemContext.AppClient
                .FirstOrDefaultAsync(a => a.ClientId == Id);
        }
    }
}
