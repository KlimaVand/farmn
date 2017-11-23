using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using FarmN_2010;
using System.Threading;
namespace testOfEverything
{
    class TestWebservice
    {

        static int numbersOfScenarios = 10;
        public TestWebservice()
        {
            //FileTest();

        }

        //public void randomTest()
        //{
        //    TextWriter ArealListe = new StreamWriter("C:\\ArealListe.txt");
        //    TextWriter CodeAndOptions = new StreamWriter("C:\\CodeAndOptions.txt");
        //    TextWriter GoednListe = new StreamWriter("C:\\GoednListe.txt");
        //    ArealListe.WriteLine("FarmNumber" + '\t' + "ArealerIArealListe" + '\t' + "arealType" + '\t' + "Referencesaedskifte" + '\t' + "Saedskifte" + '\t' + "	Jordbundstype	" + '\t' + "Ha");
        //    CodeAndOptions.WriteLine("FarmNumber" + '\t' + "	zipCode" + '\t' + "	option1" + '\t' + "	option2");
        //    GoednListe.WriteLine("FarmNumber" + '\t' + "AntalIGoedningsmaengdelListe" + '\t' + "	KgN" + '\t' + "GoedningstypeNUdnyttelsesProcent" + '\t' + "Goedningstype");
        //    for (int times = 0; times < numbersOfScenarios; times++)
        //    {


        //        NyFarmNWebService2.FarmNWebService2010SoapClient nyService = new NyFarmNWebService2.FarmNWebService2010SoapClient();
        //        Random random = new Random();
        //        int numbersOfAreas = random.Next(1, 20);
        //        NyFarmNWebService2.Areal[] arealer1 = new NyFarmNWebService2.Areal[numbersOfAreas];

        //        NyFarmNWebService2.ArealTyper arealtype1 = new NyFarmNWebService2.ArealTyper();
        //        for (int i = 0; i < arealer1.Count(); i++)
        //        {
        //            ArealListe.Write(times.ToString() + '\t');
        //            ArealListe.Write(arealer1.Count().ToString() + '\t');
        //            NyFarmNWebService2.Areal areal1 = new NyFarmNWebService2.Areal();
        //            int randomType = random.Next(0, 2);
        //            ArealListe.Write(randomType.ToString() + '\t');
        //            arealtype1 = (NyFarmNWebService2.ArealTyper)randomType;
        //            areal1.ArealType = arealtype1;
        //            List<string> listWithRotationNames = getRotationNames();
        //            int witchRotation = random.Next(0, listWithRotationNames.Count);

        //            areal1.Referencesaedskifte = listWithRotationNames.ElementAt(witchRotation);
        //            ArealListe.Write(areal1.Referencesaedskifte.ToString() + '\t');
        //            witchRotation = random.Next(0, listWithRotationNames.Count);
        //            areal1.Saedskifte = listWithRotationNames.ElementAt(witchRotation);
        //            ArealListe.Write(areal1.Saedskifte.ToString() + '\t');
        //            string[] soilType = new string[12] { "JB1", "JB2", "JB3", "JB4", "JB5", "JB6", "JB7", "JB8", "JB9", "JB10", "JB11", "JB12" };
        //            int randomSoilType = random.Next(0, 11);
        //            areal1.Jordbundstype = soilType[randomSoilType];
        //            ArealListe.Write(areal1.Jordbundstype + '\t');
        //            areal1.Ha = random.Next(1, 700);
        //            ArealListe.WriteLine(areal1.Ha.ToString() + '\t');
        //            arealer1[i] = areal1;
        //        }


        //        int randomgoedningsmaengder1 = random.Next(1, 20);
        //        NyFarmNWebService2.Goedningsmaengde[] goedningsmaengder1 = new NyFarmNWebService2.Goedningsmaengde[randomgoedningsmaengder1];
        //        for (int i = 0; i < goedningsmaengder1.Count(); i++)
        //        {
        //            NyFarmNWebService2.Goedningsmaengde goedningsmaengde1 = new NyFarmNWebService2.Goedningsmaengde();
        //            goedningsmaengde1.KgN = random.Next(1, 700);
        //            GoednListe.Write(times.ToString() + '\t' + goedningsmaengder1.Count().ToString() + '\t' + goedningsmaengde1.KgN.ToString() + '\t');

        //            goedningsmaengde1.GoedningstypeNUdnyttelsesProcent = random.Next(0, 100);
        //            GoednListe.Write(goedningsmaengde1.GoedningstypeNUdnyttelsesProcent.ToString() + '\t');

        //            NyFarmNWebService2.Goedningstype goedningstype1 = new NyFarmNWebService2.Goedningstype();
        //            string[] legelManuretype = new string[19] { "Kvæggylle", "Dybstrøelse", "Afsat ved græsning", "Fast gødning", "Ajle", "Svinegylle", "Handelsgødning", "Minkgylle", "Anden husdyrgødning", "Afgasset biomasse", "Forarbejdet husdyrgødning", "Blandet gylle", "Spildevandsslam", "Komposteret husholdningsaffald", "Kartoffelfrugtsaft", "Pressesaft fra grøntpillefraktion", "Have- og parkaffald", "Anden organisk gødning", "Fjerkrægylle" };
        //            int randomManure = random.Next(0, legelManuretype.Count());

        //            goedningstype1.Navn = legelManuretype[randomManure];
        //            GoednListe.WriteLine(goedningstype1.Navn.ToString() + '\t');
        //            goedningsmaengde1.Goedningstype = goedningstype1;
        //            goedningsmaengder1[i] = goedningsmaengde1;
        //        }


        //        CodeAndOptions.Write(times.ToString() + '\t');
        //        int randomZip = random.Next(0, 2);
        //        int zipCode = -1;
        //        if (randomZip == 0)
        //            zipCode = 1000;
        //        else if (randomZip == 1)
        //            zipCode = 9000;

        //        CodeAndOptions.Write(zipCode.ToString() + '\t');

        //        int randomoption1 = random.Next(0, 1);
        //        bool option1;
        //        if (randomoption1 == 0)
        //            option1 = false;
        //        else
        //            option1 = true;
        //        int randomoption2 = random.Next(0, 1);
        //        bool option2;
        //        if (randomoption2 == 0)
        //            option2 = false;
        //        else
        //            option2 = true;
        //        CodeAndOptions.Write(option1.ToString() + '\t');
        //        CodeAndOptions.Write(option2.ToString() + '\n');

        //        //nyService.CalculateApplication(arealer1, goedningsmaengder1, times, zipCode.ToString(), 6.5m, 100m, option1, option2,1);
        //        //nyService.CalculateExisting(arealer1, goedningsmaengder1, times, zipCode.ToString(), option1, option2);



        //    }

        //    ArealListe.Close();
        //    CodeAndOptions.Close();
        //    GoednListe.Close();
        //}
        private List<string> getRotationNames()
        {
            List<string> rotationNames = new List<string>();
            for (int i = 1; i < 14; i++)
            {
                string tmp = "k" + i.ToString();
                rotationNames.Add(tmp);
            }
            for (int i = 1; i < 17; i++)
            {
                string tmp = "s" + i.ToString();
                rotationNames.Add(tmp);
            }
            for (int i = 1; i < 11; i++)
            {
                string tmp = "g" + i.ToString();
                rotationNames.Add(tmp);
            }
            for (int i = 1; i < 14; i++)
            {
                string tmp = "08 k" + i.ToString();
                rotationNames.Add(tmp);
            }
            for (int i = 1; i < 17; i++)
            {
                string tmp = "08 s" + i.ToString();
                rotationNames.Add(tmp);
            }
            for (int i = 1; i < 11; i++)
            {
                string tmp = "08 g" + i.ToString();
                rotationNames.Add(tmp);
            }

            return rotationNames;
        }
        private struct areal
        {
            public int arealType;
            public string Referencesaedskifte, Saedskifte, Jordbundstype;
            public decimal HA;
        }
        private struct manure
        {
            public decimal KgN, GoedningstypeNUdnyttelsesProcent;
            public string godningsType;
        }
        //private List<NyFarmNWebService2.returnItems> callNewWebservice(List<areal> areals, List<manure> manures, long bedriftID, string zipCode, bool option1, bool option2)
        //{
        //    NyFarmNWebService2.FarmNWebService2010SoapClient nyService = new NyFarmNWebService2.FarmNWebService2010SoapClient();
        //    NyFarmNWebService2.Areal[] arealer = new NyFarmNWebService2.Areal[areals.Count];
        //    for (int f = 0; f < areals.Count(); f++)
        //    {

        //        NyFarmNWebService2.ArealTyper arealtype = new NyFarmNWebService2.ArealTyper();
        //        NyFarmNWebService2.Areal areal = new NyFarmNWebService2.Areal();


        //        arealtype = (NyFarmNWebService2.ArealTyper)areals[f].arealType;
        //        areal.ArealType = arealtype;
        //        areal.Referencesaedskifte = areals[f].Referencesaedskifte;
        //        areal.Saedskifte = areals[f].Saedskifte;
        //        areal.Jordbundstype = areals[f].Jordbundstype;
        //        areal.Ha = areals[f].HA;

        //        arealer[f] = areal;
        //    }
        //    NyFarmNWebService2.Goedningsmaengde[] goedningsmaengder1 = new NyFarmNWebService2.Goedningsmaengde[manures.Count];
        //    for (int f = 0; f < manures.Count(); f++)
        //    {
        //        NyFarmNWebService2.Goedningsmaengde goedningsmaengde1 = new NyFarmNWebService2.Goedningsmaengde();


        //        goedningsmaengde1.KgN = manures[f].KgN;
        //        goedningsmaengde1.GoedningstypeNUdnyttelsesProcent = manures[f].GoedningstypeNUdnyttelsesProcent;
        //        NyFarmNWebService2.Goedningstype goedningstype1 = new NyFarmNWebService2.Goedningstype();
        //        goedningstype1.Navn = manures[f].godningsType;
        //        goedningsmaengde1.Goedningstype = goedningstype1;
        //        goedningsmaengder1[f] = goedningsmaengde1;

        //    }
        //    List<NyFarmNWebService2.returnItems> data = new List<testOfEverything.NyFarmNWebService2.returnItems>();

        //    NyFarmNWebService2.returnItems item1 = nyService.CalculateExisting(arealer, goedningsmaengder1, bedriftID, zipCode, option1, option2, 1);
        //    data.Add(item1);
        //    NyFarmNWebService2.returnItems item2 = nyService.CalculateApplication(arealer, goedningsmaengder1, bedriftID, zipCode, 0m, 100m, option1, option2, 1);
        //    data.Add(item2);
        //    NyFarmNWebService2.returnItems item3 = nyService.CalculateScenario3(arealer, goedningsmaengder1, bedriftID, zipCode, 1.5m, option1, option2, 1);
        //    data.Add(item3);
            

        //     return data;
        //}

        private List<NyFarmNWebService2.returnItems> callNewWebservice(List<areal> areals, List<manure> manures, long bedriftID, string zipCode, int cropYear, int version, decimal afterCropPercent)
        {
            NyFarmNWebService2.FarmNWebService2010SoapClient nyService = new NyFarmNWebService2.FarmNWebService2010SoapClient();
            NyFarmNWebService2.Areal[] arealer = new NyFarmNWebService2.Areal[areals.Count];
            for (int f = 0; f < areals.Count(); f++)
            {

                NyFarmNWebService2.ArealTyper arealtype = new NyFarmNWebService2.ArealTyper();
                NyFarmNWebService2.Areal areal = new NyFarmNWebService2.Areal();


                arealtype = (NyFarmNWebService2.ArealTyper)areals[f].arealType;
                areal.ArealType = arealtype;
                areal.Referencesaedskifte = areals[f].Referencesaedskifte;
                areal.Saedskifte = areals[f].Saedskifte;
                areal.Jordbundstype = areals[f].Jordbundstype;
                areal.Ha = areals[f].HA;

                arealer[f] = areal;
            }
            NyFarmNWebService2.Goedningsmaengde[] goedningsmaengder1 = new NyFarmNWebService2.Goedningsmaengde[manures.Count];
            for (int f = 0; f < manures.Count(); f++)
            {
                NyFarmNWebService2.Goedningsmaengde goedningsmaengde1 = new NyFarmNWebService2.Goedningsmaengde();


                goedningsmaengde1.KgN = manures[f].KgN;
                goedningsmaengde1.GoedningstypeNUdnyttelsesProcent = manures[f].GoedningstypeNUdnyttelsesProcent;
                NyFarmNWebService2.Goedningstype goedningstype1 = new NyFarmNWebService2.Goedningstype();
                goedningstype1.Navn = manures[f].godningsType;
                goedningsmaengde1.Goedningstype = goedningstype1;
                goedningsmaengder1[f] = goedningsmaengde1;

            }
            List<NyFarmNWebService2.returnItems> data = new List<testOfEverything.NyFarmNWebService2.returnItems>();

            //NyFarmNWebService2.returnItems item1 = nyService.CalculateExisting(arealer, goedningsmaengder1, bedriftID, zipCode, cropYear, version);
            //data.Add(item1);
            NyFarmNWebService2.returnItems item2 = nyService.CalculateApplication(arealer, goedningsmaengder1, bedriftID, zipCode, afterCropPercent, 0m, cropYear, version);
            data.Add(item2);
            //NyFarmNWebService2.returnItems item3 = nyService.CalculateScenario3(arealer, goedningsmaengder1, bedriftID, zipCode, 1.5m, cropYear, version);
            //data.Add(item3);


            return data;
        }
        private bool callOldService(List<areal> areals, List<manure> manures, long bedriftID, string zipCode, bool option1, bool option2)
        {


            ServiceReferenceOld.Areal[] arealer = new ServiceReferenceOld.Areal[areals.Count];
            for (int f = 0; f < areals.Count(); f++)
            {

                ServiceReferenceOld.ArealTyper arealtype = new ServiceReferenceOld.ArealTyper();
                ServiceReferenceOld.Areal areal = new ServiceReferenceOld.Areal();


                arealtype = (ServiceReferenceOld.ArealTyper)areals[f].arealType;
                areal.ArealType = arealtype;
                areal.Referencesaedskifte = areals[f].Referencesaedskifte;
                areal.Saedskifte = areals[f].Saedskifte;
                areal.Jordbundstype = areals[f].Jordbundstype;
                areal.Ha = areals[f].HA;

                arealer[f] = areal;
            }
            ServiceReferenceOld.Goedningsmaengde[] goedningsmaengder1 = new ServiceReferenceOld.Goedningsmaengde[manures.Count];
            for (int f = 0; f < manures.Count(); f++)
            {
                ServiceReferenceOld.Goedningsmaengde goedningsmaengde1 = new ServiceReferenceOld.Goedningsmaengde();


                goedningsmaengde1.KgN = manures[f].KgN;
                goedningsmaengde1.GoedningstypeNUdnyttelsesProcent = manures[f].GoedningstypeNUdnyttelsesProcent;
                ServiceReferenceOld.Goedningstype goedningstype1 = new ServiceReferenceOld.Goedningstype();
                goedningstype1.Navn = manures[f].godningsType;
                goedningsmaengde1.Goedningstype = goedningstype1;
                goedningsmaengder1[f] = goedningsmaengde1;

            }
            ServiceReferenceOld.FarmNWebServiceSoapClient PService = new ServiceReferenceOld.FarmNWebServiceSoapClient();
            ServiceReferenceOld.FarmNData[] farmNList1 = new ServiceReferenceOld.FarmNData[1];
            ServiceReferenceOld.FarmNData[] farmNList2 = new ServiceReferenceOld.FarmNData[1];
            ServiceReferenceOld.FarmNData[] farmNList3 = new ServiceReferenceOld.FarmNData[1];
            bool notFailed = true;
            bool notOverAllFailed = true;
            try
            {
                
                int timeFailed = 0;
                TimeSpan oldServiec = new TimeSpan();
                DateTime start;
                 Console.WriteLine("calling "+bedriftID.ToString());

                 while (notFailed && notOverAllFailed==true)
                {

                    start = DateTime.Now;
                    Console.WriteLine("part 1");
                    farmNList1 = PService.BeregnNudriftNy(arealer, goedningsmaengder1, bedriftID, zipCode, option1, option2);
                    oldServiec = DateTime.Now - start;
                    
                    if (oldServiec.Minutes > 1)
                    {
                        if (timeFailed == 3)
                        {
                            notFailed = false;
                            notOverAllFailed = false;
                        }
                        timeFailed++;
                        Console.WriteLine("fail on " + bedriftID.ToString() + " for the " + timeFailed.ToString() + "th time");
                    }
                    else
                        notFailed = false;
                   
                }
                 notFailed = true;
                 while (notFailed && notOverAllFailed == true)
                {

                    start = DateTime.Now;
                    Console.WriteLine("part 2");
                    farmNList2 = PService.BeregnAnsoegtNy(arealer, goedningsmaengder1, bedriftID, zipCode, 0, 100, option1, option2);
                    oldServiec = DateTime.Now - start;
                    if (oldServiec.Minutes > 1)
                    {
                        if (timeFailed == 3)
                        {
                            notFailed = false;
                            notOverAllFailed = false;
                        }
                        timeFailed++;
                        Console.WriteLine("fail on " + bedriftID.ToString() + " for the " + timeFailed.ToString() + "th time");
                    }
                    else
                        notFailed = false; 

                }
                notFailed = true;
                while (notFailed && notOverAllFailed == true)
                {

                    start = DateTime.Now;
                    Console.WriteLine("part 3");
                    farmNList3 = PService.Pakke3Ny(arealer, goedningsmaengder1, bedriftID, zipCode, 1.5m, option1, option2);
                    oldServiec = DateTime.Now - start;
                    if (oldServiec.Minutes > 1)
                    {
                        if (timeFailed == 3)
                        {
                            notFailed = false;
                            notOverAllFailed = false;
                        }
                        timeFailed++;
                        Console.WriteLine("fail on " + bedriftID.ToString() + " for the " + timeFailed.ToString() + "th time");
                    }
                    else
                        notFailed = false; 
                    
                }
                if (notOverAllFailed == true)
                {
                    Console.WriteLine("skrives til fil");
                    printTofile(bedriftID, "BeregnNudriftNy", farmNList1);
                    printTofile(bedriftID, "BeregnAnsoegtNy", farmNList2);
                    printTofile(bedriftID, "Pakke3Ny", farmNList3);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message.ToString());
            }
            Console.WriteLine(bedriftID.ToString() + " bool: " + notOverAllFailed);
            return notOverAllFailed;


        }

        private List<DataFromOldWebserver> callOldService(List<areal> areals, List<manure> manures, long bedriftID, string zipCode, bool option1, bool option2, decimal afterCropPercent)
        {
            List<DataFromOldWebserver> returnData = new List<DataFromOldWebserver>();

            ServiceReferenceOld.Areal[] arealer = new ServiceReferenceOld.Areal[areals.Count];
            for (int f = 0; f < areals.Count(); f++)
            {

                ServiceReferenceOld.ArealTyper arealtype = new ServiceReferenceOld.ArealTyper();
                ServiceReferenceOld.Areal areal = new ServiceReferenceOld.Areal();


                arealtype = (ServiceReferenceOld.ArealTyper)areals[f].arealType;
                areal.ArealType = arealtype;
                areal.Referencesaedskifte = areals[f].Referencesaedskifte;
                areal.Saedskifte = areals[f].Saedskifte;
                areal.Jordbundstype = areals[f].Jordbundstype;
                areal.Ha = areals[f].HA;

                arealer[f] = areal;
            }
            ServiceReferenceOld.Goedningsmaengde[] goedningsmaengder1 = new ServiceReferenceOld.Goedningsmaengde[manures.Count];
            for (int f = 0; f < manures.Count(); f++)
            {
                ServiceReferenceOld.Goedningsmaengde goedningsmaengde1 = new ServiceReferenceOld.Goedningsmaengde();


                goedningsmaengde1.KgN = manures[f].KgN;
                goedningsmaengde1.GoedningstypeNUdnyttelsesProcent = manures[f].GoedningstypeNUdnyttelsesProcent;
                ServiceReferenceOld.Goedningstype goedningstype1 = new ServiceReferenceOld.Goedningstype();
                goedningstype1.Navn = manures[f].godningsType;
                goedningsmaengde1.Goedningstype = goedningstype1;
                goedningsmaengder1[f] = goedningsmaengde1;

            }
            ServiceReferenceOld.FarmNWebServiceSoapClient PService = new ServiceReferenceOld.FarmNWebServiceSoapClient();
            ServiceReferenceOld.FarmNData[] farmNList1 = new ServiceReferenceOld.FarmNData[1];
            ServiceReferenceOld.FarmNData[] farmNList2 = new ServiceReferenceOld.FarmNData[1];
            //ServiceReferenceOld.FarmNData[] farmNList3 = new ServiceReferenceOld.FarmNData[1];
            bool notFailed = true;
            bool notOverAllFailed = true;
            try
            {

                int timeFailed = 0;
                TimeSpan oldServiec = new TimeSpan();
                DateTime start;
                Console.WriteLine("calling " + bedriftID.ToString());

                while (notFailed && notOverAllFailed == true)
                {

                    start = DateTime.Now;
                    Console.WriteLine("part 1");
                    farmNList1 = PService.BeregnNudriftNy(arealer, goedningsmaengder1, bedriftID, zipCode, option1, option2);
                    oldServiec = DateTime.Now - start;

                    if (oldServiec.Minutes > 1)
                    {
                        if (timeFailed == 3)
                        {
                            notFailed = false;
                            notOverAllFailed = false;
                        }
                        timeFailed++;
                        Console.WriteLine("fail on " + bedriftID.ToString() + " for the " + timeFailed.ToString() + "th time");
                    }
                    else
                        notFailed = false;

                }
                notFailed = true;
                while (notFailed && notOverAllFailed == true)
                {

                    start = DateTime.Now;
                    Console.WriteLine("part 2");
                    farmNList2 = PService.BeregnAnsoegtNy(arealer, goedningsmaengder1, bedriftID, zipCode, afterCropPercent, 100m, option1, option2);
                    oldServiec = DateTime.Now - start;
                    if (oldServiec.Minutes > 1)
                    {
                        if (timeFailed == 3)
                        {
                            notFailed = false;
                            notOverAllFailed = false;
                        }
                        timeFailed++;
                        Console.WriteLine("fail on " + bedriftID.ToString() + " for the " + timeFailed.ToString() + "th time");
                    }
                    else
                        notFailed = false;

                }
                notFailed = true;
                //while (notFailed && notOverAllFailed == true)
                //{

                //    start = DateTime.Now;
                //    Console.WriteLine("part 3");
                //    farmNList3 = PService.Pakke3Ny(arealer, goedningsmaengder1, bedriftID, zipCode, 1.5m, option1, option2);
                //    oldServiec = DateTime.Now - start;
                //    if (oldServiec.Minutes > 1)
                //    {
                //        if (timeFailed == 3)
                //        {
                //            notFailed = false;
                //            notOverAllFailed = false;
                //        }
                //        timeFailed++;
                //        Console.WriteLine("fail on " + bedriftID.ToString() + " for the " + timeFailed.ToString() + "th time");
                //    }
                //    else
                //        notFailed = false;

                }
                //if (notOverAllFailed == true)
                //{
                //    Console.WriteLine("skrives til fil");
                //    printTofile(bedriftID, "BeregnNudriftNy", farmNList1);
                //    printTofile(bedriftID, "BeregnAnsoegtNy", farmNList2);
                //    printTofile(bedriftID, "Pakke3Ny", farmNList3);
            //   }
            //}
            catch (Exception e)
            {
                Console.WriteLine(e.Message.ToString());
            }
            Console.WriteLine(bedriftID.ToString() + " bool: " + notOverAllFailed);
            DataFromOldWebserver data = new DataFromOldWebserver();
            data.farmID = Convert.ToInt32(bedriftID);
            data.BeregnNudriftNy = farmNList1;
            data.BeregnAnsoegtNy = farmNList2;
            //data.Pakke3Ny = farmNList3;
            returnData.Add(data);
            return returnData;


        }

        bool firstTime = true;
        private static int place = 0;
        public void FileTest()
        {
            List<DataFromOldWebserver> dataFromOldTest = readOldFile();
            bool oldWebsever = false;
            TextReader ArealListe = File.OpenText("C:\\ArealListe.txt");
            TextReader CodeAndOptions = File.OpenText("C:\\CodeAndOptions.txt");
            TextReader GoednListe = File.OpenText("C:\\GoednListe.txt");


            string InputFromArealListe = ArealListe.ReadLine();
            string InputFromCodeAndOptions = CodeAndOptions.ReadLine();
            string InputFromGoednListe = GoednListe.ReadLine();
            string[] informationAreal = new string[8];
            string[] goedningsmaengderListe = new string[6];
            string[] restList = new string[4];
            int sleepTime = 2;
            while (InputFromArealListe != null || InputFromCodeAndOptions != null || InputFromGoednListe != null)
                //for (int x = 0 ;x < 2; x++)
            {
               
              //  OldService.FarmNWebServiceSoapClient oldService = new OldService.FarmNWebServiceSoapClient();

                string input = null;
                if (firstTime == true)
                {
                    goedningsmaengderListe = GoednListe.ReadLine().Split('\t');
                    informationAreal = ArealListe.ReadLine().Split('\t');
                }
                input = CodeAndOptions.ReadLine();
                if (input == null)
                    break;
                restList = input.Split('\t');
                int removeFarmNr = -1;
                while (Convert.ToInt32(informationAreal[0]) != Convert.ToInt32(goedningsmaengderListe[0]) || Convert.ToInt32(informationAreal[0]) != Convert.ToInt32(restList[0]))
                {

                    if (Convert.ToInt32(informationAreal[0]) < Convert.ToInt32(goedningsmaengderListe[0]))
                    {
                        informationAreal = ArealListe.ReadLine().Split('\t');
                    }
                    else if (Convert.ToInt32(informationAreal[0]) > Convert.ToInt32(goedningsmaengderListe[0]))
                    {
                        goedningsmaengderListe = GoednListe.ReadLine().Split('\t');
                    }
                    if (Convert.ToInt32(informationAreal[0]) < Convert.ToInt32(restList[0]))
                    {
                        informationAreal = ArealListe.ReadLine().Split('\t');
                    }
                    else if (Convert.ToInt32(informationAreal[0]) > Convert.ToInt32(restList[0]))
                    {
                        restList = CodeAndOptions.ReadLine().Split('\t');
                    }

                }

                if (informationAreal[1].CompareTo("NULL") == 0)
                {
                    removeFarmNr = Convert.ToInt32(informationAreal[0]);

                }
                while (removeFarmNr == Convert.ToInt32(informationAreal[0]))
                {
                    input = ArealListe.ReadLine();
                    if (input != null)
                        informationAreal = input.Split('\t');
                }
                while (removeFarmNr == Convert.ToInt32(goedningsmaengderListe[0]))
                {

                    input = GoednListe.ReadLine();
                    if (input != null)
                        goedningsmaengderListe = input.Split('\t');
                }
                while (removeFarmNr == Convert.ToInt32(restList[0]))
                {

                    input = CodeAndOptions.ReadLine();
                    if (input != null)
                        restList = input.Split('\t');
                }

                List<areal> allareas = new List<areal>();

                int times = Convert.ToInt32(informationAreal[1]);
                for (int f = 0; f < times; f++)
                {

                    areal area;
                    area.arealType = Convert.ToInt32(informationAreal[2]);
                    area.Referencesaedskifte = informationAreal[3];
                    area.Saedskifte = informationAreal[4];
                    area.Jordbundstype = informationAreal[5];
                    try
                    {
                        area.HA = Convert.ToDecimal(informationAreal[6]);
                    }
                    catch
                    {
                        area.HA = Decimal.Parse(informationAreal[6], System.Globalization.NumberStyles.Float);
                    }

                    allareas.Add(area);
                    input = ArealListe.ReadLine();
                    if (input != null)
                        informationAreal = input.Split('\t');
                    else
                        break;


                }


                List<manure> allManure = new List<manure>();
                times = Convert.ToInt32(goedningsmaengderListe[1]);
                for (int f = 0; f < times; f++)
                {

                    manure oneManure;
                    try
                    {
                        oneManure.KgN = Convert.ToDecimal(goedningsmaengderListe[2]);
                    }
                    catch
                    {
                        oneManure.KgN = Decimal.Parse(informationAreal[6], System.Globalization.NumberStyles.Float);
                    }

                    oneManure.GoedningstypeNUdnyttelsesProcent = Convert.ToDecimal(goedningsmaengderListe[3]);
                    oneManure.godningsType = goedningsmaengderListe[4];
                    allManure.Add(oneManure);
                    input = GoednListe.ReadLine();
                    if (input != null)
                        goedningsmaengderListe = input.Split('\t');
                    else
                        break;


                }

                long bedriftID = Convert.ToInt32(restList[0]);


                string zipCode = restList[1];

                bool option1 = Convert.ToBoolean(restList[2]);

                bool option2 = Convert.ToBoolean(restList[3]);
                int version = -1;
                int cropyear = -1;
                if (option1 == true)
                {
                    version = 1;
                    cropyear = 2007;
                }
                else
                {
                    version = 2;
                    cropyear = 2009;
                }
                NyFarmNWebService2.returnItems newData = new NyFarmNWebService2.returnItems();



                if (oldWebsever == false)
                {


                    if (dataFromOldTest.Count() > place)
                    {
                        Console.WriteLine(bedriftID);
                        //if (bedriftID < 7205351)
                        //if (bedriftID == 18619826)
                            //if (bedriftID == 15518311)//13815458 || bedriftID == 9257025)// || bedriftID == 9508062 || bedriftID == 10505822 || bedriftID == 10907556 || bedriftID == 11006115 || bedriftID == 11006761 || bedriftID == 12007318 || bedriftID == 12108768 || bedriftID == 12456907 || bedriftID == 13158614 || bedriftID == 13209572 || bedriftID == 13613750)
                        if (bedriftID > 1)//13007628)
                        {
                            decimal statutoryUtilDegree = 1;
                            foreach (manure m in allManure)
                            {
                                if (m.godningsType == "Afgasset biomasse" || m.godningsType == "Forarbejdet husdyrgødning" || m.godningsType == "Blandet gylle")
                                {
                                    statutoryUtilDegree = 0;
                                }
                            }
                            if (statutoryUtilDegree == 0)
                            {
                                statutoryUtilDegree = (from m in allManure where m.godningsType == "Afgasset biomasse" select m.GoedningstypeNUdnyttelsesProcent).Sum();
                                statutoryUtilDegree += (from m in allManure where m.godningsType == "Forarbejdet husdyrgødning" select m.GoedningstypeNUdnyttelsesProcent).Sum();
                                statutoryUtilDegree += (from m in allManure where m.godningsType == "Blandet gylle" select m.GoedningstypeNUdnyttelsesProcent).Sum();
                            }
                                if (dataFromOldTest.ElementAt(place).farmID == bedriftID)
                            {
                                if (controlForInput(dataFromOldTest) == true)
                                {
                                    var areaSum = (from a in allareas select a.HA).Sum();
                                    var manureSum = (from m in allManure select m.KgN).Sum();
                                    if (manureSum / areaSum < 700m)
                                        if (statutoryUtilDegree > 0m)
                                        {
                                            List<NyFarmNWebService2.returnItems> newDataSet = callNewWebservice(allareas, allManure, bedriftID, zipCode, cropyear,version,0);
                                            //compareAndLog(dataFromOldTest, newDataSet, bedriftID);
                                            writePlotLog(dataFromOldTest, newDataSet, bedriftID);
                                        }
                                }
                            }
                            else if (dataFromOldTest.ElementAt(place).farmID < bedriftID)
                            {
                                while (dataFromOldTest.ElementAt(place).farmID < bedriftID)
                                {
                                    place++;
                                    if (dataFromOldTest.Count() == place)
                                    {
                                        break;
                                    }
                                }
                                if (dataFromOldTest.ElementAt(place).farmID == bedriftID)
                                {
                                    if (controlForInput(dataFromOldTest) == true)
                                    {
                                        var areaSum = (from a in allareas select a.HA).Sum();
                                        var manureSum = (from m in allManure select m.KgN).Sum();
                                        if (manureSum / areaSum < 700m)
                                            if (statutoryUtilDegree > 0m)
                                            {
                                                List<NyFarmNWebService2.returnItems> newDataSet = callNewWebservice(allareas, allManure, bedriftID, zipCode, cropyear,version,0);
                                                //compareAndLog(dataFromOldTest, newDataSet, bedriftID);
                                                writePlotLog(dataFromOldTest, newDataSet, bedriftID);
                                            }
                                    }

                                }
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("we are done");
                    }
                    
                }
                else
                {
                    if (bedriftID > 14219635)
                    {
                        DateTime start = DateTime.Now;
                        //
                        //newServiec = DateTime.Now - start;
                        ServiceReferenceOld.FarmNData[] oldData = new ServiceReferenceOld.FarmNData[0];

                        int dayinWeek = (int)DateTime.Now.DayOfWeek;
                        if (DateTime.Now.Hour >= 18 || DateTime.Now.Hour <= 8 || dayinWeek == 6 || dayinWeek == 7)
                            sleepTime = 3;
                        else
                            sleepTime = 6;

                        bool notFailed = false;

                        notFailed = callOldService(allareas, allManure, bedriftID, zipCode, option1, option2);




                        StreamWriter tw = File.AppendText("c:\\date.txt");
                        if (notFailed == true)
                        {
                            tw.Write(bedriftID);
                            tw.WriteLine(" succeded");
                        }
                        else
                        {
                            tw.Write(bedriftID);
                            tw.WriteLine(" failed");
                        }
                        tw.Close();
                        Thread.Sleep(sleepTime * 1000 * 60);
                    }
                }
                firstTime = false;

            }
            ArealListe.Close();
            CodeAndOptions.Close();
            GoednListe.Close();

        }

        public void AfterCropTest()
        {
            List<DataFromOldWebserver> dataFromOldTest = null;//readOldFile();
            bool oldWebsever = false;
            TextReader ArealListe = File.OpenText("C:\\ArealListe.txt");
            TextReader CodeAndOptions = File.OpenText("C:\\CodeAndOptions.txt");
            TextReader GoednListe = File.OpenText("C:\\GoednListe.txt");


            string InputFromArealListe = ArealListe.ReadLine();
            string InputFromCodeAndOptions = CodeAndOptions.ReadLine();
            string InputFromGoednListe = GoednListe.ReadLine();
            string[] informationAreal = new string[8];
            string[] goedningsmaengderListe = new string[6];
            string[] restList = new string[4];
            int sleepTime = 2;
            while (InputFromArealListe != null || InputFromCodeAndOptions != null || InputFromGoednListe != null)
            {
                string input = null;
                if (firstTime == true)
                {
                    goedningsmaengderListe = GoednListe.ReadLine().Split('\t');
                    informationAreal = ArealListe.ReadLine().Split('\t');
                }
                input = CodeAndOptions.ReadLine();
                if (input == null)
                    break;
                restList = input.Split('\t');
                int removeFarmNr = -1;
                while (Convert.ToInt32(informationAreal[0]) != Convert.ToInt32(goedningsmaengderListe[0]) || Convert.ToInt32(informationAreal[0]) != Convert.ToInt32(restList[0]))
                {

                    if (Convert.ToInt32(informationAreal[0]) < Convert.ToInt32(goedningsmaengderListe[0]))
                    {
                        informationAreal = ArealListe.ReadLine().Split('\t');
                    }
                    else if (Convert.ToInt32(informationAreal[0]) > Convert.ToInt32(goedningsmaengderListe[0]))
                    {
                        goedningsmaengderListe = GoednListe.ReadLine().Split('\t');
                    }
                    if (Convert.ToInt32(informationAreal[0]) < Convert.ToInt32(restList[0]))
                    {
                        informationAreal = ArealListe.ReadLine().Split('\t');
                    }
                    else if (Convert.ToInt32(informationAreal[0]) > Convert.ToInt32(restList[0]))
                    {
                        restList = CodeAndOptions.ReadLine().Split('\t');
                    }

                }

                if (informationAreal[1].CompareTo("NULL") == 0)
                {
                    removeFarmNr = Convert.ToInt32(informationAreal[0]);

                }
                while (removeFarmNr == Convert.ToInt32(informationAreal[0]))
                {
                    input = ArealListe.ReadLine();
                    if (input != null)
                        informationAreal = input.Split('\t');
                }
                while (removeFarmNr == Convert.ToInt32(goedningsmaengderListe[0]))
                {

                    input = GoednListe.ReadLine();
                    if (input != null)
                        goedningsmaengderListe = input.Split('\t');
                }
                while (removeFarmNr == Convert.ToInt32(restList[0]))
                {

                    input = CodeAndOptions.ReadLine();
                    if (input != null)
                        restList = input.Split('\t');
                }

                List<areal> allareas = new List<areal>();

                int times = Convert.ToInt32(informationAreal[1]);
                for (int f = 0; f < times; f++)
                {

                    areal area;
                    area.arealType = Convert.ToInt32(informationAreal[2]);
                    area.Referencesaedskifte = informationAreal[3];
                    area.Saedskifte = informationAreal[4];
                    area.Jordbundstype = informationAreal[5];
                    try
                    {
                        area.HA = Convert.ToDecimal(informationAreal[6]);
                    }
                    catch
                    {
                        area.HA = Decimal.Parse(informationAreal[6], System.Globalization.NumberStyles.Float);
                    }

                    allareas.Add(area);
                    input = ArealListe.ReadLine();
                    if (input != null)
                        informationAreal = input.Split('\t');
                    else
                        break;


                }


                List<manure> allManure = new List<manure>();
                times = Convert.ToInt32(goedningsmaengderListe[1]);
                for (int f = 0; f < times; f++)
                {

                    manure oneManure;
                    try
                    {
                        oneManure.KgN = Convert.ToDecimal(goedningsmaengderListe[2]);
                    }
                    catch
                    {
                        oneManure.KgN = Decimal.Parse(informationAreal[6], System.Globalization.NumberStyles.Float);
                    }

                    oneManure.GoedningstypeNUdnyttelsesProcent = Convert.ToDecimal(goedningsmaengderListe[3]);
                    oneManure.godningsType = goedningsmaengderListe[4];
                    allManure.Add(oneManure);
                    input = GoednListe.ReadLine();
                    if (input != null)
                        goedningsmaengderListe = input.Split('\t');
                    else
                        break;


                }

                long bedriftID = Convert.ToInt32(restList[0]);


                string zipCode = restList[1];

                bool option1 = Convert.ToBoolean(restList[2]);

                bool option2 = Convert.ToBoolean(restList[3]);
                int version = -1;
                int cropyear = -1;
                if (option1 == true)
                {
                    version = 1;
                    cropyear = 2007;
                }
                else
                {
                    version = 2;
                    cropyear = 2009;
                }
                NyFarmNWebService2.returnItems newData = new NyFarmNWebService2.returnItems();



                if (oldWebsever == false)
                {


                        Console.WriteLine(bedriftID);
                        //if (bedriftID < 6808222)
                        //if (bedriftID == 6856056)//6807757//6807016)//6856056
                        //if (bedriftID == 15518311)//13815458 || bedriftID == 9257025)// || bedriftID == 9508062 || bedriftID == 10505822 || bedriftID == 10907556 || bedriftID == 11006115 || bedriftID == 11006761 || bedriftID == 12007318 || bedriftID == 12108768 || bedriftID == 12456907 || bedriftID == 13158614 || bedriftID == 13209572 || bedriftID == 13613750)
                        if (bedriftID == 104)
                        {
                            decimal statutoryUtilDegree = 1;
                            foreach (manure m in allManure)
                            {
                                if (m.godningsType == "Afgasset biomasse" || m.godningsType == "Forarbejdet husdyrgødning" || m.godningsType == "Blandet gylle")
                                {
                                    statutoryUtilDegree = 0;
                                }
                            }
                            if (statutoryUtilDegree == 0)
                            {
                                statutoryUtilDegree = (from m in allManure where m.godningsType == "Afgasset biomasse" select m.GoedningstypeNUdnyttelsesProcent).Sum();
                                statutoryUtilDegree += (from m in allManure where m.godningsType == "Forarbejdet husdyrgødning" select m.GoedningstypeNUdnyttelsesProcent).Sum();
                                statutoryUtilDegree += (from m in allManure where m.godningsType == "Blandet gylle" select m.GoedningstypeNUdnyttelsesProcent).Sum();
                            }
                                        var areaSum = (from a in allareas select a.HA).Sum();
                                        var manureSum = (from m in allManure select m.KgN).Sum();
                                        if (manureSum / areaSum < 700m)
                                            if (statutoryUtilDegree > 0m)
                                            {
                                                decimal d = 90m;
                                                //for (decimal d = 0.01m; d < 21m; d = d + 5)
                                                //{
                                                    dataFromOldTest = callOldService(allareas, allManure, bedriftID, zipCode, option1, option2, d);
                                                    List<NyFarmNWebService2.returnItems> newDataSet = callNewWebservice(allareas, allManure, bedriftID, zipCode, cropyear, version, d);
                                                    Thread.Sleep(4000);
                                                    writePlotLogAfterCrop(dataFromOldTest, newDataSet, bedriftID, d);
                                                //}
                                            }
                          }
                }
                else
                {
                    if (bedriftID == 6906621)
                    {
                        DateTime start = DateTime.Now;
                        //
                        //newServiec = DateTime.Now - start;
                        ServiceReferenceOld.FarmNData[] oldData = new ServiceReferenceOld.FarmNData[0];

                        int dayinWeek = (int)DateTime.Now.DayOfWeek;
                        if (DateTime.Now.Hour >= 18 || DateTime.Now.Hour <= 8 || dayinWeek == 6 || dayinWeek == 7)
                            sleepTime = 3;
                        else
                            sleepTime = 6;

                        bool notFailed = false;

                        dataFromOldTest = callOldService(allareas, allManure, bedriftID, zipCode, option1, option2, 0m);




                        StreamWriter tw = File.AppendText("c:\\date.txt");
                        if (notFailed == true)
                        {
                            tw.Write(bedriftID);
                            tw.WriteLine(" succeded");
                        }
                        else
                        {
                            tw.Write(bedriftID);
                            tw.WriteLine(" failed");
                        }
                        tw.Close();
                        //Thread.Sleep(sleepTime * 1000 * 60);
                    }
                }
                firstTime = false;

            }
            ArealListe.Close();
            CodeAndOptions.Close();
            GoednListe.Close();

        }

        private bool controlForInput(List<DataFromOldWebserver> olddata)
        {
            if (olddata.ElementAt(place).BeregnAnsoegtNy.Count() <= 0)
                return false;
            if (olddata.ElementAt(place).BeregnNudriftNy.Count() <= 0)
                return false;
            if (olddata.ElementAt(place).Pakke3Ny.Count() <= 0)
                return false;
            return true;
        }
        private void compareAndLog(List<DataFromOldWebserver> olddata, List<NyFarmNWebService2.returnItems> newData, long bedriftID)
        {
            StreamWriter tw = File.AppendText("c:\\log.txt");
            StreamWriter tq = File.AppendText("c:\\result.txt");
            if (olddata.ElementAt(place).farmID == bedriftID)
            {
                if (olddata.ElementAt(place).BeregnNudriftNy.Count() != newData.ElementAt(0).arealData.Count())
                    tw.WriteLine("not the same amount of elements in Scenarie 1 for " + bedriftID);
                if (olddata.ElementAt(place).BeregnAnsoegtNy.Count() != newData.ElementAt(1).arealData.Count())
                    tw.WriteLine("not the same amount of elements in Scenarie 2 for " + bedriftID);
                if (olddata.ElementAt(place).Pakke3Ny.Count() != newData.ElementAt(2).arealData.Count())
                    tw.WriteLine("not the same amount of elements in Scenarie 3 for " + bedriftID);
                if (newData.ElementAt(0).errorMessages.Count() > 0)
                {
                    foreach (NyFarmNWebService2.error m in newData.ElementAt(0).errorMessages)
                    {
                        tq.WriteLine(m.ErrorMessage);
                    }
                }
                decimal first = olddata.ElementAt(place).BeregnNudriftNy.ElementAt(0).NLeach_mgN_l_total;
                decimal second = newData.ElementAt(0).NLeach_mgN_l_korrigeret;
                if (Math.Abs(first - second) > 0.1m)
                    tw.WriteLine("not the same amount of NLeach_KgN_ha_total in Scenarie 1 for " + bedriftID + " Gl: " + first + " Ny: " + second);
                first = olddata.ElementAt(place).BeregnNudriftNy.ElementAt(0).NLeach_KgN_ha_total;
                second = newData.ElementAt(0).NLeach_KgN_ha_korrigeret;
                if (Math.Abs(first - second) > 0.1m)
                    tw.WriteLine("not the same amount of NLeach_KgN_ha_total in Scenarie 1 for " + bedriftID + " Gl: " + first + " Ny: " + second);
                decimal first1 = olddata.ElementAt(place).BeregnAnsoegtNy.ElementAt(0).NLeach_KgN_ha_total;
                decimal second1 = newData.ElementAt(1).NLeach_KgN_ha_korrigeret;
                if (Math.Abs(first1 - second1) > 0.1m)
                    tw.WriteLine("not the same amount of NLeach_KgN_ha_total in Scenarie 2 for " + bedriftID + " Gl: " + first1 + " Ny: " + second1);
                first1 = olddata.ElementAt(place).BeregnAnsoegtNy.ElementAt(0).NLeach_mgN_l_total;
                second1 = newData.ElementAt(1).NLeach_mgN_l_korrigeret;
                if (Math.Abs(first1 - second1) > 0.1m)
                    tw.WriteLine("not the same amount of NLeach_mgN_l_total in Scenarie 2 for " + bedriftID + " Gl: " + first1 + " Ny: " + second1);

                decimal first2 = olddata.ElementAt(place).Pakke3Ny.ElementAt(0).NLeach_KgN_ha_total;
                decimal second2 = newData.ElementAt(2).NLeach_KgN_ha_korrigeret;
                if (Math.Abs(first2 - second2) > 0.1m)
                    tw.WriteLine("not the same amount of NLeach_KgN_ha_total in Scenarie 3 for " + bedriftID + " Gl: " + first2 + " Ny: " + second2);
                first2 = olddata.ElementAt(place).Pakke3Ny.ElementAt(0).NLeach_mgN_l_total;
                second2 = newData.ElementAt(2).NLeach_mgN_l_korrigeret;
                if (Math.Abs(first2 - second2) > 0.1m)
                    tw.WriteLine("not the same amount of NLeach_mgN_l_total in Scenarie 3 for " + bedriftID + " Gl: " + first2 + " Ny: " + second2);
                tw.WriteLine("Compared " + bedriftID);

                tq.Write(bedriftID + "/Scenarie/sædskifte");
                tq.Write('\t');
                tq.Write("Gl_NLeach_KgN_ha");
                tq.Write('\t');
                tq.Write("Ny_NLeach_KgN_ha");
                tq.Write('\t');
                tq.Write("Gl_NLeach_mgN_l");
                tq.Write('\t');
                tq.Write("Ny_NLeach_mgN_l");
                tq.Write('\t');
                tq.Write("Gl_NLeach_KgN_ha_total");
                tq.Write('\t');
                tq.Write("Ny_NLeach_KgN_ha_total");
                tq.Write('\t');
                tq.Write("Gl_NLeach_mgN_l_total");
                tq.Write('\t');
                tq.Write("Ny_NLeach_mgN_l_total");
                tq.WriteLine('\t');
                tq.Write("Scenarie 1");
                tq.WriteLine('\t');

                for (int i = 0; i < olddata.ElementAt(place).BeregnNudriftNy.Count(); i++)
                {
                    tq.Write(olddata.ElementAt(place).BeregnNudriftNy[i].Saedskifte);
                    tq.Write('\t');
                    tq.Write(olddata.ElementAt(place).BeregnNudriftNy[i].NLeach_KgN_ha);
                    tq.Write('\t');
                    tq.Write(newData.ElementAt(0).arealData.ElementAt(i).UdvaskningKgNHa);
                    tq.Write('\t');
                    tq.Write(olddata.ElementAt(place).BeregnNudriftNy[i].NLeach_mgN_l);
                    tq.Write('\t');
                    tq.Write(newData.ElementAt(0).arealData.ElementAt(i).UdvaskningMgPrL);
                    tq.Write('\t');
                    tq.Write(olddata.ElementAt(place).BeregnNudriftNy[i].NLeach_KgN_ha_total);
                    tq.Write('\t');
                    tq.Write(newData.ElementAt(0).NLeach_KgN_ha_korrigeret);
                    tq.Write('\t');
                    tq.Write(olddata.ElementAt(place).BeregnNudriftNy[i].NLeach_mgN_l_total);
                    tq.Write('\t');
                    tq.Write(newData.ElementAt(0).NLeach_mgN_l_korrigeret);
                    tq.WriteLine('\t');
                }
                tq.Write("Scenarie 2");
                tq.WriteLine('\t');
                for (int i = 0; i < olddata.ElementAt(place).BeregnAnsoegtNy.Count(); i++)
                {
                    tq.Write(olddata.ElementAt(place).BeregnAnsoegtNy[i].Referencesaedskifte);
                    tq.Write('\t');
                    tq.Write(olddata.ElementAt(place).BeregnAnsoegtNy[i].NLeach_KgN_ha);
                    tq.Write('\t');
                    tq.Write(newData.ElementAt(1).arealData.ElementAt(i).UdvaskningKgNHa);
                    tq.Write('\t');
                    tq.Write(olddata.ElementAt(place).BeregnAnsoegtNy[i].NLeach_mgN_l);
                    tq.Write('\t');
                    tq.Write(newData.ElementAt(1).arealData.ElementAt(i).UdvaskningMgPrL);
                    tq.Write('\t');
                    tq.Write(olddata.ElementAt(place).BeregnAnsoegtNy[i].NLeach_KgN_ha_total);
                    tq.Write('\t');
                    tq.Write(newData.ElementAt(1).NLeach_KgN_ha_korrigeret);
                    tq.Write('\t');
                    tq.Write(olddata.ElementAt(place).BeregnAnsoegtNy[i].NLeach_mgN_l_total);
                    tq.Write('\t');
                    tq.Write(newData.ElementAt(1).NLeach_mgN_l_korrigeret);
                    tq.WriteLine('\t');
                }
                tq.Write("Scenarie 3");
                tq.WriteLine('\t');
                for (int i = 0; i < olddata.ElementAt(place).Pakke3Ny.Count(); i++)
                {
                    tq.Write(olddata.ElementAt(place).Pakke3Ny[i].Referencesaedskifte);
                    tq.Write('\t');
                    tq.Write(olddata.ElementAt(place).Pakke3Ny[i].NLeach_KgN_ha);
                    tq.Write('\t');
                    tq.Write(newData.ElementAt(2).arealData.ElementAt(i).UdvaskningKgNHa);
                    tq.Write('\t');
                    tq.Write(olddata.ElementAt(place).Pakke3Ny[i].NLeach_mgN_l);
                    tq.Write('\t');
                    tq.Write(newData.ElementAt(2).arealData.ElementAt(i).UdvaskningMgPrL);
                    tq.Write('\t');
                    tq.Write(olddata.ElementAt(place).Pakke3Ny[i].NLeach_KgN_ha_total);
                    tq.Write('\t');
                    tq.Write(newData.ElementAt(2).NLeach_KgN_ha_korrigeret);
                    tq.Write('\t');
                    tq.Write(olddata.ElementAt(place).Pakke3Ny[i].NLeach_mgN_l_total);
                    tq.Write('\t');
                    tq.Write(newData.ElementAt(2).NLeach_mgN_l_korrigeret);
                    tq.WriteLine('\t');
                }
                tw.Close();
                tq.Close();
                place++;

            }
        }
        private void writePlotLog(List<DataFromOldWebserver> olddata, List<NyFarmNWebService2.returnItems> newData, long bedriftID)
        {
            StreamWriter tw = File.AppendText("c:\\plotlog.txt");
            StreamWriter tq = File.AppendText("c:\\plotresult.txt");

            if (olddata.ElementAt(place).farmID == bedriftID)
            {
                if (olddata.ElementAt(place).BeregnNudriftNy.Count() != newData.ElementAt(0).arealData.Count())
                    tw.WriteLine("not the same amount of elements in Scenarie 1 for " + bedriftID);
                if (olddata.ElementAt(place).BeregnAnsoegtNy.Count() != newData.ElementAt(1).arealData.Count())
                    tw.WriteLine("not the same amount of elements in Scenarie 2 for " + bedriftID);
                if (olddata.ElementAt(place).Pakke3Ny.Count() != newData.ElementAt(2).arealData.Count())
                    tw.WriteLine("not the same amount of elements in Scenarie 3 for " + bedriftID);
                if (newData.ElementAt(0).errorMessages.Count() > 0)
                {
                    foreach (NyFarmNWebService2.error m in newData.ElementAt(0).errorMessages)
                    {
                        tw.WriteLine(m.ErrorMessage);
                    }
                }
                decimal first = olddata.ElementAt(place).BeregnNudriftNy.ElementAt(0).NLeach_mgN_l_total;
                decimal second = newData.ElementAt(0).NLeach_mgN_l_korrigeret;
                if (Math.Abs(first - second) > 0.1m)
                    tw.WriteLine("not the same amount of NLeach_KgN_ha_total in Scenarie 1 for " + bedriftID + " Gl: " + first + " Ny: " + second);
                first = olddata.ElementAt(place).BeregnNudriftNy.ElementAt(0).NLeach_KgN_ha_total;
                second = newData.ElementAt(0).NLeach_KgN_ha_korrigeret;
                if (Math.Abs(first - second) > 0.1m)
                    tw.WriteLine("not the same amount of NLeach_KgN_ha_total in Scenarie 1 for " + bedriftID + " Gl: " + first + " Ny: " + second);
                decimal first1 = olddata.ElementAt(place).BeregnAnsoegtNy.ElementAt(0).NLeach_KgN_ha_total;
                decimal second1 = newData.ElementAt(1).NLeach_KgN_ha_korrigeret;
                if (Math.Abs(first1 - second1) > 0.1m)
                    tw.WriteLine("not the same amount of NLeach_KgN_ha_total in Scenarie 2 for " + bedriftID + " Gl: " + first1 + " Ny: " + second1);
                first1 = olddata.ElementAt(place).BeregnAnsoegtNy.ElementAt(0).NLeach_mgN_l_total;
                second1 = newData.ElementAt(1).NLeach_mgN_l_korrigeret;
                if (Math.Abs(first1 - second1) > 0.1m)
                    tw.WriteLine("not the same amount of NLeach_mgN_l_total in Scenarie 2 for " + bedriftID + " Gl: " + first1 + " Ny: " + second1);

                decimal first2 = olddata.ElementAt(place).Pakke3Ny.ElementAt(0).NLeach_KgN_ha_total;
                decimal second2 = newData.ElementAt(2).NLeach_KgN_ha_korrigeret;
                if (Math.Abs(first2 - second2) > 0.1m)
                    tw.WriteLine("not the same amount of NLeach_KgN_ha_total in Scenarie 3 for " + bedriftID + " Gl: " + first2 + " Ny: " + second2);
                first2 = olddata.ElementAt(place).Pakke3Ny.ElementAt(0).NLeach_mgN_l_total;
                second2 = newData.ElementAt(2).NLeach_mgN_l_korrigeret;
                if (Math.Abs(first2 - second2) > 0.1m)
                    tw.WriteLine("not the same amount of NLeach_mgN_l_total in Scenarie 3 for " + bedriftID + " Gl: " + first2 + " Ny: " + second2);
                tw.WriteLine("Compared " + bedriftID);

                if (olddata.ElementAt(place).BeregnNudriftNy.Count() != newData.ElementAt(0).arealData.Count())
                { }
                else if (olddata.ElementAt(place).BeregnAnsoegtNy.Count() != newData.ElementAt(1).arealData.Count())
                { }
                else if (olddata.ElementAt(place).Pakke3Ny.Count() != newData.ElementAt(2).arealData.Count())
                { }
                else
                {
                    tq.Write(bedriftID + " Scenarie 1");
                    tq.Write('\t');

                    //if (olddata.ElementAt(place).farmID == bedriftID)
                    //{
                    tq.Write(olddata.ElementAt(place).BeregnNudriftNy[0].Saedskifte);
                    tq.Write('\t');
                    tq.Write(olddata.ElementAt(place).BeregnNudriftNy[0].NLeach_KgN_ha_total);
                    tq.Write('\t');
                    tq.Write(newData.ElementAt(0).NLeach_KgN_ha_korrigeret);
                    tq.Write('\t');
                    tq.Write(olddata.ElementAt(place).BeregnNudriftNy[0].NLeach_mgN_l_total);
                    tq.Write('\t');
                    tq.Write(newData.ElementAt(0).NLeach_mgN_l_korrigeret);
                    tq.WriteLine('\t');
                    tq.Write("Scenarie 2");
                    tq.Write('\t');
                    tq.Write(olddata.ElementAt(place).BeregnAnsoegtNy[0].Referencesaedskifte);
                    tq.Write('\t');
                    tq.Write(olddata.ElementAt(place).BeregnAnsoegtNy[0].NLeach_KgN_ha_total);
                    tq.Write('\t');
                    tq.Write(newData.ElementAt(1).NLeach_KgN_ha_korrigeret);
                    tq.Write('\t');
                    tq.Write(olddata.ElementAt(place).BeregnAnsoegtNy[0].NLeach_mgN_l_total);
                    tq.Write('\t');
                    tq.Write(newData.ElementAt(1).NLeach_mgN_l_korrigeret);
                    tq.WriteLine('\t');
                    tq.Write("Scenarie 3");
                    tq.Write('\t');
                    tq.Write(olddata.ElementAt(place).Pakke3Ny[0].Referencesaedskifte);
                    tq.Write('\t');
                    tq.Write(olddata.ElementAt(place).Pakke3Ny[0].NLeach_KgN_ha_total);
                    tq.Write('\t');
                    tq.Write(newData.ElementAt(2).NLeach_KgN_ha_korrigeret);
                    tq.Write('\t');
                    tq.Write(olddata.ElementAt(place).Pakke3Ny[0].NLeach_mgN_l_total);
                    tq.Write('\t');
                    tq.Write(newData.ElementAt(2).NLeach_mgN_l_korrigeret);
                    tq.WriteLine('\t');
                }
            tq.Close();
            tw.Close();
            place++;
            }
        }
        private void writePlotLogAfterCrop(List<DataFromOldWebserver> olddata, List<NyFarmNWebService2.returnItems> newData, long bedriftID, decimal aftercropPercent)
        {
            StreamWriter tw = File.AppendText("c:\\AfterCropPlotlog.txt");
            StreamWriter tq = File.AppendText("c:\\AfterCropPlotresult.txt");
            int place = 0;
            if (olddata.ElementAt(place).farmID == bedriftID)
            {
                //if (olddata.ElementAt(place).BeregnNudriftNy.Count() != newData.ElementAt(0).arealData.Count())
                //    tw.WriteLine("not the same amount of elements in Scenarie 1 for " + bedriftID);
                if (olddata.ElementAt(place).BeregnAnsoegtNy.Count() != newData.ElementAt(0).arealData.Count())
                    tw.WriteLine("not the same amount of elements in Scenarie 2 for " + bedriftID);
                //if (olddata.ElementAt(place).Pakke3Ny.Count() != newData.ElementAt(2).arealData.Count())
                //    tw.WriteLine("not the same amount of elements in Scenarie 3 for " + bedriftID);
                if (newData.ElementAt(0).errorMessages.Count() > 0)
                {
                    foreach (NyFarmNWebService2.error m in newData.ElementAt(0).errorMessages)
                    {
                        tw.WriteLine(m.ErrorMessage);
                    }
                }
                //decimal first = olddata.ElementAt(place).BeregnNudriftNy.ElementAt(0).NLeach_mgN_l_total;
                //decimal second = newData.ElementAt(0).NLeach_mgN_l_korrigeret;
                //if (Math.Abs(first - second) > 0.1m)
                //    tw.WriteLine("not the same amount of NLeach_KgN_ha_total in Scenarie 1 for " + bedriftID + " Gl: " + first + " Ny: " + second);
                //first = olddata.ElementAt(place).BeregnNudriftNy.ElementAt(0).NLeach_KgN_ha_total;
                //second = newData.ElementAt(0).NLeach_KgN_ha_korrigeret;
                //if (Math.Abs(first - second) > 0.1m)
                //    tw.WriteLine("not the same amount of NLeach_KgN_ha_total in Scenarie 1 for " + bedriftID + " Gl: " + first + " Ny: " + second);
                decimal first1 = olddata.ElementAt(place).BeregnAnsoegtNy.ElementAt(0).NLeach_KgN_ha_total;
                decimal second1 = newData.ElementAt(0).NLeach_KgN_ha_korrigeret;
                if (Math.Abs(first1 - second1) > 0.1m)
                    tw.WriteLine("not the same amount of NLeach_KgN_ha_total in Scenarie 2 for " + bedriftID + " Gl: " + first1 + " Ny: " + second1);
                first1 = olddata.ElementAt(place).BeregnAnsoegtNy.ElementAt(0).NLeach_mgN_l_total;
                second1 = newData.ElementAt(0).NLeach_mgN_l_korrigeret;
                if (Math.Abs(first1 - second1) > 0.1m)
                    tw.WriteLine("not the same amount of NLeach_mgN_l_total in Scenarie 2 for " + bedriftID + " Gl: " + first1 + " Ny: " + second1);

                //decimal first2 = olddata.ElementAt(place).Pakke3Ny.ElementAt(0).NLeach_KgN_ha_total;
                //decimal second2 = newData.ElementAt(2).NLeach_KgN_ha_korrigeret;
                //if (Math.Abs(first2 - second2) > 0.1m)
                //    tw.WriteLine("not the same amount of NLeach_KgN_ha_total in Scenarie 3 for " + bedriftID + " Gl: " + first2 + " Ny: " + second2);
                //first2 = olddata.ElementAt(place).Pakke3Ny.ElementAt(0).NLeach_mgN_l_total;
                //second2 = newData.ElementAt(2).NLeach_mgN_l_korrigeret;
                //if (Math.Abs(first2 - second2) > 0.1m)
                //    tw.WriteLine("not the same amount of NLeach_mgN_l_total in Scenarie 3 for " + bedriftID + " Gl: " + first2 + " Ny: " + second2);
                tw.WriteLine("Compared " + bedriftID + " : "+aftercropPercent+"%");

                //if (olddata.ElementAt(place).BeregnNudriftNy.Count() != newData.ElementAt(0).arealData.Count())
                //{ }
                //else
                    if (olddata.ElementAt(place).BeregnAnsoegtNy.Count() != newData.ElementAt(0).arealData.Count())
                { }
                //else if (olddata.ElementAt(place).Pakke3Ny.Count() != newData.ElementAt(2).arealData.Count())
                //{ }
                else
                {
                    //tq.Write(bedriftID + " Scenarie 1");
                    //tq.Write('\t');

                    ////if (olddata.ElementAt(place).farmID == bedriftID)
                    ////{
                    //tq.Write(olddata.ElementAt(place).BeregnNudriftNy[0].Saedskifte);
                    //tq.Write('\t');
                    //tq.Write(olddata.ElementAt(place).BeregnNudriftNy[0].NLeach_KgN_ha_total);
                    //tq.Write('\t');
                    //tq.Write(newData.ElementAt(0).NLeach_KgN_ha_korrigeret);
                    //tq.Write('\t');
                    //tq.Write(olddata.ElementAt(place).BeregnNudriftNy[0].NLeach_mgN_l_total);
                    //tq.Write('\t');
                    //tq.Write(newData.ElementAt(0).NLeach_mgN_l_korrigeret);
                    //tq.WriteLine('\t');
                    tq.Write("Scenarie 2");
                    tq.Write('\t');
                    tq.Write(olddata.ElementAt(place).BeregnAnsoegtNy[0].Referencesaedskifte);
                    tq.Write('\t');
                    tq.Write(olddata.ElementAt(place).BeregnAnsoegtNy[0].NLeach_KgN_ha_total);
                    tq.Write('\t');
                    tq.Write(newData.ElementAt(0).NLeach_KgN_ha_korrigeret);
                    tq.Write('\t');
                    tq.Write(olddata.ElementAt(place).BeregnAnsoegtNy[0].NLeach_mgN_l_total);
                    tq.Write('\t');
                    tq.Write(newData.ElementAt(0).NLeach_mgN_l_korrigeret);
                    tq.WriteLine('\t');
                    //tq.Write("Scenarie 3");
                    //tq.Write('\t');
                    //tq.Write(olddata.ElementAt(place).Pakke3Ny[0].Referencesaedskifte);
                    //tq.Write('\t');
                    //tq.Write(olddata.ElementAt(place).Pakke3Ny[0].NLeach_KgN_ha_total);
                    //tq.Write('\t');
                    //tq.Write(newData.ElementAt(2).NLeach_KgN_ha_korrigeret);
                    //tq.Write('\t');
                    //tq.Write(olddata.ElementAt(place).Pakke3Ny[0].NLeach_mgN_l_total);
                    //tq.Write('\t');
                    //tq.Write(newData.ElementAt(2).NLeach_mgN_l_korrigeret);
                    //tq.WriteLine('\t');
                }
                tq.Close();
                tw.Close();
                //place++;
            }
        }
        public struct DataFromOldWebserver
        {
            public int farmID;
            public ServiceReferenceOld.FarmNData[] BeregnNudriftNy;
            public ServiceReferenceOld.FarmNData[] BeregnAnsoegtNy;
            public ServiceReferenceOld.FarmNData[] Pakke3Ny;
        };
        private List<DataFromOldWebserver> readOldFile()
        {
            List<DataFromOldWebserver> dataSet=new List<DataFromOldWebserver>();
            StreamReader sr = new StreamReader("c:\\date.txt");
            DataFromOldWebserver data = new DataFromOldWebserver();
            while (sr.Peek() >= 0)
            {
                String[] seperated = sr.ReadLine().Split('\t');

                if (seperated.Count() > 1)
                {
                    
                    if (seperated[1].CompareTo("BeregnNudriftNy") == 0)
                    {
                        data = new DataFromOldWebserver();
                        data.BeregnNudriftNy= readFarmData(sr);

                        Console.WriteLine("BeregnNudriftNy " + seperated[0]);
                    }
                    else if (seperated[1].CompareTo("BeregnAnsoegtNy") == 0)
                    {
                         data.BeregnAnsoegtNy=readFarmData(sr);
                        Console.WriteLine("BeregnAnsoegtNy " + seperated[0]);
                    }
                    else if (seperated[1].CompareTo("Pakke3Ny") == 0)
                    {
                        data.Pakke3Ny=readFarmData(sr);
                        data.farmID = Convert.ToInt32(seperated[0]);
                        dataSet.Add(data);

                        Console.WriteLine("Pakke3Ny " + seperated[0]);
                    }
                }
            }
            return dataSet;

        }
        private ServiceReferenceOld.FarmNData[] readFarmData(StreamReader file)
        {

            ServiceReferenceOld.FarmNData[] output = new ServiceReferenceOld.FarmNData[0];


            while (file.Peek()==48)
            {
                String[] seperated = file.ReadLine().Split('\t');
                ServiceReferenceOld.FarmNData anInstance = new ServiceReferenceOld.FarmNData();

                anInstance.Arealtype = Convert.ToInt32(seperated[0]);
                anInstance.Jordbundstype= seperated[1];
                anInstance.NLeach_KgN_ha= Convert.ToDecimal(seperated[2]);
                anInstance.NLeach_KgN_ha_total = Convert.ToDecimal(seperated[3]);

                anInstance.NLeach_mgN_l = Convert.ToDecimal(seperated[4]);
                anInstance.NLeach_mgN_l_total = Convert.ToDecimal(seperated[5]);
                anInstance.Referencesaedskifte= seperated[6];
                anInstance.Saedskifte= seperated[7];
                output = copyArray(anInstance, output);

            }
            return output;
        }
        private ServiceReferenceOld.FarmNData[] copyArray(ServiceReferenceOld.FarmNData newArrey, ServiceReferenceOld.FarmNData[] oldArray)
        {
            ServiceReferenceOld.FarmNData[] completlyNew =new ServiceReferenceOld.FarmNData[oldArray.Count() + 1];
            for (int i = 0; i < oldArray.Count(); i++)
            {
                completlyNew[i] = oldArray[i];
            }
            completlyNew[completlyNew.Count() - 1] = newArrey;
            return completlyNew;

        }
        private void printTofile(long ID, string callname, ServiceReferenceOld.FarmNData[] oldData)
        {
            StreamWriter tw = File.AppendText("c:\\OldData.txt");
            tw.Write(ID);
            tw.Write('\t');
            tw.Write(callname);
            tw.WriteLine('\t');
            for (int i = 0; i < oldData.Count(); i++)
            {
                tw.Write(oldData[i].Arealtype);
                tw.Write('\t');
                tw.Write(oldData[i].Jordbundstype);
                tw.Write('\t');
                tw.Write(oldData[i].NLeach_KgN_ha);
                tw.Write('\t');
                tw.Write(oldData[i].NLeach_KgN_ha_total);
                tw.Write('\t');
                tw.Write(oldData[i].NLeach_mgN_l);
                tw.Write('\t');
                tw.Write(oldData[i].NLeach_mgN_l_total);
                tw.Write('\t');
                tw.Write(oldData[i].Referencesaedskifte);
                tw.Write('\t');
                tw.Write(oldData[i].Saedskifte);
                tw.WriteLine();
            }
            tw.Close();

        }
        private void printTofile(NyFarmNWebService2.returnItems newData, ServiceReferenceOld.FarmNData[] oldData)
        {
            StreamWriter tw = File.AppendText("c:\\date.txt");
            if (newData.arealData.Count() != oldData.Count())
            {
                throw new ArgumentException("Not the same amount of data");
            }

           for(int i=0;i<newData.arealData.Count();i++)
            {
                tw.Write(newData.arealData.ElementAt(i).UdvaskningKgNHa);
                tw.Write('\t');
                tw.Write(newData.arealData.ElementAt(i).UdvaskningMgPrL);
                tw.Write('\t');
                tw.Write(oldData.ElementAt(i).NLeach_KgN_ha);
                tw.Write('\t');
                tw.Write(oldData.ElementAt(i).NLeach_mgN_l);
                tw.WriteLine();
            }
            tw.Close();
        }
        private void printTofile(NyFarmNWebService2.returnItems newData)
        {
            StreamWriter tw = File.AppendText("c:\\NewData.txt");
            for (int i = 0; i < newData.arealData.Count(); i++)
            {
                tw.Write(newData.arealData.ElementAt(i).UdvaskningKgNHa);
                tw.Write('\t');
                tw.Write(newData.arealData.ElementAt(i).UdvaskningMgPrL);
                tw.Write('\t');
            }
            tw.Close();
        }
        static int tmp = 0;
        static decimal tmpValue;
        int bedriftsIDThread;
        public void setBedriftsIDThread(int bedriftsIDThread)
        {
            this.bedriftsIDThread = bedriftsIDThread;
        }
        public void intervaltesting()
        {
     
            TextReader ArealListe = File.OpenText("C:\\ArealListe.txt");
            TextReader CodeAndOptions = File.OpenText("C:\\CodeAndOptions.txt");
            TextReader GoednListe = File.OpenText("C:\\GoednListe.txt");


            string InputFromArealListe = ArealListe.ReadLine();
            string InputFromCodeAndOptions = CodeAndOptions.ReadLine();
            string InputFromGoednListe = GoednListe.ReadLine();
            string[] informationAreal = new string[8];
            string[] goedningsmaengderListe = new string[6];
            string[] restList = new string[4];
            while (InputFromArealListe != null || InputFromCodeAndOptions != null || InputFromGoednListe != null)
            {

                //  OldService.FarmNWebServiceSoapClient oldService = new OldService.FarmNWebServiceSoapClient();

                string input = null;
                if (firstTime == true)
                {
                    string tmp = GoednListe.ReadLine();
                    goedningsmaengderListe = tmp.Split('\t');
                    informationAreal = ArealListe.ReadLine().Split('\t');
                }
                firstTime = false;
                input = CodeAndOptions.ReadLine();
                if (input == null)
                    break;
                restList = input.Split('\t');
                int removeFarmNr = -1;
                while (Convert.ToInt32(informationAreal[0]) != Convert.ToInt32(goedningsmaengderListe[0]) || Convert.ToInt32(informationAreal[0]) != Convert.ToInt32(restList[0]))
                {

                    if (Convert.ToInt32(informationAreal[0]) < Convert.ToInt32(goedningsmaengderListe[0]))
                    {
                        informationAreal = ArealListe.ReadLine().Split('\t');
                    }
                    else if (Convert.ToInt32(informationAreal[0]) > Convert.ToInt32(goedningsmaengderListe[0]))
                    {
                        goedningsmaengderListe = GoednListe.ReadLine().Split('\t');
                    }
                    if (Convert.ToInt32(informationAreal[0]) < Convert.ToInt32(restList[0]))
                    {
                        informationAreal = ArealListe.ReadLine().Split('\t');
                    }
                    else if (Convert.ToInt32(informationAreal[0]) > Convert.ToInt32(restList[0]))
                    {
                        restList = CodeAndOptions.ReadLine().Split('\t');
                    }

                }

                if (informationAreal[1].CompareTo("NULL") == 0)
                {
                    removeFarmNr = Convert.ToInt32(informationAreal[0]);

                }
                while (removeFarmNr == Convert.ToInt32(informationAreal[0]))
                {
                    input = ArealListe.ReadLine();
                    if (input != null)
                        informationAreal = input.Split('\t');
                }
                while (removeFarmNr == Convert.ToInt32(goedningsmaengderListe[0]))
                {

                    input = GoednListe.ReadLine();
                    if (input != null)
                        goedningsmaengderListe = input.Split('\t');
                }
                while (removeFarmNr == Convert.ToInt32(restList[0]))
                {

                    input = CodeAndOptions.ReadLine();
                    if (input != null)
                        restList = input.Split('\t');
                }

                List<areal> allareas = new List<areal>();
                
                int times = Convert.ToInt32(informationAreal[1]);
                NyFarmNWebService2.Areal[] allArea = new testOfEverything.NyFarmNWebService2.Areal[times];
                for (int f = 0; f < times; f++)
                {

                    NyFarmNWebService2.Areal area = new NyFarmNWebService2.Areal();


                    int i = Convert.ToInt32(informationAreal[2]);
                    area.ArealType = (NyFarmNWebService2.ArealTyper)i;
                    area.Referencesaedskifte = informationAreal[3];
                    area.Saedskifte = informationAreal[4];
                    area.Jordbundstype = informationAreal[5];
                    try
                    {
                        area.Ha = Convert.ToDecimal(informationAreal[6]);
                    }
                    catch
                    {
                        area.Ha = Decimal.Parse(informationAreal[6], System.Globalization.NumberStyles.Float);
                    }

                    allArea[f] = area;
                    input = ArealListe.ReadLine();
                    if (input != null)
                        informationAreal = input.Split('\t');
                    else
                        break;


                }

               
                times = Convert.ToInt32(goedningsmaengderListe[1]);
                NyFarmNWebService2.Goedningsmaengde[] allManure = new NyFarmNWebService2.Goedningsmaengde[times];
                for (int f = 0; f < times; f++)
                {

                    NyFarmNWebService2.Goedningsmaengde oneManure = new NyFarmNWebService2.Goedningsmaengde();
                    try
                    {
                        oneManure.KgN = Convert.ToDecimal(goedningsmaengderListe[2]);
                    }
                    catch
                    {
                        oneManure.KgN = Decimal.Parse(informationAreal[6], System.Globalization.NumberStyles.Float);
                    }


                    oneManure.GoedningstypeNUdnyttelsesProcent = Convert.ToDecimal(goedningsmaengderListe[3]);
                    NyFarmNWebService2.Goedningstype goedningstype1 = new NyFarmNWebService2.Goedningstype();
                    goedningstype1.Navn = goedningsmaengderListe[4];
 
                    oneManure.Goedningstype =goedningstype1;
                    allManure[f]=oneManure;
                    input = GoednListe.ReadLine();
                    if (input != null)
                        goedningsmaengderListe = input.Split('\t');
                    else
                        break;

                }

                long bedriftID = Convert.ToInt32(restList[0]);


                string zipCode = restList[1];

                bool option1 = Convert.ToBoolean(restList[2]);

                bool option2 = Convert.ToBoolean(restList[3]);
                if (bedriftsIDThread == bedriftID)
               {
                   

                    NyFarmNWebService2.FarmNWebService2010SoapClient nyService = new NyFarmNWebService2.FarmNWebService2010SoapClient();
                    StreamWriter outputNumbers = File.AppendText("C:\\number"+bedriftsIDThread.ToString()+".txt");
                    StreamWriter outputNumbers1 = File.AppendText("C:\\error" + bedriftsIDThread.ToString() + ".txt");
                    
                    tmp++;
                    
                    for (decimal i = 0; i <= 100.0m; i = i +=0.01m)
                    {
                        NyFarmNWebService2.returnItems item1 =new  NyFarmNWebService2.returnItems();
                        try
                        {

                          item1 = nyService.CalculateApplication(allArea, allManure, bedriftID, zipCode, i, 0, option1, option2, 3);
            
                        }
                        catch(Exception e)
                        {
                            Console.WriteLine();
                            Console.WriteLine(e.Message.ToString());
                            Console.WriteLine();

                            for (int j = 0; j < item1.errorMessages.Count; j++)
                            {
                                Console.WriteLine();
                                Console.WriteLine(j.ToString() + '\t' + item1.errorMessages[j].ErrorMessage.ToString());
                                Console.WriteLine();

                            }
                            

                            
                        }
                      
                        outputNumbers.WriteLine(i.ToString() + '\t' + item1.NLeach_KgN_ha_total);
                        for (int j = 0; j < item1.errorMessages.Count; j++)
                        {
                            outputNumbers1.WriteLine(j.ToString() + '\t' + item1.errorMessages[j].ErrorMessage.ToString());
                            
                        }
                        if (i % 0.5m == 0)
                        {
                            Console.WriteLine(i.ToString() + " " + bedriftID.ToString());
                          //  outputNumbers.Close();
                            //outputNumbers = File.AppendText("C:\\number.txt");
                        }
               
                    }
                    outputNumbers.WriteLine("done for " + bedriftID.ToString());
                    outputNumbers1.WriteLine("done for " + bedriftID.ToString());
                    outputNumbers.Close();
                    outputNumbers1.Close();
                    nyService.Close();
                    Console.WriteLine("done!");
                   }
            }
            ArealListe.Close();
            CodeAndOptions.Close();
            GoednListe.Close();
            
 
        }
    }
}
