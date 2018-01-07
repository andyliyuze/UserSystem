using System;
using System.Collections.Generic;
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

        public async Task<AppClient>  Add(AppClient appClient)
        {
            return await Task.Run<AppClient>(() => _userSystemContext.AppClient.Add(appClient));
        }
    }
}
