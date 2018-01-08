using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CommonServiceLocator;
using UserSystem.Application.AppClientService;
using UserSystem.Application.DTO;

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
        public void TestMethod1()
        {
            AppClinetInput input = new AppClinetInput() { ClientType = "Owner", RetrunUrl = "http://aaa.test.cam" };
            _service.Add(input);
        }
    }
}
