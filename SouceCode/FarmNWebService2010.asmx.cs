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

namespace FarmN_2010
{
    /// <summary>
    /// Summary description for FarmNWebService2010
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class FarmNWebService2010 : System.Web.Services.WebService
    {
        private AppSettingsReader cfg = null;
        private string sqlconnstr = "";
        private SqlConnection sqlconn;
        private CalculateScenario scenario = null;
        static int i = 0;
        public FarmNWebService2010()
        {
            i++;
            cfg = new AppSettingsReader();

            sqlconnstr = cfg.GetValue("sqlConnectionStringPublic", typeof(string)).ToString();
            sqlconn = null;
            try
            {
                sqlconn = new SqlConnection(sqlconnstr);
                sqlconn.Open();
            }
            catch (Exception e)
            {
               message.Instance.addWarnings("Problemer med databasen","Cannot open Database connectíon "+e.Message.ToString(),2);
            }
        }

        [WebMethod]
        public returnItems CalculateExisting(List<Areal> arealer, List<Husdyrgodkendelse.Goedning.Goedningsmaengde> goedningsmaengder, long bedriftID, string zipCode, int normYear, int version)
        {
            returnItems tilbage =new returnItems();
            try
            {
                List<Manure> internalManureList = new List<Manure>();
                List<StandardRotation> internalStandardRotationList = new List<StandardRotation>();
                foreach (Husdyrgodkendelse.Goedning.Goedningsmaengde manure in goedningsmaengder)
                {
                    int manureType = translateManureType(manure.Goedningstype.Navn);
                    Manure newManure = new Manure(manure.KgN, manureType, manure.GoedningstypeNUdnyttelsesProcent);
                    internalManureList.Add(newManure);
                }
                foreach (Areal areal in arealer)
                {

                    if (areal.ArealType == ArealTyper.Udbringning)
                    {
                        int soilType = translateSoilType(areal.Jordbundstype);
                        StandardRotation rotation = new StandardRotation(areal.Saedskifte, soilType, areal.Ha, areal.Referencesaedskifte, areal.Saedskifte, (int)areal.ArealType);
                        internalStandardRotationList.Add(rotation);
                    }
                }
                int cropyear = -1;
                int rotationVersion = -1;
                int manureVersion = -1;
                switch (version)
                {
                    case 1: cropyear = 2007;
                        if (cropyear != normYear)
                        {
                            message.Instance.addWarnings("Det angivne år for udbytte- og gødnings-norm bør være 2007 i version 1", "normYear = " + normYear + " og burde være 2007 (version1)", 3);
                        }
                        rotationVersion = 1;
                        manureVersion = 1;
                        break;
                    case 2: cropyear = 2009;
                        if (cropyear != normYear)
                        {
                            message.Instance.addWarnings("Det angivne år for udbytte- og gødnings-norm bør være 2009 i version 2", "normYear = " + normYear + " og burde være 2009 (version2)", 3);
                        }
                        rotationVersion = 2;
                        manureVersion = 2;
                        break;

                    case 3: cropyear = 2009;
                        if (cropyear != normYear)
                        {
                            message.Instance.addWarnings("Det angivne år for udbytte- og gødnings-norm bør være 2009 i version 3", "normYear = " + normYear + " og burde være 2009 (version3)", 3);
                        }
                        rotationVersion = 2;
                        manureVersion = 1;
                        break;

                    case 4: cropyear = normYear;
                        rotationVersion = 2;
                        manureVersion = 2;
                        break;
                        
                }
                scenario = new CalculateScenario(bedriftID, zipCode, cropyear, 1, 0, 0, manureVersion, internalManureList, internalStandardRotationList, rotationVersion,version);
                scenario.run();

                tilbage = scenario.getReturnData();
            }
            catch (Exception e)
            {
                message.Instance.addWarnings("Kritisk systemfejl", "On global scale " + e.Message.ToString(), 4);
                tilbage.NLeach_KgN_ha_total = -1;
                tilbage.NLeach_mgN_l_total = -1;
                tilbage.arealData = new List<FarmNData>();

            }
            finally
            {
                message.Instance.WriteToFile();
                tilbage.errorMessages = message.Instance.getwarningsList();
                if (sqlconn != null)
                {
                    sqlconn.Close();
                }
            }

            return tilbage;
        }
        
        [WebMethod]
        public returnItems CalculateApplication(List<Areal> arealer, List<Husdyrgodkendelse.Goedning.Goedningsmaengde> goedningsmaengder, long bedriftID, string zipCode, decimal afterCropPercent, decimal aNormPercent, int normYear, int version)
        {
            returnItems tilbage = new returnItems(); ;
           try
            {
                List<Manure> internalManureList = new List<Manure>();
                List<StandardRotation> internalStandardRotationList = new List<StandardRotation>();
                foreach (Husdyrgodkendelse.Goedning.Goedningsmaengde manure in goedningsmaengder)
                {
                    int manureType = translateManureType(manure.Goedningstype.Navn);
                    Manure newManure = new Manure(manure.KgN, manureType, manure.GoedningstypeNUdnyttelsesProcent);
                    internalManureList.Add(newManure);
                }
                foreach (Areal areal in arealer)
                {

                    if (areal.ArealType == ArealTyper.Udbringning)
                    {
                        int soilType = translateSoilType(areal.Jordbundstype);
                        StandardRotation rotation = new StandardRotation(areal.Saedskifte, soilType, areal.Ha, areal.Referencesaedskifte, areal.Saedskifte, (int)areal.ArealType);
                        internalStandardRotationList.Add(rotation);
                    }
                }
                int cropyear = -1;
                int rotationVersion = -1;
                int manureVersion = -1;
                switch (version)
                {
                    case 1: cropyear = 2007;
                        if (cropyear != normYear)
                        {
                            message.Instance.addWarnings("Det angivne år for udbytte- og gødnings-norm bør være 2007 i version 1", "normYear = " + normYear + " og burde være 2007 (version1)", 3);
                        }
                        rotationVersion = 1;
                        manureVersion = 1;
                        break;
                    case 2: cropyear = 2009;
                        if (cropyear != normYear)
                        {
                            message.Instance.addWarnings("Det angivne år for udbytte- og gødnings-norm bør være 2009 i version 2", "normYear = " + normYear + " og burde være 2009 (version2)", 3);
                        }
                        rotationVersion = 2;
                        manureVersion = 2;
                        break;
                    case 3: cropyear = 2009;
                        if (cropyear != normYear)
                        {
                            message.Instance.addWarnings("Det angivne år for udbytte- og gødnings-norm bør være 2009 i version 3", "normYear = " + normYear + " og burde være 2009 (version3)", 3);
                        }
                        rotationVersion = 2;
                        manureVersion = 1;
                        break;

                    case 4: cropyear = normYear;
                        rotationVersion = 2;
                        manureVersion = 2;
                        break;

                }
                scenario = new CalculateScenario(bedriftID, zipCode, cropyear, 2, afterCropPercent, aNormPercent, manureVersion, internalManureList, internalStandardRotationList, rotationVersion, version);
                
                scenario.run();
                tilbage = scenario.getReturnData();
            }
           catch (Exception e)
            {
                message.Instance.addWarnings("Kritisk systemfejl", "On global scale " + e.Message.ToString(), 4);
                tilbage.NLeach_KgN_ha_total = -1;
                tilbage.NLeach_mgN_l_total = -1;
                tilbage.arealData = new List<FarmNData>();

            }
            finally
            {
                message.Instance.WriteToFile();
                tilbage.errorMessages = message.Instance.getwarningsList();
                message.Instance.reset();

                if (sqlconn != null)
                {
                    sqlconn.Close();
                }
                
            }
           this.Dispose();
            return tilbage;
        }
        [WebMethod]
        public returnItems CalculateScenario3(List<Areal> arealer, List<Husdyrgodkendelse.Goedning.Goedningsmaengde> goedningsmaengder, long bedriftID, string zipCode, decimal DEmax_DEreel, int normYear, int version)
        {
            returnItems tilbage = new returnItems();
            try
            {
                List<Manure> internalManureList = new List<Manure>();
                List<StandardRotation> internalStandardRotationList = new List<StandardRotation>();
                foreach (Husdyrgodkendelse.Goedning.Goedningsmaengde manure in goedningsmaengder)
                {
                    int manureType = translateManureType(manure.Goedningstype.Navn);
                    Manure newManure = new Manure(manure.KgN * DEmax_DEreel, manureType, manure.GoedningstypeNUdnyttelsesProcent);
                    internalManureList.Add(newManure);
                }
                foreach (Areal areal in arealer)
                {

                    if (areal.ArealType == ArealTyper.Udbringning)
                    {
                        int soilType = translateSoilType(areal.Jordbundstype);
                        StandardRotation rotation = new StandardRotation(areal.Saedskifte, soilType, areal.Ha, areal.Referencesaedskifte, areal.Saedskifte, (int)areal.ArealType);
                        internalStandardRotationList.Add(rotation);
                    }
                }
                int cropyear = -1;
                int rotationVersion = -1;
                int manureVersion = -1;
                switch (version)
                {
                    case 1: cropyear = 2007;
                        if (cropyear != normYear)
                        {
                            message.Instance.addWarnings("Det angivne år for udbytte- og gødnings-norm bør være 2007 i version 1", "normYear = " + normYear + " og burde være 2007 (version1)", 3);
                        }
                        rotationVersion = 1;
                        manureVersion = 1;
                        break;
                    case 2: cropyear = 2009;
                        if (cropyear != normYear)
                        {
                            message.Instance.addWarnings("Det angivne år for udbytte- og gødnings-norm bør være 2009 i version 2", "normYear = " + normYear + " og burde være 2009 (version2)", 3);
                        }
                        rotationVersion = 2;
                        manureVersion = 2;
                        break;
                    case 3: cropyear = 2009;
                        if (cropyear != normYear)
                        {
                            message.Instance.addWarnings("Det angivne år for udbytte- og gødnings-norm bør være 2009 i version 3", "normYear = " + normYear + " og burde være 2009 (version3)", 3);
                        }
                        rotationVersion = 2;
                        manureVersion = 1;
                        break;

                    case 4: cropyear = normYear;
                        rotationVersion = 2;
                        manureVersion = 2;
                        break;

                }
                scenario = new CalculateScenario(bedriftID, zipCode, cropyear, 3, 0, 0, manureVersion, internalManureList, internalStandardRotationList, rotationVersion, version);
                scenario.run();
                tilbage = scenario.getReturnData();
            }
            catch (Exception e)
            {
                message.Instance.addWarnings("Kritisk systemfejl", "On global scale " + e.Message.ToString(), 4);
                tilbage.NLeach_KgN_ha_total = -1;
                tilbage.NLeach_mgN_l_total = -1;
                tilbage.arealData = new List<FarmNData>();

            }
            finally
            {
                message.Instance.WriteToFile();
                tilbage.errorMessages = message.Instance.getwarningsList();
                if (sqlconn != null)
                {
                    sqlconn.Close();
                }
            }

            return tilbage;
        }
        private List<Manure> transferManure(List<Husdyrgodkendelse.Goedning.Goedningsmaengde> manureAmount)
        {
            Manure manure;
            List<Manure> ManureList = new List<Manure>();
            foreach (Husdyrgodkendelse.Goedning.Goedningsmaengde g in manureAmount)
            {
                int ManureTypeInt = translateManureType(g.Goedningstype.Navn);
                if ((ManureTypeInt == 12 || ManureTypeInt == 13 || ManureTypeInt == 14) && g.GoedningstypeNUdnyttelsesProcent == 0)
                    message.Instance.addWarnings("Manglende udnyttelsesprocent for goedningstypen", "transferManure: cannot translate", 2);
                manure = new Manure(g.KgN, ManureTypeInt, g.GoedningstypeNUdnyttelsesProcent);
                ManureList.Add(manure);
            }
            return ManureList;
        }

        private List<StandardRotation> transferStandardRotation(List<Areal> areaList)
        {
            StandardRotation standardRotation;
            List<StandardRotation> StandardRotationList = new List<StandardRotation>();
            foreach (Areal a in areaList)
            {
                standardRotation = new StandardRotation(a.Saedskifte, translateManureType(a.Jordbundstype), a.Ha,a.Referencesaedskifte,a.Saedskifte,(int)a.ArealType);
                StandardRotationList.Add(standardRotation);
            }
            return StandardRotationList;
        }

        private int translateManureType(string ManureType)
        {

            int ManureTypeInt = -1;
            try
            {
                string cmdstr = "MST_translateManureType";
                SqlCommand cmd = new SqlCommand(cmdstr, sqlconn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@aBoughtManureType", SqlDbType.VarChar, 100);
                cmd.Parameters[cmd.Parameters.Count - 1].Value = ManureType;
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    ManureTypeInt = Convert.ToInt32(reader["StorageID"]);
                }
                reader.Close();
            }
            catch
            {
                message.Instance.addWarnings("Problemer med databaseopslag for goedeningstype","Cannot find Manure Type " + ManureType,1);
            }
            if(ManureTypeInt==-1)
                message.Instance.addWarnings("Ukendt goedningstype","Cannot find Manure Type " + ManureType,1);
            return ManureTypeInt;

        }
        private int translateSoilType(string soilType)
        {
            int soilTypeInt = 0;
            try
            {
                string cmdstr = "MST_translateJB";
                SqlCommand cmd = new SqlCommand(cmdstr, sqlconn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@JBString", SqlDbType.VarChar, 20);
                cmd.Parameters[cmd.Parameters.Count - 1].Value = soilType;
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    soilTypeInt = Convert.ToInt32(reader["SoilTypeID"]);
                }
                reader.Close();
            }
            catch (Exception e)
            {
                message.Instance.addWarnings("Ukendt jordtype","Cannot find soiltype "+e.Message.ToString(),1);
            }
            return soilTypeInt;

        }


    }
    /// <summary>
    /// An error generated by farmn
    /// </summary>
    public struct error
    {
        /// <summary>
        /// Type of error: 1-> fatal user error, 2-> fatal program error, 3->light user error,4-> ligth programing error
        /// </summary>
        public int ErrorType;
        public string ErrorMessage;
        public string programError;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ErroMessage">End user information</param>
        /// <param name="programError">Programmer information</param>
        /// <param name="ErrorType">type</param>
        public error(string ErroMessage,string programError,int ErrorType)
        {
            this.ErrorType = ErrorType;
            this.ErrorMessage = ErroMessage;
            this.programError = programError;
        }
        /// <summary>
        /// returns a description of the error. This is intented for programming information 
        /// </summary>
        /// <returns>returns a description of the error </returns>
        public string getProgramError()
        {
            return programError;
        }
        /// <summary>
        /// returns a description of the error. This is intented for enduser information 
        /// </summary>
        /// <returns>returns a description of the error </returns>
        public string getErrorMessage()
        {
            return ErrorMessage;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns>return the type of error</returns>
        public int getErrorType()
        {
            return ErrorType;
        }
    }
    /// <summary>
    /// a struct with an area and a crop on it
    /// </summary>
    public struct CropData
    {
        public decimal Areal;
        public string AfgroedeNavn;
        public CropData(decimal areal, string afgroedeNavn)
        {
            this.Areal = areal;
            this.AfgroedeNavn = afgroedeNavn;
        }
    }
    /// <summary>
    /// holds return values for a single rotation
    /// </summary>
    public class FarmNData
    {

        public string Referencesaedskifte;
        public string Saedskifte;
        public string Jordbundstype;
        public List<CropData> CropListe;
        public decimal RunnOff;
        public decimal UdvaskningKgNHa;
        public decimal UdvaskningMgPrL;
        public decimal UdvaskningKgNHa_korrigeret;
        public decimal UdvaskningMgPrL_korrigeret;
        public decimal Hoest;
        public decimal Denitrifikation;
        public decimal Jordpuljeaendring;
        public decimal Markoverskud;
        public decimal HandelsGoedning;
        public decimal HusdyrGoedning;
        public decimal AmmoniumtabHandelsGoedning;
        public decimal AmmoniumtabHusdyrGoedning;
        public decimal nFiksering;
        public decimal AtmosfaeriskAfsaetning;
        public decimal nInSeed;

        private FarmNData()
        {
        }
        public FarmNData(string Referencesaedskifte, string Saedskifte, string Jordbundstype, List<CropData> cropListe, decimal runnOff, decimal udvaskningKgNHa, decimal udvaskningMgPrL, decimal hoest, decimal denitrifikation, decimal jordpuljeaendring, decimal markoverskud, decimal handelsGoedning, decimal husdyrGoedning, decimal ammoniumtabHandelsGoedning, decimal ammoniumtabHusdyrGoedning, decimal nFiksering, decimal atmosfaeriskAfsaetning, decimal nInSeed)
        {

            this.Referencesaedskifte = Referencesaedskifte;
            this.Saedskifte = Saedskifte;
            this.Jordbundstype = Jordbundstype;
            this.CropListe = cropListe;
            this.RunnOff = runnOff;
            this.UdvaskningKgNHa = udvaskningKgNHa;
            this.UdvaskningMgPrL = udvaskningMgPrL;
            this.UdvaskningKgNHa_korrigeret = udvaskningKgNHa;
            this.UdvaskningMgPrL_korrigeret = udvaskningMgPrL;
            this.Hoest = hoest;
            this.Denitrifikation = denitrifikation;
            this.Jordpuljeaendring = jordpuljeaendring;
            this.Markoverskud = markoverskud;
            this.HandelsGoedning = handelsGoedning;
            this.HusdyrGoedning = husdyrGoedning;
            this.AmmoniumtabHandelsGoedning = ammoniumtabHandelsGoedning;
            this.AmmoniumtabHusdyrGoedning = ammoniumtabHusdyrGoedning;
            this.nFiksering = nFiksering;
            this.AtmosfaeriskAfsaetning = atmosfaeriskAfsaetning;
            this.nInSeed = nInSeed;

        }

        public void setNLeachmgNl(decimal udvaskningMgPrL)
        {
            this.UdvaskningMgPrL = udvaskningMgPrL;
        }
        public void setNLeachKgNHa(decimal udvaskningKgNHa)
        {
            this.UdvaskningKgNHa = udvaskningKgNHa;
        }
        public void setNLeachmgNl_korrigeret(decimal udvaskningMgPrL)
        {
            this.UdvaskningMgPrL_korrigeret = udvaskningMgPrL;
        }
        public void setNLeachKgNHa_korrigeret(decimal udvaskningKgNHa)
        {
            this.UdvaskningKgNHa_korrigeret = udvaskningKgNHa;
        }
    }
    /// <summary>
    /// a summery of the results.
    /// Hold some average values, a list of errors and a list of rotation data 
    /// </summary>
    public struct returnItems
    {
        public decimal Hoest;
        public decimal Hoest_korrigeret;
        public decimal Denitrifikation;
        public decimal Denitrifikation_korrigeret;
        public decimal JordPuljeaendring;
        public decimal JordPuljeaendring_korrigeret;
        public decimal MarkOverskud;
        public decimal MarkOverskud_korrigeret;
        public decimal NLeach_KgN_ha_total;
        public decimal NLeach_mgN_l_total;
        public decimal NLeach_KgN_ha_korrigeret;
        public decimal NLeach_mgN_l_korrigeret;
        public decimal RestKorrektion;
        public List<FarmNData> arealData;
        public List<error> errorMessages;


    }
           /// <summary>
           /// represent a rotation
           /// </summary>
            public struct StandardRotation
            {
                private string StandardRotationName, ReferenceSedskifte, Sedskifte;
                private int SoilType, arealType;
                private decimal AreaHa;
                /// <summary>
                /// the constructor
                /// </summary>
                /// <param name="StandardRotationName">The name of the rotation</param>
                /// <param name="SoilType">the soil type eg from 1 to 12</param>
                /// <param name="AreaHa">Area in HA</param>
                /// <param name="ReferenceSedskifte">the name of the reference rotation</param>
                /// <param name="Sedskifte">the rotation name </param>
                /// <param name="arealType">The area type</param>
                public StandardRotation(string StandardRotationName, int SoilType, decimal AreaHa, string ReferenceSedskifte, string Sedskifte, int arealType)
                {
                    this.StandardRotationName = StandardRotationName;
                    this.SoilType = SoilType;
                    this.AreaHa = AreaHa;
                    this.ReferenceSedskifte=ReferenceSedskifte;
                    this.Sedskifte=Sedskifte;
                    this.arealType = arealType;
                }
                public string getStdRotationName()
                {
                    return StandardRotationName;
                }
                public string getReferenceSedskifte()
                {
                    return ReferenceSedskifte;
                }
                public string getSedskifte()
                {
                    return Sedskifte;
                }
                public int getarealType()
                {
                    return arealType;
                }

                public int getSoilType()
                {
                    return SoilType;
                }
                public decimal getAreaHa()
                {
                    return AreaHa;
                }
            }
            /// <summary>
            /// Represent Manure
            /// </summary>
            public struct Manure
            {
                private decimal KgN;
                private int ManureType;
                private decimal UtilizationDegree;
                /// <summary>
                /// the constructor
                /// </summary>
                /// <param name="KgN">The amount of N in the manure messured in kg</param>
                /// <param name="ManureType">The type of the manure</param>
                /// <param name="UtilizationDegree">how much of the manure that can be utilized</param>
                public Manure(decimal KgN, int ManureType, decimal UtilizationDegree)
                {
                    this.KgN = KgN;
                    this.ManureType = ManureType;
                    this.UtilizationDegree = UtilizationDegree;
                }
                public decimal getKgN()
                {
                     return KgN;
                }
                public int getManureType()
                {
                    return ManureType;
                }
                public decimal getUtilizationDegree()
                {
                    return UtilizationDegree;
                }
            }


}
