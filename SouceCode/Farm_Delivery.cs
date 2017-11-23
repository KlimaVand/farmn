using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FarmN_2010
{
    /// <summary>
    /// This is a data class holding information for Farm_Delivery
    /// </summary>
    public class Farm_Delivery
    {
        private int DeliveryID;
        /// <summary>
        /// Should not be used instance since we dont want to have a Farm_Delivery with no values
        /// </summary>
        private Farm_Delivery()
        {
        }
        /// <summary>
        /// constructor with arguments 
        /// </summary>
        /// <param name="DeliveryID">The Delivery ID</param>
        public Farm_Delivery(int DeliveryID)
        {
            this.DeliveryID = DeliveryID;
        }

    }
}
