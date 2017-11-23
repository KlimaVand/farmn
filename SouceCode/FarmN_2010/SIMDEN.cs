using System;

using System.IO;
namespace FarmN_2010
{
    /// <summary>
    /// This class calculates a float giving the total estimated denitrification (kg N) using Finn P. Vinthers method (version 2.0)
    /// </summary>
    public class SIMDEN
    {
        private decimal DenitrificationPrRotation;
        private bool ready=false;

  
        /// <summary>
        /// An empty contructor
        /// </summary>
        public SIMDEN()
        {

            ready = false;

            if (globalSettings.Instance.getExtraOutput() == true)
            {

                globalFilehandling.Instance.writeToSimden("SoilCode" + '\t' + "FarmType" + '\t' + "FertiliserN" + '\t' + "ManureNincorp" + '\t' + "ManureNspread" + '\t' + "NFixation" + '\t' + "fert" + '\t' + "fltMan" + '\t' + "fltFix" + '\t' + "DenitrificationPrRotation");

            }
        }
        /// <summary>
        /// Initializing values for SIMDEN
        /// </summary>
        /// <param name="SoilCode">The soil type</param>
        /// <param name="FarmType">the farm type</param>
        /// <param name="FertiliserN">the amount of N in the fertilizer</param>
        /// <param name="ManureNincorp"> the amount of n in the crop</param>
        /// <param name="ManureNspread">the amount of N spread from the manure</param>
        /// <param name="NFixation">Fixation</param>
        /// <returns>0 if everything is fine. negativ if something goes wrong</returns>
        public int init(int SoilCode, int FarmType, decimal FertiliserN, decimal ManureNincorp, decimal ManureNspread, decimal NFixation)
        {
          //  globalTime.Instance.start(2, "Simden");
            ready = true;
            if (1 > SoilCode || SoilCode > 12)
            {
                message.Instance.addWarnings("Ugyldig jordtype","SIMDEN: SoilType is not valid", 2);
                return -1;
            }
            if (FarmType != 1 && FarmType != 2 && FarmType != 3)
            {

                message.Instance.addWarnings("Ugyldig bedriftstype","SIMDEN: FarmType is not valid", 2);
                return -2;

            }
            if(FertiliserN < 0)
            {
                message.Instance.addWarnings("Goedningsmaengden er under 0","SIMDEN: FertiliserN is not valid",2); 
                return -3;
            }
            if (ManureNincorp < 0)
            {
                message.Instance.addWarnings("Nedploejet husdyrgoedning er under 0","SIMDEN: ManureNincorp is not valid",2); 
                return -4;
            }
            if (ManureNspread < 0)
            {
                message.Instance.addWarnings("Spredt husdyrgoedning er under 0","SIMDEN: ManureNspread is not valid",2); 
                return -5;
            }
            if (NFixation < 0)
            {
                message.Instance.addWarnings("N fiksering er under 0","SIMDEN: NFixation is not valid",2); 
                return -6;
            }


            int returnValue =calculate(SoilCode, FarmType, FertiliserN, ManureNincorp, ManureNspread, NFixation);
            //  globalTime.Instance.stop(2);
            return returnValue;

        }
        /// <summary>
        /// Calculate SIMDEN
        /// </summary>
        private int calculate(int SoilCode, int FarmType, decimal FertiliserN, decimal ManureNincorp, decimal ManureNspread, decimal NFixation)
        {
            decimal fltBackgr = 0;
            decimal fltRatio = 0;
            if (FarmType == 1 || FarmType == 2)
            {

                switch (SoilCode)
                {

                    case 1:
                        fltBackgr = 0.0m;
                        fltRatio = 0.0m;
                        break;
                    case 2:
                        fltBackgr = 0.5m;
                        fltRatio = 0.5m;
                        break;
                    case 3:
                        fltBackgr = 1.4m;
                        fltRatio = 1.5m;
                        break;
                    case 4:
                        fltBackgr = 2.8m;
                        fltRatio = 2.5m;
                        break;
                    case 5:
                        fltBackgr = 4.8m;
                        fltRatio = 3.0m;
                        break;
                    case 6:
                        fltBackgr = 7.3m;
                        fltRatio = 4.0m;
                        break;
                    case 7:
                        fltBackgr = 10.2m;
                        fltRatio = 5.0m;
                        break;
                    default:
                        fltBackgr = 14.0m;
                        fltRatio = 6.0m;
                        break;

                }
            }
            if (FarmType == 3)
            {
                switch (SoilCode)
                {

                    case 1:
                        fltBackgr = 0.8m;
                        fltRatio = 0.5m;
                        break;
                    case 2:
                        fltBackgr = 1.8m;
                        fltRatio = 1.5m;
                        break;
                    case 3:
                        fltBackgr = 3.3m;
                        fltRatio = 2.5m;
                        break;
                    case 4:
                        fltBackgr = 6.6m;
                        fltRatio = 4.5m;
                        break;
                    case 5:
                        fltBackgr = 10.8m;
                        fltRatio = 5.0m;
                        break;
                    case 6:
                        fltBackgr = 14.4m;
                        fltRatio = 6.0m;
                        break;
                    case 7:
                        fltBackgr = 18.4m;
                        fltRatio = 7.0m;
                        break;
                    default:
                        fltBackgr = 27m;
                        fltRatio = 8.0m;
                        break;

                }

            }

            decimal fert;
            fert = FertiliserN * 0.008m * (fltRatio + 1.0m);
            decimal fltMan;
            fltMan = 0.7m * ManureNincorp * 0.025m * (fltRatio + 1.5m) + 0.5m * ManureNspread * 0.025m * (fltRatio + 1.5m);
            decimal fltFix;
            fltFix = NFixation * 0.4m * 0.025m * (fltRatio + 1.5m);
            DenitrificationPrRotation = fltBackgr + fert + fltMan + fltFix;
            if (DenitrificationPrRotation < 0)
            {
                message.Instance.addWarnings("Algoritmefejl i SIMDEN","SIMDEN: DenitrificationPrRotation is not valid", 2);
                return -1;
            }
            if (globalSettings.Instance.getExtraOutput() == true)
            {
                

                globalFilehandling.Instance.writeToSimden(SoilCode.ToString() + '\t' + FarmType.ToString() + '\t' + FertiliserN.ToString() + '\t' + ManureNincorp.ToString() + '\t' + ManureNspread.ToString() + '\t' + NFixation.ToString() + '\t' + fert.ToString() + '\t' + fltMan.ToString() + '\t' + fltFix.ToString() + '\t' + DenitrificationPrRotation.ToString() + '\t');

             }
            return 0;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns>Denitrification</returns>
        public decimal getDenitrification()
        {
            if (ready == false)
            {
                message.Instance.addWarnings("Initialiseringsfejl i SIMDEN","SIMDEN:Has not been init before receiving output", 2);
                return -1.0m;
            }
            return DenitrificationPrRotation;
        }
    }
}