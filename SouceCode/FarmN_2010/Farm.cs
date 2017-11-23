using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FarmN_2010
{
   
    /// <summary>
    /// This is a data class holding information for Farm
    /// </summary>
    public class Farm
    {
        private long FarmID;
        private int FarmZipCode;
        private int FarmType;
        private int ManureVersion;
        /// <summary>
        /// Scenariolist is a list of Scenario that is associated with a single instance of Farm
        /// </summary>
        public Scenario Scenario;//= new Scenario();
        /// <summary>
        /// DeliveryList is a list of Delivery that is associated with a single instance of Farm
        /// </summary>
        public List<Farm_Delivery> DeliveryList = new List<Farm_Delivery>();
        /// <summary>
        /// Should not be used instance since we dont want to have a Farm with no values
        /// </summary>
        private Farm()
        {
        }
        /// <summary>
        /// the cunstructor 
        /// </summary>
        /// <param name="FarmID">The farm ID</param>
        /// <param name="FarmZipCode">the zip code</param>
        /// <param name="FarmType">Farm type</param>
        /// <param name="ManureVersion">The manure version</param>
        public Farm(long FarmID, int FarmZipCode, int FarmType, int ManureVersion)
        {
            this.FarmID = FarmID;
            this.FarmZipCode = FarmZipCode;
            this.FarmType = FarmType;
            this.ManureVersion = ManureVersion;
        }
        /// <summary>
        /// getting the farm type
        /// </summary>
        /// <returns>farm type</returns>
        public int getFarmtype()
        {
            return FarmType;
        }
        /// <summary>
        /// setting the farm type
        /// </summary>
        /// <param name="FarmType">the new farm type</param>
        public void setFarmType(int FarmType)
        {
            this.FarmType = FarmType;
        }
    }
}
