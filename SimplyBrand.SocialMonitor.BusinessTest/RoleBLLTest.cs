using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimplyBrand.SocialMonitor.Business;
using SimplyBrand.SocialMonitor.Business.JsonEntity;
using SimplyBrand.SocialMonitor.Business.Utility;
using SimplyBrand.SocialMonitor.DAL.Entities;

namespace SimplyBrand.SocialMonitor.BusinessTest
{
    [TestClass]
    public class RoleBLLTest
    {
        [TestMethod]
        public void GetRoleTest()
        {
            RoleBLL bll = new RoleBLL();
            string json = bll.GetRole();
            ResponseRoleListJson result = JsonHelper.ParseJSON<ResponseRoleListJson>(json);
            Assert.IsTrue(result.issucc);
            Assert.IsTrue(result.data.Count > 0);
        }
    }
}
