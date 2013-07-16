using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimplyBrand.SocialMonitor.Business;
using SimplyBrand.SocialMonitor.Business.Utility;
using SimplyBrand.SocialMonitor.Business.JsonEntity;

namespace SimplyBrand.SocialMonitor.BusinessTest
{
    /// <summary>
    /// PermissionBLLTest 的摘要说明
    /// </summary>
    [TestClass]
    public class PermissionBLLTest
    {
        public PermissionBLLTest()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //
        }

        private TestContext testContextInstance;

        /// <summary>
        ///获取或设置测试上下文，该上下文提供
        ///有关当前测试运行及其功能的信息。
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }



        [TestMethod]
        public void GetPermissionTest()
        {
            PermissionBLL bll = new PermissionBLL();
            string json = bll.GetPermission();
            var result = JsonHelper.ParseJSON<ResponsePermissionListJson>(json);
            Assert.IsTrue(result.issucc);
            Assert.IsTrue(result.data.Count > 0);
        }
    }
}
