using AutoMapper;
using System;
using System.Threading.Tasks;
using UserSystem.Application.DTO;
using UserSystem.Core.Repository;
using UserSystem.Core.Entity;
using Microsoft.AspNet.Identity;
using CommonServiceLocator;

namespace UserSystem.Application.UserService
{
    public class UserAppService : IUserAppService
    {
        private readonly IUserRepository _userRepository;
       
        public UserAppService(IUserRepository userRepository)
        {
        //    var re = ServiceLocator.Current.GetInstance<IUserRepository>();
            _userRepository = userRepository;
        //    var f = ReferenceEquals(re, _userRepository);
        }
        public async Task<string> Register(UserInput userInput)
        {
            var user = Mapper.Map<User>(userInput);
            return await _userRepository.Add(user);
        }

        public async Task<UserOutput> FindUser(string Id)
        {
            var user = await _userRepository.FindUser(Id);
            if (user == null)
            {
                throw new Exception("找不到User");
            }
            var userOutput = Mapper.Map<UserOutput>(user);
            return userOutput;
        }
    }
}
