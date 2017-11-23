using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FarmN_2010
{
    /// <summary>
    /// This is a data class holding information for Scenario
    /// </summary>
    public class Scenario
    {
        private int ScenarioID;
        private string ScenarioName;
        private int CropYear;
        private decimal NNeedPercent;
        /// <summary>
        /// RotationList is a list of Rotation that is associated with a single instance of Scenario
        /// </summary>
        public List<Rotation> RotationList= new List<Rotation>();
        /// <summary>
        /// ManureList is a list of FieldPlanRotation that is BoughtManure with a single instance of Scenario
        /// </summary>
        public List<BoughtManure> ManureList = new List<BoughtManure>();
        /// <summary>
        /// Should not be used instance since we dont want to have a Scenario with no values
        /// </summary>
        private Scenario()
        {
        }
        /// <summary>
        /// the Scenario constructor
        /// </summary>
        /// <param name="aScenarioID">The ID of the Scenario</param>
        /// <param name="aScenarioName">The name of Scenario</param>
        /// <param name="aCropYear">The year that crop need to be harvested </param>
        /// <param name="aNNeedPercent">How much the crop needs N</param>
        public Scenario(int aScenarioID, string aScenarioName,int aCropYear,decimal aNNeedPercent)
        {
            this.ScenarioID = aScenarioID;
            this.ScenarioName = aScenarioName;
            this.CropYear = aCropYear;
            this.NNeedPercent = aNNeedPercent;
        }
        /// <summary>
        /// returning the N need %
        /// </summary>
        /// <returns>The N need %</returns>
        public decimal getNNeedPercent()
        {
            return NNeedPercent;
        }
    }
}
