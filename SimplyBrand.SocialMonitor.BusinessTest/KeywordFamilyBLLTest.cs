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
    /// KeywordFamilyBLLTest 的摘要说明
    /// </summary>
    [TestClass]
    public class KeywordFamilyBLLTest
    {
        public KeywordFamilyBLLTest()
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
        public void GetKeywordFamilyTest()
        {
            KeywordFamilyBLL bll = new KeywordFamilyBLL();
            string json = bll.GetKeywordFamily(34);
            var result = JsonHelper.ParseJSON<ResponseKeywordFamilyPageJson>(json);
            Assert.IsTrue(result.issucc);
            Assert.IsTrue(result.data.count > 0);

        }

        [TestMethod]
        public void GetKeywordFamilyNoDataTest()
        {
            KeywordFamilyBLL bll = new KeywordFamilyBLL();
            string json = bll.GetKeywordFamily(0);
            var result = JsonHelper.ParseJSON<ResponseKeywordFamilyPageJson>(json);
            Assert.IsTrue(result.issucc);
            Assert.IsTrue(result.data.count == 0);

        }
    }
}
