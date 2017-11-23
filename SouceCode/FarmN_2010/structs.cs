using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FarmN_2010
{

    public struct error
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
    /*
             * This struct holds the return values for FarmN Webservice

             */
    public struct FarmNData
    {
        private string Referencesaedskifte;
        private string Saedskifte;
        private string Jordbundstype;
        private int Arealtype;
        private decimal NLeach_KgN_ha;
        private decimal NLeach_mgN_l;


        public FarmNData(string Referencesaedskifte, string Saedskifte, string Jordbundstype, int Arealtype, decimal NLeach_KgN_ha, decimal NLeach_mgN_l)
        {
            this.Referencesaedskifte = Referencesaedskifte;
            this.Saedskifte = Saedskifte;
            this.Jordbundstype = Jordbundstype;
            this.Arealtype = Arealtype;
            this.NLeach_KgN_ha = NLeach_KgN_ha;
            this.NLeach_mgN_l = NLeach_mgN_l;

        }
        public string getReferencesaedskifte()
        {
            return Referencesaedskifte;
        }
        public string getSaedskifte()
        {
            return Saedskifte;
        }
        public string getJordbundstype()
        {
            return Jordbundstype;
        }
        public int getArealtype()
        {
            return Arealtype;
        }
        public decimal getNLeachmgNl()
        {
            return NLeach_mgN_l;
        }

    }
}
