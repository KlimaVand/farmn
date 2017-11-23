
using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
namespace FarmN_2010
{
    /// <summary>
    /// a class to handle Delta soil N calculation
    /// </summary>
    public class DeltaSoilN
    {
        private decimal SoilChange;
        private bool ready;

        
        public DeltaSoilN()
        {
            ready = false;
            if (globalSettings.Instance.getExtraOutput() == true)
            {

                globalFilehandling.Instance.writeToDeltaSoilN("SoilCode" + '\t' + "FarmType" + '\t' + "PostalCode" + '\t' + "TotalCarbonFromCrops" + '\t' + "TotalCarbonFromManure" + '\t' + "FractionCatchCrops" + '\t' + "Clay" + '\t' + "result");


            }
        }



        /// <summary>
        /// Initializing values for SoilN
        /// </summary>
        /// <param name="SoilCode">The soil type, Range from 1 to 12</param>
        /// <param name="FarmType">need to be between 1 and 3</param>
        /// <param name="PostalCode">need to be somewhere in denmark</param>
        /// <param name="TotalCarbonFromCrops">carbon from crops. neeed to be between 0 and 7</param>
        /// <param name="TotalCarbonFromManure">carbon from manure.neeed to be between 0 and 5</param>
        /// <param name="FractionCatchCrops">fraction of catch crop. Need to bee between 0 and 1</param>
        /// <param name="Clay">Clay in soil. need to be between 0 and 100</param>
        /// <returns> negativ if something went wrong or 0 if it is OK </returns>
        public int init(int SoilCode, int FarmType, int PostalCode, decimal TotalCarbonFromCrops, decimal TotalCarbonFromManure, decimal FractionCatchCrops, decimal Clay)
        {
           // globalTime.Instance.start(3, "DeltaSoil");
            if (SoilCode < 1 || SoilCode>12)
            {
                message.Instance.addWarnings("Ukendt jordtype. Den er " + SoilCode.ToString() + " og burde være mellem 1 og 12", "DeltaSoilN: SoilCode is not valid", 2);
                return -1;
            }
            if (FarmType < 1 || FarmType > 3)
            {
                message.Instance.addWarnings("Bedriftstypen er forkert.Den er " + FarmType.ToString() + " og burde være mellem 1 og 3", "DeltaSoilN: FarmType is not valid", 2);
                return -2;
            }
            if (PostalCode < 1000 || PostalCode > 9990)
            {
                message.Instance.addWarnings("Postnr er forkert. Den er " + PostalCode.ToString() + " og burde være mellem 1000 og 9990", "DeltaSoilN: PostalCode is not valid", 2);
                return -3;
            }
            if (TotalCarbonFromCrops < 0|| TotalCarbonFromCrops > 7)
            {
                message.Instance.addWarnings("Carbon fra planter er usandsynlig. Den er " + TotalCarbonFromCrops.ToString() + " og vil normalt være mellem 0 og 7", "DeltaSoilN: TotalCarbonFromCrops is not valid", 3);
                return -4;
            }
            if (TotalCarbonFromManure < 0 || TotalCarbonFromManure > 5)
            {
                message.Instance.addWarnings("Carbon fra goedning er usandsynlig. Den er " + TotalCarbonFromManure.ToString() + " og vil normalt være mellem 0 og 5", "DeltaSoilN: TotalCarbonFromManure is not valid", 3);
            }
            if (FractionCatchCrops < 0 || FractionCatchCrops > 1)
            {
                message.Instance.addWarnings("Efterafgroede fraktion er forkert. Den er " + FractionCatchCrops.ToString() + " og skal være mellem 0 og 1", "DeltaSoilN: FractionCatchCrops is not valid", 2);
                return -6;
            }
            if (Clay < 0 || Clay >100)
            {
                message.Instance.addWarnings("Lerprocenten er forkert. Den er " + Clay.ToString()+" og skal være mellem 0 og 100", "DeltaSoilN: Clay is not valid", 2);
                return -7;
            }
            ready = true;
            this.SoilChange = calculate(SoilCode, FarmType, PostalCode, TotalCarbonFromCrops, TotalCarbonFromManure, FractionCatchCrops, Clay);
           // globalTime.Instance.stop(3);
            return 0;
        }
        /*
        public int initV2(int SoilCode, int FarmType, int PostalCode, decimal TotalCarbonFromCrops, decimal TotalCarbonFromManure, decimal FractionCatchCrops, decimal Clay)
        {
            globalTime.Instance.start(3, "DeltaSoil");
            if (SoilCode < 1 || SoilCode > 12)
            {
                message.Instance.addWarnings("DeltaSoilN: SoilCode is not valid", 2);
                return -1;
            }
            if (FarmType < 1 || FarmType > 3)
            {
                message.Instance.addWarnings("DeltaSoilN: FarmType is not valid", 2);
                return -2;
            }
            if (PostalCode < 1000 || PostalCode > 9990)
            {
                message.Instance.addWarnings("DeltaSoilN: PostalCode is not valid", 2);
                return -3;
            }
            if (TotalCarbonFromCrops < 0 || TotalCarbonFromCrops > 7)
            {
                message.Instance.addWarnings("DeltaSoilN: TotalCarbonFromCrops is not valid", 2);
                return -4;
            }
            if (TotalCarbonFromManure < 0 || TotalCarbonFromManure > 5)
            {
                message.Instance.addWarnings("DeltaSoilN: TotalCarbonFromManure is not valid", 2);
                return -5;
            }
            if (FractionCatchCrops < 0 || FractionCatchCrops > 1)
            {
                message.Instance.addWarnings("DeltaSoilN: FractionCatchCrops is not valid", 2);
                return -6;
            }
            if (Clay < 0 || Clay > 100)
            {
                message.Instance.addWarnings("DeltaSoilN: Clay is not valid", 2);
                return -7;
            }
            ready = true;
            this.SoilChange = calculateV2(SoilCode, FarmType, PostalCode, TotalCarbonFromCrops, TotalCarbonFromManure, FractionCatchCrops, Clay);
            globalTime.Instance.stop(3);
            return 0;
        }
        */
        private decimal calculate(int SoilCode, int FarmType, int PostalCode, decimal TotalCarbonFromCrops, decimal TotalCarbonFromManure, decimal FractionCatchCrops,decimal Clay)
        {
            
            decimal fltR,soilDecay,fltTotalCarbonFromCrops;


            fltR = (decimal)(3.09+2.67*Math.Exp(-0.079*(double)Clay));
            if (fltR <=0) 
            {
                message.Instance.addWarnings("Mellemregning i delta soil N er forkert","DeltaSoilN.calculate: fltR er mindre end 0", 2);
            }
            fltR = 1m/(1m+fltR);
            fltTotalCarbonFromCrops = TotalCarbonFromCrops + 0.72m*FractionCatchCrops;// ' fraction is 0-1: areaCatchCrop/totalArea
            soilDecay = CalculateTotalDecayInSoil(SoilCode, FarmType, PostalCode);
            SoilChange = 82m * (fltR * fltTotalCarbonFromCrops + (fltR + 0.08m) * TotalCarbonFromManure - 0.01m * soilDecay) - 4;
            if (SoilChange < -200 || SoilChange > 200)
            {
                message.Instance.addWarnings("Jordpuljeaendring er usandsynlig", "DeltaSoilN: SoilChange is not valid", 3);
            }
            if (globalSettings.Instance.getExtraOutput() == true)
            {
               
                globalFilehandling.Instance.writeToDeltaSoilN(SoilCode.ToString() + '\t' + FarmType.ToString() + '\t' + PostalCode.ToString() + '\t' + TotalCarbonFromCrops.ToString() + '\t' + TotalCarbonFromManure.ToString() + '\t' + FractionCatchCrops.ToString() + '\t' + Clay.ToString() + '\t' + SoilChange.ToString());

            }

            return SoilChange;
        }
        /*
        private decimal calculateV2(int SoilCode, int FarmType, int PostalCode, decimal TotalCarbonFromCrops, decimal TotalCarbonFromManure, decimal FractionCatchCrops, decimal Clay)
        {

            decimal fltR, soilDecay, fltTotalCarbonFromCrops;
     
            fltR = (decimal)(3.0895 + 2.672 * Math.Exp(-7.86 * (double)Clay));
            if (fltR <= 0)
            {
                message.Instance.addWarnings("DeltaSoilN.calculate: fltR er mindre end 0", 2);
            }
            fltR = 1m / (1m + fltR);
            
            fltTotalCarbonFromCrops = TotalCarbonFromCrops + 0.72m * FractionCatchCrops;// ' fraction is 0-1: areaCatchCrop/totalArea
            soilDecay = CalculateTotalDecayInSoilV2(SoilCode, FarmType, PostalCode);
            SoilChange = soilDecay + 86.2546987717013m * fltR * fltTotalCarbonFromCrops + 79.1943982123104m * (fltR + 0.116m) * TotalCarbonFromManure;
            if (globalSettings.Instance.getExtraOutput() == true)
            {

                globalFilehandling.Instance.writeToDeltaSoilN(SoilCode.ToString() + '\t' + FarmType.ToString() + '\t' + PostalCode.ToString() + '\t' + TotalCarbonFromCrops.ToString() + '\t' + TotalCarbonFromManure.ToString() + '\t' + FractionCatchCrops.ToString() + '\t' + Clay.ToString() + '\t' + SoilChange.ToString());

            }
            return SoilChange;
        }
        */
        private decimal CalculateTotalDecayInSoil(int SoilCode, int FarmType, int PostalCode)
        {

            decimal returnValue;
            returnValue = -1;//70m;
            int code;
            if (globalSettings.Instance.getZipkodeError() == true)
                code = 4999;
            else
                code = 5999;
            if (PostalCode > code) // Jylland
            {
                if (FarmType == 1 ||FarmType == 2)
                {
                        if (SoilCode == 1)
                        {
                                returnValue = 50m;
                        }
                        else if (SoilCode == 2)
                        {
                                returnValue = 50.5m;
                        }
                        else if (SoilCode == 3)
                        {
                                returnValue = 57.6m;
                        }
                        else if (SoilCode == 4 || SoilCode == 11 || SoilCode == 12)
                        {
                                returnValue = 69.6m;
                        }
                        else if (SoilCode == 5)
                        {
                                returnValue = 89.9m;
                        }
                        else if (SoilCode == 6)
                        {
                                returnValue = 86.6m;
                        }
                        else if (SoilCode == 7 || SoilCode == 8 || SoilCode == 9 || SoilCode == 10)
                        {
                                returnValue = 106.5m;
                        }
                }
                if (FarmType == 3)
                {
                        if (SoilCode == 1)
                        {
                                returnValue = 59.2m;
                        }
                        else if (SoilCode == 2)
                        {
                                returnValue = 63.9m;
                        }
                        else if (SoilCode == 3)
                        {
                                returnValue = 68.8m;
                        }
                        else if (SoilCode == 4 || SoilCode == 11 || SoilCode == 12)
                        {                                
                            returnValue = 84m;
                        }
                        else if (SoilCode == 5)
                        {
                                returnValue = 111.6m;
                        }
                        else if (SoilCode == 6)
                        {
                                returnValue = 108.1m;
                        }
                        else if (SoilCode == 7 || SoilCode == 8 || SoilCode == 9 || SoilCode == 10)
                        {
                                returnValue = 132.2m;
                        }
                }
            }
            else
            {
                        if (FarmType == 1 ||FarmType == 2)
                        {
                            if (SoilCode == 1)
                            {
                                returnValue = 48.3m;
                            }
                            else if (SoilCode == 2)
                            {
                                returnValue = 47.3m;
                            }
                            else if (SoilCode == 3)
                            {
                                returnValue = 55.7m;
                            }
                            else if (SoilCode == 4 || SoilCode == 11 || SoilCode == 12)
                            {
                                returnValue = 68.2m;
                            }
                            else if (SoilCode == 5)
                            {
                                returnValue = 85.8m;
                            }
                            else if (SoilCode == 6)
                            {
                                returnValue = 81.6m;
                            }
                            else if (SoilCode == 7 || SoilCode == 8 || SoilCode == 9 || SoilCode == 10)
                            {
                                returnValue = 96.1m;
                            }

                        }
                        if (FarmType == 3)
                        {
                            if (SoilCode == 1)
                            {
                                returnValue = 58.6m;
                            }
                            if (SoilCode == 2)
                            {
                                returnValue = 62.4m;
                            }
                            if (SoilCode == 3)
                            {
                                returnValue = 68.3m;
                            }
                            if (SoilCode == 4 || SoilCode == 11 || SoilCode == 12)
                            {
                                returnValue = 84.7m;
                            }
                            if (SoilCode == 5)
                            {
                                returnValue = 110.5m;
                            }
                            if (SoilCode == 6)
                            {
                                returnValue = 106.1m;
                            }
                            if (SoilCode == 7 || SoilCode == 8 || SoilCode == 9 || SoilCode == 10)
                            {
                                returnValue = 124m;
                            }
                        }
           }
            if (returnValue == -1)
            {
                message.Instance.addWarnings("Algoritmefejl", "DeltaSoilN:ReturnValue has not been set properly", 1);
            }
            return returnValue;
        }
        private decimal CalculateTotalDecayInSoilV2(int SoilCode, int FarmType, int PostalCode)
        {

            decimal returnValue;
            returnValue = -1;
            int code;
            if (globalSettings.Instance.getZipkodeError() == true)
                code = 4999;
            else
                code = 5999;
            if (PostalCode > code) // Jylland
            {
                if (FarmType == 1 || FarmType == 2)
                {
                    if (SoilCode == 1)
                    {
                        returnValue = -48m;
                    }
                    else if (SoilCode == 2)
                    {
                        returnValue = -50m;
                    }
                    else if (SoilCode == 3)
                    {
                        returnValue = -56;
                    }
                    else if (SoilCode == 4 || SoilCode == 11 || SoilCode == 12)
                    {
                        returnValue = -69m;
                    }
                    else if (SoilCode == 5)
                    {
                        returnValue = -90m;
                    }
                    else if (SoilCode == 6)
                    {
                        returnValue = -87m;
                    }
                    else if (SoilCode == 7 || SoilCode == 8 || SoilCode == 9 || SoilCode == 10)
                    {
                        returnValue = -109;
                    }
                }
                if (FarmType == 3)
                {
                    if (SoilCode == 1)
                    {
                        returnValue = -58m;
                    }
                    else if (SoilCode == 2)
                    {
                        returnValue = -63m;
                    }
                    else if (SoilCode == 3)
                    {
                        returnValue = -68m;
                    }
                    else if (SoilCode == 4 || SoilCode == 11 || SoilCode == 12)
                    {
                        returnValue = -83m;
                    }
                    else if (SoilCode == 5)
                    {
                        returnValue = -112m;
                    }
                    else if (SoilCode == 6)
                    {
                        returnValue = -109m;
                    }
                    else if (SoilCode == 7 || SoilCode == 8 || SoilCode == 9 || SoilCode == 10)
                    {
                        returnValue = -136m;
                    }
                }
            }
            else
            {
                if (FarmType == 1 || FarmType == 2)
                {
                    if (SoilCode == 1)
                    {
                        returnValue = -46m;
                    }
                    else if (SoilCode == 2)
                    {
                        returnValue = -45.2m;
                    }
                    else if (SoilCode == 3)
                    {
                        returnValue = -53m;
                    }
                    else if (SoilCode == 4 || SoilCode == 11 || SoilCode == 12)
                    {
                        returnValue = -65m;
                    }
                    else if (SoilCode == 5)
                    {
                        returnValue = -85m;
                    }
                    else if (SoilCode == 6)
                    {
                        returnValue = -80m;
                    }
                    else if (SoilCode == 7 || SoilCode == 8 || SoilCode == 9 || SoilCode == 10)
                    {
                        returnValue = -96m;
                    }

                }
                if (FarmType == 3)
                {
                    if (SoilCode == 1)
                    {
                        returnValue = -56m;
                    }
                    if (SoilCode == 2)
                    {
                        returnValue = -60m;
                    }
                    if (SoilCode == 3)
                    {
                        returnValue = -65m;
                    }
                    if (SoilCode == 4 || SoilCode == 11 || SoilCode == 12)
                    {
                        returnValue =-82m;
                    }
                    if (SoilCode == 5)
                    {
                        returnValue = -111m;
                    }
                    if (SoilCode == 6)
                    {
                        returnValue = -105m;
                    }
                    if (SoilCode == 7 || SoilCode == 8 || SoilCode == 9 || SoilCode == 10)
                    {
                        returnValue = -125m;
                    }
                }
            }
            if (returnValue == -1)
            {
                message.Instance.addWarnings("Algoritmefejl","DeltaSoilN:ReturnValue has not been set properly", 2);
            }
            return returnValue;
        }
        /// <summary>
        /// get the soil n changes
        /// it will trogh a type 1 warning if it has not been initlized
        /// </summary>
        /// <returns>the Soil n changes</returns>
        public decimal getSoilChange()
        {
            if (ready == false)
            {
                message.Instance.addWarnings("Algoritmefejl", "DeltaSoilN: Has not been init before receiving output", 2);
                return -1;

            }
            return this.SoilChange;
        }
    }
}
