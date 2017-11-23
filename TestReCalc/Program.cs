using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FarmN_2010;
using System.IO;

namespace FarmN_2010
{
    class Program
    {
       
        static void Main(string[] args)
        {

            randomDeltaSoilN();
        }

        public static void randomDeltaSoilN()
        {
            decimal[] clayRange = new decimal[12] { 3.6m, 3.6m, 6.4m,7.2m,12.2m,12m,17.4m,20m,50m,20m,7.5m,7.5m};
            System.Random RandNum = new System.Random();
            DeltaSoilN instanec = new DeltaSoilN();
            int SoilCode;
            int FarmType;
            int PostalCode;
            decimal TotalCarbonFromCrops;
            decimal TotalCarbonFromManure;
            decimal FractionCatchCrops;
            decimal Clay;
            instanec.print();
            for(int i=0;i<10000;i++)
            {
                SoilCode=RandNum.Next(1,12);
                FarmType = RandNum.Next(1, 3);
                PostalCode= RandNum.Next(1000, 9900);
                TotalCarbonFromCrops=(decimal)RandNum.NextDouble() * (7);
                TotalCarbonFromManure=(decimal)RandNum.NextDouble() * (5);
                FractionCatchCrops=(decimal)RandNum.NextDouble() * (1);
                Clay = clayRange[SoilCode - 1];
                instanec.init(SoilCode, FarmType, PostalCode, TotalCarbonFromCrops, TotalCarbonFromManure, FractionCatchCrops, Clay);
            }
            instanec.close();
            

        }
        public static void systematicDeltaSoilN()
        {
            decimal[] clayRange = new decimal[12] { 3.6m, 3.6m, 6.4m, 7.2m, 12.2m, 12m, 17.4m, 20m, 50m, 20m, 7.5m, 7.5m };
            DeltaSoilN instanec = new DeltaSoilN();
            instanec.print();
            for (int i = 0; i < 12; i++)
            {
                instanec.init(i, 2, 5000, 3.5m, 2.5m, 0.5m, clayRange[i]);
            }
            instanec.print();
            for (int i = 1; i < 4; i++)
            {
                instanec.init(6, i, 5000, 3.5m, 2.5m, 0.5m, clayRange[5]);
            }
            instanec.print();
            for (int i = 1000; i < 9990;i=i+ 90)
            {
                instanec.init(6, 1, i, 3.5m, 2.5m, 0.5m, clayRange[5]);
            }
            instanec.print();
            for (int i = 0; i < 101; i = i + 1)
            {
                instanec.init(6, 1, 5000, i*0.07m, 2.5m, 0.5m, clayRange[5]);
            }
            instanec.print();
            for (int i = 0; i < 101; i = i + 1)
            {
                instanec.init(6, 1, 5000, 3.5m, i*0.05m, 0.5m, clayRange[5]);
            }
            instanec.print();
            for (int i = 0; i < 101; i = i + 1)
            {
                instanec.init(6, 1, 5000, 3.5m,2.5m, i * 0.01m,clayRange[5]);
            }
            instanec.close();
        }
        public static void randomTestSIMDEN()
        {
            System.Random RandNum = new System.Random();
            SIMDEN instance = new SIMDEN();
            instance.print();
            int SoilCode;
            int FarmType;
            decimal FertiliserN;
            decimal ManureNincorp;
            decimal ManureNspread;
            decimal NFixation;

 

            for (int i = 0; i < 10000; i++)
            {
                SoilCode= RandNum.Next(1,12);
                FarmType = RandNum.Next(1, 3);
                FertiliserN = (decimal)RandNum.NextDouble() * (250);
                ManureNincorp = (decimal)RandNum.NextDouble() * (350);
                ManureNspread = (decimal)RandNum.NextDouble() * (350);
                NFixation = (decimal)RandNum.NextDouble() * (200);
                instance.init(SoilCode, FarmType, FertiliserN, ManureNincorp, ManureNspread, NFixation);
            }
            instance.close();
        }
        public static void systematicTestSIMDEN()
        {
            SIMDEN instance = new SIMDEN();
            instance.print();
            for (int i = 1; i <= 12; i++)
            {
                instance.init(i, 2, 125m, 175m, 175m, 100m);
            }
            instance.print();
            for (int i = 1; i <= 3; i++)
            {
                instance.init(6, i, 125m, 175m, 175m, 100m);
            }
            instance.print();
            decimal FertiliserN = 0;
            for (int i = 0; i <=100 ; i=i+1)
            {
                instance.init(6, 2, FertiliserN, 175m, 175m, 100m);
                FertiliserN += 2.5m;
            }
            instance.print();
            
            for (int i = 0; i <= 300; i = i + 3)
            {
                instance.init(6, 2, 125m, i, 175m, 100m);
            }
            instance.print();
            for (int i = 0; i <= 300; i = i + 3)
            {
                instance.init(6, 2, 125m, 175m,i, 100m);
            }
            instance.print();
            for (int i = 0; i <= 100; i += 1 )
            {
                instance.init(6, 2, 125m, 175m, 175m, i);
            }
            instance.close();
        }
        public static void randomTestNless()
        {
            System.Random RandNum = new System.Random();
            N_LES instance = new N_LES();
            instance.print();
            decimal N_Niveau;
            decimal N_Spring; 
            decimal N_Fall;
            decimal N_Fix;
            decimal N_GrazingManure;
            decimal N_Removed;
            int Year;
            int SoilType ;
            decimal Humus;
            decimal Clay ;
            decimal Run_Off1;
            decimal Run_Off2;
            decimal[] CrpCoeffValues = { (decimal)(-165.7),(decimal)(-98.6),-42,(decimal)(-7.6),0,(decimal)28.8 };;
            decimal[] PreCropCoeffValues = { (decimal)(37.7), (decimal)(14.2),0, (decimal)(-38.5)};
            decimal CropCoeff;
            decimal PreCropCoeff ;
            for (int i = 0; i < 10000; i++)
            {
                N_Niveau = (decimal) RandNum.NextDouble() * (2);
                N_Spring =(decimal) RandNum.NextDouble() * (300);
                N_Fall =(decimal) RandNum.NextDouble() * ( 150);
                N_Fix = (decimal)RandNum.NextDouble() * (300);
                N_GrazingManure = (decimal)RandNum.NextDouble() * (200);
                N_Removed = (decimal)RandNum.NextDouble() * (250);
                Year = RandNum.Next(1963, 2063);
                SoilType =RandNum.Next(1,12);
                Humus = (decimal)(RandNum.NextDouble() * (0.5));
                Clay = (decimal)(RandNum.NextDouble() * (0.7));
                Run_Off1 = (decimal)(50+RandNum.NextDouble() * (650));
                Run_Off2 = (decimal)(50 + RandNum.NextDouble() * (650));


                CropCoeff = CrpCoeffValues[RandNum.Next(0, 5)];
               
                PreCropCoeff = PreCropCoeffValues[RandNum.Next(0, 3)];
                instance.init(N_Niveau, N_Spring, N_Fall, N_Fix, N_GrazingManure, N_Removed, Year, SoilType, Humus, Clay, Run_Off1, Run_Off2, CropCoeff, PreCropCoeff);
            }
            instance.close();

        }
        public static void exelTestNLess()
        {
            N_LES instance = new N_LES();

            instance.print();

            instance.init(183, (decimal)166, (decimal)0, (decimal)2, (decimal)0, 120, 2007, 3, (decimal)7.1, (decimal)6.9, (decimal)45.417, (decimal)45.417, 0, 0);

            instance.print();

            instance.init(183, (decimal)166, (decimal)0, (decimal)2, (decimal)0, 120, 2007, 2, 4m, 4.9m, (decimal)51.7003, (decimal)51.7003, (decimal)(-7.6), 0);
            instance.print();

            instance.init(183, (decimal)166, (decimal)0, (decimal)2, (decimal)0, 120, 2007, 3, 7.1m, 6.9m, (decimal)44.2059, (decimal)44.2059, (decimal)(-7.6), 0);
            instance.print();

            instance.init(183, (decimal)166, (decimal)0, (decimal)2, (decimal)0, 120, 2007, 4, 9.7m, 7.6m, (decimal)42.0424, (decimal)42.0424, (decimal)(-7.6), 0);
            instance.print();

            instance.init(183, (decimal)166, (decimal)0, (decimal)2, (decimal)0, 120, 2007, 5, 12.2m, 12.1m, (decimal)36.2735, (decimal)36.2735, (decimal)(-7.6), 0);
            instance.print();

            instance.init(183, (decimal)166, (decimal)0, (decimal)2, (decimal)0, 120, 2007, 6, 13.3m, 12.3m, (decimal)35.8916, (decimal)35.8916, (decimal)(-7.6), 0);
            instance.print();

            instance.init(183, (decimal)166, (decimal)0, (decimal)2, (decimal)0, 120, 2007, 7, 14.4m, 17.8m, (decimal)39.777, (decimal)39.7777, (decimal)(-7.6), 0);
            instance.print();

            instance.init(183, (decimal)166, (decimal)0, (decimal)2, (decimal)0, 120, 2007, 1, 3.4m, 3.6m, (decimal)38.7943, (decimal)38.7943, (decimal)(-7.6), 0);
            instance.close();
           
        }
        public static void systematicTestNLess()
        {
            N_LES instance = new N_LES();

            instance.print();
            decimal N_Niveau = (decimal)0;
            for (int i = 0; i < 101; i++)
            {
                instance.init(N_Niveau, (decimal)150, (decimal)75, (decimal)150, (decimal)100, 125, 2013, 6, (decimal)0.25, (decimal)0.35, 375, 375, 1, 1);
                N_Niveau += (decimal)0.02;
            }
            instance.print();
            decimal spring = (decimal)0;
            for (int i = 0; i < 101; i++)
            {
                instance.init(1, (decimal)spring, (decimal)75, (decimal)150, (decimal)100, 125, 2013, 6, (decimal)0.25, (decimal)0.35, 375, 375, 1, 1);
                spring += (decimal)3;
            }
            instance.print();
            decimal fall = (decimal)0;
            for (int i = 0; i < 101; i++)
            {
                instance.init(1, (decimal)150, (decimal)fall, (decimal)150, (decimal)100, 125, 2013, 6, (decimal)0.25, (decimal)0.35, 375, 375, 1, 1);
                fall += (decimal)0.75;
            }
            instance.print();
            decimal fix = (decimal)0;
            for (int i = 0; i < 101; i++)
            {
                instance.init(1, (decimal)150, (decimal)75, (decimal)fix, (decimal)100, 125, 2013, 6, (decimal)0.25, (decimal)0.35, 375, 375, 1, 1);
                fix += (decimal)3;
            }
            instance.print();
            decimal graceing = (decimal)0;
            for (int i = 0; i < 101; i++)
            {
                instance.init(1, (decimal)150, (decimal)75, (decimal)150, (decimal)graceing, 125, 2013, 6, (decimal)0.25, (decimal)0.35, 375, 375, 1, 1);
                graceing += (decimal)1;
            }
            instance.print();
            decimal nremove = (decimal)0;
            for (int i = 0; i < 101; i++)
            {
                instance.init(1, (decimal)150, (decimal)75, (decimal)150, (decimal)100, nremove, 2013, 6, (decimal)0.25, (decimal)0.35, 375, 375, 1, 1);
                nremove += (decimal)2.5;
            }
            instance.print();
            int year = 1963;
            for (int i = 0; i < 101; i++)
            {
                instance.init(1, (decimal)150, (decimal)75, (decimal)150, (decimal)100, 125, year, 6, (decimal)0.25, (decimal)0.35, 375, 375, 1, 1);
                year += 1;
            }
            instance.print();
            decimal humus = (decimal)0;
            for (int i = 0; i < 101; i++)
            {
                instance.init(1, (decimal)150, (decimal)75, (decimal)150, (decimal)100, 125, 2013, 6, humus, (decimal)0.35, 375, 375, 1, 1);
                humus += (decimal)0.005;
            }
            instance.print();
            decimal clay = (decimal)0;
            for (int i = 0; i < 101; i++)
            {
                instance.init(1, (decimal)150, (decimal)75, (decimal)150, (decimal)100, 125, 2013, 6, (decimal)0.25, (decimal)clay, 375, 375, 1, 1);
                clay += (decimal)0.007;
            }
            instance.print();
            decimal RunOff = (decimal)50;
            for (int i = 0; i < 101; i++)
            {
                instance.init(1, (decimal)150, (decimal)75, (decimal)150, (decimal)100, 125, 2013, 6, (decimal)0.25, (decimal)0.35, RunOff, RunOff, 1, 1);
                RunOff += (decimal)6.5;
            }
            instance.print();
            int SoilType = 1;
            for (int i = 0; i < 12; i++)
            {
                instance.init(1, (decimal)150, (decimal)75, (decimal)150, (decimal)100, 125, 2013, SoilType, (decimal)0.25, (decimal)0.35, 375, 375, 1, 1);
                SoilType += 1;
            }
            instance.print();
            //Y1
            decimal CropOFF = (decimal)(-165.7);
            instance.init(1, (decimal)150, (decimal)75, (decimal)150, (decimal)100, 125, 2013, 6, (decimal)0.25, (decimal)0.35, 375, 375, CropOFF, 1);
            instance.print();
            //Y2
            CropOFF = (decimal)(-98.6);
            instance.init(1, (decimal)150, (decimal)75, (decimal)150, (decimal)100, 125, 2013, 6, (decimal)0.25, (decimal)0.35, 375, 375, CropOFF, 1);
            instance.print();
            //Y3
            CropOFF = (decimal)(-42);
            instance.init(1, (decimal)150, (decimal)75, (decimal)150, (decimal)100, 125, 2013, 6, (decimal)0.25, (decimal)0.35, 375, 375, CropOFF, 1);
            instance.print();
            //Y4
            CropOFF = (decimal)(-7.6);
            instance.init(1, (decimal)150, (decimal)75, (decimal)150, (decimal)100, 125, 2013, 6, (decimal)0.25, (decimal)0.35, 375, 375, CropOFF, 1);
            //Y5
            instance.print();
            CropOFF = (decimal)(0);
            instance.init(1, (decimal)150, (decimal)75, (decimal)150, (decimal)100, 125, 2013, 6, (decimal)0.25, (decimal)0.35, 375, 375, CropOFF, 1);
            //Y6
            instance.print();
            CropOFF = (decimal)(28.8);
            instance.init(1, (decimal)150, (decimal)75, (decimal)150, (decimal)100, 125, 2013, 6, (decimal)0.25, (decimal)0.35, 375, 375, CropOFF, 1);

            instance.print();
            decimal PreCropCoeff = (decimal)(34.7);
            instance.init(1, (decimal)150, (decimal)75, (decimal)150, (decimal)100, 125, 2013, 6, (decimal)0.25, (decimal)0.35, 375, 375, 1, PreCropCoeff);

            instance.print();
            PreCropCoeff = (decimal)(14.2);
            instance.init(1, (decimal)150, (decimal)75, (decimal)150, (decimal)100, 125, 2013, 6, (decimal)0.25, (decimal)0.35, 375, 375, 1, PreCropCoeff);

            instance.print();
            PreCropCoeff = (decimal)(0);
            instance.init(1, (decimal)150, (decimal)75, (decimal)150, (decimal)100, 125, 2013, 6, (decimal)0.25, (decimal)0.35, 375, 375, 1, PreCropCoeff);

            instance.print();
            PreCropCoeff = (decimal)(0);
            instance.init(1, (decimal)150, (decimal)75, (decimal)150, (decimal)100, 125, 2013, 6, (decimal)0.25, (decimal)0.35, 375, 375, 1, PreCropCoeff);
        }
    }
}
