using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FarmN_2010
{
    /// <summary>
    /// This is a data class holding information for BoughtManure
    /// </summary>
    public class BoughtManure
    {
        private decimal BoughtManureAmount;
        private int BoughtManureType;
        private decimal Utilization_degree;
        /// <summary>
        /// Should not be used instance since we dont want to have a BoughtManure with no values
        /// </summary>
        private BoughtManure()
        {
        }
        public BoughtManure(decimal BoughtManureAmount,int BoughtManureType,decimal Utilization_degree)
        {
            this.BoughtManureAmount = BoughtManureAmount;
            this.BoughtManureType = BoughtManureType;
            this.Utilization_degree = Utilization_degree;
        }
        public decimal getManureAmount()
        {
            return this.BoughtManureAmount;
        }
        public int getManureType()
        {
            return this.BoughtManureType;
        }
        public decimal getBoughtUtilDegree()
        {
            return this.Utilization_degree;
        }
        public void setBoughtUtilDegree(decimal Utilization_degree)
        {
            this.Utilization_degree = Utilization_degree;
        }

    }
}
