using SimplyBrand.SocialMonitor.Business;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using SimplyBrand.SocialMonitor.Business.Utility;
using SimplyBrand.SocialMonitor.Business.JsonEntity;

namespace SimplyBrand.SocialMonitor.BusinessTest
{
    
    
    /// <summary>
    ///这是 ContactUserBLLTest 的测试类，旨在
    ///包含所有 ContactUserBLLTest 单元测试
    ///</summary>
    [TestClass()]
    public class ContactUserBLLTest
    {


        private TestContext testContextInstance;

        /// <summary>
        ///获取或设置测试上下文，上下文提供
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

        #region 附加测试特性
        // 
        //编写测试时，还可使用以下特性:
        //
        //使用 ClassInitialize 在运行类中的第一个测试前先运行代码
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //使用 ClassCleanup 在运行完类中的所有测试后再运行代码
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //使用 TestInitialize 在运行每个测试前先运行代码
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //使用 TestCleanup 在运行完每个测试后运行代码
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        /// <summary>
        ///Find 的测试
        ///</summary>
        [TestMethod()]
        public void FindTest()
        {
            ContactUserBLL target = new ContactUserBLL(); // TODO: 初始化为适当的值    
            SysUserBLL bll = new SysUserBLL();
            string json = target.Find(17);
            ResponseContactUserJson result = JsonHelper.ParseJSON<ResponseContactUserJson>(json);
            Assert.IsTrue(result.issucc);
            Assert.IsTrue(result.item.Count > 0);
           
        }

        /// <summary>
        ///Find 的测试
        ///</summary>
        [TestMethod()]
        public void FindTestNoRegult()
        {
            ContactUserBLL target = new ContactUserBLL(); // TODO: 初始化为适当的值    
            SysUserBLL bll = new SysUserBLL();
            string json = target.Find(100);
            ResponseContactUserJson result = JsonHelper.ParseJSON<ResponseContactUserJson>(json);
            Assert.IsTrue(result.issucc);
            Assert.IsTrue(result.item.Count==0);

        }

        /// <summary>
        ///Update 的测试
        ///</summary>
        [TestMethod()]
        public void UpdateTestIsFail()
        {
            ContactUserBLL target = new ContactUserBLL(); // TODO: 初始化为适当的值
            int sysuserid = 0; // TODO: 初始化为适当的值
            int linkId = 0; // TODO: 初始化为适当的值
            string linkman = string.Empty; // TODO: 初始化为适当的值
            string linkTel = string.Empty; // TODO: 初始化为适当的值
            string linkEmail = string.Empty; // TODO: 初始化为适当的值
             int txtId = 0; // TODO: 初始化为适当的值
            string linkmans = string.Empty; // TODO: 初始化为适当的值
            string linkTels = string.Empty; // TODO: 初始化为适当的值
            string linkEmails = string.Empty; // TODO: 初始化为适当的值
            string expected = string.Empty; // TODO: 初始化为适当的值           
            string json = target.Update(sysuserid,linkId,linkman,linkTel,linkEmail,txtId,linkmans,linkTels,linkEmail);
            ResponseContactUserJson result = JsonHelper.ParseJSON<ResponseContactUserJson>(json);
            Assert.IsFalse(result.issucc);
            Assert.IsTrue(result.errormsg == "第一联系人资料不全,请填写");
        }
        /// <summary>
        ///Update 的测试
        ///</summary>
        [TestMethod()]
        public void UpdateTestIsTrue()
        {
            ContactUserBLL target = new ContactUserBLL(); // TODO: 初始化为适当的值
            int sysuserid = 0; // TODO: 初始化为适当的值
            int linkId = 17; // TODO: 初始化为适当的值
            string linkman ="lisi"; // TODO: 初始化为适当的值
            string linkTel ="1500000000"; // TODO: 初始化为适当的值
            string linkEmail ="2525252@aa.com"; // TODO: 初始化为适当的值
            int txtId = 0; // TODO: 初始化为适当的值
            string linkmans = string.Empty; // TODO: 初始化为适当的值
            string linkTels = string.Empty; // TODO: 初始化为适当的值
            string linkEmails = string.Empty; // TODO: 初始化为适当的值
            string expected = string.Empty; // TODO: 初始化为适当的值           
            string json = target.Update(sysuserid, linkId, linkman, linkTel, linkEmail, txtId, linkmans, linkTels, linkEmail);
            ResponseContactUserJson result = JsonHelper.ParseJSON<ResponseContactUserJson>(json);
            Assert.IsTrue(result.issucc);
            Assert.IsTrue(result.errormsg == "");
        }
        /// <summary>
        ///Update 的测试
        ///</summary>
        [TestMethod()]
        public void UpdateTestlinkTelIsFalse()
        {
            ContactUserBLL target = new ContactUserBLL(); // TODO: 初始化为适当的值
            int sysuserid = 0; // TODO: 初始化为适当的值
            int linkId = 17; // TODO: 初始化为适当的值
            string linkman = "lisi"; // TODO: 初始化为适当的值
            string linkTel = "150000000"; // TODO: 初始化为适当的值
            string linkEmail = "2525252@aa.com"; // TODO: 初始化为适当的值
            int txtId = 0; // TODO: 初始化为适当的值
            string linkmans = string.Empty; // TODO: 初始化为适当的值
            string linkTels = string.Empty; // TODO: 初始化为适当的值
            string linkEmails = string.Empty; // TODO: 初始化为适当的值
            string expected = string.Empty; // TODO: 初始化为适当的值           
            string json = target.Update(sysuserid, linkId, linkman, linkTel, linkEmail, txtId, linkmans, linkTels, linkEmail);
            ResponseContactUserJson result = JsonHelper.ParseJSON<ResponseContactUserJson>(json);
            Assert.IsFalse(result.issucc);
            Assert.IsTrue(result.errormsg == "第一联系人电话号码不正确");
        }
        /// <summary>
        ///Update 的测试
        ///</summary>
        [TestMethod()]
        public void UpdateTestlinkTelIsTrue()
        {
            ContactUserBLL target = new ContactUserBLL(); // TODO: 初始化为适当的值
            int sysuserid = 17; // TODO: 初始化为适当的值
            int linkId = 1; // TODO: 初始化为适当的值
            string linkman = "lisi"; // TODO: 初始化为适当的值
            string linkTel = "15000000000"; // TODO: 初始化为适当的值
            string linkEmail = "2525252@aa.com"; // TODO: 初始化为适当的值
            int txtId = 0; // TODO: 初始化为适当的值
            string linkmans = string.Empty; // TODO: 初始化为适当的值
            string linkTels = string.Empty; // TODO: 初始化为适当的值
            string linkEmails = string.Empty; // TODO: 初始化为适当的值
            string expected = string.Empty; // TODO: 初始化为适当的值           
            string json = target.Update(sysuserid, linkId, linkman, linkTel, linkEmail, txtId, linkmans, linkTels, linkEmail);
            ResponseContactUserJson result = JsonHelper.ParseJSON<ResponseContactUserJson>(json);
            Assert.IsTrue(result.issucc);
           // Assert.IsTrue(result.errormsg == "null");
        }

        /// <summary>
        ///Update 的测试
        ///</summary>
        [TestMethod()]
        public void UpdateTestlinkEmailIsFalse()
        {
            ContactUserBLL target = new ContactUserBLL(); // TODO: 初始化为适当的值
            int sysuserid = 17; // TODO: 初始化为适当的值
            int linkId = 1; // TODO: 初始化为适当的值
            string linkman = "lisi"; // TODO: 初始化为适当的值
            string linkTel = "15000000000"; // TODO: 初始化为适当的值
            string linkEmail = "2525252aa.com"; // TODO: 初始化为适当的值
            int txtId = 0; // TODO: 初始化为适当的值
            string linkmans = string.Empty; // TODO: 初始化为适当的值
            string linkTels = string.Empty; // TODO: 初始化为适当的值
            string linkEmails = string.Empty; // TODO: 初始化为适当的值
            string expected = string.Empty; // TODO: 初始化为适当的值           
            string json = target.Update(sysuserid, linkId, linkman, linkTel, linkEmail, txtId, linkmans, linkTels, linkEmail);
            ResponseContactUserJson result = JsonHelper.ParseJSON<ResponseContactUserJson>(json);
            Assert.IsFalse(result.issucc);
            Assert.IsTrue(result.errormsg == "第一联系人邮箱格式不正确");
        }


        /// <summary>
        ///Update 的测试
        ///</summary>
        [TestMethod()]
        public void UpdateTesttxtEmailIsFalse()
        {
            ContactUserBLL target = new ContactUserBLL(); // TODO: 初始化为适当的值
            int sysuserid = 17; // TODO: 初始化为适当的值
            int linkId = 1; // TODO: 初始化为适当的值
            string linkman = "lisi"; // TODO: 初始化为适当的值
            string linkTel = "15000000000"; // TODO: 初始化为适当的值
            string linkEmail = "2525252@aa.com"; // TODO: 初始化为适当的值
            int txtId = 2; // TODO: 初始化为适当的值
            string linkmans = "王武"; // TODO: 初始化为适当的值
            string linkTels = "15000000000"; // TODO: 初始化为适当的值
            string linkEmails = "2525252aa.com"; // TODO: 初始化为适当的值
            string expected = string.Empty; // TODO: 初始化为适当的值           
            string json = target.Update(sysuserid, linkId, linkman, linkTel, linkEmail, txtId, linkmans, linkTels, linkEmails);
            ResponseContactUserJson result = JsonHelper.ParseJSON<ResponseContactUserJson>(json);
            Assert.IsFalse(result.issucc);
            Assert.IsTrue(result.errormsg == "第二联系人邮箱格式不正确");
        }        

    }


}
