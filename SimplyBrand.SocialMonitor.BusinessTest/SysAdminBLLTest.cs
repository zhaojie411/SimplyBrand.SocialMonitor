using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimplyBrand.SocialMonitor.Business;
using SimplyBrand.SocialMonitor.Business.JsonEntity;
using SimplyBrand.SocialMonitor.Business.Utility;
using SimplyBrand.SocialMonitor.DAL.Data;
using SimplyBrand.SocialMonitor.DAL.Data.SqlClient;
using SimplyBrand.SocialMonitor.DAL.Data.Bases;
using SimplyBrand.SocialMonitor.DAL.Entities;
namespace SimplyBrand.SocialMonitor.BusinessTest
{
    [TestClass]
    public class SysAdminBLLTest
    {
        #region Login

        [TestMethod]
        public void LoginWithSysAdminNameIsNullTest()
        {
            SysAdminBLL bll = new SysAdminBLL();
            string json = bll.Login(null);
            ResponseSysAdminJson result = JsonHelper.ParseJSON<ResponseSysAdminJson>(json);
            Assert.IsFalse(result.issucc);
            Assert.IsTrue(result.errormsg == "用户名或密码不能为空");

        }
        [TestMethod]
        public void LoginWithSysAdminNameIsEmptyTest()
        {
            SysAdminBLL bll = new SysAdminBLL();
            string json = bll.Login("");
            ResponseSysAdminJson result = JsonHelper.ParseJSON<ResponseSysAdminJson>(json);
            Assert.IsFalse(result.issucc);
            Assert.IsTrue(result.errormsg == "用户名或密码不能为空");
        }
        [TestMethod]
        public void LoginWithSysAdminPwdIsNullTest()
        {
            SysAdminBLL bll = new SysAdminBLL();
            string json = bll.Login("admin");
            ResponseSysAdminJson result = JsonHelper.ParseJSON<ResponseSysAdminJson>(json);
            Assert.IsFalse(result.issucc);
            Assert.IsTrue(result.errormsg == "用户名或密码不能为空");
        }

        [TestMethod]
        public void LoginWithSysAdminPwdIsEmptyTest()
        {
            SysAdminBLL bll = new SysAdminBLL();
            string json = bll.Login("admin", "");
            ResponseSysAdminJson result = JsonHelper.ParseJSON<ResponseSysAdminJson>(json);
            Assert.IsFalse(result.issucc);
            Assert.IsTrue(result.errormsg == "用户名或密码不能为空");
        }
        [TestMethod]
        public void LoginWithSysAdminNameIsNotCorrectTest()
        {
            SysAdminBLL bll = new SysAdminBLL();
            string json = bll.Login("adminerror", "123456");
            ResponseSysAdminJson result = JsonHelper.ParseJSON<ResponseSysAdminJson>(json);
            Assert.IsFalse(result.issucc);
            Assert.IsTrue(result.errormsg == "用户名或密码不正确");
        }
        [TestMethod]
        public void LoginWithSysAdminPwdIsNotCorrectTest()
        {
            SysAdminBLL bll = new SysAdminBLL();
            string json = bll.Login("admin", "123456error");
            ResponseSysAdminJson result = JsonHelper.ParseJSON<ResponseSysAdminJson>(json);
            Assert.IsFalse(result.issucc);
            Assert.IsTrue(result.errormsg == "用户名或密码不正确");
        }
        [TestMethod]
        public void LoginSuccTest()
        {
            SysAdminBLL bll = new SysAdminBLL();
            string json = bll.Login("admin", "123456");
            ResponseSysAdminJson result = JsonHelper.ParseJSON<ResponseSysAdminJson>(json);
            Assert.IsTrue(result.issucc);
        }

        #endregion

        //[TestMethod]
        //public void Test()
        //{
        //    TList<DataCenter> tlist = new TList<DataCenter>();
        //    string[] strs = File.ReadAllLines("c:\\宜家.txt");
        //    for (int i = 0; i < strs.Length; i++)
        //    {
        //        try
        //        {

        //            string[] s = strs[i].Split('|');
        //            DataCenter datacenter = new DataCenter()
        //            {
        //                Uid = s[0],
        //                Dataauthor = s[1],
        //                Weiboid = long.Parse(s[2]),
        //                Datatime = DateTime.Parse(s[3]),
        //                Dataurl = s[4],
        //                Datatitle = s[5],
        //                Datacomment = int.Parse(s[6]),
        //                Dataforward = int.Parse(s[7])


        //            };
        //            tlist.Add(datacenter);
        //        }
        //        catch (Exception)
        //        {


        //        }
        //    }
        //    int count = DataRepository.DataCenterProvider.Insert(tlist);
        //}
    }
}
