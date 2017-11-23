using System;
using System.IO;

namespace FarmN_2010
{
    public class N_LES
    {
        private decimal N_Les;
        private decimal CurrentYearRunOff;
        private decimal LesMgPrL;
        private bool ready;
        StreamWriter tw;
        public N_LES()
         {
             ready = false;
             FileInfo imgInfo = new FileInfo("c:\\dateNles.xls");
             try
             {
                 imgInfo.Delete();
             }
             catch (Exception e)
             {
             }
             tw = File.AppendText("c:\\dateNles.xls");
         }
        public int init(decimal N_Niveau, decimal N_Spring, decimal N_Fall, decimal N_Fix, decimal N_GrazingManure, decimal N_Removed, int Year, int SoilType, decimal Humus, decimal Clay, decimal Run_Off1, decimal Run_Off2, decimal CropCoeff, decimal PreCropCoeff)
        {
            ready = true;
            CurrentYearRunOff = Run_Off1;
            if (N_Niveau < 0)
            {
                message.Instance.addWarnings("N_LES: N_Niveau is not valid",2); 
                return -1;
            }
            if (N_Spring < 0)
            {
                message.Instance.addWarnings("N_LES: N_Spring is not valid",2); 
                return -1;
            }
            if (N_Fall < 0)
            {
                message.Instance.addWarnings("N_LES: N_Fall is not valid",2); 
                return -1;
            }
            if (N_Fix < 0)
            {
                message.Instance.addWarnings("N_LES: N_Fix is not valid", 2);
                return -1;
            }
            if (N_GrazingManure < 0)
            {
                message.Instance.addWarnings("N_LES: N_GrazingManure is not valid",2);
                return -1;
            }
            if (N_Removed < 0)
            {
                message.Instance.addWarnings("N_LES: N_Removed is not valid",2); 
                return -1;
            }
            if (Year < 1963)
            {
                message.Instance.addWarnings("N_LES: Year is not valid",2); 
                return -1;
            }
            if (SoilType < 0)
            {
                message.Instance.addWarnings("N_LES: SoilType is not valid",2); 
                return -1;
            }
            if (Humus < 0)
            {
                message.Instance.addWarnings("N_LES: Humus is not valid",2); 
                return -1;
            }
            if (Clay < 0)
            {
                message.Instance.addWarnings("N_LES: Clay is not valid",2); 
                return -1;
            }
            if (Run_Off1 <= 0)
            {
                message.Instance.addWarnings("N_LES: Run_Off is not valid",2);
                return -1;
            }
            if (Run_Off2 <= 0)
            {
                message.Instance.addWarnings("N_LES: Run_Off is not valid",2); 
                return -1;
            }

            return calculate(N_Niveau, N_Spring, N_Fall, N_Fix,  N_GrazingManure, N_Removed, Year,  SoilType,  Humus,  Clay,  Run_Off1,  Run_Off2,  CropCoeff,  PreCropCoeff);
        }

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
            if (T<0)
                    U = (decimal)59.6 + 2455 / (Year - (decimal)1962.2) + T * (decimal)0.5466;
            else
                    U = (decimal)59.6 + 2455 / (Year - (decimal)1962.2) + 0 * (decimal)0.5466;
            if (U < 0)
                U = 0;
            N_Les = (U + (decimal)Math.Pow((double)V, (double)1.2)) * (1 - (decimal)Math.Exp((double)-0.001502 * (double)Run_Off1)) * (decimal)Math.Exp((double)-0.000554 * (double)Run_Off2) * (decimal)Math.Exp((double)-0.1064 * (double)Humus) * (decimal)Math.Exp((double)-0.0325 * (double)Clay) * (decimal)1.2453;
            if (N_Les < 0)
            {
                message.Instance.addWarnings("N_LES: N_Les is not valid", 2);
                return -1;
            }
            	
           
            LesMgPrL = 100 * N_Les * (decimal)4.43 / CurrentYearRunOff;

           
            // write a line of text to the file

            tw.WriteLine(N_Niveau.ToString() + '\t' + N_Spring.ToString() + '\t' + N_Fall.ToString() + '\t' + N_Fix.ToString() + '\t' + N_GrazingManure.ToString() + '\t' + SoilType.ToString() + '\t' + N_Removed.ToString() + '\t' + Year.ToString() + '\t' + Humus.ToString() + '\t' + Clay.ToString() + '\t' + Run_Off1.ToString() + '\t' + Run_Off2.ToString() + '\t' + CropCoeff + '\t' + PreCropCoeff.ToString() + '\t' + T.ToString() + '\t' + U.ToString() + '\t' + V.ToString() + '\t' + N_Les.ToString() + '\t' + LesMgPrL.ToString());
            // close the stream
            

		return 0;
	  }
        public void print()
        {
            tw.WriteLine("N_Niveau" + '\t' + "N_Spring" + '\t' + "N_Fall" + '\t' + "N_Fix" + '\t' + "N_GrazingManure" + '\t' + "SoilType" + '\t' + "N_Removed" + '\t' + "Year" + '\t' + "Humus" + '\t' + "Clay" + '\t' + "Run_OffBefore" + '\t' + "Run_offNow" + '\t' + "CropCoeff" + '\t' + "PreCropCoeff" + '\t' + "T" + '\t' + "U" + '\t' + "V" + '\t' + "KG pr HA" + '\t' + "Mg pr L");
        }
        public void close()
        {
            tw.Close();
        }
      public decimal getNLesKgNPrHa()
      {
          if (ready == false)
          {
              message.Instance.addWarnings("N_LES:Has not been init before receiving output", 1);
              return -1.0m;
          }
          return N_Les;
          
      }
      public decimal getNLesMgPrL()
      {
          if (ready == false)
          {
              message.Instance.addWarnings("N_LES:Has not been init before receiving output", 1);
              return -1.0m;
          }
          return LesMgPrL;
      }
    }

}
