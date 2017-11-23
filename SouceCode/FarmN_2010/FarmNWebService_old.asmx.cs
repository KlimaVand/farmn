using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Linq;
using System.Data.SqlClient;
using System.Net;
using Mosel;
using Husdyrgodkendelse;
using Husdyrgodkendelse.Goedning;
using System.Configuration;
using System.Collections.Generic;
using System.Threading;

namespace FarmN_2010
{
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class FarmNWebService : System.Web.Services.WebService
    {
        private AppSettingsReader cfg = null;

        private string sqlconnstr = "";

        public FarmNWebService()
        {

            cfg = new AppSettingsReader();

            sqlconnstr = cfg.GetValue("sqlConnectionStringTest", typeof(string)).ToString();
        }

        [WebMethod]
        public bool TestConnection()
        {

            bool OK = true;
            return OK;
        }


        [WebMethod(Description = "Denne metode beregner FarmN-udvaskning som Kg N/ha og som mg NO<sub>3</sub>/l")]
        public List<FarmNData> BeregnNudriftNy(List<Areal> arealer, List<Husdyrgodkendelse.Goedning.Goedningsmaengde> goedningsmaengder, long bedriftID, string zipCode, bool option1, bool option2)
        {
            FarmNData foulumData = new FarmNData();
            List<FarmNData> datalist = new List<FarmNData>();
            if (zipCode == null) { throw new ArgumentException("Bedriftens postnummer er ugyldigt"); }
            else
            {
                if (zipCode == "") { throw new ArgumentException("Bedriftens postnummer er ugyldigt"); }
                else
                {
                    if (IsZipCodeValid(Convert.ToInt32(zipCode)))
                    {
                        long aNewFarmNumber = 0;
                        this.createVVMFarm(Convert.ToString(bedriftID), out aNewFarmNumber);
                        this.vvmUpdateFarm_ZipCode(aNewFarmNumber, zipCode);
                        int scenario = 1;
                        DateTime yearnow = System.DateTime.Today;
                        //        int cropyear = yearnow.Year;
                        int cropyear = 2009;
                        if (option1 == true)
                        {
                            cropyear = 2007;
                        }
                        this.UpdateScenarioCropYear(aNewFarmNumber, scenario, cropyear);

                        bool manurecheck = false;
                        foreach (Goedningsmaengde g in goedningsmaengder)
                        {
                            this.incrementBoughtManure(aNewFarmNumber, scenario, g.KgN, g.Goedningstype.Navn, g.GoedningstypeNUdnyttelsesProcent);
                            manurecheck = true;
                        }
                        int rotation = 0;
                        int FarmType = 2;

                        foreach (Areal a in arealer)
                        {
                            if (a.ArealType == ArealTyper.Udbringning)
                            {
                                rotation++;
                                // Vi laver to checks, for at kunne differenciere på beskrivelsen af fejlen.
                                //if (String.IsNullOrEmpty(a.Referencesaedskifte))
                                //    throw new ArgumentException("Referencesædskiftet er null eller ikke angivet.");
                                //if (String.IsNullOrEmpty(a.Jordbundstype))
                                //    throw new ArgumentException("Jordbundstypen er null eller ikke angivet.");
                                if (option1 == true)
                                {
                                    this.copyStandardRotation(a.Referencesaedskifte, aNewFarmNumber, a.Jordbundstype, a.Ha, 1);
                                }
                                else
                                {
                                    this.copyStandardRotation(a.Referencesaedskifte, aNewFarmNumber, a.Jordbundstype, a.Ha, 2);
                                }
                                if (rotation == 1)
                                {
                                    if (manurecheck == true)
                                    {
                                        FarmType = 2;
                                        if (a.Referencesaedskifte.Substring(0, 1).ToString() == "K") { FarmType = 3; }
                                        this.vvmUpdateFarm_FarmType(aNewFarmNumber, FarmType);
                                    }
                                }
                            }
                        }

                        //Thread.Sleep(10000);
                        if (option1 == true)
                        {
                            this.startASPPage(Convert.ToString("http://130.226.173.223/FarmNTest_original/CalculateResult.asp?version=1&farmNumber=" + Convert.ToString(aNewFarmNumber) + "&scenarioID=" + Convert.ToString(scenario) + "&password=" + Convert.ToString(aNewFarmNumber) + ""));
                        }
                        else
                        {
                            if (option2 == true)
                            {
                                this.startASPPage(Convert.ToString("http://130.226.173.223/FarmNTest_original/CalculateResult.asp?version=1&farmNumber=" + Convert.ToString(aNewFarmNumber) + "&scenarioID=" + Convert.ToString(scenario) + "&password=" + Convert.ToString(aNewFarmNumber) + ""));
                            }
                            else
                            {
                                //this.insertTest("9999_" + Convert.ToString("http://130.226.173.223/FarmNTest_original/CalculateResult.asp?version=2&farmNumber=" + Convert.ToString(aNewFarmNumber) + "&scenarioID=" + Convert.ToString(scenario) + "&password=" + Convert.ToString(aNewFarmNumber) + "") + "");    
                                this.startASPPage(Convert.ToString("http://130.226.173.223/FarmNTest_original/CalculateResult.asp?version=2&farmNumber=" + Convert.ToString(aNewFarmNumber) + "&scenarioID=" + Convert.ToString(scenario) + "&password=" + Convert.ToString(aNewFarmNumber) + ""));
                            }
                        }
                        decimal NLeach_Kg_ha = -1;
                        decimal NLeach_mg_l = -1;
                        decimal NLeach_Kg_ha_total = -1;
                        decimal NLeach_mg_l_total = -1;
                        rotation = 0;
                        foreach (Areal a in arealer)
                        {
                            if (a.ArealType == ArealTyper.Udbringning)
                            {
                                foulumData.Referencesaedskifte = a.Referencesaedskifte;
                                foulumData.Saedskifte = a.Saedskifte;
                                foulumData.Jordbundstype = a.Jordbundstype;
                                foulumData.Arealtype = (int)a.ArealType;
                                rotation++;
                                this.calculateNLesValues(aNewFarmNumber, scenario, rotation);
                                this.calculateNLeachFarm_Rotation(aNewFarmNumber, scenario, rotation, out NLeach_Kg_ha, out NLeach_mg_l, out NLeach_Kg_ha_total, out NLeach_mg_l_total);
                                foulumData.NLeach_KgN_ha = NLeach_Kg_ha;//hent KgN/ha data for rotation
                                foulumData.NLeach_mgN_l = NLeach_mg_l;//hent mg/l data for rotation
                                foulumData.NLeach_KgN_ha_total = NLeach_Kg_ha_total;//hent KgN/ha data for hele bedriften
                                foulumData.NLeach_mgN_l_total = NLeach_mg_l_total;//hent mg/l data for hele bedriften
                                datalist.Add(foulumData);
                            }
                        }


                        return datalist;
                    }
                    else
                    {
                        throw new ArgumentException("Bedriftens postnummer er ugyldigt");
                    }
                }
            }
        }


        [WebMethod(Description = "Denne metode beregner FarmN-udvaskning som Kg N/ha og som mg NO<sub>3</sub>/l")]
        public List<FarmNData> BeregnAnsoegtNy(List<Areal> arealer, List<Husdyrgodkendelse.Goedning.Goedningsmaengde> goedningsmaengder, long bedriftID, string zipCode, decimal afterCropPercent, decimal aNormPercent, bool option1, bool option2)
        {
            FarmNData foulumData = new FarmNData();
            List<FarmNData> datalist = new List<FarmNData>();
            long aNewFarmNumber = 0;
            if (zipCode == null) { throw new ArgumentException("Bedriftens postnummer er ugyldigt"); }
            else
            {
                if (IsZipCodeValid(Convert.ToInt32(zipCode)))
                {

                    this.checkVVMFarm(Convert.ToString(bedriftID), out aNewFarmNumber);
                    if (aNewFarmNumber == 0)
                    {
                        this.createVVMFarm(Convert.ToString(bedriftID), out aNewFarmNumber);
                    }
                    this.vvmUpdateFarm_ZipCode(aNewFarmNumber, zipCode);
                    int scenario = 1;
                    this.createScenario(aNewFarmNumber, out scenario);
                    DateTime yearnow = System.DateTime.Today;
                    //        int cropyear = yearnow.Year;
                    int cropyear = 2009;
                    if (option1 == true)
                    {
                        cropyear = 2007;
                    }
                    this.UpdateScenarioCropYear(aNewFarmNumber, scenario, cropyear);

                    foreach (Goedningsmaengde g in goedningsmaengder)
                    {
                        this.incrementBoughtManure(aNewFarmNumber, scenario, g.KgN, g.Goedningstype.Navn, g.GoedningstypeNUdnyttelsesProcent);
                    }
                    int rotation = 0;

                    foreach (Areal a in arealer)
                    {
                        if (a.ArealType == ArealTyper.Udbringning)
                        {
                            rotation = rotation + 1;
                            if (option1 == true)
                            {
                                this.copyStandardRotation(a.Saedskifte, aNewFarmNumber, a.Jordbundstype, 100m, 1);
                            }
                            else
                            {
                                this.copyStandardRotation(a.Saedskifte, aNewFarmNumber, a.Jordbundstype, 100m, 2);
                            }
                            //this.copyStandardRotation(a.Saedskifte, aNewFarmNumber, a.Jordbundstype, 100m);
                            if (afterCropPercent != 0)
                            {
                                this.CalculateAfterCrop(aNewFarmNumber, scenario, rotation, afterCropPercent);
                                this.RotXPress(aNewFarmNumber, scenario, rotation);
                            }
                            //her skal arealet ganges på FieldPlan og FieldPlanRotation records this.MultiplyRotationAreas(aNewFarmNumber, scenario, rotation,a.Ha)
                            this.MultiplyRotationAreas(aNewFarmNumber, scenario, rotation, a.Ha);
                            //int FarmType = 2;
                            //if (a.Referencesaedskifte.Substring(0, 1).ToString() == "K") { FarmType = 3; }
                            //this.vvmUpdateFarm_FarmType(aNewFarmNumber, FarmType);

                        }
                    }
                    aNormPercent = 100.0m - aNormPercent;
                    //**************************************************
                    //string str = Convert.ToString("http://130.226.173.223/FarmNTest/CalculateResult.asp?farmNumber=" + Convert.ToString(aNewFarmNumber) + "&scenarioID=" + Convert.ToString(scenario) + "&password=" + Convert.ToString(aNewFarmNumber) + "&normpercent=" + Convert.ToString(aNormPercent) + "");
                    //this.insertTest(str.ToString(), (str.GetType().ToString()));
                    //this.startASPPage(Convert.ToString("http://172.23.173.223/FarmNTest/CalculateResult.asp?farmNumber=" + Convert.ToString(aNewFarmNumber) + "&scenarioID=" + Convert.ToString(scenario) + "&password=" + Convert.ToString(aNewFarmNumber) + "&normpercent=" + Convert.ToString(aNormPercent) + ""));
                    if (option1 == true)
                    {
                        this.startASPPage(Convert.ToString("http://130.226.173.223/FarmNTest_original/CalculateResult.asp?version=1&farmNumber=" + Convert.ToString(aNewFarmNumber) + "&scenarioID=" + Convert.ToString(scenario) + "&password=" + Convert.ToString(aNewFarmNumber) + "&normpercent=" + Convert.ToString(aNormPercent) + ""));
                    }
                    else
                    {
                        if (option2 == true)
                        {
                            this.startASPPage(Convert.ToString("http://130.226.173.223/FarmNTest_original/CalculateResult.asp?version=1&farmNumber=" + Convert.ToString(aNewFarmNumber) + "&scenarioID=" + Convert.ToString(scenario) + "&password=" + Convert.ToString(aNewFarmNumber) + "&normpercent=" + Convert.ToString(aNormPercent) + ""));
                        }
                        else
                        {
                            this.startASPPage(Convert.ToString("http://130.226.173.223/FarmNTest_original/CalculateResult.asp?version=2&farmNumber=" + Convert.ToString(aNewFarmNumber) + "&scenarioID=" + Convert.ToString(scenario) + "&password=" + Convert.ToString(aNewFarmNumber) + "&normpercent=" + Convert.ToString(aNormPercent) + ""));
                        }
                    }
                    //this.startASPPage(Convert.ToString("http://172.23.0.211/FarmNTest/CalculateResult.asp?version=2&farmNumber=" + Convert.ToString(aNewFarmNumber) + "&scenarioID=2&password=" + Convert.ToString(aNewFarmNumber) + "&normpercent=" + Convert.ToString(aNormPercent) + ""));

                    //this.startASPPage(""+str+"");

                    ////**************************************************
                    //string str = Convert.ToString("http://130.226.173.223/FarmNTest/CalculateResult.asp?farmNumber=" + Convert.ToString(aNewFarmNumber) + "&scenarioID=" + Convert.ToString(scenario) + "&password=" + Convert.ToString(aNewFarmNumber) + "&normpercent=" + Convert.ToString(aNormPercent) + "");
                    //this.insertTest(str, (aNormPercent.GetType().ToString()));
                    ////this.startASPPage(Convert.ToString("http://130.226.173.223/FarmNTest_original/CalculateResult.asp?farmNumber=" + Convert.ToString(aNewFarmNumber) + "&scenarioID=" + Convert.ToString(scenario) + "&password=" + Convert.ToString(aNewFarmNumber) + "&normpercent=" + Convert.ToString(aNormPercent) + ""));
                    //this.startASPPage(str);

                    DateTime timestamp = System.DateTime.Now;
                    rotation = 0;
                    decimal NLeach_Kg_ha = -1;
                    decimal NLeach_mg_l = -1;
                    decimal NLeach_Kg_ha_total = -1;
                    decimal NLeach_mg_l_total = -1;
                    foreach (Areal a in arealer)
                    {
                        if (a.ArealType == ArealTyper.Udbringning)
                        {
                            foulumData.Referencesaedskifte = a.Referencesaedskifte;
                            foulumData.Saedskifte = a.Saedskifte;
                            foulumData.Jordbundstype = a.Jordbundstype;
                            foulumData.Arealtype = (int)a.ArealType;
                            rotation = rotation + 1;
                            this.calculateNLesValues(aNewFarmNumber, scenario, rotation);
                            this.calculateNLeachFarm_Rotation(aNewFarmNumber, scenario, rotation, out NLeach_Kg_ha, out NLeach_mg_l, out NLeach_Kg_ha_total, out NLeach_mg_l_total);
                            foulumData.NLeach_KgN_ha = NLeach_Kg_ha;//hent KgN/ha data for rotation
                            foulumData.NLeach_mgN_l = NLeach_mg_l;//hent mg/l data for rotation
                            foulumData.NLeach_KgN_ha_total = NLeach_Kg_ha_total;//hent KgN/ha data for hele bedriften
                            foulumData.NLeach_mgN_l_total = NLeach_mg_l_total;//hent mg/l data for hele bedriften
                            //this.insertTest(timestamp.ToString() + " " + bedriftID.ToString() + "rot" + rotation, NLeach_Kg_ha_total.ToString());
                            datalist.Add(foulumData);
                        }
                    }

                    //this.startASPPage(Convert.ToString("http://172.23.173.223/FarmNTest/CalculateResult.asp?farmNumber=" + Convert.ToString(aNewFarmNumber) + "&scenarioID=" + Convert.ToString(scenario) + "&password=" + Convert.ToString(aNewFarmNumber) + "&normpercent=" + Convert.ToString(aNormPercent) + ""));
                    return datalist;
                }
                else
                {
                    throw new ArgumentException("Bedriftens postnummer er ugyldigt");
                }
            }
        }

        //[WebMethod(Description = "Denne metode kalder ASP-siden 'Calculateresult.asp'")]
        //public void TestCallASPPage(long FarmNumber, int scenario, decimal aNormPercent)
        //{
        //    aNormPercent = 100.0m - aNormPercent;
        //    //**************************************************
        //    this.startASPPage(Convert.ToString("http://172.23.173.223/FarmNTest/CalculateResult.asp?farmNumber=" + Convert.ToString(FarmNumber) + "&scenarioID=" + Convert.ToString(scenario) + "&password=" + Convert.ToString(FarmNumber) + "&normpercent=" + Convert.ToString(aNormPercent) + ""));

        //}
        [WebMethod(Description = "test")]
        public void TestCallASPPage(long FarmNumber, int scenario)
        {
            //aNormPercent = 100.0m - aNormPercent;
            //**************************************************
            string str = Convert.ToString("http://172.23.173.223/FarmNTest/CalculateResult.asp?version=2&farmNumber=" + Convert.ToString(FarmNumber) + "&scenarioID=" + Convert.ToString(scenario) + "&password=" + Convert.ToString(FarmNumber) + "&normpercent=100");
            this.startASPPage(str);
            //this.startASPPage(Convert.ToString("http://172.23.173.223/FarmNTest/CalculateResult.asp?farmNumber=" + Convert.ToString(FarmNumber) + "&scenarioID=" + Convert.ToString(scenario) + "&password=" + Convert.ToString(FarmNumber) + "&normpercent=100"));

        }

        [WebMethod(Description = "Denne metode beregner FarmN-udvaskning som Kg N/ha og som mg NO<sub>3</sub>/l")]
        public List<FarmNData> Pakke3Ny(List<Areal> arealer, List<Husdyrgodkendelse.Goedning.Goedningsmaengde> goedningsmaengder, long bedriftID, string zipCode, decimal DEmax_DEreel, bool option1, bool option2)
        {
            FarmNData foulumData = new FarmNData();
            List<FarmNData> datalist = new List<FarmNData>();
            long aNewFarmNumber = 0;
            if (zipCode == null) { throw new ArgumentException("Bedriftens postnummer er ugyldigt"); }
            else
            {
                if (IsZipCodeValid(Convert.ToInt32(zipCode)))
                {

                    this.checkVVMFarm(Convert.ToString(bedriftID), out aNewFarmNumber);
                    if (aNewFarmNumber == 0)
                    { this.createVVMFarm(Convert.ToString(bedriftID), out aNewFarmNumber); }
                    this.vvmUpdateFarm_ZipCode(aNewFarmNumber, zipCode);
                    int scenario = 1;
                    this.createScenario(aNewFarmNumber, out scenario);
                    DateTime yearnow = System.DateTime.Today;
                    //        int cropyear = yearnow.Year;
                    int cropyear = 2009;
                    if (option1 == true)
                    {
                        cropyear = 2007;
                    }
                    this.UpdateScenarioCropYear(aNewFarmNumber, scenario, cropyear);

                    foreach (Goedningsmaengde g in goedningsmaengder)
                    {
                        this.incrementBoughtManure(aNewFarmNumber, scenario, DEmax_DEreel * g.KgN, g.Goedningstype.Navn, g.GoedningstypeNUdnyttelsesProcent);
                    }
                    int rotation = 0;
//                    int FarmType = 2;

                    foreach (Areal a in arealer)
                    {
                        if (a.ArealType == ArealTyper.Udbringning)
                        {
                            rotation++;
                            if (option1 == true)
                            {
                                this.copyStandardRotation(a.Referencesaedskifte, aNewFarmNumber, a.Jordbundstype, a.Ha, 1);
                            }
                            else
                            {
                                this.copyStandardRotation(a.Referencesaedskifte, aNewFarmNumber, a.Jordbundstype, a.Ha, 2);
                            }
                            //this.copyStandardRotation(a.Referencesaedskifte, aNewFarmNumber, a.Jordbundstype, a.Ha);
                            //if (rotation == 1)
                            //{
                            //    FarmType = 2;
                            //    if (a.Referencesaedskifte.Substring(0, 1) == "K") { FarmType = 3; }
                            //    this.vvmUpdateFarm_FarmType(aNewFarmNumber, FarmType);
                            //}
                        }
                    }

                    if (option1 == true)
                    {
                        this.startASPPage(Convert.ToString("http://130.226.173.223/FarmNTest_original/CalculateResult.asp?version=1&farmNumber=" + Convert.ToString(aNewFarmNumber) + "&scenarioID=" + Convert.ToString(scenario) + "&password=" + Convert.ToString(aNewFarmNumber) + ""));
                    }
                    else
                    {
                        if (option2 == true)
                        {
                            this.startASPPage(Convert.ToString("http://130.226.173.223/FarmNTest_original/CalculateResult.asp?version=1&farmNumber=" + Convert.ToString(aNewFarmNumber) + "&scenarioID=" + Convert.ToString(scenario) + "&password=" + Convert.ToString(aNewFarmNumber) + ""));
                        }
                        else
                        {
                            this.startASPPage(Convert.ToString("http://130.226.173.223/FarmNTest_original/CalculateResult.asp?version=2&farmNumber=" + Convert.ToString(aNewFarmNumber) + "&scenarioID=" + Convert.ToString(scenario) + "&password=" + Convert.ToString(aNewFarmNumber) + ""));
                        }
                    }
                    //                this.startASPPage(Convert.ToString("http://130.226.173.223/FarmNTest_original/CalculateResult.asp?version=2&farmNumber=" + Convert.ToString(aNewFarmNumber) + "&scenarioID=" + Convert.ToString(scenario) + "&password=" + Convert.ToString(aNewFarmNumber) + ""));
                    decimal NLeach_Kg_ha = -1;
                    decimal NLeach_mg_l = -1;
                    decimal NLeach_Kg_ha_total = -1;
                    decimal NLeach_mg_l_total = -1;
                    rotation = 0;
                    foreach (Areal a in arealer)
                    {
                        if (a.ArealType == ArealTyper.Udbringning)
                        {
                            foulumData.Referencesaedskifte = a.Referencesaedskifte;
                            foulumData.Saedskifte = a.Saedskifte;
                            foulumData.Jordbundstype = a.Jordbundstype;
                            foulumData.Arealtype = (int)a.ArealType;
                            rotation++;
                            this.calculateNLesValues(aNewFarmNumber, scenario, rotation);
                            this.calculateNLeachFarm_Rotation(aNewFarmNumber, scenario, rotation, out NLeach_Kg_ha, out NLeach_mg_l, out NLeach_Kg_ha_total, out NLeach_mg_l_total);
                            foulumData.NLeach_KgN_ha = NLeach_Kg_ha;//hent KgN/ha data for rotation
                            foulumData.NLeach_mgN_l = NLeach_mg_l;//hent mg/l data for rotation
                            foulumData.NLeach_KgN_ha_total = NLeach_Kg_ha_total;//hent KgN/ha data for hele bedriften
                            foulumData.NLeach_mgN_l_total = NLeach_mg_l_total;//hent mg/l data for hele bedriften
                            datalist.Add(foulumData);
                        }
                    }

                    return datalist;
                }
                else
                {
                    throw new ArgumentException("Bedriftens postnummer er ugyldigt");
                }
            }
        }
        public struct FarmNData
        {
            public string Referencesaedskifte;
            public string Saedskifte;
            public string Jordbundstype;
            public int Arealtype;
            public decimal NLeach_KgN_ha;
            public decimal NLeach_mgN_l;
            public decimal NLeach_KgN_ha_total;
            public decimal NLeach_mgN_l_total;

            public FarmNData(string Referencesaedskifte, string Saedskifte, string Jordbundstype, int Arealtype, decimal NLeach_KgN_ha, decimal NLeach_mgN_l, decimal NLeach_KgN_ha_total, decimal NLeach_mgN_l_total)
            {
                this.Referencesaedskifte = Referencesaedskifte;
                this.Saedskifte = Saedskifte;
                this.Jordbundstype = Jordbundstype;
                this.Arealtype = Arealtype;
                this.NLeach_KgN_ha = NLeach_KgN_ha;
                this.NLeach_mgN_l = NLeach_mgN_l;
                this.NLeach_KgN_ha_total = NLeach_KgN_ha_total;
                this.NLeach_mgN_l_total = NLeach_mgN_l_total;
            }
        }

        [WebMethod]
        public int createVVMFarm(string DbId, out long aNewFarmNumber)
        {

            SqlConnection sqlconn = null;
            aNewFarmNumber = -1;
            int retval;
            try
            {
                sqlconn = new SqlConnection(sqlconnstr);
                sqlconn.Open();

                SqlCommand cmd = new SqlCommand("VVM_CreateFarm", sqlconn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@UGisDbId", SqlDbType.NVarChar, 50);
                cmd.Parameters[cmd.Parameters.Count - 1].Value = DbId;

                cmd.Parameters.Add("@FarmNumber", SqlDbType.BigInt);
                cmd.Parameters["@FarmNumber"].Direction = ParameterDirection.Output;

                retval = cmd.ExecuteNonQuery();

                aNewFarmNumber = Convert.ToInt64(cmd.Parameters["@FarmNumber"].Value);

                int scenario = 1;
                DateTime yearnow = System.DateTime.Today;
                //            int cropyear = yearnow.Year;
                int cropyear = 2007;
                this.UpdateScenarioCropYear(aNewFarmNumber, scenario, cropyear);

                return retval;

            }
            finally
            {
                if (sqlconn != null)
                { sqlconn.Close(); }
            }
        }
        //[WebMethod]
        public int checkVVMFarm(string DbId, out long aNewFarmNumber)
        {

            SqlConnection sqlconn = null;
            aNewFarmNumber = -1;
            int retval;
            try
            {
                sqlconn = new SqlConnection(sqlconnstr);
                sqlconn.Open();

                SqlCommand cmd = new SqlCommand("VVM_CheckFarmNumber", sqlconn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@UGisDbId", SqlDbType.NVarChar, 50);
                cmd.Parameters[cmd.Parameters.Count - 1].Value = DbId;

                cmd.Parameters.Add("@aFarmNumber", SqlDbType.BigInt);
                cmd.Parameters["@aFarmNumber"].Direction = ParameterDirection.Output;

                retval = cmd.ExecuteNonQuery();

                aNewFarmNumber = Convert.ToInt64(cmd.Parameters["@aFarmNumber"].Value);

                return retval;

            }
            finally
            {
                if (sqlconn != null)
                { sqlconn.Close(); }
            }
        }
        //[WebMethod]
        public int checkZipCode(int zipCode, out int OK)
        {

            SqlConnection sqlconn = null;
            int retval;
            try
            {
                sqlconn = new SqlConnection(sqlconnstr);
                sqlconn.Open();

                SqlCommand cmd = new SqlCommand("CheckZipCode", sqlconn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@zipCode", SqlDbType.Int);
                cmd.Parameters[cmd.Parameters.Count - 1].Value = zipCode;

                cmd.Parameters.Add("@OK", SqlDbType.Int);
                cmd.Parameters["@OK"].Direction = ParameterDirection.Output;

                retval = cmd.ExecuteNonQuery();

                OK = Convert.ToInt32(cmd.Parameters["@OK"].Value);

                return OK;

            }
            finally
            {
                if (sqlconn != null)
                { sqlconn.Close(); }
            }
        }
        [WebMethod]
        public bool IsZipCodeValid(int? zipCode)
        {

            SqlConnection sqlconn = null;
            int retval;
            bool OK = false;
            try
            {
                sqlconn = new SqlConnection(sqlconnstr);
                sqlconn.Open();

                SqlCommand cmd = new SqlCommand("CheckZipCode", sqlconn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@zipCode", SqlDbType.Int);
                cmd.Parameters[cmd.Parameters.Count - 1].Value = zipCode;

                cmd.Parameters.Add("@OK", SqlDbType.Int);
                cmd.Parameters["@OK"].Direction = ParameterDirection.Output;

                retval = cmd.ExecuteNonQuery();

                OK = Convert.ToBoolean(cmd.Parameters["@OK"].Value);

                return OK;

            }
            finally
            {
                if (sqlconn != null)
                { sqlconn.Close(); }
            }
        }


        ////[WebMethod(Description="Kun til test")]//KUN TIL TEST***********************
        //public decimal createVVMFarmTest(string DbId, out long aNewFarmNumber)
        //{

        //    SqlConnection sqlconn = null;
        //    aNewFarmNumber = -1;
        //    //decimal retdecimal;
        //    string retstr;
        //    //int retval;
        //    try
        //    {
        //    //    sqlconn = new SqlConnection(sqlconnstr);
        //    //    sqlconn.Open();

        //    //    SqlCommand cmd = new SqlCommand("VVM_CreateFarm", sqlconn);
        //    //    cmd.CommandType = CommandType.StoredProcedure;

        //    //    cmd.Parameters.Add("@UGisDbId", SqlDbType.NVarChar, 50);
        //    //    cmd.Parameters[cmd.Parameters.Count - 1].Value = DbId;

        //    //    cmd.Parameters.Add("@FarmNumber", SqlDbType.BigInt);
        //    //    cmd.Parameters["@FarmNumber"].Direction = ParameterDirection.Output;

        //    //    retval = cmd.ExecuteNonQuery();

        //    //    aNewFarmNumber = Convert.ToInt64(cmd.Parameters["@FarmNumber"].Value);

        //        this.checkVVMFarm(Convert.ToString(DbId), out aNewFarmNumber);
        //    if (aNewFarmNumber == 0)
        //    { this.createVVMFarm(Convert.ToString(DbId), out aNewFarmNumber); }
        //        this.vvmUpdateFarm_ZipCode(aNewFarmNumber, "8830");
        //        int scenario = 1;
        //        this.createScenario(aNewFarmNumber, out scenario);
        //        DateTime yearnow = System.DateTime.Today;
        //        int cropyear = yearnow.Year;
        //        this.UpdateScenarioCropYear(aNewFarmNumber, scenario, cropyear);
        //        //her gennemløbes gødningslisten
        //        this.incrementBoughtManure(aNewFarmNumber, scenario, Convert.ToDecimal(1500), "Kvæggylle", Convert.ToDecimal(70));
        //        this.incrementBoughtManure(aNewFarmNumber, scenario, Convert.ToDecimal(2000), "Dybstrøelse", Convert.ToDecimal(45));
        //        this.incrementBoughtManure(aNewFarmNumber, scenario, Convert.ToDecimal(150), "Afsat ved græsning", Convert.ToDecimal(45));
        //        //her gennemløbes areallisten
        //        int FarmType = 2;
        //        string rotationName = "K2";
        //        if (rotationName.Substring(0,1) == "K") { FarmType = 3;}
        //        this.vvmUpdateFarm_FarmType(aNewFarmNumber, FarmType);
        //        this.copyStandardRotation("K12", aNewFarmNumber, "JB3", Convert.ToDecimal(15.5));
        //        this.copyStandardRotation("K3", aNewFarmNumber, "JB1", Convert.ToDecimal(13.5));
        //        //this.copyStandardRotation("9", aNewFarmNumber, "JB3", Convert.ToInt64(5.5));
        //        //this.copyStandardRotation("14", aNewFarmNumber, "JB1", Convert.ToInt64(3.5));
        //        long afterCropPercent = 6;
        //        this.CalculateAfterCrop(aNewFarmNumber, scenario, 1, afterCropPercent);
        //        this.CalculateAfterCrop(aNewFarmNumber, scenario, 2, afterCropPercent);
        //        this.RotXPress(aNewFarmNumber, scenario, 1);//kan ikke kaldes på test
        //        this.RotXPress(aNewFarmNumber, scenario, 2);//kan ikke kaldes på test
        //        long aNormPercent = 100;
        //        retstr = Convert.ToString("http://130.226.173.223/FarmN/CalculateResult.asp?farmNumber=" + Convert.ToString(aNewFarmNumber) + "&scenarioID=1&password=" + Convert.ToString(aNewFarmNumber) + "&normpercent=" + aNormPercent + "");
        //        //this.startASPPage(Convert.ToString("http://172.20.107.138/FarmN/CalculateResult.asp?farmNumber=" + Convert.ToString(aNewFarmNumber) + "&scenarioID=1&password=" + Convert.ToString(aNewFarmNumber)+"&normpercent="+aNormPercent+""));
        //        this.startASPPage(Convert.ToString("http://172.23.173.223/FarmN/CalculateResult.asp?farmNumber=" + Convert.ToString(aNewFarmNumber) + "&scenarioID=" + Convert.ToString(scenario) + "&password=" + Convert.ToString(aNewFarmNumber)+"&normpercent="+aNormPercent+""));

        //        this.calculateNLesValues(aNewFarmNumber, scenario, 1);
        //        this.calculateNLesValues(aNewFarmNumber, scenario, 2);
        //        decimal a = -1;
        //        decimal b = -1;
        //        this.calculateNLeachRotation(aNewFarmNumber, scenario, 1, out a, out b);
        //        return a;

        //    }
        //    finally
        //    {
        //        if (sqlconn != null)
        //        { sqlconn.Close(); }
        //    }
        //}

        private int vvmUpdateFarm_ZipCode(long aFarmNumber, string aZipCode)
        {

            SqlConnection sqlconn = null;
            try
            {
                int testVal;
                //string sqlString;

                sqlconn = new SqlConnection(sqlconnstr);
                sqlconn.Open();

                SqlCommand cmd = new SqlCommand("VVM_UpdateFarmZipCode", sqlconn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@aFarmNumber", SqlDbType.BigInt);
                cmd.Parameters[cmd.Parameters.Count - 1].Value = aFarmNumber;

                cmd.Parameters.Add("@aFarmOwnerZipCode", SqlDbType.NVarChar);
                cmd.Parameters[cmd.Parameters.Count - 1].Value = aZipCode;


                testVal = cmd.ExecuteNonQuery();
                return testVal;

            }
            finally
            {
                if (sqlconn != null)
                { sqlconn.Close(); }
            }
        }

        private int vvmUpdateFarm_FarmType(long aFarmNumber, int aFarmType)
        {

            SqlConnection sqlconn = null;
            try
            {
                int testVal;

                sqlconn = new SqlConnection(sqlconnstr);
                sqlconn.Open();

                SqlCommand cmd = new SqlCommand("VVM_UpdateFarm_FarmType", sqlconn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@aFarmNumber", SqlDbType.BigInt);
                cmd.Parameters[cmd.Parameters.Count - 1].Value = aFarmNumber;

                cmd.Parameters.Add("@aFarmType", SqlDbType.Int);
                cmd.Parameters[cmd.Parameters.Count - 1].Value = aFarmType;


                testVal = cmd.ExecuteNonQuery();
                return testVal;

            }
            finally
            {
                if (sqlconn != null)
                { sqlconn.Close(); }
            }
        }


        private int insertTest(string httpString)
        {

            SqlConnection sqlconn = null;
            int retval;
            try
            {


                sqlconn = new SqlConnection(sqlconnstr);
                sqlconn.Open();

                SqlCommand cmd = new SqlCommand("InsertTest", sqlconn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@httpString", SqlDbType.VarChar, 150);
                cmd.Parameters[cmd.Parameters.Count - 1].Value = httpString;

                retval = cmd.ExecuteNonQuery();
                return retval;

            }
            finally
            {
                if (sqlconn != null)
                { sqlconn.Close(); }
            }
        }

        private int insertTest(string httpString, string additional)
        {

            SqlConnection sqlconn = null;
            int retval;
            try
            {


                sqlconn = new SqlConnection(sqlconnstr);
                sqlconn.Open();

                SqlCommand cmd = new SqlCommand("InsertTest", sqlconn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@httpString", SqlDbType.VarChar, 150);
                cmd.Parameters[cmd.Parameters.Count - 1].Value = httpString;

                cmd.Parameters.Add("@additional", SqlDbType.VarChar, 50);
                cmd.Parameters[cmd.Parameters.Count - 1].Value = additional;

                retval = cmd.ExecuteNonQuery();
                return retval;

            }
            finally
            {
                if (sqlconn != null)
                { sqlconn.Close(); }
            }
        }

        private int incrementBoughtManure(long aFarmNumber, int aScenarioID, decimal aBoughtManureAmount, string aBoughtManureType, decimal UtilDegree)
        {

            SqlConnection sqlconn = null;
            int retval;
            try
            {


                sqlconn = new SqlConnection(sqlconnstr);
                sqlconn.Open();

                SqlCommand cmd = new SqlCommand("xxVVM_IncrementBoughtManure", sqlconn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@aFarmNumber", SqlDbType.BigInt);
                cmd.Parameters[cmd.Parameters.Count - 1].Value = aFarmNumber;

                cmd.Parameters.Add("@aScenarioID", SqlDbType.Int);
                cmd.Parameters[cmd.Parameters.Count - 1].Value = aScenarioID;

                cmd.Parameters.Add("@aBoughtManureAmount", SqlDbType.Float);
                cmd.Parameters[cmd.Parameters.Count - 1].Value = aBoughtManureAmount;

                cmd.Parameters.Add("@aBoughtManureType", SqlDbType.VarChar, 50);
                cmd.Parameters[cmd.Parameters.Count - 1].Value = aBoughtManureType;

                cmd.Parameters.Add("@aUtilizationDegree", SqlDbType.Float);
                cmd.Parameters[cmd.Parameters.Count - 1].Value = UtilDegree;

                retval = cmd.ExecuteNonQuery();
                return retval;

            }
            finally
            {
                if (sqlconn != null)
                { sqlconn.Close(); }
            }
        }


        private int createScenario(long aFarmNumber, out int aNewScenarioID)
        {

            SqlConnection sqlconn = null;
            aNewScenarioID = -1;
            int retval;
            try
            {
                sqlconn = new SqlConnection(sqlconnstr);
                sqlconn.Open();

                SqlCommand cmd = new SqlCommand("CreateNewScenario", sqlconn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@aFarmNumber", SqlDbType.BigInt);
                cmd.Parameters[cmd.Parameters.Count - 1].Value = aFarmNumber;

                cmd.Parameters.Add("@aNewScenarioID", SqlDbType.Int);
                cmd.Parameters["@aNewScenarioID"].Direction = ParameterDirection.Output;

                retval = cmd.ExecuteNonQuery();

                aNewScenarioID = Convert.ToInt32(cmd.Parameters["@aNewScenarioID"].Value);

                return retval;

            }
            finally
            {
                if (sqlconn != null)
                { sqlconn.Close(); }
            }
        }


        public int copyStandardRotation(string standardRotationName, long aFarmNumber, string JB, decimal rotationArea, int rotationversion)
        {

            SqlConnection sqlconn = null;
            int retval;
            try
            {
                sqlconn = new SqlConnection(sqlconnstr);
                sqlconn.Open();
                string cmdstr = "xxVVM_CopyStandardRotation";
                if (rotationversion == 2)
                {
                    cmdstr = "ver2_VVM_CopyStandardRotation";
                }
                SqlCommand cmd = new SqlCommand(cmdstr, sqlconn);

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@StandardRotationName", SqlDbType.VarChar, 20);
                cmd.Parameters[cmd.Parameters.Count - 1].Value = standardRotationName;

                cmd.Parameters.Add("@ToFarmNumber", SqlDbType.BigInt);
                cmd.Parameters[cmd.Parameters.Count - 1].Value = aFarmNumber;

                cmd.Parameters.Add("@JBString", SqlDbType.VarChar, 20);
                cmd.Parameters[cmd.Parameters.Count - 1].Value = JB;

                cmd.Parameters.Add("@RotationArea", SqlDbType.Float);
                cmd.Parameters[cmd.Parameters.Count - 1].Value = rotationArea;

                retval = cmd.ExecuteNonQuery();

                return retval;

            }
            finally
            {
                if (sqlconn != null)
                { sqlconn.Close(); }
            }
        }


        public int calculateNLeachRotation(long aFarmNumber, int aScenarioID, int aRotationID, out decimal NLeach_Kg_ha, out decimal NLeach_mg_l)
        {

            SqlConnection sqlconn = null;
            NLeach_Kg_ha = -1m;
            NLeach_mg_l = -1m;
            int retval;
            try
            {
                sqlconn = new SqlConnection(sqlconnstr);
                sqlconn.Open();

                SqlCommand cmd = new SqlCommand("VVM_CalcNLeachResultRotation", sqlconn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@FarmNumber", SqlDbType.BigInt);
                cmd.Parameters[cmd.Parameters.Count - 1].Value = aFarmNumber;

                cmd.Parameters.Add("@ScenarioID", SqlDbType.Int);
                cmd.Parameters[cmd.Parameters.Count - 1].Value = aScenarioID;

                cmd.Parameters.Add("@RotationID", SqlDbType.Int);
                cmd.Parameters[cmd.Parameters.Count - 1].Value = aRotationID;

                cmd.Parameters.Add("@NLeach_Kg_ha", SqlDbType.Float);
                cmd.Parameters["@NLeach_Kg_ha"].Direction = ParameterDirection.Output;

                cmd.Parameters.Add("@NLeach_mg_l", SqlDbType.Float);
                cmd.Parameters["@NLeach_mg_l"].Direction = ParameterDirection.Output;

                retval = cmd.ExecuteNonQuery();

                NLeach_Kg_ha = Convert.ToDecimal(cmd.Parameters["@NLeach_Kg_ha"].Value);
                NLeach_mg_l = Convert.ToDecimal(cmd.Parameters["@NLeach_mg_l"].Value);

                return retval;

            }
            finally
            {
                if (sqlconn != null)
                { sqlconn.Close(); }
            }
        }
        [WebMethod]
        public int calculateNLeachFarm_Rotation(long aFarmNumber, int aScenarioID, int aRotationID, out decimal NLeach_Kg_ha, out decimal NLeach_mg_l, out decimal NLeach_Kg_ha_total, out decimal NLeach_mg_l_total)
        {

            SqlConnection sqlconn = null;
            NLeach_Kg_ha = -1m;
            NLeach_mg_l = -1m;
            NLeach_Kg_ha_total = -1m;
            NLeach_mg_l_total = -1m;
            int retval;
            try
            {
                sqlconn = new SqlConnection(sqlconnstr);
                sqlconn.Open();

                SqlCommand cmd = new SqlCommand("VVM_CalcNLeachFarmResult", sqlconn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@FarmNumber", SqlDbType.BigInt);
                cmd.Parameters[cmd.Parameters.Count - 1].Value = aFarmNumber;

                cmd.Parameters.Add("@ScenarioID", SqlDbType.Int);
                cmd.Parameters[cmd.Parameters.Count - 1].Value = aScenarioID;

                cmd.Parameters.Add("@RotationID", SqlDbType.Int);
                cmd.Parameters[cmd.Parameters.Count - 1].Value = aRotationID;

                cmd.Parameters.Add("@NLeach_Kg_ha", SqlDbType.Float);
                cmd.Parameters["@NLeach_Kg_ha"].Direction = ParameterDirection.Output;

                cmd.Parameters.Add("@NLeach_mg_l", SqlDbType.Float);
                cmd.Parameters["@NLeach_mg_l"].Direction = ParameterDirection.Output;

                cmd.Parameters.Add("@NLeach_Kg_ha_total", SqlDbType.Float);
                cmd.Parameters["@NLeach_Kg_ha_total"].Direction = ParameterDirection.Output;

                cmd.Parameters.Add("@NLeach_mg_l_total", SqlDbType.Float);
                cmd.Parameters["@NLeach_mg_l_total"].Direction = ParameterDirection.Output;

                retval = cmd.ExecuteNonQuery();

                NLeach_Kg_ha = Convert.ToDecimal(cmd.Parameters["@NLeach_Kg_ha"].Value);
                NLeach_mg_l = Convert.ToDecimal(cmd.Parameters["@NLeach_mg_l"].Value);
                NLeach_Kg_ha_total = Convert.ToDecimal(cmd.Parameters["@NLeach_Kg_ha_total"].Value);
                NLeach_mg_l_total = Convert.ToDecimal(cmd.Parameters["@NLeach_mg_l_total"].Value);

                return retval;

            }
            finally
            {
                if (sqlconn != null)
                { sqlconn.Close(); }
            }
        }
        [WebMethod]
        public int calculateNLesValues(long aFarmNumber, int aScenarioID, int aRotationID)
        {

            SqlConnection sqlconn = null;
            int retval;
            try
            {
                sqlconn = new SqlConnection(sqlconnstr);
                sqlconn.Open();

                SqlCommand cmd = new SqlCommand("CalculateNLesValues", sqlconn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@FarmNumber", SqlDbType.BigInt);
                cmd.Parameters[cmd.Parameters.Count - 1].Value = aFarmNumber;

                cmd.Parameters.Add("@ScenarioID", SqlDbType.Int);
                cmd.Parameters[cmd.Parameters.Count - 1].Value = aScenarioID;

                cmd.Parameters.Add("@RotationID", SqlDbType.Int);
                cmd.Parameters[cmd.Parameters.Count - 1].Value = aRotationID;

                retval = cmd.ExecuteNonQuery();


                return retval;

            }
            finally
            {
                if (sqlconn != null)
                { sqlconn.Close(); }
            }
        }


        private int UpdateScenarioCropYear(
                                            long farmNumber,
                                            int scenarioID,
                                            int cropYear)
        {

            SqlConnection sqlconn = null;
            try
            {
                sqlconn = new SqlConnection(sqlconnstr);
                sqlconn.Open();

                SqlCommand cmd = new SqlCommand("VVM_UpdateScenarioCropYear", sqlconn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@aFarmNumber", SqlDbType.BigInt);
                cmd.Parameters[cmd.Parameters.Count - 1].Value = farmNumber;

                cmd.Parameters.Add("@aScenario", SqlDbType.Int);
                cmd.Parameters[cmd.Parameters.Count - 1].Value = scenarioID;

                cmd.Parameters.Add("@aCropYear", SqlDbType.VarChar, 50);
                cmd.Parameters[cmd.Parameters.Count - 1].Value = cropYear;

                return cmd.ExecuteNonQuery();

            }
            finally
            {
                if (sqlconn != null)
                { sqlconn.Close(); }
            }
        }

        private int CalculateAfterCrop(
                                        long farmNumber,
                                        int scenarioID,
                                        int rotationID,
                                        decimal afterCropPercent)
        {

            SqlConnection sqlconn = null;
            try
            {
                sqlconn = new SqlConnection(sqlconnstr);
                sqlconn.Open();

                //SqlCommand cmd = new SqlCommand("VVM_CalculateAfterCrop", sqlconn);
                SqlCommand cmd = new SqlCommand("VVM_CalculateAfterCrop", sqlconn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@FarmNumber", SqlDbType.BigInt);
                cmd.Parameters[cmd.Parameters.Count - 1].Value = farmNumber;

                cmd.Parameters.Add("@ScenarioID", SqlDbType.Int);
                cmd.Parameters[cmd.Parameters.Count - 1].Value = scenarioID;

                cmd.Parameters.Add("@RotationID", SqlDbType.Int);
                cmd.Parameters[cmd.Parameters.Count - 1].Value = rotationID;

                cmd.Parameters.Add("@AfterCropPercent", SqlDbType.Float);
                cmd.Parameters[cmd.Parameters.Count - 1].Value = afterCropPercent;

                return cmd.ExecuteNonQuery();

            }
            finally
            {
                if (sqlconn != null)
                { sqlconn.Close(); }
            }
        }

        private int MultiplyRotationAreas(
                                    long farmNumber,
                                    int scenarioID,
                                    int rotationID,
                                    decimal area)
        {

            SqlConnection sqlconn = null;
            try
            {
                sqlconn = new SqlConnection(sqlconnstr);
                sqlconn.Open();

                SqlCommand cmd = new SqlCommand("xxVVM_MultiplyRotationAreas", sqlconn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@aFarmNumber", SqlDbType.BigInt);
                cmd.Parameters[cmd.Parameters.Count - 1].Value = farmNumber;

                cmd.Parameters.Add("@aScenarioID", SqlDbType.Int);
                cmd.Parameters[cmd.Parameters.Count - 1].Value = scenarioID;

                cmd.Parameters.Add("@aRotationID", SqlDbType.Int);
                cmd.Parameters[cmd.Parameters.Count - 1].Value = rotationID;

                cmd.Parameters.Add("@aArea", SqlDbType.Float);
                cmd.Parameters[cmd.Parameters.Count - 1].Value = area;

                return cmd.ExecuteNonQuery();

            }
            finally
            {
                if (sqlconn != null)
                { sqlconn.Close(); }
            }
        }


        private void AddCmdParameters(ref SqlCommand cmd, Parameter[] param)
        {
            for (int i = 0; i < param.Length; i++)
            {
                cmd.Parameters.Add(param[i].name, param[i].type);
                cmd.Parameters[i].Value = param[i].value;
            }
        }

        private struct Parameter
        {
            public string name;
            public SqlDbType type;
            public object value;

            public Parameter(string name, SqlDbType type, object value)
            {
                this.name = name;
                this.type = type;
                this.value = value;
            }
        }
        [WebMethod]
        public double RotXPress(long BedriftID, int ScenarioID, int RotationID)
        {
            double result;
            int NumberCrops;

            SqlConnection sqlconn = null;
            try
            {

                sqlconn = new SqlConnection(sqlconnstr);
                sqlconn.Open();

                SqlCommand cmd = new SqlCommand("GetNumberOfCropsPrRotation", sqlconn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@FarmNumber", SqlDbType.BigInt);
                cmd.Parameters[cmd.Parameters.Count - 1].Value = BedriftID;
                cmd.Parameters.Add("@ScenarioID", SqlDbType.Int);
                cmd.Parameters[cmd.Parameters.Count - 1].Value = ScenarioID;
                cmd.Parameters.Add("@RotationID", SqlDbType.Int);
                cmd.Parameters[cmd.Parameters.Count - 1].Value = RotationID;

                cmd.Parameters.Add("@NumberOfCrops", SqlDbType.Int);
                cmd.Parameters["@NumberOfCrops"].Direction = ParameterDirection.Output;

                int retval = cmd.ExecuteNonQuery();

                NumberCrops = Convert.ToInt32(cmd.Parameters["@NumberOfCrops"].Value);

                string sourcepath;
                sourcepath = "c:\\Inetpub\\wwwroot\\farmn\\XPressModels\\BladeRotationModelProductionTest.mos";
                string bimpath;
                bimpath = "c:\\Inetpub\\wwwroot\\farmn\\XPressModels\\BladeRotationModelProductionTest.bim";
                XPRM mosel = XPRM.Init();
                mosel.Compile("", sourcepath, bimpath);

                XPRMModel myModel = mosel.LoadModel(bimpath);
                myModel.ExecParams = "BedriftID = " + BedriftID + ", ScenarioID = " + ScenarioID + ", RotationID = " + RotationID + ", NumberCrops = " + NumberCrops;

                myModel.Run();

                result = myModel.ObjectiveValue;

                mosel.Dispose();
                myModel.Dispose();
                return result;
            }
            finally
            {
                if (sqlconn != null)
                { sqlconn.Close(); }
            }
        }
        [WebMethod]
        public void startASPPage(string url)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            response.Close();

        }

    }
}