using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FarmN_2010
{
    /// <summary>
    /// This is a data class holding information for FieldPlan
    /// </summary>
    public class FieldPlan
    {
        private int Crop;
        private string CropName;
        private decimal Area, N_InSeed;
        private int AfterCropID;
        private bool StatutoryAfterCropBasis, SpringSown, CanHaveAfterCrop;
        /// <summary>
        /// Should not be used instance since we dont want to have a FieldPlan with no values
        /// </summary>
        private FieldPlan()
        {
        }
        public FieldPlan(int Crop, string CropName, decimal Area, int AfterCropID, bool StatutoryAfterCropBasis, bool SpringSown, bool CanHaveAfterCrop, decimal N_InSeed)
        {
            this.Crop = Crop;
            this.CropName = CropName;
            this.Area = Area;
            this.AfterCropID = AfterCropID;
            this.StatutoryAfterCropBasis = StatutoryAfterCropBasis;
            this.SpringSown = SpringSown;
            this.CanHaveAfterCrop = CanHaveAfterCrop;
            this.N_InSeed = N_InSeed;
        }
        public int getCrop()
        {
            return Crop;
        }
        public string getCropName()
        {
            return CropName;
        }
        public void setCropName(string cropName)
        {
            CropName = cropName;
        }
        public decimal getArea()
        {
            return Area;
        }
        public int getAfterCropID()
        {
            return AfterCropID;
        }
        public bool getStatutoryAfterCropBasis()
        {
            return StatutoryAfterCropBasis;
        }
        public bool getSpringSown()
        {
            return SpringSown;
        }
        public bool getCanHaveAfterCrop()
        {
            return CanHaveAfterCrop;
        }
        public void setArea(decimal Area)
        {
            this.Area = Area;
        }
        public void setAfterCropID(int AfterCropID)
        {
            this.AfterCropID = AfterCropID;
        }
        public void setCrop(int CropID)
        {
            this.Crop = CropID;
        }
        public void setStatutoryAfterCropBasis(bool isStatutoryAfterCropBasis)
        {
            this.StatutoryAfterCropBasis = isStatutoryAfterCropBasis;
        }
        public void setSpringSown(bool isSpringSown)
        {
            this.SpringSown = isSpringSown;
        }
        public void setCanHaveAfterCrop(bool canHaveAfterCrop)
        {
            this.CanHaveAfterCrop = canHaveAfterCrop;
        }
        public void setNInSeed(decimal N_InSeed)
        {
            this.N_InSeed = N_InSeed;
        }
        public decimal getNInSeed()
        {
            return N_InSeed;
        }
    }
}
