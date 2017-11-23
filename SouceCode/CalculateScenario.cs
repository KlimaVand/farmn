using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using Husdyrgodkendelse;
using Husdyrgodkendelse.Goedning;

namespace FarmN_2010
{
    /// <summary>
    /// this is our primary class that connects the rest of our functionality
    /// </summary>
    public class CalculateScenario
    {
        private AppSettingsReader cfg = null;
        private string sqlconnstr = "";
        private SqlConnection sqlconn;
        private long FarmID;
        private int ScenarioID;
        public Farm farm;
        public SIMDEN Denitrification = new SIMDEN();
        public N_LES NLes = new N_LES();
        public DeltaSoilN SoilChange = new DeltaSoilN();
        public CalculateCropRotation CropRotation;
        public CalculateManureFertilizerDistribution CalculateManureFertilizer;
        private int cropYear;
        private int zipCode;
        private int manureVersion;
        private decimal TotalNLesKg = 0;
        private decimal TotalNLesMg = 0;
        private returnItems returnData = new returnItems();
        private List<FarmNData> rotationData=new List<FarmNData>();
        private int[] globalCropList;
        private double[][] globalDeliveryList;
        private double[][] globalUtilDegreeList;
        private double[][] globalLossList;

        private decimal grazingConversionFactor;
        private decimal totalArea;
        private decimal totalRotArea;
        private decimal adjustmentValue = 0;
        private long BedriftID; 
        private string ZipCodeString; private int Cropyear; 

        private decimal AfterCropPercent; 
        private decimal NNeedPercent; 
        private int ManureVersion;
        private List<Manure> manureList;
        private List<StandardRotation> arealList; 
        private int StdRotationVersion;
        private int version;
        /// <summary>
    /// Should not be used instance, since we don't want to have a calculation with no farm- and scenario-values
    /// </summary>
        private CalculateScenario()
        {
            
        }
        /// <summary>
        /// give s list of rotation output
        /// </summary>
        /// <returns>a list of outputs</returns>
        public List<FarmNData> getFarmNData()
        {
            return rotationData;
        }
        /// <summary>
        /// returns a stuct with Farm Data
        /// </summary>
        /// <returns>returns a stuct with Farm Data</returns>
        public returnItems getReturnData()
        {
            return returnData;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public decimal getTotalNLesKgPrHa()
        {
            return TotalNLesKg;
        }
        public decimal getTotalNLesMgPrL()
        {
            return TotalNLesMg;
        }
        private void controlInput(long BedriftID, string ZipCodeString, int Cropyear, int ScenarioID, decimal AfterCropPercent, decimal NNeedPercent, int ManureVersion, List<Manure> manureList, List<StandardRotation> arealList)
        {

            if (BedriftID < 0)
            {
                message.Instance.addWarnings("Ugyldigt BedriftID", "controlInput: Wrong BedriftID", 1);
            }
            try
            {
                int zipcoide = Convert.ToInt32(ZipCodeString);
                if (zipcoide < 1000 || zipcoide > 9990)
                {
                    message.Instance.addWarnings("Ukendt postnummer", "controlInput: Wrong Zipcode", 1);
                }
            }
            catch(Exception e)
            {
                message.Instance.addWarnings("Ukendt postnummer", "controlInput: cannot convert Zipcode. Error is " + e.Message, 1);
            }
 
            if (ScenarioID < 1 || ScenarioID > 3)
            {
                message.Instance.addWarnings("Ukendt ScenarioID", "controlInput: wrong ScenarioID", 1);
            }
            if (AfterCropPercent < 0 || AfterCropPercent > 100)
            {
                message.Instance.addWarnings("Ugyldig efterafgrøde procent","controlInput: wrong AfterCropPercent", 1);
            }
            if (NNeedPercent < 0 || NNeedPercent>100)
            {
                message.Instance.addWarnings("Ugyldig N behovs procent","controlInput: wrong NNeedPercent", 1);
            }
            if (ManureVersion != 1 && ManureVersion != 2)
            {
                message.Instance.addWarnings("Ukendt goednings version","controlInput: wrong ManureVersion", 1);
            }
            decimal manureNsum = 0;
            for(int i=0;i<manureList.Count();i++)
            {

                Manure tmp= manureList.ElementAt(i);
                manureNsum += tmp.getKgN();
                if (tmp.getKgN() <= 0)
                {
                    message.Instance.addWarnings("Nr "+i.ToString()+" i goedningslisten har ingen N","controlInput: manure nr "+i.ToString()+" has no kg pr n", 1);
                }
            }
            //decimal areaSum = 0;
            for (int i = 0; i < arealList.Count(); i++)
            {
                StandardRotation tmp= arealList.ElementAt(i);
                totalArea += tmp.getAreaHa();
                if(tmp.getSoilType()<1||tmp.getSoilType()>12)
                {
                    message.Instance.addWarnings("StandardRotation nr " + i.ToString() + " har ukendt jordtype","controlInput: StandardRotation nr " + i.ToString() + " has illegal soil type", 1);
                }
                if(tmp.getAreaHa()<=0)
                {
                    message.Instance.addWarnings("StandardRotation nr " + i.ToString() + " har ukendt areal","controlInput: StandardRotation nr " + i.ToString() + " has illegal areal", 1);
                }
                List<string> rotationList = getRotationNames();
                bool found=false;
                for (int j = 0; j < rotationList.Count; j++)
                {
                    if (tmp.getStdRotationName().ToUpper().CompareTo(rotationList[j])==0)
                    {
                        found = true;
                        break;
                    }
                }
                if (false == found)
                {
                    message.Instance.addWarnings("Ugyldigt saedskiftenavn","controlInput: wrong standard rotation string", 1);
                }
            }
            if (manureNsum/totalArea > 1500)
            {
                message.Instance.addWarnings("Den samlede maengde af N i husdyrgoedning overstiger 1500 kg/ha", "controlInput: to much manure", 1);
            }
        }
        static List<string> rotationNames = new List<string>();
        /// <summary>
        /// creates a list of current rotation names
        /// </summary>
        /// <returns>a list of rotation names</returns>

        public List<string> getRotationNames()
        {

            cfg = new AppSettingsReader();

            try
            {


                SqlCommand cmd = new SqlCommand("MST_getRotationNames", sqlconn);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    string name = Convert.ToString(reader["RotationName"]);
                    rotationNames.Add(name);
  
                }
                reader.Close();



            }
            catch (Exception e)
            {

                message.Instance.addWarnings("Problemer med databaseopslag for saedskiftenavn", "Something wrong with database with getRotationNames "+e.Message.ToString(), 2);
            }
            return rotationNames;
        }
        /// <summary>
        /// This instance is calling all necessary methods for calculating end result
        /// The constructor connects to a database stored in Web.config
        /// Will throw a "Cannot open Database connectíon"-Exception if it fails to make a connection
        /// </summary>
        /// <param name="BedriftID"></param>
        /// <param name="ZipCodeString">the dansih zipcode</param>
        /// <param name="Cropyear">The year of harvesting </param>
        /// <param name="ScenarioID"></param>
        /// <param name="AfterCropPercent"></param>
        /// <param name="NNeedPercent">how much N the crops need</param>
        /// <param name="ManureVersion"></param>
        /// <param name="manureList"></param>
        /// <param name="arealList"></param>
        /// <param name="StdRotationVersion"></param>
        /// <param name="version">the version of FarmN that is in use</param>
        public CalculateScenario(long BedriftID, string ZipCodeString, int Cropyear, int ScenarioID, decimal AfterCropPercent, decimal NNeedPercent, int ManureVersion, List<Manure> manureList, List<StandardRotation> arealList, int StdRotationVersion, int version)
        {
            this.BedriftID=BedriftID;
            this.ZipCodeString=ZipCodeString;
            this.Cropyear=Cropyear;
            this.ScenarioID=ScenarioID;
            this.AfterCropPercent=AfterCropPercent;
            this.NNeedPercent=NNeedPercent;
            this.ManureVersion=ManureVersion;
            this.manureList = new List<Manure>();
            for(int i=0;i<manureList.Count();i++)
            {
                this.manureList.Add(manureList.ElementAt(i));
            }
            this.arealList = new List<StandardRotation>();
            for(int i=0;i<arealList.Count();i++)
            {
                this.arealList.Add(arealList.ElementAt(i));
            }
            this.StdRotationVersion=StdRotationVersion;
            this.version=version;


        }
        public void run()
        {
            cfg = new AppSettingsReader();

            sqlconnstr = cfg.GetValue("sqlConnectionStringPublic", typeof(string)).ToString();
            sqlconn = null;
            try
            {
                sqlconn = new SqlConnection(sqlconnstr);
                sqlconn.Open();
            }
            catch (Exception e)
            {
                message.Instance.addWarnings("Problemer med at forbinde til databasen", "Cannot open Database connection " + e.Message.ToString(), 2);
            }
            NNeedPercent = 100m - NNeedPercent;
            decimal totalFixation = 0;
            globalSettings.Instance.setVersion(version);
            controlInput(BedriftID, ZipCodeString, Cropyear, ScenarioID, AfterCropPercent, NNeedPercent, ManureVersion, manureList, arealList);
            
            this.zipCode = translateZipCode(ZipCodeString);
            this.cropYear = Cropyear;
            this.manureVersion = ManureVersion;
            string ScenarioName = "Scenario " + ScenarioID.ToString();


            int farmType = 2;
            if (arealList.ElementAt(0).getReferenceSedskifte().StartsWith("k", System.StringComparison.OrdinalIgnoreCase))
            {
                farmType = 3;
            }
            this.farm = new Farm(BedriftID, zipCode, farmType, ManureVersion);
            this.farm.Scenario = new Scenario(ScenarioID, ScenarioName, Cropyear, NNeedPercent);
            this.farm.Scenario = addBoughtManure(this.farm.Scenario, manureList);
            if (ScenarioID == 2)
            {
                if (AfterCropPercent > 0m)
                {
                    if (version == 1 || version == 2 || version == 3)
                    {
                        this.farm.Scenario = addAfterCropRotationList(this.farm.Scenario, arealList, AfterCropPercent, StdRotationVersion, manureList);
                    }
                    if (version == 4)
                    {
                        this.farm.Scenario = addRotationList(this.farm.Scenario, arealList, true, StdRotationVersion, manureList);
                        for (int i = 0; i < this.farm.Scenario.RotationList.Count; i++)
                        {
                            this.CalculateCropRotations(this.farm.Scenario.RotationList.ElementAt(i), AfterCropPercent);
                        }
                    }
                }
                else
                {
                    this.farm.Scenario = addRotationList(this.farm.Scenario, arealList, true, StdRotationVersion, manureList);
                }
                totalArea = 0;
                for (int i = 0; i < this.farm.Scenario.RotationList.Count; i++)//alle arealer skal ganges op, da de stadig giver 100 ha med eller uden beregning af efterafgrøde
                {
                    foreach (FieldPlan fpl in this.farm.Scenario.RotationList.ElementAt(i).FieldPlanList)
                    {
                        fpl.setArea(fpl.getArea() * arealList.ElementAt(i).getAreaHa() / 100m);
                        totalArea += fpl.getArea();
                    }
                    var FPRarea =
                        (from fpr in this.farm.Scenario.RotationList.ElementAt(i).FieldPlanRotationList
                         select fpr.getArea()).Sum();
                    foreach (FieldPlanRotation fpr in this.farm.Scenario.RotationList.ElementAt(i).FieldPlanRotationList)
                    {
                        fpr.setArea(fpr.getArea() * arealList.ElementAt(i).getAreaHa() / 100m);
                    }
                    decimal rotFixation = this.farm.Scenario.RotationList.ElementAt(i).getCropFixation();
                    decimal rotSecondFixation = this.farm.Scenario.RotationList.ElementAt(i).getSecondCropFixation();
                    this.farm.Scenario.RotationList.ElementAt(i).setCropFixation(rotFixation / 100m * arealList.ElementAt(i).getAreaHa());
                    this.farm.Scenario.RotationList.ElementAt(i).setSecondCropFixation(rotSecondFixation / 100m * arealList.ElementAt(i).getAreaHa());
                }
            }
            else
            {
                this.farm.Scenario = addRotationList(this.farm.Scenario, arealList, false, StdRotationVersion, manureList);
            }

            decimal totalManure = 0;
            foreach (BoughtManure bm in this.farm.Scenario.ManureList)
            {
                totalManure += bm.getManureAmount();
            }
            CalculateManureFertilizer = new CalculateManureFertilizerDistribution(this.manureVersion, this.farm.Scenario.getNNeedPercent(), this.farm.Scenario.ManureList, ref this.farm.Scenario);//(this.manureVersion,100m,scenario.ManureList,scenario.RotationList)
            decimal totalManureDist = CalculateManureFertilizer.totalManureDist;
            decimal totalManureLoss = CalculateManureFertilizer.getTotalManureLoss();
            decimal totalFertLoss = CalculateManureFertilizer.getTotalFertilizerLoss();
            grazingConversionFactor = CalculateManureFertilizer.getGrazingConversionFactor();
            decimal totalMineralFertilizer = Convert.ToDecimal(CalculateManureFertilizer.getTotalMineralFertilizerNiveau());
            foreach (Rotation rot in this.farm.Scenario.RotationList)
            {
                totalFixation += rot.getCropFixation() + rot.getSecondCropFixation();
            }
            var boughtGrazing =
                (from m in manureList
                 where m.getManureType() == 3m
                 select m.getKgN()).Sum();
            totalManureDist += boughtGrazing;

            decimal areaSum = 0;
            decimal amountManure = 0;
            decimal amountUtil = 0;
            for (int i = 0; i < this.farm.Scenario.ManureList.Count(); i++)
            {
                amountManure = amountManure + this.farm.Scenario.ManureList[i].getManureAmount();
                amountUtil = amountUtil + this.farm.Scenario.ManureList[i].getManureAmount() * this.farm.Scenario.ManureList[i].getBoughtUtilDegree() / 100;
            }
            decimal totalDenitrification = 0;
            decimal totalRunOff = 0;
            decimal totalDeltaSoilN = 0;
            decimal totalStrawRemoved = 0;
            decimal totalSoldCrop = 0;
            decimal totalSoldFeed = 0;
            decimal totalN_InSeed = 0;
            for (int i = 0; i < this.farm.Scenario.RotationList.Count; i++)
            {
                Rotation rot = this.CalculateDenitrification(this.farm.Scenario.RotationList.ElementAt(i), farm.getFarmtype());

                decimal fertilizer = 0;
                decimal manure = 0;
                decimal fertilizerLoss = 0;
                decimal manureLoss = 0;
                decimal catchCropAreaSum = 0;
                decimal NLesKgPrHa = 0;
                decimal NLesMgPrL = 0;
                decimal rotArea = 0;
                decimal rotHoest = 0;
                decimal rotRunoff = 0;

                for (int j = 0; j < rot.FieldPlanRotationList.Count; j++)
                {
                    FieldPlanRotation field = this.CalculateNLesValues(this.farm.Scenario.getNNeedPercent(), totalManureDist, totalMineralFertilizer, rot.FieldPlanRotationList.ElementAt(j), rot.getSoilType(), rot.getClayRatio(), rot.getHumusRatio(), totalFixation, boughtGrazing);
                    rot.FieldPlanRotationList[j] = field;
                    if (globalSettings.Instance.getRoundedValuesError() == true)
                    {
                        field.setNLesKgPrHa(Math.Round(field.getNLesKgPrHa(), 10));
                        field.setNLesMgPrL(Math.Round(field.getNLesMgPrL(), 10));
                    }
                    NLesKgPrHa += field.getNLesKgPrHa() * field.getArea();
                    NLesMgPrL += field.getNLesMgPrL() * field.getArea();
                    totalRotArea += field.getArea();
                    rotArea += field.getArea();
                    totalSoldCrop += field.getCropYield_N() * field.getArea();
                    totalSoldFeed += field.getSecondCropYield_N() * field.getArea();
                    totalStrawRemoved += field.getStrawRemoved_N() * field.getArea();
                    rotHoest += (field.getCropYield_N() + field.getSecondCropYield_N() + field.getStrawRemoved_N()) * field.getArea();
                    totalRunOff += field.getRunoff() * field.getArea();
                    rotRunoff += field.getRunoff() * field.getArea();
                    if (field.getCropAfterCrop() != 0)
                    {
                        catchCropAreaSum += field.getArea();
                    }
                    var fertilizerField =
                        (from m in field.ManureFertilizerDeliveryList
                         where m.StorageID == 9
                         select m.Kg_N_Delivered).Sum();
                    var fertilizerFieldLoss =
                        (from m in field.ManureFertilizerDeliveryList
                         where m.StorageID == 9
                         select m.Kg_N_Loss).Sum();
                    var manureField =
                        (from m in field.ManureFertilizerDeliveryList
                         where m.StorageID != 9
                         select m.Kg_N_Delivered).Sum();
                    var manureFieldLoss =
                        (from m in field.ManureFertilizerDeliveryList
                         where m.StorageID != 9
                         select m.Kg_N_Loss).Sum();
                    fertilizer += fertilizerField;
                    manure += manureField;
                    manureLoss += manureFieldLoss;
                    fertilizerLoss += fertilizerFieldLoss;
                }

                //public FarmNData(public FarmNData(string Referencesaedskifte, string Saedskifte, string Jordbundstype, List<CropData> cropListe, decimal runnOff, decimal udvaskningKgNHa, decimal udvaskningMgPrL, decimal hoest, decimal denitrifikation, decimal jordpuljeaendring, decimal markoverskud, decimal handelsGoedning, decimal husdyrGoedning, decimal ammoniumtabHandelsGoedning, decimal ammoniumtabHusdyrGoedning, decimal nFiksering, decimal atmosfaeriskAfsaetning)
                decimal fractionCatchCrop = catchCropAreaSum / rotArea;
                rot = this.CalculateSoilChange(rot, farm.getFarmtype(), zipCode, fractionCatchCrop);
                this.farm.Scenario.RotationList[i] = rot;
                List<CropData> input = new List<CropData>();
                decimal fpArea = 0;
                foreach (FieldPlan fp in rot.FieldPlanList)
                {
                    CropData cropdata = new CropData(fp.getArea(), fp.getCropName());
                    input.Add(cropdata);
                    areaSum += fp.getArea();
                    fpArea += fp.getArea();
                }

                string referenceSaedskifte = rot.GetReferencesaedskifte();
                string saedSkifte = rot.GetSaedskifte();
                string jbType = "JB" + rot.getSoilType().ToString();
                var N_InSeed =
                   (from fp in rot.FieldPlanList
                    select fp.getNInSeed() * fp.getArea()).Sum();
                totalN_InSeed += N_InSeed;
                TotalNLesKg += NLesKgPrHa;
                TotalNLesMg += NLesMgPrL;
                totalDeltaSoilN += rot.getDeltaSoilN() * rotArea;
                totalDenitrification += rot.getSimden() * fpArea;//rotArea;
                //                decimal 
                //rotRunoff = totalRunOff / rotArea;//-- rotRunoff changed 30-10-2012
                rotRunoff = rotRunoff / rotArea;
                decimal atmosfaeriskAfsaetning = 15;
                decimal fixation = (rot.getSecondCropFixation() + rot.getCropFixation()) / rotArea;
                rotHoest = rotHoest / rotArea;
                var nInSeed =
                    (from fp in rot.FieldPlanList
                     select fp.getNInSeed() * fp.getArea()).Sum() / rotArea;
                decimal markOverskud = ((fertilizer + manure) / rotArea + fixation + nInSeed + atmosfaeriskAfsaetning) - (rotHoest);


                //FarmNData returns = new FarmNData(referenceSaedskifte, saedSkifte, jbType, input, rotRunoff, NLesKgPrHa / rotArea, NLesMgPrL / rotArea, rotHoest, rot.getSimden(), rot.getDeltaSoilN(), markOverskud, fertilizer / rotArea, manure / rotArea, fertilizerLoss / rotArea, manureLoss / rotArea, fixation, atmosfaeriskAfsaetning, nInSeed);//-- Calculation of NLesMgPrL changed 30-10-2012
                FarmNData returns = new FarmNData(referenceSaedskifte, saedSkifte, jbType, input, rotRunoff, NLesKgPrHa / rotArea, NLesKgPrHa / rotArea * 443m / (rotRunoff), rotHoest, rot.getSimden(), rot.getDeltaSoilN(), markOverskud, fertilizer / rotArea, manure / rotArea, fertilizerLoss / rotArea, manureLoss / rotArea, fixation, atmosfaeriskAfsaetning, nInSeed);
                returns.CropListe = input;
                rotationData.Add(returns);
            }
            TotalNLesMg = TotalNLesKg * 443m / (totalRunOff / areaSum);
            totalManure = formatValue(totalManure, 1);
            if (globalSettings.Instance.getRoundedValuesError() == true)
            {
                totalManureLoss = Math.Floor(totalManureLoss);
                totalN_InSeed = Math.Floor(totalN_InSeed);
                //totalManure = CalculateManureFertilizer.getTotalManureDist() + boughtGrazing;
            }
            totalMineralFertilizer = Convert.ToDecimal(CalculateManureFertilizer.getTotalMineralFertilizerNiveauNegativeNNeed());
            if (totalMineralFertilizer == 0)
            {
                totalMineralFertilizer = Convert.ToDecimal(CalculateManureFertilizer.getTotalMineralFertilizerNiveau());
            }
            decimal inputTotal = totalMineralFertilizer + totalManure + totalN_InSeed + 15 * areaSum;
            decimal inputPrHa = 0;
            inputPrHa = inputTotal / areaSum;
            adjustNless(NNeedPercent, totalFixation / totalRotArea + inputPrHa, totalManure / areaSum, amountUtil / amountManure, totalDenitrification / areaSum, totalRunOff / areaSum, TotalNLesMg / areaSum, totalDeltaSoilN / totalRotArea, TotalNLesKg / areaSum, totalMineralFertilizer / areaSum, totalManureLoss / areaSum, totalSoldCrop / areaSum, totalMineralFertilizer * 0.03m / areaSum, totalStrawRemoved / areaSum, totalSoldFeed / areaSum, boughtGrazing * 0.07m / areaSum);
            for (int i = 0; i < rotationData.Count; i++)
            {
                decimal old = rotationData.ElementAt(i).UdvaskningKgNHa;
                rotationData.ElementAt(i).setNLeachKgNHa_korrigeret(old * adjustmentValue);
                old = rotationData.ElementAt(i).UdvaskningMgPrL;
                rotationData.ElementAt(i).setNLeachmgNl_korrigeret(old * adjustmentValue);
            }
            returnData.arealData = getFarmNData();
            if (sqlconn != null)
            {
                sqlconn.Close();
            }
        }

  
        /// <summary>
        /// create an opimal rotation
        /// </summary>
        /// <param name="rotation"></param>
        /// <param name="AfterCropPercent"></param>
        /// <returns></returns>
        public Rotation CalculateCropRotations(Rotation rotation,decimal AfterCropPercent)
        {
            //CalculateCropRotation CropRotation = new CalculateCropRotation(ref rotation, AfterCropPercent);
            CropRotation = new CalculateCropRotation(ref rotation, AfterCropPercent);
            return rotation;

            
        }
        /// <summary>
        /// This method is calculating and distributing BoughtManure on FieldPlanRotations Creating a List of ManureFertilizerDelivery on every relevant FieldPlanRotation
        /// </summary>
        /// <param name="scenario"></param>
        public Scenario CalculateManureFertilizerDistributions(Scenario scenario)
        {
            CalculateManureFertilizer = new CalculateManureFertilizerDistribution(this.manureVersion, 100m, scenario.ManureList,ref scenario);//(this.manureVersion,100m,scenario.ManureList,scenario.RotationList)
            grazingConversionFactor = CalculateManureFertilizer.getGrazingConversionFactor();
            
            return scenario;
        }
        /// <summary>
        /// This method is calculating total N need on the scenario
        /// </summary>
        public Rotation CalculateDenitrification(Rotation rotation, int farmType)
        {
            //int SoilCode, int FarmType, decimal FertiliserN, decimal ManureNincorp, decimal ManureNspread, decimal NFixation)
            int soilType = rotation.getSoilType();
            decimal fertilizerN = 0m;
            decimal manureNincorp = 0m;
            decimal manureNspread = 0m;
            decimal nFixation = 0;
            decimal rotArea = 0m;
            foreach (FieldPlanRotation fpr in rotation.FieldPlanRotationList)
            {
                foreach (ManureFertilizerDelivery mf in fpr.ManureFertilizerDeliveryList)
                {
                    if (mf.StorageID == 9)
                    { fertilizerN += mf.Kg_N_Delivered; }
                    else
                    {
                        if (mf.DeliveryID == 1)
                        {manureNincorp += mf.Kg_N_Delivered;}
                        else
                        {manureNspread += mf.Kg_N_Delivered;}
                    }
                }
                rotArea += fpr.getArea();
                manureNspread += fpr.getGrazingManure();
            }
            //rotArea = (from fp in rotation.FieldPlanList
            //           select fp.getArea()).Sum();
            nFixation += rotation.getCropFixation() + rotation.getSecondCropFixation();
            if (rotArea == 0) rotArea = 0.0000000000001m;
            Denitrification.init(soilType, farmType, fertilizerN/rotArea, manureNincorp/rotArea, manureNspread/rotArea, nFixation/rotArea);
            decimal result= this.Denitrification.getDenitrification();
            rotation.setSimden(result );
            return rotation;
        }
        /// <summary>
        /// preparation for calculateing N leach
        /// </summary>
        /// <param name="normPercent"></param>
        /// <param name="totalManure">Total manure that has been used</param>
        /// <param name="totalMineralFertilizer">Total Mineral Fertilizer that has neen used </param>
        /// <param name="fieldPlanRotation"></param>
        /// <param name="SoilCode">The type of soil </param>
        /// <param name="Clay">the amount of clay in the soil</param>
        /// <param name="humus">the amount of Humus in the soil</param>
        /// <param name="totalFixation"></param>
        /// <param name="boughtGrazing"></param>
        /// <returns></returns>
        public FieldPlanRotation CalculateNLesValues(decimal normPercent,decimal totalManure,decimal totalMineralFertilizer,FieldPlanRotation fieldPlanRotation,  int SoilCode, decimal Clay, decimal humus, decimal totalFixation, decimal boughtGrazing)
        {
            
            decimal n_niveau = 0;
            decimal n_spring = 0;
            n_niveau += totalMineralFertilizer;
            n_niveau += totalManure;
            n_niveau += totalFixation;
            //n_niveau += boughtGrazing;
            n_spring =
                (from m in fieldPlanRotation.ManureFertilizerDeliveryList
                 select (m.Kg_N_Delivered * m.AmmoniumRatio)).Sum();
            decimal N_Removed = fieldPlanRotation.getCropYield_N() + fieldPlanRotation.getSecondCropYield_N() + fieldPlanRotation.getStrawRemoved_N();
            this.NLes.init(n_niveau / totalArea, n_spring / fieldPlanRotation.getArea(), 0, fieldPlanRotation.getFixation() , fieldPlanRotation.getGrazingManure() / fieldPlanRotation.getArea(), N_Removed, 2001, SoilCode, humus, Clay, fieldPlanRotation.getRunoff(), fieldPlanRotation.getRunoff(), fieldPlanRotation.getCropCoeff(), fieldPlanRotation.getPreCropCoeff());
            decimal result=this.NLes.getNLesKgNPrHa();

            fieldPlanRotation.setNLesKgPrHa(result);
            result = this.NLes.getNLesMgPrL();
            fieldPlanRotation.setNLesMgPrL(result);
            
            return fieldPlanRotation;
        }
        private const decimal RED_NORM_CORRECTION_HARVEST = 0.45m;
        private const decimal RED_NORM_CORRECTION_NLES = 0.35m;
        private const decimal RED_NORM_CORRECTION_SOIL = 0.1m;
        private const decimal RED_NORM_CORRECTION_NH3EVAP = 0.01m;
        private const decimal RED_NORM_CORRECTION_DENITR = 0.08m;
        private const int NUMBER_COL = 3;
        private const int NUMBER_ROW = 10;
        private const int NUMBER_COL_SURPLUS = 4;
        private const int NUMBER_ROW_SURPLUS = 12;
        private const decimal SURPLUSCORRECTION_HARVEST = 0.45m;
        private const decimal SURPLUSCORRECTION_SOIL = 0.10m;
        private const decimal SURPLUSCORRECTION_DENITR = 0.10m;
        private const decimal SURPLUSCORRECTION_NLES = 0.35m;

        /// <summary>
        /// ajust nleaching
        /// </summary>
        /// <param name="NNeedPercent"></param>
        /// <param name="inputTotal">the maximum amount of N in the system</param>
        /// <param name="manure"></param>
        /// <param name="meanUtilization"></param>
        /// <param name="Denitrification"></param>
        /// <param name="meanRunOff"> the average runoff </param>
        /// <param name="meanNLES_mg"></param>
        /// <param name="aSoilChange"></param>
        /// <param name="meanNLES_N">the average leaching</param>
        /// <param name="fertilizer"></param>
        /// <param name="totalLossManure"></param>
        /// <param name="soldCrop"></param>
        /// <param name="totalLossFertilizer"></param>
        /// <param name="totalStrawRemoved"></param>
        /// <param name="soldFeed"></param>
        /// <param name="grazingLoss"></param>
        public void adjustNless(decimal NNeedPercent, decimal inputTotal, decimal manure, decimal meanUtilization, decimal Denitrification, decimal meanRunOff, decimal meanNLES_mg, decimal aSoilChange, decimal meanNLES_N, decimal fertilizer, decimal totalLossManure, decimal soldCrop, decimal totalLossFertilizer, decimal totalStrawRemoved, decimal soldFeed, decimal grazingLoss)
        {
 
            decimal reducedFertilizer = 0;

            decimal outputTotal = soldCrop+soldFeed+totalStrawRemoved;
            decimal NLeaching = inputTotal - outputTotal - totalLossManure - totalLossFertilizer - aSoilChange - Denitrification - grazingLoss;
            decimal remainder = NLeaching - meanNLES_N;

            decimal[,] arrInput = new decimal[NUMBER_ROW, NUMBER_COL];
            decimal[,] arrOutput = new decimal[NUMBER_ROW, NUMBER_COL + 1];
            decimal[,] arrSurplus = new decimal[NUMBER_ROW_SURPLUS, NUMBER_COL_SURPLUS];
            arrInput[0, 1] = fertilizer;
            arrInput[1, 1] = manure;
            decimal minNormReduction = Math.Round(100 * arrInput[1, 1] * meanUtilization / (arrInput[0, 1] + (arrInput[1, 1] * meanUtilization)),0);
            if (NNeedPercent < minNormReduction)
            {
                NNeedPercent = minNormReduction;
            }
            arrInput[0, 2] = Math.Round(arrInput[0, 1], 1) - (((meanUtilization) * Math.Round(arrInput[1, 1], 1) + Math.Round(arrInput[0, 1], 1)) - ((meanUtilization) * Math.Round(arrInput[1, 1], 1) + Math.Round(arrInput[0, 1], 1)) * (NNeedPercent / 100));
            reducedFertilizer = Math.Round(arrInput[0, 1], 1) - Math.Round(arrInput[0, 2], 1);
            arrOutput[0, 1] = soldCrop;
            arrOutput[6, 1] = totalStrawRemoved;
            //arrOutput(8,1)
            arrOutput[8, 2] = arrOutput[0, 1] + totalStrawRemoved;
            arrOutput[8, 3] = arrOutput[8, 2]- reducedFertilizer * RED_NORM_CORRECTION_HARVEST;
            //arrOutput[9, 1] = arrOutput[0, 1] + arrOutput[1, 1] + arrOutput[2, 1] + arrOutput[3, 1] + arrOutput[4, 1] + arrOutput[5, 1] + arrOutput[6, 1] + arrOutput[7, 1];
            arrOutput[9, 1] = soldCrop + totalStrawRemoved + soldFeed;
            decimal surplus = inputTotal - arrOutput[9, 1];
            arrSurplus[3, 1] = surplus;
            decimal surplusCorr = surplus-remainder*SURPLUSCORRECTION_HARVEST;
            arrSurplus[3, 2] = surplusCorr - arrSurplus[0, 1] - arrSurplus[1, 1] - arrSurplus[2, 1];
            arrSurplus[5, 2] = totalLossManure;
            arrSurplus[5, 3] = totalLossManure;

            arrSurplus[6, 3] = totalLossFertilizer - reducedFertilizer * RED_NORM_CORRECTION_NH3EVAP;
            
            arrSurplus[7, 1] = Denitrification;
            arrSurplus[7, 2] = arrSurplus[7, 1] + remainder * SURPLUSCORRECTION_DENITR;
            arrSurplus[7, 3] = arrSurplus[7, 2] - reducedFertilizer * RED_NORM_CORRECTION_DENITR;
            arrSurplus[8, 1] = aSoilChange;
            arrSurplus[8, 2] = (arrSurplus[8, 1] + remainder * SURPLUSCORRECTION_SOIL);
            arrSurplus[8, 3] = arrSurplus[8, 2] - reducedFertilizer * RED_NORM_CORRECTION_SOIL;
            arrSurplus[9, 1] = meanNLES_N;
            returnData.NLeach_KgN_ha_total = meanNLES_N;
            arrSurplus[9, 2] = arrSurplus[9, 1] + remainder * SURPLUSCORRECTION_NLES;
            arrSurplus[9,3] = formatValue(arrSurplus[9,2],1)-reducedFertilizer*RED_NORM_CORRECTION_NLES;
            arrSurplus[10, 1] = remainder;
            arrSurplus[11, 1] = meanNLES_mg;
            returnData.NLeach_mgN_l_total = meanNLES_mg;
            arrSurplus[11, 2] = 100m * arrSurplus[9, 2] * 4.43m / meanRunOff;
            arrSurplus[11, 3] = 100m * formatValue(arrSurplus[9, 3],1) * 4.43m / meanRunOff;
            TotalNLesKg = formatValue(arrSurplus[9, 3],1);
            TotalNLesMg = formatValue(arrSurplus[11, 3],1);
            adjustmentValue = formatValue(arrSurplus[9, 3],1) / formatValue(arrSurplus[9, 1],1);
            returnData.NLeach_KgN_ha_korrigeret = TotalNLesKg;
            returnData.NLeach_mgN_l_korrigeret = TotalNLesMg;
            returnData.JordPuljeaendring = aSoilChange;
            returnData.JordPuljeaendring_korrigeret = arrSurplus[8, 3];
            returnData.Hoest = outputTotal;
            returnData.Hoest_korrigeret = outputTotal + (remainder * SURPLUSCORRECTION_HARVEST) - (reducedFertilizer * RED_NORM_CORRECTION_HARVEST);
            returnData.Denitrifikation = Denitrification;
            returnData.Denitrifikation_korrigeret = arrSurplus[7, 3];
            returnData.RestKorrektion = remainder;
            returnData.MarkOverskud = surplus;
            returnData.MarkOverskud_korrigeret = inputTotal - reducedFertilizer - outputTotal - (remainder * SURPLUSCORRECTION_HARVEST) + (reducedFertilizer * RED_NORM_CORRECTION_HARVEST);
        }
        /// <summary>
        /// Preparion for calling the Soil change algoritme 
        /// </summary>
        /// <param name="rotation"></param>
        /// <param name="farmType"></param>
        /// <param name="postalCode"></param>
        /// <param name="fractionCatchCrop"></param>
        /// <returns></returns>
        public Rotation CalculateSoilChange(Rotation rotation, int farmType, int postalCode, decimal fractionCatchCrop)
        {
            //init(int SoilCode, int FarmType, int PostalCode, decimal TotalCarbonFromCrops, decimal TotalCarbonFromManure, decimal FractionCatchCrops, decimal Clay)
            decimal totalCarbonFromManure = 0;
            decimal grazingManure = 0;
            var totalCarbonFromCrops =
                (from fpr in rotation.FieldPlanRotationList
                 select 0.45m * (fpr.getPotentialDMDeposition() - fpr.getStrawDMRemoved())*fpr.getArea() ).Sum();
            var rotArea =
                (from fpr in rotation.FieldPlanList//
                 select fpr.getArea()).Sum();
            var totalFPRArea = (from fp in rotation.FieldPlanRotationList
                           select fp.getArea()).Sum();
            totalCarbonFromCrops = totalCarbonFromCrops / rotArea;
            foreach (FieldPlanRotation fpl in rotation.FieldPlanRotationList)
            {
                var CarbonManure =
                    (from md in fpl.ManureFertilizerDeliveryList
                     where md.StorageID != 9
                     select (md.Kg_N_Delivered * md.ConversionFactor) / 1000m / totalFPRArea ).Sum();//der deles med 1000 for at konvertere til tons C pr. ha
                totalCarbonFromManure += CarbonManure;
                grazingManure += fpl.getGrazingManure();// / fpl.getArea();
            }
            grazingManure = grazingManure * grazingConversionFactor / 1000m;//der deles med 1000 for at konvertere til tons C pr. ha
            totalCarbonFromManure = totalCarbonFromManure + (grazingManure / rotArea);
            this.SoilChange.init(rotation.getSoilType(), farmType, postalCode, totalCarbonFromCrops, totalCarbonFromManure, fractionCatchCrop, rotation.getClayRatio());
            decimal result=this.SoilChange.getSoilChange();
            rotation.setDeltaSoilN(result);
            return rotation;
        }
      
        /*
         * Checking the database if we know the Zipcode
         * If it is unknown there will be trown a "ZipCode is not valid"
         * If there is a problem with the database it will trow a "Something wrong with database"
         */
        private int translateZipCode(string zipCode)
        {

            int retval = -1;
            bool OK = false;
            try
            {
              
                SqlCommand cmd = new SqlCommand("MST_CheckZipCode", sqlconn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@zipCode", SqlDbType.Int);
                cmd.Parameters[cmd.Parameters.Count - 1].Value = Int32.Parse(zipCode);

                cmd.Parameters.Add("@OK", SqlDbType.Int);
                cmd.Parameters["@OK"].Direction = ParameterDirection.Output;

                retval = cmd.ExecuteNonQuery();

                OK = Convert.ToBoolean(cmd.Parameters["@OK"].Value);

                if (OK == false)
                {
                    retval = -1;
                    message.Instance.addWarnings("Ukendt postnummer","ZipCode is not valid",1);
                }
                else
                {
                    retval = Int32.Parse(zipCode);
                }



            }
            catch(Exception e)
            {
                if(e.Message.CompareTo("ZipCode is not valid")==0)
                    message.Instance.addWarnings("Ukendt postnummer", "ZipCode is not valid", 1);
                message.Instance.addWarnings("Problemer med databaseopslag for postnummer", "Something wrong with database with translate zipcode", 2);
            }
            return retval;
        }
        /*
         * Creates the ManureList in scenarioIput. 
         * It creates a new Manure instance for each item in manureAmount
         * 
         */
        private Scenario addBoughtManure(Scenario scenarioInput, List<Manure> manureAmount)
        {

            foreach (Manure g in manureAmount)
            {
                //int ManureTypeInt = translateManureType(g.Goedningstype.Navn);
                BoughtManure tmpManure = new BoughtManure(g.getKgN(), g.getManureType(), g.getUtilizationDegree());
                scenarioInput.ManureList.Add(tmpManure);

            }
            return scenarioInput;
        }
        /*
         * Returning how many StorageTypes is in the database 
         */
        private int getNumberOfStorageTypes()
        {
            int count = 0;
            int retval;
            try
            {
                string cmdstr = "MST_GetSalesOrderList";
                SqlCommand cmd = new SqlCommand(cmdstr, sqlconn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@count", SqlDbType.Int);
                cmd.Parameters["@count"].Direction = ParameterDirection.Output;
                retval = cmd.ExecuteNonQuery();
                count = Convert.ToInt32(cmd.Parameters["@count"].Value);
            }
            catch (Exception e)
            {
                message.Instance.addWarnings("Problemer med databasen", "Something wrong with getNumberOfStorageTypes " + e.Message.ToString(), 2);
            }
            return count;
        }

        /*
         * It will add fieldplans to rotationInput.
         * The data stored in the fieldplans is found the the database.
         * If it fails it will throw a "Something wrong with addFieldPlanList" exception
         */
        private Rotation addFieldPlanList(Rotation rotationInput, string standardRotationName, decimal rotationArea, int rotationversion)
        {
            try
            {
                string cmdstr = "MST_getStandardRotationFieldPlan_" + rotationversion;
                SqlCommand cmd = new SqlCommand(cmdstr, sqlconn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@StandardRotationName", SqlDbType.VarChar, 20);
                cmd.Parameters[cmd.Parameters.Count - 1].Value = standardRotationName;
                cmd.Parameters.Add("@RotationArea", SqlDbType.Float);
                cmd.Parameters[cmd.Parameters.Count - 1].Value = rotationArea;
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    int Crop = Convert.ToInt32(reader["Crop"]);
                    string CropName = Convert.ToString(reader["CropName"]);
                    decimal Area = Convert.ToDecimal(reader["Area"]);
                    int AfterCropID = Convert.ToInt32(reader["AfterCropID"]);
                    bool StatutoryAfterCropBasis = Convert.ToBoolean(reader["Statutory_AfterCrop_Basis"]);
                    bool SpringSown = Convert.ToBoolean(reader["SpringSown"]);
                    bool CanHaveAfterCrop = Convert.ToBoolean(reader["Can_Have_AfterCrop"]);
                    decimal N_InSeed = Convert.ToDecimal(reader["N_InSeed"]);
                    FieldPlan tmp = new FieldPlan(Crop, CropName, Area, AfterCropID, StatutoryAfterCropBasis, SpringSown, CanHaveAfterCrop, N_InSeed);
                    rotationInput.FieldPlanList.Add(tmp);
                }
                reader.Close();
            }
            catch(Exception e)
            {
                message.Instance.addWarnings("Problemer med databasen","Something wrong with addFieldPlanList "+e.Message.ToString(),2);
            }
            return rotationInput;
        }

        /*
         * It will add fieldplans with calculated aftercrop to rotationInput.
         * The data stored in the fieldplans is found in the database.
         * If it fails it will throw a "Something wrong with addAfterCropFieldPlanList" exception
         */
        private Rotation addAfterCropFieldPlanList(Rotation rotationInput, string standardRotationName, decimal AfterCropPercent, int rotationversion)
        {
            int retval = -1;
            decimal realAfterCropPercent = 0;
            try
            {
                string cmdstr = "MST_getAfterCropStandardRotationFieldPlan";
                SqlCommand cmd = new SqlCommand(cmdstr, sqlconn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@rotationName", SqlDbType.VarChar, 4);
                cmd.Parameters[cmd.Parameters.Count - 1].Value = standardRotationName;
                cmd.Parameters.Add("@afterCropPercent", SqlDbType.Float);
                cmd.Parameters[cmd.Parameters.Count - 1].Value = AfterCropPercent;
                cmd.Parameters.Add("@version", SqlDbType.Int);
                cmd.Parameters[cmd.Parameters.Count - 1].Value = rotationversion;
                cmd.Parameters.Add("@RealAfterCropPercent", SqlDbType.Float);
                cmd.Parameters["@RealAfterCropPercent"].Direction = ParameterDirection.Output;
                retval = cmd.ExecuteNonQuery();
                realAfterCropPercent = Convert.ToDecimal(cmd.Parameters["@RealAfterCropPercent"].Value);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    int Crop = Convert.ToInt32(reader["Crop"]);
                    string CropName = Convert.ToString(reader["CropName"]);
                    decimal Area = Convert.ToDecimal(reader["Area"]);
                    int AfterCropID = Convert.ToInt32(reader["AfterCropID"]);
                    bool StatutoryAfterCropBasis = Convert.ToBoolean(reader["Statutory_AfterCrop_Basis"]);
                    bool SpringSown = Convert.ToBoolean(reader["SpringSown"]);
                    bool CanHaveAfterCrop = Convert.ToBoolean(reader["Can_Have_AfterCrop"]);
                    decimal N_InSeed = Convert.ToDecimal(reader["N_InSeed"]);
                    FieldPlan tmp = new FieldPlan(Crop, CropName, Area, AfterCropID, StatutoryAfterCropBasis, SpringSown, CanHaveAfterCrop, N_InSeed);
                    rotationInput.FieldPlanList.Add(tmp);
                }
                reader.Close();
                if (AfterCropPercent > 40m)
                {
                    AfterCropPercent = Math.Round(AfterCropPercent, 1);
                }
                if (realAfterCropPercent < AfterCropPercent)
                {
                    message.Instance.addWarnings("Der kan maximalt placeres " + realAfterCropPercent.ToString() + " % paa saedskifte " + standardRotationName, "RealAftercropPercent adjusted", 3);
                }
            }
            catch (Exception e)
            {
                message.Instance.addWarnings("Problemer med databasen", "Something wrong with addAfterCropFieldPlanList " + e.Message.ToString(), 2);
            }
            return rotationInput;
        }
 
        /*
         * Creating a new FieldPlanRotation and adding that to rotationInput.
         * N_Les,CropFixation,SecondCropFixation,CropCoeff, PreCropCoeff, Runoff, PotentialDMDeposition and FieldNNeed is set to -1
         * PreviousCrop, PreCropOriginalID, PreCrop_afterCrop,PreCropSecondCrop,Crop,CropAfterCrop, area, StrawUseType,SecondCropID, PrePreCropID, OrganicFertilizer, GrazingPart and SalePart is found in the databse.
         * It will throw a "Something wrong with addFieldPlanRotationList" exeption if it fails
         */
        private Rotation addFieldPlanRotationList(Rotation rotationInput, string standardRotationName, decimal rotationArea, int rotationversion, List<Manure> manureList)
        {
            decimal totalFixation = 0;
            decimal totalSecondFixation = 0;
            int numberOfStorageTypes = this.getNumberOfStorageTypes();
            double[] arrDelivery = new double[numberOfStorageTypes];
            double[] arrCropUtil = new double[numberOfStorageTypes];
            double[] arrLossList = new double[numberOfStorageTypes];
            try
            {
                
                string cmdstr = "MST_getStandardRotationFieldPlanRotation_" + rotationversion;
                SqlCommand cmd = new SqlCommand(cmdstr, sqlconn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@StandardRotationName", SqlDbType.VarChar, 20);
                cmd.Parameters["@StandardRotationName"].Value = standardRotationName;
                cmd.Parameters.Add("@RotationArea", SqlDbType.Float);
                cmd.Parameters["@RotationArea"].Value = rotationArea;
                cmd.Parameters.Add("@aSoilType", SqlDbType.Int);
                cmd.Parameters["@aSoilType"].Value = rotationInput.getSoilType();
                cmd.Parameters.Add("@aCropYear", SqlDbType.Int);
                cmd.Parameters["@aCropYear"].Value = cropYear;
                cmd.Parameters.Add("@aZipCode", SqlDbType.Int);
                cmd.Parameters.Add("@StrawDMError", SqlDbType.Bit);
                cmd.Parameters["@aZipCode"].Value = zipCode;
                if (globalSettings.Instance.getDeltaSoilNStrawDMRemovedError() == true)
                {
                    cmd.Parameters["@StrawDMError"].Value = true;
                }
                else
                {
                    cmd.Parameters["@StrawDMError"].Value = false;
                }
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    int ID = Convert.ToInt32(reader["FieldPlanRotationID"]);
                    int PreviousCrop = Convert.ToInt32(reader["PreviousCrop"]);
                    int PreCropOriginalID = Convert.ToInt32(reader["PreCropOriginalID"]); 
                    int PreCrop_afterCrop = Convert.ToInt32(reader["PreCrop_afterCrop"]); 
                    int PreCropSecondCrop = Convert.ToInt32(reader["PreCropSecondCrop"]); 
                    int Crop = Convert.ToInt32(reader["Crop"]); 
                    int CropAfterCrop = Convert.ToInt32(reader["Crop_AfterCrop"]);
                    decimal area = Convert.ToDecimal(reader["Area"]);
                    int StrawUseType = Convert.ToInt32(reader["StrawUseType"]);
                    int SecondCropID = Convert.ToInt32(reader["SecondCropID"]);
                    int PrePreCropID = Convert.ToInt32(reader["PrePreCropID"]);
                    int OrganicFertilizer = Convert.ToInt32(reader["OrganicFertilizer"]); 
                    decimal GrazingPart = Convert.ToDecimal(reader["UseGrazing"]);
                    decimal SalePart = Convert.ToDecimal(reader["SalePart"]);
                    decimal N_Les = -1;
                    decimal CropFixation = Convert.ToDecimal(reader["CropFixation"]);
                    decimal SecondCropFixation = Convert.ToDecimal(reader["SecondCropFixation"]);
                    decimal CropCoeff = Convert.ToDecimal(reader["CropCoeff"]);
                    decimal PreCropCoeff ;
                    if (globalSettings.Instance.getMissingPreCropCoeffInDatabaseError() == true)
                    {
                        if (standardRotationName.ToLower().CompareTo("k13") == 0 || standardRotationName.ToLower().CompareTo("08 k13") == 0)
                            PreCropCoeff = -9999;
                        else
                            PreCropCoeff = Convert.ToDecimal(reader["PreCropCoeff"]);
                    }
                    else
                        PreCropCoeff = Convert.ToDecimal(reader["PreCropCoeff"]);
                    decimal Runoff = Convert.ToDecimal(reader["Runoff"]);
                    decimal PotentialDMDeposition = Convert.ToDecimal(reader["PotentialDMDeposition"]);
                    double FieldNNeed = Convert.ToDouble(reader["FieldNNeed"]);
                    decimal CropYield = Convert.ToDecimal(reader["CropYield"]);
                    decimal SecondCropYield = Convert.ToDecimal(reader["SecondCropYield"]);
                    decimal StrawYield = Convert.ToDecimal(reader["StrawYield_N"]);
                    decimal StrawRemoved;
                    decimal StrawDMRemoved;
                    try
                    {
                         StrawRemoved = Convert.ToDecimal(reader["StrawRemoved_N"]);
                    }
                    catch
                    {
                        StrawRemoved = 0;
                    }
                    try
                    {
                        StrawDMRemoved = Convert.ToDecimal(reader["StrawDMRemoved"]);
                    }
                    catch
                    {
                        StrawDMRemoved = 0;
                    }

                    arrDelivery = new double[numberOfStorageTypes];
                    arrCropUtil = new double[numberOfStorageTypes];
                    arrLossList = new double[numberOfStorageTypes];
                    FieldPlanRotation tmp = new FieldPlanRotation(ID, PreviousCrop, PreCropOriginalID, PreCrop_afterCrop, PreCropSecondCrop, Crop, CropAfterCrop, OrganicFertilizer, StrawUseType, SecondCropID, PrePreCropID, area, GrazingPart, SalePart, CropFixation, SecondCropFixation, CropCoeff, PreCropCoeff, Runoff, PotentialDMDeposition, FieldNNeed, CropYield, SecondCropYield, StrawYield, StrawRemoved, StrawDMRemoved, rotationInput.getSoilType(), arrDelivery, arrCropUtil, arrLossList);
                    rotationInput.FieldPlanRotationList.Add(tmp);
                    totalFixation += CropFixation * area;
                    totalSecondFixation += SecondCropFixation * area;
                }
                reader.Close();
                rotationInput = this.fillUtilDeliveryArrays(rotationInput, numberOfStorageTypes,  manureList);
                rotationInput.setCropFixation(totalFixation);
                rotationInput.setSecondCropFixation(totalSecondFixation);
            }
                
            catch(Exception e)
            {
                message.Instance.addWarnings("Problemer med databasen MST_getStandardRotationFieldPlanRotation", "Something wrong with addFieldPlanRotationList. Error is: " + e.Message, 2);
            }
            return rotationInput;
        }
        /*
         * Creating a new FieldPlanRotation with calculated aftercrop and adding it to rotationInput.
         * N_Les,CropFixation,SecondCropFixation,CropCoeff, PreCropCoeff, Runoff, PotentialDMDeposition and FieldNNeed is set to -1
         * PreviousCrop, PreCropOriginalID, PreCrop_afterCrop,PreCropSecondCrop,Crop,CropAfterCrop, area, StrawUseType,SecondCropID, PrePreCropID, OrganicFertilizer, GrazingPart and SalePart is found in the databse.
         * It will throw a "Something wrong with addAfterCropFieldPlanRotationList" exeption if it fails
         */
        private Rotation addAfterCropFieldPlanRotationList(Rotation rotationInput, string standardRotationName, decimal AfterCropPercent, int rotationversion, List<Manure> manureList)
        {
            decimal totalFixation = 0;
            decimal totalSecondFixation = 0;
            int numberOfStorageTypes = this.getNumberOfStorageTypes();
            double[] arrDelivery = new double[numberOfStorageTypes];
            double[] arrCropUtil = new double[numberOfStorageTypes];
            double[] arrLossList = new double[numberOfStorageTypes];
            try
            {

                string cmdstr = "MST_getAfterCropStandardRotationFieldPlanRotation";
                SqlCommand cmd = new SqlCommand(cmdstr, sqlconn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@rotationName", SqlDbType.VarChar, 4);
                cmd.Parameters["@rotationName"].Value = standardRotationName;
                cmd.Parameters.Add("@afterCropPercent", SqlDbType.Float);
                cmd.Parameters["@afterCropPercent"].Value = AfterCropPercent;
                cmd.Parameters.Add("@version", SqlDbType.Int);
                cmd.Parameters["@version"].Value = rotationversion;
                cmd.Parameters.Add("@aSoilType", SqlDbType.Int);
                cmd.Parameters["@aSoilType"].Value = rotationInput.getSoilType();
                cmd.Parameters.Add("@aCropYear", SqlDbType.Int);
                cmd.Parameters["@aCropYear"].Value = cropYear;
                cmd.Parameters.Add("@aZipCode", SqlDbType.Int);
                cmd.Parameters.Add("@StrawDMError", SqlDbType.Bit);
                cmd.Parameters["@aZipCode"].Value = zipCode;
                if (globalSettings.Instance.getDeltaSoilNStrawDMRemovedError() == true)
                {
                    cmd.Parameters["@StrawDMError"].Value = true;
                }
                else
                {
                    cmd.Parameters["@StrawDMError"].Value = false;
                }
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    int ID = Convert.ToInt32(reader["FieldPlanRotationID"]);
                    int PreviousCrop = Convert.ToInt32(reader["PreviousCrop"]);
                    int PreCropOriginalID = Convert.ToInt32(reader["PreCropOriginalID"]);
                    int PreCrop_afterCrop = Convert.ToInt32(reader["PreCrop_afterCrop"]);
                    int PreCropSecondCrop = Convert.ToInt32(reader["PreCropSecondCrop"]);
                    int Crop = Convert.ToInt32(reader["Crop"]);
                    int CropAfterCrop = Convert.ToInt32(reader["Crop_AfterCrop"]);
                    decimal area = Convert.ToDecimal(reader["Area"]);
                    int StrawUseType = Convert.ToInt32(reader["StrawUseType"]);
                    int SecondCropID = Convert.ToInt32(reader["SecondCropID"]);
                    int PrePreCropID = Convert.ToInt32(reader["PrePreCropID"]);
                    int OrganicFertilizer = Convert.ToInt32(reader["OrganicFertilizer"]);
                    decimal GrazingPart = Convert.ToDecimal(reader["UseGrazing"]);
                    decimal SalePart = Convert.ToDecimal(reader["SalePart"]);
                    decimal N_Les = -1;
                    decimal CropFixation = Convert.ToDecimal(reader["CropFixation"]);
                    decimal SecondCropFixation = Convert.ToDecimal(reader["SecondCropFixation"]);
                    decimal CropCoeff;
                    try
                    {
                        CropCoeff = Convert.ToDecimal(reader["CropCoeff"]);
                    }
                    catch
                    {
                    if (globalSettings.Instance.getMissingPreCropCoeffInDatabaseError() == true)
                        {
                            CropCoeff = -9999m;
                        }
                    else
                        {
                            switch (Crop)
                            {
                                case 20: CropCoeff = -7.6m;
                                    break;
                                case 22: CropCoeff = -98.6m;
                                    break;
                                default : CropCoeff = -9999m;
                                    break;
                            }
                        }
                    }
                    decimal PreCropCoeff;
                    //try
                    //    {
                            PreCropCoeff = Convert.ToDecimal(reader["PreCropCoeff"]);
                        //}
                    //catch
                    //    {
                            if (globalSettings.Instance.getMissingPreCropCoeffInDatabaseError() == true)
                            {
                                if (standardRotationName.ToLower().CompareTo("k13") == 0 || standardRotationName.ToLower().CompareTo("08 k13") == 0)
                                    PreCropCoeff = -9999;
                                else
                                    PreCropCoeff = Convert.ToDecimal(reader["PreCropCoeff"]);
                            }
                            else
                            {
                                switch (PreCropOriginalID)
                                {
                                    case 20: PreCropCoeff = 14.2m;
                                        break;
                                    case 22: PreCropCoeff = 0m;
                                        break;
                                    default: PreCropCoeff = -9999m;
                                        break;
                                }
                            }
                    //        if (standardRotationName.ToLower().CompareTo("k13") == 0 || standardRotationName.ToLower().CompareTo("08 k13") == 0)//---
                    //        {
                    //            PreCropCoeff = -9999m;
                    //        }//---
                        //}
                    decimal Runoff = Convert.ToDecimal(reader["Runoff"]);
                    decimal PotentialDMDeposition = Convert.ToDecimal(reader["PotentialDMDeposition"]);
                    double FieldNNeed = Convert.ToDouble(reader["FieldNNeed"]);
                    decimal CropYield = Convert.ToDecimal(reader["CropYield"]);
                    decimal SecondCropYield = Convert.ToDecimal(reader["SecondCropYield"]);
                    decimal StrawYield = Convert.ToDecimal(reader["StrawYield_N"]);
                    decimal StrawRemoved;
                    decimal StrawDMRemoved;
                    try
                    {
                        StrawRemoved = Convert.ToDecimal(reader["StrawRemoved_N"]);
                    }
                    catch
                    {
                        StrawRemoved = 0;
                    }
                    try
                    {
                        StrawDMRemoved = Convert.ToDecimal(reader["StrawDMRemoved"]);
                    }
                    catch
                    {
                        StrawDMRemoved = 0;
                    }

                    arrDelivery = new double[numberOfStorageTypes];
                    arrCropUtil = new double[numberOfStorageTypes];
                    arrLossList = new double[numberOfStorageTypes];
                    FieldPlanRotation tmp = new FieldPlanRotation(ID, PreviousCrop, PreCropOriginalID, PreCrop_afterCrop, PreCropSecondCrop, Crop, CropAfterCrop, OrganicFertilizer, StrawUseType, SecondCropID, PrePreCropID, area, GrazingPart, SalePart, CropFixation, SecondCropFixation, CropCoeff, PreCropCoeff, Runoff, PotentialDMDeposition, FieldNNeed, CropYield, SecondCropYield, StrawYield, StrawRemoved, StrawDMRemoved, rotationInput.getSoilType(), arrDelivery, arrCropUtil, arrLossList);
                    rotationInput.FieldPlanRotationList.Add(tmp);
                    totalFixation += CropFixation * area;
                    totalSecondFixation += SecondCropFixation * area;
                }
                reader.Close();
                rotationInput = this.fillUtilDeliveryArrays(rotationInput, numberOfStorageTypes, manureList);
                rotationInput.setCropFixation(totalFixation);
                rotationInput.setSecondCropFixation(totalSecondFixation);
            }

            catch (Exception e)
            {
                message.Instance.addWarnings("Problemer med databasen MST_getAfterCropStandardRotationFieldPlanRotation", "Something wrong with addAfterCropFieldPlanRotationList. Error is: " + e.Message, 2);
            }
            return rotationInput;
        }
        /*
         * Filling arrays in FieldPlanRotations with DeliveryID and CropUtilizationDegrees. 
         * If usedInApplication is true the created rotation has the area 100, otherwise it will have the area Ha stored in arealer
         * 
         */
        private Rotation fillUtilDeliveryArrays(Rotation rotationInput, int numberOfStorageTypes,List<Manure> manureList)
        {
            int count = 0;
            int retval;
            int x = 0;
            string cmdstr1;
            SqlCommand cmd1;

            try
            {


                cmdstr1 = "MST_GetGlobalCropList";
                cmd1 = new SqlCommand(cmdstr1, sqlconn);
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.Parameters.Add("@count", SqlDbType.Int);
                cmd1.Parameters["@count"].Direction = ParameterDirection.Output;
                retval = cmd1.ExecuteNonQuery();
                count = Convert.ToInt32(cmd1.Parameters["@count"].Value);
                globalCropList = new int[count];
                globalDeliveryList = new double[count][];
                globalUtilDegreeList = new double[count][];
                globalLossList = new double[count][];
                SqlDataReader reader1 = cmd1.ExecuteReader();
                x = 0;
                while (reader1.Read())
                {
                    globalCropList[x] = Convert.ToInt32(reader1["Crop"]);
                    x += 1;
                }
                reader1.Close();
                int cnt=0;
                foreach (int i in globalCropList)
                {
                    cmdstr1 = "MST_GetSalesOrderUtilDegreeList";
                    cmd1 = new SqlCommand(cmdstr1, sqlconn);
                    cmd1.CommandType = CommandType.StoredProcedure;
                    cmd1.Parameters.Add("@CropID", SqlDbType.Int);
                    cmd1.Parameters["@CropID"].Value = i;
                    reader1 = cmd1.ExecuteReader();
                    x = 0;
                    double[] deliveryList = new double[numberOfStorageTypes];
                    double[] cropUtilList = new double[numberOfStorageTypes];
                    double[] lossList = new double[numberOfStorageTypes];
                    while (reader1.Read())
                    {
                        deliveryList[x] = Convert.ToDouble(reader1["DeliveryID"]);
                        cropUtilList[x] = Convert.ToDouble(reader1["CropUtilizationDegree"]);
                        lossList[x] = Convert.ToDouble(reader1["NLoss"]);
                        x += 1;
                    }
                    reader1.Close();
                    globalDeliveryList[cnt] = deliveryList;
                    globalUtilDegreeList[cnt] = cropUtilList;
                    globalLossList[cnt] = lossList;
                    deliveryList = null;
                    cropUtilList = null;
                    lossList = null;
                    cnt += 1;
                }
            }
            catch (Exception e)
            {
                message.Instance.addWarnings("Problemer med Databasen MST_GetSalesOrderUtilDegreeList", "fillUtilDeliveryArrays " + e.Message.ToString(), 2);
            }

            //Nu skal alle fieldPlanrotations have erstattet deres DeliveryArrays og UtilizationArrays
            foreach (FieldPlanRotation fplr in rotationInput.FieldPlanRotationList)
            {
                int indx = Array.IndexOf(globalCropList, Convert.ToInt32(fplr.getCrop()));
                fplr.CropUtilList = globalUtilDegreeList[indx];
                fplr.DeliveryList = globalDeliveryList[indx];
                fplr.LossList = globalLossList[indx];
            }
            return rotationInput;

        }
        /*
         * Adding Rotations to scenarioInput for each Areal. 
         * If usedInApplication is true the created rotation has the area 100, otherwise it will have the area Ha stored in arealer
         * 
         */
        private Scenario addRotationList(Scenario scenarioInput, List<StandardRotation> arealer, bool usedInApplication, int stdRotationVersion, List<Manure> manureList)
        {
            foreach (StandardRotation a in arealer)
            {
                if (usedInApplication == true)
                    scenarioInput.RotationList.Add(getStandardRotation(a.getStdRotationName(), a.getSoilType(), 100m, stdRotationVersion, a.getReferenceSedskifte(), a.getSedskifte(), a.getarealType(), manureList));
                else
                {
                    Rotation tmp = getStandardRotation(a.getReferenceSedskifte(), a.getSoilType(), a.getAreaHa(), stdRotationVersion, a.getReferenceSedskifte(), a.getSedskifte(), a.getarealType(), manureList);
                    scenarioInput.RotationList.Add(tmp);
                }

            }
            return scenarioInput;

        }
        /*
         * Adding Rotations with calculated aftercrop to scenarioInput for each Areal. 
         * The created rotation has the area 100
         * 
         */
        private Scenario addAfterCropRotationList(Scenario scenarioInput, List<StandardRotation> arealer, decimal AfterCropPercent, int stdRotationVersion, List<Manure> manureList)
        {
            foreach (StandardRotation a in arealer)
            {
                    scenarioInput.RotationList.Add(getAfterCropStandardRotation(a.getStdRotationName(), a.getSoilType(), AfterCropPercent, stdRotationVersion, a.getReferenceSedskifte(), a.getSedskifte(), a.getarealType(), manureList));

            }
            return scenarioInput;

        }
        /*
         *  Creates a single Rotation and adds FieldPlanRotation and FieldPlan to that Rotation
         * 
         * 
         */
        private Rotation getStandardRotation(string standardRotationName, int STint, decimal rotationArea, int rotationversion, string Referencesaedskifte, string Saedskifte, int ArealType, List<Manure> manureList)
        {
            decimal clayRatio=-1;
            decimal HumusRatio = -1;
            try
            {
                string cmdstr = "MST_GetClayAndHumusRatioPrSoilType";
                SqlCommand cmd = new SqlCommand(cmdstr, sqlconn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@soilTypeID", SqlDbType.Int);
                cmd.Parameters["@soilTypeID"].Value = STint;
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {

                    clayRatio = Convert.ToDecimal(reader["Clay"]);
                    HumusRatio = Convert.ToDecimal(reader["Humus"]);
                }
                reader.Close();
            }
            catch(Exception e)
            {
                message.Instance.addWarnings("Problemer med databasen", "Cannot find clay in DB " + e.Message, 2);
            }
            //int STint = translateST(ST);
            Rotation rotation = new Rotation(standardRotationName, STint, clayRatio, 100m, HumusRatio, Referencesaedskifte, Saedskifte,  ArealType);

            rotation = addFieldPlanList(rotation, standardRotationName, rotationArea, rotationversion);
            rotation = addFieldPlanRotationList(rotation, standardRotationName, rotationArea, rotationversion, manureList);
            return rotation;
        }
        /*
         *  Creates a single Rotation and adds FieldPlanRotation and FieldPlan to that Rotation
         * 
         * 
         */
        private Rotation getAfterCropStandardRotation(string standardRotationName, int STint, decimal AfterCropPercent, int rotationversion, string Referencesaedskifte, string Saedskifte, int ArealType, List<Manure> manureList)
        {
            decimal clayRatio = -1;
            decimal HumusRatio = -1;
            try
            {
                string cmdstr = "MST_GetClayAndHumusRatioPrSoilType";
                SqlCommand cmd = new SqlCommand(cmdstr, sqlconn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@soilTypeID", SqlDbType.Int);
                cmd.Parameters["@soilTypeID"].Value = STint;
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {

                    clayRatio = Convert.ToDecimal(reader["Clay"]);
                    HumusRatio = Convert.ToDecimal(reader["Humus"]);
                }
                reader.Close();
            }
            catch (Exception e)
            {
                message.Instance.addWarnings("Problemer med databasen", "Cannot find clay in DB " + e.Message, 2);
            }
            //int STint = translateST(ST);
            Rotation rotation = new Rotation(standardRotationName, STint, clayRatio, 100m, HumusRatio, Referencesaedskifte, Saedskifte, ArealType);

            rotation = addAfterCropFieldPlanList(rotation, standardRotationName, AfterCropPercent, rotationversion);
            rotation = addAfterCropFieldPlanRotationList(rotation, standardRotationName, AfterCropPercent, rotationversion, manureList);
            if (rotation.FieldPlanRotationList.Count() == 0)
            {
                rotation = addFieldPlanRotationList(rotation, standardRotationName, 100m, rotationversion, manureList);
            }
            return rotation;
        }
        /*
         * Looking up in the DataBase and translate string ManureType into a corresponding int  
         * It will throw a "Cannot find Manure Type" exception if something goes wrong
         */
        /*
         * Looking up in the DataBase and translate string ST (SoilType) into a corresponding int  
         * It will throw a "Cannot find JB" exception if something goes wrong
         */
        private int translateST(string ST)
        {
            int STint = 0;
            try
            {
                string cmdstr = "MST_translateJB";
                SqlCommand cmd = new SqlCommand(cmdstr, sqlconn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@JBString", SqlDbType.VarChar, 20);
                cmd.Parameters[cmd.Parameters.Count - 1].Value = ST;
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    STint = Convert.ToInt32(reader["SoilTypeID"]);
                }
            }
            catch(Exception e) 
            {
                message.Instance.addWarnings("Problemer med databasen", "Cannot find JB " + e.Message.ToString(), 1);
            }
            return STint;

        }
        private decimal formatValue(decimal value,int digits)
        {
            if (globalSettings.Instance.getRoundedValuesError())
            {
                //return Math.Round(value, 1);
                return Math.Round(value, digits);
            }
            else
            {
                return value;
            }
        }

    }


}
