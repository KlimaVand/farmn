using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FarmN_2010
{
    /// <summary>
    /// This is a data class holding information for FieldPlanRotation
    /// </summary>
    public class FieldPlanRotation
    {
        private int PreviousCrop, PreCropOriginalID,PreCrop_afterCrop,PreCropSecondCrop, Crop, CropAfterCrop, OrganicFertilizer, StrawUseType, SecondCropID,PrePreCropID, ID, soilType;
        private decimal area, UseGrazing, SalePart, N_LesKgPrHa,N_LesMgPrL, CropFixation, SecondCropFixation, CropCoeff, PreCropCoeff, Runoff, PotentialDMDeposition, GrazingManure;
        private decimal CropYield_N, SecondCropYield_N, StrawYield_N, StrawRemoved_N, StrawDMRemoved;
        private double FieldNNeed;
        /// <summary>
        /// ManureFertilizerDeliveryList is a list of ManureFertilizerDelivery that is associated with a single instance of FieldPlanRotation
        /// </summary>
        public List<ManureFertilizerDelivery> ManureFertilizerDeliveryList = new List<ManureFertilizerDelivery>();
        /// <summary>
        /// Should not be used instance since we dont want to have a FieldPlanRotation with no values
        /// </summary>
        public double[] DeliveryList;
        public double[] CropUtilList;
        public double[] LossList;
        private FieldPlanRotation()
        {
        }
        public FieldPlanRotation(int ID,int PreviousCrop, int PreCropOriginalID, int PreCrop_afterCrop, int PreCropSecondCrop, int Crop, int CropAfterCrop, int OrganicFertilizer, int StrawUseType, int SecondCropID, int PrePreCropID, decimal area, decimal UseGrazing, decimal SalePart, decimal CropFixation, decimal SecondCropFixation, decimal CropCoeff, decimal PreCropCoeff, decimal Runoff, decimal PotentialDMDeposition, double FieldNNeed, decimal CropYield_N, decimal SecondCropYield_N, decimal StrawYield_N, decimal StrawRemoved_N, decimal StrawDMRemoved, int soilType, double[] DeliveryList, double[] CropUtilList, double[] LossList)
        {
            this.ID = ID;
            this.PreviousCrop = PreviousCrop;
            this.PreCropOriginalID = PreCropOriginalID;
            this.PreCrop_afterCrop = PreCrop_afterCrop;
            this.PreCropSecondCrop = PreCropSecondCrop;
            this.Crop = Crop;
            this.CropAfterCrop = CropAfterCrop;
            this.area = area;
            this.StrawUseType = StrawUseType;
            this.SecondCropID = SecondCropID;
            this.PrePreCropID = PrePreCropID;
            this.OrganicFertilizer = OrganicFertilizer;
            this.UseGrazing = UseGrazing;
            this.SalePart = SalePart;
            this.CropFixation = CropFixation;
            this.SecondCropFixation = SecondCropFixation;
            this.CropCoeff = CropCoeff;
            this.PreCropCoeff = PreCropCoeff;
            this.Runoff = Runoff;
            this.PotentialDMDeposition = PotentialDMDeposition;
            this.FieldNNeed = FieldNNeed;
            this.CropYield_N = CropYield_N;
            this.SecondCropYield_N = SecondCropYield_N;
            this.StrawRemoved_N = StrawRemoved_N;
            this.StrawYield_N = StrawYield_N;
            this.StrawDMRemoved = StrawDMRemoved;
            this.soilType = soilType;
            this.DeliveryList = DeliveryList;
            this.CropUtilList = CropUtilList;
            this.LossList = LossList;

            this.N_LesKgPrHa = -1;
            this.N_LesMgPrL = -1;
            this.GrazingManure = 0;

        }

        public FieldPlanRotation copyItem()
        {
            FieldPlanRotation newItem = new FieldPlanRotation(this.ID, this.PreviousCrop, this.PreCropOriginalID, this.PreCrop_afterCrop, this.PreCropSecondCrop, this.Crop, this.CropAfterCrop, this.OrganicFertilizer, this.StrawUseType, this.SecondCropID, this.PrePreCropID, this.area, this.UseGrazing, this.SalePart, this.CropFixation, this.SecondCropFixation, this.CropCoeff, this.PreCropCoeff, this.Runoff, this.PotentialDMDeposition, this.FieldNNeed, this.CropYield_N, this.SecondCropYield_N, this.StrawYield_N, this.StrawRemoved_N, this.StrawDMRemoved, this.soilType, this.DeliveryList, this.CropUtilList, this.LossList);
            return newItem;
        }
        public decimal getCrop()
        {
            return Crop;
        }
        public decimal getSoilType()
        {
            return soilType;
        }
        public int getID()
        {
            return ID;
        }
        public void setCrop(int Crop)
        {
            this.Crop = Crop;
        }
        public void setCropCoeff(decimal CropCoeff)
        {
            this.CropCoeff = CropCoeff;
        }
        public void setPreCropCoeff(decimal PreCropCoeff)
        {
            this.PreCropCoeff = PreCropCoeff;
        }
        public void setCropAfterCrop(int CropAfterCrop)
        {
            this.CropAfterCrop = CropAfterCrop;
        }
        public int getCropAfterCrop()
        {
            return CropAfterCrop;
        }
        public void setArea(decimal Area)
        {
            this.area = Area;
        }
        public decimal getArea()
        {
            return area;
        }
        public void setNLesMgPrL(decimal NLes)
        {
            this.N_LesMgPrL = NLes;
        }
        public void setNLesKgPrHa(decimal NLes)
        {
            this.N_LesKgPrHa = NLes;
        }
        public decimal getNLesMgPrL()
        {
            return N_LesMgPrL ;
        }
        public decimal getNLesKgPrHa()
        {
            return N_LesKgPrHa;
        }
        public decimal getRunoff()
        {
            return Runoff;
        }
        public decimal getFixation()
        {
            return CropFixation+SecondCropFixation;
        }
        public decimal getCropCoeff()
        {
            return CropCoeff;
        }
        public decimal getPreCropCoeff()
        {
            return PreCropCoeff;
        }
        public decimal getUseGrazing()
        {
            return UseGrazing;
        }
        public double getFieldNNeed()
        {
            return FieldNNeed;
        }
        public decimal getGrazingManure()
        {
            return GrazingManure;
        }
        public decimal getCropYield_N()
        {
            return CropYield_N;
        }
        public decimal getSecondCropYield_N()
        {
            return SecondCropYield_N;
        }
        public decimal getStrawYield_N()
        {
            return StrawYield_N;
        }
        public decimal getStrawRemoved_N()
        {
            return StrawRemoved_N;
        }
        public decimal getStrawDMRemoved()
        {
            return StrawDMRemoved;
        }
        //public void setPotentialDMDeposition(decimal PotentialDMDeposition)
        //{
        //    this.PotentialDMDeposition = PotentialDMDeposition;
        //}
        public decimal getPotentialDMDeposition()
        {
            return PotentialDMDeposition;
        }
        public void setGrazingManure(decimal GrazingManure)
        {
            this.GrazingManure = GrazingManure;
        }
    }
}
