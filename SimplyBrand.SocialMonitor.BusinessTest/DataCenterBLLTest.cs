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
            string file = "c:\\百安居.txt";
            string[] lines = File.ReadAllLines(file);
            int[] ints = new int[3] { -1, 0, 1 };
            Random random = new Random();
            foreach (string item in lines)
            {
                try
                {
                    string[] currentitems = item.Split('\t');
                    DataCenter dataCenter = new DataCenter()
                    {
                        Datasourceid = 1,
                        Uid = currentitems[0],
                        Dataauthor = currentitems[1],
                        Weiboid = long.Parse(currentitems[2]),
                        Datatime = DateTime.Parse(currentitems[3]),
                        Dataurl = currentitems[4],
                        Datatitle = currentitems[5],
                        Datacomment = int.Parse(currentitems[6]),
                        Dataforward = int.Parse(currentitems[7]),
                        Emotionalvalue = (short)ints[random.Next(4) - 1]
                    };
                    DataRepository.DataCenterProvider.Insert(dataCenter);
                }
                catch (Exception)
                {


                }

            }


        }
    }
}
