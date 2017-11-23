
using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Linq;
using System.Web;

using System.Xml.Linq;
using System.IO;
using System.Collections.Generic;

using System.Text;
using System.Net;

namespace FarmN_2010
{
    public class DeltaSoilN
    {
        private decimal SoilChange;
        private bool ready;
        private StreamWriter twCsharp;
        public DeltaSoilN()
        {
            FileInfo imgInfoCsharp = new FileInfo("c:\\DeltaSOILN.xls");
            try
            {
                imgInfoCsharp.Delete();


                
            }
            catch (Exception e)
            {
            }
            twCsharp = File.AppendText("c:\\DeltaSOILN.xls");

        }
        /// <summary>
        /// Initializing values for NLes
        /// return: -1 if something went wrong or 0 if it is OK
        /// </summary>
        public void close()
        {
            twCsharp.Close();

        }
        public void print()
        {
            try
            {
                twCsharp.WriteLine("SoilCode" + '\t' + "FarmType" + '\t' + "PostalCode" + '\t' + "TotalCarbonFromCrops" + '\t' + "TotalCarbonFromManure" + '\t' + "FractionCatchCrops" + '\t' + "Clay" + '\t' + "C#old" +'\t' + "database" + '\t' + "cnew" + '\t' + "database");

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
        public int init(int SoilCode, int FarmType, int PostalCode, decimal TotalCarbonFromCrops, decimal TotalCarbonFromManure, decimal FractionCatchCrops, decimal Clay)
        {
            if (SoilCode < 1 || SoilCode>12)
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
            if (TotalCarbonFromCrops < 0|| TotalCarbonFromCrops > 7)
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
            if (Clay < 0 || Clay >100)
            {
                message.Instance.addWarnings("DeltaSoilN: Clay is not valid", 2);
                return -7;
            }
            ready = true;
            twCsharp.Write(SoilCode.ToString() + '\t' + FarmType.ToString() + '\t' + PostalCode.ToString() + '\t' + TotalCarbonFromCrops.ToString() + '\t' + TotalCarbonFromManure.ToString() + '\t' + FractionCatchCrops.ToString() + '\t' + Clay.ToString());
            globalSettings.Instance.setOldBugs(true);
            this.SoilChange = calculate(SoilCode, FarmType, PostalCode, TotalCarbonFromCrops, TotalCarbonFromManure, FractionCatchCrops, Clay);
            globalSettings.Instance.setOldBugs(false);
            this.SoilChange = calculate(SoilCode, FarmType, PostalCode, TotalCarbonFromCrops, TotalCarbonFromManure, FractionCatchCrops, Clay);
            twCsharp.WriteLine();
            return 0;
        }

        private decimal calculate(int SoilCode, int FarmType, int PostalCode, decimal TotalCarbonFromCrops, decimal TotalCarbonFromManure, decimal FractionCatchCrops,decimal Clay)
        {
            
            decimal fltR,soilDecay,fltTotalCarbonFromCrops;


            fltR = (decimal)(3.09+2.67*Math.Exp(-0.079*(double)Clay));
            fltR = 1m/(1m+fltR);
            fltTotalCarbonFromCrops = TotalCarbonFromCrops + 0.72m*FractionCatchCrops;// ' fraction is 0-1: areaCatchCrop/totalArea
            soilDecay = CalculateTotalDecayInSoil(SoilCode, FarmType, PostalCode);
            SoilChange = 82m * (fltR * fltTotalCarbonFromCrops + (fltR + 0.08m) * TotalCarbonFromManure - 0.01m * soilDecay) - 4;
            twCsharp.Write('\t'+SoilChange.ToString());
            WebserviceResponse(SoilCode, FarmType, PostalCode, TotalCarbonFromCrops, TotalCarbonFromManure, FractionCatchCrops, Clay);
            return SoilChange;
        }
        public void WebserviceResponse(int SoilCode, int FarmType, int PostalCode, decimal TotalCarbonFromCrops, decimal TotalCarbonFromManure, decimal FractionCatchCrops, decimal Clay)
        {
            string url = "http://172.20.107.138/FarmN/CalculateDeltaSoilN.asp?SoilCode=" + SoilCode + "&FarmType=" + FarmType + "&TotalCarbonFromCrops=" + TotalCarbonFromCrops + "&TotalCarbonFromManure=" + TotalCarbonFromManure + "&PostalCode=" + PostalCode + "&fractionCatchCrops=" + FractionCatchCrops;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            System.IO.Stream resStream = response.GetResponseStream();
            StringBuilder sb = new StringBuilder();
            byte[] buf = new byte[8192];
            string tempString = null;
            int count = 0;

            do
            {
                // fill the buffer with data
                count = resStream.Read(buf, 0, buf.Length);

                // make sure we read some data
                if (count != 0)
                {
                    // translate from bytes to ASCII text
                    tempString = Encoding.ASCII.GetString(buf, 0, count);

                    // continue building the string
                    sb.Append(tempString);
                }
            }
            while (count > 0); // any more data to read?
            string output = sb.ToString();

            twCsharp.Write('\t'+output);

            response.Close();
        }
        private decimal CalculateTotalDecayInSoil(int SoilCode, int FarmType, int PostalCode)
        {

            decimal returnValue;
            returnValue = -1;//70m;
            int code;
            if (globalSettings.Instance.getOldBugs() == true)
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
              message.Instance.addWarnings("DeltaSoilN:ReturnValue has not been set properly", 1);
            }
            return returnValue;
        }

        public decimal getSoilChange()
        {
            if (ready == false)
            {
                message.Instance.addWarnings("DeltaSoilN: Has not been init before receiving output", 1);
                return -1;

            }
            return this.SoilChange;
        }
    }
}
