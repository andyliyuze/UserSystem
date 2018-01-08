using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserSystem.Core.Entity;

namespace UserSystem.Core.Repository
{
    public interface IAppClientRepository
    {
        AppClient Add(AppClient appClient);

        Task<AppClient> Find(string Id);
    }
}
