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
    public class AppClientService : IAppClientService
    {
        private readonly IAppClientRepository  _appClientRepository;
        private readonly IUnitOfWork _unitOfWork;

        public AppClientService(IAppClientRepository appClientRepository, IUnitOfWork unitOfWork)
        {
            _appClientRepository = appClientRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<AppClinetOutput> Add(AppClinetInput appClinetInput)
        {
            var  validUrlRegex = new Regex(@"^http(s) ?://([\w-]+\.)+[\w-]+(/[\w- ./?%&=]*)?$");

            if (validUrlRegex.IsMatch(appClinetInput.RetrunUrl.Trim()))
            {
                throw new Exception("无效的url");
            }
         
            AppClient appClient = new AppClient()
            {
                ClientSecret = Hash256Encryption.Create(),
                ClientType = appClinetInput.ClientType,
                RetrunUrl = appClinetInput.RetrunUrl
            };

            var client = await _appClientRepository.Add(appClient);
            return Mapper.Map<AppClinetOutput>(client);
        }
    }
}
