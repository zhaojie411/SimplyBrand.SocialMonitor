using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimplyBrand.SocialMonitor.Business;
using SimplyBrand.SocialMonitor.Business.Utility;
using SimplyBrand.SocialMonitor.Business.JsonEntity;
using SimplyBrand.SocialMonitor.DAL.Data;

namespace SimplyBrand.SocialMonitor.BusinessTest
{
    /// <summary>
    /// HotKeywordBLLTest 的摘要说明
    /// </summary>
    [TestClass]
    public class HotKeywordBLLTest
    {
        public HotKeywordBLLTest()
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
        public void GetHotKeywordTestWithNoData()
        {
            HotKeywordBLL bll = new HotKeywordBLL();
            string json = bll.GetHotKeyword(0, "");
            var result = JsonHelper.ParseJSON<ResponseKeywordFamilyPageJson>(json);
            Assert.IsFalse(result.issucc);
        }
        [TestMethod]
        public void GetHotKeywordTestWithKeywordFamilyIsNullTest()
        {
            HotKeywordBLL bll = new HotKeywordBLL();
            string json = bll.GetHotKeyword(34, string.Empty);
            var result = JsonHelper.ParseJSON<ResponseKeywordFamilyPageJson>(json);
            Assert.IsTrue(result.issucc);
        }

        [TestMethod]
        public void GetHotKeywordTestWithKeywordFamilyIsErrorTest()
        {
            HotKeywordBLL bll = new HotKeywordBLL();
            string json = bll.GetHotKeyword(34, "-1,-10");
            var result = JsonHelper.ParseJSON<ResponseKeywordFamilyPageJson>(json);
            Assert.IsTrue(result.data.items.Count == 0);
        }

        [TestMethod]
        public void GetHotKeywordTest()
        {
            HotKeywordBLL bll = new HotKeywordBLL();
            string json = bll.GetHotKeyword(34, string.Join(",", DataRepository.KeywordFamilyProvider.GetBySysUserId(34).Select(p => p.KeywordFamilyId).ToList()));
            var result = JsonHelper.ParseJSON<ResponseKeywordFamilyPageJson>(json);
            Assert.IsTrue(result.issucc);
        }
    }
}
