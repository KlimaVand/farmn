using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Collections.Generic;

namespace FarmN_2010
{
    public class Calculation
    {
        public Calculation()
        {
        }
        public Rotation modifyRotationAfterCrop(Rotation rotationInput, decimal afterCropPercent)
        {
            decimal StatutoryAfterCrop = 0;
            decimal AfterCropHaExisting = 0;
            decimal RealAfterCropPercent = -1;
            decimal AfterCropHa = -1;
            decimal SpringSownArea = 0;
            decimal PossibleAfterCropArea = 0;

            foreach (FieldPlan fp in rotationInput.FieldPlanList)
            {
                if (fp.getStatutoryAfterCropBasis())
                {
                    StatutoryAfterCrop = StatutoryAfterCrop + fp.getArea();
                }
                if (fp.getAfterCropID() != 0)
                {
                    AfterCropHaExisting = AfterCropHaExisting + fp.getArea();
                }
                if (fp.getSpringSown())
                {
                    SpringSownArea = SpringSownArea + fp.getArea();
                }
            }
            AfterCropHa = (StatutoryAfterCrop * afterCropPercent / 100) + AfterCropHaExisting;

            if (System.Text.RegularExpressions.Regex.IsMatch(rotationInput.getRotationName(),"K12",System.Text.RegularExpressions.RegexOptions.IgnoreCase))
            {
                AfterCropHa = afterCropPercent + AfterCropHaExisting;
            }
            if (SpringSownArea < AfterCropHa)
            {
                AfterCropHa = SpringSownArea;
            }

            
            //var numQuery =
            //from fp2 in rotationInput.FieldPlanList
            //where (fp2.getStatutoryAfterCropBasis())
            //select fp2.getArea();
            //decimal sumArea = numQuery.Sum();


            return rotationInput;
        }
    }
}
