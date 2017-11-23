using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FarmN_2010
{
    /// <summary>
    /// This is a data class holding information for ManureFertilizerDelivery
    /// </summary>
    public class ManureFertilizerDelivery
    {
        public int StorageID;
        public int DeliveryID;
        public decimal Kg_N_Delivered;
        public decimal Kg_N_Loss;
        public decimal Kg_N_Utilized;
        public decimal ConversionFactor;
        public decimal AmmoniumRatio;
        /// <summary>
        /// Should not be used instance since we dont want to have a ManureFertilizerDelivery with no values
        /// </summary>
        private ManureFertilizerDelivery()
        {
        }
        public ManureFertilizerDelivery(int StorageID, int DeliveryID, decimal Kg_N_Delivered, decimal Kg_N_Loss, decimal Kg_N_Utilized, decimal AmmoniumRatio, decimal ConversionFactor)
        {
            this.StorageID = StorageID;
            this.DeliveryID = DeliveryID;            
            this.Kg_N_Delivered = Kg_N_Delivered;
            this.Kg_N_Loss = Kg_N_Loss;
            this.Kg_N_Utilized = Kg_N_Utilized;
            this.ConversionFactor = ConversionFactor;
            this.AmmoniumRatio = AmmoniumRatio;
        }
        public decimal getKg_N_Loss()
        {
            return Kg_N_Loss;
        }    
    }
}
