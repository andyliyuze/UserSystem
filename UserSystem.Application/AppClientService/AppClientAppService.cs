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
            var validUrlRegex = new Regex(@"((http|ftp|https)://)(([a-zA-Z0-9\._-]+\.[a-zA-Z]{2,6})|([0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}))(:[0-9]{1,4})*(/[a-zA-Z0-9\&%_\./-~-]*)?");

            if (!validUrlRegex.IsMatch(appClinetInput.RetrunUrl.Trim()))
            {
                throw new Exception("无效的url");
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
