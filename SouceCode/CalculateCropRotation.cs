using System;
using System.Data;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

namespace FarmN_2010
{
    /// <summary>
    /// calculate a rotation
    /// </summary>
    public class CalculateCropRotation
    {
        private CalculateCropRotation()
        {
        }

        public CalculateCropRotation(ref Rotation Rotation, decimal AfterCropPercent)
        {
            Rotation = this.placeAfterCropOnFieldPlanLists(Rotation, AfterCropPercent, Rotation.getRotationName());
        }

        private Rotation placeAfterCropOnFieldPlanLists(Rotation rotation, decimal AfterCropPercent, string RotationName)
        {
            List<FieldPlan> FieldPlanList = rotation.FieldPlanList;
            List<FieldPlanRotation> FieldPlanRotationList = rotation.FieldPlanRotationList;
            FieldPlanRotation nextFprItem;
            FieldPlanRotation nextNewItem;
            int nextFprIndex = 0;
            decimal splitarea = 0;
            var statutoryAfterCrop =
                (from f in FieldPlanList
                 where f.getStatutoryAfterCropBasis()
                 select f.getArea()).Sum();
            var afterCropHaExisting =
                (from f in FieldPlanList
                 where f.getAfterCropID() == 1
                 select f.getArea()).Sum();
            var springSownArea =
                (from f in FieldPlanList
                 where f.getSpringSown()
                 select f.getArea()).Sum();
            decimal afterCropHa = (statutoryAfterCrop * AfterCropPercent / 100) + afterCropHaExisting;

            if (RotationName == "K12")
            {
                ///Since aftercropcalculations always is performed on 100 Ha, AfterCropPercent equals Ha
                afterCropHa = AfterCropPercent + afterCropHaExisting;
            }
            if (springSownArea < afterCropHa)
            {
                afterCropHa = springSownArea;
            }
            decimal cropDoubletArea = 0;

            ///First two items in FieldPlanList is the cropDoublets except in G-rotations
            if (RotationName.StartsWith("G"))
            {
                foreach (FieldPlan fld in FieldPlanList)
                {
                    if (fld.getAfterCropID() == 2 && fld.getStatutoryAfterCropBasis())
                    {
                        cropDoubletArea = fld.getArea();
                    }
                }
            }
            else
            {
                decimal cropDoubletArea_0 =
                    FieldPlanList.ElementAt(0).getArea();

                decimal cropDoubletArea_1 =
                    FieldPlanList.ElementAt(1).getArea();
                cropDoubletArea = cropDoubletArea_0 + cropDoubletArea_1;
            }
            FieldPlan removeItem = null;
            if (RotationName.StartsWith("G"))
            {
                if (afterCropHa > (100 - cropDoubletArea))
                {
                    afterCropHa = (100 - cropDoubletArea);
                }
                foreach (FieldPlan fld in FieldPlanList)
                {
                    if (fld.getAfterCropID() == 0)
                    {
                        removeItem = fld;
                    }
                    if (fld.getAfterCropID() == 1)
                    {
                        fld.setAfterCropID(0);
                        fld.setArea(100 - cropDoubletArea);
                    }
                }
                if (removeItem != null)
                {
                    FieldPlanList.Remove(removeItem);
                }
                cropDoubletArea = 0;
            }
            if (afterCropHa < cropDoubletArea)///If wanted area is less than doublets area, these (areas) are then adjusted
            {
                FieldPlanList.ElementAt(0).setArea(cropDoubletArea - afterCropHa);//Area on doublet without aftercrop
                FieldPlanList.ElementAt(1).setArea(afterCropHa);///Area on doublet with aftercrop
                var area = (from fpr in FieldPlanRotationList
                            where fpr.getCrop() == FieldPlanList.ElementAt(0).getCrop() && fpr.getCropAfterCrop() == 1
                            select fpr.getArea()).Sum();
                decimal tmpAfterCropArea = afterCropHa-area;
                foreach (FieldPlanRotation fpr in FieldPlanRotationList)
                {
                    if (fpr.getCrop() == FieldPlanList.ElementAt(0).getCrop() && fpr.getCropAfterCrop() == 0 && tmpAfterCropArea >= fpr.getArea())
                    {
                        fpr.setCropAfterCrop(1);
                        tmpAfterCropArea = tmpAfterCropArea - fpr.getArea();
                        nextFprIndex = FieldPlanRotationList.IndexOf(fpr) + 1;
                    }
                }
                    nextFprItem = FieldPlanRotationList.ElementAt(nextFprIndex);
                    splitarea = nextFprItem.getArea();
                    if (tmpAfterCropArea > 0m && tmpAfterCropArea <= splitarea)
                    {
                        nextFprItem.setArea(tmpAfterCropArea);
                        nextFprItem.setCropAfterCrop(1);
                        nextNewItem = nextFprItem.copyItem();
                        nextNewItem.setArea(splitarea - tmpAfterCropArea);
                        nextNewItem.setCropAfterCrop(0);
                  
                    if (splitarea - tmpAfterCropArea > 0)
                    {
                        FieldPlanRotationList.Add(nextNewItem);
                    }
                    }
                    else if (tmpAfterCropArea > splitarea)
                    {
                        nextFprItem.setCropAfterCrop(1);
                        tmpAfterCropArea = tmpAfterCropArea - splitarea;
                        nextFprIndex = FieldPlanRotationList.IndexOf(nextFprItem) + 1;
                        nextFprItem = FieldPlanRotationList.ElementAt(nextFprIndex);
                        splitarea = nextFprItem.getArea();
                        if (tmpAfterCropArea > 0m && tmpAfterCropArea <= splitarea)
                        {
                            nextFprItem.setArea(tmpAfterCropArea);
                            nextFprItem.setCropAfterCrop(1);
                            nextNewItem = nextFprItem.copyItem();
                            nextNewItem.setArea(splitarea - tmpAfterCropArea);
                            nextNewItem.setCropAfterCrop(0);
                            if (splitarea - tmpAfterCropArea > 0)
                            {
                                FieldPlanRotationList.Add(nextNewItem);
                            }
                        }
                    }
                var checkSum2 = (from fpr in FieldPlanRotationList
                                select fpr.getArea()).Sum();
                var checkSum1 = (from fp in FieldPlanList
                                 select fp.getArea()).Sum(); 
            }
            if (afterCropHa == cropDoubletArea)///If wanted area equals doublet area, aftercrop is placed on the doublet without aftercrop
            {
                FieldPlanList.ElementAt(0).setAfterCropID(1);
                foreach (FieldPlanRotation fpr in FieldPlanRotationList)
                {
                    if (fpr.getCrop() == FieldPlanList.ElementAt(0).getCrop() && fpr.getCropAfterCrop() == 0 )
                    {
                        fpr.setCropAfterCrop(1);
                    }
                }
            }
            if (afterCropHa > cropDoubletArea)///If wanted area is more than doublet area, aftercrop is placed on subsequent items in list if the item CanHaveAfterCrop. This also applies to G-rotations since cropDoubletArea is set to 0 in these rotations
            {
                decimal excessArea = afterCropHa - cropDoubletArea;
                decimal nextArea = 0;
                int nextItem = 0;
                if (RotationName.StartsWith("G") == false)
                {
                    FieldPlanList.ElementAt(0).setAfterCropID(1);///Aftercrop is placed on the doublet without aftercrop
                }
                foreach (FieldPlan fld in FieldPlanList)
                {
                    if (fld.getAfterCropID() == 0 && fld.getCanHaveAfterCrop())
                    {
                        nextArea = fld.getArea();
                        nextItem = FieldPlanList.IndexOf(fld);
                        break;
                    }
                }
                if (excessArea < nextArea)///The nextItem has to be divided in two, one with aftercrop, one without
                {
                    FieldPlan newItem = new FieldPlan(-1,"", 0m, 0, false, false, false,0m);///New item inserted in list
                    newItem.setCrop(FieldPlanList.ElementAt(nextItem).getCrop());
                    newItem.setCropName(FieldPlanList.ElementAt(nextItem).getCropName());
                    newItem.setStatutoryAfterCropBasis(FieldPlanList.ElementAt(nextItem).getStatutoryAfterCropBasis());
                    newItem.setSpringSown(FieldPlanList.ElementAt(nextItem).getSpringSown());
                    newItem.setCanHaveAfterCrop(FieldPlanList.ElementAt(nextItem).getCanHaveAfterCrop());
                    newItem.setNInSeed(FieldPlanList.ElementAt(nextItem).getNInSeed());
                    FieldPlanList.Insert(nextItem + 1, newItem);
                    newItem = null;

                    FieldPlanList.ElementAt(nextItem).setArea(excessArea);///Area and aftercrop on nextItem and newItem is set
                    FieldPlanList.ElementAt(nextItem).setAfterCropID(1);
                    FieldPlanList.ElementAt(nextItem + 1).setArea(nextArea - excessArea);
                }
                else
                {
                    FieldPlanList.ElementAt(nextItem).setAfterCropID(1);
                    if (excessArea > nextArea)
                    {
                        excessArea = excessArea - nextArea;
                        while (excessArea > 0)
                        {
                            foreach (FieldPlan fld in FieldPlanList)
                            {
                                if (fld.getAfterCropID() == 0 && fld.getCanHaveAfterCrop())
                                {
                                    nextArea = fld.getArea();
                                    nextItem = FieldPlanList.IndexOf(fld);
                                    break;
                                }
                            }
                            if (excessArea < nextArea)///The nextItem has to be divided in two, one with aftercrop, one without
                            {
                                FieldPlan newItem = new FieldPlan(-1,"", 0m, 0, false, false, false, 0m);///New item inserted in list
                                newItem.setCrop(FieldPlanList.ElementAt(nextItem).getCrop());
                                newItem.setCropName(FieldPlanList.ElementAt(nextItem).getCropName());
                                newItem.setStatutoryAfterCropBasis(FieldPlanList.ElementAt(nextItem).getStatutoryAfterCropBasis());
                                newItem.setSpringSown(FieldPlanList.ElementAt(nextItem).getSpringSown());
                                newItem.setCanHaveAfterCrop(FieldPlanList.ElementAt(nextItem).getCanHaveAfterCrop());
                                newItem.setNInSeed(FieldPlanList.ElementAt(nextItem).getNInSeed());
                                FieldPlanList.Insert(nextItem + 1, newItem);
                                newItem = null;

                                FieldPlanList.ElementAt(nextItem).setArea(excessArea);///Area and aftercrop on nextItem and newItem is set
                                FieldPlanList.ElementAt(nextItem).setAfterCropID(1);
                                FieldPlanList.ElementAt(nextItem + 1).setArea(nextArea - excessArea);
                                excessArea = 0;
                            }
                            else
                            {
                                FieldPlanList.ElementAt(nextItem).setAfterCropID(1);
                                excessArea = excessArea - nextArea;
                            }
                        }
                    }
                }
                var checkSum = (from fpr in FieldPlanRotationList
                                 select fpr.getArea()).Sum();
                var checkSuma = (from fpr in FieldPlanRotationList
                                  where fpr.getCropAfterCrop() == 1
                                  select fpr.getArea()).Sum();
                var checkSum3 = (from fp in FieldPlanList
                                 select fp.getArea()).Sum();
                var checkSum3a = (from fp in FieldPlanList
                                  where fp.getAfterCropID() == 1
                                  select fp.getArea()).Sum();
                var areaTaken = (from fpr in FieldPlanRotationList
                                 where fpr.getCropAfterCrop() == 1
                                 select fpr.getArea()).Sum();
                decimal tmpAfterCropArea = afterCropHa -areaTaken;//- cropDoubletArea;
                foreach (FieldPlanRotation fpr in FieldPlanRotationList)//Aftercrop is placed on subsequent items in the FielPlanRotationList
                {
                    if (tmpAfterCropArea >= fpr.getArea() && fpr.getCropAfterCrop() == 0)
                    {
                        fpr.setCropAfterCrop(1);
                        tmpAfterCropArea = tmpAfterCropArea - fpr.getArea();
                        if (FieldPlanRotationList.IndexOf(fpr) < FieldPlanRotationList.Count()-1)
                        {
                            nextFprIndex = FieldPlanRotationList.IndexOf(fpr) + 1;
                        }
                    }
                }
                if (tmpAfterCropArea > 0m)
                {

                    nextFprItem = FieldPlanRotationList.ElementAt(nextFprIndex);

                    splitarea = nextFprItem.getArea();
                    while (tmpAfterCropArea >= splitarea)
                    {
                        nextFprItem.setCropAfterCrop(1);
                        tmpAfterCropArea = tmpAfterCropArea - splitarea;
                        if (nextFprIndex < FieldPlanRotationList.Count()-1)
                        {
                            nextFprIndex = FieldPlanRotationList.IndexOf(nextFprItem) + 1;
                            nextFprItem = FieldPlanRotationList.ElementAt(nextFprIndex);
                        }
                    }
                    if (tmpAfterCropArea > 0m)
                    {
                        nextFprItem.setArea(tmpAfterCropArea);
                        nextFprItem.setCropAfterCrop(1);
                        nextNewItem = nextFprItem.copyItem();
                        nextNewItem.setArea(splitarea - tmpAfterCropArea);
                        nextNewItem.setCropAfterCrop(0);
                        FieldPlanRotationList.Add(nextNewItem);
                    }
                }
                var checkSum2 = (from fpr in FieldPlanRotationList
                                 select fpr.getArea()).Sum();
                var checkSum2a = (from fpr in FieldPlanRotationList
                                 where fpr.getCropAfterCrop() == 1
                                 select fpr.getArea()).Sum();
                var checkSum1 = (from fp in FieldPlanList
                                 select fp.getArea()).Sum();
                var checkSum1a = (from fp in FieldPlanList
                                 where fp.getAfterCropID() == 1
                                 select fp.getArea()).Sum();
            }
            rotation.FieldPlanList = FieldPlanList;
            return rotation;
        }
    }
}
