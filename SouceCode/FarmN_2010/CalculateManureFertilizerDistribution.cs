using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Linq;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.IO;
namespace FarmN_2010
{
    /// <summary>
    /// a Class that is responsiblity of calculate how the Manure should be distribution
    /// </summary>
    public class CalculateManureFertilizerDistribution
    {

        private AppSettingsReader cfg = new AppSettingsReader();
        private string sqlconnstr = "";
        private SqlConnection sqlconn;
        private decimal boughtGrazingNPrGrazeArea = 0;
        private decimal boughtGrazing = 0;
        private int numberOfFieldPlanRotations = 0;
        private decimal afterCropTotalArea = 0;
        private decimal grazingConversionFactor = 0;
        private double totalMineralFertilizerNiveau = 0;
        private double totalMineralFertilizerNiveauNegativeNeed = 0;
        private decimal totalManureLoss = 0;
        private decimal totalFertilizerLoss = 0;
        public decimal totalManureDist = 0;
        private CalculateManureFertilizerDistribution()
        {
        }
        /// <summary>
        /// Calculate the manure distrubtion
        /// </summary>
        /// <param name="ManureVersion">The version that should be used. It can be either 1 or 2</param>
        /// <param name="NPercent">The need of N</param>
        /// <param name="ManureList">A list of Manure</param>
        /// <param name="scenario">A given scenario</param>
        public CalculateManureFertilizerDistribution(int ManureVersion, decimal NPercent, List<BoughtManure> ManureList,ref Scenario scenario)
        {
            decimal grazingArea=0;
            decimal grazingSum=0;
            sqlconnstr = cfg.GetValue("sqlConnectionStringPublic", typeof(string)).ToString();
            sqlconn = null;
            string cmdstr;
            SqlCommand cmd;
            try
            {
                sqlconn = new SqlConnection(sqlconnstr);
                sqlconn.Open();
            }
            catch (Exception e)
            {
                message.Instance.addWarnings("Problemer med at forbinde til Databasen", "Cannot open Database connectíon " + e.Message.ToString(), 2);
            }
            boughtGrazing =
                (from m in ManureList
                 where m.getManureType() == 3m
                 select m.getManureAmount()).Sum();
            foreach (Rotation rot in scenario.RotationList)
            {
                var afterCropArea =
                    (from fp in rot.FieldPlanList
                     where fp.getAfterCropID() == 1
                     select fp.getArea()).Sum();
                if (boughtGrazing > 0m)
                {
                
                var rotArea =
                    (from f in rot.FieldPlanRotationList
                     select f.getArea()).Sum();
                cmdstr = "MST_GetGrazeArea";
                cmd = new SqlCommand(cmdstr, sqlconn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@StandardRotationName", SqlDbType.VarChar, 20);
                cmd.Parameters["@StandardRotationName"].Value = rot.getRotationName();
                cmd.Parameters.Add("@RotationArea", SqlDbType.Float);
                cmd.Parameters["@RotationArea"].Value = rotArea;
                cmd.Parameters.Add("@StandardRotationVersion", SqlDbType.Float);
                cmd.Parameters["@StandardRotationVersion"].Value = ManureVersion;
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    grazingArea = Convert.ToDecimal(reader["GrazingArea"]);
                }
                grazingSum += grazingArea;
                reader.Close();
                }
                numberOfFieldPlanRotations += rot.FieldPlanRotationList.Count;
                afterCropTotalArea += afterCropArea;
            }
            if (boughtGrazing > 0m && grazingSum == 0)
            {
                message.Instance.addWarnings("Der er ikke græsmarker til græssende dyr i de valgte sædskifter", "CalculateManureFertilizerDistributions: GrazingManure and no grazing", 3);
            }
            else
            {
                if (grazingSum > 0)
                {
                    boughtGrazingNPrGrazeArea = boughtGrazing / grazingSum;
                }
                else
                {
                    boughtGrazingNPrGrazeArea = 0;
                }
            } 
            if (sqlconn != null)
            {
                sqlconn.Close();
            }
            
            switch (ManureVersion)
            {
                case 1:
                    scenario = this.getNDistribution_ver1(scenario, NPercent, ManureList);
                    break;
                case 2:
                    scenario = this.getNDistribution_ver2(scenario, NPercent, ManureList);
                    break;
            }
        }
        private Scenario getNDistribution_ver1(Scenario scenario, decimal NPercent, List<BoughtManure> ManureList)
        {
            FillArrays(scenario, NPercent, ManureList, 1);
            return scenario;
        }
        private Scenario getNDistribution_ver2(Scenario scenario, decimal NPercent, List<BoughtManure> ManureList)
        {
            FillArrays(scenario, NPercent, ManureList,2);
            return scenario;
        }
   
        private void FillArrays(Scenario scenario, decimal NPercent, List<BoughtManure> ManureList, int ManureVersion)
        {
            double[] ArrA;//Den organiske og mineralske gødning der skal fordeles ganget med udnyttelsesprocenten
            double[] ArrManureUtil;
            int[] ArrSalesOrderList;
            decimal[][] ArrAmmoniumAndConversion;
            int[] ArrSoilType;
            double[][] ArrDelivery;
            FieldPlanRotation[] ArrBHelp = new FieldPlanRotation[numberOfFieldPlanRotations];//Gødningsbehovet NNeed for hver record i FieldPlanRotation skal lægge records fra alle rotations sammen i et array
            double[] ArrB = new double[numberOfFieldPlanRotations];//Gødningsbehovet NNeed for hver record i FieldPlanRotation skal lægge records fra alle rotations sammen i et array
            double[][] ObjectCoeff;//dimension ArrB * dimension ArrA
            double[][] ObjectCoeff1;//dimension ArrA * dimension ArrB
            double[][] ArrResult;//dimension ArrA * dimension ArrB
            int n=0;
            decimal DE_ha = 0;
            decimal totalArea = 0;
            decimal Bought_Amount = 0;
            double BoughtFertilizer = 0;
            decimal GrazingUtilDegree = 0;

            sqlconnstr = cfg.GetValue("sqlConnectionStringPublic", typeof(string)).ToString();
            sqlconn = null;
            try
            {
                sqlconn = new SqlConnection(sqlconnstr);
                sqlconn.Open();
            }
            catch (Exception e)
            {
                message.Instance.addWarnings("Problemer med at forbinde til Databasen", "Cannot open Database connectíon " + e.Message.ToString(), 2);
            }
            int count = 0;
            int retval;
            string cmdstr = "MST_GetSalesOrderList";
            SqlCommand cmd = new SqlCommand(cmdstr, sqlconn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@count", SqlDbType.Int);
            cmd.Parameters["@count"].Direction = ParameterDirection.Output;
            retval = cmd.ExecuteNonQuery();
            count = Convert.ToInt32(cmd.Parameters["@count"].Value);
            SqlDataReader reader = cmd.ExecuteReader();

            //}
            ArrA = new double[count];
            ArrManureUtil = new double[count];
            ArrSalesOrderList = new int[count];
            ArrAmmoniumAndConversion = new decimal[count][];
            ArrSoilType = new int[numberOfFieldPlanRotations];
            ArrDelivery = new double[numberOfFieldPlanRotations][];
            count = 0;
            while (reader.Read())
            {
                int storageID=Convert.ToInt32(reader["StorageID"]);

                var BoughtAmount =
                (from m in ManureList
                 where m.getManureType() ==storageID
                 select m.getManureAmount()).Sum();
                if (BoughtAmount == null)
                {
                    BoughtAmount = 0m;
                }
                Bought_Amount += BoughtAmount;
                var BoughtUtilDegree =
               (from m in ManureList
                where m.getManureType() == storageID
                select m.getBoughtUtilDegree()).Sum();
                if (storageID == 3)
                {
                    GrazingUtilDegree = BoughtUtilDegree;
                }
                if (BoughtUtilDegree == 0m)
                {
                    BoughtUtilDegree = Convert.ToDecimal(reader["N_UtilizationDegree"]);
                    if (BoughtUtilDegree > 100m)
                    {
                        message.Instance.addWarnings("Gødningens udnyttelsesprocent er ikke angivet", "CalculateManureFertilizerDistributions: Wrong ManureUtilization% from database", 1);
                    }
                    foreach (BoughtManure bm in scenario.ManureList)
                    {
                        if (bm.getManureType() == storageID)
                        {
                            bm.setBoughtUtilDegree(BoughtUtilDegree);
                        }
                    }
                }
                if (Convert.ToInt32(reader["StorageID"]) == 3)//if StorageID == 3: GrazingManure
                {
                    ArrA[count] = 0;
                    grazingConversionFactor = Convert.ToDecimal(reader["ConversionFactor"]);
                    if (GrazingUtilDegree == 0)
                    {
                        GrazingUtilDegree = Convert.ToDecimal(reader["N_UtilizationDegree"]);
                    }
                }
                else
                {
                    ArrA[count] = Convert.ToDouble(BoughtAmount * BoughtUtilDegree / 100m);
                }
                ArrManureUtil[count] = Convert.ToDouble(BoughtUtilDegree);
                ArrSalesOrderList[count] = Convert.ToInt32(reader["StorageID"]);
                ArrAmmoniumAndConversion[count] = new decimal[2];
                decimal[] tmp = new decimal[2];
                tmp[0] = Convert.ToDecimal(reader["AmmoniumRatio"]);
                tmp[1] = Convert.ToDecimal(reader["ConversionFactor"]);
                ArrAmmoniumAndConversion[count] = tmp;
                count = count + 1;
            }
            reader.Close();
            var fertEqDist =
                (from m in ManureList
                 select m.getManureAmount() * m.getBoughtUtilDegree()/100m).Sum();
            var sortRotList =
                (from Rot in scenario.RotationList
                 orderby Rot.getSoilType()
                 select Rot);
            foreach (Rotation rot in sortRotList)//skal sorteres efter soiltype og cropid
            {
                var sortFplrList =
                    (from Fpl in rot.FieldPlanRotationList
                     orderby Fpl.getCrop(),Fpl.getID()
                     select Fpl);
                foreach (FieldPlanRotation fpl in sortFplrList)
                {
                    if (n < numberOfFieldPlanRotations)
                    {
                        ArrSoilType[n] = rot.getSoilType();
                        ArrBHelp[n] = fpl;
                    }
                    n += 1;
                    totalArea += fpl.getArea();
                }
            }
            var ArrBSHelp =
                from FieldPlanRotation fpr in ArrBHelp
                orderby fpr.getSoilType(), fpr.getCrop(), fpr.getID()
                select fpr;
            for (int m = 0; m < ArrBSHelp.Count(); m++)
            {
                    ArrB[m] = ArrBSHelp.ElementAt(m).getFieldNNeed() * Convert.ToDouble(ArrBSHelp.ElementAt(m).getArea());//NNeed skal ganges med den enkelte records areal for at få totalen
                if (boughtGrazingNPrGrazeArea > 0 && ArrBSHelp.ElementAt(m).getUseGrazing() == 1)
                {
                    decimal red = (boughtGrazingNPrGrazeArea * GrazingUtilDegree / 100 * ArrBSHelp.ElementAt(m).getArea());
                    ArrB[m] = (ArrBSHelp.ElementAt(m).getFieldNNeed() * Convert.ToDouble(ArrBSHelp.ElementAt(m).getArea()) - Convert.ToDouble(red));
                    if (ArrB[m] < 0)
                    {
                        message.Instance.addWarnings("Der er ikke græsmarker nok til græssende dyr", "CalculateManureFertilizerDistributions: negative NNeed in ArrB", 3);
                    }
                    ArrBSHelp.ElementAt(m).setGrazingManure(boughtGrazingNPrGrazeArea * ArrBSHelp.ElementAt(m).getArea());
                }
            }
            double TotalManureShareOfNneed = 0;
            if (globalSettings.Instance.getRoundedValuesError()==true)
            {
            TotalManureShareOfNneed =
                (from b in ArrB
                 select Math.Round(b,3,MidpointRounding.ToEven)).Sum();
            }
            else
            {
            TotalManureShareOfNneed =
                (from b in ArrB
                 select b).Sum();
            }
            
            ObjectCoeff = new double[numberOfFieldPlanRotations][];//dimension ArrB * dimension ArrA
            ObjectCoeff1 = new double[count][];//dimension ArrB * dimension ArrA
            ArrResult = new double[count][];//dimension ArrA * dimension ArrB
            sqlconnstr = cfg.GetValue("sqlConnectionStringPublic", typeof(string)).ToString();
            sqlconn = null;
            try
            {
                sqlconn = new SqlConnection(sqlconnstr);
                sqlconn.Open();
            }
            catch (Exception e)
            {
                message.Instance.addWarnings("Problemer med at forbinde til Databasen", "Cannot open Database connectíon " + e.Message.ToString(), 2);
            }
            DE_ha = Bought_Amount / (100*totalArea);
            double normReduction = this.GetNormReduction(DE_ha);
            var totalNNeed =
                (from fn in ArrBSHelp
                 select fn.getFieldNNeed()* Convert.ToDouble(fn.getArea())).Sum();
            totalNNeed = totalNNeed - normReduction;
            double fertEqNorm = 0;
            if (globalSettings.Instance.getRoundedValuesError())
            {
                fertEqNorm =
                    (from fq in ArrA
                     select Math.Round(fq, 2)).Sum();
            }
            else
            {
                fertEqNorm =
                    (from fq in ArrA
                     select fq).Sum();
            }
            fertEqNorm = fertEqNorm + Convert.ToDouble(boughtGrazing / 100 * GrazingUtilDegree);
            if (fertEqNorm < totalNNeed)
            {
                BoughtFertilizer = totalNNeed - fertEqNorm;//Afgræsning skal også trækkes fra
            }
            else
            {
                BoughtFertilizer = 0;
            }
            fertEqDist += Convert.ToDecimal(BoughtFertilizer);
            ArrA[count - 1] = Convert.ToDouble(BoughtFertilizer);//Indkøbt gødning lægges i sidste plads i ArrA
            for (int a = 0; a < ArrA.Count(); a++)
            {
                for (int b = 0; b < ArrB.Count(); b++ )
                {
                    ArrResult[a] = new double[numberOfFieldPlanRotations];
                    ObjectCoeff[b] = new double[count];
                    ObjectCoeff1[a] = new double[numberOfFieldPlanRotations];
                    ArrDelivery[b] = new double[count];
                    ArrResult[a][b] = -1;
                    ArrDelivery[b] = ArrBSHelp.ElementAt(b).DeliveryList;
                    ObjectCoeff[b] = ArrBSHelp.ElementAt(b).CropUtilList;
                    //reader.Close();
                }
            }
            for (int a = 0; a < ObjectCoeff1.Count(); a++)
            {
                for (int b = 0; b < ObjectCoeff.Count();  b++)
                {
                    if (ObjectCoeff[b][a] == -10000)
                    {
                        ObjectCoeff1[a][b] = -10000;
                    }
                    else
                    {
                        ObjectCoeff1[a][b] = Math.Round(10 * ((ObjectCoeff[b][a]) + (5 * ArrSoilType[b])), 0);
                    }
                }
            }
            double scale = 0;
            if (globalSettings.Instance.getRoundedValuesError())
            {
                fertEqDist = Math.Round(fertEqDist - (boughtGrazing / 100 * GrazingUtilDegree), 2);
                scale = Convert.ToDouble(fertEqDist) / Math.Round(TotalManureShareOfNneed, 1);
            }
            else
            {
                scale = (Convert.ToDouble(fertEqDist)- (Convert.ToDouble(boughtGrazing) / 100 * Convert.ToDouble(GrazingUtilDegree)))  / TotalManureShareOfNneed ;
            }
           for (int i = 0; i < ArrB.Count(); i++)
            {
                ArrB[i] = Math.Round((ArrB[i] * scale),2);
            }
           for (int i = 0; i < ArrA.Count(); i++)
           {
               ArrA[i] = Math.Round(ArrA[i], 2);

           }
           
           double totalA = ArrA.Sum();
           double totalB = ArrB.Sum();
           //double diff = totalB - totalA;
           if (totalA < totalB)
           {
               ArrA[ArrA.Count() - 1] = ArrA[ArrA.Count() - 1] + Math.Round(totalB-totalA, 2);
           }
           else
           {
               ArrB[ArrB.Count() - 1] = ArrB[ArrB.Count() - 1] + Math.Round(totalA-totalB, 2);
           }
           FieldPlanRotation[] ArrBHelp1 = ArrBSHelp.ToArray();
           var numberSameSoilType =
               (from f in scenario.RotationList
                group f by f.getSoilType() into s
                select new {SoilType = s.Key, Number = s.Count()});
           var multiSameSoilType =
               (from s in numberSameSoilType
                where s.Number > 1
                select s);
           for (int i = 0 ; i < multiSameSoilType.Count(); i++)
           { 
               int first = -1;
               int last = -1;
               decimal crop = ArrBHelp1.ElementAt(0).getCrop();
               for (int f = 0; f < ArrBHelp1.Count(); f++)
               {
                   if (ArrBHelp1.ElementAt(f).getSoilType() == multiSameSoilType.ElementAt(i).SoilType && ArrBHelp1.ElementAt(f).getCrop() == crop)
                   {
                       if (first == -1)
                       {
                           first = f;
                       }
                       last = f;
                   }
                   crop = ArrBHelp1.ElementAt(f).getCrop();
               }
               //advancedSwap(ref ArrBHelp1, ref ArrB, first, last, numberSameSoilType.ElementAt(i).Number);
           }

           totalA = ArrA.Sum();
           totalB = ArrB.Sum();
           double diff = totalB - totalA;
           if (diff > (0.01 * ArrB.Count()) || diff < (-0.01 * ArrB.Count()))
           {
               message.Instance.addWarnings("Algoritmefejl ved afstemning i gødningsberegning", "CalculateManureFertilizerDistribution: FillArrays: Difference mll. ArrA og ArrB for stor", 2);
           }
           if (ManureVersion == 1)
           {
               SendArrays_old(ArrA, ArrB, ref ObjectCoeff1, ref ArrResult);
           }
           else
           {
               SendArrays_new(ArrA, ArrB, ref ObjectCoeff1, ref ArrResult);
           }
            double tempLoss = 10;
            bool negativeNeed = false;
            for (int b = 0; b < ObjectCoeff.Count(); b++)
            {
                List<ManureFertilizerDelivery> manureDeliveryList = new List<ManureFertilizerDelivery>();
                for (int a = 0; a < ObjectCoeff1.Count(); a++)
                {
                    if (ArrResult[a][b] > 0.01 && ObjectCoeff1[a][b] > 0)
                    {
                        tempLoss = ArrBSHelp.ElementAt(b).LossList[a];
                        ManureFertilizerDelivery mfd;
                        if (globalSettings.Instance.getRoundedValuesError() == true)
                        {
                            mfd = new ManureFertilizerDelivery(ArrSalesOrderList[a], Convert.ToInt32(ArrDelivery[b][a]), Math.Round(Convert.ToDecimal(Math.Round(ArrResult[a][b], 13) * 100 / ArrManureUtil[a]), 2), Math.Round(Convert.ToDecimal(tempLoss * (Math.Round(ArrResult[a][b], 13) * 100 / ArrManureUtil[a]) / 100), 2), Math.Round(Convert.ToDecimal(Math.Round(ArrResult[a][b], 13)), 4), ArrAmmoniumAndConversion[a][0], ArrAmmoniumAndConversion[a][1]);
                        }
                        else
                        {
                            mfd = new ManureFertilizerDelivery(ArrSalesOrderList[a], Convert.ToInt32(ArrDelivery[b][a]), Convert.ToDecimal(ArrResult[a][b] * 100 / ArrManureUtil[a]), Convert.ToDecimal(tempLoss * (ArrResult[a][b] * 100 / ArrManureUtil[a]) / 100), Convert.ToDecimal(ArrResult[a][b]), ArrAmmoniumAndConversion[a][0], ArrAmmoniumAndConversion[a][1]);
                        }
                        if (mfd.StorageID != 9)
                        {
                            totalManureLoss += mfd.getKg_N_Loss();
                            totalManureDist += mfd.Kg_N_Delivered;
                        }
                        else
                        {
                            totalFertilizerLoss += mfd.getKg_N_Loss();
                            if (globalSettings.Instance.getRoundedValuesError() == true)
                            {
                                totalMineralFertilizerNiveau += Math.Round(ArrResult[a][b], 2);
                            }
                            else
                            {
                                totalMineralFertilizerNiveau += ArrResult[a][b];
                            }
                        }
                        manureDeliveryList.Add(mfd);
                    }
                    else
                    {
                        if (ArrResult[a][b] < 0)
                            negativeNeed = true;
                    }
                }
                ArrBHelp1.ElementAt(b).ManureFertilizerDeliveryList = manureDeliveryList;
                manureDeliveryList = null;
            }
            if (negativeNeed == true)
                totalMineralFertilizerNiveauNegativeNeed = BoughtFertilizer;
            if (sqlconn != null)
            {
                sqlconn.Close();
            }
        }
        /// <summary>
        /// a get method to access how much manure that is lost
        /// </summary>
        /// <returns>The amount of manure that is lost</returns>
        public decimal getTotalManureLoss()
        {
            return totalManureLoss;
        }
        /// <summary>
        /// a get method to access how much manure is distributed
        /// </summary>
        /// <returns>The amount of manure that is lost</returns>
        public decimal getTotalManureDist()
        {
            return totalManureDist;
        }
        /// <summary>
        /// a get method to access how much Fertilizer that is lost
        /// </summary>
        /// <returns>The amount of Fertilizer that is lost</returns>
        public decimal getTotalFertilizerLoss()
        {
            return totalFertilizerLoss;
        }
        private void SendArrays_old(double[] A, double[] B,ref double[][] ObjectCoeff,ref double[][] arrRotationList)
        {
           rotationModel2 rotationModel = new rotationModel2();

            bool test=rotationModel.CalcRotation(A, B, ref ObjectCoeff, ref arrRotationList);
            int i = 0;
            while (test == false)
            {
                for (int a = 0; a < A.Count() - 2; a++)
                {
                    for (int b = 0; b < B.Count() - 1; b++)
                    {
                        if (arrRotationList[a][b] != 0 && ObjectCoeff[a][b] < -1000)
                        {
                            totalMineralFertilizerNiveau = totalMineralFertilizerNiveau + Math.Round(arrRotationList[a][b], 2);
                            A[a] = A[a] - Math.Round(arrRotationList[a][b], 2);
                            A[A.Count() - 1] = A[A.Count() - 1] + Math.Round(arrRotationList[a][b], 2);
                        }
                    }
                }
                test = rotationModel.CalcRotation(A, B, ref ObjectCoeff, ref arrRotationList);
            }

        }
        private void SendArrays_new(double[] A, double[] B, ref double[][] ObjectCoeff, ref double[][] arrRotationList)
        {
            double totNDist = A.Sum();
            double tot = totNDist;
            for (int a = 0; a < A.Count(); a++)
            {
                tot = A[a];
                int bval = 0;
                for (int b = 0; b < B.Count(); b++)
                {
                    arrRotationList[a][b] = (A[a] / totNDist) * B[b];
                    tot = tot - arrRotationList[a][b];
                    bval = b;
                }
                arrRotationList[a][bval] = arrRotationList[a][bval] + tot;
            }
        }
        public decimal getGrazingConversionFactor()
        {
            return grazingConversionFactor;
        }
        /// <summary>
        /// Returning the Fertilizer niveau
        /// </summary>
        /// <returns>Returning the Fertilizer niveau</returns>
        public double getTotalMineralFertilizerNiveau()
        {
            return totalMineralFertilizerNiveau;
        }
        /// <summary>
        /// Returning the Fertilizer niveau. If NNeed on some fields is negative, this will hold the correct fertilizer niveau 
        /// </summary>
        /// <returns>Returning the Fertilizer niveau</returns>
        public double getTotalMineralFertilizerNiveauNegativeNNeed()
        {
            return totalMineralFertilizerNiveauNegativeNeed;
        }
        /// <summary>
        /// Calculate the new Norm Reduction
        /// </summary>
        /// <param name="DE_ha"></param>
        /// <returns></returns>
        private double GetNormReduction(decimal DE_ha)
        {
            decimal normReductionFactor = 17m;
            if (DE_ha>0.8m)
            {
                normReductionFactor = 25m;
            }
            return Convert.ToDouble(afterCropTotalArea * normReductionFactor);
        }
        /// <summary>
        /// advanced swapping function. It will swap a certain amount, then skip the same amount and then swap again. Untill stop has been reached
        /// </summary>
        /// <param name="FieldPlanRotationListTmp"> a array of FieldPlanRotation</param>
        /// <param name="arreyB">array of doubles</param>
        /// <param name="start">the starting place in those 2 arrays that will be swapped</param>
        /// <param name="stop">the starting place in those 2 arrays that will be swapped</param>
        /// <param name="numbers">The numbers of double/FieldPlanRotation that will be swapped before the next skipping</param>
        public void advancedSwap(ref FieldPlanRotation[] FieldPlanRotationListTmp, ref double[] arreyB, int start, int stop, int numbers)
        {
            if (arreyB.Count() < start || arreyB.Count() < stop || start > stop || (start + stop) < numbers || FieldPlanRotationListTmp.Count() != arreyB.Count())
            {
                Console.WriteLine("something is wrong");
            }

            double[] tmpDouble = new double[numbers];
            FieldPlanRotation[] tmpFieldPlanRotatio = new FieldPlanRotation[numbers];
            for (int k = start; k < stop; k = k + numbers * 2)
            {
                start = k;
                for (int i = start; i < (numbers + k); i = i + numbers)
                {
                    for (int j = 0; j < numbers; j++)
                    {
                        tmpDouble[j] = arreyB[j + i];
                        tmpFieldPlanRotatio[j] = FieldPlanRotationListTmp[j + i]; ;
                    }
                    for (int j = 0; j < numbers; j++)
                    {
                        arreyB[start + numbers - j - 1] = tmpDouble[j];
                        FieldPlanRotationListTmp[start + numbers - j - 1] = tmpFieldPlanRotatio[j];

                    }


                }
            }


        }

    }
}
