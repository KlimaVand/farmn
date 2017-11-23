using FarmN_2010;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting.Web;
using System;
namespace UnitTestinFarmN
{
    
    
    /// <summary>
    ///This is a test class for SIMDENTest and is intended
    ///to contain all SIMDENTest Unit Tests
    ///</summary>
    [TestClass()]
    public class SIMDENTest
    {


        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
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

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        /// <summary>
        ///A test for SIMDEN Constructor for bad inputs
        ///</summary>
        [TestMethod()]
        public void SIMDENExeption()
        {
            SIMDEN target =new SIMDEN();

            
            int returnValue;
            bool expetion=false;
            try
            {
                returnValue = target.init(0, 1, 0, 0, 1, 2);
            }
            catch (Exception e)
            {
                if (e.Message.Equals("Message: Cannot handle exeption with name \"SIMDEN: SoilType is not valid\" and type 2"))
                    expetion = true;
            }

            Assert.IsTrue(expetion);
            expetion = false;
            try
            {
                returnValue = target.init(-900, 1, 0, 0, 1, 2);
            }
            catch (Exception e)
            {
                if (e.Message.Equals("Message: Cannot handle exeption with name \"SIMDEN: SoilType is not valid\" and type 2"))
                    expetion = true;
            }

            Assert.IsTrue(expetion);
            returnValue = target.init(1, 1, 0, 0, 1, 2);
            Assert.AreEqual(0, returnValue);
            returnValue = target.init(6, 1, 0, 0, 1, 2);
            Assert.AreEqual(0, returnValue);
            returnValue = target.init(12, 1, 0, 0, 1, 2);
            Assert.AreEqual(0, returnValue);
            expetion = false;
            try
            {
                returnValue = target.init(13, 1, 0, 0, 1, 2);
            }
            catch (Exception e)
            {
                if (e.Message.Equals("Message: Cannot handle exeption with name \"SIMDEN: SoilType is not valid\" and type 2"))
                    expetion = true;
            }

            Assert.IsTrue(expetion);
            expetion = false;
            try
            {
                returnValue = target.init(18, 1, 0, 0, 1, 2);
            }
            catch (Exception e)
            {
                if (e.Message.Equals("Message: Cannot handle exeption with name \"SIMDEN: SoilType is not valid\" and type 2"))
                    expetion = true;
            }

            Assert.IsTrue(expetion);
            expetion = false;
            try
            {
                returnValue = target.init(1, -900, 0, 0, 1, 2);
            }
            catch (Exception e)
            {
                if (e.Message.Equals("Message: Cannot handle exeption with name \"SIMDEN: FarmType is not valid\" and type 2"))
                    expetion = true;
            }

            Assert.IsTrue(expetion);
            expetion = false;
            try
            {
                returnValue = target.init(1, 0, 0, 0, 1, 2);
            }
            catch (Exception e)
            {
                if (e.Message.Equals("Message: Cannot handle exeption with name \"SIMDEN: FarmType is not valid\" and type 2"))
                    expetion = true;
            }

            Assert.IsTrue(expetion);
            returnValue = target.init(1, 1, 0, 0, 1, 0);
            Assert.AreEqual(0, returnValue);
            returnValue = target.init(1, 2, 0, 0, 1, 0);
            Assert.AreEqual(0, returnValue);
            returnValue = target.init(1, 3, 0, 0, 1, 0);
            Assert.AreEqual(0, returnValue);
            expetion = false;
            try
            {
                returnValue = target.init(1, 4, 0, 0, 1, 2);
            }
            catch (Exception e)
            {
                if (e.Message.Equals("Message: Cannot handle exeption with name \"SIMDEN: FarmType is not valid\" and type 2"))
                    expetion = true;
            }

            Assert.IsTrue(expetion);
            expetion = false;
            try
            {
                returnValue = target.init(1,900, 0, 0, 1, 2);
            }
            catch (Exception e)
            {
                if (e.Message.Equals("Message: Cannot handle exeption with name \"SIMDEN: FarmType is not valid\" and type 2"))
                    expetion = true;
            }

            Assert.IsTrue(expetion);
            expetion = false;
            try
            {
                returnValue = target.init(1, 1, -10, 0, 1, 2);
            }
            catch (Exception e)
            {
                if (e.Message.Equals("Message: Cannot handle exeption with name \"SIMDEN: FertiliserN is not valid\" and type 2"))
                    expetion = true;
            }

            Assert.IsTrue(expetion);
            expetion = false;
            try
            {
                returnValue = target.init(1, 1, -1, 0, 1, 2);
            }
            catch (Exception e)
            {
                if (e.Message.Equals("Message: Cannot handle exeption with name \"SIMDEN: FertiliserN is not valid\" and type 2"))
                    expetion = true;
            }

            Assert.IsTrue(expetion);
            returnValue = target.init(1, 1, 0, 0, 1, 0);
            Assert.AreEqual(0, returnValue);
            returnValue = target.init(1, 1, 10, 0, 1, 0);
            Assert.AreEqual(0, returnValue);
            expetion = false;
            try
            {
                returnValue = target.init(1, 1, 0, -10, 1, 2);
            }
            catch (Exception e)
            {
                if (e.Message.Equals("Message: Cannot handle exeption with name \"SIMDEN: ManureNincorp is not valid\" and type 2"))
                    expetion = true;
            }

            Assert.IsTrue(expetion);
            expetion = false;
            try
            {
                returnValue = target.init(1, 1, 0, -1, 1, 2);
            }
            catch (Exception e)
            {
                if (e.Message.Equals("Message: Cannot handle exeption with name \"SIMDEN: ManureNincorp is not valid\" and type 2"))
                    expetion = true;
            }

            Assert.IsTrue(expetion);
            returnValue = target.init(1, 1, 2, 0, 1, 0);
            Assert.AreEqual(0, returnValue);
            returnValue = target.init(1, 1, 2, 10, 1, 0);
            Assert.AreEqual(0, returnValue);
            expetion = false;
            try
            {
                returnValue = target.init(1, 1, 0, 0, -1, 2);
            }
            catch (Exception e)
            {
                if (e.Message.Equals("Message: Cannot handle exeption with name \"SIMDEN: ManureNspread is not valid\" and type 2"))
                    expetion = true;
            }

            Assert.IsTrue(expetion);
            expetion = false;
            try
            {
                returnValue = target.init(1, 1, 0, 0, -10, 2);
            }
            catch (Exception e)
            {
                if (e.Message.Equals("Message: Cannot handle exeption with name \"SIMDEN: ManureNspread is not valid\" and type 2"))
                    expetion = true;
            }

            Assert.IsTrue(expetion);
            returnValue = target.init(1, 1, 0, 0, 0, 0);
            Assert.AreEqual(0, returnValue);
            returnValue = target.init(1, 1, 0, 0, 900, 0);
            Assert.AreEqual(0, returnValue);

            expetion = false;
            try
            {
                returnValue = target.init(1, 1, 0, 0, 1, -10);
            }
            catch (Exception e)
            {
                if (e.Message.Equals("Message: Cannot handle exeption with name \"SIMDEN: NFixation is not valid\" and type 2"))
                    expetion = true;
            }

            Assert.IsTrue(expetion);
            expetion = false;
            try
            {
                returnValue = target.init(1, 1, 0, 0, 1, -1);
            }
            catch (Exception e)
            {
                if (e.Message.Equals("Message: Cannot handle exeption with name \"SIMDEN: NFixation is not valid\" and type 2"))
                    expetion = true;
            }

            Assert.IsTrue(expetion);
            returnValue = target.init(1, 1, 0, 0, 1, 0);
            Assert.AreEqual(0, returnValue);
            returnValue = target.init(1, 1, 0, 0, 1, 10);
            Assert.AreEqual(0, returnValue);
            returnValue = target.init(1, 1, 0, 0, 1, 0);
            if(returnValue==-1)
                 Assert.Fail();
        }

        /// <summary>
        ///A test for SIMDEN Calculates correct if we use farm type 1 (Eg the fist switch). In every case we use low amount of everything (1) or nothing at all (0)
        ///</summary>
        [TestMethod()]
        public void SIMDENFarmType1()
        {   bool used=false;
            SIMDEN target = new SIMDEN();
            try
            {
                target.getDenitrification();
            }
            catch (Exception e)
            {
                string df = e.Message;
                if (e.Message.Equals("Message: Cannot handle exeption with name \"SIMDEN:Has not been init before receiving output\" and type 1"))
                used = true;
            }
            Assert.IsTrue(used);
            target = new SIMDEN();  target.init(1, 1, 1, 1, 1, 1);
            decimal actual = target.getDenitrification();
            decimal expected = 0.068m;
            Assert.AreEqual(actual, expected);

            target = new SIMDEN();  target.init(1, 1, 0, 0, 0, 0);
            actual = target.getDenitrification();
            expected = 0.0m;
            Assert.AreEqual(actual, expected);


            target = new SIMDEN();  target.init(2, 1, 1, 1, 1, 1);
            actual = target.getDenitrification();
            expected = 0.592m;
            Assert.AreEqual(actual, expected);

            target = new SIMDEN();  target.init(2, 1, 0, 0, 0, 0);
            actual = target.getDenitrification();
            expected = 0.5m;
            Assert.AreEqual(actual, expected);



            target = new SIMDEN();  target.init(3, 1, 1, 1, 1, 1);
            actual = target.getDenitrification();
            expected = 1.54m;
            Assert.AreEqual(actual, expected);

            target = new SIMDEN();  target.init(3, 1, 0, 0, 0, 0);
            actual = target.getDenitrification();
            expected = 1.4m;
            Assert.AreEqual(actual, expected);



          
            Assert.AreEqual(actual, expected);
            target = new SIMDEN();  target.init(4, 1, 1, 1, 1, 1);
            actual = target.getDenitrification();
            expected = 2.988m;
            Assert.AreEqual(actual, expected);
  
            target = new SIMDEN();  target.init(4, 1, 0, 0, 0, 0);
            actual = target.getDenitrification();
            expected = 2.8m;
            Assert.AreEqual(actual, expected);


            target = new SIMDEN();  target.init(5, 1, 1, 1, 1, 1);
            actual = target.getDenitrification();
            expected = 5.012m;
            Assert.AreEqual(actual, expected);

            target = new SIMDEN();  target.init(5, 1, 0, 0, 0, 0);
            actual = target.getDenitrification();
            expected = 4.8m;


            target = new SIMDEN();  target.init(6, 1, 1, 1, 1, 1);
            actual = target.getDenitrification();
            expected = 7.56m;
            Assert.AreEqual(actual, expected);

            target = new SIMDEN();  target.init(6, 1, 0, 0, 0, 0);
            actual = target.getDenitrification();
            expected = 7.3m;


            target = new SIMDEN();  target.init(7, 1, 1, 1, 1, 1);
            actual = target.getDenitrification();
            expected = 10.508m;
            Assert.AreEqual(actual, expected);

            target = new SIMDEN();  target.init(7, 1, 0, 0, 0, 0);
            actual = target.getDenitrification();
            expected = 10.2m;


            target = new SIMDEN();  target.init(10, 1, 1, 1, 1, 1);
            actual = target.getDenitrification();
            expected = 14.356m;
            Assert.AreEqual(actual, expected);
 
            target = new SIMDEN();  target.init(10, 1, 0, 0, 0, 0);
            actual = target.getDenitrification();
            expected = 14m;
            Assert.AreEqual(actual, expected);
        }


        /// <summary>
        ///A test for SIMDEN Calculates correct if we use farm type 3 (Eg the 2. switch). In every case we use low amount of everything (1) or nothing at all (0)
        ///</summary>
        [TestMethod()]
        public void SIMDENFarmType3()
        {
            SIMDEN target;

            target = new SIMDEN();  target.init(1, 3, 1, 1, 1, 1);
            decimal actual = target.getDenitrification();
            decimal expected = 0.892m;
            Assert.AreEqual(actual, expected);
       
            target = new SIMDEN();  target.init(1, 3, 0, 0, 0, 0);
            actual = target.getDenitrification();
            expected = 0.8m;
            Assert.AreEqual(actual, expected);

            target = new SIMDEN();  target.init(2, 3, 1, 1, 1, 1);
            actual = target.getDenitrification();
            expected = 1.94m;
            Assert.AreEqual(actual, expected);

            target = new SIMDEN();  target.init(2, 3, 0, 0, 0, 0);
            actual = target.getDenitrification();
            expected = 1.8m;
            Assert.AreEqual(actual, expected);

            target = new SIMDEN();  target.init(3, 3, 1, 1, 1, 1);
            actual = target.getDenitrification();
            expected = 3.488m;
            Assert.AreEqual(actual, expected);

            target = new SIMDEN();  target.init(3, 3, 0, 0, 0, 0);
            actual = target.getDenitrification();
            expected = 3.3m;
            Assert.AreEqual(actual, expected);


            target = new SIMDEN();  target.init(4, 3, 1, 1, 1, 1);
            actual = target.getDenitrification();
            expected = 6.884m;
            Assert.AreEqual(actual, expected);

            target = new SIMDEN();  target.init(4, 3, 0, 0, 0, 0);
            actual = target.getDenitrification();
            expected = 6.6m;
            Assert.AreEqual(actual, expected);


            target = new SIMDEN();  target.init(5, 3, 1, 1, 1, 1);
            actual = target.getDenitrification();
            expected = 11.108m;
            Assert.AreEqual(actual, expected);

            target = new SIMDEN();  target.init(5, 3, 0, 0, 0, 0);
            actual = target.getDenitrification();
            expected = 10.8m;
            Assert.AreEqual(actual, expected);

            target = new SIMDEN();  target.init(6, 3, 1, 1, 1, 1);
            actual = target.getDenitrification();
            expected = 14.756m;
            Assert.AreEqual(actual, expected);

            target = new SIMDEN();  target.init(6, 3, 0, 0, 0, 0);
            actual = target.getDenitrification();
            expected = 14.4m;
            Assert.AreEqual(actual, expected);


            target = new SIMDEN();  target.init(7, 3, 1, 1, 1, 1);
            actual = target.getDenitrification();
            expected = 18.804m;
            Assert.AreEqual(actual, expected);

            target = new SIMDEN();  target.init(7, 3, 0, 0, 0, 0);
            actual = target.getDenitrification();
            expected = 18.4m;
            Assert.AreEqual(actual, expected);

            
            target = new SIMDEN();  target.init(10, 3, 1, 1, 1, 1);
            actual = target.getDenitrification();
            expected = 27.452m;
            Assert.AreEqual(actual, expected);

            target = new SIMDEN();  target.init(10, 3, 0, 0, 0, 0);
            actual = target.getDenitrification();
            expected = 27m;
            Assert.AreEqual(actual, expected);

        }

    }
}
