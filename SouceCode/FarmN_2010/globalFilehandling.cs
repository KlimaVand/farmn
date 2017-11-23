using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
namespace FarmN_2010
{
    /// <summary>
    /// assisting in loggin of data
    /// </summary>
    public class globalFilehandling
    {
        private string nLesOutput;
        private string simdenOutput;
        private string DeltaSoilNOutput;


        private globalFilehandling()
        {
            
                nLesOutput = "c:\\dateNles.xls";
                simdenOutput = "c:\\dateSIMDENCsharp.xls";
                DeltaSoilNOutput = "c:\\DeltaSOILN.xls";

            
        }
        /// <summary>
        /// append a string to "c:\\dateNles.xls";
        /// </summary>
        /// <param name="input">string that should be appended</param>
        public void writeToNless(String input)
        {
            if (globalSettings.Instance.getExtraOutput() == true)
            {
                StreamWriter nLes;
                nLes = File.AppendText(nLesOutput);
                nLes.WriteLine(input);
                nLes.Close();
            }

           

        }
        /// <summary>
        /// append a string to "c:\\dateSIMDENCsharp.xls";
        /// </summary>
        /// <param name="input">string that should be appended</param>
        public void writeToDeltaSoilN(String input)
        {
            if (globalSettings.Instance.getExtraOutput() == true)
            {
                StreamWriter deltaSoilN;
                deltaSoilN = File.AppendText(DeltaSoilNOutput);
                deltaSoilN.WriteLine(input);
                deltaSoilN.Close();
            }


            
        }
        /// <summary>
        /// append a string to "c:\\DeltaSOILN.xls"
        /// </summary>
        /// <param name="input">string that should be appended</param>
        public void writeToSimden(String input)
        {
            if (globalSettings.Instance.getExtraOutput() == true)
            {
                StreamWriter simden;
                simden = File.AppendText(simdenOutput);
                simden.WriteLine(input);
                simden.Close();
     
            }
            
        }

        private static readonly globalFilehandling _instance = new globalFilehandling();
        /// <summary>
        /// getting the global globalFilehandling
        /// </summary>
        public static globalFilehandling Instance
        {
            
            get
            {
                return _instance;
            }
        }


    }
}
