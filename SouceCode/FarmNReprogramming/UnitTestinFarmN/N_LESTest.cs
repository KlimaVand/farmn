using FarmN_2010;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting.Web;
using System;

namespace UnitTestinFarmN
{
    
    
    /// <summary>
    ///This is a test class for N_LESTest and is intended
    ///to contain all N_LESTest Unit Tests
    ///</summary>
    [TestClass()]
    public class N_LESTest
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
        /// <summary>
        ///A test for N_LES Constructor for bad inputs
        ///</summary>
        [TestMethod()]
        public void N_LESExeption()
        {
            bool expetion = false;
            N_LES target=target = new N_LES();
            try{
                target.getNLesKgNPrHa();
            }
            catch (Exception e)
            {
                if (e.Message.Equals("Message: Cannot handle exeption with name \"N_LES:Has not been init before receiving output\" and type 2"))
                    expetion = true;
            }
            Assert.IsTrue(expetion);
            expetion = false;
            try
            {
                target.getNLesKgNPrHa();
            }
            catch (Exception e)
            {
                if (e.Message.Equals("Message: Cannot handle exeption with name \"N_LES:Has not been init before receiving output\" and type 2"))
                    expetion = true;
            }
            Assert.IsTrue(expetion);
            int returnvalute;
            expetion = false;
            try
            {
                returnvalute = target.init(-9000, 0, 0, 0, 0, 0, 1963, 1, 0, 0, 1, 1, 0, 0);
            }
            catch (Exception e)
            {
                if (e.Message.Equals("Message: Cannot handle exeption with name \"N_LES: N_Niveau is not valid\" and type 2"))
                    expetion = true;
            }
            Assert.IsTrue(expetion);
            expetion = false;
            try
            {
                returnvalute = target.init(-1, 0, 0, 0, 0, 0, 1963, 1, 0, 0, 1, 1, 0, 0);
            }
            catch (Exception e)
            {
                if (e.Message.Equals("Message: Cannot handle exeption with name \"N_LES: N_Niveau is not valid\" and type 2"))
                    expetion = true;
            }
            Assert.IsTrue(expetion);
            returnvalute = target.init(0, 0, 0, 0, 0, 0, 1963, 1, 0, 0, 1, 1, 0, 0);
            Assert.AreEqual(0, returnvalute);
           
            returnvalute = target.init(900, 0, 0, 0, 0, 0, 1963, 1, 0, 0, 1, 1, 0, 0);
            Assert.AreEqual(0, returnvalute);
            expetion = false;
            try
            {
                returnvalute =target.init(0,-999, 0, 0, 0, 0, 1963, 1, 0, 0, 1,1, 0, 0);
            }
            catch (Exception e)
            {
                if (e.Message.Equals("Message: Cannot handle exeption with name \"N_LES: N_Spring is not valid\" and type 2"))
                    expetion = true;
            }
            Assert.IsTrue(expetion);
            expetion = false;
            try
            {
                returnvalute = target.init(0, -1, 0, 0, 0, 0, 1963, 1, 0, 0, 1, 1, 0, 0);
            }
            catch (Exception e)
            {
                if (e.Message.Equals("Message: Cannot handle exeption with name \"N_LES: N_Spring is not valid\" and type 2"))
                    expetion = true;
            }
            Assert.IsTrue(expetion);
            returnvalute = target.init(0, 0, 0, 0, 0, 0, 1963, 1, 0, 0, 1, 1, 0, 0);
            Assert.AreEqual(0, returnvalute);

            returnvalute = target.init(0, 100, 0, 0, 0, 0, 1963, 1, 0, 0, 1, 1, 0, 0);
            Assert.AreEqual(0, returnvalute);
            expetion = false;
            try
            {
                returnvalute = target.init(0, 0, -999, 0, 0, 0, 1963, 1, 0, 0, 1, 1, 0, 0);
            }
            catch (Exception e)
            {
                if (e.Message.Equals("Message: Cannot handle exeption with name \"N_LES: N_Fall is not valid\" and type 2"))
                    expetion = true;
            }
            Assert.IsTrue(expetion);
            expetion = false;
            try
            {
                returnvalute = target.init(0, 0, -1, 0, 0, 0, 1963, 1, 0, 0, 1, 1, 0, 0);
            }
            catch (Exception e)
            {
                if (e.Message.Equals("Message: Cannot handle exeption with name \"N_LES: N_Fall is not valid\" and type 2"))
                    expetion = true;
            }
            Assert.IsTrue(expetion);

            returnvalute = target.init(0, 0, 0, 0, 0, 0, 1963, 1, 0, 0, 1, 1, 0, 0);
            Assert.AreEqual(0, returnvalute);

            returnvalute = target.init(0, 0, 999, 0, 0, 0, 1963, 1, 0, 0, 1, 1, 0, 0);
            Assert.AreEqual(0, returnvalute);
            expetion = false;
            try
            {
                returnvalute = target.init(0, 0, 0, -1999, 0, 0, 1963, 1, 0, 0, 1, 1, 0, 0);
            }
            catch (Exception e)
            {
                if (e.Message.Equals("Message: Cannot handle exeption with name \"N_LES: N_Fix is not valid\" and type 2"))
                    expetion = true;
            }
            Assert.IsTrue(expetion);
            expetion = false;
            try
            {
                returnvalute = target.init(0, 0, 0, -1, 0, 0, 1963, 1, 0, 0, 1, 1, 0, 0);
            }
            catch (Exception e)
            {
                if (e.Message.Equals("Message: Cannot handle exeption with name \"N_LES: N_Fix is not valid\" and type 2"))
                    expetion = true;
            }
            Assert.IsTrue(expetion);
            returnvalute = target.init(0, 0, 0, 0, 0, 0, 1963, 1, 0, 0, 1, 1, 0, 0);
            Assert.AreEqual(0, returnvalute);

            returnvalute = target.init(0, 0, 0, 999, 0, 0, 1963, 1, 0, 0, 1, 1, 0, 0);
            Assert.AreEqual(0, returnvalute);
            expetion = false;
            try
            {   
                returnvalute =target.init(0, 0, 0, 0, -1999, 0, 1963, 1, 0, 0, 1, 1, 0, 0);
            }
            catch (Exception e)
            {
                if (e.Message.Equals("Message: Cannot handle exeption with name \"N_LES: N_GrazingManure is not valid\" and type 2"))
                    expetion = true;
            }
            Assert.IsTrue(expetion);
            expetion = false;
            try
            { 
                returnvalute = target.init(0, 0, 0, 0, -1, 0, 1963, 1, 0, 0, 1, 1, 0, 0);
            }
            catch (Exception e)
            {
                if (e.Message.Equals("Message: Cannot handle exeption with name \"N_LES: N_GrazingManure is not valid\" and type 2"))
                    expetion = true;
            }
            Assert.IsTrue(expetion);
            returnvalute = target.init(0, 0, 0, 0, 0, 0, 1963, 1, 0, 0, 1, 1, 0, 0);
            Assert.AreEqual(0, returnvalute);

            returnvalute = target.init(0, 0, 0, 0, 999, 0, 1963, 1, 0, 0, 1, 1, 0, 0);
            Assert.AreEqual(0, returnvalute);
            expetion = false;
            try
            {     
                returnvalute =target.init(0, 0, 0, 0, 0, -900, 1963, 1, 0, 0, 1, 1, 0, 0);
            }
            catch (Exception e)
            {
                if (e.Message.Equals("Message: Cannot handle exeption with name \"N_LES: N_Removed is not valid\" and type 2"))
                    expetion = true;
            }
            Assert.IsTrue(expetion);
            expetion = false;
            try
            {
                returnvalute = target.init(0, 0, 0, 0, 0, -1, 1963, 1, 0, 0, 1, 1, 0, 0);
            }
            catch (Exception e)
            {
                if (e.Message.Equals("Message: Cannot handle exeption with name \"N_LES: N_Removed is not valid\" and type 2"))
                    expetion = true;
            }
            Assert.IsTrue(expetion);
            returnvalute = target.init(0, 0, 0, 0, 0, 0, 1963, 1, 0, 0, 1, 1, 0, 0);
            Assert.AreEqual(0, returnvalute);

            returnvalute = target.init(0, 0, 0, 0, 0, 9000, 1963, 1, 0, 0, 1, 1, 0, 0);
            Assert.AreEqual(0, returnvalute);
            expetion = false;
            try
            {      
                returnvalute =target.init(0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 1, 1, 0, 0);
            }
            catch (Exception e)
            {
                if (e.Message.Equals("Message: Cannot handle exeption with name \"N_LES: Year is not valid\" and type 2"))
                    expetion = true;
            }
            Assert.IsTrue(expetion);
            expetion = false;
            try
            {
                returnvalute = target.init(0, 0, 0, 0, 0, 0, 1962, 1, 0, 0, 1, 1, 0, 0);
            }
            catch (Exception e)
            {
                if (e.Message.Equals("Message: Cannot handle exeption with name \"N_LES: Year is not valid\" and type 2"))
                    expetion = true;
            }
            Assert.IsTrue(expetion);
            returnvalute = target.init(0, 0, 0, 0, 0, 0, 1963, 1, 0, 0, 1, 1, 0, 0);
            Assert.AreEqual(0, returnvalute);
            returnvalute = target.init(0, 0, 0, 0, 0, 0, 2050, 1, 0, 0, 1, 1, 0, 0);
            Assert.AreEqual(0, returnvalute);
            expetion = false;
            try
            {
                returnvalute = target.init(0, 0, 0, 0, 0, 0, 1963, -90, 0, 0, 1, 1, 0, 0);
            }
            catch (Exception e)
            {
                if (e.Message.Equals("Message: Cannot handle exeption with name \"N_LES: SoilType is not valid\" and type 2"))
                    expetion = true;
            }
            Assert.IsTrue(expetion);
            expetion = false;
            try
            {
                returnvalute = target.init(0, 0, 0, 0, 0, 0, 1963, 0, 0, 0, 1, 1, 0, 0);
            }
            catch (Exception e)
            {
                if (e.Message.Equals("Message: Cannot handle exeption with name \"N_LES: SoilType is not valid\" and type 2"))
                    expetion = true;
            }
            Assert.IsTrue(expetion);
            returnvalute = target.init(0, 0, 0, 0, 0, 0, 1963, 1, 0, 0, 1, 1, 0, 0);
            Assert.AreEqual(0, returnvalute);
            returnvalute = target.init(0, 0, 0, 0, 0, 0, 1963, 6, 0, 0, 1, 1, 0, 0);
            Assert.AreEqual(0, returnvalute);

            returnvalute = target.init(0, 0, 0, 0, 0, 0, 1963, 12, 0, 0, 1, 1, 0, 0);
            Assert.AreEqual(0, returnvalute);

            expetion = false;
            try
            {
                returnvalute = target.init(0, 0, 0, 0, 0, 0, 1963, 13, 0, 0, 1, 1, 0, 0);
            }
            catch (Exception e)
            {
                if (e.Message.Equals("Message: Cannot handle exeption with name \"N_LES: SoilType is not valid\" and type 2"))
                    expetion = true;
            }
            Assert.IsTrue(expetion);
            expetion = false;
            try
            {
                returnvalute = target.init(0, 0, 0, 0, 0, 0, 1963, 90, 0, 0, 1, 1, 0, 0);
            }
            catch (Exception e)
            {
                if (e.Message.Equals("Message: Cannot handle exeption with name \"N_LES: SoilType is not valid\" and type 2"))
                    expetion = true;
            }
            Assert.IsTrue(expetion);
     
            expetion = false;
            try
            {
                returnvalute =target.init(0, 0, 0, 0, 0, 0, 1963, 1, -199, 0, 1, 1, 0, 0);
            }
            catch (Exception e)
            {
                if (e.Message.Equals("Message: Cannot handle exeption with name \"N_LES: Humus is not valid\" and type 2"))
                    expetion = true;
            }
            Assert.IsTrue(expetion);


            expetion = false;
            try
            {
                returnvalute = target.init(0, 0, 0, 0, 0, 0, 1963, 1, -1, 0, 1, 1, 0, 0);
            }
            catch (Exception e)
            {
                if (e.Message.Equals("Message: Cannot handle exeption with name \"N_LES: Humus is not valid\" and type 2"))
                    expetion = true;
            }
            Assert.IsTrue(expetion);

            returnvalute = target.init(0, 0, 0, 0, 0, 0, 1963, 1, 0, 0, 1, 1, 0, 0);
            Assert.AreEqual(0, returnvalute);
            returnvalute = target.init(0, 0, 0, 0, 0, 0, 1963, 1, 9000, 0, 1, 1, 0, 0);
            Assert.AreEqual(0, returnvalute);

            expetion = false;
            try
            {
                returnvalute =target.init(0, 0, 0, 0, 0, 0, 1963, 1, 0, -199, 1, 1, 0, 0);
            }
            catch (Exception e)
            {
                if (e.Message.Equals("Message: Cannot handle exeption with name \"N_LES: Clay is not valid\" and type 2"))
                    expetion = true;
            }
            Assert.IsTrue(expetion);

            expetion = false;
            try
            {
                returnvalute = target.init(0, 0, 0, 0, 0, 0, 1963, 1, 0, -1, 1, 1, 0, 0);
            }
            catch (Exception e)
            {
                if (e.Message.Equals("Message: Cannot handle exeption with name \"N_LES: Clay is not valid\" and type 2"))
                    expetion = true;
            }
            Assert.IsTrue(expetion);
            returnvalute = target.init(0, 0, 0, 0, 0, 0, 1963, 1, 0, 0, 1, 1, 0, 0);
            Assert.AreEqual(0, returnvalute);
            returnvalute = target.init(0, 0, 0, 0, 0, 0, 1963, 1, 0, 999, 1, 1, 0, 0);
            Assert.AreEqual(0, returnvalute);

            expetion = false;
            try
            {
                returnvalute =target.init(0, 0, 0, 0, 0, 0, 1963, 1, 0, 0, -9999, 1, 0, 0);
            }
            catch (Exception e)
            {
                if (e.Message.Equals("Message: Cannot handle exeption with name \"N_LES: Run_Off is not valid\" and type 2"))
                    expetion = true;
            }
            Assert.IsTrue(expetion);
            expetion = false;
            try
            {
                returnvalute = target.init(0, 0, 0, 0, 0, 0, 1963, 1, 0, 0, 0, 1, 0, 0);
            }
            catch (Exception e)
            {
                if (e.Message.Equals("Message: Cannot handle exeption with name \"N_LES: Run_Off is not valid\" and type 2"))
                    expetion = true;
            }
            Assert.IsTrue(expetion);

            returnvalute = target.init(0, 0, 0, 0, 0, 0, 1963, 1, 0, 0, 1, 1, 0, 0);
            Assert.AreEqual(0, returnvalute);

            returnvalute = target.init(0, 0, 0, 0, 0, 0, 1963, 1, 0, 0, 9000,1, 0, 0);
            Assert.AreEqual(0, returnvalute);


            expetion = false;
            try
            {
                returnvalute = target.init(0, 0, 0, 0, 0, 0, 1963, 1, 0, 0, 1,-9999, 0, 0);
            }
            catch (Exception e)
            {
                if (e.Message.Equals("Message: Cannot handle exeption with name \"N_LES: Run_Off is not valid\" and type 2"))
                    expetion = true;
            }
            Assert.IsTrue(expetion);
            expetion = false;
            try
            {
                returnvalute = target.init(0, 0, 0, 0, 0, 0, 1963, 1, 0, 0,1, 0, 0, 0);
            }
            catch (Exception e)
            {
                if (e.Message.Equals("Message: Cannot handle exeption with name \"N_LES: Run_Off is not valid\" and type 2"))
                    expetion = true;
            }
            Assert.IsTrue(expetion);

            returnvalute = target.init(0, 0, 0, 0, 0, 0, 1963, 1, 0, 0, 1, 1, 0, 0);
            Assert.AreEqual(0, returnvalute);
            returnvalute = target.init(0, 0, 0, 0, 0, 0, 1963, 1, 0, 0, 1, 999, 0, 0);
            Assert.AreEqual(0, returnvalute);


            returnvalute = target.init(0, 0, 0, 0, 0, 0, 1963, 1, 0, 0, 1, 1, -165.7m, 0);
            Assert.AreEqual(0, returnvalute);
            returnvalute = target.init(0, 0, 0, 0, 0, 0, 1963, 1, 0, 0, 1, 1, -98.6m, 0);
            Assert.AreEqual(0, returnvalute);
            returnvalute = target.init(0, 0, 0, 0, 0, 0, 1963, 1, 0, 0, 1, 1, -42m, 0);
            Assert.AreEqual(0, returnvalute);
            expetion = false;
            try
            {
                returnvalute = target.init(0, 0, 0, 0, 0, 0, 1963, 1, 0, 0, 1, 1, -8, 0);
            }
            catch (Exception e)
            {
                if (e.Message.Equals("Message: Cannot handle exeption with name \"N_LES: CropCoeff is not valid\" and type 2"))
                    expetion = true;
            }
            Assert.IsTrue(expetion);



            returnvalute = target.init(0, 0, 0, 0, 0, 0, 1963, 1, 0, 0, 1, 1, -7.6m, 0);
            Assert.AreEqual(0, returnvalute);
            expetion = false;
            try
            {
                returnvalute = target.init(0, 0, 0, 0, 0, 0, 1963, 1, 0, 0, 1, 1, -2, 0);
            }
            catch (Exception e)
            {
                if (e.Message.Equals("Message: Cannot handle exeption with name \"N_LES: CropCoeff is not valid\" and type 2"))
                    expetion = true;
            }
            Assert.IsTrue(expetion);

            returnvalute = target.init(0, 0, 0, 0, 0, 0, 1963, 1, 0, 0, 1, 1, 0, 0);
            Assert.AreEqual(0, returnvalute);
            expetion = false;
            try
            {
                returnvalute = target.init(0, 0, 0, 0, 0, 0, 1963, 1, 0, 0, 1, 1, 5, 0);
            }
            catch (Exception e)
            {
                if (e.Message.Equals("Message: Cannot handle exeption with name \"N_LES: CropCoeff is not valid\" and type 2"))
                    expetion = true;
            }
            Assert.IsTrue(expetion);
            returnvalute = target.init(0, 0, 0, 0, 0, 0, 1963, 1, 0, 0, 1, 1, 28.8m, 0);
            Assert.AreEqual(0, returnvalute);
            expetion = false;
            try
            {
                returnvalute = target.init(0, 0, 0, 0, 0, 0, 1963, 1, 0, 0, 1, 1, 30, 0);
            }
            catch (Exception e)
            {
                if (e.Message.Equals("Message: Cannot handle exeption with name \"N_LES: CropCoeff is not valid\" and type 2"))
                    expetion = true;
            }
            Assert.IsTrue(expetion);
            expetion = false;
            try
            {
                returnvalute = target.init(0, 0, 0, 0, 0, 0, 1963, 1, 0, 0, 1, 1, 0, 900);
            }
            catch (Exception e)
            {
                if (e.Message.Equals("Message: Cannot handle exeption with name \"N_LES: PreCropCoeff is not valid\" and type 2"))
                    expetion = true;
            }
            Assert.IsTrue(expetion);
            expetion = false;
            try
            {
                returnvalute = target.init(0, 0, 0, 0, 0, 0, 1963, 1, 0, 0, 1, 1, 0, 34.8m);
            }
            catch (Exception e)
            {
                if (e.Message.Equals("Message: Cannot handle exeption with name \"N_LES: PreCropCoeff is not valid\" and type 2"))
                    expetion = true;
            }
            Assert.IsTrue(expetion);
            
            returnvalute = target.init(0, 0, 0, 0, 0, 0, 1963, 1, 0, 0, 1, 1, 0, 34.7m);
            Assert.AreEqual(0, returnvalute);
            expetion = false;
            try
            {
                returnvalute = target.init(0, 0, 0, 0, 0, 0, 1963, 1, 0, 0, 1, 1, 0, 34.6m);
            }
            catch (Exception e)
            {
                if (e.Message.Equals("Message: Cannot handle exeption with name \"N_LES: PreCropCoeff is not valid\" and type 2"))
                    expetion = true;
            }
            Assert.IsTrue(expetion);
            expetion = false;
            try
            {
                returnvalute = target.init(0, 0, 0, 0, 0, 0, 1963, 1, 0, 0, 1, 1, 0, 17);
            }
            catch (Exception e)
            {
                if (e.Message.Equals("Message: Cannot handle exeption with name \"N_LES: PreCropCoeff is not valid\" and type 2"))
                    expetion = true;
            }
            Assert.IsTrue(expetion);
            expetion = false;
            try
            {
                returnvalute = target.init(0, 0, 0, 0, 0, 0, 1963, 1, 0, 0, 1, 1, 0, 14.3m);
            }
            catch (Exception e)
            {
                if (e.Message.Equals("Message: Cannot handle exeption with name \"N_LES: PreCropCoeff is not valid\" and type 2"))
                    expetion = true;
            }
            Assert.IsTrue(expetion);

            returnvalute = target.init(0, 0, 0, 0, 0, 0, 1963, 1, 0, 0, 1, 1, 0, 14.2m);
            Assert.AreEqual(0, returnvalute);

            expetion = false;
            try
            {
                returnvalute = target.init(0, 0, 0, 0, 0, 0, 1963, 1, 0, 0, 1, 1, 0, 14.1m);
            }
            catch (Exception e)
            {
                if (e.Message.Equals("Message: Cannot handle exeption with name \"N_LES: PreCropCoeff is not valid\" and type 2"))
                    expetion = true;
            }
            Assert.IsTrue(expetion);
            expetion = false;
            try
            {
                returnvalute = target.init(0, 0, 0, 0, 0, 0, 1963, 1, 0, 0, 1, 1, 0, 7);
            }
            catch (Exception e)
            {
                if (e.Message.Equals("Message: Cannot handle exeption with name \"N_LES: PreCropCoeff is not valid\" and type 2"))
                    expetion = true;
            }
            Assert.IsTrue(expetion);

            expetion = false;
            try
            {
                returnvalute = target.init(0, 0, 0, 0, 0, 0, 1963, 1, 0, 0, 1, 1, 0, 0.1m);
            }
            catch (Exception e)
            {
                if (e.Message.Equals("Message: Cannot handle exeption with name \"N_LES: PreCropCoeff is not valid\" and type 2"))
                    expetion = true;
            }
            Assert.IsTrue(expetion);
            returnvalute = target.init(0, 0, 0, 0, 0, 0, 1963, 1, 0, 0, 1, 1, 0, 0);
            Assert.AreEqual(0, returnvalute);
            expetion = false;
            try
            {
                returnvalute = target.init(0, 0, 0, 0, 0, 0, 1963, 1, 0, 0, 1, 1, 0, -0.1m);
            }
            catch (Exception e)
            {
                if (e.Message.Equals("Message: Cannot handle exeption with name \"N_LES: PreCropCoeff is not valid\" and type 2"))
                    expetion = true;
            }
            Assert.IsTrue(expetion);
            expetion = false;
            try
            {
                returnvalute = target.init(0, 0, 0, 0, 0, 0, 1963, 1, 0, 0, 1, 1, 0, -10m);
            }
            catch (Exception e)
            {
                if (e.Message.Equals("Message: Cannot handle exeption with name \"N_LES: PreCropCoeff is not valid\" and type 2"))
                    expetion = true;
            }
            Assert.IsTrue(expetion);
            expetion = false;
            try
            {
                returnvalute = target.init(0, 0, 0, 0, 0, 0, 1963, 1, 0, 0, 1, 1, 0, -38.4m);
            }
            catch (Exception e)
            {
                if (e.Message.Equals("Message: Cannot handle exeption with name \"N_LES: PreCropCoeff is not valid\" and type 2"))
                    expetion = true;
            }
            Assert.IsTrue(expetion);
            returnvalute = target.init(0, 0, 0, 0, 0, 0, 1963, 1, 0, 0, 1, 1, 0,-38.5m);
            Assert.AreEqual(0, returnvalute);
            expetion = false;
            try
            {
                returnvalute = target.init(0, 0, 0, 0, 0, 0, 1963, 1, 0, 0, 1, 1, 0, -38.6m);
            }
            catch (Exception e)
            {
                if (e.Message.Equals("Message: Cannot handle exeption with name \"N_LES: PreCropCoeff is not valid\" and type 2"))
                    expetion = true;
            }
            Assert.IsTrue(expetion);
            expetion = false;
            try
            {
                returnvalute = target.init(0, 0, 0, 0, 0, 0, 1963, 1, 0, 0, 1, 1, 0, 40);
            }
            catch (Exception e)
            {
                if (e.Message.Equals("Message: Cannot handle exeption with name \"N_LES: PreCropCoeff is not valid\" and type 2"))
                    expetion = true;
            }
            Assert.IsTrue(expetion);
          
        }

        /// <summary>
        ///A test for getNLesMgPrL and getNLesKgNPrHa is correct when the lowest amount of everything is used
        ///</summary>
        [TestMethod()]
        public void getNLesBasicInputZero()
        {
            N_LES target = new N_LES();
            int errorcode=target.init(0, 0, 0, 0, 0, 0, 1963, 1, 0, 0, 1, 1, -165.7m, -38.5m);
            Assert.AreEqual(errorcode, 0);
            Decimal expectedHigh = (Decimal)5.635264457;
            Decimal expectedLow = (Decimal)5.635264456;
            Decimal actual = target.getNLesKgNPrHa();
            if (expectedHigh < actual || expectedLow > actual)
                Assert.Fail();
            expectedHigh = (Decimal)2496.422155;
            expectedLow = (Decimal)2496.422154;
            actual = target.getNLesMgPrL();
            if (expectedHigh < actual || expectedLow > actual)
                Assert.Fail();

        }


        /// <summary>
        ///A test for getNLesMgPrL and getNLesKgNPrHa is correct when small amount of everything is used
        ///</summary>
        [TestMethod()]
        public void getNLesBasicInputOne()
        {
            N_LES target = new N_LES();
            target.init(1, 1, 1, 1, 1, 1, 1963, 1, 1, 1, 1, 1, 28.8m, 14.2m);
            Decimal expectedHigh = (Decimal)5.242925834;
            Decimal expectedLow = (Decimal)5.242925833;
            Decimal actual;

            actual = target.getNLesKgNPrHa();
            if(expectedHigh<actual||expectedLow>actual)
                Assert.Fail();
            expectedHigh = (Decimal)2322.616145;
            expectedLow = (Decimal)2322.616144;
            actual = target.getNLesMgPrL();
            if (expectedHigh < actual || expectedLow > actual)
                Assert.Fail();
        }


        /// <summary>
        ///A test for getNLesKgNPrHa and getNLesKgNPrHa 
        ///</summary>
        [TestMethod()]
        public void getNLesBasicInputTwo()
        {
            N_LES target = new N_LES();
            target.init(2, 2, 2, 2, 2, 2, 1963, 2, 2, 2, 2, 2, 28.8m, 14.2m);
            Decimal expectedHigh = (Decimal)9.129335463;
            Decimal expectedLow = (Decimal)9.129335462;
            Decimal actual;

            actual = target.getNLesKgNPrHa();
            if (expectedHigh < actual || expectedLow > actual)
                Assert.Fail();

            expectedHigh = (Decimal)2022.147805;
            expectedLow = (Decimal)2022.147804;
            actual = target.getNLesMgPrL();
            if (expectedHigh < actual || expectedLow > actual)
                Assert.Fail();
        }



        /// <summary>
        /// A test for getNLesKgNPrHa and getNLesKgNPrHa 
        ///</summary>

        [TestMethod()]
        public void getNLesBasicInputTree()
        {
            N_LES target = new N_LES();
            target.init(3, 3, 3, 3, 3, 3, 1963, 3, 3, 3, 3, 3, 28.8m, 14.2m);
            Decimal expectedHigh = (Decimal)11.92260595;
            Decimal expectedLow = (Decimal)11.92260594;
            Decimal actual;

            actual = target.getNLesKgNPrHa();

            if (expectedHigh < actual || expectedLow > actual)
               Assert.Fail();

            expectedHigh = (Decimal)1760.571479;
            expectedLow = (Decimal)1760.571478;
            actual = target.getNLesMgPrL();

            // Assert.AreEqual(actual, expectedHigh);
            if (expectedHigh < actual || expectedLow > actual)
                Assert.Fail();
        }

     
        /// <summary>
        ///A test for getNLesKgNPrHa and getNLesKgNPrHa when SoilType is above 4
        ///</summary>
        [TestMethod()]
        public void getNLesBasicInputFive()
        {
            N_LES target = new N_LES();
            target.init(5, 5, 5, 5, 5, 5, 1963, 5, 5, 5, 5, 5, 28.8m, 14.2m);
            Decimal expectedHigh = (Decimal)15.01900124;
            Decimal expectedLow = (Decimal)15.01900123;
            Decimal actual;

            actual = target.getNLesKgNPrHa();

            if (expectedHigh < actual || expectedLow > actual)
                Assert.Fail();

            expectedHigh = (Decimal)1330.68351;
            expectedLow = (Decimal)1330.683509;
            actual = target.getNLesMgPrL();
            if (expectedHigh < actual || expectedLow > actual)
                Assert.Fail();
        }


        /// <summary>
        ///A test for getNLesKgNPrHa and getNLesKgNPrHa when T (and internarl variable) is negative
        ///</summary>
        [TestMethod()]
        public void getNLesWhereTIsNegative()
        {
            N_LES target = new N_LES();
            int t = target.init(1, 1, 1, 1, 1, 1, 1963, 5, 1, 1, 1, 1,0,-38.5m);
            Assert.AreEqual(0, t);
            Decimal expectedHigh = (Decimal)5.052917410;
            Decimal expectedLow = (Decimal)5.052917409;
            Decimal actual = target.getNLesKgNPrHa();
         //   Assert.AreEqual(expectedLow, actual);
            if (expectedHigh < actual || expectedLow > actual)
                Assert.Fail();

            expectedHigh = (Decimal)2238.442413;
            expectedLow = (Decimal)2238.442412;
            actual = target.getNLesMgPrL();


            if (expectedHigh < actual || expectedLow > actual)
                Assert.Fail();
        }
        /// <summary>
        ///A test for getNLesKgNPrHa and getNLesKgNPrHa when T (and internarl variable) are small but posetiv (need to be between 0.001 and 0)
        ///</summary>
        [TestMethod()]
        public void getNLesWhereTIsPosetivAndSmall()
        {
            N_LES target = new N_LES();
            target.init(1, 1, 574.136m, 1, 1, 1, 1963, 5, 1, 1, 1, 1, -165.7m, -38.5m); 
            Decimal expectedHigh = (Decimal)5.085914731;
            Decimal expectedLow = (Decimal)5.085914730;
            Decimal actual = target.getNLesKgNPrHa();

            //Assert.AreEqual(actual, expectedHigh);
            if (expectedHigh < actual || expectedLow > actual)
                Assert.Fail();

            expectedHigh = (Decimal)2253.060226;
            expectedLow = (Decimal)2253.060225;
            actual = target.getNLesMgPrL();


            if (expectedHigh < actual || expectedLow > actual)
                Assert.Fail();
        }
        /// <summary>
        ///A test for getNLesKgNPrHa and getNLesKgNPrHa where the calculated U will be below 0 therefore changed
        ///</summary>
        [TestMethod()]
        public void getNLesWhereTIsPosetivAndSmall2()
        {
            N_LES target = new N_LES();
            target.init(0, 0, 0, 0, 0, 0, 2010, 1, 0, 0, 1, 1, (Decimal)(-165.7), (Decimal)(-38.5));
            
            Decimal expectedHigh = (Decimal)0.000000469221;
            Decimal expectedLow = (Decimal)0.000000469220;
            Decimal actual = target.getNLesKgNPrHa();
            if (expectedHigh <= actual || expectedLow >= actual)
                Assert.Fail();

            expectedHigh = (Decimal)0.000207865;
            expectedLow = (Decimal)0.000207864;
            actual = target.getNLesMgPrL();


            if (expectedHigh < actual || expectedLow > actual)
                Assert.Fail();
        }
     
    }
}
