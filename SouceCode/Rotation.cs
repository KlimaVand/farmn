using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FarmN_2010
{
    /// <summary>
    /// This is a data class holding information for Rotation
    /// </summary>
    public class Rotation
    {
        private String RotationName;
        private int soilType;
        private decimal clayRatio;
        private decimal YieldLevel;
        private decimal CropFixation;
        private decimal SecondCropFixation;
        private decimal deltaSoiln;
        private decimal simden;
        private decimal humus;
        private string Referencesaedskifte;
        private string Saedskifte;
        private int ArealType;
        /// <summary>
        /// FieldPlanList is a list of FieldPlan that is associated with a single instance of Rotation
        /// </summary>
        public List<FieldPlan> FieldPlanList = new List<FieldPlan>();
        /// <summary>
        /// FieldPlanRotationList is a list of FieldPlanRotation that is associated with a single instance of Rotation
        /// </summary>
        public List<FieldPlanRotation> FieldPlanRotationList = new List<FieldPlanRotation>();
        /// <summary>
        /// Should not be used instance since we dont want to have a Rotation with no values
        /// </summary>
        private Rotation()
        {

        }
        /// <summary>
        /// a constructor with arguments
        /// </summary>
        /// <param name="RotationName">The name of the rotation </param>
        /// <param name="soilType">The soil type</param>
        /// <param name="clayRatio">Ratio of clay on the soil</param>
        /// <param name="YieldLevel">the Yield</param>
        /// <param name="humus">The humus in the soil</param>
        /// <param name="Referencesaedskifte">The reference rotation</param>
        /// <param name="Saedskifte">The rotation</param>
        /// <param name="ArealType">The area type</param>
        public Rotation(String RotationName, int soilType, decimal clayRatio, decimal YieldLevel,decimal humus,string Referencesaedskifte, string Saedskifte, int ArealType)
        {
            this.humus = humus;
            this.RotationName = RotationName;
            this.soilType = soilType;
            this.clayRatio = clayRatio;
            this.YieldLevel = YieldLevel;
            deltaSoiln = -1;
            simden = -1;
            this.Referencesaedskifte= Referencesaedskifte;
            this.Saedskifte= Saedskifte;
            this.ArealType = ArealType;
        }
        /// <summary>
        /// a get function for eference rotation
        /// </summary>
        /// <returns>reference rotation</returns>
        public string GetReferencesaedskifte()
        {
          return  Referencesaedskifte;
        }
        /// <summary>
        /// a get function for rotation
        /// </summary>
        /// <returns>the rotation</returns>
        public string GetSaedskifte()
        {
            return Saedskifte;
        }
        /// <summary>
        /// a get function for area type 
        /// </summary>
        /// <returns>area type</returns>
        public int getArealType()
        {
            return this.ArealType;
        }
        /// <summary>
        /// a get function for clay ratio
        /// </summary>
        /// <returns>The Clay ratio</returns>
        public decimal getClayRatio()
        {
            return this.clayRatio;
        }
        /// <summary>
        /// a get function for humus ratio
        /// </summary>
        /// <returns>The humus ratio</returns>
        public decimal getHumusRatio()
        {
            return this.humus;
        }
       /// <summary>
       /// a set function for Delta soil N
       /// </summary>
       /// <param name="input">The new Delta soil N</param>
        public void setDeltaSoilN(decimal input)
        {
            this.deltaSoiln = input;
        }
        /// <summary>
        /// a set function for SimdDen
        /// </summary>
        /// <param name="input">The new Simden</param>
        public void setSimden(decimal input)
        {
            this.simden = input;
        }
        /// <summary>
        ///  a get function for Delta soil N
        /// </summary>
        /// <returns>Delta SoilN</returns>
        public decimal getDeltaSoilN()
        {
            return deltaSoiln;
        }
        /// <summary>
        ///  a get function for SimDen
        /// </summary>
        /// <returns>SimDen</returns>
        public decimal getSimden()
        {
            return simden;
        }
        /// <summary>
        ///  a get function for rotatin Name
        /// </summary>
        /// <returns>rotatin Name</returns>
        public string getRotationName()
        {
            return RotationName;
        }
        /// <summary>
        ///  a get function for soil Type
        /// </summary>
        /// <returns>soil Type</returns>
        public int getSoilType()
        {
            return soilType;
        }
        /// <summary>
        /// A set tunction for Crop Fixation
        /// </summary>
        /// <param name="CropFixation">The new Crop Fixation</param>
        public void setCropFixation(decimal CropFixation)
        {
            this.CropFixation = CropFixation;
        }
        /// <summary>
        /// a get function for crop fixation
        /// </summary>
        /// <returns></returns>
        public decimal getCropFixation()
        {
            return CropFixation;
        }
        /// <summary>
        /// a set function for Second Crop Fixation
        /// </summary>
        /// <param name="SecondCropFixation">the new Second Crop Fixation</param>
        public void setSecondCropFixation(decimal SecondCropFixation)
        {
            this.SecondCropFixation = SecondCropFixation;
        }
        /// <summary>
        /// a get function for Second Crop Fixation
        /// </summary>
        /// <returns>Second Crop Fixation</returns>
        public decimal getSecondCropFixation()
        {
            return SecondCropFixation;
        }
       

    }
}
