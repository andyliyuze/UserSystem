﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserSystem.Application.DTO;
using UserSystem.Core.Entity;

namespace UserSystem.Application.AppClientService
{
    public interface IAppClientAppService
    {
        Task<AppClinetOutput> Add(AppClinetInput appClinetInput);

        Task<AppClinetOutput> Find(string Id);
    }
}
