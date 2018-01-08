using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CommonServiceLocator;
using UserSystem.Application.AppClientService;
using UserSystem.Application.DTO;
using System.Threading.Tasks;

namespace Application.Test
{
    [TestClass]
    public class AppClientAppServiceTest : TestBase
    {
        private readonly IAppClientAppService _service;

        public AppClientAppServiceTest()
        {
            _service = ServiceLocator.Current.GetInstance<IAppClientAppService>();
        }

        [TestMethod]
        public async Task Test_Add()
        {
            AppClinetInput input = new AppClinetInput() { ClientType = "Owner", RetrunUrl = "http://aaa.test.cam" };
            var output = await _service.Add(input);
        }

        [TestMethod]
        public async Task Test_Find()
        {
            var output = await _service.Find("0ac30169-ec86-4cde-bbe4-cd3fd3680dd6");
        }
    }
}
