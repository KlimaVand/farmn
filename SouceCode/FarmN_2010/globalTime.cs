using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
namespace FarmN_2010
{
    /// <summary>
    /// a class that keep track of times for different functions
    /// </summary>
    public class globalTime
    {
        /// <summary>
        /// This struck represented a functions time. It keeps track of how many times a function has been called, numbers of 
        /// times that function has been called, when it last time has been started, the min and max amount of time spent on a
        /// single call.
        /// </summary>
        public struct functionsTime
        {
            public int timeCalled;
            public TimeSpan timeUsed;
            public DateTime start;
            public int largeAmount;
            public int minAmount;
            public string name;
            public bool inUse;
        }
        private globalTime()
        {

            
        }
        /// <summary>
        /// write all the information about times recored in "C:\\time.txt"
        /// </summary>
        public void timeWrite()
        {
            if (globalSettings.Instance.getBasicOutput() == true)
            {
                TextWriter timeFile = new StreamWriter("C:\\time.txt");
                timeFile.WriteLine("Name" + '\t' + "total time" + '\t' + "avarage time" + '\t' + "min time" + '\t' + "max time" + '\t' + "time called");
                for (int i = 0; i < allFunctions.Count; i++)
                {
                    functionsTime tmp = allFunctions.ElementAt(i);
                    timeFile.Write(tmp.name.ToString() + '\t');
                    timeFile.Write(this.getTotal(i).ToString() + '\t');
                    timeFile.Write(this.getAvarage(i).ToString() + '\t');
                    timeFile.Write(this.getmin(i).ToString() + '\t');
                    timeFile.Write(this.getMax(i).ToString() + '\t');
                    timeFile.WriteLine(tmp.timeCalled.ToString() + '\t');
                }
                timeFile.Close();
            }

        }

        private static readonly globalTime _instance = new globalTime();
        /// <summary>
        /// access globalTime
        /// </summary>
        public static globalTime Instance
        {
            
            get
            {
                return _instance;
            }
        }
        static List<functionsTime> allFunctions =new List<functionsTime>();
        /// <summary>
        /// Start recording
        /// </summary>
        /// <param name="i">unique identifier </param>
        /// <param name="name">Name of the function one would record</param>
        public void start(int i, string name)
        {
            int tmpsk = allFunctions.Count;

            while (allFunctions.Count <= i || allFunctions.Count == 0)
            {
                functionsTime time = new functionsTime();
                time.inUse = false;
                allFunctions.Add(time);
                

            }
           
            {
               functionsTime tmp = allFunctions.ElementAt(i);
               allFunctions.Remove(tmp);
               if (tmp.inUse == false)
               {
                   tmp.timeCalled = 1;
                   tmp.start = DateTime.Now;
                   tmp.largeAmount = -1;
                   tmp.minAmount = 999999999;
                   tmp.name = name;
                   tmp.inUse = true;
               }
               else
               {
                   
                   tmp.start = DateTime.Now;

                   tmp.timeCalled += 1;
               }
               allFunctions.Insert(i,tmp);
            }

                 
        }
        /// <summary>
        /// stop with recording 
        /// </summary>
        /// <param name="i">same identifier as used in stating it</param>
        public void stop(int i)
        {
            functionsTime tmp = allFunctions.ElementAt(i);
            allFunctions.Remove(tmp);
            int timeAdded =TimeSpanToMiliSec (DateTime.Now - tmp.start);
            if (tmp.largeAmount < timeAdded)
                tmp.largeAmount = timeAdded;
            if (tmp.minAmount > timeAdded)
                tmp.minAmount = timeAdded;
            tmp.timeUsed = tmp.timeUsed+(DateTime.Now-tmp.start) ;
            
            allFunctions.Insert(i,tmp);
 


        }
        /// <summary>
        /// calculate total time in milisec
        /// </summary>
        /// <param name="timeUsed">the time mesured</param>
        /// <returns>totoal milisec</returns>
        public int TimeSpanToMiliSec(TimeSpan timeUsed)
        {
            int hours = timeUsed.Hours;
            int min = hours * 60 + timeUsed.Minutes;
            int sec = min * 60 + timeUsed.Seconds;

            int milisec = sec * 1000 + timeUsed.Milliseconds;
            return milisec;
        }
        /// <summary>
        /// avarage time for all a specefied function i
        /// </summary>
        /// <param name="i">identifier for function i</param>
        /// <returns>avare time in mili sec</returns>
        public int getAvarage(int i)
        {
            functionsTime tmp = allFunctions.ElementAt(i);

            int result = TimeSpanToMiliSec(tmp.timeUsed) / tmp.timeCalled;

            return result;
        }
        /// <summary>
        /// returns minimum time for function i
        /// </summary>
        /// <param name="i">identifier for function i</param>
        /// <returns>time</returns>
        public int getmin(int i)
        {
            functionsTime tmp = allFunctions.ElementAt(i);
            return tmp.minAmount;
        }
        /// <summary>
        /// returns max time for function i
        /// </summary>
        /// <param name="i">identifier for function i</param>
        /// <returns></returns>
        public int getMax(int i)
        {
            functionsTime tmp = allFunctions.ElementAt(i);
            return tmp.largeAmount;
        }
        /// <summary>
        /// return total time for function i
        /// </summary>
        /// <param name="i">identifier for function i</param>
        /// <returns></returns>
        public TimeSpan getTotal(int i)
        {
            functionsTime tmp = allFunctions.ElementAt(i);
            return tmp.timeUsed;
        }
    }
}
