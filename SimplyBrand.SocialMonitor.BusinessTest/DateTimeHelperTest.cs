using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimplyBrand.SocialMonitor.Business.Utility;

namespace SimplyBrand.SocialMonitor.BusinessTest
{
    /// <summary>
    /// DateTimeHelperTest 的摘要说明
    /// </summary>
    [TestClass]
    public class DateTimeHelperTest
    {
        public DateTimeHelperTest()
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
        public void GetFirstDayOfMonthTest()
        {
            for (int i = 1; i < 12; i++)
            {
                DateTime dt = DateTimeHelper.GetFirstDayOfMonth(2013, i);
            }

        }

        [TestMethod]
        public void GetLastDayOfMonthTest()
        {
            for (int i = 1; i < 12; i++)
            {
                DateTime dt = DateTimeHelper.GetLastDayOfMonth(2013, i);
            }

        }
    }
}
