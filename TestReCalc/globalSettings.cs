using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FarmN_2010
{
    public class globalSettings
    {
        private static readonly globalSettings _instance = new globalSettings();
        private bool oldbugs;
        private globalSettings()
        {
            oldbugs = false;
        }
        public static globalSettings Instance
        {
            get
            {
                return _instance;
            }
        }
        public bool getOldBugs()
        {
            return oldbugs;
        }
        public void setOldBugs(bool oldBugs)
        {
            this.oldbugs=oldBugs;
        }
    }
}
