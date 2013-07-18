using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimplyBrand.SocialMonitor.Business;
using System.IO;
using SimplyBrand.SocialMonitor.DAL.Entities;
using SimplyBrand.SocialMonitor.DAL.Data;
namespace SimplyBrand.SocialMonitor.BusinessTest
{
    /// <summary>
    /// DataCenterBLLTest 的摘要说明
    /// </summary>
    [TestClass]
    public class DataCenterBLLTest
    {
        public DataCenterBLLTest()
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
        public void GetSummaryDataTest()
        {
            DataCenterBLL bll = new DataCenterBLL();
            string json = bll.GetSummaryData(1, "8", "1,2,3,4", "0,-1,1", true);
            json = bll.GetSummaryData(1, "8", "1,2,3,4", "0,-1,1", false);
        }


        [TestMethod]
        public void GetEmotionalDataJsonTest()
        {
            DataCenterBLL bll = new DataCenterBLL();
            string json = bll.GetEmotionalData(1, "8", "1,2,3,4", true);
            json = bll.GetEmotionalData(1, "8", "1,2,3,4", false);
        }


        [TestMethod]
        public void InsertData()
        {
            string file = "c:\\摩恩 铅.txt";
            string[] lines = File.ReadAllLines(file);
            int[] ints = new int[3] { -1, 0, 1 };
            Random random = new Random();
            foreach (string item in lines)
            {
                try
                {
                    string[] currentitems = item.Split('\t');
                    DataCenter dataCenter = new DataCenter();
                    int comment = 0;
                    int forward = 0;
                    int.TryParse(currentitems[6], out comment);
                    int.TryParse(currentitems[7], out forward);
                    dataCenter.Datasourceid = 1;
                    dataCenter.Uid = currentitems[0];
                    dataCenter.Dataauthor = currentitems[1];
                    //Weiboid = long.Parse(currentitems[2]),
                    dataCenter.Weiboid = 0;
                    dataCenter.Datatime = DateTime.Parse(currentitems[3]);
                    dataCenter.Dataurl = currentitems[4];
                    dataCenter.Datatitle = currentitems[5];
                    dataCenter.Datacomment = comment;
                    dataCenter.Dataforward = forward;
                    dataCenter.Emotionalvalue = 0;

                    DataRepository.DataCenterProvider.Insert(dataCenter);
                }
                catch (Exception ex)
                {


                }

            }


        }
    }
}
