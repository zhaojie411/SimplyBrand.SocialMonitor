using SimplyBrand.SocialMonitor.Business.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace SimplyBrand.SocialMonitor.BusinessTest
{
    
    
    /// <summary>
    ///这是 CommonValidatorTest 的测试类，旨在
    ///包含所有 CommonValidatorTest 单元测试
    ///</summary>
    [TestClass()]
    public class CommonValidatorTest
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
        ///ValidatePhone 的测试
        ///</summary>
        [TestMethod()]
        public void ValidatePhoneIsFasleTest()
        {
            CommonValidator target = new CommonValidator(); // TODO: 初始化为适当的值
            string Phone = "32134632155"; // TODO: 初始化为适当的值            
            bool actual;
            actual = target.ValidatePhone(Phone);
            Assert.IsFalse(actual);
          
        }
        /// <summary>
        ///ValidatePhone 的测试
        ///</summary>
        [TestMethod()]
        public void ValidatePhoneIsTrueTest()
        {
            CommonValidator target = new CommonValidator(); // TODO: 初始化为适当的值
            string Phone = "15000000020"; // TODO: 初始化为适当的值            
            bool actual;
            actual = target.ValidatePhone(Phone);
            Assert.IsTrue(actual);

        }
        /// <summary>
        ///ValidatePhone 的测试
        ///</summary>
        [TestMethod()]
        public void ValidatePhoneIsNUllTest()
        {
            CommonValidator target = new CommonValidator(); // TODO: 初始化为适当的值
            string Phone = string.Empty; // TODO: 初始化为适当的值            
            bool actual;
            actual = target.ValidatePhone(Phone);
            Assert.IsFalse(actual);

        }
        /// <summary>
        ///ValidatePhone 的测试
        ///</summary>
        [TestMethod()]
        public void ValidatePhoneIsfaTest()
        {
            CommonValidator target = new CommonValidator(); // TODO: 初始化为适当的值
            string Phone = "15000000a20"; // TODO: 初始化为适当的值            
            bool actual;
            actual = target.ValidatePhone(Phone);
            Assert.IsFalse(actual);

        }

        /// <summary>
        ///ValidateEmail 的测试
        ///</summary>
        [TestMethod()]
        public void ValidateEmailErrorTest()
        {
            CommonValidator target = new CommonValidator(); // TODO: 初始化为适当的值
            string Phone = "123465"; // TODO: 初始化为适当的值           
            bool actual;
            actual = target.ValidateEmail(Phone);
            Assert.IsFalse(actual);
          
        }
        /// <summary>
        ///ValidateEmail 的测试
        ///</summary>
        [TestMethod()]
        public void ValidateEmailTrueTest()
        {
            CommonValidator target = new CommonValidator(); // TODO: 初始化为适当的值
            string Phone = "55@qq.com"; // TODO: 初始化为适当的值           
            bool actual;
            actual = target.ValidateEmail(Phone);
            Assert.IsTrue(actual);

        }
        /// <summary>
        ///ValidateEmail 的测试
        ///</summary>
        [TestMethod()]
        public void ValidateEmalIsNullTest()
        {
            CommonValidator target = new CommonValidator(); // TODO: 初始化为适当的值
            string Phone = string.Empty; // TODO: 初始化为适当的值           
            bool actual;
            actual = target.ValidateEmail(Phone);
            Assert.IsFalse(actual);

        }
    }
}
