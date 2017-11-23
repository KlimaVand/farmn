using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;

namespace FarmN_2010
{
    public sealed  class message
    {

        private struct error
        {
            private int ErrorType;
            private string ErroMessage;

            public error(int ErrorType, string ErroMessage)
            {
                this.ErrorType = ErrorType;
                this.ErroMessage = ErroMessage;
            }
            public string getErrorMessage()
            {
                return ErroMessage;
            }
            public int getErrorType()
            {
                return ErrorType;
            }
        }

        private static List<error> warningsList = new List<error>();
        private static readonly message _instance = new message();
        private message()
        {
          
        }
        public static message Instance
        {
            get
            {
                return _instance;
            }
        }
        public void addWarnings(string warning, int type)
        {
            error oneError = new error(type, warning);
            warningsList.Add(oneError);
            if (type == 1 || type == 1)
            {
                throw new ArgumentException("Message: Cannot handle exeption with name \"" + warning+"\" and type "+type);
            }
        }

        ~message()
        {
            TextWriter tw=null;
            try
            {

                tw = new StreamWriter("C:\\farmn.txt");
                for (int i = 0; i < warningsList.Count(); i++)
                {
                    string output = warningsList.ElementAt(i).getErrorMessage();
                    output = output + warningsList.ElementAt(i).getErrorType().ToString();
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
