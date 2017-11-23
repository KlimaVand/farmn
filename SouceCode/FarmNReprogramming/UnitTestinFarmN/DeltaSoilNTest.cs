using FarmN_2010;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting.Web;
using System;

namespace UnitTestinFarmN
{
    
    
    /// <summary>
    ///This is a test class for DeltaSoilNTest and is intended
    ///to contain all DeltaSoilNTest Unit Tests
    ///</summary>
    [TestClass()]
    public class DeltaSoilNTest
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
        ///A test for N_LES Constructor for bad inputs
        ///</summary>
        [TestMethod()]
        public void getDeltaSoilNConstroctor()
        {
            bool expetion=false;
            DeltaSoilN target = new DeltaSoilN(); 
            try
            {
                decimal actual = target.getSoilChange();
            }
            catch (Exception e)
            {
                if (e.Message.Equals("Message: Cannot handle exeption with name \"DeltaSoilN: Has not been init before receiving output\" and type 1"))
                  expetion=true;
            }
            Assert.IsTrue(expetion);
            int errorcode=target.init(1, 1, 1000, 0, 0, 0, 0);
            Assert.AreEqual(errorcode, 0);
            errorcode = target.init(6, 2, 1000, 3, 2.6m, 0.5m, 50);
            Assert.AreEqual(errorcode, 0);
            errorcode = target.init(12, 3, 9990, 7, 5, 1, 100);
            Assert.AreEqual(errorcode, 0);
            expetion = false;
            try
            {
                errorcode = target.init(0, 1, 1000, 1, 1, 1, 1);
            }
            catch (Exception e)
            {
                if (e.Message.Equals("Message: Cannot handle exeption with name \"DeltaSoilN: SoilCode is not valid\" and type 2"))
                    expetion = true;
            }
            Assert.IsTrue(expetion);
            expetion = false;
            try
            {
                errorcode = target.init(-13, 1, 1000, 1, 1, 1, 1);
            }
            catch (Exception e)
            {
                if (e.Message.Equals("Message: Cannot handle exeption with name \"DeltaSoilN: SoilCode is not valid\" and type 2"))
                    expetion = true;
            }
            Assert.IsTrue(expetion);
            expetion = false;
            try
            {
                errorcode = target.init(13, 1, 1000, 1, 1, 1, 1);
            }
            catch (Exception e)
            {
                if (e.Message.Equals("Message: Cannot handle exeption with name \"DeltaSoilN: SoilCode is not valid\" and type 2"))
                    expetion = true;
            }
            Assert.IsTrue(expetion);
            expetion = false;
            try
            {
                errorcode = target.init(900, 1, 1000, 1, 1, 1, 1);
            }
            catch (Exception e)
            {
                if (e.Message.Equals("Message: Cannot handle exeption with name \"DeltaSoilN: SoilCode is not valid\" and type 2"))
                    expetion = true;
            }
            Assert.IsTrue(expetion);
            expetion = false;
            try
            {
                errorcode = target.init(1, 0, 1000, 1, 1, 1, 1);
            }
            catch (Exception e)
            {
                if (e.Message.Equals("Message: Cannot handle exeption with name \"DeltaSoilN: FarmType is not valid\" and type 2"))
                    expetion = true;
            }
            Assert.IsTrue(expetion);

            expetion = false;
            try
            {
                errorcode = target.init(1, -9, 1000, 1, 1, 1, 1);
            }
            catch (Exception e)
            {
                if (e.Message.Equals("Message: Cannot handle exeption with name \"DeltaSoilN: FarmType is not valid\" and type 2"))
                    expetion = true;
            }
            Assert.IsTrue(expetion);
            expetion = false;
            try
            {
                errorcode = target.init(1, 4, 1000, 1, 1, 1, 1);
            }
            catch (Exception e)
            {
                if (e.Message.Equals("Message: Cannot handle exeption with name \"DeltaSoilN: FarmType is not valid\" and type 2"))
                    expetion = true;
            }
            Assert.IsTrue(expetion);
            expetion = false;
            try
            {
                errorcode = target.init(1, 20, 1000, 1, 1, 1, 1);
            }
            catch (Exception e)
            {
                if (e.Message.Equals("Message: Cannot handle exeption with name \"DeltaSoilN: FarmType is not valid\" and type 2"))
                    expetion = true;
            }
            Assert.IsTrue(expetion);
            errorcode = target.init(1, 1, 1000, 1, 1, 1, 1);
            Assert.AreEqual(errorcode, 0);
            errorcode = target.init(1, 2, 1000, 1, 1, 1, 1);
            Assert.AreEqual(errorcode, 0);
            errorcode = target.init(1, 3, 1000, 1, 1, 1, 1);
            Assert.AreEqual(errorcode, 0);

            expetion = false;
            try
            {
                errorcode = target.init(1, 1, -1000, 1, 1, 1, 1);
            }
            catch (Exception e)
            {
                if (e.Message.Equals("Message: Cannot handle exeption with name \"DeltaSoilN: PostalCode is not valid\" and type 2"))
                    expetion = true;
            }
            Assert.IsTrue(expetion);
            expetion = false;
            try
            {
                errorcode = target.init(1, 1, 999, 1, 1, 1, 1);
            }
            catch (Exception e)
            {
                if (e.Message.Equals("Message: Cannot handle exeption with name \"DeltaSoilN: PostalCode is not valid\" and type 2"))
                    expetion = true;
            }
            Assert.IsTrue(expetion);
            expetion = false;
            try
            {
                errorcode = target.init(1, 1, 9991, 1, 1, 1, 1);
            }
            catch (Exception e)
            {
                if (e.Message.Equals("Message: Cannot handle exeption with name \"DeltaSoilN: PostalCode is not valid\" and type 2"))
                    expetion = true;
            }
            Assert.IsTrue(expetion);
            expetion = false;
            try
            {
                errorcode = target.init(1, 1, 77777777, 1, 1, 1, 1);
            }
            catch (Exception e)
            {
                if (e.Message.Equals("Message: Cannot handle exeption with name \"DeltaSoilN: PostalCode is not valid\" and type 2"))
                    expetion = true;
            }
            Assert.IsTrue(expetion);


            errorcode = target.init(1, 1, 1000, 1, 1, 1, 1);
            Assert.AreEqual(errorcode, 0);
            errorcode = target.init(1, 1, 9990, 1, 1, 1, 1);
            Assert.AreEqual(errorcode, 0);
            errorcode = target.init(1, 1, 7000, 1, 1, 1, 1);
            Assert.AreEqual(errorcode, 0);

            expetion = false;
            try
            {
                errorcode = target.init(1, 1, 1000, -1999, 1, 1, 1);
            }
            catch (Exception e)
            {
                if (e.Message.Equals("Message: Cannot handle exeption with name \"DeltaSoilN: TotalCarbonFromCrops is not valid\" and type 2"))
                    expetion = true;
            }
            Assert.IsTrue(expetion);
            expetion = false;
            try
            {
                errorcode = target.init(1, 1, 1000, -1, 1, 1, 1);
            }
            catch (Exception e)
            {
                if (e.Message.Equals("Message: Cannot handle exeption with name \"DeltaSoilN: TotalCarbonFromCrops is not valid\" and type 2"))
                    expetion = true;
            }
            Assert.IsTrue(expetion);

            expetion = false;
            try
            {
                errorcode = target.init(1, 1, 1000, 8, 1, 1, 1);
            }
            catch (Exception e)
            {
                if (e.Message.Equals("Message: Cannot handle exeption with name \"DeltaSoilN: TotalCarbonFromCrops is not valid\" and type 2"))
                    expetion = true;
            }
            Assert.IsTrue(expetion);

            expetion = false;
            try
            {
                errorcode = target.init(1, 1, 1000,1000, 1, 1, 1);
            }
            catch (Exception e)
            {
                if (e.Message.Equals("Message: Cannot handle exeption with name \"DeltaSoilN: TotalCarbonFromCrops is not valid\" and type 2"))
                    expetion = true;
            }
            Assert.IsTrue(expetion);
            errorcode = target.init(1, 1, 1000, 0, 1, 1, 1);
            Assert.AreEqual(errorcode, 0);
            errorcode = target.init(1, 1, 1000, 7, 1, 1, 1);
            Assert.AreEqual(errorcode, 0);
            errorcode = target.init(1, 1, 1000, 3, 1, 1, 1);
            Assert.AreEqual(errorcode, 0);
            expetion = false;
            try
            {
                errorcode = target.init(1, 1, 1000, 1, -1, 1, 1);
            }
            catch (Exception e)
            {
                if (e.Message.Equals("Message: Cannot handle exeption with name \"DeltaSoilN: TotalCarbonFromManure is not valid\" and type 2"))
                    expetion = true;
            }
            Assert.IsTrue(expetion);
            expetion = false;
            try
            {
                errorcode = target.init(1, 1, 1000, 1, -9000, 1, 1);
            }
            catch (Exception e)
            {
                if (e.Message.Equals("Message: Cannot handle exeption with name \"DeltaSoilN: TotalCarbonFromManure is not valid\" and type 2"))
                    expetion = true;
            }
            Assert.IsTrue(expetion);
            expetion = false;
            try
            {
                errorcode = target.init(1, 1, 1000, 1, 6, 1, 1);
            }
            catch (Exception e)
            {
                if (e.Message.Equals("Message: Cannot handle exeption with name \"DeltaSoilN: TotalCarbonFromManure is not valid\" and type 2"))
                    expetion = true;
            }
            Assert.IsTrue(expetion);
            expetion = false;
            try
            {
                errorcode = target.init(1, 1, 1000, 1, 9000, 1, 1);
            }
            catch (Exception e)
            {
                if (e.Message.Equals("Message: Cannot handle exeption with name \"DeltaSoilN: TotalCarbonFromManure is not valid\" and type 2"))
                    expetion = true;
            }
            Assert.IsTrue(expetion);
            errorcode = target.init(1, 1, 1000, 1, 0, 1, 1);
            Assert.AreEqual(errorcode, 0);
            errorcode = target.init(1, 1, 1000, 1, 3, 1, 1);
            Assert.AreEqual(errorcode, 0);
            errorcode = target.init(1, 1, 1000, 1, 5, 1, 1);
            Assert.AreEqual(errorcode, 0);

            expetion = false;
            try
            {
                errorcode = target.init(1, 1, 1000, 1, 1, -1, 1);
            }
            catch (Exception e)
            {
                if (e.Message.Equals("Message: Cannot handle exeption with name \"DeltaSoilN: FractionCatchCrops is not valid\" and type 2"))
                    expetion = true;
            }
            Assert.IsTrue(expetion);

            expetion = false;
            try
            {
                errorcode = target.init(1, 1, 1000, 1, 1, -100, 1);
            }
            catch (Exception e)
            {
                if (e.Message.Equals("Message: Cannot handle exeption with name \"DeltaSoilN: FractionCatchCrops is not valid\" and type 2"))
                    expetion = true;
            }
            Assert.IsTrue(expetion);
            expetion = false;
            try
            {
                errorcode = target.init(1, 1, 1000, 1, 1, 1.1m, 1);
            }
            catch (Exception e)
            {
                if (e.Message.Equals("Message: Cannot handle exeption with name \"DeltaSoilN: FractionCatchCrops is not valid\" and type 2"))
                    expetion = true;
            }
            Assert.IsTrue(expetion);

            expetion = false;
            try
            {
                errorcode = target.init(1, 1, 1000, 1, 1, 100, 1);
            }
            catch (Exception e)
            {
                if (e.Message.Equals("Message: Cannot handle exeption with name \"DeltaSoilN: FractionCatchCrops is not valid\" and type 2"))
                    expetion = true;
            }
            Assert.IsTrue(expetion);
            errorcode = target.init(1, 1, 1000, 1, 1, 0, 1);
            Assert.AreEqual(errorcode, 0);
            errorcode = target.init(1, 1, 1000, 1, 1, 0.5m, 1);
            Assert.AreEqual(errorcode, 0);
            errorcode = target.init(1, 1, 1000, 1, 1, 1, 1);
            Assert.AreEqual(errorcode, 0);
            expetion = false;
            try
            {
                errorcode = target.init(1, 1, 1000, 1, 1, 1, -1);
            }
            catch (Exception e)
            {
                if (e.Message.Equals("Message: Cannot handle exeption with name \"DeltaSoilN: Clay is not valid\" and type 2"))
                    expetion = true;
            }
            Assert.IsTrue(expetion);
            expetion = false;
            try
            {
                errorcode = target.init(1, 1, 1000, 1, 1, 1, -100);
            }
            catch (Exception e)
            {
                if (e.Message.Equals("Message: Cannot handle exeption with name \"DeltaSoilN: Clay is not valid\" and type 2"))
                    expetion = true;
            }
            Assert.IsTrue(expetion);
            expetion = false;
            try
            {
                errorcode = target.init(1, 1, 1000, 1, 1, 1, 101);
            }
            catch (Exception e)
            {
                if (e.Message.Equals("Message: Cannot handle exeption with name \"DeltaSoilN: Clay is not valid\" and type 2"))
                    expetion = true;
            }
            Assert.IsTrue(expetion);
            expetion = false;
            try
            {
                errorcode = target.init(1, 1, 1000, 1, 1, 1, 1000);
            }
            catch (Exception e)
            {
                if (e.Message.Equals("Message: Cannot handle exeption with name \"DeltaSoilN: Clay is not valid\" and type 2"))
                    expetion = true;
            }
            Assert.IsTrue(expetion);
            errorcode = target.init(1, 1, 1000, 1, 1, 1, 0);
            Assert.AreEqual(errorcode, 0);
            errorcode = target.init(1, 1, 1000, 1, 1, 1, 100);
            Assert.AreEqual(errorcode, 0);
            errorcode = target.init(1, 1, 1000, 1, 1, 1, 50);
            Assert.AreEqual(errorcode, 0);

        }
        /// <summary>
        /// test for everysoilCode with farmtype 1 with postal code on 8000. Test with as little as possilbe and 0
        ///</summary>
        [TestMethod()]
        public void typicalInput()
        {
            DeltaSoilN target = new DeltaSoilN();
            int errorcode = target.init(1, 1, 8000, 0, 0, 0, 3.6m);
            Assert.AreEqual(errorcode, 0);
            decimal result=target.getSoilChange();
            Assert.AreEqual(-45m, result);

            errorcode = target.init(1, 1, 8000, 1, 1, 1, 3.6m);
            Assert.AreEqual(errorcode, 0);
            result = target.getSoilChange();
            withinrange(result, -1.870569053m);
           
            target = new DeltaSoilN();
            errorcode = target.init(2, 1, 8000, 0, 0, 0, 3.6m);
            Assert.AreEqual(errorcode, 0);
            result = target.getSoilChange();
            Assert.AreEqual(-45.41m, result);

            errorcode = target.init(2, 1, 8000, 1, 1, 1, 3.6m);
            Assert.AreEqual(errorcode, 0);
            result = target.getSoilChange();
            withinrange(result, -2.280569053m);

            target = new DeltaSoilN();
            errorcode = target.init(3, 1, 8000, 0, 0, 0, 6.4m);
            Assert.AreEqual(errorcode, 0);
            result = target.getSoilChange();
            Assert.AreEqual(-51.232m, result);

            errorcode = target.init(3, 1, 8000, 1, 1, 1, 6.4m);
            Assert.AreEqual(errorcode, 0);
            result = target.getSoilChange();
            withinrange(result, -5.544875625m);

            target = new DeltaSoilN();
            errorcode = target.init(4, 1, 8000, 0, 0, 0, 7.2m);
            Assert.AreEqual(errorcode, 0);
            result = target.getSoilChange();
            Assert.AreEqual(-61.072m, result);

            errorcode = target.init(4, 1, 8000, 1, 1, 1, 7.2m);
            Assert.AreEqual(errorcode, 0);
            result = target.getSoilChange();
            withinrange(result, -14.6959845m);

            target = new DeltaSoilN();
            errorcode = target.init(5, 1, 8000, 0, 0, 0, 12.2m);
            Assert.AreEqual(errorcode, 0);
            result = target.getSoilChange();
            Assert.AreEqual(-77.718m, result);

            errorcode = target.init(5, 1, 8000, 1, 1, 1, 12.2m);
            Assert.AreEqual(errorcode, 0);
            result = target.getSoilChange();
            withinrange(result, -27.49697742m);
           
            target = new DeltaSoilN();
            errorcode = target.init(6, 1, 8000, 0, 0, 0, 12m);
            Assert.AreEqual(errorcode, 0);
            result = target.getSoilChange();
            Assert.AreEqual(-75.012m, result);

            errorcode = target.init(6, 1, 8000, 1, 1, 1, 12m);
            Assert.AreEqual(errorcode, 0);
            result = target.getSoilChange();
            withinrange(result, -24.92916185m);
            
           target = new DeltaSoilN();
           errorcode = target.init(7, 1, 8000, 0, 0, 0, 17.4m);
           Assert.AreEqual(errorcode, 0);
           result = target.getSoilChange();
           Assert.AreEqual(-91.33m, result);

           errorcode = target.init(7, 1, 8000, 1, 1, 1, 17.4m);
           Assert.AreEqual(errorcode, 0);
           result = target.getSoilChange();
           withinrange(result, -37.96548084m);

           target = new DeltaSoilN();
           errorcode = target.init(8, 1, 8000, 0, 0, 0,20m);
           Assert.AreEqual(errorcode, 0);
           result = target.getSoilChange();
           Assert.AreEqual(-91.33m, result);

           errorcode = target.init(8, 1, 8000, 1, 1, 1, 20m);
           Assert.AreEqual(errorcode, 0);
           result = target.getSoilChange();
           withinrange(result, -36.70055288m);

           target = new DeltaSoilN();
           errorcode = target.init(9, 1, 8000, 0, 0, 0, 50m);
           Assert.AreEqual(errorcode, 0);
           result = target.getSoilChange();
           Assert.AreEqual(-91.33m, result);

           errorcode = target.init(9, 1, 8000, 1, 1, 1, 50m);
           Assert.AreEqual(errorcode, 0);
           result = target.getSoilChange();
           withinrange(result, -30.91394684m);

          target = new DeltaSoilN();
          errorcode = target.init(10, 1, 8000, 0, 0, 0, 20m);
           Assert.AreEqual(errorcode, 0);
           result = target.getSoilChange();
           Assert.AreEqual(-91.33m, result);

           errorcode = target.init(10, 1, 8000, 1, 1, 1, 20m);
           Assert.AreEqual(errorcode, 0);
           result = target.getSoilChange();
           withinrange(result, -36.70055288m);

           target = new DeltaSoilN();
           errorcode = target.init(11, 1, 8000, 0, 0, 0, 7.5m);
           Assert.AreEqual(errorcode, 0);
           result = target.getSoilChange();
           Assert.AreEqual(-61.072m, result);

           errorcode = target.init(11, 1, 8000, 1, 1, 1, 7.5m);
           Assert.AreEqual(errorcode, 0);
           result = target.getSoilChange();
           withinrange(result, -14.44271475m);

           target = new DeltaSoilN();
           errorcode = target.init(12, 1, 8000, 0, 0, 0, 7.5m);
           Assert.AreEqual(errorcode, 0);
           result = target.getSoilChange();
           Assert.AreEqual(-61.072m, result);

           errorcode = target.init(12, 1, 8000, 1, 1, 1, 7.5m);
           Assert.AreEqual(errorcode, 0);
           result = target.getSoilChange();
           withinrange(result, -14.44271475m);
            
        }

        /// <summary>
        /// test for everysoilCode with farmtype 1 with postal code on 1000. Test with as little as possilbe and 0
        ///</summary>
        [TestMethod()]
        public void typicalInputpart2()
        {
            DeltaSoilN target = new DeltaSoilN();
            int errorcode = target.init(1, 1, 1000, 0, 0, 0, 3.6m);
            Assert.AreEqual(errorcode, 0);
            decimal result = target.getSoilChange();
            Assert.AreEqual(-43.606m, result);

            errorcode = target.init(1, 1, 1000, 1, 1, 1, 3.6m);
            Assert.AreEqual(errorcode, 0);
            result = target.getSoilChange();
            withinrange(result, -0.476569053m);

            target = new DeltaSoilN();
            errorcode = target.init(2, 1, 1000, 0, 0, 0, 3.6m);
            Assert.AreEqual(errorcode, 0);
            result = target.getSoilChange();
            Assert.AreEqual(-42.786m, result);

            errorcode = target.init(2, 1, 1000, 1, 1, 1, 3.6m);
            Assert.AreEqual(errorcode, 0);
            result = target.getSoilChange();
            withinrange(result, 0.343430947m);

            target = new DeltaSoilN();
            errorcode = target.init(3, 1, 1000, 0, 0, 0, 6.4m);
            Assert.AreEqual(errorcode, 0);
            result = target.getSoilChange();
            Assert.AreEqual(-49.674m, result);

            errorcode = target.init(3, 1, 1000, 1, 1, 1, 6.4m);
            Assert.AreEqual(errorcode, 0);
            result = target.getSoilChange();
            withinrange(result, -3.986875625m);

            target = new DeltaSoilN();
            errorcode = target.init(4, 1, 1000, 0, 0, 0, 7.2m);
            Assert.AreEqual(errorcode, 0);
            result = target.getSoilChange();
            Assert.AreEqual(-59.924m, result);

            errorcode = target.init(4, 1, 1000, 1, 1, 1, 7.2m);
            Assert.AreEqual(errorcode, 0);
            result = target.getSoilChange();
            withinrange(result, -13.5479845m);

            target = new DeltaSoilN();
            errorcode = target.init(5, 1, 1000, 0, 0, 0, 12.2m);
            Assert.AreEqual(errorcode, 0);
            result = target.getSoilChange();
            Assert.AreEqual(-74.356m, result);

            errorcode = target.init(5, 1, 1000, 1, 1, 1, 12.2m);
            Assert.AreEqual(errorcode, 0);
            result = target.getSoilChange();
            withinrange(result, -24.13497742m);

            target = new DeltaSoilN();
            errorcode = target.init(6, 1, 1000, 0, 0, 0, 12m);
            Assert.AreEqual(errorcode, 0);
            result = target.getSoilChange();
            Assert.AreEqual(-70.912m, result);

            errorcode = target.init(6, 1, 1000, 1, 1, 1, 12m);
            Assert.AreEqual(errorcode, 0);
            result = target.getSoilChange();
            withinrange(result, -20.82916185m);

            target = new DeltaSoilN();
            errorcode = target.init(7, 1, 1000, 0, 0, 0, 17.4m);
            Assert.AreEqual(errorcode, 0);
            result = target.getSoilChange();
            Assert.AreEqual(-82.802m, result);

            errorcode = target.init(7, 1, 1000, 1, 1, 1, 17.4m);
            Assert.AreEqual(errorcode, 0);
            result = target.getSoilChange();
            withinrange(result, -29.43748084m);

            target = new DeltaSoilN();
            errorcode = target.init(8, 1, 1000, 0, 0, 0, 20m);
            Assert.AreEqual(errorcode, 0);
            result = target.getSoilChange();
            Assert.AreEqual(-82.802m, result);

            errorcode = target.init(8, 1, 1000, 1, 1, 1, 20m);
            Assert.AreEqual(errorcode, 0);
            result = target.getSoilChange();
            withinrange(result, -28.17255288m);

            target = new DeltaSoilN();
            errorcode = target.init(9, 1, 1000, 0, 0, 0, 50m);
            Assert.AreEqual(errorcode, 0);
            result = target.getSoilChange();
            Assert.AreEqual(-82.802m, result);

            errorcode = target.init(9, 1, 1000, 1, 1, 1, 50m);
            Assert.AreEqual(errorcode, 0);
            result = target.getSoilChange();
            withinrange(result, -22.38594684m);

            target = new DeltaSoilN();
            errorcode = target.init(10, 1, 1000, 0, 0, 0, 20m);
            Assert.AreEqual(errorcode, 0);
            result = target.getSoilChange();
            Assert.AreEqual(-82.802m, result);

            errorcode = target.init(10, 1, 1000, 1, 1, 1, 20m);
            Assert.AreEqual(errorcode, 0);
            result = target.getSoilChange();
            withinrange(result, -28.17255288m);

            target = new DeltaSoilN();
            errorcode = target.init(11, 1, 1000, 0, 0, 0, 7.5m);
            Assert.AreEqual(errorcode, 0);
            result = target.getSoilChange();
            Assert.AreEqual(-59.924m, result);

            errorcode = target.init(11, 1, 1000, 1, 1, 1, 7.5m);
            Assert.AreEqual(errorcode, 0);
            result = target.getSoilChange();
            withinrange(result, -13.29471475m);

            target = new DeltaSoilN();
            errorcode = target.init(12, 1, 1000, 0, 0, 0, 7.5m);
            Assert.AreEqual(errorcode, 0);
            result = target.getSoilChange();
            Assert.AreEqual(-59.924m, result);

            errorcode = target.init(12, 1, 1000, 1, 1, 1, 7.5m);
            Assert.AreEqual(errorcode, 0);
            result = target.getSoilChange();
            withinrange(result, -13.29471475m);

        }


        /// <summary>
        /// test for everysoilCode with farmtype 3 with postal code on 8000. Test with as little as possilbe and 0
        ///</summary>
        [TestMethod()]
        public void typicalInput3()
        {
            DeltaSoilN target = new DeltaSoilN();
            int errorcode = target.init(1, 3, 8000, 0, 0, 0, 3.6m);
            Assert.AreEqual(errorcode, 0);
            decimal result = target.getSoilChange();
            Assert.AreEqual(-52.544m, result);

            errorcode = target.init(1, 3, 8000, 1, 1, 1, 3.6m);
            Assert.AreEqual(errorcode, 0);
            result = target.getSoilChange();
            withinrange(result, -9.414569053m);

            target = new DeltaSoilN();
            errorcode = target.init(2, 3, 8000, 0, 0, 0, 3.6m);
            Assert.AreEqual(errorcode, 0);
            result = target.getSoilChange();
            Assert.AreEqual(-56.398m, result);

            errorcode = target.init(2, 3, 8000, 1, 1, 1, 3.6m);
            Assert.AreEqual(errorcode, 0);
            result = target.getSoilChange();
            withinrange(result, -13.26856905m);

            target = new DeltaSoilN();
            errorcode = target.init(3, 3, 8000, 0, 0, 0, 6.4m);
            Assert.AreEqual(errorcode, 0);
            result = target.getSoilChange();
            Assert.AreEqual(-60.416m, result);

            errorcode = target.init(3, 3, 8000, 1, 1, 1, 6.4m);
            Assert.AreEqual(errorcode, 0);
            result = target.getSoilChange();
            withinrange(result, -14.72887563m);

            target = new DeltaSoilN();
            errorcode = target.init(4, 3, 8000, 0, 0, 0, 7.2m);
            Assert.AreEqual(errorcode, 0);
            result = target.getSoilChange();
            Assert.AreEqual(-72.88m, result);

            errorcode = target.init(4, 3, 8000, 1, 1, 1, 7.2m);
            Assert.AreEqual(errorcode, 0);
            result = target.getSoilChange();
            withinrange(result, -26.5039845m);

            target = new DeltaSoilN();
            errorcode = target.init(5, 3, 8000, 0, 0, 0, 12.2m);
            Assert.AreEqual(errorcode, 0);
            result = target.getSoilChange();
            Assert.AreEqual(-95.512m, result);

            errorcode = target.init(5, 3, 8000, 1, 1, 1, 12.2m);
            Assert.AreEqual(errorcode, 0);
            result = target.getSoilChange();
            withinrange(result, -45.29097742m);

            target = new DeltaSoilN();
            errorcode = target.init(6, 3, 8000, 0, 0, 0, 12m);
            Assert.AreEqual(errorcode, 0);
            result = target.getSoilChange();
            Assert.AreEqual(-92.642m, result);

            errorcode = target.init(6, 3, 8000, 1, 1, 1, 12m);
            Assert.AreEqual(errorcode, 0);
            result = target.getSoilChange();
            withinrange(result, -42.55916185m);

            target = new DeltaSoilN();
            errorcode = target.init(7, 3, 8000, 0, 0, 0, 17.4m);
            Assert.AreEqual(errorcode, 0);
            result = target.getSoilChange();
            Assert.AreEqual(-112.404m, result);

            errorcode = target.init(7, 3, 8000, 1, 1, 1, 17.4m);
            Assert.AreEqual(errorcode, 0);
            result = target.getSoilChange();
            withinrange(result, -59.03948084m);

            target = new DeltaSoilN();
            errorcode = target.init(8, 3, 8000, 0, 0, 0, 20m);
            Assert.AreEqual(errorcode, 0);
            result = target.getSoilChange();
            Assert.AreEqual(-112.404m, result);

            errorcode = target.init(8, 3, 8000, 1, 1, 1, 20m);
            Assert.AreEqual(errorcode, 0);
            result = target.getSoilChange();
            withinrange(result, -57.77455288m);

            target = new DeltaSoilN();
            errorcode = target.init(9, 3, 8000, 0, 0, 0, 50m);
            Assert.AreEqual(errorcode, 0);
            result = target.getSoilChange();
            Assert.AreEqual(-112.404m, result);

            errorcode = target.init(9, 3, 8000, 1, 1, 1, 50m);
            Assert.AreEqual(errorcode, 0);
            result = target.getSoilChange();
            withinrange(result, -51.98794684m);

            target = new DeltaSoilN();
            errorcode = target.init(10, 3, 8000, 0, 0, 0, 20m);
            Assert.AreEqual(errorcode, 0);
            result = target.getSoilChange();
            Assert.AreEqual(-112.404m, result);

            errorcode = target.init(10, 3, 8000, 1, 1, 1, 20m);
            Assert.AreEqual(errorcode, 0);
            result = target.getSoilChange();
            withinrange(result, -57.77455288m);

            target = new DeltaSoilN();
            errorcode = target.init(11, 3, 8000, 0, 0, 0, 7.5m);
            Assert.AreEqual(errorcode, 0);
            result = target.getSoilChange();
            Assert.AreEqual(-72.88m, result);

            errorcode = target.init(11, 3, 8000, 1, 1, 1, 7.5m);
            Assert.AreEqual(errorcode, 0);
            result = target.getSoilChange();
            withinrange(result, -26.25071475m);

            target = new DeltaSoilN();
            errorcode = target.init(12, 3, 8000, 0, 0, 0, 7.5m);
            Assert.AreEqual(errorcode, 0);
            result = target.getSoilChange();
            Assert.AreEqual(-72.88m, result);

            errorcode = target.init(12, 3, 8000, 1, 1, 1, 7.5m);
            Assert.AreEqual(errorcode, 0);
            result = target.getSoilChange();
            withinrange(result, -26.25071475m);

        }

        /// <summary>
        /// test for everysoilCode with farmtype 1 with postal code on 1000. Test with as little as possilbe and 0
        ///</summary>
        [TestMethod()]
        public void typicalInputpart4()
        {
            DeltaSoilN target = new DeltaSoilN();
            int errorcode = target.init(1, 3, 1000, 0, 0, 0, 3.6m);
            Assert.AreEqual(errorcode, 0);
            decimal result = target.getSoilChange();
            Assert.AreEqual(-52.052m, result);

            errorcode = target.init(1, 3, 1000, 1, 1, 1, 3.6m);
            Assert.AreEqual(errorcode, 0);
            result = target.getSoilChange();
            withinrange(result, -8.922569053m);

            target = new DeltaSoilN();
            errorcode = target.init(2, 3, 1000, 0, 0, 0, 3.6m);
            Assert.AreEqual(errorcode, 0);
            result = target.getSoilChange();
            Assert.AreEqual(-55.168m, result);

            errorcode = target.init(2, 3, 1000, 1, 1, 1, 3.6m);
            Assert.AreEqual(errorcode, 0);
            result = target.getSoilChange();
            withinrange(result, -12.03856905m);

            target = new DeltaSoilN();
            errorcode = target.init(3, 3, 1000, 0, 0, 0, 6.4m);
            Assert.AreEqual(errorcode, 0);
            result = target.getSoilChange();
            Assert.AreEqual(-60.006m, result);

            errorcode = target.init(3, 3, 1000, 1, 1, 1, 6.4m);
            Assert.AreEqual(errorcode, 0);
            result = target.getSoilChange();
            withinrange(result, -14.31887563m);

            target = new DeltaSoilN();
            errorcode = target.init(4, 3, 1000, 0, 0, 0, 7.2m);
            Assert.AreEqual(errorcode, 0);
            result = target.getSoilChange();
            Assert.AreEqual(-73.454m, result);

            errorcode = target.init(4, 3, 1000, 1, 1, 1, 7.2m);
            Assert.AreEqual(errorcode, 0);
            result = target.getSoilChange();
            withinrange(result, -27.0779845m);

            target = new DeltaSoilN();
            errorcode = target.init(5, 3, 1000, 0, 0, 0, 12.2m);
            Assert.AreEqual(errorcode, 0);
            result = target.getSoilChange();
            Assert.AreEqual(-94.61m, result);

            errorcode = target.init(5, 3, 1000, 1, 1, 1, 12.2m);
            Assert.AreEqual(errorcode, 0);
            result = target.getSoilChange();
            withinrange(result, -44.38897742m);

            target = new DeltaSoilN();
            errorcode = target.init(6, 3, 1000, 0, 0, 0, 12m);
            Assert.AreEqual(errorcode, 0);
            result = target.getSoilChange();
            Assert.AreEqual(-91.002m, result);

            errorcode = target.init(6, 3, 1000, 1, 1, 1, 12m);
            Assert.AreEqual(errorcode, 0);
            result = target.getSoilChange();
            withinrange(result, -40.91916185m);

            target = new DeltaSoilN();
            errorcode = target.init(7, 3, 1000, 0, 0, 0, 17.4m);
            Assert.AreEqual(errorcode, 0);
            result = target.getSoilChange();
            Assert.AreEqual(-105.68m, result);

            errorcode = target.init(7, 3, 1000, 1, 1, 1, 17.4m);
            Assert.AreEqual(errorcode, 0);
            result = target.getSoilChange();
            withinrange(result, -52.31548084m);

            target = new DeltaSoilN();
            errorcode = target.init(8, 3, 1000, 0, 0, 0, 20m);
            Assert.AreEqual(errorcode, 0);
            result = target.getSoilChange();
            Assert.AreEqual(-105.68m, result);

            errorcode = target.init(8, 3, 1000, 1, 1, 1, 20m);
            Assert.AreEqual(errorcode, 0);
            result = target.getSoilChange();
            withinrange(result, -51.05055288m);

            target = new DeltaSoilN();
              errorcode = target.init(9, 3, 1000, 0, 0, 0, 50m);
             Assert.AreEqual(errorcode, 0);
             result = target.getSoilChange();
             Assert.AreEqual(-105.68m, result);

            errorcode = target.init(9, 3, 1000, 1, 1, 1, 50m);
            Assert.AreEqual(errorcode, 0);
            result = target.getSoilChange();
            withinrange(result, -45.26394684m);

            target = new DeltaSoilN();
             errorcode = target.init(10, 3, 1000, 0, 0, 0, 20m);
             Assert.AreEqual(errorcode, 0);
             result = target.getSoilChange();
             Assert.AreEqual(-105.68m, result);

            errorcode = target.init(10, 3, 1000, 1, 1, 1, 20m);
             Assert.AreEqual(errorcode, 0);
             result = target.getSoilChange();
             withinrange(result, -51.05055288m);

             target = new DeltaSoilN();
             errorcode = target.init(11, 3, 1000, 0, 0, 0, 7.5m);
             Assert.AreEqual(errorcode, 0);
             result = target.getSoilChange();
             Assert.AreEqual(-73.454m, result);

             errorcode = target.init(11, 3, 1000, 1, 1, 1, 7.5m);
             Assert.AreEqual(errorcode, 0);
             result = target.getSoilChange();
             withinrange(result, -26.82471475m);

             target = new DeltaSoilN();
             errorcode = target.init(12, 3, 1000, 0, 0, 0, 7.5m);
             Assert.AreEqual(errorcode, 0);
             result = target.getSoilChange();
             Assert.AreEqual(-73.454m, result);

            errorcode = target.init(12, 3, 1000, 1, 1, 1, 7.5m);
             Assert.AreEqual(errorcode, 0);
             result = target.getSoilChange();
             withinrange(result, -26.82471475m);

        }
        public void withinrange(decimal result, decimal exel)
        {

            if ((exel + 0.00000002m) < result || (exel - 0.00000002m) > result)
                Assert.Fail();

        }
    }
}
