using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;

namespace FarmN_2010
{
    /// <summary>
    /// A class for handling warrnings and error
    /// </summary>
    public sealed  class message
    {

      

        private static List<error> warningsList = new List<error>();
        private static readonly message _instance = new message();
        private message()
        {
          
        }
        /// <summary>
        /// return all warning
        /// </summary>
        /// <returns>all warnings made so far</returns>
        public List<error> getwarningsList()
        {
            return warningsList;
        }
        /// <summary>
        /// Return a message class so one can use its function
        /// </summary>
        public static message Instance
        {
            get
            {
                return _instance;
            }
        }

      /// <summary>
        /// Add a warrning/error
        /// There are:
        /// 1: Large Enduser error. Will trough a ArgumentException
        /// 2: Large System error. Will trough a ArgumentException
        /// 3: Small Enduser Error. The program will continue but the result will not be garanteret
        /// 4: Small System Error. The program will continue but the result will not be garanteret
      /// </summary>
        /// <param name="warning">warning Enduser intended warning.</param>
        /// <param name="programError">programError debug information.</param>
        /// <param name="type">Types of warning. See above</param>
        public void addWarnings(string warning,string programError, int type)
        {
            error oneError = new error(warning, programError, type);
            warningsList.Add(oneError);
            if (type == 1 || type == 2)
            {
                throw new ArgumentException("Message: Cannot handle exeption with name \"" + programError + "\" and type " + type);
            }
        }
        /// <summary>
        /// remove all warnings. Should be call after each call to webservice
        /// </summary>
        public void reset()
        {
            warningsList = new List<error>();
        }
        /// <summary>
        /// If basic output is enablet this function will write all warnings out to C:\\farmnWarnings.txt
        /// </summary>
        public void WriteToFile()
        {
            if (globalSettings.Instance.getBasicOutput() == true)
            {
                TextWriter tw = null;
                try
                {

                    tw = new StreamWriter("C:\\farmnWarnings.txt");
                    for (int i = 0; i < warningsList.Count(); i++)
                    {
                        string output = warningsList.ElementAt(i).getErrorMessage() + " : " + warningsList.ElementAt(i).getProgramError()+" : "+warningsList.ElementAt(i).getErrorType().ToString();
                        tw.WriteLine(output);

                    }

                    tw.Flush();
                    tw.Close();

                }
                catch (Exception ex)
                {
                }
                finally
                {
                    tw.Close();
                }
            }
        }
    }
}
