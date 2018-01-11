using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using UserSystem.Application.DTO;
using UserSystem.Core.Entity;
using UserSystem.Core.Repository;
using UserSystem.Data;
using UserSystem.Infrastructure;

namespace UserSystem.Application.AppClientService
{
    public class AppClientAppService : IAppClientAppService
    {
        private readonly IAppClientRepository _appClientRepository;
        private readonly IUnitOfWork _unitOfWork;

        public AppClientAppService(IAppClientRepository appClientRepository, IUnitOfWork unitOfWork)
        {
            _appClientRepository = appClientRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<AppClinetOutput> Add(AppClinetInput appClinetInput)
        {
            if (!UrlChecker.CheckIsValid(appClinetInput.RetrunUrl,UriHostNameType.Dns))
            {
                return null;
            }
       
            AppClient appClient = new AppClient()
            {
                ClientSecret = Base64UrlFactory.Create(),
                ClientType = appClinetInput.ClientType,
                RetrunUrl = appClinetInput.RetrunUrl
            };

            var client = _appClientRepository.Add(appClient);
            await _unitOfWork.CommitAsync();
            return Mapper.Map<AppClinetOutput>(client);

        }

        public async Task<AppClinetOutput> Find(string Id)
        {
            var client = await _appClientRepository.Find(Id);
            return Mapper.Map<AppClinetOutput>(client);
        }




    }
}
