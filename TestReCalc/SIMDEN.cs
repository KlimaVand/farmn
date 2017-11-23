using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

namespace FarmN_2010
{
    /// <summary>
    /// This class calculates a float giving the total estimated denitrification (kg N) using Finn P. Vinthers method (version 2.0)
    /// </summary>
    public class SIMDEN
    {
        int i = 0;
        private StreamWriter twCsharp;

        private decimal DenitrificationPrRotation;
        /// <summary>
        /// An empy contructor
        /// </summary>
        public SIMDEN()
        {
            FileInfo imgInfoCsharp = new FileInfo("c:\\dateSIMDENCsharp.xls");
            try
            {
                imgInfoCsharp.Delete();


                
            }
            catch (Exception e)
            {
            }
            twCsharp = File.AppendText("c:\\dateSIMDENCsharp.xls");

        }
        public void close()
        {
            twCsharp.Close();

        }
        public void print()
        {
            try
            {
                twCsharp.WriteLine("SoilCode" + '\t' + "FarmType" + '\t' + "FertiliserN" + '\t' + "ManureNincorp" + '\t' + "ManureNspread" + '\t' + "NFixation" + '\t' + "fert" + '\t' + "fltMan" + '\t' + "fltFix" + '\t' + "DenitrificationPrRotation");
               
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
        /// <summary>
        /// Initializing values for SIMDEN
        /// return: -1 if something went wrong or 0 if it is OK
        /// </summary>
        public int init(int SoilCode, int FarmType, decimal FertiliserN, decimal ManureNincorp, decimal ManureNspread, decimal NFixation)
        {
            
            if(FertiliserN < 0)
            {
                message.Instance.addWarnings("SIMDEN: FertiliserN is not valid",2); 
                return -1;
            }
            if (ManureNincorp < 0)
            {
                message.Instance.addWarnings("SIMDEN: ManureNincorp is not valid",2); 
                return -1;
            }
            if (ManureNspread < 0)
            {
                message.Instance.addWarnings("SIMDEN: ManureNspread is not valid",2); 
                return -1;
            }
            if (NFixation < 0)
            {
                message.Instance.addWarnings("SIMDEN: NFixation is not valid",2); 
                return -1;
            }

            if (FarmType != 1 && FarmType != 2 && FarmType != 3)
            {
                message.Instance.addWarnings("SIMDEN: FarmType is not valid",2);
                return -1;
            }
            if (1 > SoilCode)
            {
                message.Instance.addWarnings("SIMDEN: SoilType is not valid",2); 
                return -1;
            }
            
          return calculate(SoilCode, FarmType, FertiliserN, ManureNincorp, ManureNspread, NFixation);

        }
        /// <summary>
        /// Calculate SIMDEN
        /// </summary>
        private int calculate(int SoilCode, int FarmType, decimal FertiliserN, decimal ManureNincorp, decimal ManureNspread, decimal NFixation)
        {
            decimal fltBackgr = 0m;
            decimal fltRatio = 0m;
            if (FarmType == 1 || FarmType == 2)
            {

                switch (SoilCode)
                {

                    case 1:
                        fltBackgr = 0.0m;
                        fltRatio = 0.0m;
                        break;
                    case 2:
                        fltBackgr = 0.5m;
                        fltRatio = 0.5m;
                        break;
                    case 3:
                        fltBackgr = 1.4m;
                        fltRatio = 1.5m;
                        break;
                    case 4:
                        fltBackgr = 2.8m;
                        fltRatio = 2.5m;
                        break;
                    case 5:
                        fltBackgr = 4.8m;
                        fltRatio = 3.0m;
                        break;
                    case 6:
                        fltBackgr = 7.3m;
                        fltRatio = 4.0m;
                        break;
                    case 7:
                        fltBackgr = 10.2m;
                        fltRatio = 5.0m;
                        break;
                    default:
                        fltBackgr = 14.0m;
                        fltRatio = 6.0m;
                        break;

                }
            }
            if (FarmType == 3)
            {
                switch (SoilCode)
                {

                    case 1:
                        fltBackgr = 0.8m;
                        fltRatio = 0.5m;
                        break;
                    case 2:
                        fltBackgr = 1.8m;
                        fltRatio = 1.5m;
                        break;
                    case 3:
                        fltBackgr = 3.3m;
                        fltRatio = 2.5m;
                        break;
                    case 4:
                        fltBackgr = 6.6m;
                        fltRatio = 4.5m;
                        break;
                    case 5:
                        fltBackgr = 10.8m;
                        fltRatio = 5.0m;
                        break;
                    case 6:
                        fltBackgr = 14.4m;
                        fltRatio = 6.0m;
                        break;
                    case 7:
                        fltBackgr = 18.4m;
                        fltRatio = 7.0m;
                        break;
                    default:
                        fltBackgr = 27.0m;
                        fltRatio = 8.0m;
                        break;

                }

            }

            decimal fert;
            fert = FertiliserN * 0.008m * (fltRatio + 1.0m);
            decimal fltMan;
            fltMan = 0.7m * ManureNincorp * 0.025m * (fltRatio + 1.5m) + 0.5m * ManureNspread * 0.025m * (fltRatio + 1.5m);
            decimal fltFix;
            fltFix = NFixation * 0.4m * 0.025m * (fltRatio + 1.5m);
            DenitrificationPrRotation = fltBackgr + fert + fltMan + fltFix;
            if (DenitrificationPrRotation < 0)
            {
                message.Instance.addWarnings("SIMDEN: DenitrificationPrRotation is not valid", 2);
                return -1;
            }

            twCsharp.Write(SoilCode.ToString() + '\t' + FarmType.ToString() + '\t' + FertiliserN.ToString() + '\t' + ManureNincorp.ToString() + '\t' + ManureNspread.ToString() + '\t' + NFixation.ToString() + '\t' + fert.ToString() + '\t' + fltMan.ToString() + '\t' + fltFix.ToString() + '\t' + DenitrificationPrRotation.ToString() + '\t');
            WebserviceResponse(SoilCode,FarmType,FertiliserN,ManureNincorp,ManureNspread,NFixation);
            return 0;
        }
        public void WebserviceResponse(int SoilCode, int FarmType, decimal FertiliserN, decimal ManureNincorp, decimal ManureNspread, decimal NFixation)
        {
            string url = "http://172.20.107.138/FarmN/CalculateSimDen.asp?SoilCode=" + SoilCode + "&FarmType=" + FarmType + "&FertilizerN=" + FertiliserN + "&ManureNincorp=" + ManureNincorp + "&ManureNspread=" + ManureNspread + "&Fixation=" + NFixation + "";
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
            string formatOutput = output.Substring(9, output.Length - 9);

            twCsharp.WriteLine(formatOutput);
            
            response.Close();
        }
        /// <summary>
        /// Return Denitrification
        /// </summary>
        public float getDenitrification()
        {
            return (float)DenitrificationPrRotation;
        }
    }
}
