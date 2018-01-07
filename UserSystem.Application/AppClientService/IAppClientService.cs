using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserSystem.Application.DTO;

namespace UserSystem.Application.AppClientService
{
    public interface IAppClientService
    {
        Task<AppClinetOutput> Add(AppClinetInput appClinetInput);
    }
}
