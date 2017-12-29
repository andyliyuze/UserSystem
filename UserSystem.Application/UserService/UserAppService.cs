using AutoMapper;
using System;
using System.Threading.Tasks;
using UserSystem.Application.DTO;
using UserSystem.Core.Repository;
using UserSystem.Core.Entity;
using Microsoft.AspNet.Identity;
using CommonServiceLocator;
using System.Security.Claims;
using Newtonsoft.Json;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

namespace UserSystem.Application.UserService
{
    public class UserAppService : IUserAppService
    {
        private readonly IUserRepository _userRepository;
       
        public UserAppService(IUserRepository userRepository)
        {      
            _userRepository = userRepository;
        }

        public UserAppService()
        {
        }

        public async Task<string> Register(UserInput userInput)
        {
            var user = Mapper.Map<User>(userInput);
            return await _userRepository.Register(user, userInput.Password);
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

        public async Task<UserOutput> FindUser(string userName, string password)
        {
            if (string.IsNullOrWhiteSpace(userName) || string.IsNullOrWhiteSpace(password))
            {
                throw new ArgumentNullException("用户名或密码不可为空");
            }
            var user = await _userRepository.FindUser(userName, password);          
            var userOutput = Mapper.Map<UserOutput>(user);
            return userOutput;
        }

        public async Task<ClaimsIdentity> TryLogin(string userName, string password, string authenticationType)
        {
            if (string.IsNullOrWhiteSpace(userName) || string.IsNullOrWhiteSpace(password))
            {
                throw new ArgumentNullException("用户名或密码不可为空");
            }

            var user = await _userRepository.FindUser(userName, password);
            var userInfo = Mapper.Map<UserOutput>(user);

            if (user == null) { return null; }
            
            var identity = await _userRepository.CreateClaimsIdentity(user, authenticationType);

            var identitysCliams = user.Claims.Where(a => a.ClaimType == ClaimTypes.Role).ToList();
            var claims = Mapper.Map<IEnumerable<Claim>>(identitysCliams);
                       
            identity.AddClaim(new Claim(ClaimTypes.Role, JsonConvert.SerializeObject(userInfo)));
            
            //identity.AddClaims(claims);

            return identity;
        }
    }
}
