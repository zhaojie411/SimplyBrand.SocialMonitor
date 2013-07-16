using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimplyBrand.SocialMonitor.Business;
using SimplyBrand.SocialMonitor.Business.Utility;
using SimplyBrand.SocialMonitor.Business.JsonEntity;
using SimplyBrand.SocialMonitor.DAL.Data;
using SimplyBrand.SocialMonitor.DAL.Entities;
namespace SimplyBrand.SocialMonitor.BusinessTest
{
    /// <summary>
    /// SysUserBLLTest 的摘要说明
    /// </summary>
    [TestClass]
    public class SysUserBLLTest
    {
        public SysUserBLLTest()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //
        }


        #region CreateSysUser
        [TestMethod]
        public void CreateSysUserWithSysUserNameIsNullTest()
        {
            SysUserBLL bll = new SysUserBLL();
            string json = bll.CreateSysUser(null, "123456", 1, "1,2", DateTime.Now.AddDays(100).ToString());
            ResponseSysUserJson result = JsonHelper.ParseJSON<ResponseSysUserJson>(json);
            Assert.IsFalse(result.issucc);
            Assert.IsTrue(result.errormsg == "用户名或密码不能为空");
        }
        [TestMethod]
        public void CreateSysUserWithSysUserNameIsEmptyTest()
        {
            SysUserBLL bll = new SysUserBLL();
            string json = bll.CreateSysUser(string.Empty, "123456", 1, "1,2", DateTime.Now.AddDays(100).ToString());
            ResponseSysUserJson result = JsonHelper.ParseJSON<ResponseSysUserJson>(json);
            Assert.IsFalse(result.issucc);
            Assert.IsTrue(result.errormsg == "用户名或密码不能为空");
        }
        [TestMethod]
        public void CreateSysUserWithSysUserNameLenghtIsMoreThan50Test()
        {
            SysUserBLL bll = new SysUserBLL();
            string json = bll.CreateSysUser("UnitTest" + TestUtility.RandomString(50, false), "123456", 1, "1,2", DateTime.Now.AddDays(100).ToString());
            ResponseSysUserJson result = JsonHelper.ParseJSON<ResponseSysUserJson>(json);
            Assert.IsFalse(result.issucc);
            Assert.IsTrue(result.errormsg == "用户名长度不能超过50");
        }
        [TestMethod]
        public void CreateSysUserWithSysUserPwdIsNullTest()
        {
            SysUserBLL bll = new SysUserBLL();
            string json = bll.CreateSysUser("UnitTest" + TestUtility.RandomNumber(1000, 100000), null, 1, "1,2", DateTime.Now.AddDays(100).ToString());
            ResponseSysUserJson result = JsonHelper.ParseJSON<ResponseSysUserJson>(json);
            Assert.IsFalse(result.issucc);
            Assert.IsTrue(result.errormsg == "用户名或密码不能为空");
        }
        [TestMethod]
        public void CreateSysUserWithSysUserPwdIsEmptyTest()
        {
            SysUserBLL bll = new SysUserBLL();
            string json = bll.CreateSysUser("UnitTest" + TestUtility.RandomNumber(1000, 100000), string.Empty, 1, "1,2", DateTime.Now.AddDays(100).ToString());
            ResponseSysUserJson result = JsonHelper.ParseJSON<ResponseSysUserJson>(json);
            Assert.IsFalse(result.issucc);
            Assert.IsTrue(result.errormsg == "用户名或密码不能为空");
        }
        [TestMethod]
        public void CreateSysUserWithSysUserPwdLenghtIsMoreThan50Test()
        {
            SysUserBLL bll = new SysUserBLL();
            string json = bll.CreateSysUser("UnitTest" + TestUtility.RandomNumber(1000, 100000), "UnitTest" + TestUtility.RandomString(50, false), 1, "1,2", DateTime.Now.AddDays(100).ToString());
            ResponseSysUserJson result = JsonHelper.ParseJSON<ResponseSysUserJson>(json);
            Assert.IsFalse(result.issucc);
            Assert.IsTrue(result.errormsg == "密码长度不能超过50");
        }
        [TestMethod]
        public void CreateSysUserWithRoleIdFKErrorTest()
        {
            SysUserBLL bll = new SysUserBLL();
            string json = bll.CreateSysUser("UnitTest" + TestUtility.RandomNumber(1000, 100000), "123456", 0, "1,2", DateTime.Now.AddDays(100).ToString());
            ResponseSysUserJson result = JsonHelper.ParseJSON<ResponseSysUserJson>(json);
            Assert.IsFalse(result.issucc);
            Assert.IsTrue(result.errormsg == "内部错误");
        }
        [TestMethod]
        public void CreateSysUserWithPlatformIdIsNotCorrectTest()
        {
            SysUserBLL bll = new SysUserBLL();
            string json = bll.CreateSysUser("UnitTest" + TestUtility.RandomNumber(1000, 100000), "123456", 1, "errorid", DateTime.Now.AddDays(100).ToString());
            ResponseSysUserJson result = JsonHelper.ParseJSON<ResponseSysUserJson>(json);
            Assert.IsFalse(result.issucc);
            Assert.IsTrue(result.errormsg == "对应的参数不是int类型");
        }
        [TestMethod]
        public void CreateSysUserWithPlatformIdFKErrorTest()
        {
            SysUserBLL bll = new SysUserBLL();
            string json = bll.CreateSysUser("UnitTest" + TestUtility.RandomNumber(1000, 100000), "123456", 1, "99999", DateTime.Now.AddDays(100).ToString());
            ResponseSysUserJson result = JsonHelper.ParseJSON<ResponseSysUserJson>(json);
            Assert.IsFalse(result.issucc);
            Assert.IsTrue(result.errormsg == "内部错误");
        }
        [TestMethod]
        public void CreateSysUserTest()
        {
            SysUserBLL bll = new SysUserBLL();
            string json = bll.CreateSysUser("UnitTest" + TestUtility.RandomNumber(1000, 100000), "123456", 1, "1,2", DateTime.Now.AddDays(100).ToString());
            ResponseSysUserJson result = JsonHelper.ParseJSON<ResponseSysUserJson>(json);
            Assert.IsTrue(result.issucc);
        }

        [TestMethod]
        public void CreateSysUserWithEndTimeFormatErrorTest()
        {
            SysUserBLL bll = new SysUserBLL();
            string json = bll.CreateSysUser("UnitTest" + TestUtility.RandomNumber(1000, 100000), "123456", 1, "1,2", "xxxx");
            ResponseSysUserJson result = JsonHelper.ParseJSON<ResponseSysUserJson>(json);
            Assert.IsFalse(result.issucc);
            Assert.IsTrue(result.errormsg == ConstHelper.SYSUSERENDTIMEFORMAT_ERROR);
        }
        [TestMethod]
        public void CreateSysUserWithEndTimeLessThanNowTest()
        {
            SysUserBLL bll = new SysUserBLL();
            string json = bll.CreateSysUser("UnitTest" + TestUtility.RandomNumber(1000, 100000), "123456", 1, "1,2", DateTime.Now.AddDays(-1).ToString());
            ResponseSysUserJson result = JsonHelper.ParseJSON<ResponseSysUserJson>(json);
            Assert.IsFalse(result.issucc);
            Assert.IsTrue(result.errormsg == ConstHelper.SYSUSERENDTIMEFORMAT_ERROR);
        }
        #endregion

        #region CreateSysUserNew
        [TestMethod]
        public void CreateSysUserNew()
        {
            SysUserBLL bll = new SysUserBLL();
            string name = "UnitTest" + TestUtility.RandomNumber(1000, 100000);
            string json = bll.CreateSysUser(name, "123456", name, DateTime.Now.AddDays(10).ToString(), "1,2", "1,2,3,4");
            ResponseSysUserJson result = JsonHelper.ParseJSON<ResponseSysUserJson>(json);
            Assert.IsTrue(result.issucc);
            Assert.IsTrue(result.data != null);
        }

        #endregion
        #region StopOrEnableSysUser
        [TestMethod]
        public void StopOrEnableSysUserWithIdNotExistsTest()
        {
            SysUserBLL bll = new SysUserBLL();
            string json = bll.StopOrEnableSysUser(0);
            ResponseJson result = JsonHelper.ParseJSON<ResponseJson>(json);
            Assert.IsFalse(result.issucc);
        }

        [TestMethod]
        public void StopOrEnableSysUserTest()
        {
            string name = "UnitTest" + TestUtility.RandomNumber(1000, 100000);
            SysUserBLL bll = new SysUserBLL();
            string json = bll.CreateSysUser(name, "123456", 1, "1,2,3,4", DateTime.Now.AddDays(100).ToString());
            ResponseSysUserJson result = JsonHelper.ParseJSON<ResponseSysUserJson>(json);
            json = bll.StopOrEnableSysUser(result.data.id);
            ResponseJson rresult = JsonHelper.ParseJSON<ResponseJson>(json);
            Assert.IsTrue(result.issucc);
        }
        #endregion

        #region Delete

        [TestMethod]
        public void DeleteWithIdNotExistsTest()
        {
            SysUserBLL bll = new SysUserBLL();
            string json = bll.DeleteSysUser(0);
            ResponseJson result = JsonHelper.ParseJSON<ResponseJson>(json);
            Assert.IsFalse(result.issucc);
        }

        [TestMethod]
        public void DeleteTest()
        {
            string name = "UnitTest" + TestUtility.RandomNumber(1000, 100000);
            SysUserBLL bll = new SysUserBLL();
            string json = bll.CreateSysUser(name, "123456", 1, "1,2,3,4", DateTime.Now.AddDays(100).ToString());
            ResponseSysUserJson result = JsonHelper.ParseJSON<ResponseSysUserJson>(json);
            json = bll.DeleteSysUser(result.data.id);
            ResponseJson rresult = JsonHelper.ParseJSON<ResponseJson>(json);
            Assert.IsTrue(result.issucc);
        }

        #endregion



        #region UpdateSysUser
        [TestMethod]
        public void UpdateSysUserWithSysUserIdNotExistsTest()
        {
            SysUserBLL bll = new SysUserBLL();
            string json = bll.UpdateSysUser(0, "123456", 1, "1,2,3,4", DateTime.Now.AddDays(100).ToString());
            ResponseJson result = JsonHelper.ParseJSON<ResponseJson>(json);
            Assert.IsFalse(result.issucc);
        }
        [TestMethod]
        public void UpdateSysUserWithChangeRoleTest()
        {
            SysUserBLL bll = new SysUserBLL();
            string name = "UnitTest" + TestUtility.RandomNumber(1000, 100000);
            string json = bll.CreateSysUser(name, "123456", 1, "1,2", DateTime.Now.AddDays(100).ToString());
            ResponseSysUserJson responseSysUserJson = JsonHelper.ParseJSON<ResponseSysUserJson>(json);
            int sysUserId = responseSysUserJson.data.id;
            json = bll.UpdateSysUser(sysUserId, "123456", 2, "1,2", DateTime.Now.AddDays(100).ToString());
            TList<SysUserRole> tlist = DataRepository.SysUserRoleProvider.GetBySysUserId(sysUserId);
            Assert.IsTrue(tlist.Count == 1);
            Assert.IsTrue(tlist[0].RoleId == 2);

        }
        [TestMethod]
        public void UpdateSysUserWithChangePlatformTest()
        {
            SysUserBLL bll = new SysUserBLL();
            string name = "UnitTest" + TestUtility.RandomNumber(1000, 100000);
            string json = bll.CreateSysUser(name, "123456", 1, "1,2,3,4", DateTime.Now.AddDays(100).ToString());
            ResponseSysUserJson responseSysUserJson = JsonHelper.ParseJSON<ResponseSysUserJson>(json);
            int sysUserId = responseSysUserJson.data.id;
            json = bll.UpdateSysUser(sysUserId, "123456", 2, "1,2", DateTime.Now.AddDays(100).ToString());
            TList<SysUserRole> tlist = DataRepository.SysUserRoleProvider.GetBySysUserId(sysUserId);
            List<int> platformIdList = DataRepository.SysUserPlatformProvider.GetBySysUserId(sysUserId).Select(p => p.PlatformId).ToList().ConvertAll(p => int.Parse(p.ToString()));
            Assert.IsTrue(tlist.Count == 1);
            Assert.IsTrue(tlist[0].RoleId == 2);
            Assert.IsTrue(platformIdList.Count == 2);
            Assert.IsTrue(platformIdList.Contains(1));
            Assert.IsTrue(platformIdList.Contains(2));


        }

        [TestMethod]
        public void UpdateSysUserWithChangePlatformAddTest()
        {
            SysUserBLL bll = new SysUserBLL();
            string name = "UnitTest" + TestUtility.RandomNumber(1000, 100000);
            string json = bll.CreateSysUser(name, "123456", 1, "1", DateTime.Now.AddDays(100).ToString());
            ResponseSysUserJson responseSysUserJson = JsonHelper.ParseJSON<ResponseSysUserJson>(json);
            int sysUserId = responseSysUserJson.data.id;
            json = bll.UpdateSysUser(sysUserId, "123456", 2, "1,2,3", DateTime.Now.AddDays(100).ToString());
            TList<SysUserRole> tlist = DataRepository.SysUserRoleProvider.GetBySysUserId(sysUserId);
            List<int> platformIdList = DataRepository.SysUserPlatformProvider.GetBySysUserId(sysUserId).Select(p => p.PlatformId).ToList().ConvertAll(p => int.Parse(p.ToString()));
            Assert.IsTrue(tlist.Count == 1);
            Assert.IsTrue(tlist[0].RoleId == 2);
            Assert.IsTrue(platformIdList.Count == 3);
            Assert.IsTrue(platformIdList.Contains(1));
            Assert.IsTrue(platformIdList.Contains(2));
            Assert.IsTrue(platformIdList.Contains(3));

        }
        [TestMethod]
        public void UpdateSysUserWithChangePlatformAddAndDeleteTest()
        {
            SysUserBLL bll = new SysUserBLL();
            string name = "UnitTest" + TestUtility.RandomNumber(1000, 100000);
            string json = bll.CreateSysUser(name, "123456", 1, "1,2", DateTime.Now.AddDays(100).ToString());
            ResponseSysUserJson responseSysUserJson = JsonHelper.ParseJSON<ResponseSysUserJson>(json);
            int sysUserId = responseSysUserJson.data.id;
            json = bll.UpdateSysUser(sysUserId, "123456", 2, "2,3,4", DateTime.Now.AddDays(100).ToString());
            TList<SysUserRole> tlist = DataRepository.SysUserRoleProvider.GetBySysUserId(sysUserId);
            List<int> platformIdList = DataRepository.SysUserPlatformProvider.GetBySysUserId(sysUserId).Select(p => p.PlatformId).ToList().ConvertAll(p => int.Parse(p.ToString()));
            Assert.IsTrue(tlist.Count == 1);
            Assert.IsTrue(tlist[0].RoleId == 2);
            Assert.IsTrue(platformIdList.Count == 3);
            Assert.IsTrue(platformIdList.Contains(2));
            Assert.IsTrue(platformIdList.Contains(3));
            Assert.IsTrue(platformIdList.Contains(4));
        }
        [TestMethod]
        public void UpdateSysUserWithNullPasswordTest()
        {
            SysUserBLL bll = new SysUserBLL();
            string name = "UnitTest" + TestUtility.RandomNumber(1000, 100000);
            string json = bll.CreateSysUser(name, "123456", 1, "1,2", DateTime.Now.AddDays(100).ToString());
            ResponseSysUserJson responseSysUserJson = JsonHelper.ParseJSON<ResponseSysUserJson>(json);
            int sysUserId = responseSysUserJson.data.id;
            json = bll.UpdateSysUser(sysUserId, null, 2, "2,3,4", DateTime.Now.AddDays(100).ToString());
            ResponseJson result = JsonHelper.ParseJSON<ResponseJson>(json);
            Assert.IsTrue(result.issucc);
            Assert.IsTrue(DataRepository.SysUserProvider.GetBySysUserId(responseSysUserJson.data.id).SysUserPwd == EncryHelper.MD5Encrypt("123456"));
        }
        [TestMethod]
        public void UpdateSysUserWithEmptyPasswordTest()
        {
            SysUserBLL bll = new SysUserBLL();
            string name = "UnitTest" + TestUtility.RandomNumber(1000, 100000);
            string json = bll.CreateSysUser(name, "123456", 1, "1,2", DateTime.Now.AddDays(100).ToString());
            ResponseSysUserJson responseSysUserJson = JsonHelper.ParseJSON<ResponseSysUserJson>(json);
            int sysUserId = responseSysUserJson.data.id;
            json = bll.UpdateSysUser(sysUserId, string.Empty, 2, "2,3,4", DateTime.Now.AddDays(100).ToString());
            ResponseJson result = JsonHelper.ParseJSON<ResponseJson>(json);
            Assert.IsTrue(result.issucc);
            Assert.IsTrue(DataRepository.SysUserProvider.GetBySysUserId(responseSysUserJson.data.id).SysUserPwd == EncryHelper.MD5Encrypt("123456"));
        }
        [TestMethod]
        public void UpdateSysUserWithEndTimeFormatErrorTest()
        {
            SysUserBLL bll = new SysUserBLL();
            string name = "UnitTest" + TestUtility.RandomNumber(1000, 100000);
            string json = bll.CreateSysUser(name, "123456", 1, "1,2", DateTime.Now.AddDays(100).ToString());
            ResponseSysUserJson responseSysUserJson = JsonHelper.ParseJSON<ResponseSysUserJson>(json);
            int sysUserId = responseSysUserJson.data.id;
            json = bll.UpdateSysUser(sysUserId, string.Empty, 2, "2,3,4", "xxxxx");
            ResponseJson result = JsonHelper.ParseJSON<ResponseJson>(json);

            Assert.IsFalse(result.issucc);
            Assert.IsTrue(result.errormsg == ConstHelper.SYSUSERENDTIMEFORMAT_ERROR);
        }
        [TestMethod]
        public void UpdateSysUserWithEndTimeLessThanNowTest()
        {
            SysUserBLL bll = new SysUserBLL();
            string name = "UnitTest" + TestUtility.RandomNumber(1000, 100000);
            string json = bll.CreateSysUser(name, "123456", 1, "1,2", DateTime.Now.AddDays(100).ToString());
            ResponseSysUserJson responseSysUserJson = JsonHelper.ParseJSON<ResponseSysUserJson>(json);
            int sysUserId = responseSysUserJson.data.id;
            json = bll.UpdateSysUser(sysUserId, string.Empty, 2, "2,3,4", DateTime.Now.AddDays(-1).ToString());
            ResponseJson result = JsonHelper.ParseJSON<ResponseJson>(json);

            Assert.IsFalse(result.issucc);
            Assert.IsTrue(result.errormsg == ConstHelper.SYSUSERENDTIMEFORMAT_ERROR);
        }

        #endregion

        #region UpdateSysUserNew
        [TestMethod]
        public void UpdateSysUserNew()
        {
            SysUserBLL bll = new SysUserBLL();
            string name = "UnitTest" + TestUtility.RandomNumber(1000, 100000);
            string json = bll.CreateSysUser(name, "123456", name, DateTime.Now.AddDays(10).ToString(), "1", "1,2,3,4");
            ResponseSysUserJson resultSysUser = JsonHelper.ParseJSON<ResponseSysUserJson>(json);

            json = bll.UpdateSysUser(resultSysUser.data.id, "222222", "UnitTest" + TestUtility.RandomNumber(1000, 100000), "2", "1,2,3", DateTime.Now.AddDays(20).ToString());
            ResponseJson response = JsonHelper.ParseJSON<ResponseJson>(json);
            Assert.IsTrue(response.issucc);
        }
        #endregion

        #region Login
        [TestMethod]
        public void LoginWithSysUserNameIsNullTest()
        {
            SysUserBLL bll = new SysUserBLL();
            string json = bll.Login(null);
            ResponseSysUserJson result = JsonHelper.ParseJSON<ResponseSysUserJson>(json);
            Assert.IsFalse(result.issucc);
            Assert.IsTrue(result.errormsg == "用户名或密码不能为空");

        }
        [TestMethod]
        public void LoginWithSysUserNameIsEmptyTest()
        {
            SysUserBLL bll = new SysUserBLL();
            string json = bll.Login("");
            ResponseSysUserJson result = JsonHelper.ParseJSON<ResponseSysUserJson>(json);
            Assert.IsFalse(result.issucc);
            Assert.IsTrue(result.errormsg == "用户名或密码不能为空");
        }
        [TestMethod]
        public void LoginWithSysUserPwdIsNullTest()
        {
            SysUserBLL bll = new SysUserBLL();
            string json = bll.Login("admin");
            ResponseSysUserJson result = JsonHelper.ParseJSON<ResponseSysUserJson>(json);
            Assert.IsFalse(result.issucc);
            Assert.IsTrue(result.errormsg == "用户名或密码不能为空");
        }

        [TestMethod]
        public void LoginWithSysUserPwdIsEmptyTest()
        {
            SysUserBLL bll = new SysUserBLL();
            string json = bll.Login("admin", "");
            ResponseSysUserJson result = JsonHelper.ParseJSON<ResponseSysUserJson>(json);
            Assert.IsFalse(result.issucc);
            Assert.IsTrue(result.errormsg == "用户名或密码不能为空");
        }
        [TestMethod]
        public void LoginWithSysUserNameIsNotCorrectTest()
        {
            SysUserBLL bll = new SysUserBLL();
            string json = bll.Login("adminerror", "123456");
            ResponseSysUserJson result = JsonHelper.ParseJSON<ResponseSysUserJson>(json);
            Assert.IsFalse(result.issucc);
            Assert.IsTrue(result.errormsg == "用户名或密码不正确");
        }
        [TestMethod]
        public void LoginWithSysUserPwdIsNotCorrectTest()
        {
            SysUserBLL bll = new SysUserBLL();
            string json = bll.Login("admin", "123456error");
            ResponseSysUserJson result = JsonHelper.ParseJSON<ResponseSysUserJson>(json);
            Assert.IsFalse(result.issucc);
            Assert.IsTrue(result.errormsg == "用户名或密码不正确");
        }
        [TestMethod]
        public void LoginSuccTest()
        {
            SysUserBLL bll = new SysUserBLL();
            string json = bll.Login("admin", "123456");
            ResponseSysUserJson result = JsonHelper.ParseJSON<ResponseSysUserJson>(json);
            Assert.IsTrue(result.issucc);
        }
        [TestMethod]
        public void LoginWithSysUserIsStopTest()
        {
            string name = "UnitTest" + TestUtility.RandomNumber(1000, 100000);
            SysUserBLL bll = new SysUserBLL();
            string json = bll.CreateSysUser(name, "123456", 1, "1,2,3,4", DateTime.Now.AddDays(100).ToString());
            ResponseSysUserJson result = JsonHelper.ParseJSON<ResponseSysUserJson>(json);
            json = bll.StopOrEnableSysUser(result.data.id);
            json = bll.Login(name, "123456");
            result = JsonHelper.ParseJSON<ResponseSysUserJson>(json);
            Assert.IsFalse(result.issucc);
            Assert.IsTrue(result.errormsg == ConstHelper.SYSUSERSTOP_INFO);
        }
        [TestMethod]
        public void LoginWithSysUserIsDeleteTest()
        {
            string name = "UnitTest" + TestUtility.RandomNumber(1000, 100000);
            SysUserBLL bll = new SysUserBLL();
            string json = bll.CreateSysUser(name, "123456", 1, "1,2,3,4", DateTime.Now.AddDays(100).ToString());
            ResponseSysUserJson result = JsonHelper.ParseJSON<ResponseSysUserJson>(json);
            json = bll.DeleteSysUser(result.data.id);
            json = bll.Login(name, "123456");
            result = JsonHelper.ParseJSON<ResponseSysUserJson>(json);
            Assert.IsFalse(result.issucc);
            Assert.IsTrue(result.errormsg == ConstHelper.SYSUSERDELETE_INFO);

        }
        #endregion



    }
}
