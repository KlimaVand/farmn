using System;
using System.IO;


namespace FarmN_2010
{
    /// <summary>
    /// calculate Nles
    /// </summary>
    public class N_LES
    {
        private decimal N_Les;
        private decimal CurrentYearRunOff;
        private decimal LesMgPrL;
        private bool ready;
        private decimal[] CropCoeffRange = new decimal[7] { -202.4m,-165.7m,-98.6m ,-42m,-7.6m,0 ,28.8m };
        private decimal[] PreCropCoeffRange = new decimal[4] { 34.7m,14.2m,0,-38.5m};
        private bool doubleEquals(decimal left, decimal right, decimal epsilon)
        {
            return (Math.Abs(left - right) < epsilon);
        }

        /// <summary>
        /// An empty constructor
        /// </summary>
        public N_LES()
         {
             ready = false;
             if (globalSettings.Instance.getExtraOutput() == true)
             {
                 

                globalFilehandling.Instance.writeToNless("N_Niveau" + '\t' + "N_Spring" + '\t' + "N_Fall" + '\t' + "N_Fix" + '\t' + "N_GrazingManure" + '\t' + "SoilType" + '\t' + "N_Removed" + '\t' + "Year" + '\t' + "Humus" + '\t' + "Clay" + '\t' + "Run_OffBefore" + '\t' + "Run_offNow" + '\t' + "CropCoeff" + '\t' + "PreCropCoeff" + '\t' + "T" + '\t' + "U" + '\t' + "V" + '\t' + "KG pr HA" + '\t' + "Mg pr L");
               
                 
             }
         }

        static int tmp = 0;
        /// <summary>
        ///Initializing values for NLes
        /// </summary>
        /// <param name="N_Niveau">n nuveau</param>
        /// <param name="N_Spring">nspring</param>
        /// <param name="N_Fall">Nfall</param>
        /// <param name="N_Fix">fixation</param>
        /// <param name="N_GrazingManure">manure from grazing</param>
        /// <param name="N_Removed">n that is removed</param>
        /// <param name="Year">year</param>
        /// <param name="SoilType">soil type</param>
        /// <param name="Humus"> humus in soil</param>
        /// <param name="Clay">Clay in soil</param>
        /// <param name="Run_Off1">Run off</param>
        /// <param name="Run_Off2">Run off</param>
        /// <param name="CropCoeff">crop coefficient for this year</param>
        /// <param name="PreCropCoeff">Last years crop coefficient</param>
        /// <returns>negativ if something went wrong or 0 if it is OK</returns>
        public int init(decimal N_Niveau, decimal N_Spring, decimal N_Fall, decimal N_Fix, decimal N_GrazingManure, decimal N_Removed, int Year, int SoilType, decimal Humus, decimal Clay, decimal Run_Off1, decimal Run_Off2, decimal CropCoeff, decimal PreCropCoeff)
        {
            tmp++;

//            globalTime.Instance.start(1,"Nless");
            CurrentYearRunOff = Run_Off1;
            ready = true;
            if (N_Niveau < 0)
            {
                message.Instance.addWarnings("N niveau er forkert. Den er " + N_Niveau.ToString()+" men burde ligge under 0", "N_LES: N_Niveau is not valid", 2);
                return -1;
            }
            if (N_Spring < 0)
            {
                message.Instance.addWarnings("N foraar er forkert. Den er " + N_Spring.ToString() + " men burde ligge under 0", "N_LES: N_Spring is not valid", 2);
                return -2;
            }
            if (N_Fall < 0)
            {
                message.Instance.addWarnings("N efteraar er forkert. Den er " + N_Fall.ToString() + " men burde ligge under 0", "N_LES: N_Fall is not valid", 2);
                return -3;
            }
            if (N_Fix < 0)
            {
                message.Instance.addWarnings("N fix er forkert. Den er " + N_Fix.ToString() + " men burde ligge under 0", "N_LES: N_Fix is not valid", 2);
                return -4;
            }
            if (N_GrazingManure < 0)
            {
                message.Instance.addWarnings("N afgraesning er forkert. Den er " + N_GrazingManure.ToString() + " men burde ligge over 0", "N_LES: N_GrazingManure is not valid", 2);
                return -5;
            }
            if (N_Removed < 0)
            {
                message.Instance.addWarnings("Fjernelse af N er forkert. Den er " + N_Removed.ToString() + " men burde ligge under 0", "N_LES: N_Removed is not valid", 2);
                return -6;
            }
            if (Year < 1963)
            {
                message.Instance.addWarnings("Aaret er forkert. Den er " + Year.ToString() + " men burde ligge under 0", "N_LES: Year is not valid", 2);
                return -7;
            }
            if (SoilType < 1 || SoilType > 12)
            {
                message.Instance.addWarnings("Jordtypen er forkert. Den er " + SoilType.ToString() + " men burde ligge mellem 1 og 12", "N_LES: SoilType is not valid", 2);
                return -8;
            }
            if (Humus < 0)
            {
                message.Instance.addWarnings("Humus er forkert. Den er " + Humus.ToString() + " men burde ligge under 0", "N_LES: Humus is not valid", 2);
                return -9;
            }
            if (Clay < 0)
            {
                message.Instance.addWarnings("Ler er forkert. Den er " + Clay.ToString() + " men burde ligge under 0", "N_LES: Clay is not valid", 2);
                return -10;
            }
            if (Run_Off1 <= 0 || Run_Off1 > 1200)
            {
                message.Instance.addWarnings("Run Off er forkert. Den er " + Run_Off1.ToString() + " men burde ligge mellem 0 og 1200", "N_LES: Run_Off is not valid", 2);
                return -11;
            }
            if (Run_Off2 <= 0 || Run_Off2 > 1200)
            {
                message.Instance.addWarnings("Run Off er forkert. Den er " + Run_Off2.ToString() + " men burde ligge mellem 0 og 1200", "N_LES: Run_Off is not valid", 2);
                return -12;
            }
            bool found = false;
            for (int i = 0; i < CropCoeffRange.Length; i++)
            {
               
                if (doubleEquals(CropCoeffRange[i], CropCoeff, 0.000000001m))
                {
                    found = true;
                    break;
                }
            }
            if (found==false)
            {
                if (globalSettings.Instance.getMissingPreCropCoeffInDatabaseError() == true)
                    message.Instance.addWarnings("CropCoeff er uden for range. Den er " + CropCoeff, "N_LES: CropCoeff is not valid", 4);
                else
                message.Instance.addWarnings("CropCoeff er uden for range. Den er " + CropCoeff, "N_LES: CropCoeff is not valid", 2);
            }
            found = false;
            for (int i = 0; i < PreCropCoeffRange.Length; i++)
            {
                if (doubleEquals(PreCropCoeffRange[i], PreCropCoeff,0.000000001m))
                {
                    found = true;
                    break;
                }
            }
            if (found == false)
            {
                if (globalSettings.Instance.getMissingPreCropCoeffInDatabaseError() == true)
                    message.Instance.addWarnings("PreCrop coeff er uden for range. Den er " + PreCropCoeff, "N_LES: PreCropCoeff is not valid", 4);
                else
                    message.Instance.addWarnings("PreCrop coeff er uden for range. Den er " + PreCropCoeff, "N_LES: PreCropCoeff is not valid", 2);
            }

            int returnValue = calculate(N_Niveau, N_Spring, N_Fall, N_Fix, N_GrazingManure, N_Removed, Year, SoilType, Humus, Clay, Run_Off1, Run_Off2, CropCoeff, PreCropCoeff);

            // globalTime.Instance.stop(1);
            return returnValue;
            
        }
        /// <summary>
        /// Calculate NLes
        /// </summary>
        private int calculate(decimal N_Niveau, decimal N_Spring, decimal N_Fall, decimal N_Fix, decimal N_GrazingManure, decimal N_Removed, int Year, int SoilType, decimal Humus, decimal Clay, decimal Run_Off1, decimal Run_Off2, decimal CropCoeff, decimal PreCropCoeff)
	   {

		decimal CoeffNFall; 
            if (SoilType <= 4)
                CoeffNFall = (decimal)1.0749;
            else
                CoeffNFall = (decimal)0.3539;
        
            decimal T = (decimal)0.3255 * N_Niveau + (decimal)0.2528 * (N_Spring + N_Fix) +(decimal)0.3760 * N_GrazingManure + CoeffNFall * N_Fall - (decimal)0.1936 * N_Removed + CropCoeff + PreCropCoeff;

            decimal V;
            if (T > (decimal) 0.001)
                V = T;
            else
                V = (decimal) 0.001;
            decimal U;
            if (globalSettings.Instance.getMissingPreCropCoeffInDatabaseError() == true)
            {
                if (PreCropCoeff < -900 || CropCoeff < -900)
                {
                    T = (decimal)0;
                }
            }
            if (T<0)
                    U = (decimal)59.6 + 2455 / (Year - (decimal)1962.2) + T * (decimal)0.5466;
            else
                    U = (decimal)59.6 + 2455 / (Year - (decimal)1962.2) + 0 * (decimal)0.5466;
            if (U < 0)
                U = 0;
            N_Les = (U + (decimal)Math.Pow((double)V, (double)1.2)) * (1 - (decimal)Math.Exp((double)-0.001502 * (double)Run_Off1)) * (decimal)Math.Exp((double)-0.000554 * (double)Run_Off2) * (decimal)Math.Exp((double)-0.1064 * (double)Humus) * (decimal)Math.Exp((double)-0.0325 * (double)Clay) * (decimal)1.2453;
            if (N_Les <= 0)
            {
                message.Instance.addWarnings("Algoritmefejl i NLes","N_LES: N_Les is not valid", 2);
                return -1;
            }
            	
           
            LesMgPrL = 100 * N_Les * (decimal)4.43 / CurrentYearRunOff;
            if (globalSettings.Instance.getExtraOutput() == true)
            {

                globalFilehandling.Instance.writeToNless(N_Niveau.ToString() + '\t' + N_Spring.ToString() + '\t' + N_Fall.ToString() + '\t' + N_Fix.ToString() + '\t' + N_GrazingManure.ToString() + '\t' + SoilType.ToString() + '\t' + N_Removed.ToString() + '\t' + Year.ToString() + '\t' + Humus.ToString() + '\t' + Clay.ToString() + '\t' + Run_Off1.ToString() + '\t' + Run_Off2.ToString() + '\t' + CropCoeff + '\t' + PreCropCoeff.ToString() + '\t' + T.ToString() + '\t' + U.ToString() + '\t' + V.ToString() + '\t' + N_Les.ToString() + '\t' + LesMgPrL.ToString());

            }
		return 0;
	  }
 
        /// <summary>
        /// 
        /// </summary>
        /// <returns>Return: Nless kg pr HA</returns>
      public decimal getNLesKgNPrHa()
      {
          if(ready==false)
          {
              message.Instance.addWarnings("Inititaliseringsfejl i NLes","N_LES:Has not been init before receiving output", 2);
              return -1.0m;
          }
          return N_Les;
          
      }
      /// <summary>
      /// 
      /// </summary>
      /// <returns>N leach in mg pr L</returns>
      public decimal getNLesMgPrL()
      {
          if (ready == false)
          {
              message.Instance.addWarnings("Initialiseringsfejl i NLes","N_LES:Has not been init before receiving output", 2);
              return -1.0m;
          }
          return LesMgPrL;
      }
    
    }

}
