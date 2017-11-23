using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
namespace FarmN_2010
{
    /// <summary>
    /// this class keeps track of witch old bug should be enablet or disablet and if the farmNWebserver should 
    /// write to log or not
    /// </summary>
    public class globalSettings
    {
        private static readonly globalSettings _instance = new globalSettings();

        private bool extraOutput;
        private bool basicOutput;
        private bool ZipkodeError;
        private bool MissingPreCropCoeffInDatabaseError;
        private bool DeltaSoilNStrawDMRemovedError;
        private bool RoundedValuesError;
        private globalSettings()
        {

            extraOutput = false;
            basicOutput = false;
            setVersion(3);
        }
        /// <summary>
        /// enable/disable old bugs based on version nr 
        /// </summary>
        /// <param name="verionNumber"> version number</param>
        public void setVersion(int versionNumber)
        {
            if (versionNumber==1)
            {
                ZipkodeError = true;
                MissingPreCropCoeffInDatabaseError = true;
                DeltaSoilNStrawDMRemovedError = true;
                RoundedValuesError = true;
            }
            else if (versionNumber == 2)
            {
                ZipkodeError = true;
                MissingPreCropCoeffInDatabaseError = true;
                DeltaSoilNStrawDMRemovedError = true;
                RoundedValuesError = true;
            }
            else if (versionNumber == 3)
            {
                ZipkodeError = true;
                MissingPreCropCoeffInDatabaseError = true;
                DeltaSoilNStrawDMRemovedError = true;
                RoundedValuesError = true;
            }
            else if (versionNumber == 4)
            {
                ZipkodeError = false;
                MissingPreCropCoeffInDatabaseError = false;
                DeltaSoilNStrawDMRemovedError = false;
                RoundedValuesError = false;
            }
            else
            {
                message.Instance.addWarnings("Kald af ikke-oprettet version ", "non eksisting version",1);
            }
        }
        /// <summary>
        /// use this to access globalSettings 
        /// </summary>
        public static globalSettings Instance
        {
            get
            {
                return _instance;
            }
        }
        /// <summary>
        /// in the old version of farmN they used wrong zipcode. This function will return true if this old behavior
        /// should still be prestent
        /// </summary>
        /// <returns></returns>
        public bool getZipkodeError()
        {
            return ZipkodeError;
        }
        
        /// In the old version k13 CropCoeff was missing in the database. This function will tell you if this old behavior
        /// should be disablet or not
        /// </summary>
        /// <returns>if true the k13 values will not be used</returns>
        public bool getMissingPreCropCoeffInDatabaseError()
        {
            return MissingPreCropCoeffInDatabaseError;
        }
        /// <summary>
        /// checks if farmN webserver should write extra output
        /// </summary>
        /// <returns>if true then write to file</returns>
        public bool getExtraOutput()
        {
            return this.extraOutput;
        }
        /// <summary>
        /// checks if farmN webserver should write basic output
        /// </summary>
        /// <returns>if true then write to file</returns>
        public bool getBasicOutput()
        {
            return this.basicOutput;
        }
        /// <summary>
        /// Disable or enable a bug related to N in straw
        /// </summary>
        /// <returns>if true then it will use the old version</returns>
        public bool getDeltaSoilNStrawDMRemovedError()
        {
            return DeltaSoilNStrawDMRemovedError;
        }
        /// <summary>
        /// Disable or enable extra output
        /// </summary>
        /// <param name="input">if true then there will come extra output in files</param>
        public void setExtraOutput(bool input)
        {
            this.extraOutput = input;
        }
        /// <summary>
        /// Checks if distrobution of manure should be rounded or not
        /// </summary>
        /// <returns>if true then round</returns>
        public bool getRoundedValuesError()
        {
            return this.RoundedValuesError;
        }
       
    }
}
