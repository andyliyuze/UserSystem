using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UserSystem.Infrastructure;

namespace Test.Infrastructure
{
    [TestClass]
    public class Hash256EncryptionTest
    {
        [TestMethod]
        public void Test_Create()
        {
            var encryption = new Base64UrlFactory();
            var result= encryption.Create("123456");
        }
    }
}
