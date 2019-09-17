using Microsoft.VisualStudio.TestTools.UnitTesting;
using Nest.BaseCore.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace UnitTestProject1
{
    [TestClass]
    public class TestSecurity
    {
        [TestMethod]
        public void TestMd5()
        {
            var str = MD5Helper.GetMd5("123456");
        }

        [TestMethod]
        public void TestAES()
        {
            var str = "123456";
            str = AESHelper.AESEncrypt("123456");
            str = AESHelper.AESDecrypt(str);

            var key = GuidTool.GetGuid();
            str = AESHelper.AESEncrypt("123456", key);
            str = AESHelper.AESDecrypt(str, key);
        }
    }
}
